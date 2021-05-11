// *********************************************************************** Assembly : Com.MarcusTS.SharedForms Author :
// steph Created : 08-04-2019
//
// Last Modified By : steph Last Modified On : 08-08-2019
// ***********************************************************************
// <copyright file="ValidatableDateTime.cs" company="Marcus Technical Services, Inc.">
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
   ///    Interface IValidatableDateTime Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IValidatableDateTime : IValidatableView
   {
      DatePicker                                  EditableDatePicker { get; }
      DateTime?                                   NullableResult     { get; set; }
      event EventUtils.GenericDelegate<DateTime?> NullableResultChanged;
   }

   /// <summary>
   ///    A UI element that includes an DateTime surrounded by a border. Implements the <see cref="Xamarin.Forms.Grid" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableDateTime" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Grid" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableDateTime" />
   public class ValidatableDateTime : ValidatableViewBase, IValidatableDateTime
   {
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
               view.NullableResultChanged?.Invoke(newVal);
            }
         );

      private          DatePicker      _editableDateTime;
      private readonly double? _fontSize;

      public ValidatableDateTime
      (
         double?                  borderViewHeight                   = BORDER_VIEW_HEIGHT,
         BindingMode              bindingMode                        = BindingMode.TwoWay,
         IValueConverter          converter                          = null,
         object                   converterParameter                 = null,
         bool                     emptyAllowed                       = false,
         string                   fontFamilyOverride                 = "",
         double?                  fontSize                           = null,
         string                   instructions                       = "",
         double?                  instructionsHeight                 = null,
         Keyboard                 keyboard                           = null,
         Action                   onIsValidChangedAction             = null,
         string                   placeholder                        = "",
         double?                  placeholderHeight                  = null,
         bool                     returnNonNullableResult            = false,
         bool                     showInstructionsOrValidations      = false,
         bool                     showValidationErrorsAsInstructions = true,
         string                   stringFormat                       = null,
         ICanBeValid              validator                          = null,
         string                   viewModelPropertyName              = ""
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
                                ?
                                (
                                    returnNonNullableResult
                                    ?
                                    viewAsValidatableDateTime?.NullableResult
                                    :
                                    viewAsValidatableDateTime.EditableDatePicker?.Date
                                )
                                :
                                default,
                     onIsValidChangedAction
                  )
                  { EmptyAllowed = emptyAllowed },
               viewModelPropertyName
            )
      {
         _fontSize = fontSize;
         CallCreateViews();
      }

      public DateTime? NullableResult
      {
         get => (DateTime?)GetValue(NullableResultProperty);
         set => SetValue(NullableResultProperty, value);
      }

      protected override bool DerivedViewIsFocused => false;

      protected override View EditableView => EditableDatePicker;

      protected override View EditableViewContainer => EditableDatePicker;

      protected override bool UserHasEnteredValidContent => EditableDatePicker.Date.IsNotEmpty();

      public DatePicker EditableDatePicker
      {
         get
         {
            if (_editableDateTime.IsNullOrDefault())
            {
               _editableDateTime = new DatePicker
               {
                  FontSize = _fontSize ?? FormsConst.EDITABLE_VIEW_FONT_SIZE,
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

      public event EventUtils.GenericDelegate<DateTime?> NullableResultChanged;

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