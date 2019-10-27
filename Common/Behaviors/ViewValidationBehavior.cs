#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ViewValidationBehavior.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using Interfaces;
   using SharedUtils.Utils;
   using System;
   using System.ComponentModel;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IViewValidationBehavior
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Interfaces.ICanBeValid" />
   /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Interfaces.ICanBeValid" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IViewValidationBehavior : ICanBeValid, INotifyPropertyChanged
   {
      /// <summary>
      /// Gets or sets a value indicating whether [empty allowed].
      /// </summary>
      /// <value><c>true</c> if [empty allowed]; otherwise, <c>false</c>.</value>
      bool EmptyAllowed { get; set; }

      // CALL REVALIDATE ON ANY VALUE CHANGED

      /// <summary>
      /// Gets or sets a value indicating whether this instance is numeric.
      /// </summary>
      /// <value><c>true</c> if this instance is numeric; otherwise, <c>false</c>.</value>
      bool IsNumeric { get; set; }
   }

   /// <summary>
   /// Class ViewValidationBehavior.
   /// Implements the <see cref="Xamarin.Forms.Behavior{Xamarin.Forms.View}" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Behaviors.IViewValidationBehavior" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Behavior{Xamarin.Forms.View}" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Behaviors.IViewValidationBehavior" />
   public class ViewValidationBehavior : Behavior<View>, IViewValidationBehavior
   {
      /// <summary>
      /// The get value from view function
      /// </summary>
      private readonly Func<View, object> _getValueFromViewFunc;
      /// <summary>
      /// The is numeric
      /// </summary>
      private readonly bool               _isNumeric;
      /// <summary>
      /// The on is valid changed action
      /// </summary>
      private readonly Action             _onIsValidChangedAction;

      /// <summary>
      /// The empty allowed
      /// </summary>
      private bool  _emptyAllowed;
      /// <summary>
      /// The is valid
      /// </summary>
      private bool? _isValid = Extensions.EmptyNullableBool;
      /// <summary>
      /// The view
      /// </summary>
      private View  _view;

      /// <summary>
      /// Initializes a new instance of the <see cref="ViewValidationBehavior" /> class.
      /// </summary>
      /// <param name="getValueFromViewFunc">The get value from view function.</param>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public ViewValidationBehavior(Func<View, object> getValueFromViewFunc, Action onIsValidChangedAction)
      {
         _getValueFromViewFunc   = getValueFromViewFunc;
         _onIsValidChangedAction = onIsValidChangedAction;
      }

      /// <summary>
      /// Gets a value indicating whether this instance is focused.
      /// </summary>
      /// <value><c>true</c> if this instance is focused; otherwise, <c>false</c>.</value>
      public bool                                        IsFocused { get; private set; }
      /// <summary>
      /// Gets or sets the validator.
      /// </summary>
      /// <value>The validator.</value>
      protected virtual Func<IViewValidationBehavior, object, bool> Validator { get; set; } = DefaultValidator;

      /// <summary>
      /// Occurs when [is valid changed].
      /// </summary>
      public event EventUtils.GenericDelegate<bool?> IsValidChanged;

      /// <summary>
      /// Gets or sets a value indicating whether [empty allowed].
      /// </summary>
      /// <value><c>true</c> if [empty allowed]; otherwise, <c>false</c>.</value>
      public bool EmptyAllowed
      {
         get => _emptyAllowed;
         set
         {
            _emptyAllowed = value;
            Revalidate();
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is numeric.
      /// </summary>
      /// <value><c>true</c> if this instance is numeric; otherwise, <c>false</c>.</value>
      public bool IsNumeric { get; set; }

      /// <summary>
      /// Returns true if ... is valid.
      /// </summary>
      /// <value><c>null</c> if [is valid] contains no value, <c>true</c> if [is valid]; otherwise, <c>false</c>.</value>
      public bool? IsValid
      {
         get => _isValid;

         private set
         {
            if (_view == null)
            {
               _isValid = Extensions.EmptyNullableBool;
               return;
            }

            if (_isValid.IsNotTheSame(value))
            {
               _isValid = value;

               // Notify that a change has taken place
               _onIsValidChangedAction?.Invoke();

               IsValidChanged?.Invoke(_isValid);
            }
         }
      }

      /// <summary>
      /// Gets or sets the last validation error.
      /// </summary>
      /// <value>The last validation error.</value>
      public string LastValidationError { get; set; }

      /// <summary>
      /// Revalidates this instance.
      /// </summary>
      public void Revalidate()
      {
         if (_view.IsNullOrDefault() || Validator.IsNullOrDefault())
         {
            IsValid = Extensions.EmptyNullableBool;
            return;
         }

         var isValid = Validator.Invoke(this, _getValueFromViewFunc?.Invoke(_view));

         IsValid = isValid;
      }

      /// <summary>
      /// Defaults the validator.
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="o">The o.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool DefaultValidator(IViewValidationBehavior behavior, object o)
      {
         return behavior.EmptyAllowed || !behavior.IsNumeric && o.IsNotNullOrDefault() ||
                behavior.IsNumeric                           && IsANumberGreaterThanZero(o);
      }

      /// <summary>
      /// Called when [attached to].
      /// </summary>
      /// <param name="view">The view.</param>
      protected override void OnAttachedTo(View view)
      {
         view.Focused   += OnFocused;
         view.Unfocused += OnUnfocused;

         base.OnAttachedTo(view);

         _view = view;

         Revalidate();
      }

      /// <summary>
      /// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(`0)" /> method and then detaches from the superclass.
      /// </summary>
      /// <param name="bindable">The bindable object from which the behavior was detached.</param>
      /// <remarks>To be added.</remarks>
      protected override void OnDetachingFrom(View bindable)
      {
         bindable.Focused   -= OnFocused;
         bindable.Unfocused -= OnUnfocused;

         base.OnDetachingFrom(bindable);

         _view = null;
      }

      /// <summary>
      /// Handles the <see cref="E:Focused" /> event.
      /// </summary>
      /// <param name="o">The o.</param>
      /// <param name="e">The <see cref="FocusEventArgs" /> instance containing the event data.</param>
      protected virtual void OnFocused
      (
         object         o,
         FocusEventArgs e
      )
      {
         IsFocused = true;
      }

      /// <summary>
      /// Handles the <see cref="E:Unfocused" /> event.
      /// </summary>
      /// <param name="o">The o.</param>
      /// <param name="e">The <see cref="FocusEventArgs" /> instance containing the event data.</param>
      protected virtual void OnUnfocused
      (
         object         o,
         FocusEventArgs e
      )
      {
         IsFocused = false;
      }

      /// <summary>
      /// Determines whether [is a number greater than zero] [the specified o].
      /// </summary>
      /// <param name="o">The o.</param>
      /// <returns><c>true</c> if [is a number greater than zero] [the specified o]; otherwise, <c>false</c>.</returns>
      private static bool IsANumberGreaterThanZero(object o)
      {
         if (double.TryParse(o?.ToString(), out var testDouble))
         {
            return testDouble.IsGreaterThan(0);
         }

         return false;
      }
   }
}