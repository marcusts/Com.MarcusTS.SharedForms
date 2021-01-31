// *********************************************************************** Assembly : Com.MarcusTS.SharedForms Author :
// steph Created : 08-04-2019
//
// Last Modified By : steph Last Modified On : 08-08-2019
// ***********************************************************************
// <copyright file="ValidatablePicker.cs" company="Marcus Technical Services, Inc.">
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
   using System.Collections;
   using System.Globalization;
   using Common.Behaviors;
   using Common.Converters;
   using Common.Interfaces;
   using Common.Utils;
   using SharedUtils.Utils;
   using ViewModels;
   using Xamarin.Forms;

   /// <summary>
   ///    Interface IValidatablePicker Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IValidatablePicker : IValidatableView
   {
      Picker EditablePicker { get; }
   }

   /// <summary>
   ///    A UI element that includes an Picker surrounded by a border. Implements the <see cref="Xamarin.Forms.Grid" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatablePicker" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Grid" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatablePicker" />
   public class ValidatablePicker : ValidatableViewBase, IValidatablePicker
   {
      private          Picker  _editablePicker;
      private readonly double? _fontSize;
      private readonly IList _items;
      private string _viewModelPropertyName;

      public ValidatablePicker
      (
         IList                    items, 
         double?                  borderViewHeight                   = BORDER_VIEW_HEIGHT,
         BindingMode              bindingMode                        = BindingMode.TwoWay,
         IValueConverter          converter                          = null,
         object                   converterParameter                 = null,
         bool                     emptyAllowed                       = false,
         string                   fontFamilyOverride                 = "",
         double?                  fontSize                           = null,
         string                   instructions                       = "",
         double?                  instructionsHeight                 = INSTRUCTIONS_HEIGHT,
         Action                   onIsValidChangedAction             = null,
         string                   placeholder                        = "",
         double?                  placeholderHeight                  = PLACEHOLDER_HEIGHT,

         // For returning strings as numeric
         bool                     returnAsNumeric                    = false,

         bool                     showInstructionsOrValidations      = false,
         bool                     showValidationErrorsAsInstructions = true,
         string                   stringFormat                       = null,
         ICanBeValid              validator                          = null,
         string                   viewModelPropertyName              = "",
         bool isNumeric = false
      )
         : base
            (
               Picker.SelectedItemProperty,
               borderViewHeight,
               bindingMode,
               returnAsNumeric ? new StringToNumericConverter() { ConvertBackFunc = ValidatableNumericEntry.NumericConverterFromNumericType(validator) } : converter,
               returnAsNumeric ? null  : converterParameter,
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
                                ?
                                viewAsValidatablePicker.EditablePicker.SelectedItem
                                :
                                default,
                     onIsValidChangedAction
                  )
                  { EmptyAllowed = emptyAllowed, IsNumeric = isNumeric },
               viewModelPropertyName
            )
      {
         BackgroundColor = Color.Transparent;
         _fontSize = fontSize;
         _items = items;
         _viewModelPropertyName = viewModelPropertyName;

         if (EditablePicker.IsNotNullOrDefault())
         {
            EditablePicker.ItemsSource = _items;
         }

         CallCreateViews();
      }

      protected override bool DerivedViewIsFocused => false;

      protected override View EditableView => EditablePicker;

      protected override View EditableViewContainer => EditablePicker;

      protected override bool UserHasEnteredValidContent => EditablePicker.SelectedItem.IsNotNullOrDefault();

      public Picker EditablePicker
      {
         get
         {
            if (_editablePicker.IsNullOrDefault())
            {
               _editablePicker = new Picker()
               {
                  FontSize = _fontSize ?? FormsConst.EDITABLE_VIEW_FONT_SIZE,
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
