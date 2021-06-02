// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=ValidatableDateTime.cs
// company="Marcus Technical Services, Inc.">
// </copyright>
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// *********************************************************************************

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using System;
   using Common.Behaviors;
   using Common.Interfaces;
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Essentials;
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
            BindingMode.TwoWay,
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

      private readonly double?    _fontSize;
      private          DatePicker _editableDateTime;

      public ValidatableDateTime
      (
         bool        emptyAllowed            = false,
         double?     fontSize                = null,
         Action      onIsValidChangedAction  = null,
         bool        returnNonNullableResult = false,
         ICanBeValid validator               = null,
         bool        asleepInitially         = false
      )
         : base
         (
            returnNonNullableResult ? DatePicker.DateProperty : NullableResultProperty,
            validator
            ??
            new ViewValidationBehavior
               (
                  view => view is IValidatableDateTime viewAsValidatableDateTime
                     ? returnNonNullableResult
                        ? viewAsValidatableDateTime.EditableDatePicker?.Date
                        : viewAsValidatableDateTime.NullableResult
                     : default,
                  onIsValidChangedAction
               )
               { EmptyAllowed = emptyAllowed },
            asleepInitially
         )
      {
         _fontSize = fontSize;
         
         if (!IsConstructing)
         {
            RecreateAllViewsBindingsAndStyles();
         }
      }

      protected override bool DerivedViewIsFocused => false;

      protected override View EditableView => EditableDatePicker;

      protected override View EditableViewContainer => EditableDatePicker;

      protected override bool UserHasEnteredValidContent => EditableDatePicker.Date.IsNotEmpty();

      public event EventUtils.GenericDelegate<DateTime?> NullableResultChanged;

      public DatePicker EditableDatePicker
      {
         get
         {
            if (_editableDateTime.IsNullOrDefault())
            {
               _editableDateTime = new DatePicker
               {
                  FontSize        = _fontSize ?? FormsConst.EDITABLE_VIEW_FONT_SIZE
                 //,
                 // BackgroundColor = Color.Transparent
               };

               _editableDateTime.DateSelected +=
                  async (sender, args) =>
                  {
                     NullableResult = _editableDateTime.Date;

                     CallRevalidate();

                     //MainThread.BeginInvokeOnMainThread(() =>
                     //                               {
                     //                                  _editableDateTime.Unfocus();
                     //                               });
                     
                     // custom dialog shown to user ...
                     // await ResetPlaceholderPosition().WithoutChangingContext();
                  };
            }

            return _editableDateTime;
         }
      }

      public DateTime? NullableResult
      {
         get => (DateTime?) GetValue(NullableResultProperty);
         set => SetValue(NullableResultProperty, value);
      }

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