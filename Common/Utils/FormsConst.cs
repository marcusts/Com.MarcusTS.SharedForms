#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, FormsConst.cs, is a part of a program called AccountViewMobile.
//
// AccountViewMobile is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Permission to use, copy, modify, and/or distribute this software
// for any purpose with or without fee is hereby granted, provided
// that the above copyright notice and this permission notice appear
// in all copies.
//
// AccountViewMobile is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// For the complete GNU General Public License,
// see <http://www.gnu.org/licenses/>.

#endregion

namespace Com.MarcusTS.SharedForms.Common.Utils
{
   using Services;
   using System;
   using Xamarin.Forms;

   /// <summary>
   /// Class FormsConst.
   /// </summary>
   /// <remarks>https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/fonts
   /// IOS     Android    UWP
   /// Default	   16       14	      14
   /// Micro	   11	      10	      15.667
   /// Small	   13	      14	      18.667
   /// Medium	   16	      17	      22.667
   /// Large	   20	      22	      32
   /// Body	      17	      16	      14
   /// Header	   17	      96	      46
   /// Title	   28	      24	      24
   /// Subtitle	22	      16	      20
   /// Caption	   12	      12	      12</remarks>
   public static class FormsConst
   {
      /// <summary>
      /// Enum OffScreenPositions
      /// </summary>
      public enum OffScreenPositions
      {
         /// <summary>
         /// The none
         /// </summary>
         NONE,

         /// <summary>
         /// The left
         /// </summary>
         LEFT,

         /// <summary>
         /// The top
         /// </summary>
         TOP,

         /// <summary>
         /// The right
         /// </summary>
         RIGHT,

         /// <summary>
         /// The bottom
         /// </summary>
         BOTTOM
      }

      /// <summary>
      /// Enum OnScreenPositions
      /// </summary>
      public enum OnScreenPositions
      {
         /// <summary>
         /// The none
         /// </summary>
         NONE,

         /// <summary>
         /// The left center
         /// </summary>
         LEFT_CENTER,

         /// <summary>
         /// The top left
         /// </summary>
         TOP_LEFT, // Same as Left_Upper

         /// <summary>
         /// The top center
         /// </summary>
         TOP_CENTER,

         /// <summary>
         /// The top right
         /// </summary>
         TOP_RIGHT, // Same as Right_Upper

         /// <summary>
         /// The right center
         /// </summary>
         RIGHT_CENTER,

         /// <summary>
         /// The bottom left
         /// </summary>
         BOTTOM_LEFT, // Same as Left_Lower

         /// <summary>
         /// The bottom center
         /// </summary>
         BOTTOM_CENTER,

         /// <summary>
         /// The bottom right
         /// </summary>
         BOTTOM_RIGHT, // Same as Right_Lower

         /// <summary>
         /// The center
         /// </summary>
         CENTER
      }

      /// <summary>
      /// Enum StageHeaderPositions
      /// </summary>
      public enum StageHeaderPositions
      {
         /// <summary>
         /// The none
         /// </summary>
         NONE,

         /// <summary>
         /// The top
         /// </summary>
         TOP
      }

      /// <summary>
      /// Enum StageToolbarPositions
      /// </summary>
      public enum StageToolbarPositions
      {
         /// <summary>
         /// The none
         /// </summary>
         NONE,

         /// <summary>
         /// The bottom
         /// </summary>
         BOTTOM,

         /// <summary>
         /// The left
         /// </summary>
         LEFT,

         /// <summary>
         /// The top
         /// </summary>
         TOP,

         /// <summary>
         /// The right
         /// </summary>
         RIGHT
      }

      /// <summary>
      /// Enum SubStageFlowDirections
      /// </summary>
      public enum SubStageFlowDirections
      {
         /// <summary>
         /// The left to right
         /// </summary>
         LEFT_TO_RIGHT,

         /// <summary>
         /// The right to left
         /// </summary>
         RIGHT_TO_LEFT,

         /// <summary>
         /// The top to bottom
         /// </summary>
         TOP_TO_BOTTOM,

         /// <summary>
         /// The bottom to top
         /// </summary>
         BOTTOM_TO_TOP
      }

      /// <summary>
      /// Enum ViewAlignments
      /// </summary>
      public enum ViewAlignments
      {
         /// <summary>
         /// The start
         /// </summary>
         START,

         /// <summary>
         /// The middle
         /// </summary>
         MIDDLE,

         /// <summary>
         /// The end
         /// </summary>
         END,

         /// <summary>
         /// The justify
         /// </summary>
         JUSTIFY
      }

