// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=ScaleUtils.cs
// company="Marcus Technical Services, Inc.">
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
   using Xamarin.Essentials;
   using Xamarin.Forms;

   /// <summary>
   /// 
   /// </summary>
   public static class ScaleUtils
   {
      // The baseline for fonts, widths and heights of buttons etc., is an iPhone X: 375 pixels.
      //    All other phones will scale to ths based on their screen width.
      private const double BASELINE_SCREEN_WIDTH = 375;

      // The operating system is also a potential factor
      private const double IOS_MULTIPLIER = 1.0;

      private const double ANDROID_MULTIPLIER = 1.15;

      private const double BASELINE_SCREEN_HEIGHT = 812;

      public static readonly double CURRENT_DEVICE_HEIGHT = 
         DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;

      private static readonly double CURRENT_DEVICE_LENGTH_RATIO =
         CURRENT_DEVICE_HEIGHT / BASELINE_SCREEN_HEIGHT;

      public static readonly double CURRENT_DEVICE_WIDTH = 
         DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

      private static readonly double CURRENT_DEVICE_WIDTH_RATIO =
         CURRENT_DEVICE_WIDTH / BASELINE_SCREEN_WIDTH;

      private static readonly double MAX_OS_AND_DEVICE_ADJUSTMENT =
         Math.Min(CURRENT_DEVICE_WIDTH_RATIO, CURRENT_DEVICE_LENGTH_RATIO);

      /// <summary>
      /// MUST GO FIRST
      /// </summary>
      public static double AdjustForOsAndDevice(this double startingSize)
      {
         var factor = 
            Math.Min(MAX_OS_AND_DEVICE_ADJUSTMENT,
            CURRENT_DEVICE_WIDTH_RATIO *
            (FormsUtils.IsIos() ? IOS_MULTIPLIER : ANDROID_MULTIPLIER));
         
         var retSize = startingSize * factor;

         return retSize;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="namedSize"></param>
      /// <param name="additionalFactor"></param>
      /// <returns></returns>
      public static double AdjustForOsAndDevice(this NamedSize namedSize, double additionalFactor = 1.0)
      {
         return Device.GetNamedSize(namedSize, typeof(Label)).AdjustForOsAndDevice() * additionalFactor;
      }
   }
}