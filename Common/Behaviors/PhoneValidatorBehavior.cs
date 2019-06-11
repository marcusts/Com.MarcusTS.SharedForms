// *********************************************************************************
// <copyright file=PhoneValidatorBehavior.cs company="Marcus Technical Services, Inc.">
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
   ///    Class PhoneValidatorBehavior.
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Behaviors.BehaviorBase" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Behaviors.BehaviorBase" />
   public class PhoneValidatorBehavior : BehaviorBase
   {
      /// <summary>
      ///    The reg ex valid chars
      /// </summary>
      private const string REG_EX_VALID_CHARS = @"^[\-(). 0-9]*$";

      /// <summary>
      ///    Initializes a new instance of the <see cref="PhoneValidatorBehavior" /> class.
      /// </summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public PhoneValidatorBehavior(Action onIsValidChangedAction)
         : base
         (
            onIsValidChangedAction,
            (
               b,
               s
            ) => s.IsNonNullRegexMatch
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
            ),
            PhoneNumberIllegalCharFunc
         )
      {
      }

      /// <summary>
      ///    Phones the number illegal character function.
      /// </summary>
      /// <param name="arg">The argument.</param>
      /// <returns>System.String.</returns>
      private static string PhoneNumberIllegalCharFunc(string arg)
      {
         // Overall: some complexity in what to allow, including spaces -- ??? or dots -- ??
         // But as the user types, we have to allow partially accurate values so the user can complete their work.
         var retStr = string.Empty;

         foreach (var c in arg)
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