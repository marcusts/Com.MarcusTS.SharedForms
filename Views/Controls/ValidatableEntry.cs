// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ValidatableEntry.cs, is a part of a program called AccountViewMobile.
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

// MIT License

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
// Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Images;
   using Common.Interfaces;
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IValidatableEntry Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// Implements the <see cref="IValidatableView" />
   /// </summary>
   /// <seealso cref="IValidatableView" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IValidatableEntry : IValidatableView
   {
      /// <summary>
      /// Gets the editable entry.
      /// </summary>
      /// <value>The editable entry.</value>
      Entry EditableEntry { get; }
   }

   /// <summary>
   /// A UI element that includes an Entry surrounded by a border. Implements the <see cref="Xamarin.Forms.Grid" />
   /// Implements the <see cref="IValidatableEntry" />
   /// Implements the <see cref="ValidatableViewBase" />
   /// </summary>
   /// <seealso cref="ValidatableViewBase" />
   /// <seealso cref="Xamarin.Forms.Grid" />
   /// <seealso cref="IValidatableEntry" />
   public class ValidatableEntry : ValidatableViewBase, IValidatableEntry
   {
      /// <summary>
      /// The hide password image
      /// </summary>
      private const string HIDE_PASSWORD_IMAGE = SharedImageUtils.IMAGE_PRE_PATH + "hide_password.png";

      /// <summary>
      /// The show password image
      /// </summary>
      private const string SHOW_PASSWORD_IMAGE = SharedImageUtils.IMAGE_PRE_PATH + "show_password.png";

      /// <summary>
      /// The can unmask password
      /// </summary>
      private readonly bool _canUnmaskPassword;

      /// <summary>
      /// The is password
      /// </summary>
      private readonly bool _isPassword;

      /// <summary>
      /// The keyboard
      /// </summary>
      private readonly Keyboard _keyboard;

      /// <summary>
      /// The editable entry
      /// </summary>
      private Entry _editableEntry;

      /// <summary>
      /// The editable view container
      /// </summary>
      private View _editableViewContainer;

      /// <summary>
      /// The is password showing
      /// </summary>
      private bool _isPasswordShowing;

      /// <summary>
      /// The show hide image
      /// </summary>
      private Image _showHideImage;

      /// <summary>
      /// Initializes a new instance of the <see cref="ValidatableEntry" /> class.
      /// </summary>
      /// <param name="borderViewHeight">Height of the border view.</param>
      /// <param name="canUnmaskPassword">if set to <c>true</c> [can unmask password].</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="converter">The converter.</param>
      /// <param name="converterParameter">The converter parameter.</param>
      /// <param name="entryFontSize">Size of the entry font.</param>
      /// <param name="fontFamilyOverride">The font family override.</param>
      /// <param name="instructions">The instructions.</param>
      /// <param name="instructionsHeight">Height of the instructions.</param>
      /// <param name="isPassword">if set to <c>true</c> [is password].</param>
      /// <param name="keyboard">The keyboard.</param>
      /// <param name="placeholder">The placeholder.</param>
      /// <param name="placeholderHeight">Height of the placeholder.</param>
      /// <param name="showInstructionsOrValidations">if set to <c>true</c> [show instructions or validations].</param>
      /// <param name="showValidationErrorsAsInstructions">if set to <c>true</c> [show validation errors as instructions].</param>
      /// <param name="stringFormat">The string format.</param>
      /// <param name="validator">The validator.</param>
      /// <param name="viewModelPropertyName">Name of the view model property.</param>
      public ValidatableEntry
      (
         double? borderViewHeight = BORDER_VIEW_HEIGHT,
         bool canUnmaskPassword = true,
         BindingMode bindingMode = BindingMode.TwoWay,
         IValueConverter converter = null,
         object converterParameter = null,
         double? entryFontSize = null,
         string fontFamilyOverride = "",
         string instructions = "",
         double? instructionsHeight = INSTRUCTIONS_HEIGHT,
         bool isPassword = false,
         Keyboard keyboard = null,
         string placeholder = "",
         double? placeholderHeight = PLACEHOLDER_HEIGHT,
         bool showInstructionsOrValidations = false,
         bool showValidationErrorsAsInstructions = true,
         string stringFormat = null,
         ICanBeValid validator = null,
         string viewModelPropertyName = ""
      )
         : base
         (
            Entry.TextProperty,
            borderViewHeight,
            bindingMode,
            converter,
            converterParameter,
            fontFamilyOverride,
            instructions,
            instructionsHeight,
            placeholder,
            placeholderHeight,
            showInstructionsOrValidations,
            showValidationErrorsAsInstructions,
            stringFormat,
            validator,
            viewModelPropertyName
         )

      {
         _keyboard = keyboard;
         _isPassword = isPassword;
         _canUnmaskPassword = canUnmaskPassword;

         // The editable entry gets created "too soon" for these assignment to work, so repeating them here.
         if (EditableEntry.IsNotNullOrDefault())
         {
            EditableEntry.Keyboard = _keyboard;
            EditableEntry.IsPassword = isPassword;
            EditableEntry.FontSize = entryFontSize ?? Device.GetNamedSize(NamedSize.Small, typeof(Entry));
         }

         CallCreateViews();
      }

      /// <summary>
      /// Gets the editable entry.
      /// </summary>
      /// <value>The editable entry.</value>
      public Entry EditableEntry
      {
         get
         {
            if (_editableEntry.IsNullOrDefault())
            {
               _editableEntry = new CustomEntry
               {
                  // Cannot use non-standard keyboards when there is a mask
                  Keyboard = _keyboard,
                  IsEnabled = true,
                  IsReadOnly = false,
                  MaxLength = int.MaxValue,
                  IsPassword = _isPassword,
                  TextColor = Color.Black,
                  BackgroundColor = Color.Transparent,
                  HorizontalOptions = LayoutOptions.FillAndExpand,
                  VerticalOptions = LayoutOptions.FillAndExpand,
                  Margin = DEFAULT_BORDER_VIEW_PADDING
               };
            }

            return _editableEntry;
         }
      }

      /// <summary>
      /// Gets a value indicating whether [derived view is focused].
      /// </summary>
      /// <value><c>true</c> if [derived view is focused]; otherwise, <c>false</c>.</value>
      protected override bool DerivedViewIsFocused => _showHideImage.IsNotNullOrDefault() && _showHideImage.IsFocused;

      /// <summary>
      /// Gets the editable view.
      /// </summary>
      /// <value>The editable view.</value>
      protected override View EditableView => EditableEntry;

      /// <summary>
      /// Gets the editable view container.
      /// </summary>
      /// <value>The editable view container.</value>
      protected override View EditableViewContainer
      {
         get
         {
            if (_editableViewContainer.IsNullOrDefault())
            {
               if (_isPassword && _canUnmaskPassword)
               {
                  // Put the editable entry in a grid with the password unmask button
                  var editGrid = FormsUtils.GetExpandingGrid();

                  editGrid.AddStarColumn();
                  editGrid.AddAutoColumn();
                  editGrid.Children.Add(EditableEntry);
                  SetColumn(EditableEntry, 0);

                  var showHideImageWidthHeight = BorderView.HeightRequest * 0.85;

                  _showHideImage = FormsUtils.GetImage("", showHideImageWidthHeight, showHideImageWidthHeight,
                                                       Aspect.AspectFit, true, GetType());
                  _showHideImage.Margin = new Thickness(0, 0, 5, 0);
                  var tapGesture = new TapGestureRecognizer();

                  tapGesture.Tapped += (sender, args) => { IsPasswordShowing = !IsPasswordShowing; };
                  _showHideImage.GestureRecognizers.Add(tapGesture);
                  SetShowHideImageSource();

                  _showHideImage.Focused += ReportGlobalFocusAndRaisePlaceholder;
                  _showHideImage.Unfocused += ConsiderLoweringPlaceholder;

                  editGrid.Children.Add(_showHideImage);
                  SetColumn(_showHideImage, 1);

                  _editableViewContainer = editGrid;
               }
               else
               {
                  _editableViewContainer = EditableEntry;
               }
            }

            return _editableViewContainer;
         }
      }

      /// <summary>
      /// Gets a value indicating whether [user has entered valid content].
      /// </summary>
      /// <value><c>true</c> if [user has entered valid content]; otherwise, <c>false</c>.</value>
      protected override bool UserHasEnteredValidContent => EditableEntry.Text.IsNotEmpty();

      /// <summary>
      /// Gets or sets a value indicating whether this instance is password showing.
      /// </summary>
      /// <value><c>true</c> if this instance is password showing; otherwise, <c>false</c>.</value>
      private bool IsPasswordShowing
      {
         get => _isPasswordShowing;
         set
         {
            if (_isPasswordShowing == value)
            {
               return;
            }

            _isPasswordShowing = value;

            EditableEntry.IsPassword = !_isPasswordShowing;

            // BUG - HACK - Sometimes fails to refresh
            EditableEntry.IsPassword = _isPasswordShowing;
            EditableEntry.IsPassword = !_isPasswordShowing;

            SetShowHideImageSource();
         }
      }

      /// <summary>
      /// Sets the show hide image source.
      /// </summary>
      private void SetShowHideImageSource()
      {
         if (_showHideImage.IsNotNullOrDefault())
         {
            // ReSharper disable once RedundantAssignment
            var imageResourceStr = string.Empty;

            imageResourceStr = _isPasswordShowing ? SHOW_PASSWORD_IMAGE : HIDE_PASSWORD_IMAGE;

            _showHideImage.Source = ImageSource.FromResource(imageResourceStr, GetType().Assembly);
         }
      }
   }
}
