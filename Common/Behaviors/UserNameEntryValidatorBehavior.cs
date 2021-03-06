// *********************************************************************************
// <copyright file=UserNameEntryValidatorBehavior.cs company="Marcus Technical Services, Inc.">
//     Copyright @2019 Marcus Technical Services, Inc.
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

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using Com.MarcusTS.SharedUtils.Utils;
   using System;

   /// <summary>
   ///    Class UserNameEntryValidatorBehavior.
   ///    Implements the <see cref="EntryValidationBehavior" />
   /// </summary>
   /// <seealso cref="EntryValidationBehavior" />
   public class UserNameEntryValidatorBehavior : EntryValidationBehavior
   {
      /// <summary>
      ///    The maximum user name length
      /// </summary>
      private const int MAX_USER_NAME_LEN = 16;

      /// <summary>
      ///    The reg ex valid chars
      /// </summary>
      private const string REG_EX_VALID_CHARS = "^[a-zA-Z0-9]*$";

      /// <summary>
      ///    Initializes a new instance of the <see cref="UserNameEntryValidatorBehavior" /> class.
      /// </summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public UserNameEntryValidatorBehavior(Action onIsValidChangedAction = null)
         : base(onIsValidChangedAction)
      {
      }

      protected override string IllegalCharFilter(IEntryValidationBehavior behavior, string newText, string originalText, out bool isOutsideOfRange)
      {
         return UserNameIllegalCharFunc(behavior, base.IllegalCharFilter(behavior, newText, originalText, out isOutsideOfRange), out isOutsideOfRange);
      }

      protected override bool IsWholeEntryValid(IEntryValidationBehavior behavior, string currentText)
      {
         return base.IsWholeEntryValid(behavior, currentText) &&
                currentText.IsNonNullRegexMatch(@"^[a-zA-Z0-9]{1,16}$");
      }

      private static string UserNameIllegalCharFunc(IEntryValidationBehavior behaviorBase, string newText, out bool isOutsideOfRange)
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