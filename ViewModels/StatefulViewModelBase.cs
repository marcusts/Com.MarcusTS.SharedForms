#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, StatefulViewModelBase.cs, is a part of a program called AccountViewMobile.
//
// AccountViewMobile is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Permission to use, copy, modify, and/or distribute this software
// for any purpose with or without fee is hereby granted, provided
// that the above copyright notice and this permission notice appear
// in all copies.
//
// AccountViewMobile is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// For the complete GNU General Public License,
// see <http://www.gnu.org/licenses/>.

#endregion

namespace Com.MarcusTS.SharedForms.ViewModels
{
   using Common.Behaviors;
   using Common.Interfaces;
   using Common.Notifications;
   using Common.Utils;
   using SharedUtils.Utils;
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Linq;
   using System.Linq.Expressions;
   using System.Reflection;
   using System.Threading.Tasks;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IStatefulViewModel
   /// Implements the <see cref="Com.MarcusTS.SharedForms.ViewModels.IViewModelBase" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Interfaces.ICanPullToRefresh" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.ViewModels.IViewModelBase" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Interfaces.ICanPullToRefresh" />
   public interface IStatefulViewModel : IViewModelBase, ICanPullToRefresh
   {
      // Reports if any of the managed properties has changed
      /// <summary>
      /// Gets a value indicating whether [any property value has changed].
      /// </summary>
      /// <value><c>true</c> if [any property value has changed]; otherwise, <c>false</c>.</value>
      bool AnyPropertyValueHasChanged { get; }

      // A temporary bool to prevent us from accessing the "save" button during an actual save or refresh.
      /// <summary>
      /// Gets a value indicating whether this instance is refreshing.
      /// </summary>
      /// <value><c>true</c> if this instance is refreshing; otherwise, <c>false</c>.</value>
      bool IsRefreshing { get; }

      // Reports whether the page can be saved; usually this is the same as PageIsValid, but there are subtleties.
      /// <summary>
      /// Gets a value indicating whether [page can be saved].
      /// </summary>
      /// <value><c>null</c> if [page can be saved] contains no value, <c>true</c> if [page can be saved]; otherwise,
      /// <c>false</c>.</value>
      bool? PageCanBeSaved { get; }

      // Reports if the page has been validated successfully.
      /// <summary>
      /// Gets a value indicating whether [page is valid].
      /// </summary>
      /// <value><c>null</c> if [page is valid] contains no value, <c>true</c> if [page is valid]; otherwise, <c>false</c>.</value>
      bool? PageIsValid { get; }

      // If set true, the user can save even if they have not changed any properties.
      // This is only the case where there are no editable properties.
      /// <summary>
      /// Gets a value indicating whether [proceed without property changes].
      /// </summary>
      /// <value><c>true</c> if [proceed without property changes]; otherwise, <c>false</c>.</value>
      bool ProceedWithoutPropertyChanges { get; }

      // Adds the prescribed behaviors to the view model.
      // This makes them accessible and manageable by the view model.
      /// <summary>
      /// Adds the behaviors.
      /// </summary>
      /// <param name="behaviors">The behaviors.</param>
      void AddBehaviors(params IEntryValidationBehavior[] behaviors);

      // This class is an "editable buffer" of properties.
      // This is why any deriver of StatefulViewModel always must state the generic type T,
      // which is the type of class that holds the properties being edited.
      // Since this class and its source (LocalSettings) contain the same set of property declarations,
      // we can copy them from one place to another using reflection.
      // This method is used at startup to assign the view model's properties to the values derived from the original source,
      // usually inside LocalSettings.
      /// <summary>
      /// Copies the original values to live values.
      /// </summary>
      void CopyOriginalValuesToLiveValues();

      // Responds to validations.
      /// <summary>
      /// Handles the input validation changed.
      /// </summary>
      void HandleInputValidationChanged();

      // Refreshes the validation state of all properties.
      /// <summary>
      /// Revalidates the behaviors.
      /// </summary>
      void RevalidateBehaviors();

      // A specialized method that restores the state of the view model to when it was originally constructed.
      // This occurs when the user backs out of some operation, or if the operation fails.
      // If we don't do this, we cannot set the correct state of various buttons, especially Save.
      // We have lost the state of the view model vs. its original data.
      // This method restores it.
      /// <summary>
      /// Reverts to fail safe original values.
      /// </summary>
      void RevertToFailSafeOriginalValues();

      // The opposite of the method above; this one copies from this class back to the original source.
      // It is used when saving after a user edit.
      /// <summary>
      /// Saves the live values back to original values.
      /// </summary>
      void SaveLiveValuesBackToOriginalValues();

