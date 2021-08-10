// ********************************************************************************* <copyright
// file=EmailEntryValidatorBehavior.cs company="Marcus Technical Services, Inc."> Copyright @2019 Marcus Technical Services,
// Inc. </copyright>
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

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using System;
   using SharedUtils.Utils;

   /// <summary>Class EmailEntryValidatorBehavior. Implements the <see cref="EntryValidationBehavior" /></summary>
   /// <seealso cref="EntryValidationBehavior" />
   public class EmailEntryValidatorBehavior : EntryValidationBehavior
   {
      // Can have a period as long as it is not at the start or end of either the local part or the domain.
      /// <summary>At sign</summary>
      private const char AT_SIGN = '@';

      // private const string REG_EX_VALID_CHARS = "^[a–zA–Z0-9!#$%&‘*+/=?^_`{|}~.-]*$";
      /// <summary>The reg ex valid chars</summary>
      private const string REG_EX_VALID_CHARS = "^[a-zA-Z0-9.]*$";

      /// <summary>Initializes a new instance of the <see cref="EmailEntryValidatorBehavior" /> class.</summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public EmailEntryValidatorBehavior(Action onIsValidChangedAction)
         : base(onIsValidChangedAction)
      {
      }

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

      protected override string IllegalCharFilter(
         IEntryValidationBehavior behavior,
         string                   newText,
         string                   originalText,
         out bool isOutsideOfRange)
      {
         return EmailIllegalCharFunc(base.IllegalCharFilter(behavior, newText, originalText, out isOutsideOfRange), out isOutsideOfRange);
      }

      private static string EmailIllegalCharFunc(string newText, out bool isOutsideOfRange)
      {
         isOutsideOfRange = false;

         // Overall: too much complexity for easy management; will just focus on completely illegal characters, spaces,
         // etc. But as the user types, we have to allow partially accurate values so the user can complete their work.
         var retStr      = string.Empty;
         var atSignFound = false;

         foreach (var c in newText)
         {
            if (!atSignFound && (c == AT_SIGN))
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