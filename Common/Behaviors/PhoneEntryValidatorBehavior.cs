#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, PhoneEntryValidatorBehavior.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using SharedUtils.Utils;
   using System;

   /// <summary>
   /// Class PhoneEntryValidatorBehavior. Implements the <see cref="EntryValidationBehavior" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Behaviors.EntryValidationBehavior" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Behaviors.EntryValidationBehavior" />
   /// <seealso cref="EntryValidationBehavior" />
   public class PhoneEntryValidatorBehavior : EntryValidationBehavior
   {
      /// <summary>
      /// The reg ex valid chars
      /// </summary>
      private const string REG_EX_VALID_CHARS = @"^[\-(). 0-9]*$";

      /// <summary>
      /// Initializes a new instance of the <see cref="PhoneEntryValidatorBehavior" /> class.
      /// </summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public PhoneEntryValidatorBehavior(Action onIsValidChangedAction)
         : base(onIsValidChangedAction)
      {
      }

      /// <summary>
      /// Illegals the character filter.
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="newText">The new text.</param>
      /// <param name="originalText">The original text.</param>
      /// <param name="isOutsideOfRange">if set to <c>true</c> [is outside of range].</param>
      /// <returns>System.String.</returns>
      protected override string IllegalCharFilter(IEntryValidationBehavior behavior,     string   newText,
                                                  string                   originalText, out bool isOutsideOfRange)
      {
         return PhoneNumberIllegalCharFunc(
            behavior, base.IllegalCharFilter(behavior, newText, originalText, out isOutsideOfRange),
            out isOutsideOfRange);
      }

      /// <summary>
      /// Determines whether [is whole entry valid] [the specified behavior].
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="currentText">The current text.</param>
      /// <returns><c>true</c> if [is whole entry valid] [the specified behavior]; otherwise, <c>false</c>.</returns>
      protected override bool IsWholeEntryValid(IEntryValidationBehavior behavior, string currentText)
      {
         return base.IsWholeEntryValid(behavior, currentText) && currentText.IsNonNullRegexMatch
                (

                   //@"
                   //^                  # From Beginning of line
                   //(?:\(?)            # Match but don't capture optional (
                   //(?<AreaCode>\d{3}) # 3 digit area code
                   //(?:[-\).\s]?)      # Optional ) or - or . or space
                   //(?<Prefix>\d{3})   # Prefix
                   //(?:[-\.\s]?)       # optional - or . or space
                   //(?<Suffix>\d{4})   # Suffix
                   //(?!\d)             # Fail if eleventh number found"
                   // http://stackoverflow.com/questions/4338267/validate-phone-number-with-javascript
                   // @"/^[+]*[(]{0,1}[0-9]{1,3}[)]{0,1}[-\s\./0-9]*$/g"
                   // http://stackoverflow.com/questions/29970244/how-to-validate-a-phone-number
                   // @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$"
                   // http://stackoverflow.com/questions/8596088/c-sharp-regex-phone-number-check
                   // @"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$"
                   // http://stackoverflow.com/questions/8596088/c-sharp-regex-phone-number-check
                   @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})"
                );
      }

      /// <summary>
      /// Phones the number illegal character function.
      /// </summary>
      /// <param name="behaviorBase">The behavior base.</param>
      /// <param name="newText">The new text.</param>
      /// <param name="isOutsideOfRange">if set to <c>true</c> [is outside of range].</param>
      /// <returns>System.String.</returns>
      private static string PhoneNumberIllegalCharFunc(IEntryValidationBehavior behaviorBase, string newText,
                                                       out bool                 isOutsideOfRange)
      {
         isOutsideOfRange = false;

         // Overall: some complexity in what to allow, including spaces -- ??? or dots -- ?? But as the user types, we
         // have to allow partially accurate values so the user can complete their work.
         var retStr = string.Empty;

         foreach (var c in newText)
         {
            if (c.ToString().IsNonNullRegexMatch(REG_EX_VALID_CHARS))
            {
               retStr += c;
            }
         }

         return retStr;
      }
   }
}