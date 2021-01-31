// ********************************************************************************* <copyright
// file=PhoneEntryValidatorBehavior.cs company="Marcus Technical Services, Inc."> Copyright @2019 Marcus Technical
// Services, Inc. </copyright>
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

   /// <summary>Class PhoneEntryValidatorBehavior. Implements the <see cref="EntryValidationBehavior"/></summary>
   /// <seealso cref="EntryValidationBehavior"/>
   public class PhoneEntryValidatorBehavior : EntryValidationBehavior
   {
      private const int MAX_PHONE_NUMBER_LENGTH = 10;

      /// <summary>Initializes a new instance of the <see cref="PhoneEntryValidatorBehavior"/> class.</summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public PhoneEntryValidatorBehavior(Action onIsValidChangedAction)
         : base(onIsValidChangedAction)
      {
      }

      protected override string IllegalCharFilter(IEntryValidationBehavior behavior, string newText, string originalText, out bool isOutsideOfRange)
      {
         return PhoneNumberIllegalCharFunc(base.IllegalCharFilter(behavior, newText, originalText, out isOutsideOfRange), out isOutsideOfRange);
      }

      /// <summary>Phones the number illegal character function.</summary>
      private static string PhoneNumberIllegalCharFunc(string newText, out bool isOutsideOfRange)
      {
         isOutsideOfRange = false;

         // Overall: some complexity in what to allow, including spaces -- ??? or dots -- ?? But as the user types, we
         // have to allow partially accurate values so the user can complete their work.
         var retStr = string.Empty;

         foreach (var c in newText)
         {
            if (char.IsNumber(c) && retStr.Length < MAX_PHONE_NUMBER_LENGTH)
            {
               retStr += c;
            }
         }

         return retStr;
      }
   }
}
