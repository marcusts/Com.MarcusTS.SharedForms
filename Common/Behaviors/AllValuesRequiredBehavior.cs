// *********************************************************************************
// <copyright file=AllValuesRequiredBehavior.cs company="Marcus Technical Services, Inc.">
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
   ///    Interface IAllValuesRequiredBehavior
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Behaviors.IBehaviorBase" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Behaviors.IBehaviorBase" />
   public interface IAllValuesRequiredBehavior : IBehaviorBase
   {
      /// <summary>
      ///    Gets or sets the minimum length.
      /// </summary>
      /// <value>The minimum length.</value>
      int MinLength { get; set; }
   }

   /// <summary>
   ///    No restrictions on input, but the field *must* have a value.
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Behaviors.BehaviorBase" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Behaviors.IAllValuesRequiredBehavior" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Behaviors.BehaviorBase" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Behaviors.IAllValuesRequiredBehavior" />
   public class AllValuesRequiredBehavior : BehaviorBase, IAllValuesRequiredBehavior
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="AllValuesRequiredBehavior" /> class.
      /// </summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      public AllValuesRequiredBehavior(Action onIsValidChangedAction)
         : base(onIsValidChangedAction,
                (
                   b,
                   s
                ) => s.IsNotEmpty() && s.Length >= (b as AllValuesRequiredBehavior)?.MinLength)
      {
      }

      /// <summary>
      ///    Gets or sets the minimum length.
      /// </summary>
      /// <value>The minimum length.</value>
      public int MinLength { get; set; } = 0;
   }
}