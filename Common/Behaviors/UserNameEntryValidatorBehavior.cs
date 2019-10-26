#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, UserNameEntryValidatorBehavior.cs, is a part of a program called AccountViewMobile.
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
   /// Class UserNameEntryValidatorBehavior.
   /// Implements the <see cref="EntryValidationBehavior" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Behaviors.EntryValidationBehavior" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Behaviors.EntryValidationBehavior" />
   /// <seealso cref="EntryValidationBehavior" />
   public class UserNameEntryValidatorBehavior : EntryValidationBehavior
   {
      /// <summary>
      /// The maximum user name length
      /// </summary>
      private const int MAX_USER_NAME_LEN = 16;

      /// <summary>
      /// The reg ex valid chars
      /// </summary>
      private const string REG_EX_VALID_CHARS = "^[a-zA-Z0-9]*$";

      /// <summary>
      /// Initializes a new instance of the <see cref="UserNameEntryValidatorBehavior" /> class.
      /// </summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public UserNameEntryValidatorBehavior(Action onIsValidChangedAction = null)
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
         return UserNameIllegalCharFunc(
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
         return base.IsWholeEntryValid(behavior, currentText) &&
                currentText.IsNonNullRegexMatch(@"^[a-zA-Z0-9]{1,16}$");
      }

      /// <summary>
      /// Users the name illegal character function.
      /// </summary>
      /// <param name="behaviorBase">The behavior base.</param>
      /// <param name="newText">The new text.</param>
      /// <param name="isOutsideOfRange">if set to <c>true</c> [is outside of range].</param>
      /// <returns>System.String.</returns>
      private static string UserNameIllegalCharFunc(IEntryValidationBehavior behaviorBase, string newText,
                                                    out bool                 isOutsideOfRange)
      {
         isOutsideOfRange = false;

         // Overall: allow numbers and characters
         // But as the user types, we have to allow partially accurate values so the user can complete their work.
         var retStr = string.Empty;

         for (var charIdx = 0; charIdx < Math.Min(newText.Length, MAX_USER_NAME_LEN); charIdx++)
         {
            var c = newText[charIdx];

            if (c.ToString().IsNonNullRegexMatch(REG_EX_VALID_CHARS))
            {
               retStr += c;
            }
         }

         // return retStr;

         // The API stores this way
         return retStr.ToLower();
      }
   }
}