// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ValidatableNumericEntry.cs, is a part of a program called AccountViewMobile.
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
   using Common.Behaviors;
   using Common.Converters;
   using Common.Interfaces;
   using SharedUtils.Utils;
   using System;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IValidatableNumericEntry
   /// Implements the <see cref="IValidatableEntry" />
   /// </summary>
   /// <seealso cref="IValidatableEntry" />
   public interface IValidatableNumericEntry : IValidatableEntry
   {
   }

   /// <summary>
   /// Class ValidatableNumericEntry.
   /// Implements the <see cref="ValidatableEntry" />
   /// Implements the <see cref="IValidatableNumericEntry" />
   /// </summary>
   /// <seealso cref="ValidatableEntry" />
   /// <seealso cref="IValidatableNumericEntry" />
   public class ValidatableNumericEntry : ValidatableEntry, IValidatableNumericEntry
   {
      /// <summary>
      /// The validator
      /// </summary>
      private readonly ICanBeValid _validator;

      /// <summary>
      /// Initializes a new instance of the <see cref="ValidatableNumericEntry" /> class.
      /// </summary>
      /// <param name="borderViewHeight">Height of the border view.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="entryFontSize">Size of the entry font.</param>
      /// <param name="fontFamilyOverride">The font family override.</param>
      /// <param name="instructions">The instructions.</param>
      /// <param name="instructionsHeight">Height of the instructions.</param>
      /// <param name="keyboard">The keyboard.</param>
      /// <param name="placeholder">The placeholder.</param>
      /// <param name="placeholderHeight">Height of the placeholder.</param>
      /// <param name="showInstructionsOrValidations">if set to <c>true</c> [show instructions or validations].</param>
      /// <param name="showValidationErrorsAsInstructions">if set to <c>true</c> [show validation errors as instructions].</param>
      /// <param name="stringFormat">The string format.</param>
      /// <param name="validator">The validator.</param>
      /// <param name="viewModelPropertyName">Name of the view model property.</param>
      public ValidatableNumericEntry
      (
         double? borderViewHeight = BORDER_VIEW_HEIGHT,
         BindingMode bindingMode = BindingMode.TwoWay,
         double? entryFontSize = null,
         string fontFamilyOverride = "",
         string instructions = "",
         double? instructionsHeight = INSTRUCTIONS_HEIGHT,
         Keyboard keyboard = null,
         string placeholder = "",
         double? placeholderHeight = PLACEHOLDER_HEIGHT,
         bool showInstructionsOrValidations = false,
         bool showValidationErrorsAsInstructions = true,
         string stringFormat = null,
         INumericEntryValidationBehavior validator = null,
         string viewModelPropertyName = ""
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

      /// <summary>
      /// Gets a value indicating whether [derived view is focused].
      /// </summary>
      /// <value><c>true</c> if [derived view is focused]; otherwise, <c>false</c>.</value>
      protected override bool DerivedViewIsFocused => false;

      /// <summary>
      /// Gets a value indicating whether [user has entered valid content].
      /// </summary>
      /// <value><c>true</c> if [user has entered valid content]; otherwise, <c>false</c>.</value>
      protected override bool UserHasEnteredValidContent =>
         _validator is INumericEntryValidationBehavior validatorAsNumeric
            ? validatorAsNumeric.PrepareTextForEditing(EditableEntry.Text).IsNotEmpty()
            : EditableEntry.Text.IsNotEmpty();

      /// <summary>
      /// Numerics the type of the converter from numeric.
      /// </summary>
      /// <param name="validator">The validator.</param>
      /// <returns>Func&lt;System.String, System.Object&gt;.</returns>
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

      /// <summary>
      /// Doubles the function.
      /// </summary>
      /// <param name="valueEntered">The value entered.</param>
      /// <returns>Func&lt;System.String, System.Object&gt;.</returns>
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

      /// <summary>
      /// Ints the function.
      /// </summary>
      /// <param name="valueEntered">The value entered.</param>
      /// <returns>Func&lt;System.String, System.Object&gt;.</returns>
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

      /// <summary>
      /// Longs the function.
      /// </summary>
      /// <param name="valueEntered">The value entered.</param>
      /// <returns>Func&lt;System.String, System.Object&gt;.</returns>
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

      /// <summary>
      /// Nullables the double function.
      /// </summary>
      /// <param name="valueEntered">The value entered.</param>
      /// <returns>Func&lt;System.String, System.Object&gt;.</returns>
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

      /// <summary>
      /// Nullables the int function.
      /// </summary>
      /// <param name="valueEntered">The value entered.</param>
      /// <returns>Func&lt;System.String, System.Object&gt;.</returns>
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

      /// <summary>
      /// Nullables the long function.
      /// </summary>
      /// <param name="valueEntered">The value entered.</param>
      /// <returns>Func&lt;System.String, System.Object&gt;.</returns>
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
