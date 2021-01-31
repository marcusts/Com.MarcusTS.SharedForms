// *********************************************************************** Assembly : Com.MarcusTS.SharedForms Author :
// steph Created : 08-04-2019
//
// Last Modified By : steph Last Modified On : 08-08-2019
// ***********************************************************************
// <copyright file="ValidatableEnumPicker.cs" company="Marcus Technical Services, Inc.">
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
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   ///    Interface IValidatableEnumPicker Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IValidatableEnumPicker : IValidatableView
   {
      EnumPicker EditableEnumPicker { get; }
   }

   /// <summary>
   ///    A UI element that includes an EnumPicker surrounded by a border. Implements the <see cref="Xamarin.Forms.Grid" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableEnumPicker" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Grid" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableEnumPicker" />
   public class ValidatableEnumPicker : ValidatableViewBase, IValidatableEnumPicker
   {
      private          EnumPicker      _editableEnumPicker;
      private readonly double? _fontSize;
      private readonly Type _enumType;
      private readonly string _viewModelPropertyName;

      public ValidatableEnumPicker
      (
         Type enumType,
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
         bool                     showInstructionsOrValidations      = false,
         bool                     showValidationErrorsAsInstructions = true,
         string                   stringFormat                       = null,
         ICanBeValid              validator                          = null,
         string                   viewModelPropertyName              = ""
      )
         : base
            (
               Picker.SelectedItemProperty,
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
               //?? 
               //   new ViewValidationBehavior
               //   (
               //      view => view is IValidatableEnumPicker viewAsValidatableEnumPicker 
               //                 ? 
               //                 viewAsValidatableEnumPicker.EditableEnumPicker.SelectedItem 
               //                 : 
               //                 default, 
               //      onIsValidChangedAction
               //   ) 
               //   { EmptyAllowed = emptyAllowed },
               viewModelPropertyName
            )
      {
         _viewModelPropertyName = viewModelPropertyName;
         _fontSize = fontSize;
         _enumType = enumType;

         CallCreateViews();
      }

      protected override bool DerivedViewIsFocused => false;

      protected override View EditableView => EditableEnumPicker;

      protected override View EditableViewContainer => EditableEnumPicker;

      protected override bool UserHasEnteredValidContent => EditableEnumPicker.SelectedItem.IsNotNullOrDefault();

      public EnumPicker EditableEnumPicker
      {
         get
         {
            if (_editableEnumPicker.IsNullOrDefault() && _enumType.IsNotNullOrDefault())
            {
               _editableEnumPicker = new EnumPicker(_enumType, "", _viewModelPropertyName)
               {
                  FontSize = _fontSize ?? FormsConst.EDITABLE_VIEW_FONT_SIZE,
                  BackgroundColor = Color.Transparent
               };

               _editableEnumPicker.PropertyChanged +=
                  async (sender, args) =>
                  {
                     if (args.PropertyName.IsSameAs(Picker.SelectedItemProperty.PropertyName))
                     {
                        CallRevalidate();
                        await ResetPlaceholderPosition().WithoutChangingContext();
                     }
                  };
            }

            return _editableEnumPicker;
         }
      }
   }
}
