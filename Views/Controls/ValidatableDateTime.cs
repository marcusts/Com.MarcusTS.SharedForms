#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ValidatableDateTime.cs, is a part of a program called AccountViewMobile.
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
   using Common.Interfaces;
   using Common.Utils;
   using SharedUtils.Utils;
   using System;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IValidatableDateTime Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableView" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableView" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IValidatableDateTime : IValidatableView
   {
      /// <summary>
      /// Gets the editable date picker.
      /// </summary>
      /// <value>The editable date picker.</value>
      DatePicker EditableDatePicker { get; }
      /// <summary>
      /// Gets or sets the nullable result.
      /// </summary>
      /// <value>The nullable result.</value>
      DateTime?  NullableResult     { get; set; }
   }

   /// <summary>
   /// A UI element that includes an DateTime surrounded by a border. Implements the <see cref="Xamarin.Forms.Grid" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableDateTime" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ValidatableViewBase" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ValidatableViewBase" />
   /// <seealso cref="Xamarin.Forms.Grid" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableDateTime" />
   public class ValidatableDateTime : ValidatableViewBase, IValidatableDateTime
   {
      /// <summary>
      /// The invalid border view style property
      /// </summary>
      public static readonly BindableProperty NullableResultProperty =
         CreateValidatableViewBindableProperty
         (
            nameof(NullableResult),
            default(DateTime?),
            BindingMode.OneWay,
            (
               view,
               oldVal,
               newVal
            ) =>
            {
               view.EditableDatePicker.Date = newVal.GetValueOrDefault();
            }
         );

      /// <summary>
      /// The font size
      /// </summary>
      private readonly double?    _fontSize;
      /// <summary>
      /// The editable date time
      /// </summary>
      private DatePicker _editableDateTime;

      /// <summary>
      /// Initializes a new instance of the <see cref="ValidatableDateTime"/> class.
      /// </summary>
      /// <param name="borderViewHeight">Height of the border view.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="converter">The converter.</param>
      /// <param name="converterParameter">The converter parameter.</param>
      /// <param name="emptyAllowed">if set to <c>true</c> [empty allowed].</param>
      /// <param name="fontFamilyOverride">The font family override.</param>
      /// <param name="fontSize">Size of the font.</param>
      /// <param name="instructions">The instructions.</param>
      /// <param name="instructionsHeight">Height of the instructions.</param>
      /// <param name="keyboard">The keyboard.</param>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      /// <param name="placeholder">The placeholder.</param>
      /// <param name="placeholderHeight">Height of the placeholder.</param>
      /// <param name="returnNonNullableResult">if set to <c>true</c> [return non nullable result].</param>
      /// <param name="showInstructionsOrValidations">if set to <c>true</c> [show instructions or validations].</param>
      /// <param name="showValidationErrorsAsInstructions">if set to <c>true</c> [show validation errors as instructions].</param>
      /// <param name="stringFormat">The string format.</param>
      /// <param name="validator">The validator.</param>
      /// <param name="viewModelPropertyName">Name of the view model property.</param>
      public ValidatableDateTime
      (
         double?         borderViewHeight                   = BORDER_VIEW_HEIGHT,
         BindingMode     bindingMode                        = BindingMode.TwoWay,
         IValueConverter converter                          = null,
         object          converterParameter                 = null,
         bool            emptyAllowed                       = false,
         string          fontFamilyOverride                 = "",
         double?         fontSize                           = null,
         string          instructions                       = "",
         double?         instructionsHeight                 = INSTRUCTIONS_HEIGHT,
         Keyboard        keyboard                           = null,
         Action          onIsValidChangedAction             = null,
         string          placeholder                        = "",
         double?         placeholderHeight                  = PLACEHOLDER_HEIGHT,
         bool            returnNonNullableResult            = false,
         bool            showInstructionsOrValidations      = false,
         bool            showValidationErrorsAsInstructions = true,
         string          stringFormat                       = null,
         ICanBeValid     validator                          = null,
         string          viewModelPropertyName              = ""
      )
         : base
         (
            returnNonNullableResult ? DatePicker.DateProperty : NullableResultProperty,
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
            validator
          ??
            new ViewValidationBehavior
               (
                  view => view is IValidatableDateTime viewAsValidatableDateTime
                             ? returnNonNullableResult
                                  ? viewAsValidatableDateTime.NullableResult
                                  : viewAsValidatableDateTime.EditableDatePicker?.Date
                             : default,
                  onIsValidChangedAction
               )
               {EmptyAllowed = emptyAllowed},
            viewModelPropertyName
         )
      {
         _fontSize = fontSize;
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
      protected override View EditableView => EditableDatePicker;

      /// <summary>
      /// Gets the editable view container.
      /// </summary>
      /// <value>The editable view container.</value>
      protected override View EditableViewContainer => EditableDatePicker;

      /// <summary>
      /// Gets a value indicating whether [user has entered valid content].
      /// </summary>
      /// <value><c>true</c> if [user has entered valid content]; otherwise, <c>false</c>.</value>
      protected override bool UserHasEnteredValidContent => EditableDatePicker.Date.IsNotEmpty();

      /// <summary>
      /// Gets the editable date picker.
      /// </summary>
      /// <value>The editable date picker.</value>
      public DatePicker EditableDatePicker
      {
         get
         {
            if (_editableDateTime.IsNullOrDefault())
            {
               _editableDateTime = new DatePicker
               {
                  FontSize        = _fontSize ?? FormsConst.EDITABLE_VIEW_FONT_SIZE,
                  BackgroundColor = Color.Transparent
               };

               _editableDateTime.PropertyChanged +=
                  async (sender, args) =>
                  {
                     if (args.PropertyName.IsSameAs(DatePicker.DateProperty.PropertyName))
                     {
                        CallRevalidate();

                        NullableResult = _editableDateTime.Date;

                        await ResetPlaceholderPosition().WithoutChangingContext();
                     }
                  };
            }

            return _editableDateTime;
         }
      }

      /// <summary>
      /// Gets or sets the nullable result.
      /// </summary>
      /// <value>The nullable result.</value>
      public DateTime? NullableResult { get; set; }

      /// <summary>
      /// Creates the validatable view bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateValidatableViewBindableProperty<PropertyTypeT>
      (
         string                                                    localPropName,
         PropertyTypeT                                             defaultVal     = default,
         BindingMode                                               bindingMode    = BindingMode.OneWay,
         Action<ValidatableDateTime, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }
   }
}