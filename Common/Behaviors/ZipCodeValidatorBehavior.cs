// *********************************************************************************
// <copyright file=ZipCodeValidatorBehavior.cs company="Marcus Technical Services, Inc.">
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
   ///    Class ZipCodeValidatorBehavior.
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Behaviors.BehaviorBase" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Behaviors.BehaviorBase" />
   public class ZipCodeValidatorBehavior : BehaviorBase
   {
      /// <summary>
      ///    The hyphen
      /// </summary>
      private const char HYPHEN = '-';

      /// <summary>
      ///    Initializes a new instance of the <see cref="ZipCodeValidatorBehavior" /> class.
      /// </summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public ZipCodeValidatorBehavior(Action onIsValidChangedAction)
         : base(onIsValidChangedAction,
                (
                   b,
                   s
                ) => s.IsNonNullRegexMatch(@"^\d{5}(?:[-\s]\d{4})?$"),
                ZipCodeIllegalCharFunc)
      {
      }

      /// <summary>
      ///    Zips the code illegal character function.
      /// </summary>
      /// <param name="arg">The argument.</param>
      /// <returns>System.String.</returns>
      private static string ZipCodeIllegalCharFunc(string arg)
      {
         // Overall: only allow numbers and one hyphen
         // But as the user types, we have to allow partially accurate values so the user can complete their work.
         var retStr = string.Empty;

         for (var charIdx = 0; charIdx < arg.Length; charIdx++)
         {
            var c = arg[charIdx];

            if (charIdx == 5)
            {
               retStr += HYPHEN;
            }
            else if (c >= '0' && c <= '9')
            {
               retStr += c;
            }
         }

         return retStr;
      }
   }
}