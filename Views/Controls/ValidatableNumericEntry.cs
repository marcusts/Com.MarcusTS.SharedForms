// *********************************************************************** Assembly : Com.MarcusTS.SharedForms Author :
// steph Created : 08-04-2019
//
// Last Modified By : steph Last Modified On : 08-08-2019
// ***********************************************************************
// <copyright file="ValidatableNumericEntry.cs" company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using System;
   using Common.Behaviors;
   using Common.Converters;
   using Common.Interfaces;
   using SharedUtils.Utils;
   using ViewModels;
   using Xamarin.Forms;

   public interface IValidatableNumericEntry : IValidatableEntry
   {
   }

   public class ValidatableNumericEntry : ValidatableEntry, IValidatableNumericEntry
   {
      private readonly ICanBeValid _validator;

      public ValidatableNumericEntry
      (
         double?                         borderViewHeight                   = BORDER_VIEW_HEIGHT,
         BindingMode                     bindingMode                        = BindingMode.TwoWay,
         double? entryFontSize = null,
         string                          fontFamilyOverride                 = "",
         string                          instructions                       = "",
         double?                         instructionsHeight                 = null,
         Keyboard                        keyboard                           = null,
         string                          placeholder                        = "",
         double?                         placeholderHeight                  = null,
         bool                            showInstructionsOrValidations      = false,
         bool                            showValidationErrorsAsInstructions = true,
         string                          stringFormat                       = null,
         INumericEntryValidationBehavior validator                          = null,
         string                          viewModelPropertyName              = ""
      )
         : base
         (
            borderViewHeight,
            false,
            bindingMode,
            new StringToNumericConverter
            {
               // ConvertBackFunc = NumericConverterFromNumericType(validator),
               StringFormat = stringFormat,
               ValidationType = validator.IsNotNullOrDefault()
                                   ? validator.ValidationType
                                   : ValidationTypes.DecimalNumber
            },
            null,
            entryFontSize,
            fontFamilyOverride,
            instructions,
            instructionsHeight,
            false,
            keyboard,
            placeholder,
            placeholderHeight,
            showInstructionsOrValidations,
            showValidationErrorsAsInstructions,
            stringFormat,
            validator,
            viewModelPropertyName
         )

      {
         _validator = validator;
      }

      protected override bool DerivedViewIsFocused => false;

      protected override bool UserHasEnteredValidContent =>
         _validator is INumericEntryValidationBehavior validatorAsNumeric
            ? validatorAsNumeric.PrepareTextForEditing(EditableEntry.Text).IsNotEmpty()
            : EditableEntry.Text.IsNotEmpty();

      public static Func<string, object> NumericConverterFromNumericType(ICanBeValid validator)
      {
         if (validator is IHaveMinAndMaxNumbers validatorAsMinMax)
         {
            switch (validatorAsMinMax.NumericType)
            {
               case NumericTypes.DoubleNumericType:
                  return DoubleFunc;

               case NumericTypes.IntNumericType:
                  return IntFunc;

               case NumericTypes.LongNumericType:
                  return LongFunc;

               case NumericTypes.NullableDoubleNumericType:
                  return NullableDoubleFunc;

               case NumericTypes.NullableIntNumericType:
                  return NullableIntFunc;

               case NumericTypes.NullableLongNumericType:
                  return NullableLongFunc;

               default:
                  return null;
            }
         }

         return null;
      }

      private static Func<string, object> DoubleFunc(string valueEntered)
      {
         return str =>
                {
                   if (valueEntered.IsNotEmpty() && double.TryParse(valueEntered, out var valueAsDouble))
                   {
                      return valueAsDouble;
                   }

                   return null;
                };
      }

      private static Func<string, object> IntFunc(string valueEntered)
      {
         return str =>
                {
                   if (valueEntered.IsNotEmpty() && int.TryParse(valueEntered, out var valueAsInt))
                   {
                      return valueAsInt;
                   }

                   return null;
                };
      }

      private static Func<string, object> LongFunc(string valueEntered)
      {
         return str =>
                {
                   if (valueEntered.IsNotEmpty() && long.TryParse(valueEntered, out var valueAsLong))
                   {
                      return valueAsLong;
                   }

                   return null;
                };
      }

      private static Func<string, object> NullableDoubleFunc(string valueEntered)
      {
         return str =>
                {
                   if (valueEntered.IsNotEmpty() && double.TryParse(valueEntered, out var valueAsDouble))
                   {
                      return valueAsDouble as double?;
                   }

                   return null;
                };
      }

      private static Func<string, object> NullableIntFunc(string valueEntered)
      {
         return str =>
                {
                   if (valueEntered.IsNotEmpty() && int.TryParse(valueEntered, out var valueAsInt))
                   {
                      return valueAsInt as int?;
                   }

                   return null;
                };
      }

      private static Func<string, object> NullableLongFunc(string valueEntered)
      {
         return str =>
                {
                   if (valueEntered.IsNotEmpty() && long.TryParse(valueEntered, out var valueAsLong))
                   {
                      return valueAsLong as long?;
                   }

                   return null;
                };
      }
   }
}