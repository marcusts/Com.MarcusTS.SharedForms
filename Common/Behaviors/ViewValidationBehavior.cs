// ********************************************************************************* <copyright
// file=ValidationBehavior.cs company="Marcus Technical Services, Inc."> Copyright @2019 Marcus Technical Services, Inc.
// </copyright>
//
// MIT License
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
// Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// *********************************************************************************

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using System;
   using System.ComponentModel;
   using Interfaces;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   public interface IViewValidationBehavior : ICanBeValid, INotifyPropertyChanged
   {
      bool EmptyAllowed { get; set; }

      // CALL REVALIDATE ON ANY VALUE CHANGED

      bool IsNumeric { get; set; }
   }

   public class ViewValidationBehavior : Behavior<View>, IViewValidationBehavior
   {
      private readonly Action _onIsValidChangedAction;

      private View _view;

      private bool? _isValid = Extensions.EmptyNullableBool;
      private readonly Func<View, object> _getValueFromViewFunc;
      private bool _emptyAllowed;
      private readonly bool _isNumeric;

      public ViewValidationBehavior(Func<View, object> getValueFromViewFunc, Action onIsValidChangedAction)
      {
         _getValueFromViewFunc = getValueFromViewFunc;
         _onIsValidChangedAction = onIsValidChangedAction;
      }

      public bool IsFocused { get; private set; }

      protected virtual Func<IViewValidationBehavior, object, bool> Validator { get; set; } = DefaultValidator;

      public event EventUtils.GenericDelegate<bool?> IsValidChanged;

      public bool EmptyAllowed
      {
         get => _emptyAllowed;
         set
         {
            _emptyAllowed = value;
            Revalidate();
         }
      }

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

            if (_isValid.IsDifferentThan(value))
            {
               _isValid = value;

               // Notify that a change has taken place
               _onIsValidChangedAction?.Invoke();

               IsValidChanged?.Invoke(_isValid);
            }
         }
      }

      public string LastValidationError { get; set; }

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

      public static bool DefaultValidator(IViewValidationBehavior behavior, object o)
      {
         return behavior.EmptyAllowed || (!behavior.IsNumeric && o.IsNotNullOrDefault()) || behavior.IsNumeric && IsANumberGreaterThanZero(o);
      }

      private static bool IsANumberGreaterThanZero(object o)
      {
         if (double.TryParse(o?.ToString(), out var testDouble))
         {
            return (testDouble.IsGreaterThan(0));
         }

         return false;
      }

      protected override void OnAttachedTo(View view)
      {
         view.Focused     += OnFocused;
         view.Unfocused   += OnUnfocused;

         base.OnAttachedTo(view);

         _view = view;

         Revalidate();
      }

      protected override void OnDetachingFrom(View bindable)
      {
         bindable.Focused     -= OnFocused;
         bindable.Unfocused   -= OnUnfocused;

         base.OnDetachingFrom(bindable);

         _view = null;
      }

      protected virtual void OnFocused
      (
         object o,
         FocusEventArgs e
      )
      {
         IsFocused = true;
      }

      protected virtual void OnUnfocused
      (
         object o,
         FocusEventArgs e
      )
      {
         IsFocused = false;
      }

      public bool IsNumeric { get; set; }
   }
}
