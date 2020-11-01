// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, EditorValidationBehavior.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using Interfaces;
   using SharedUtils.Utils;
   using System;
   using System.ComponentModel;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IEditorValidationBehavior
   /// Implements the <see cref="ICanBeValid" />
   /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="ICanBeValid" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IEditorValidationBehavior : ICanBeValid, INotifyPropertyChanged
   {
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
      /// Gets or sets the original text.
      /// </summary>
      /// <value>The original text.</value>
      string OriginalText { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [text must change].
      /// </summary>
      /// <value><c>true</c> if [text must change]; otherwise, <c>false</c>.</value>
      bool TextMustChange { get; set; }
   }

   /// <summary>
   /// Class EditorValidationBehavior.
   /// Implements the <see cref="Xamarin.Forms.Behavior{Xamarin.Forms.Editor}" />
   /// Implements the <see cref="IEditorValidationBehavior" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Behavior{Xamarin.Forms.Editor}" />
   /// <seealso cref="IEditorValidationBehavior" />
   public class EditorValidationBehavior : Behavior<Editor>, IEditorValidationBehavior
   {
      /// <summary>
      /// The on is valid changed action
      /// </summary>
      private readonly Action _onIsValidChangedAction;

      /// <summary>
      /// The editor
      /// </summary>
      private Editor _editor;

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
      /// Initializes a new instance of the <see cref="EditorValidationBehavior" /> class.
      /// </summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public EditorValidationBehavior(Action onIsValidChangedAction)
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
      public bool IsEditorFocused { get; private set; }

      /// <summary>
      /// Returns true if ... is valid.
      /// </summary>
      /// <value><c>null</c> if [is valid] contains no value, <c>true</c> if [is valid]; otherwise, <c>false</c>.</value>
      public bool? IsValid
      {
         get => _isValid;

         private set
         {
            if (!OverrideFocusEvents && _editor.IsNullOrDefault())
            {
               _isValid = Extensions.EmptyNullableBool;
               return;
            }

            if (_isValid.IsNotTheSame(value))
            {
               _isValid = value;

               // Fire first so related validators can gety up to date
               IsValidChanged?.Invoke(_isValid);

               // Notify that a change has taken place Fire last because this is usually the highest level validator
               _onIsValidChangedAction?.Invoke();
            }
         }
      }

      /// <summary>
      /// Gets or sets the last validation error.
      /// </summary>
      /// <value>The last validation error.</value>
      public string LastValidationError { get; set; }

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

      public bool OverrideFocusEvents { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [text must change].
      /// </summary>
      /// <value><c>true</c> if [text must change]; otherwise, <c>false</c>.</value>
      public bool TextMustChange { get; set; }

      /// <summary>
      /// Minimums an maximum length validator.
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="currentText">The current text.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool MinAnMaxLengthValidator(IEditorValidationBehavior behavior, string currentText)
      {
         return
            (behavior.MinLength == 0 || currentText.IsNotEmpty() && currentText.Length >= behavior.MinLength)
          &&
            (behavior.MaxLength == 0 || currentText.IsNotEmpty() && currentText.Length <= behavior.MaxLength);
      }

      public void AttachManually(Editor editor, bool ignoreFocusEvents = false)
      {
         OverrideFocusEvents = ignoreFocusEvents;
      }

      public void ForceTextChangedManually(string newText)
      {
         OnTextChanged(this, new TextChangedEventArgs(_lastAssignedText, newText));
      }

      /// <summary>
      ///
      /// </summary>
      public void Neutralize()
      {
         IsValid = default;
      }

      public void RevalidateDirectText(string text)
      {
         var isValid = IsEditTextValid(this, text);
         IsValid = isValid;
      }

      public void RevalidateEditorText()
      {
         if (_editor.IsNullOrDefault())
         {
            IsValid = Extensions.EmptyNullableBool;
            return;
         }

         RevalidateDirectText(_editor.Text);
      }

      /// <summary>
      /// Determines whether [is edit text valid] [the specified behavior].
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="currentText">The current text.</param>
      /// <returns><c>true</c> if [is edit text valid] [the specified behavior]; otherwise, <c>false</c>.</returns>
      protected virtual bool IsEditTextValid(IEditorValidationBehavior behavior, string currentText)
      {
         if (!MinAnMaxLengthValidator(behavior, currentText))
         {
            return false;
         }

         // Check against the original text, if any
         if (TextMustChange)
         {
            return currentText.IsDifferentThan(OriginalText);
         }

         // DEFAULT
         return true;
      }

      /// <summary>
      /// Determines whether [is longer than maximum length] [the specified behavior].
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="newText">The new text.</param>
      /// <returns><c>true</c> if [is longer than maximum length] [the specified behavior]; otherwise, <c>false</c>.</returns>
      protected virtual bool IsLongerThanMaxLength(IEditorValidationBehavior behavior, string newText)
      {
         return behavior.MaxLength > 0 && newText.IsNotEmpty() && newText.Length > behavior.MaxLength;
      }

      /// <summary>
      /// Called when [attached to].
      /// </summary>
      /// <param name="editor">The editor.</param>
      protected override void OnAttachedTo(Editor editor)
      {
         editor.TextChanged += OnTextChanged;

         if (!OverrideFocusEvents)
         {
            editor.Focused += OnFocused;
            editor.Unfocused += OnUnfocused;
         }

         base.OnAttachedTo(editor);

         _editor = editor;

         RevalidateEditorText();
      }

      /// <summary>
      /// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(`0)" /> method and then detaches from the superclass.
      /// </summary>
      /// <param name="bindable">The bindable object from which the behavior was detached.</param>
      /// <remarks>To be added.</remarks>
      protected override void OnDetachingFrom(Editor bindable)
      {
         bindable.TextChanged -= OnTextChanged;

         if (!OverrideFocusEvents)
         {
            bindable.Focused -= OnFocused;
            bindable.Unfocused -= OnUnfocused;
         }

         base.OnDetachingFrom(bindable);

         _editor = null;
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
         IsEditorFocused = true;
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
         IsEditorFocused = false;
      }

      /// <summary>
      ///    Validates the text.
      /// </summary>
      /// <param name="newText">The new text.</param>
      /// <param name="oldText">The old text.</param>
      private void ValidateText(string newText, string oldText)
      {
         if ((!OverrideFocusEvents && !IsEditorFocused) || newText.IsSameAs(_lastAssignedText))
         {
            return;
         }

         // This is another "success" case, since there is nothing to do.
         if (newText.IsSameAs(_lastAssignedText))
         {
            return;
         }

         if (newText.IsDifferentThan(oldText))
         {
            _lastAssignedText = newText;

            if (_editor.IsNotNullOrDefault() && _editor.Text.IsDifferentThan(newText))
            {
               _ignoreTextChanged = true;
               _editor.Text = newText;
               _ignoreTextChanged = false;
            }

            RevalidateDirectText(newText);
         }

         _lastAssignedText = newText;
      }
   }
}
