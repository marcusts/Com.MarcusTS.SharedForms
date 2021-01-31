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
   using Interfaces;
   using SharedUtils.Utils;
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Text;
   using Utils;
   using ViewModels;
   using Xamarin.Forms;

   public interface IEntryValidationBehavior : ICanBeValid, INotifyPropertyChanged, ICommonValidationProps
   {
      string OriginalText { get; set; }

      bool TextMustChange { get; set; }

      string UnmaskedText { get; }

      string PrepareTextForEditing(string entryText, bool firstFocused = false);

      string StripMaskFromText(string text);
   }

   public class EntryValidationBehavior : Behavior<Entry>, IEntryValidationBehavior
   {
      private const char X_CHAR = 'X';

      private readonly Action _onIsValidChangedAction;
      private Entry _entry;

      private bool _ignoreTextChanged;
      private bool? _isValid = Extensions.EmptyNullableBool;

      private string _lastAssignedText;
      private string _mask;

      private IDictionary<int, char> _maskPositions;

      public EntryValidationBehavior(Action onIsValidChangedAction)
      {
         _onIsValidChangedAction = onIsValidChangedAction;
      }

      public event EventUtils.GenericDelegate<bool?> IsValidChanged;

      public bool DoNotForceMaskInitially { get; set; }
      public string ExcludedChars { get; set; }
      public bool IsFocused { get; private set; }

      public bool? IsValid
      {
         get => _isValid;

         private set
         {
            if (_entry == null)
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

      public string Mask
      {
         get => _mask;
         set
         {
            if (_mask.IsDifferentThan(value))
            {
               _mask = value;

               CreateMaskPositions();

               if (_mask.IsNotEmpty())
               {
                  MinLength = _mask.Length;
                  MaxLength = _mask.Length;

                  // Can't mask with numeric keyboard, etc.
                  if (_entry.IsNotNullOrDefault())
                  {
                     _entry.Keyboard = FormsConst.STANDARD_KEYBOARD;
                  }
               }
            }
         }
      }

      public int MaxLength { get; set; }
      public int MinLength { get; set; }
      public string OriginalText { get; set; }
      public bool TextMustChange { get; set; }
      public string UnmaskedText { get; private set; }
      public ValidationTypes ValidationType { get; set; }

      public static bool MinAnMaxLengthValidator(IEntryValidationBehavior behavior, string currentText)
      {
         return
            (behavior.MinLength == 0 || currentText.IsNotEmpty() && currentText.Length >= behavior.MinLength)
          &&
            (behavior.MaxLength == 0 || currentText.IsNotEmpty() && currentText.Length <= behavior.MaxLength);
      }

      public virtual string PrepareTextForEditing(string entryText, bool firstFocused = false)
      {
         return entryText;
      }

      public void Revalidate()
      {
         if (_entry == null)
         {
            IsValid = Extensions.EmptyNullableBool;
            return;
         }

         var isValid = IsWholeEntryValid(this, _entry.Text);
         IsValid = isValid;
      }

      public string StripMaskFromText(string text)
      {
         if (Mask.IsEmpty() || _maskPositions.IsEmpty() || text.IsEmpty())
         {
            return text;
         }

         // ELSE
         var strBuilder = new StringBuilder();

         for (var i = 0; i < text.Length; i++)
         {
            if (!_maskPositions.ContainsKey(i))
            {
               strBuilder.Append(text[i]);
            }
         }

         return strBuilder.ToString();
      }

      protected virtual string FormatDepartingText(string entryText)
      {
         return entryText;
      }

      protected virtual string IllegalCharFilter(
               IEntryValidationBehavior behavior,
         string newText,
         string originalText,
         out bool isOutsideOfRange)
      {
         isOutsideOfRange = false;

         // Works for max; not helpful for min -- the user can back-space until the string is empty.
         if (IsLongerThanMaxLength(behavior, newText))
         {
            return originalText;
         }

         if (ExcludedChars.IsNotEmpty())
         {
            var finalTextStrBuilder = new StringBuilder();

            foreach (var ch in newText)
            {
               if (!ExcludedChars.Contains(ch.ToString()))
               {
                  finalTextStrBuilder.Append(ch);
               }
            }

            newText = finalTextStrBuilder.ToString();
         }

         // ELSE success
         return newText;
      }

      protected virtual bool IsLongerThanMaxLength(IEntryValidationBehavior behavior, string newText)
      {
         return behavior.MaxLength > 0 && newText.IsNotEmpty() && newText.Length > behavior.MaxLength;
      }

      protected virtual bool IsWholeEntryValid(IEntryValidationBehavior behavior, string currentText)
      {
         if (!MinAnMaxLengthValidator(behavior, currentText))
         {
            return false;
         }

         // Check against the original text, if any
         if (TextMustChange)
         {
            return _entry.Text.IsDifferentThan(OriginalText);
         }

         // DEFAULT
         return true;
      }

      protected override void OnAttachedTo(Entry entry)
      {
         entry.TextChanged += OnTextChanged;
         entry.Focused += OnFocused;
         entry.Unfocused += OnUnfocused;

         base.OnAttachedTo(entry);

         _entry = entry;

         if (!DoNotForceMaskInitially)
         {
            ValidateText(_entry.Text, _entry.Text);
         }

         // Might be redundant in some cases but must occur.
         SetUnmaskedText();
         Revalidate();
      }

      protected override void OnDetachingFrom(Entry bindable)
      {
         bindable.TextChanged -= OnTextChanged;
         bindable.Focused -= OnFocused;
         bindable.Unfocused -= OnUnfocused;

         base.OnDetachingFrom(bindable);

         _entry = null;
      }

      protected virtual void OnFocused
      (
         object sender,
         FocusEventArgs e
      )
      {
         var strippedText = PrepareTextForEditing(_entry.Text);

         if (_entry.Text.IsDifferentThan(strippedText))
         {
            _ignoreTextChanged = true;
            _entry.Text = strippedText;
            _ignoreTextChanged = false;
         }

         IsFocused = true;
      }

      protected virtual void OnTextChanged
      (
         object sender,
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
         object sender,
         FocusEventArgs e
      )
      {
         _ignoreTextChanged = true;
         _entry.Text = FormatDepartingText(_entry.Text);
         _ignoreTextChanged = false;

         IsFocused = false;
      }

      private void CreateMaskPositions()
      {
         var dict = new Dictionary<int, char>();

         if (Mask.IsNotEmpty())
         {
            for (var i = 0; i < Mask.Length; i++)
            {
               if (Mask[i] != X_CHAR)
               {
                  dict.Add(i, Mask[i]);
               }
            }
         }

         _maskPositions = dict;
      }

      private void SetUnmaskedText()
      {
         UnmaskedText = StripMaskFromText(_entry.Text);
      }

      /// <remarks>All stripping is repeated ere for safety</remarks>
      private void ValidateText(string newText, string oldText)
      {
         if (_entry.IsNullOrDefault() || !_entry.IsFocused || newText.IsSameAs(_lastAssignedText))
         {
            return;
         }

         var preparedNewText = PrepareTextForEditing(newText);
         var preparedOldText = PrepareTextForEditing(oldText);

         // This is another "success" case, since there is nothing to do.
         if (preparedNewText.IsSameAs(_lastAssignedText))
         {
            return;
         }

         // Allow the deriving class to filter out illegal characters
         var filteredText = IllegalCharFilter(this, preparedNewText, preparedOldText, out var isOutsideOfRange);

         // If the illegal char func kills the current character, there is nothing else to do.
         //if (filteredText.IsSameAs(preparedOldText))
         //{
         //}

         //if (_entry.Text.IsDifferentThan(filteredText))
         //{
         //   _lastAssignedText  = filteredText;
         //   _ignoreTextChanged = true;
         //   _entry.Text        = filteredText;
         //   _ignoreTextChanged = false;
         //}

         preparedNewText = filteredText;

         if (preparedNewText.IsNotEmpty() && _maskPositions.IsNotEmpty() && preparedNewText.IsDifferentThan(_lastAssignedText))
         {
            foreach (var position in _maskPositions)
            {
               if (preparedNewText.Length >= position.Key + 1)
               {
                  var value = position.Value.ToString();
                  if (preparedNewText.Substring(position.Key, 1).IsDifferentThan(value))
                  {
                     preparedNewText = preparedNewText.Insert(position.Key, value);

                     // Insertion can potentially push a new character off the end of the mask and thereby exceed the max
                     // length
                     if (MaxLength > 0 && preparedNewText.Length > MaxLength)
                     {
                        preparedNewText = preparedNewText.Remove(preparedNewText.Length - 1);
                     }
                  }
               }
               /* CANT BACK-SPACE THROUGH A MASKED CHARACTER IF THIS IS TRUE

               // This should allow an append as well as an insert The key is the position inside th string. The length
               // is zero-based. If the masked position is 1 (the second position), and the string has 1 character, the
               // top test will be: "if 1 >= 2" That will fail to append. This test checks to see if the dictionary
               // position is to the right of the current character by one character.
               else if (preparedNewText.Length == position.Key)
               {
                  // The preparedNewText does not contain the mask (yet), so add it
                  preparedNewText += position.Value;

                  // Nothing else to do
                  break;
               }
               */
               else
               {
                  // Nothing else to do
                  break;
               }
            }
         }

         //if (preparedNewText.IsDifferentThan(preparedOldText))
         //{
            _lastAssignedText = preparedNewText;

            if (_entry.Text.IsDifferentThan(preparedNewText))
            {
               _ignoreTextChanged = true;
               _entry.Text = preparedNewText;
               _ignoreTextChanged = false;
            }

            SetUnmaskedText();
            Revalidate();
         //}

         _lastAssignedText = preparedNewText;
      }
   }
}
