﻿// ********************************************************************************* <copyright file=ThemeUtils.cs
// company="Marcus Technical Services, Inc."> Copyright 2019 Marcus Technical Services, Inc. </copyright>
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

namespace Com.MarcusTS.SharedForms.Common.Utils
{
   using Xamarin.Forms;

   /// <summary>Class ThemeUtils.</summary>
   /// <remarks>
   ///    https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/fonts
   ///               IOS      Android  UWP
   ///    Default	   16       14	      14
   ///    Micro	   11	      10	      15.667
   ///    Small	   13	      14	      18.667
   ///    Medium	   16	      17	      22.667
   ///    Large	   20	      22	      32
   ///    Body	      17	      16	      14
   ///    Header	   17	      96	      46
   ///    Title	   28	      24	      24
   ///    Subtitle	22	      16	      20
   ///    Caption	   12	      12	      12
   /// </remarks>
   public static class ThemeUtils
   {
      public static readonly Color DARK_THEME_COLOR = Color.FromRgb(0, 48, 87);

      public static readonly Color LIGHT_THEME_COLOR = Color.FromRgb(0, 153, 216);
   }
}