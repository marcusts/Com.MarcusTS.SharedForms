// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, EntryValidationBehavior.cs, is a part of a program called AccountViewMobile.
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

#define HACK_IS_VALID

using Com.MarcusTS.SharedUtils.Utils;

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using Interfaces;
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Text;
   using Utils;
   using Xamarin.Forms;

   /// <summary>
   /// Enum ValidationTypes
   /// </summary>
   public enum ValidationTypes
   {
      /// <summary>
      /// Any text
      /// </summary>
      AnyText,

      /// <summary>
      /// The whole number
      /// </summary>
      WholeNumber,

      /// <summary>
      /// The decimal number
      /// </summary>
      DecimalNumber
   }

   /// <summary>
   /// Interface ICommonValidationProps
   /// </summary>
   public interface ICommonValidationProps
   {
      /// <summary>
      /// Gets or sets a value indicating whether [do not force mask initially].
      /// </summary>
      /// <value><c>true</c> if [do not force mask initially]; otherwise, <c>false</c>.</value>
      bool DoNotForceMaskInitially { get; set; }

      /// <summary>
      /// Gets or sets the mask.
      /// </summary>
      /// <value>The mask.</value>
      string Mask { get; set; }

      /// <summary>
      /// Gets or sets the maximum length.
      /// </summary>
      /// <value>The maximum length.</value>
      int MaxLength { get; set; }

      /// <summary>
      /// Gets or sets the minimum length.
      /// </summary>
      /// <value>The minimum length.</value>
      int MinLength { get; set; }

      /// <summary>
      /// Gets or sets the type of the validation.
      /// </summary>
      /// <value>The type of the validation.</value>
      ValidationTypes ValidationType { get; set; }
   }

   /// <summary>
   /// Interface IEntryValidationBehavior
   /// Implements the <see cref="ICanBeValid" />
   /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// Implements the <see cref="ICommonValidationProps" />
   /// </summary>
   /// <seealso cref="ICanBeValid" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   /// <seealso cref="ICommonValidationProps" />
   public interface IEntryValidationBehavior : ICanBeValid, INotifyPropertyChanged, ICommonValidationProps
   {
      /// <summary>
      /// Gets or sets the original text.
      /// </summary>
      /// <value>The original text.</value>
      string OriginalText { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [text must change].
      /// </summary>
      /// <value><c>true</c> if [text must change]; otherwise, <c>false</c>.</value>
      bool TextMustChange { get; set; }

      /// <summary>
      /// Gets the unmasked text.
      /// </summary>
      /// <value>The unmasked text.</value>
      string UnmaskedText { get; }

      /// <summary>
      /// Prepares the text for editing.
      /// </summary>
      /// <param name="entryText">The entry text.</param>
      /// <param name="firstFocused">if set to <c>true</c> [first focused].</param>
      /// <returns>System.String.</returns>
      string PrepareTextForEditing(string entryText, bool firstFocused = false);

      /// <summary>
      /// Strips the mask from text.
      /// </summary>
      /// <param name="text">The text.</param>
      /// <returns>System.String.</returns>
      string StripMaskFromText(string text);
   }

   /// <summary>
   /// Class EntryValidationBehavior.
   /// Implements the <see cref="Xamarin.Forms.Behavior{Xamarin.Forms.Entry}" />
   /// Implements the <see cref="IEntryValidationBehavior" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Behavior{Xamarin.Forms.Entry}" />
   /// <seealso cref="IEntryValidationBehavior" />
   public class EntryValidationBehavior : Behavior<Entry>, IEntryValidationBehavior
   {
      /// <summary>
      /// The x character
      /// </summary>
      private const char X_CHAR = 'X';

      /// <summary>
      /// The on is valid changed action
      /// </summary>
      private readonly Action _onIsValidChangedAction;

      /// <summary>
      /// The entry
      /// </summary>
      private Entry _entry;

      /// <summary>
      /// The ignore text changed
      /// </summary>
      private bool _ignoreTextChanged;

      /// <summary>
      /// The is valid
      /// </summary>
      private bool? _isValid = Extensions.EmptyNullableBool;

      /// <summary>
      /// The last assigned text
      /// </summary>
      private string _lastAssignedText;

      /// <summary>
      /// The mask
      /// </summary>
      private string _mask;

      /// <summary>
      /// The mask positions
      /// </summary>
      private IDictionary<int, char> _maskPositions;

      /// <summary>
      /// Initializes a new instance of the <see cref="EntryValidationBehavior" /> class.
      /// </summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public EntryValidationBehavior(Action onIsValidChangedAction)
      {
         _onIsValidChangedAction = onIsValidChangedAction;
      }

      /// <summary>
      /// Occurs when [is valid changed].
      /// </summary>
      public event EventUtils.GenericDelegate<bool?> IsValidChanged;

      /// <summary>
      /// Gets or sets a value indicating whether [do not force mask initially].
      /// </summary>
      /// <value><c>true</c> if [do not force mask initially]; otherwise, <c>false</c>.</value>
      public bool DoNotForceMaskInitially { get; set; }

      /// <summary>
      /// Gets a value indicating whether this instance is focused.
      /// </summary>
      /// <value><c>true</c> if this instance is focused; otherwise, <c>false</c>.</value>
      public bool IsFocused { get; private set; }

      public bool? IsValid
      {
         get => _isValid;

#if !HACK_IS_VALID
         private set
         {
            SetIsValid(value);
         }
#endif
      }

      /// <summary>
      ///    Gets or sets the last validation error.
      /// </summary>
      /// <value>The last validation error.</value>
      public string LastValidationError { get; set; }

      /// <summary>
      ///    Gets or sets the mask.
      /// </summary>
      /// <value>The mask.</value>
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

      /// <summary>
      /// Gets or sets the maximum length.
      /// </summary>
      /// <value>The maximum length.</value>
      public int MaxLength { get; set; }

      /// <summary>
      /// Gets or sets the minimum length.
      /// </summary>
      /// <value>The minimum length.</value>
      public int MinLength { get; set; }

      /// <summary>
      /// Gets or sets the original text.
      /// </summary>
      /// <value>The original text.</value>
      public string OriginalText { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [text must change].
      /// </summary>
      /// <value><c>true</c> if [text must change]; otherwise, <c>false</c>.</value>
      public bool TextMustChange { get; set; }

      /// <summary>
      /// Gets the unmasked text.
      /// </summary>
      /// <value>The unmasked text.</value>
      public string UnmaskedText { get; private set; }

      /// <summary>
      /// Gets or sets the type of the validation.
      /// </summary>
      /// <value>The type of the validation.</value>
      public ValidationTypes ValidationType { get; set; }

      /// <summary>
      /// Minimums an maximum length validator.
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="currentText">The current text.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool MinAnMaxLengthValidator(IEntryValidationBehavior behavior, string currentText)
      {
         return
            (behavior.MinLength == 0 || currentText.IsNotEmpty() && currentText.Length >= behavior.MinLength)
          &&
            (behavior.MaxLength == 0 || currentText.IsNotEmpty() && currentText.Length <= behavior.MaxLength);
      }

      /// <summary>
      ///
      /// </summary>
      public void Neutralize()
      {
#if HACK_IS_VALID
         SetIsValid(default);
#else
         IsValid = default;
#endif
      }

      /// <summary>
      /// Prepares the text for editing.
      /// </summary>
      /// <param name="entryText">The entry text.</param>
      /// <param name="firstFocused">if set to <c>true</c> [first focused].</param>
      /// <returns>System.String.</returns>
      public virtual string PrepareTextForEditing(string entryText, bool firstFocused = false)
      {
         return entryText;
      }

      /// <summary>
      /// Revalidates this instance.
      /// </summary>
      public void RevalidateEditorText()
      {
         if (_entry == null)
         {
#if HACK_IS_VALID
            SetIsValid(Extensions.EmptyNullableBool);
#else
            IsValid = Extensions.EmptyNullableBool;
#endif
            return;
         }

         var isValid = IsWholeEntryValid(this, _entry.Text);

#if HACK_IS_VALID
         SetIsValid(isValid);
#else
         IsValid = isValid;
#endif
      }

      /// <summary>
      /// Strips the mask from text.
      /// </summary>
      /// <param name="text">The text.</param>
      /// <returns>System.String.</returns>
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

      /// <summary>
      /// Formats the departing text.
      /// </summary>
      /// <param name="entryText">The entry text.</param>
      /// <returns>System.String.</returns>
      protected virtual string FormatDepartingText(string entryText)
      {
         return entryText;
      }

      /// <summary>
      /// Illegals the character filter.
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="newText">The new text.</param>
      /// <param name="originalText">The original text.</param>
      /// <param name="isOutsideOfRange">if set to <c>true</c> [is outside of range].</param>
      /// <returns>System.String.</returns>
      protected virtual string IllegalCharFilter(
         IEntryValidationBehavior behavior,
         string newText,
         string originalText,
         out bool isOutsideOfRange)
      {
         isOutsideOfRange = false;

         if (IsLongerThanMaxLength(behavior, newText))
         {
            return originalText;
         }

         // ELSE success
         return newText;
      }

      /// <summary>
      /// Determines whether [is longer than maximum length] [the specified behavior].
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="newText">The new text.</param>
      /// <returns><c>true</c> if [is longer than maximum length] [the specified behavior]; otherwise, <c>false</c>.</returns>
      protected virtual bool IsLongerThanMaxLength(IEntryValidationBehavior behavior, string newText)
      {
         return behavior.MaxLength > 0 && newText.IsNotEmpty() && newText.Length > behavior.MaxLength;
      }

      /// <summary>
      /// Determines whether [is whole entry valid] [the specified behavior].
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="currentText">The current text.</param>
      /// <returns><c>true</c> if [is whole entry valid] [the specified behavior]; otherwise, <c>false</c>.</returns>
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

      /// <summary>
      /// Called when [attached to].
      /// </summary>
      /// <param name="entry">The entry.</param>
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
         RevalidateEditorText();
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
         bindable.Focused -= OnFocused;
         bindable.Unfocused -= OnUnfocused;

         base.OnDetachingFrom(bindable);

         _entry = null;
      }

      /// <summary>
      /// Handles the <see cref="E:Focused" /> event.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="FocusEventArgs" /> instance containing the event data.</param>
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

      /// <summary>
      /// Handles the <see cref="E:TextChanged" /> event.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="TextChangedEventArgs" /> instance containing the event data.</param>
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

      /// <summary>
      /// Handles the <see cref="E:Unfocused" /> event.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="FocusEventArgs" /> instance containing the event data.</param>
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

      /// <summary>
      /// Creates the mask positions.
      /// </summary>
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

      private void SetIsValid(bool? isValid)
      {
         if (_entry == null)
         {
            _isValid = Extensions.EmptyNullableBool;
            return;
         }

         if (_isValid.IsNotTheSame(isValid))
         {
            _isValid = isValid;

            // Fire first so related validators can get up to date
            IsValidChanged?.Invoke(_isValid);

            // Notify that a change has taken place Fire last because this is usually the highest level validator
            _onIsValidChangedAction?.Invoke();
         }
      }

      /// <summary>
      /// Sets the unmasked text.
      /// </summary>
      private void SetUnmaskedText()
      {
         UnmaskedText = StripMaskFromText(_entry.Text);
      }

      /// <summary>
      /// Validates the text.
      /// </summary>
      /// <param name="newText">The new text.</param>
      /// <param name="oldText">The old text.</param>
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
         if (filteredText.IsSameAs(preparedOldText))
         {
            _lastAssignedText = filteredText;

            if (_entry.Text.IsDifferentThan(filteredText))
            {
               _ignoreTextChanged = true;
               _entry.Text = filteredText;
               _ignoreTextChanged = false;
            }

            return;
         }

         if (preparedNewText.IsNotEmpty() && _maskPositions.IsNotEmpty() &&
             preparedNewText.IsDifferentThan(_lastAssignedText))
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

         if (preparedNewText.IsDifferentThan(preparedOldText))
         {
            _lastAssignedText = preparedNewText;

            if (_entry.Text.IsDifferentThan(preparedNewText))
            {
               _ignoreTextChanged = true;
               _entry.Text = preparedNewText;
               _ignoreTextChanged = false;
            }

            SetUnmaskedText();
            RevalidateEditorText();
         }

         _lastAssignedText = preparedNewText;
      }
   }
}
