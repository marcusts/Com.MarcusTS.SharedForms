﻿// *********************************************************************************
// <copyright file=FormsExtensions.cs company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Common.Utils
{
   using System;
   using Com.MarcusTS.SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   ///    Class FormsExtensions.
   /// </summary>
   public static class FormsExtensions
   {
      /// <summary>
      ///    Determines whether [is different than] [the specified other rect].
      /// </summary>
      /// <param name="mainRect">The main rect.</param>
      /// <param name="otherRect">The other rect.</param>
      /// <returns><c>true</c> if [is different than] [the specified other rect]; otherwise, <c>false</c>.</returns>
      public static bool IsDifferentThan
      (
         this Rectangle mainRect,
         Rectangle      otherRect
      )
      {
         return !mainRect.IsSameAs(otherRect);
      }

      /// <summary>
      ///    Determines whether [is same as] [the specified other rect].
      /// </summary>
      /// <param name="mainRect">The main rect.</param>
      /// <param name="otherRect">The other rect.</param>
      /// <returns><c>true</c> if [is same as] [the specified other rect]; otherwise, <c>false</c>.</returns>
      public static bool IsSameAs
      (
         this Rectangle mainRect,
         Rectangle      otherRect
      )
      {
         return mainRect.Width.IsSameAs(otherRect.Width)
          &&
            mainRect.Height.IsSameAs(otherRect.Height)
          &&
            mainRect.X.IsSameAs(otherRect.X)
          &&
            mainRect.Y.IsSameAs(otherRect.Y);
      }

      public static bool IsEmpty
      (
         this Rectangle mainRect
      )
      {
         return mainRect.X.IsLessThanOrEqualTo(0)     &&
                mainRect.Y.IsLessThanOrEqualTo(0)     &&
                mainRect.Width.IsLessThanOrEqualTo(0) &&
                mainRect.Height.IsLessThanOrEqualTo(0);
      }

      public static bool IsNotEmpty
      (
         this Rectangle mainRect
      )
      {
         return !mainRect.IsEmpty();
      }

      public static double CheckAgainstZero(this double dbl)
      {
         return Math.Max(0, dbl);
      }

      public static Rectangle FixNegativeDimensions(this Rectangle rect)
      {
         return new Rectangle(CheckAgainstZero(rect.X), CheckAgainstZero(rect.Y), CheckAgainstZero(rect.Width), CheckAgainstZero(rect.Height));
      }
   }
}