      /// <summary>
      /// The button radius factor
      /// </summary>
      public const double BUTTON_RADIUS_FACTOR = 0.15f;

      // 0.85 works for denser copy
      /// <summary>
      /// The characters to font size estimator
      /// </summary>
      public const double CHARACTERS_TO_FONT_SIZE_ESTIMATOR = 1;

      /// <summary>
      /// The credit card text mask
      /// </summary>
      public const string CREDIT_CARD_TEXT_MASK     = "XXXX-XXXX-XXXX-XXXX";
      /// <summary>
      /// The currency column width
      /// </summary>
      public const double CURRENCY_COLUMN_WIDTH     = 65;
      /// <summary>
      /// The currency string format
      /// </summary>
      public const string CURRENCY_STRING_FORMAT    = "{0:C}";
      /// <summary>
      /// The date column width
      /// </summary>
      public const double DATE_COLUMN_WIDTH         = 65;
      /// <summary>
      /// The date time format
      /// </summary>
      public const string DATE_TIME_FORMAT          = "{0:M/d/yy - h:mm tt}";
      /// <summary>
      /// The default shape view radius
      /// </summary>
      public const float  DEFAULT_SHAPE_VIEW_RADIUS = 6;

      /// <summary>
      /// The default text size
      /// </summary>
      public const float DEFAULT_TEXT_SIZE = 20;

      /// <summary>
      /// The false string
      /// </summary>
      public const string FALSE_STR = "false";

      /// <summary>
      /// The haptic vibration milliseconds
      /// </summary>
      public const float HAPTIC_VIBRATION_MILLISECONDS = 250;

      /// <summary>
      /// The height property name
      /// </summary>
      public const string HEIGHT_PROPERTY_NAME = "Height";

      /// <summary>
      /// The ios nested stage reduction
      /// </summary>
      public const float IOS_NESTED_STAGE_REDUCTION = -IOS_TOP_MARGIN;

      // public const float IOS_TOP_MARGIN = 20;
      /// <summary>
      /// The ios top margin
      /// </summary>
      public const float IOS_TOP_MARGIN = 30;

      /// <summary>
      /// The legal aspect ratio
      /// </summary>
      public const double LEGAL_ASPECT_RATIO = 8.5 / 14;

      /// <summary>
      /// The ios top margin
      /// </summary>
      public const string LONG_DATE_FORMAT = "{0:MMM d, yyyy}";

      /// <summary>
      /// The no scale
      /// </summary>
      public const double NO_SCALE = 0;

      /// <summary>
      /// The normal button font size
      /// </summary>
      public const double NORMAL_BUTTON_FONT_SIZE = 20;

      /// <summary>
      /// The normal scale
      /// </summary>
      public const double NORMAL_SCALE = 1;

      /// <summary>
      /// The not visible opacity
      /// </summary>
      public const double NOT_VISIBLE_OPACITY = 0;

      /// <summary>
      /// The selected button font size
      /// </summary>
      public const double SELECTED_BUTTON_FONT_SIZE = NORMAL_BUTTON_FONT_SIZE * 1.1;

      /// <summary>
      /// The short date format
      /// </summary>
      public const string SHORT_DATE_FORMAT        = "{0:M/d/yy}";
      /// <summary>
      /// The simple date format
      /// </summary>
      public const string SIMPLE_DATE_FORMAT       = "{0:M/d/yyyy}";
      /// <summary>
      /// The space
      /// </summary>
      public const string SPACE                    = " ";
      /// <summary>
      /// The space character
      /// </summary>
      public const char   SPACE_CHAR               = ' ';
      /// <summary>
      /// The standard keyboard number
      /// </summary>
      public const int    STANDARD_KEYBOARD_NUMBER = 0;

      /// <summary>
      /// The true string
      /// </summary>
      public const string TRUE_STR = "true";

      /// <summary>
      /// The visible opacity
      /// </summary>
      public const double VISIBLE_OPACITY = 1;

      /// <summary>
      /// The width property name
      /// </summary>
      public const string WIDTH_PROPERTY_NAME = "Width";

      /// <summary>
      /// The x property name
      /// </summary>
      public const string X_PROPERTY_NAME = "X";

      /// <summary>
      /// The y property name
      /// </summary>
      public const string Y_PROPERTY_NAME = "Y";

