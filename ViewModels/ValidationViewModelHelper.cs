#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ValidationViewModelHelper.cs, is a part of a program called AccountViewMobile.
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

#define FORCE_REVALIDATION

namespace Com.MarcusTS.SharedForms.ViewModels
{
   using Common.Interfaces;
   using SharedUtils.Utils;
   using System.Collections.Generic;
   using System.Linq;

   /// <summary>
   /// Interface IHaveValidationViewModelHelper
   /// </summary>
   public interface IHaveValidationViewModelHelper
   {
      /// <summary>
      /// Gets or sets the validation helper.
      /// </summary>
      /// <value>The validation helper.</value>
      IValidationViewModelHelper ValidationHelper { get; set; }
   }

   /// <summary>
   /// Interface IValidationViewModelHelper
   /// Implements the <see cref="Com.MarcusTS.SharedForms.ViewModels.IBindableViewModel" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.ViewModels.IBindableViewModel" />
   public interface IValidationViewModelHelper : IBindableViewModel
   {
      /// <summary>
      /// Gets a value indicating whether [any property value has changed].
      /// </summary>
      /// <value><c>true</c> if [any property value has changed]; otherwise, <c>false</c>.</value>
      bool AnyPropertyValueHasChanged            { get; }
      /// <summary>
      /// Gets or sets a value indicating whether [multiple sub helpers must all validate true].
      /// </summary>
      /// <value><c>true</c> if [multiple sub helpers must all validate true]; otherwise, <c>false</c>.</value>
      bool MultipleSubHelpersMustAllValidateTrue { get; set; }
      /// <summary>
      /// Gets or sets a value indicating whether [page is never valid].
      /// </summary>
      /// <value><c>true</c> if [page is never valid]; otherwise, <c>false</c>.</value>
      bool PageIsNeverValid                      { get; set; }
      /// <summary>
      /// Gets a value indicating whether [page is valid].
      /// </summary>
      /// <value><c>true</c> if [page is valid]; otherwise, <c>false</c>.</value>
      bool PageIsValid                           { get; }
      /// <summary>
      /// Gets a value indicating whether [proceed without property changes].
      /// </summary>
      /// <value><c>true</c> if [proceed without property changes]; otherwise, <c>false</c>.</value>
      bool ProceedWithoutPropertyChanges         { get; }
      /// <summary>
      /// Gets or sets a value indicating whether [validates true when empty].
      /// </summary>
      /// <value><c>true</c> if [validates true when empty]; otherwise, <c>false</c>.</value>
      bool ValidatesTrueWhenEmpty                { get; set; }

      /// <summary>
      /// Occurs when [page is valid changed].
      /// </summary>
      event EventUtils.GenericDelegate<bool> PageIsValidChanged;

      /// <summary>
      /// Adds the behaviors.
      /// </summary>
      /// <param name="behaviors">The behaviors.</param>
      void AddBehaviors(ICanBeValid[]                              behaviors);

      /// <summary>
      /// Adds the sub view model helpers.
      /// </summary>
      /// <param name="helpers">The helpers.</param>
      void AddSubViewModelHelpers(IHaveValidationViewModelHelper[] helpers);

      /// <summary>
      /// Gets the behaviors.
      /// </summary>
      /// <returns>ICanBeValid[].</returns>
      ICanBeValid[]                          GetBehaviors();

      /// <summary>
      /// Kills the behaviors.
      /// </summary>
      void KillBehaviors();

      /// <summary>
      /// Removes the sub view model helpers.
      /// </summary>
      /// <param name="helpers">The helpers.</param>
      void RemoveSubViewModelHelpers(IHaveValidationViewModelHelper[] helpers);

      /// <summary>
      /// Revalidates the behaviors.
      /// </summary>
      /// <param name="forceAll">if set to <c>true</c> [force all].</param>
      void RevalidateBehaviors(bool                                   forceAll);

      /// <summary>
      /// Revalidates the behaviors.
      /// </summary>
      void RevalidateBehaviors();
   }

   /// <summary>
   /// Class ValidationViewModelHelper.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.ViewModels.BindableViewModel" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.ViewModels.IValidationViewModelHelper" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.ViewModels.BindableViewModel" />
   /// <seealso cref="Com.MarcusTS.SharedForms.ViewModels.IValidationViewModelHelper" />
   public class ValidationViewModelHelper : BindableViewModel, IValidationViewModelHelper
   {
      /// <summary>
      /// The behaviors
      /// </summary>
      private readonly IList<ICanBeValid> _behaviors = new List<ICanBeValid>();

      /// <summary>
      /// The sub view model helpers
      /// </summary>
      private readonly IList<IHaveValidationViewModelHelper> _subViewModelHelpers =
         new List<IHaveValidationViewModelHelper>();

      /// <summary>
      /// Any property value has changed
      /// </summary>
      private volatile bool _anyPropertyValueHasChanged;
      /// <summary>
      /// The page is never valid
      /// </summary>
      private volatile bool _pageIsNeverValid;
      /// <summary>
      /// The page is valid
      /// </summary>
      private volatile bool _pageIsValid;
      /// <summary>
      /// The revalidate behaviors entered
      /// </summary>
      private volatile bool _revalidateBehaviorsEntered;