      // Notifies listeners that the bool PageCanBeSaved (see above) has changed.
      // event EventUtils.GenericEventHandler<bool> NotifyPageCanBeSavedChanged;
   }

   /// <summary>
   /// A view model specialized at inputting and saving values.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.ViewModels.ViewModelBase" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.ViewModels.IStatefulViewModel" />
   /// </summary>
   /// <typeparam name="InterfaceT">The type of interface storing the savable values -
   /// can be the entire original view model or a sub-set.</typeparam>
   /// <typeparam name="ClassT">The type of class to be instantiated to create saved values.</typeparam>
   /// <seealso cref="Com.MarcusTS.SharedForms.ViewModels.ViewModelBase" />
   /// <seealso cref="Com.MarcusTS.SharedForms.ViewModels.IStatefulViewModel" />
   public abstract class StatefulViewModelBase<InterfaceT, ClassT> : ViewModelBase, IStatefulViewModel
      where InterfaceT : class
      where ClassT : InterfaceT, new()
   {
      /// <summary>
      /// The behaviors
      /// </summary>
      private readonly IList<IEntryValidationBehavior> _behaviors = new List<IEntryValidationBehavior>();

      /// <summary>
      /// The cached property infos
      /// </summary>
      protected readonly PropertyInfo[] _cachedPropInfos;

      /// <summary>
      /// Any property value has changed
      /// </summary>
      private bool _anyPropertyValueHasChanged;

      /// <summary>
      /// The is initialized
      /// </summary>
      private bool _isInitialized;

      /// <summary>
      /// The original values
      /// </summary>
      private InterfaceT _originalValues;

      /// <summary>
      /// The page can be saved
      /// </summary>
      private bool? _pageCanBeSaved;

      /// <summary>
      /// The page is valid
      /// </summary>
      private bool? _pageIsValid;

      /// <summary>
      /// The revertable original values
      /// </summary>
      private InterfaceT _revertableOriginalValues;

      /// <summary>
      /// The start up entered
      /// </summary>
      private bool _startUpEntered;

      /// <summary>
      /// The view model
      /// </summary>
      private INotifyPropertyChanged _viewModel;

      /// <summary>
      /// Initializes a new instance of the <see cref="StatefulViewModelBase{InterfaceT, ClassT}" /> class.
      /// </summary>
      protected StatefulViewModelBase()
      {
         // A bit clunky, but not sure what else to do -- if this fails, the class is being used improperly.
         ErrorUtils.ConsiderArgumentError(!(this is InterfaceT),
                                          "The stateful view model base ->" + GetType() +
                                          "must implement interface type "  + typeof(InterfaceT));

         // Stash the set of reflected properties for use in this class.
         _cachedPropInfos = Extensions.GetAllPropInfos<InterfaceT>();

         SetUpFodyPropertyChangedListener(() => AnyPropertyValueHasChanged);

         RefreshDataCommand =
            new Command(
               async () =>
               {
                  IsRefreshing = true;

                  if (Device.RuntimePlatform.IsSameAs(Device.Android))
                  {
                     /* TODO
                     SetIsBusyGlobally(true);
                     */
                  }

                  RefreshDataCommand.ChangeCanExecute();

                  await RefreshDataAndOverwriteExisting().WithoutChangingContext();

                  if (Device.RuntimePlatform.IsSameAs(Device.Android))
                  {
                     /* TODO
                     SetIsBusyGlobally(false);
                     */
                     RaiseForceRefreshUIView();
                  }

                  IsRefreshing = false;
                  RefreshDataCommand.ChangeCanExecute();
               },
               () => !IsRefreshing
            );

         // NOTE: Derivers must call InitializeOriginalValues !!!
      }

      /// <summary>
      /// Gets the this as interface t.
      /// </summary>
      /// <value>The this as interface t.</value>
      private InterfaceT ThisAsInterfaceT => this as InterfaceT;

      /// <summary>
      /// Gets a value indicating whether [any property value has changed].
      /// </summary>
      /// <value><c>true</c> if [any property value has changed]; otherwise, <c>false</c>.</value>
      public bool AnyPropertyValueHasChanged
      {
         get => _anyPropertyValueHasChanged;

         private set
         {
            _anyPropertyValueHasChanged = value;
            OnAnyPropertyValueHasChanged();
            ResetPageCanBeSaved();
         }
      }

      /// <summary>
      /// Gets a value indicating whether this instance is refreshing.
      /// </summary>
      /// <value><c>true</c> if this instance is refreshing; otherwise, <c>false</c>.</value>
      public bool IsRefreshing { get; private set; }

      /// <summary>
      /// Gets or sets a value indicating whether [page can be saved].
      /// </summary>
      /// <value><c>null</c> if [page can be saved] contains no value, <c>true</c> if [page can be saved]; otherwise,
      /// <c>false</c>.</value>
      public bool? PageCanBeSaved
      {
         get => _pageCanBeSaved;

         set
         {
            // On reversion, we might not have a different value, but must still enforce this (especially raising the event).
            _pageCanBeSaved = value;

            FormsMessengerUtils.Send(new NotifyPageCanBeSavedChangedMessage {Payload = this});
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether [page is valid].
      /// </summary>
      /// <value><c>null</c> if [page is valid] contains no value, <c>true</c> if [page is valid]; otherwise, <c>false</c>.</value>
      public bool? PageIsValid
      {
         get => _pageIsValid;

         protected set
         {
            // On reversion, we might not have a different value, but must still enforce this (especially raising the event).
            //if ( _pageIsValid.IsNotTheSame( value ) )
            //{
            _pageIsValid = value;

            ResetPageCanBeSaved();

            //}
         }
      }

      /// <summary>
      /// If true, we will allow the user to "save" even if no change has taken place.
      /// The default is false.
      /// </summary>
      /// <value><c>true</c> if [proceed without property changes]; otherwise, <c>false</c>.</value>
      public bool ProceedWithoutPropertyChanges { get; protected set; }

      /// <summary>
      /// Gets the refresh data command.
      /// </summary>
      /// <value>The refresh data command.</value>
      public Command RefreshDataCommand { get; }

      /// <summary>
      /// The deriver can add behaviors as properties, but *MUST* add them here so they
      /// will validate - !!!
      /// </summary>
      /// <param name="behaviors">The behaviors.</param>
      public void AddBehaviors(params IEntryValidationBehavior[] behaviors)
      {
         foreach (var behavior in behaviors)
         {
            if (!_behaviors.Contains(behavior))
            {
               _behaviors.Add(behavior);
            }
         }

         RevalidateBehaviors();
      }

      /// <summary>
      /// Copies the original values to live values.
      /// </summary>
      public void CopyOriginalValuesToLiveValues()
      {
         // See constructor for precautions taken to protect this type-cast.
         ThisAsInterfaceT.CopySettablePropertyValuesFrom(_originalValues, _cachedPropInfos);

         AnyPropertyValueHasChanged = false;
      }

      /// <summary>
      /// Handles the input validation changed.
      /// </summary>
      public virtual void HandleInputValidationChanged()
      {
         PageIsValid = _behaviors.IsEmpty() || _behaviors.All(b => b.IsValid.IsTrue());
      }

      /// <summary>
      /// Revalidates the behaviors.
      /// </summary>
      public void RevalidateBehaviors()
      {
         // Run through all behaviors; ask to validate; respond only once at the end
         if (_behaviors.IsNotEmpty())
         {
            foreach (var behavior in _behaviors)
            {
               behavior.Revalidate();
            }
         }

         HandleInputValidationChanged();
      }

      /// <summary>
      /// Copy the safe-safe original values back to the original values,
      /// then re-validate.  This is called when we have already destroyed the
      /// state of the original values at an attempt to save, but then failed to save.
      /// It restores the state of the UI based on differences between the "live" values
      /// and the *real* original values (before edit and save).
      /// </summary>
      public void RevertToFailSafeOriginalValues()
      {
         // Restore the original values from the very beginning.
         // Do *not* copy back to "this" -- those are live values that should be preserved.
         _originalValues.CopySettablePropertyValuesFrom(_revertableOriginalValues, _cachedPropInfos);

         // Re-run the checker to see if any values have changed, and to test whether they are valid.
         ResetAnyPropertyValueHasChanged();

         // This also resets PageIsValid and PageCanBeSaved.  That in turn refreshes all command can-executes.
         HandleInputValidationChanged();
      }

      /// <summary>
      /// Saves the live values back to original values.
      /// </summary>
      public void SaveLiveValuesBackToOriginalValues()
      {
         // See constructor for precautions taken to protect this type-cast.
         _originalValues.CopySettablePropertyValuesFrom(ThisAsInterfaceT, _cachedPropInfos);

         AnyPropertyValueHasChanged = false;
      }

      /// <summary>
      /// as the single property has changed.
      /// </summary>
      /// <param name="propertyName">Name of the property.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      protected bool ASinglePropertyHasChanged(string propertyName)
      {
         return (this as InterfaceT).AnySettablePropertyHasChanged(_originalValues, propertyName, _cachedPropInfos);
      }

      /// <summary>
      /// REQUIRED at the deriving constructor !!!
      /// </summary>
      /// <param name="originalValues">The original values.</param>
      protected void InitializeOriginalValues(InterfaceT originalValues)
      {
         // This data is a full class, including functionality, so the assignment remains permanently.
         _originalValues = originalValues;

         // This secondary set allows us to revert after attempting to save.
         _revertableOriginalValues = Activator.CreateInstance(typeof(ClassT)) as InterfaceT;
         _revertableOriginalValues.CopySettablePropertyValuesFrom(_originalValues, _cachedPropInfos);

         StartUpStatefulViewModel();

         // Safety measure -- we must not react to property changes until the reflection has completed.
         _isInitialized = true;

         // SetIsBusyGlobally(false);
      }

      /// <summary>
      /// Called when [any property value has changed].
      /// </summary>
      protected virtual void OnAnyPropertyValueHasChanged()
      {
      }

      /// <summary>
      /// Raises the force refresh UI view.
      /// </summary>
      protected void RaiseForceRefreshUIView()
      {
         FormsMessengerUtils.Send(new RefreshUIViewMessage {Payload = this});
      }

      /// <summary>
      /// Refreshes the data.
      /// </summary>
      /// <returns>Task.</returns>
      protected virtual Task RefreshData()
      {
         return Task.FromResult(true);
      }

      /// <summary>
      /// Refreshes the data and overwrite existing.
      /// </summary>
      /// <returns>Task.</returns>
      protected async Task RefreshDataAndOverwriteExisting()
      {
         await RefreshData().WithoutChangingContext();

         // The new data must load in on top of the existing values, or the changes will not be clear to the user.
         // NOTE that the user will lose their current changes -- !!!
         StartUpStatefulViewModel();

         RaiseForceRefreshUIView();
      }

      /// <summary>
      /// Resets the page can be saved.
      /// </summary>
      protected void ResetPageCanBeSaved()
      {
         PageCanBeSaved = PageIsValid.IsTrue() && (AnyPropertyValueHasChanged || ProceedWithoutPropertyChanges);
      }

      /// <summary>
      /// Resets any property value has changed.
      /// </summary>
      private void ResetAnyPropertyValueHasChanged()
      {
         AnyPropertyValueHasChanged = _isInitialized &&
                                      ThisAsInterfaceT.AnySettablePropertyHasChanged(_originalValues, string.Empty,
                                                                                     _cachedPropInfos);
      }

      /// <summary>
      /// Sets up fody property changed listener.
      /// </summary>
      /// <param name="propExpression">The property expression.</param>
      private void SetUpFodyPropertyChangedListener(Expression<Func<bool>> propExpression)
      {
         var member = propExpression.Body as MemberExpression;

         ErrorUtils.ConsiderArgumentError(member.IsNullOrDefault(),
                                          $"SetUpFodyPropertyChangedListener: Expression '{propExpression}' must be a property.");

         var expression = member?.Expression as ConstantExpression;

         ErrorUtils.ConsiderArgumentError(expression.IsNullOrDefault(),
                                          $"SetUpFodyPropertyChangedListener: Expression '{propExpression}' must be a constant expression");

         // This is a sneaky way to refer to ourselves -- but only after Fody weaves us into the proper implementation .
         _viewModel = expression?.Value as INotifyPropertyChanged;

         ErrorUtils.ConsiderArgumentError(_viewModel.IsNullOrDefault(),
                                          $"SetUpFodyPropertyChangedListener: Expression '{propExpression}'.Value must implement INotifyPropertyChanged");

         // On any property change, we re-check to see if
         if (_viewModel != null)
         {
            _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
         }
      }

      /// <summary>
      /// Starts up stateful view model.
      /// </summary>
      private void StartUpStatefulViewModel()
      {
         // A precaution -- if we start up automatically, and then receive a data event, we might recur
         if (_startUpEntered)
         {
            Debug.WriteLine("SERIOUS ERROR: Recursive call to StatefulViewModelBase.Startup");
            return;
         }

         _startUpEntered = true;

         // Copy from the settings using reflection
         // NOTE that if the data is ever refreshed, all live values will be replaced by these new ones.
         // In that case, the user will lose their work.
         // The only other solution is to merge (very carefully).
         CopyOriginalValuesToLiveValues();

         // Unfortunately, we have to default to true because when behaviors are missing, this might never get set properly.
         // If behaviors do exist, they auto-fire the validation check on their own.
         PageIsValid = true;

         // This bool indicates that a change has been made *and* that the page is valid.
         // It always starts as null.
         PageCanBeSaved = Extensions.EmptyNullableBool;

         // Monitors property changes
         AnyPropertyValueHasChanged = false;

         _startUpEntered = false;
      }

      /// <summary>
      /// Views the model on property changed.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="propertyChangedEventArgs">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
      private void ViewModelOnPropertyChanged
      (
         object                   sender,
         PropertyChangedEventArgs propertyChangedEventArgs
      )
      {
         ResetAnyPropertyValueHasChanged();
      }
   }
}