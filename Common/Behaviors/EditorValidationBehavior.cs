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
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Text;
   using Interfaces;
   using SharedUtils.Utils;
   using Utils;
   using ViewModels;
   using Xamarin.Forms;

   public interface IEditorValidationBehavior : ICanBeValid, INotifyPropertyChanged
   {
      int MaxLength { get; set; }
      int MinLength { get; set; }
      string OriginalText { get; set; }
      bool TextMustChange { get; set; }
   }

   public class EditorValidationBehavior : Behavior<Editor>, IEditorValidationBehavior
   {
      private readonly Action _onIsValidChangedAction;
      private Editor _editor;
      private bool  _ignoreTextChanged;
      private bool? _isValid = Extensions.EmptyNullableBool;
      private string _lastAssignedText;

      public EditorValidationBehavior(Action onIsValidChangedAction)
      {
         _onIsValidChangedAction = onIsValidChangedAction;
      }

      public bool IsFocused { get; private set; }

      public event EventUtils.GenericDelegate<bool?> IsValidChanged;

      public bool DoNotForceMaskInitially { get; set; }

      public bool? IsValid
      {
         get => _isValid;

         private set
         {
            if (_editor == null)
            {
               _isValid = Extensions.EmptyNullableBool;
               return;
            }

            if (_isValid.IsDifferentThan(value))
            {
               _isValid = value;

               // Fire first so related validators can gety up to date
               IsValidChanged?.Invoke(_isValid);

               // Notify that a change has taken place Fire last because this is usually the highest level validator
               _onIsValidChangedAction?.Invoke();
            }
         }
      }

      public string LastValidationError { get; set; }

      public int             MaxLength      { get; set; }
      public int             MinLength      { get; set; }

      public void Revalidate()
      {
         if (_editor == null)
         {
            IsValid = Extensions.EmptyNullableBool;
            return;
         }

         var isValid = IsEditTextValid(this, _editor.Text);
         IsValid = isValid;
      }

      public static bool MinAnMaxLengthValidator(IEditorValidationBehavior behavior, string currentText)
      {
         return
            (behavior.MinLength == 0 || currentText.IsNotEmpty() && currentText.Length >= behavior.MinLength)
          &&
            (behavior.MaxLength == 0 || currentText.IsNotEmpty() && currentText.Length <= behavior.MaxLength);
      }

      protected virtual bool IsLongerThanMaxLength(IEditorValidationBehavior behavior, string newText)
      {
         return behavior.MaxLength > 0 && newText.IsNotEmpty() && newText.Length > behavior.MaxLength;
      }

      protected virtual bool IsEditTextValid(IEditorValidationBehavior behavior, string currentText)
      {
         if (!MinAnMaxLengthValidator(behavior, currentText))
         {
            return false;
         }

         // Check against the original text, if any
         if (TextMustChange)
         {
            return _editor.Text.IsDifferentThan(OriginalText);
         }

         // DEFAULT
         return true;
      }

      protected override void OnAttachedTo(Editor editor)
      {
         editor.TextChanged += OnTextChanged;
         editor.Focused     += OnFocused;
         editor.Unfocused   += OnUnfocused;

         base.OnAttachedTo(editor);

         _editor = editor;

         Revalidate();
      }

      protected override void OnDetachingFrom(Editor bindable)
      {
         bindable.TextChanged -= OnTextChanged;
         bindable.Focused     -= OnFocused;
         bindable.Unfocused   -= OnUnfocused;

         base.OnDetachingFrom(bindable);

         _editor = null;
      }

      protected virtual void OnFocused
      (
         object         sender,
         FocusEventArgs e
      )
      {
         IsFocused = true;
      }

      protected virtual void OnTextChanged
      (
         object               sender,
         TextChangedEventArgs e
      )
      {
         if (_ignoreTextChanged)
         {
            return;
         }

         ValidateText(e.NewTextValue, e.OldTextValue);
      }

      protected virtual void OnUnfocused
      (
         object         sender,
         FocusEventArgs e
      )
      {
         IsFocused = false;
      }

      private void ValidateText(string newText, string oldText)
      {
         if (_editor.IsNullOrDefault() || !_editor.IsFocused || newText.IsSameAs(_lastAssignedText))
         {
            return;
         }

         if (newText.IsDifferentThan(oldText))
         {
            _lastAssignedText = newText;

            if (_editor.Text.IsDifferentThan(newText))
            {
               _ignoreTextChanged = true;
               _editor.Text        = newText;
               _ignoreTextChanged = false;
            }

            Revalidate();
         }

         _lastAssignedText = newText;
      }

      public string OriginalText   { get; set; }

      public bool   TextMustChange { get; set; }
   }
}
