#region License
// Copyright (c) 2020  Marcus Technical Services, Inc. <marcus@marcusts.com>
// 
// This file, PasswordEntryValidationBehavior.cs, is a part of a program called VDT.Mobile.
// 
// VDT.Mobile is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Permission to use, copy, modify, and/or distribute this software
// for any purpose with or without fee is hereby granted, provided
// that the above copyright notice and this permission notice appear
// in all copies.
// 
// VDT.Mobile is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For the complete GNU General Public License,
// see <http://www.gnu.org/licenses/>.
#endregion

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using System;
   using System.Linq;
using Com.MarcusTS.SharedForms.ViewModels;
   using SharedUtils.Utils;
   using Utils;

   public interface IPasswordEntryValidationBehavior : IEntryValidationBehavior, IPasswordProps
   {
   }

   public class PasswordEntryValidationBehavior : EntryValidationBehavior, IPasswordEntryValidationBehavior
   {
      private const int DEFAULT_MIN_CAPITAL_CHARACTER_COUNT = 1;
      private const int DEFAULT_MIN_LOW_CASE_CHARACTER_COUNT = 1;
      public const int DEFAULT_MIN_CHARACTER_COUNT = 8;
      public const int DEFAULT_MAX_CHARACTER_COUNT = 15;
      private const int DEFAULT_MIN_NUMERIC_CHARACTER_COUNT = 0;
      private const int DEFAULT_MIN_SPECIAL_CHARACTER_COUNT = 0;
      private const int DEFAULT_MAX_REPEAT_CHARS = 2;
      private const string VALIDATION_ERROR_PREFIX = "Please enter a password that has at least ";

      public PasswordEntryValidationBehavior(Action onIsValidChangedAction) : base(onIsValidChangedAction)
      {
      }

      public int MinCapitalCharacterCount { get; set; } = DEFAULT_MIN_CAPITAL_CHARACTER_COUNT;
      public int MinCharacterCount { get; set; } = DEFAULT_MIN_CHARACTER_COUNT;
      public int MaxCharacterCount { get; set; } = DEFAULT_MAX_CHARACTER_COUNT;
      public int MinLowCaseCharacterCount { get; set; } = DEFAULT_MIN_LOW_CASE_CHARACTER_COUNT;
      public int MinNumericCharacterCount { get; set; } = DEFAULT_MIN_NUMERIC_CHARACTER_COUNT;
      public int MaxRepeatChars { get; set; } = DEFAULT_MAX_REPEAT_CHARS;
      public int MinSpecialCharacterCount { get; set; } = DEFAULT_MIN_SPECIAL_CHARACTER_COUNT;

      protected override string IllegalCharFilter(IEntryValidationBehavior behavior,
                                                  string                   newText,
                                                  string                   originalText,
                                                  out bool                 isOutsideOfRange)
      {
         isOutsideOfRange = false;

         // strip spaces
         return newText.Trim();
      }

      /// <summary>
      /// Determines whether [is whole entry valid] [the specified behavior].
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="currentText">The current text.</param>
      /// <returns><c>true</c> if [is whole entry valid] [the specified behavior]; otherwise, <c>false</c>.</returns>
      protected override bool IsWholeEntryValid(IEntryValidationBehavior behavior, string currentText)
      {
         if (!base.IsWholeEntryValid(behavior, currentText))
         {
             return false;
         }

         if (currentText.IsEmpty())
         {
            IssueMinLengthValidationError();
            return false;
         }

         var capitalCharacterCount = currentText.Count(char.IsUpper);
         var lowCaseCharacterCount = currentText.Count(char.IsLower);
         var characterCount =  currentText.Count();
         var numericCharacterCount = currentText.Count(char.IsNumber);
         var repeatedAdjacentCharacterCount = currentText.GetAdjacentCharacterCount(MaxRepeatChars);
         var specialCharacterCount =
            currentText.Length - capitalCharacterCount - lowCaseCharacterCount - numericCharacterCount;

         if (characterCount < MinCharacterCount)
         {
            IssueMinLengthValidationError();
            return false;
         }

         // Max characters is restricted inside the entry validator

         if (characterCount < MinCharacterCount)
         {
            IssueMinLengthValidationError();
            return false;
         }

         if (capitalCharacterCount < MinCapitalCharacterCount)
         {
            IssueValidationError(MinCapitalCharacterCount + " total capital characters.");
            return false;
         }

         if (lowCaseCharacterCount < MinLowCaseCharacterCount)
         {
            IssueValidationError(MinLowCaseCharacterCount + " total lower-case characters.");
            return false;
         }

         if (numericCharacterCount < MinNumericCharacterCount)
         {
            IssueValidationError(MinNumericCharacterCount + " total numeric characters.");
            return false;
         }

         if (specialCharacterCount < MinSpecialCharacterCount)
         {
            IssueValidationError(MinSpecialCharacterCount + " total special characters.");
            return false;
         }

         if (repeatedAdjacentCharacterCount > 0)
         {
            LastValidationError = "The password should not have more than " + MaxRepeatChars + " adjacent characters";
            return false;
         }
         
         ClearValidationError();
         return true;
      }
      
      private void ClearValidationError()
      {
         LastValidationError = "";
      }

      private void IssueMinLengthValidationError()
      {
         IssueValidationError(MinCharacterCount + " total characters.");
      }

      private void IssueValidationError(string validationSuffix)
      {
         LastValidationError = VALIDATION_ERROR_PREFIX + validationSuffix;
      }
   }
}