      /// <summary>
      /// Initializes a new instance of the <see cref="ValidationViewModelHelper" /> class.
      /// </summary>
      public ValidationViewModelHelper()
      {
         AnyPropertyValueHasChanged = false;
         PageIsValid                = ValidatesTrueWhenEmpty;
      }

      /// <summary>
      /// Occurs when [page is valid changed].
      /// </summary>
      public event EventUtils.GenericDelegate<bool> PageIsValidChanged;

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
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether [multiple sub helpers must all validate true].
      /// </summary>
      /// <value><c>true</c> if [multiple sub helpers must all validate true]; otherwise, <c>false</c>.</value>
      public bool MultipleSubHelpersMustAllValidateTrue { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [page is never valid].
      /// </summary>
      /// <value><c>true</c> if [page is never valid]; otherwise, <c>false</c>.</value>
      public bool PageIsNeverValid
      {
         get => _pageIsNeverValid;
         set
         {
            _pageIsNeverValid = value;

            if (_pageIsNeverValid)
            {
               PageIsValid = false;
            }
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether [page is valid].
      /// </summary>
      /// <value><c>true</c> if [page is valid]; otherwise, <c>false</c>.</value>
      public bool PageIsValid
      {
         get => _pageIsValid;
         protected set
         {
            if (_pageIsValid != value && !PageIsNeverValid)
            {
               _pageIsValid = value;
               PageIsValidChanged?.Invoke(_pageIsValid);
            }
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether [proceed without property changes].
      /// </summary>
      /// <value><c>true</c> if [proceed without property changes]; otherwise, <c>false</c>.</value>
      public bool ProceedWithoutPropertyChanges { get; protected set; }
      /// <summary>
      /// Gets or sets a value indicating whether [validates true when empty].
      /// </summary>
      /// <value><c>true</c> if [validates true when empty]; otherwise, <c>false</c>.</value>
      public bool ValidatesTrueWhenEmpty        { get; set; }

      /// <summary>
      /// Adds the behaviors.
      /// </summary>
      /// <param name="behaviors">The behaviors.</param>
      public void AddBehaviors(ICanBeValid[] behaviors)
      {
         foreach (var behavior in behaviors)
         {
            if (!_behaviors.Contains(behavior))
            {
               behavior.IsValidChanged += b => { RevalidateBehaviors(); };
               _behaviors.Add(behavior);
            }
         }

         RevalidateBehaviors();
      }

      /// <summary>
      /// Adds the sub view model helpers.
      /// </summary>
      /// <param name="helpers">The helpers.</param>
      public void AddSubViewModelHelpers(IHaveValidationViewModelHelper[] helpers)
      {
         foreach (var helper in helpers)
         {
            _subViewModelHelpers.Add(helper);
         }
      }

      /// <summary>
      /// Gets the behaviors.
      /// </summary>
      /// <returns>ICanBeValid[].</returns>
      public ICanBeValid[] GetBehaviors()
      {
         return _behaviors.ToArray();
      }

      /// <summary>
      /// Kills the behaviors.
      /// </summary>
      public void KillBehaviors()
      {
         _behaviors.Clear();
      }

      /// <summary>
      /// Removes the sub view model helpers.
      /// </summary>
      /// <param name="helpers">The helpers.</param>
      public void RemoveSubViewModelHelpers(IHaveValidationViewModelHelper[] helpers)
      {
         foreach (var helper in helpers)
         {
            if (_subViewModelHelpers.Contains(helper))
            {
               _subViewModelHelpers.Remove(helper);
            }
         }
      }

      /// <summary>
      /// Revalidates the behaviors.
      /// </summary>
      /// <param name="forceAll">if set to <c>true</c> [force all].</param>
      public void RevalidateBehaviors(bool forceAll)
      {
         if (_revalidateBehaviorsEntered)
         {
            return;
         }

         _revalidateBehaviorsEntered = true;

         if (_subViewModelHelpers.IsNotEmpty())
         {
            PageIsValid =
               ValidatesTrueWhenEmpty && _subViewModelHelpers.IsEmpty()
             ||
               MultipleSubHelpersMustAllValidateTrue &&
               _subViewModelHelpers.All(h => h.ValidationHelper.PageIsValid)
             ||
               !MultipleSubHelpersMustAllValidateTrue &&
               _subViewModelHelpers.Any(h => h.ValidationHelper.PageIsValid);
         }
         else
         {
            if (forceAll)
            {
               // Run through all behaviors; ask to validate; respond only once at the end
               if (_behaviors.IsNotEmpty())
               {
                  foreach (var behavior in _behaviors)
                  {
                     behavior.Revalidate();
                  }
               }
            }

            PageIsValid = _behaviors.IsEmpty() || _behaviors.All(b => b.IsValid.IsTrue());
         }

         _revalidateBehaviorsEntered = false;
      }

      /// <summary>
      /// Revalidates the behaviors.
      /// </summary>
      public void RevalidateBehaviors()
      {
         RevalidateBehaviors(
#if FORCE_REVALIDATION
            true
#else
            false
#endif
         );
      }

      /// <summary>
      /// Called when [any property value has changed].
      /// </summary>
      protected virtual void OnAnyPropertyValueHasChanged()
      {
      }
   }
}