      /// <summary>
      /// The zero character
      /// </summary>
      public const           char   ZERO_CHAR                  = '0';
      /// <summary>
      /// The button bounce milliseconds
      /// </summary>
      public static readonly uint   BUTTON_BOUNCE_MILLISECONDS = 75;
      /// <summary>
      /// The default font color
      /// </summary>
      public static readonly Color  DEFAULT_FONT_COLOR         = Color.Black;
      /// <summary>
      /// The editable view font size
      /// </summary>
      public static readonly double EDITABLE_VIEW_FONT_SIZE    = Device.GetNamedSize(NamedSize.Small, typeof(View));
      /// <summary>
      /// The estimated keyboard height
      /// </summary>
      public static readonly double ESTIMATED_KEYBOARD_HEIGHT  = 225.0.AdjustForOsAndDevice();

      /// <summary>
      /// The major button height
      /// </summary>
      public static readonly double MAJOR_BUTTON_HEIGHT = 45.0;

      /// <summary>
      /// The major button width
      /// </summary>
      public static readonly double MAJOR_BUTTON_WIDTH = 120.0;

      // Half the screen width
      /// <summary>
      /// The minimum entry column width
      /// </summary>
      public static readonly double MINIMUM_ENTRY_COLUMN_WIDTH = 50.0.WidthFromPercentOfScreenWidth();

      /// <summary>
      /// The selected image button border width
      /// </summary>
      public static readonly double SELECTED_IMAGE_BUTTON_BORDER_WIDTH = 2;

      /// <summary>
      /// The standard keyboard
      /// </summary>
      public static readonly Keyboard STANDARD_KEYBOARD      = Keyboard.Create(STANDARD_KEYBOARD_NUMBER);
      /// <summary>
      /// The android multiplier
      /// </summary>
      public static          double   ANDROID_MULTIPLIER     = 1.0;
      /// <summary>
      /// The baseline screen height
      /// </summary>
      public static          double   BASELINE_SCREEN_HEIGHT = 812;

      // The baseline for fonts, widths and heights of buttons etc., is an iPhone X: 375 pixels.
      //    All other phones will scale to ths based on their screen width.
      /// <summary>
      /// The baseline screen width
      /// </summary>
      public static double BASELINE_SCREEN_WIDTH = 375;

      // The operating system is also a potential factor
      /// <summary>
      /// The ios multiplier
      /// </summary>
      public static double IOS_MULTIPLIER = 1.0;

      /// <summary>
      /// The short device factor
      /// </summary>
      public static double SHORT_DEVICE_FACTOR = 0.85;

      /// <summary>
      /// The current device length ratio
      /// </summary>
      private static readonly double CURRENT_DEVICE_LENGTH_RATIO =
         OrientationService.ScreenHeight / BASELINE_SCREEN_HEIGHT;

      /// <summary>
      /// The current device width ratio
      /// </summary>
      private static readonly double CURRENT_DEVICE_WIDTH_RATIO =
         OrientationService.ScreenWidth / BASELINE_SCREEN_WIDTH;

      /// <summary>
      /// The maximum os and device adjustment
      /// </summary>
      private static readonly double MAX_OS_AND_DEVICE_ADJUSTMENT =
         Math.Min(CURRENT_DEVICE_WIDTH_RATIO, CURRENT_DEVICE_LENGTH_RATIO);

      /// <summary>
      /// Gets a value indicating whether this instance is short device.
      /// </summary>
      /// <value><c>true</c> if this instance is short device; otherwise, <c>false</c>.</value>
      public static bool IsShortDevice =>
         CURRENT_DEVICE_LENGTH_RATIO / CURRENT_DEVICE_LENGTH_RATIO <= SHORT_DEVICE_FACTOR;

      /// <summary>
      /// MUST GO FIRST
      /// </summary>
      /// <param name="startingSize">Size of the starting.</param>
      /// <returns>System.Double.</returns>
      public static double AdjustForOsAndDevice(this double startingSize)
      {
         return startingSize * Math.Min(MAX_OS_AND_DEVICE_ADJUSTMENT,
                                        CURRENT_DEVICE_WIDTH_RATIO *
                                        (FormsUtils.IsIos() ? IOS_MULTIPLIER : ANDROID_MULTIPLIER));
      }

      /// <summary>
      /// Adjusts for os and device.
      /// </summary>
      /// <param name="namedSize">Size of the named.</param>
      /// <param name="additionalFactor">The additional factor.</param>
      /// <returns>System.Double.</returns>
      public static double AdjustForOsAndDevice(this NamedSize namedSize, double additionalFactor = 1.0)
      {
         return Device.GetNamedSize(namedSize, typeof(Label)).AdjustForOsAndDevice() * additionalFactor;
      }

