#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ValidatablePicker.cs, is a part of a program called AccountViewMobile.
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

// #define USE_BACK_COLOR

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Behaviors;
   using Common.Converters;
   using Common.Interfaces;
   using Common.Utils;
   using SharedUtils.Utils;
   using System;
   using System.Collections;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IValidatablePicker Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableView" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableView" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IValidatablePicker : IValidatableView
   {
      /// <summary>
      /// Gets the editable picker.
      /// </summary>
      /// <value>The editable picker.</value>
      Picker EditablePicker { get; }
   }

   /// <summary>
   /// A UI element that includes an Picker surrounded by a border. Implements the <see cref="Xamarin.Forms.Grid" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatablePicker" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ValidatableViewBase" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ValidatableViewBase" />
   /// <seealso cref="Xamarin.Forms.Grid" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatablePicker" />
   public class ValidatablePicker : ValidatableViewBase, IValidatablePicker
   {
      /// <summary>
      /// The font size
      /// </summary>
      private readonly double? _fontSize;
      /// <summary>
      /// The items
      /// </summary>
      private readonly IList   _items;
      /// <summary>
      /// The editable picker
      /// </summary>
      private Picker  _editablePicker;
      /// <summary>
      /// The view model property name
      /// </summary>
      private string  _viewModelPropertyName;

      /// <summary>
      /// Initializes a new instance of the <see cref="ValidatablePicker" /> class.
      /// </summary>
      /// <param name="items">The items.</param>
      /// <param name="borderViewHeight">Height of the border view.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="converter">The converter.</param>
      /// <param name="converterParameter">The converter parameter.</param>
      /// <param name="emptyAllowed">if set to <c>true</c> [empty allowed].</param>
      /// <param name="fontFamilyOverride">The font family override.</param>
      /// <param name="fontSize">Size of the font.</param>
      /// <param name="instructions">The instructions.</param>
      /// <param name="instructionsHeight">Height of the instructions.</param>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      /// <param name="placeholder">The placeholder.</param>
      /// <param name="placeholderHeight">Height of the placeholder.</param>
      /// <param name="returnAsNumeric">if set to <c>true</c> [return as numeric].</param>
      /// <param name="showInstructionsOrValidations">if set to <c>true</c> [show instructions or validations].</param>
      /// <param name="showValidationErrorsAsInstructions">if set to <c>true</c> [show validation errors as instructions].</param>
      /// <param name="stringFormat">The string format.</param>
      /// <param name="validator">The validator.</param>
      /// <param name="viewModelPropertyName">Name of the view model property.</param>
      /// <param name="isNumeric">if set to <c>true</c> [is numeric].</param>
      public ValidatablePicker
      (
         IList           items,
         double?         borderViewHeight       = BORDER_VIEW_HEIGHT,
         BindingMode     bindingMode            = BindingMode.TwoWay,
         IValueConverter converter              = null,
         object          converterParameter     = null,
         bool            emptyAllowed           = false,
         string          fontFamilyOverride     = "",
         double?         fontSize               = null,
         string          instructions           = "",
         double?         instructionsHeight     = INSTRUCTIONS_HEIGHT,
         Action          onIsValidChangedAction = null,
         string          placeholder            = "",
         double?         placeholderHeight      = PLACEHOLDER_HEIGHT,

         // For returning strings as numeric
         bool        returnAsNumeric                    = false,
         bool        showInstructionsOrValidations      = false,
         bool        showValidationErrorsAsInstructions = true,
         string      stringFormat                       = null,
         ICanBeValid validator                          = null,
         string      viewModelPropertyName              = "",
         bool        isNumeric                          = false
      )
         : base
         (
            Picker.SelectedItemProperty,
            borderViewHeight,
            bindingMode,
            returnAsNumeric
               ? new StringToNumericConverter
                  {ConvertBackFunc = ValidatableNumericEntry.NumericConverterFromNumericType(validator)}
               : converter,
            returnAsNumeric ? null : converterParameter,
            fontFamilyOverride,
            instructions,
            instructionsHeight,
            placeholder,
            placeholderHeight,
            showInstructionsOrValidations,
            showValidationErrorsAsInstructions,
            stringFormat,
            validator
          ??
            new ViewValidationBehavior
               (
                  view => view is IValidatablePicker viewAsValidatablePicker
                             ? viewAsValidatablePicker.EditablePicker.SelectedItem
                             : default,
                  onIsValidChangedAction
               )
               {EmptyAllowed = emptyAllowed, IsNumeric = isNumeric},
            viewModelPropertyName
         )
      {
         BackgroundColor        = Color.Transparent;
         _fontSize              = fontSize;
         _items                 = items;
         _viewModelPropertyName = viewModelPropertyName;

         if (EditablePicker.IsNotNullOrDefault())
         {
            EditablePicker.ItemsSource = _items;
         }

         CallCreateViews();
      }

      /// <summary>
      /// Gets a value indicating whether [derived view is focused].
      /// </summary>
      /// <value><c>true</c> if [derived view is focused]; otherwise, <c>false</c>.</value>
      protected override bool DerivedViewIsFocused => false;

      /// <summary>
      /// Gets the editable view.
      /// </summary>
      /// <value>The editable view.</value>
      protected override View EditableView => EditablePicker;

      /// <summary>
      /// Gets the editable view container.
      /// </summary>
      /// <value>The editable view container.</value>
      protected override View EditableViewContainer => EditablePicker;

      /// <summary>
      /// Gets a value indicating whether [user has entered valid content].
      /// </summary>
      /// <value><c>true</c> if [user has entered valid content]; otherwise, <c>false</c>.</value>
      protected override bool UserHasEnteredValidContent => EditablePicker.SelectedItem.IsNotNullOrDefault();

      /// <summary>
      /// Gets the editable picker.
      /// </summary>
      /// <value>The editable picker.</value>
      public Picker EditablePicker
      {
         get
         {
            if (_editablePicker.IsNullOrDefault())
            {
               _editablePicker = new Picker
               {
                  FontSize    = _fontSize ?? FormsConst.EDITABLE_VIEW_FONT_SIZE,
                  ItemsSource = _items
               };

#if USE_BACK_COLOR
               BackgroundColor = Color.PaleGreen;
#else
               _editablePicker.BackgroundColor = Color.Transparent;
#endif

               _editablePicker.PropertyChanged +=
                  async (sender, args) =>
                  {
                     if (args.PropertyName.IsSameAs(Picker.SelectedItemProperty.PropertyName))
                     {
                        CallRevalidate();
                        await ResetPlaceholderPosition().WithoutChangingContext();
                     }
                  };
            }

            return _editablePicker;
         }
      }
   }
}