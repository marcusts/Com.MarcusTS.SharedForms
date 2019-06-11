// *********************************************************************************
// <copyright file=BehaviorBase.cs company="Marcus Technical Services, Inc.">
//     Copyright @2019 Marcus Technical Services, Inc.
// </copyright>
//
// MIT License
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// *********************************************************************************

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using Com.MarcusTS.SharedForms.Common.Utils;
   using Com.MarcusTS.SharedUtils.Utils;
   using System;
   using System.ComponentModel;
   using Xamarin.Forms;

   /// <summary>
   ///    Interface IBehaviorBase
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Behaviors.IIsValid" />
   ///    Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Behaviors.IIsValid" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IBehaviorBase : IIsValid, INotifyPropertyChanged
   {
      //bool IsFocused { get; }

      /// <summary>
      ///    Revalidates this instance.
      /// </summary>
      void Revalidate();
   }

   /// <summary>
   ///    Base abstract class for managing complex validations, including validating
   ///    as the user types, as well as validating after user input has completed.
   ///    Implements the <see cref="Xamarin.Forms.Behavior{Xamarin.Forms.Entry}" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Behaviors.IBehaviorBase" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Behavior{Xamarin.Forms.Entry}" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Behaviors.IBehaviorBase" />
   public abstract class BehaviorBase : Behavior<Entry>, IBehaviorBase
   {
      /// <summary>
      ///    The illegal character filter
      /// </summary>
      private readonly Func<string, string> _illegalCharFilter;

      /// <summary>
      ///    The on is valid changed action
      /// </summary>
      private readonly Action _onIsValidChangedAction;

      /// <summary>
      ///    The bindable
      /// </summary>
      private Entry _bindable;

      /// <summary>
      ///    The is valid
      /// </summary>
      private bool? _isValid;

#if PREVENT_LOSS_OF_FOCUS
      private DateTime _focusedStartTime = DateTime.Now;
#endif

      // private static readonly TimeSpan MINIMUM_FOCUS_TIME = TimeSpan.FromMilliseconds(150);

      /// <summary>
      ///    Initializes a new instance of the <see cref="BehaviorBase" /> class.
      /// </summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      /// <param name="validator">The validator.</param>
      /// <param name="illegalCharFilter">The illegal character filter.</param>
      protected BehaviorBase
      (
         Action                            onIsValidChangedAction,
         Func<IBehaviorBase, string, bool> validator,
         Func<string, string>              illegalCharFilter = null
      )
      {
         _onIsValidChangedAction = onIsValidChangedAction;
         Validator               = validator;
         _illegalCharFilter      = illegalCharFilter;

         // Needs to show a change when set either way --
         _isValid = Extensions.EmptyNullableBool;
      }

      /// <summary>
      ///    Gets a value indicating whether this instance is focused.
      /// </summary>
      /// <value><c>true</c> if this instance is focused; otherwise, <c>false</c>.</value>
      public bool IsFocused { get; private set; }

      /// <summary>
      ///    Returns true if ... is valid.
      /// </summary>
      /// <value><c>null</c> if [is valid] contains no value, <c>true</c> if [is valid]; otherwise, <c>false</c>.</value>
      public bool? IsValid
      {
         get => _isValid;

         private set
         {
            if (_bindable == null)
            {
               _isValid = Extensions.EmptyNullableBool;
               return;
            }

            if (_isValid.IsNotTheSame(value))
            {
               _isValid = value;

               var simpleValidBool = _isValid.IsTrue();

               _bindable.BackgroundColor = simpleValidBool ? Color.White : ColorUtils.ILLEGAL_ENTRY_COLOR;

               // Notify that a change has taken place
               _onIsValidChangedAction?.Invoke();
            }
         }
      }

      /// <summary>
      ///    Gets or sets the validator.
      /// </summary>
      /// <value>The validator.</value>
      protected Func<IBehaviorBase, string, bool> Validator { get; set; }

      /// <summary>
      ///    Revalidates this instance.
      /// </summary>
      public void Revalidate()
      {
         if (_bindable == null || Validator == null)
         {
            IsValid = Extensions.EmptyNullableBool;
            return;
         }

         // Auto-validate initially on binding
         var isValid = Validator.Invoke(this, _bindable.Text);

         IsValid = isValid;
      }

      /// <summary>
      ///    Attaches to the superclass and then calls the <see cref="M:Xamarin.Forms.Behavior`1.OnAttachedTo(T)" /> method
      ///    on this object.
      /// </summary>
      /// <param name="bindable">To be added.</param>
      /// <remarks>To be added.</remarks>
      protected override void OnAttachedTo(Entry bindable)
      {
         bindable.TextChanged += OnTextChanged;
         bindable.Focused     += OnFocused;
         bindable.Unfocused   += OnUnfocused;

         base.OnAttachedTo(bindable);

         _bindable = bindable;

         Revalidate();
      }

      /// <summary>
      ///    Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(`0)" /> method and then detaches from the
      ///    superclass.
      /// </summary>
      /// <param name="bindable">The bindable object from which the behavior was detached.</param>
      /// <remarks>To be added.</remarks>
      protected override void OnDetachingFrom(Entry bindable)
      {
         bindable.TextChanged -= OnTextChanged;
         bindable.Focused     -= OnFocused;
         bindable.Unfocused   -= OnUnfocused;

         base.OnDetachingFrom(bindable);

         _bindable = null;
      }

      /// <summary>
      ///    Handles the <see cref="E:Focused" /> event.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="FocusEventArgs" /> instance containing the event data.</param>
      protected virtual void OnFocused
      (
         object         sender,
         FocusEventArgs e
      )
      {
         IsFocused = true;
      }

#if PREVENT_LOSS_OF_FOCUS
         _focusedStartTime = DateTime.Now;
#endif

      /// <summary>
      ///    Handles the <see cref="E:TextChanged" /> event.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="TextChangedEventArgs" /> instance containing the event data.</param>
      protected virtual void OnTextChanged
      (
         object               sender,
         TextChangedEventArgs e
      )
      {
         if (_bindable == null)
         {
            return;
         }

         // Allow the deriving class to filter out illegal characters
         if (_bindable.IsFocused && _illegalCharFilter != null)
         {
            _bindable.Text = _illegalCharFilter(e.NewTextValue);
         }

         // Now see if the text has really changed - including text upper/lower case
         if (_bindable.Text.IsDifferentThan(e.OldTextValue))
         {
            Revalidate();
         }
      }

      /// <summary>
      ///    Handles the <see cref="E:Unfocused" /> event.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="FocusEventArgs" /> instance containing the event data.</param>
      protected virtual void OnUnfocused
      (
         object         sender,
         FocusEventArgs e
      ) =>
#if PREVENT_LOSS_OF_FOCUS
         if (_focusedStartTime.Subtract(DateTime.Now) < MINIMUM_FOCUS_TIME)
         {
            _bindable.Focus();
         }
         else
         {
#endif
         IsFocused = false;

#if PREVENT_LOSS_OF_FOCUS
         }
#endif
   }
}