      /// <summary>
      /// Adjusts the height for short device.
      /// </summary>
      /// <param name="startingValue">The starting value.</param>
      /// <returns>System.Double.</returns>
      public static double AdjustHeightForShortDevice(this double startingValue)
      {
         return IsShortDevice ? startingValue * SHORT_DEVICE_FACTOR : startingValue;
      }

      /*

         https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.keyboardflags?view=xamarin-forms

      All	-1
      Capitalize the first leter of the first words of sentences, perform spellcheck, and offer suggested word completions on text that the user enters.

         CapitalizeCharacter	16
      Indicates that every character will be automatically capitalized.

         CapitalizeNone	32
      Indicates that nothing will be automatically capitalized.

         CapitalizeSentence	1
      Indicates that the first letters of the first words of each sentence will be automatically capitalized.

         CapitalizeWord	8
      Indicates that the first letter of each word will be automatically capitalized.

         None	0
      Indicates that nothing will be automatically capitalized.

         Spellcheck	2
      Perform spellcheck on text that the user enters.

         Suggestions	4
      Offer suggested word completions on text that the user enters.
      */
   }
}

/*
      // These mirror the NamedSize enum:
      //public enum NamedSize
      //{
      //	//
      //	// Summary:
      //	//     The default font size.
      //	Default = 0,
      //	//
      //	// Summary:
      //	//     The smallest readable font size for the device. Think about this like legal footnotes.
      //	Micro = 1,
      //	//
      //	// Summary:
      //	//     A small but readable font size. Use this for block of text.
      //	Small = 2,
      //	//
      //	// Summary:
      //	//     A default font size, to be used in stand alone labels or buttons.
      //	Medium = 3,
      //	//
      //	// Summary:
      //	//     A Large font size, for titles or other important text elements.
      //	Large = 4
      public static readonly double[] BUTTON_WIDTHS =
      {
         // Default is "medium" for us -- ?
         72.0.AdjustSmallPhoneValueForActualDevice(),

         // Micro
         48.0.AdjustSmallPhoneValueForActualDevice(),

         // Small
         60.0.AdjustSmallPhoneValueForActualDevice(),

         // Medium
         72.0.AdjustSmallPhoneValueForActualDevice(),

         // Large
         90.0.AdjustSmallPhoneValueForActualDevice()
      };

      public const double BUTTON_HEIGHT_FROM_WIDTH_FACTOR = 0.5;

      public static readonly double[] BUTTON_HEIGHTS = BUTTON_WIDTHS.ToList().Select(w => w * BUTTON_HEIGHT_FROM_WIDTH_FACTOR).ToArray();

      public static readonly IDeviceManager _deviceManager = AppContainer.Container.Resolve<IDeviceManager>();

      private static bool IsALargePhone()
      {
         return _deviceManager.ScreenWidth > LARGE_PHONE_BOUNDARY;
      }

      /// <summary>Adjusts can the perspective of an iphone 5s upwards if we have a larger device or a tablet</summary>
      /// <param name="smallPhoneValue"></param>
      /// <remarks>
      /// http://stackoverflow.com/questions/25969533/how-to-handle-image-scale-on-all-the-available-iphone-resolutions
      /// </remarks>
      /// <returns></returns>
      internal static double AdjustSmallPhoneValueForActualDevice(this double smallPhoneValue)
      {
         /// WARNING: XAMARIN BUG when trying to return this as a series of "?" and ":" statements.

         if (Device.Idiom == TargetIdiom.Tablet)
         {
            return smallPhoneValue * RAW_VALUE_TO_TABLET_MULTIPLIER;
         }

         if (IsALargePhone())
         {
            return smallPhoneValue * LARGE_PHONE_FACTOR;
         }

         return smallPhoneValue;
      }

      /// <summary>
      /// This value is *supposed* to adjust for devices, but it only recognizes tablets vs. phones. It needs an
      /// adjustment from a small phone to a large phone.
      /// </summary>
      /// <param name="tabletOrLargePhoneValue"></param>
      /// <returns></returns>
      internal static double AdjustNamedSizeForDevice(this double tabletOrLargePhoneValue)
      {
         /// WARNING: XAMARIN BUG when trying to return this as a series of "?" and ":" statements.

         if (Device.Idiom == TargetIdiom.Tablet || IsALargePhone())
         {
            return tabletOrLargePhoneValue;
         }

         // Adjust downward for small phone if necessary
         return tabletOrLargePhoneValue / LARGE_PHONE_FACTOR;
      }
 */