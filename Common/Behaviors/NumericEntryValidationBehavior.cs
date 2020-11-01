// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, NumericEntryValidationBehavior.cs, is a part of a program called AccountViewMobile.
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

// #define STOP_REVERSING_VALUES

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using SharedUtils.Utils;
   using System;
   using System.Diagnostics;
   using System.Text.RegularExpressions;
   using Utils;

   /// <summary>
   /// Enum NumericTypes
   /// </summary>
   public enum NumericTypes
   {
      /// <summary>
      /// The no numeric type
      /// </summary>
      NoNumericType,

      /// <summary>
      /// The double numeric type
      /// </summary>
      DoubleNumericType,

      /// <summary>
      /// The nullable double numeric type
      /// </summary>
      NullableDoubleNumericType,

      /// <summary>
      /// The int numeric type
      /// </summary>
      IntNumericType,

      /// <summary>
      /// The nullable int numeric type
      /// </summary>
      NullableIntNumericType,

      /// <summary>
      /// The long numeric type
      /// </summary>
      LongNumericType,

      /// <summary>
      /// The nullable long numeric type
      /// </summary>
      NullableLongNumericType
   }

   /// <summary>
   /// Interface IHaveMinAndMaxNumbers
   /// </summary>
   public interface IHaveMinAndMaxNumbers
   {
      /// <summary>
      /// Gets or sets the maximum decimal number.
      /// </summary>
      /// <value>The maximum decimal number.</value>
      double MaxDecimalNumber { get; set; }

      /// <summary>
      /// Gets or sets the minimum decimal number.
      /// </summary>
      /// <value>The minimum decimal number.</value>
      double MinDecimalNumber { get; set; }

      /// <summary>
      /// Gets or sets the type of the numeric.
      /// </summary>
      /// <value>The type of the numeric.</value>
      NumericTypes NumericType { get; set; }
   }

   /// <summary>
   /// Interface INumericEntryValidationBehavior
   /// Implements the <see cref="IEntryValidationBehavior" />
   /// Implements the <see cref="IHaveMinAndMaxNumbers" />
   /// </summary>
   /// <seealso cref="IEntryValidationBehavior" />
   /// <seealso cref="IHaveMinAndMaxNumbers" />
   public interface INumericEntryValidationBehavior : IEntryValidationBehavior, IHaveMinAndMaxNumbers
   {
      /// <summary>
      /// Gets or sets the chars to right of decimal.
      /// </summary>
      /// <value>The chars to right of decimal.</value>
      int CharsToRightOfDecimal { get; set; }

      /// <summary>
      /// Gets or sets the string format.
      /// </summary>
      /// <value>The string format.</value>
      string StringFormat { get; set; }
   }

   /// <summary>
   /// Class NumericEntryValidationBehavior.
   /// Implements the <see cref="EntryValidationBehavior" />
   /// Implements the <see cref="INumericEntryValidationBehavior" />
   /// </summary>
   /// <seealso cref="EntryValidationBehavior" />
   /// <seealso cref="INumericEntryValidationBehavior" />
   public class NumericEntryValidationBehavior : EntryValidationBehavior, INumericEntryValidationBehavior
   {
      /// <summary>
      /// The fail text
      /// </summary>
      private const string FAIL_TEXT = "Fail!^%$^%(&*)_()_)*((*^&";

      /// <summary>
      /// Initializes a new instance of the <see cref="NumericEntryValidationBehavior" /> class.
      /// </summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public NumericEntryValidationBehavior(Action onIsValidChangedAction) : base(onIsValidChangedAction)
      {
      }

      /// <summary>
      /// Gets or sets the chars to right of decimal.
      /// </summary>
      /// <value>The chars to right of decimal.</value>
      public int CharsToRightOfDecimal { get; set; }

      /// <summary>
      /// Gets or sets the maximum decimal number.
      /// </summary>
      /// <value>The maximum decimal number.</value>
      public double MaxDecimalNumber { get; set; }

      /// <summary>
      /// Gets or sets the minimum decimal number.
      /// </summary>
      /// <value>The minimum decimal number.</value>
      public double MinDecimalNumber { get; set; }

      /// <summary>
      /// Gets or sets the type of the numeric.
      /// </summary>
      /// <value>The type of the numeric.</value>
      public NumericTypes NumericType { get; set; }

      /// <summary>
      /// Gets or sets the string format.
      /// </summary>
      /// <value>The string format.</value>
      public string StringFormat { get; set; }

      /// <summary>
      /// This method must also *restore* a decimal and trailing zeroes when first focused.
      /// </summary>
      /// <param name="entryText">The entry text.</param>
      /// <param name="stringFormat">The string format.</param>
      /// <param name="validationType">Type of the validation.</param>
      /// <param name="charsToRight">The chars to right.</param>
      /// <param name="firstFocused">if set to <c>true</c> [first focused].</param>
      /// <returns>System.String.</returns>
      public static string StripStringFormatCharacters(string entryText,
                                                       string stringFormat,
                                                       ValidationTypes validationType,
                                                       int charsToRight = 0,
                                                       bool firstFocused = false)
      {
         // Remove the numeric string formats (all except for numbers and dots)
         if (stringFormat.IsEmpty() || entryText.IsEmpty())
         {
            return entryText;
         }

         // Numbers are either "Whole" (no special symbols allowed) or "Decimal" (a single dot is allowed, plus a certain
         // number of characters after that).
         switch (validationType)
         {
            case ValidationTypes.DecimalNumber:
               var retString = Regex.Replace(entryText, "[^0-9.]+", "");

               // Add the period and zeroes
               if (firstFocused)
               {
                  var decimalPos = retString.PositionOfDecimal();
                  if (decimalPos == -1)
                  {
                     retString += Extensions.DECIMAL;
                     decimalPos = retString.PositionOfDecimal();
                  }

                  var trueEndPos = decimalPos + charsToRight;
                  for (var charIdx = decimalPos + 1; charIdx <= trueEndPos; charIdx++)
                  {
                     // char idx of 2 with ret string length of 2 means that retString[2] doesn't yet exist.
                     if (charIdx >= retString.Length)
                     {
                        retString += "0";
                     }
                  }
               }

               return retString;

            case ValidationTypes.WholeNumber:

               //entryText = entryText.StripLeadingZeroes();
               //entryText = entryText.StripTrailingNumbers(_maxDecimalChars);

               return Regex.Replace(entryText, "[^0-9]+", "");

            default:

               // Illegal
               Debug.WriteLine(nameof(NumericEntryValidationBehavior) + ": " + nameof(PrepareTextForEditing) +
                               ": illegal numeric validation type ->" + validationType + "<-");
               break;
         }

         return entryText;
      }

      /// <summary>
      /// Prepares the text for editing.
      /// </summary>
      /// <param name="entryText">The entry text.</param>
      /// <param name="firstFocused">if set to <c>true</c> [first focused].</param>
      /// <returns>System.String.</returns>
      public override string PrepareTextForEditing(string entryText, bool firstFocused = false)
      {
         var retStr =
            StripStringFormatCharacters(entryText, StringFormat, ValidationType, CharsToRightOfDecimal, firstFocused);
         return retStr;
      }

      /// <summary>
      /// Formats the departing text.
      /// </summary>
      /// <param name="entryText">The entry text.</param>
      /// <returns>System.String.</returns>
      protected override string FormatDepartingText(string entryText)
      {
         // Consider reformatting the decimal case
         if (StringFormat.IsNotEmpty())
         {
            // First, fix the decimals
            var preparedStr = PrepareTextForEditing(entryText, true);
            var retStr = string.Format(StringFormat, preparedStr);
            return retStr;
         }

         // ELSE let the base handle it
         return base.FormatDepartingText(entryText);
      }

      /// <summary>
      /// Illegals the character filter.
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="newText">The new text.</param>
      /// <param name="originalText">The original text.</param>
      /// <param name="isOutsideOfRange">if set to <c>true</c> [is outside of range].</param>
      /// <returns>System.String.</returns>
      /// <remarks>We can only evaluate the unmasked text (otherwise all of our tests fail).</remarks>
      protected override string IllegalCharFilter(IEntryValidationBehavior behavior,
                                                  string newText,
                                                  string originalText,
                                                  out bool isOutsideOfRange)
      {
         isOutsideOfRange = false;

         // CRITICAL
         var strippedNewText = StripMaskFromText(newText);
         var strippedOldText = StripMaskFromText(originalText);

         if (strippedNewText.IsEmpty() || strippedNewText.IsSameAs(strippedOldText))
         {
            return newText;
         }

         // Spaces are illegal
         if (strippedNewText.IsNotEmpty() && strippedNewText.Contains(FormsConst.SPACE))
         {
            // FAIL
            return originalText;
         }

         // The new text is not empty and has changed
         if (StringFormat.IsEmpty())
         {
            // If the base rejects, allow that to have control
            // Do not send in stripped values; the base evaluates the mask as well as the net entry.
            var testString = base.IllegalCharFilter(behavior, newText, originalText, out var itOutsideOfRange);
            if (testString.IsSameAs(originalText))
            {
               // Probably failed the length test
               return originalText;
            }
         }

         // Consider illegal min and max values
         if (MaxDecimalNumber.IsLessThanOrEqualTo(MinDecimalNumber))
         {
            Debug.WriteLine(nameof(NumericEntryValidationBehavior) + ": " + nameof(IllegalCharFilter) +
                            ": Max number is less than or equal to min number!!!");
            return originalText;
         }

         // Numbers are either:
         // * "Whole" (no special symbols allowed) or
         // * "Decimal" (a single dot is allowed, plus a certain number of characters after that).
         switch (ValidationType)
         {
            case ValidationTypes.DecimalNumber:
               if (!double.TryParse(strippedNewText, out var testDouble))
               {
                  return originalText;
               }

               isOutsideOfRange = !testDouble.IsGreaterThanOrEqualTo(MinDecimalNumber) ||
                                  !testDouble.IsLessThanOrEqualTo(MaxDecimalNumber);

               // ... else watch out for excessive decimal characters
               var decimalPos = strippedNewText.PositionOfDecimal();
               if (decimalPos > 0)
               {
                  var newTextCharsToRightOfDecimal = strippedNewText.Length - 1 - decimalPos;

                  if (newTextCharsToRightOfDecimal > CharsToRightOfDecimal)
                  {
                     return originalText;
                  }

                  // ELSE fall through and return the new text.
               }

               // ELSE fall through and return the new text.

               break;

            case ValidationTypes.WholeNumber:
               if (!long.TryParse(strippedNewText, out var testLong))
               {
                  return originalText;
               }

               // Does not apply to whole numbers
               isOutsideOfRange = false;

               // ELSE fall through and return the new text.

               break;

            default:

               // This is a text field with numeric constraints

               // Illegal
               Debug.WriteLine(nameof(NumericEntryValidationBehavior) + ": " + nameof(PrepareTextForEditing) +
                               ": illegal numeric validation type ->" + ValidationType + "<-");
               break;
         }

         return newText;
      }

      /// <summary>
      /// Determines whether [is whole entry valid] [the specified behavior].
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="currentText">The current text.</param>
      /// <returns><c>true</c> if [is whole entry valid] [the specified behavior]; otherwise, <c>false</c>.</returns>
      protected override bool IsWholeEntryValid(IEntryValidationBehavior behavior, string currentText)
      {
         // Empty is not illegal for the illegal char case. The user has to be able to clear the entry field. For
         // validation, they fail if there is a min length required. Repeat the controls in the base class, since the
         // string format is empty.
         if (StringFormat.IsEmpty())
         {
            if (
               currentText.IsEmpty() && MinLength > 0
             ||
               currentText.IsNotEmpty() && currentText.Length < MinLength
             ||
               MaxLength > 0 && (currentText.IsEmpty() || currentText.Length < MaxLength)
            )
            {
               return false;
            }
         }

         // ELSE

         // Empty text is handled by the "min" value check

         // The remainder is the same as the illegal char filter
         var testText = IllegalCharFilter(behavior, currentText, FAIL_TEXT, out var isOutsideOfRange);

         // Corner case: the illega char filter lets blank entries through
         if (testText.IsEmpty() && MinDecimalNumber.IsGreaterThan(0) || testText.IsSameAs(FAIL_TEXT) ||
             isOutsideOfRange)
         {
            return false;
         }

         return true;
      }
   }
}
