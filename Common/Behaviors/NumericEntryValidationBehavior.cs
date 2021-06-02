// ********************************************************************************* <copyright
// file=ValidationBehavior.cs company="Marcus Technical Services, Inc."> Copyright @2019 Marcus Technical Services, Inc.
// </copyright>
//
// MIT License
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
// Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// *********************************************************************************

// #define STOP_REVERSING_VALUES

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using System;
   using System.Diagnostics;
   using System.Text.RegularExpressions;
   using SharedUtils.Utils;
   using Utils;
   using ViewModels;

   public interface INumericEntryValidationBehavior : IEntryValidationBehavior, IHaveMinAndMaxNumbers
   {
   }

   public class NumericEntryValidationBehavior : EntryValidationBehavior, INumericEntryValidationBehavior
   {
      private const string FAIL_TEXT = "Fail!^%$^%(&*)_()_)*((*^&";

      public NumericEntryValidationBehavior(Action onIsValidChangedAction) : base(onIsValidChangedAction)
      {
      }

      public int          CharsToRightOfDecimal { get; set; }
      public double       MaxDecimalNumber      { get; set; }
      public double       MinDecimalNumber      { get; set; }
      public NumericTypes NumericType           { get; set; }

      public override string PrepareTextForEditing(string entryText, bool firstFocused = false)
      {
         var retStr = StripStringFormatCharacters(entryText, StringFormat, ValidationType, CharsToRightOfDecimal, firstFocused);
         return retStr;
      }

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
      /// This method must also *restore* a decimal and trailing zeroes when first focused.
      /// </summary>
      public static string StripStringFormatCharacters(string          entryText, 
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
               Debug.WriteLine(nameof(NumericEntryValidationBehavior) + ": "           + nameof(PrepareTextForEditing) +
                               ": illegal numeric validation type ->" + validationType + "<-");
               break;
         }

         return entryText;
      }

      /// <remarks>
      /// We can only evaluate the unmasked text (otherwise all of our tests fail).
      /// </remarks>
      protected override string IllegalCharFilter(IEntryValidationBehavior behavior, 
                                                  string                   newText,
                                                  string                   originalText,
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

               isOutsideOfRange = !testDouble.IsGreaterThanOrEqualTo(MinDecimalNumber) || !testDouble.IsLessThanOrEqualTo(MaxDecimalNumber);

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

      protected override bool IsWholeEntryValid(IEntryValidationBehavior behavior, string currentText)
      {
         if (!base.IsWholeEntryValid(behavior, currentText))
         {
            return false;
         }
         
         // Empty is not illegal for the illegal char case. The user has to be able to clear the entry field. For
         // validation, they fail if there is a min length required. Repeat the controls in the base class, since the
         // string format is empty.
         if (StringFormat.IsEmpty())
         {
            if (
                  (currentText.IsEmpty() && MinLength > 0)
                  ||
                  (currentText.IsNotEmpty() && currentText.Length < MinLength)
                  ||
                  (MaxLength > 0 && (currentText.IsEmpty() || currentText.Length < MaxLength))
               )
            {
               return false;
            }
         }

         // ELSE

         // Empty text is handled by the "min" value check

         // The remainder is the same as the illegal char filter
         var testText = IllegalCharFilter(behavior, currentText, FAIL_TEXT, out var isOutsideOfRange);

         // Corner case: the illegal char filter lets blank entries through
         if ((testText.IsEmpty() && MinDecimalNumber.IsGreaterThan(0)) || testText.IsSameAs(FAIL_TEXT) || isOutsideOfRange)
         {
            return false;
         }

         return true;
      }
   }
}
