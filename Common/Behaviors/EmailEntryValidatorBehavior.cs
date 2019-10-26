#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, EmailEntryValidatorBehavior.cs, is a part of a program called AccountViewMobile.
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
   /// Class EmailEntryValidatorBehavior. Implements the <see cref="EntryValidationBehavior" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Behaviors.EntryValidationBehavior" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Behaviors.EntryValidationBehavior" />
   /// <seealso cref="EntryValidationBehavior" />
   public class EmailEntryValidatorBehavior : EntryValidationBehavior
   {
      // Can have a period as long as it is not at the start or end of either the local part or the domain.
      /// <summary>
      /// At sign
      /// </summary>
      private const char AT_SIGN = '@';

      // private const string REG_EX_VALID_CHARS = "^[a–zA–Z0-9!#$%&‘*+/=?^_`{|}~.-]*$";
      /// <summary>
      /// The reg ex valid chars
      /// </summary>
      private const string REG_EX_VALID_CHARS = "^[a-zA-Z0-9.]*$";

      /// <summary>
      /// Initializes a new instance of the <see cref="EmailEntryValidatorBehavior" /> class.
      /// </summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public EmailEntryValidatorBehavior(Action onIsValidChangedAction)
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
      protected override string IllegalCharFilter(
         IEntryValidationBehavior behavior,
         string                   newText,
         string                   originalText,
         out bool                 isOutsideOfRange)
      {
         return EmailIllegalCharFunc(
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

                   // Original does *not* work at all
                   // @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@)) (?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$"
                   // https://www.rhyous.com/2010/06/15/regular-expressions-in-cincluding-a-new-comprehensive-email-pattern/
                   //@"^ (([^<> ()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$"
                   // https://msdn.microsoft.com/en-us/library/01escwtf(v=vs.110).aspx
                   @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$"
                );
      }

      /// <summary>
      /// Emails the illegal character function.
      /// </summary>
      /// <param name="behaviorBase">The behavior base.</param>
      /// <param name="newText">The new text.</param>
      /// <param name="isOutsideOfRange">if set to <c>true</c> [is outside of range].</param>
      /// <returns>System.String.</returns>
      private static string EmailIllegalCharFunc(IEntryValidationBehavior behaviorBase, string newText,
                                                 out bool                 isOutsideOfRange)
      {
         isOutsideOfRange = false;

         // Overall: too much complexity for easy management; will just focus on completely illegal characters, spaces,
         // etc. But as the user types, we have to allow partially accurate values so the user can complete their work.
         var retStr      = string.Empty;
         var atSignFound = false;

         foreach (var c in newText)
         {
            if (!atSignFound && c == AT_SIGN)
            {
               retStr      += c;
               atSignFound =  true;
            }
            else if (c.ToString().IsNonNullRegexMatch(REG_EX_VALID_CHARS))
            {
               retStr += c;
            }
         }

         return retStr;
      }
   }
}