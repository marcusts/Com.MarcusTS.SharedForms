﻿// *********************************************************************** Assembly : Com.MarcusTS.SharedForms Author :
// steph Created : 08-04-2019
//
// Last Modified By : steph Last Modified On : 08-08-2019
// ***********************************************************************
// <copyright file="ValidatableCheckBox.cs" company="Marcus Technical Services, Inc.">
//     Copyright @2019 Marcus Technical Services, Inc.
// </copyright>
// <summary></summary>

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
   using System;
   using Common.Behaviors;
   using Common.Interfaces;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   ///    Interface IValidatableCheckBox Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IValidatableCheckBox : IValidatableView
   {
      CustomCheckBox EditableCheckBox { get; }
   }

   /// <summary>
   ///    A UI element that includes an CheckBox surrounded by a border. Implements the <see cref="Xamarin.Forms.Grid" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableCheckBox" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Grid" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableCheckBox" />
   public class ValidatableCheckBox : ValidatableViewBase, IValidatableCheckBox
   {
      private          CustomCheckBox  _editableCheckBox;

      public ValidatableCheckBox
      (
         double?                  borderViewHeight                   = BORDER_VIEW_HEIGHT,
         BindingMode              bindingMode                        = BindingMode.TwoWay,
         IValueConverter          converter                          = null,
         object                   converterParameter                 = null,
         string                   fontFamilyOverride                 = "",
         string                   instructions                       = "",
         double?                  instructionsHeight                 = INSTRUCTIONS_HEIGHT,
         Action                   onIsValidChangedAction             = null,
         string                   placeholder                        = "",
         double?                  placeholderHeight                  = PLACEHOLDER_HEIGHT,
         bool                     showInstructionsOrValidations      = false,
         bool                     showValidationErrorsAsInstructions = true,
         string                   stringFormat                       = null,
         ICanBeValid              validator                          = null,
         string                   viewModelPropertyName              = ""
      )
         : base
            (
               CustomCheckBox.IsCheckedProperty,
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
                     view => view is IValidatableCheckBox viewAsValidatableCheckBox && viewAsValidatableCheckBox.EditableCheckBox.IsNotNullOrDefault()
                                ?
                                viewAsValidatableCheckBox.EditableCheckBox.IsChecked
                                :
                                default,
                     onIsValidChangedAction
                  ),
               viewModelPropertyName
            )
      {
         CallCreateViews();
      }

      private static object NullableLongFunc(string valueEntered)
      {
         if (valueEntered.IsNotEmpty() && long.TryParse(valueEntered, out var valueAsLong))
         {
            return valueAsLong as long?;
         }

         return default(long?);
      }

      protected override bool DerivedViewIsFocused => false;

      protected override View EditableView => EditableCheckBox;

      protected override View EditableViewContainer => EditableCheckBox;

      // Checked or not, the content is always valid
      protected override bool UserHasEnteredValidContent => true;

      public CustomCheckBox EditableCheckBox
      {
         get
         {
            if (_editableCheckBox.IsNullOrDefault())
            {
               _editableCheckBox = new CustomCheckBox { BackgroundColor = Color.Transparent, Margin = 7.5 };

               _editableCheckBox.PropertyChanged +=
                  (sender, args) =>
                  {
                     if (args.PropertyName.IsSameAs(CustomCheckBox.IsCheckedProperty.PropertyName))
                     {
                        CallRevalidate();
                     }
                  };
            }

            return _editableCheckBox;
         }
      }

      //private class StringToNumericConverter : IValueConverter
      //{
      //   public static readonly StringToNumericConverter StringToNumericConverter_Static = new StringToNumericConverter();

      //   public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      //   {
      //      if (parameter is Func<string, object>  parameterAsFunc)
      //      {
      //         return parameterAsFunc(value.IsNullOrDefault() ? "" : value.ToString());
      //      }

      //      return default;
      //   }

      //   public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      //   {
      //      if (value.IsNullOrDefault())
      //      {
      //         return "";
      //      }

      //      return value.ToString();
      //   }
      //}
   }
}
