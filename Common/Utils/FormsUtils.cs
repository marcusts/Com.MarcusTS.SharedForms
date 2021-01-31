// *********************************************************************** Assembly : Com.MarcusTS.SharedForms Author :
// steph Created : 07-29-2019
//
// Last Modified By : steph Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="FormsUtils.cs" company="Marcus Technical Services, Inc.">
//     Copyright @2019 Marcus Technical Services, Inc.
// </copyright>
// <summary></summary>

// MIT License

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
// Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

// #define AVOID_CONTEXT_MANAGEMENT

namespace Com.MarcusTS.SharedForms.Common.Utils
{
   using System;
   using System.Collections;
   using System.Collections.Concurrent;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Globalization;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using Annotations;
   using Behaviors;
   using Interfaces;
   using SharedUtils.Utils;
   using ViewModels;
   using Views.Controls;
   using Xamarin.Essentials;
   using Xamarin.Forms;
   using Xamarin.Forms.PancakeView;

   /// <summary>Class FormsUtils.</summary>
   public static class FormsUtils
   {
      /// <summary>
      ///    Enum OffScreenPositions
      /// </summary>
      public enum OffScreenPositions
      {
         /// <summary>
         ///    The none
         /// </summary>
         NONE,

         /// <summary>
         ///    The left
         /// </summary>
         LEFT,

         /// <summary>
         ///    The top
         /// </summary>
         TOP,

         /// <summary>
         ///    The right
         /// </summary>
         RIGHT,

         /// <summary>
         ///    The bottom
         /// </summary>
         BOTTOM
      }

      /// <summary>
      ///    Enum OnScreenPositions
      /// </summary>
      public enum OnScreenPositions
      {
         /// <summary>
         ///    The none
         /// </summary>
         NONE,

         /// <summary>
         ///    The left center
         /// </summary>
         LEFT_CENTER,

         /// <summary>
         ///    The top left
         /// </summary>
         TOP_LEFT, // Same as Left_Upper

         /// <summary>
         ///    The top center
         /// </summary>
         TOP_CENTER,

         /// <summary>
         ///    The top right
         /// </summary>
         TOP_RIGHT, // Same as Right_Upper

         /// <summary>
         ///    The right center
         /// </summary>
         RIGHT_CENTER,

         /// <summary>
         ///    The bottom left
         /// </summary>
         BOTTOM_LEFT, // Same as Left_Lower

         /// <summary>
         ///    The bottom center
         /// </summary>
         BOTTOM_CENTER,

         /// <summary>
         ///    The bottom right
         /// </summary>
         BOTTOM_RIGHT, // Same as Right_Lower

         /// <summary>
         ///    The center
         /// </summary>
         CENTER
      }

      /// <summary>
      ///    Enum StageHeaderPositions
      /// </summary>
      public enum StageHeaderPositions
      {
         /// <summary>
         ///    The none
         /// </summary>
         NONE,

         /// <summary>
         ///    The top
         /// </summary>
         TOP
      }

      /// <summary>
      ///    Enum StageToolbarPositions
      /// </summary>
      public enum StageToolbarPositions
      {
         /// <summary>
         ///    The none
         /// </summary>
         NONE,

         /// <summary>
         ///    The bottom
         /// </summary>
         BOTTOM,

         /// <summary>
         ///    The left
         /// </summary>
         LEFT,

         /// <summary>
         ///    The top
         /// </summary>
         TOP,

         /// <summary>
         ///    The right
         /// </summary>
         RIGHT
      }

      /// <summary>
      ///    Enum SubStageFlowDirections
      /// </summary>
      public enum SubStageFlowDirections
      {
         /// <summary>
         ///    The left to right
         /// </summary>
         LEFT_TO_RIGHT,

         /// <summary>
         ///    The right to left
         /// </summary>
         RIGHT_TO_LEFT,

         /// <summary>
         ///    The top to bottom
         /// </summary>
         TOP_TO_BOTTOM,

         /// <summary>
         ///    The bottom to top
         /// </summary>
         BOTTOM_TO_TOP
      }

      /// <summary>
      ///    Enum ViewAlignments
      /// </summary>
      public enum ViewAlignments
      {
         /// <summary>
         ///    The start
         /// </summary>
         START,

         /// <summary>
         ///    The middle
         /// </summary>
         MIDDLE,

         /// <summary>
         ///    The end
         /// </summary>
         END,

         /// <summary>
         ///    The justify
         /// </summary>
         JUSTIFY
      }

      public const string CREDIT_CARD_TEXT_MASK = "XXXX-XXXX-XXXX-XXXX";

      public const string DATE_TIME_FORMAT = "{0:M/d/yy - h:mm tt}";

      /// <summary>
      ///    The ios top margin
      /// </summary>
      public const float IOS_TOP_MARGIN = 40;

      public const string LONG_DATE_FORMAT = "{0:MMM d, yyyy}";

      public const double NO_SCALE = 0;

      public const double NORMAL_SCALE = 1;

      /// <summary>
      ///    The not visible opacity
      /// </summary>
      public const double NOT_VISIBLE_OPACITY = 0;

      public const string SERVER_DATE_FORMAT = "yyyy-MM-dd'T'HH:mm:ss'Z'";

      public const string SHORT_DATE_FORMAT = "{0:M/d/yy}";

      public const string SIMPLE_DATE_FORMAT = "{0:M/d/yyyy}";

      public const char SPACE_CHAR = ' ';

      /// <summary>
      ///    The visible opacity
      /// </summary>
      public const double VISIBLE_OPACITY = 1;

      // 0.85 works for denser copy
      private const double CHARACTERS_TO_FONT_SIZE_ESTIMATOR = 1;

      /// <summary>
      ///    The maximum expiration years
      /// </summary>
      private const int MAX_EXPIRATION_YEARS = 10;

      private static readonly string[] STATIC_MONTHS =
      {
         " ",
         "January",
         "February",
         "March",
         "April",
         "May",
         "June",
         "July",
         "August",
         "September",
         "October",
         "November",
         "December"
      };

      private static readonly string[] STATIC_STATES =
      {
         " ",
         "Alabama",
         "Alaska",
         "Arizona",
         "Arkansas",
         "California",
         "Colorado",
         "Connecticut",
         "Delaware",
         "Florida",
         "Georgia",
         "Hawaii",
         "Idaho",
         "Illinois",
         "Indiana",
         "Iowa",
         "Kansas",
         "Kentucky",
         "Louisiana",
         "Maine",
         "Maryland",
         "Massachusetts",
         "Michigan",
         "Minnesota",
         "Mississippi",
         "Missouri",
         "Montana",
         "Nebraska",
         "Nevada",
         "New Hampshire",
         "New Jersey",
         "New Mexico",
         "New York",
         "North Carolina",
         "North Dakota",
         "Ohio",
         "Oklahoma",
         "Oregon",
         "Pennsylvania",
         "Rhode Island",
         "South Carolina",
         "South Dakota",
         "Tennessee",
         "Texas",
         "Utah",
         "Vermont",
         "Virginia",
         "Washington",
         "West Virginia",
         "Wisconsin",
         "Wyoming"
      };

      private static int[] STATIC_EXPIRATION_YEARS
      {
         get
         {
            var yearsList = new List<int>();
            var thisYear = DateTime.Now.Year;

            for (var yearIdx = thisYear; yearIdx < thisYear + MAX_EXPIRATION_YEARS; yearIdx++)
            {
               yearsList.Add(yearIdx);
            }

            return yearsList.ToArray();
         }
      }

      public static void AddAndSetRowsAndColumns(this Grid grid, View view, int? row = default,
         int? column = default,
         int? rowSpan = default, int? colSpan = default)
      {
         grid.Children.Add(view);

         if (row.HasValue)
         {
            Grid.SetRow(view, row.GetValueOrDefault());
         }

         if (column.HasValue)
         {
            Grid.SetColumn(view, column.GetValueOrDefault());
         }

         if (rowSpan.HasValue)
         {
            Grid.SetRowSpan(view, rowSpan.GetValueOrDefault());
         }

         if (colSpan.HasValue)
         {
            Grid.SetColumnSpan(view, colSpan.GetValueOrDefault());
         }
      }

      /// <summary>Adds the animation and haptic feedback.</summary>
      /// <param name="view">The view.</param>
      /// <param name="animate">if set to <c>true</c> [animate].</param>
      /// <param name="vibrate">if set to <c>true</c> [vibrate].</param>
      /// <returns>Task.</returns>
      public static async Task AddAnimationAndHapticFeedback
      (
         this View view,
         bool animate = true,
         bool vibrate = true
      )
      {
         if (animate)
         {
            await view.ScaleTo(0.95, FormsConst.BUTTON_BOUNCE_MILLISECONDS, Easing.CubicOut).WithoutChangingContext();
            await view.ScaleTo(1, FormsConst.BUTTON_BOUNCE_MILLISECONDS, Easing.CubicIn).WithoutChangingContext();
         }

         if (vibrate)
         {
            // var v = CrossVibrate.Current;
            // v.Vibration(TimeSpan.FromMilliseconds(FormsConst.HAPTIC_VIBRATION_MILLISECONDS));
            Vibration.Vibrate(TimeSpan.FromMilliseconds(FormsConst.HAPTIC_VIBRATION_MILLISECONDS));
         }
      }

      /// <summary>Adds the automatic column.</summary>
      /// <param name="grid">The grid.</param>
      public static void AddAutoColumn(this Grid grid)
      {
         grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
      }

      /// <summary>Adds the automatic row.</summary>
      /// <param name="grid">The grid.</param>
      public static void AddAutoRow(this Grid grid)
      {
         grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
      }

      /// <summary>Adds the fixed column.</summary>
      /// <param name="grid">The grid.</param>
      /// <param name="width">The width.</param>
      public static void AddFixedColumn
      (
         this Grid grid,
         double width
      )
      {
         grid.ColumnDefinitions.Add(new ColumnDefinition { Width = width });
      }

      /// <summary>Adds the fixed row.</summary>
      /// <param name="grid">The grid.</param>
      /// <param name="height">The height.</param>
      public static void AddFixedRow
      (
         this Grid grid,
         double height
      )
      {
         grid.RowDefinitions.Add(new RowDefinition { Height = height });
      }

      public static void AddIfNotEmpty(this StringBuilder mainStrBuilder, string appendStr, string prefix = "")
      {
         if (appendStr.IsNotEmpty())
         {
            if (mainStrBuilder.ToString().IsNotEmpty())
            {
               mainStrBuilder.Append(prefix);
            }

            mainStrBuilder.Append(appendStr);
         }
      }

      public static void AddOverlayBasedOnPosition(this RelativeLayout layout,
         View view,
         OnScreenPositions position,
         double viewWidth, double viewHeight)
      {
         if (position == OnScreenPositions.NONE)
         {
            return;
         }

         switch (position)
         {
            case OnScreenPositions.CENTER:
               layout.Children.Add
               (
                  view,
                  GetHorizontallyCenteredConstraint(),
                  GetVerticallyCenteredConstraint()
               );
               break;

            case OnScreenPositions.BOTTOM_CENTER:
               layout.Children.Add
               (
                  view,
                  GetHorizontallyCenteredConstraint(),
                  GetBottomVerticalConstraint()
               );
               break;

            case OnScreenPositions.BOTTOM_LEFT:
               layout.Children.Add
               (
                  view,
                  GetLeftHorizontalConstraint(),
                  GetBottomVerticalConstraint()
               );
               break;

            case OnScreenPositions.BOTTOM_RIGHT:
               layout.Children.Add
               (
                  view,
                  GetRightHorizontalConstraint(),
                  GetBottomVerticalConstraint()
               );
               break;

            case OnScreenPositions.TOP_CENTER:
               layout.Children.Add
               (
                  view,
                  GetHorizontallyCenteredConstraint(),
                  GetTopVerticalConstraint()
               );
               break;

            case OnScreenPositions.TOP_LEFT:
               layout.Children.Add
               (
                  view,
                  GetLeftHorizontalConstraint(),
                  GetTopVerticalConstraint()
               );
               break;

            case OnScreenPositions.TOP_RIGHT:
               layout.Children.Add
               (
                  view,
                  GetRightHorizontalConstraint(),
                  GetTopVerticalConstraint()
               );
               break;

            case OnScreenPositions.LEFT_CENTER:
               layout.Children.Add
               (
                  view,
                  GetLeftHorizontalConstraint(),
                  GetVerticallyCenteredConstraint()
               );
               break;

            case OnScreenPositions.RIGHT_CENTER:
               layout.Children.Add
               (
                  view,
                  GetRightHorizontalConstraint(),
                  GetVerticallyCenteredConstraint()
               );
               break;
         }

         Constraint GetHorizontallyCenteredConstraint()
         {
            return Constraint.RelativeToParent(relativeLayout => (relativeLayout.Width - viewWidth) / 2);
         }

         Constraint GetVerticallyCenteredConstraint()
         {
            return Constraint.RelativeToParent(relativeLayout => (relativeLayout.Height - viewHeight) / 2);
         }

         Constraint GetBottomVerticalConstraint()
         {
            return Constraint.RelativeToParent(relativeLayout => relativeLayout.Height - viewHeight);
         }

         Constraint GetTopVerticalConstraint()
         {
            return Constraint.Constant(0);
         }

         Constraint GetLeftHorizontalConstraint()
         {
            return Constraint.Constant(0);
         }

         Constraint GetRightHorizontalConstraint()
         {
            return Constraint.RelativeToParent(relativeLayout => relativeLayout.Width - viewWidth);
         }
      }

      public static void AddRowContent(this Grid grid, View view)
      {
         // The count is zero-based, so before we add, the physical count is the same as the "next" count
         var nextRow = grid.Children.Count;
         grid.AddAutoRow();
         grid.Children.Add(view);
         Grid.SetRow(view, nextRow);
      }

      /// <summary>Adds the star column.</summary>
      /// <param name="grid">The grid.</param>
      /// <param name="factor">The factor.</param>
      public static void AddStarColumn
      (
         this Grid grid,
         double factor = 1
      )
      {
         grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(factor, GridUnitType.Star) });
      }

      /// <summary>Adds the star row.</summary>
      /// <param name="grid">The grid.</param>
      /// <param name="factor">The factor.</param>
      public static void AddStarRow
      (
         this Grid grid,
         double factor = 1
      )
      {
         grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(factor, GridUnitType.Star) });
      }

      /// <summary>
      ///    Adjusteds for screen height bug.
      /// </summary>
      /// <param name="originalHeight">Height of the original.</param>
      /// <returns>System.Single.</returns>
      public static float AdjustedForScreenHeightBug(this float originalHeight)
      {
         switch (Device.RuntimePlatform)
         {
            case Device.iOS:
               return originalHeight - 52;

            case Device.Android:
               // return originalHeight - 24;
               return originalHeight - 39;
         }

         return originalHeight;
      }

      public static void AnimateHeightChange(
         this View view,
         double nextHeight,
         uint fadeMilliseconds = 250,
         [CanBeNull] Easing easing = null)
      {
         // Nothing to do
         if (view.Height.IsSameAs(nextHeight))
         {
            return;
         }

         var fadeAnimation = new Animation(f => view.HeightRequest = f,
            view.Opacity,
            nextHeight, easing);
         fadeAnimation.Commit(view, "fadeAnimation", length: fadeMilliseconds, easing: easing);
      }

      public static void AnimateWidthChange(
         this View view,
         double nextWidth,
         uint fadeMilliseconds = 250,
         [CanBeNull] Easing easing = null)
      {
         // Nothing to do
         if (view.Width.IsSameAs(nextWidth))
         {
            return;
         }

         var fadeAnimation = new Animation(f => view.WidthRequest = f,
            view.Opacity,
            nextWidth, easing);
         fadeAnimation.Commit(view, "fadeAnimation", length: fadeMilliseconds, easing: easing);
      }

      /// <summary>Boundses the are valid and have changed.</summary>
      /// <param name="bounds">The bounds.</param>
      /// <param name="propName">Name of the property.</param>
      /// <param name="lastBounds">The last bounds.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool AreValidAndHaveChanged
      (
         this Rectangle bounds,
         string propName,
         Rectangle lastBounds
      )
      {
         return propName.IsBoundsRelatedPropertyChange() && bounds.IsValid() && bounds.IsDifferentThan(lastBounds);
      }

      /// <summary>
      ///    Assigns the internal date time properties.
      /// </summary>
      /// <param name="picker">The picker.</param>
      /// <param name="itemHeight">Height of the item.</param>
      /// <param name="fontSize">Size of the font.</param>
      /// <param name="nextTabIndex">Index of the next tab.</param>
      /// <returns>System.Int32.</returns>
      public static int AssignInternalDateTimeProperties(DatePicker picker, double itemHeight, double fontSize,
         int nextTabIndex)
      {
         picker.FontSize = fontSize;

         nextTabIndex = AssignInternalViewProperties(picker, itemHeight, nextTabIndex);

         return nextTabIndex;
      }

      public static int AssignInternalEntryProperties(Entry entry, double itemHeight, double fontSize, int nextTabIndex)
      {
         entry.FontSize = fontSize;

         nextTabIndex = AssignInternalViewProperties(entry, itemHeight, nextTabIndex);
         return nextTabIndex;
      }

      /// <summary>
      ///    Assigns the internal picker properties.
      /// </summary>
      /// <param name="picker">The picker.</param>
      /// <param name="itemHeight">Height of the item.</param>
      /// <param name="fontSize">Size of the font.</param>
      /// <param name="nextTabIndex">Index of the next tab.</param>
      /// <returns>System.Int32.</returns>
      public static int AssignInternalPickerProperties(Picker picker, double itemHeight, double fontSize,
         int nextTabIndex)
      {
         picker.FontSize = fontSize;
         nextTabIndex = AssignInternalViewProperties(picker, itemHeight, nextTabIndex);

         return nextTabIndex;
      }

      /// <summary>
      ///    Assigns the internal view properties.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="itemHeight">Height of the item.</param>
      /// <param name="nextTabIndex">Index of the next tab.</param>
      /// <returns>System.Int32.</returns>
      public static int AssignInternalViewProperties(View view, double itemHeight, int nextTabIndex)
      {
         view.HeightRequest = itemHeight;
         view.HorizontalOptions = LayoutOptions.FillAndExpand;

         if (view is IValidatableView viewAsValidatable)
         {
            nextTabIndex = viewAsValidatable.SetTabIndexes(nextTabIndex);
         }

         return nextTabIndex;
      }

      /// <summary>Checks the against zero.</summary>
      /// <param name="dbl">The double.</param>
      /// <returns>System.Double.</returns>
      public static double CheckAgainstZero(this double dbl)
      {
         return Math.Max(0, dbl);
      }

      /// <remarks>
      ///    This only returns entry validators for the AllText case.
      /// </remarks>
      public static IEntryValidationBehavior ChooseEntryValidator(Type attributeValidatorType,
         ValidationTypes validationTypes,
         Action handleInputValidationChanged)
      {
         if (attributeValidatorType == typeof(PasswordValidationBehavior))
         {
            return new PasswordValidationBehavior(handleInputValidationChanged);
         }

         if (attributeValidatorType == typeof(PhoneEntryValidatorBehavior))
         {
            return new PhoneEntryValidatorBehavior(handleInputValidationChanged);
         }

         if (attributeValidatorType == typeof(ComparisonEntryValidatorBehavior))
         {
            return new ComparisonEntryValidatorBehavior(handleInputValidationChanged);
         }

         if (attributeValidatorType == typeof(EmailEntryValidatorBehavior))
         {
            return new EmailEntryValidatorBehavior(handleInputValidationChanged);
         }

         if (attributeValidatorType == typeof(NumericEntryValidationBehavior))
         {
            return new NumericEntryValidationBehavior(handleInputValidationChanged);
         }

         if (attributeValidatorType == typeof(EntryValidationBehavior))
         {
            return new EntryValidationBehavior(handleInputValidationChanged);
         }

         // Else consider numerics
         if (validationTypes == ValidationTypes.DecimalNumber || validationTypes == ValidationTypes.WholeNumber)
         {
            return new NumericEntryValidationBehavior(handleInputValidationChanged);
         }

         // Finally, general entry
         return new EntryValidationBehavior(handleInputValidationChanged);
      }

      /// <summary>Clears the completely.</summary>
      /// <param name="grid">The grid.</param>
      public static void ClearCompletely(this Grid grid)
      {
         grid.Children.Clear();
         grid.ColumnDefinitions.Clear();
         grid.RowDefinitions.Clear();
      }

      /// <summary>
      ///    Compresses the date.
      /// </summary>
      /// <param name="dt">The dt.</param>
      /// <returns>System.String.</returns>
      public static string CompressedDate(this DateTime dt)
      {
         return $"{dt:M/d/yy}";
      }

      //public static Style CreateEntryStyle(
      //   [CanBeNull] string fontFamily = "", double? fontSize = null, Color? backColor = null,
      //   Color? textColor = null,
      //   FontAttributes? fontAttributes = null)
      //{
      //   var retStyle = new Style(typeof(Entry));

      //   if (fontFamily.IsNotEmpty())
      //   {
      //      retStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamily });
      //   }

      //   if (fontSize.HasValue)
      //   {
      //      retStyle.Setters.Add(new Setter { Property = Entry.FontSizeProperty, Value = fontSize });
      //   }

      //   if (backColor != null)
      //   {
      //      retStyle.Setters.Add(new Setter { Property = VisualElement.BackgroundColorProperty, Value = backColor });
      //   }

      //   if (textColor != null)
      //   {
      //      retStyle.Setters.Add(new Setter { Property = Entry.TextColorProperty, Value = textColor });
      //   }

      //   if (fontAttributes != null)
      //   {
      //      retStyle.Setters.Add(new Setter { Property = Entry.FontAttributesProperty, Value = fontAttributes });
      //   }

      //   return retStyle;
      //}

      public static Style CreateLabelStyle(
         [CanBeNull] string fontFamily = "", double? fontSize = null, Color? backColor = null,
         Color? textColor = null,
         FontAttributes? fontAttributes = null)
      {
         var retStyle = new Style(typeof(Label));

         if (fontFamily.IsNotEmpty())
         {
            retStyle.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = fontFamily });
         }

         if (fontSize.HasValue)
         {
            retStyle.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = fontSize });
         }

         if (backColor != null)
         {
            retStyle.Setters.Add(new Setter { Property = VisualElement.BackgroundColorProperty, Value = backColor });
         }

         if (textColor != null)
         {
            retStyle.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = textColor });
         }

         if (fontAttributes != null)
         {
            retStyle.Setters.Add(new Setter { Property = Label.FontAttributesProperty, Value = fontAttributes });
         }

         return retStyle;
      }

      /// <summary>
      ///    Creates the off screen rect.
      /// </summary>
      /// <param name="parentViewSize">Size of the parent view.</param>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      /// <param name="position">The position.</param>
      /// <param name="forceLongSize">if set to <c>true</c> [force long size].</param>
      /// <param name="additionalTopAllowance">The additional top allowance.</param>
      /// <returns>Rectangle.</returns>
      public static Rectangle CreateOffScreenRect
      (
         Size parentViewSize,
         double width,
         double height,
         OffScreenPositions position,
         bool forceLongSize = false,

         // Allows space for a header above a toolbar.
         double additionalTopAllowance = 0
      )
      {
         var adjustedHeight = forceLongSize ? 0 : parentViewSize.Height / 2 - height / 2;
         var adjustedWidth = forceLongSize ? 0 : parentViewSize.Width / 2 - width / 2;

         switch (position)
         {
            case OffScreenPositions.LEFT:
               height = GetForcedHeight(height, forceLongSize, parentViewSize);
               return new Rectangle(-width - 1, adjustedHeight + additionalTopAllowance, width,
                  height - additionalTopAllowance);

            case OffScreenPositions.TOP:
               width = GetForcedWidth(width, forceLongSize, parentViewSize);
               return new Rectangle(adjustedWidth, -height, width, height);

            case OffScreenPositions.RIGHT:
               height = GetForcedHeight(height, forceLongSize, parentViewSize);
               return new Rectangle(parentViewSize.Width + 1, adjustedHeight + additionalTopAllowance, width,
                  height - additionalTopAllowance);

            case OffScreenPositions.BOTTOM:
               width = GetForcedWidth(width, forceLongSize, parentViewSize);
               return new Rectangle(adjustedWidth, parentViewSize.Height + 1, width, height);

            default:
               return default;
         }
      }

      /// <summary>
      ///    Creates an on-screen rectangle for the current control based on its width, height and position.
      ///    If forceLongSize==true, either the width or height are set to the maximum screen width or height, depending on the
      ///    position.
      ///    forceLongSize *only* works with basic positions --
      ///    - LEFT_CENTER
      ///    - TOP_CENTER
      ///    - RIGHT_CENTER
      ///    - BOTTOM_CENTER
      /// </summary>
      /// <param name="parentViewSize">Size of the parent view.</param>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      /// <param name="position">The position.</param>
      /// <param name="parentViewPadding">The parent view padding.</param>
      /// <param name="forceLongSize">if set to <c>true</c> [force long size].</param>
      /// <param name="additionalTopAllowance">The additional top allowance.</param>
      /// <returns>Rectangle.</returns>
      public static Rectangle CreateOnScreenRect
      (
         Size parentViewSize,
         double width,
         double height,
         OnScreenPositions position,
         Thickness parentViewPadding = default,
         bool forceLongSize = false,

         // Allows space for a header
         double additionalTopAllowance = 0
      )
      {
         ErrorUtils.ConsiderArgumentError
         (
            forceLongSize &&
            position != OnScreenPositions.LEFT_CENTER &&
            position != OnScreenPositions.TOP_CENTER &&
            position != OnScreenPositions.RIGHT_CENTER &&
            position != OnScreenPositions.BOTTOM_CENTER,
            nameof(CreateOnScreenRect) + ": illegal position when requesting 'forceLongSize' ->" + position + "<-"
         );

         if (parentViewPadding.IsAnEqualObjectTo(default(Thickness)))
         {
            parentViewPadding = new Thickness(0);
         }

         // Initially adjust the width and height if we have a forced long side (as with a toolbar)
         //    This can only be done on one side (not both width and height)
         if (position == OnScreenPositions.TOP_CENTER || position == OnScreenPositions.BOTTOM_CENTER)
         {
            width = GetForcedWidth(width, forceLongSize, parentViewSize);
         }
         else if (position == OnScreenPositions.LEFT_CENTER || position == OnScreenPositions.RIGHT_CENTER)
         {
            height = GetForcedHeight(height, forceLongSize, parentViewSize);
         }

         // Then adjust according to the padding
         width = Math.Min(width, parentViewSize.Width - parentViewPadding.Left - parentViewPadding.Right);
         height = Math.Min(height,
            parentViewSize.Height - parentViewPadding.Top - parentViewPadding.Bottom - additionalTopAllowance);

         // Set up the adjusted X & Y positions
         var paddingAdjustedLeftXPos = parentViewPadding.Left;
         var paddingAdjustedRightXPos = parentViewSize.Width - width - parentViewPadding.Right;
         var paddingAdjustedTopYPos = parentViewPadding.Top + additionalTopAllowance;
         var paddingAdjustedBottomYPos = parentViewSize.Height - height - parentViewPadding.Bottom;

         // Ignores padding, as it splits the difference in order to center
         var horizontalCenterX = (parentViewSize.Width - width) / 2;
         var verticalCenterY = additionalTopAllowance + (parentViewSize.Height - height - additionalTopAllowance) / 2;

         double newLeftX;
         double newTopY;

         switch (position)
         {
            case OnScreenPositions.LEFT_CENTER:
               newLeftX = paddingAdjustedLeftXPos;
               newTopY = verticalCenterY;
               break;

            case OnScreenPositions.TOP_LEFT:
               newLeftX = paddingAdjustedLeftXPos;
               newTopY = paddingAdjustedTopYPos;
               break;

            case OnScreenPositions.TOP_CENTER:
               newLeftX = horizontalCenterX;
               newTopY = paddingAdjustedTopYPos;
               break;

            case OnScreenPositions.TOP_RIGHT:
               newLeftX = paddingAdjustedRightXPos;
               newTopY = paddingAdjustedTopYPos;
               break;

            case OnScreenPositions.RIGHT_CENTER:
               newLeftX = paddingAdjustedRightXPos;
               newTopY = verticalCenterY;
               break;

            case OnScreenPositions.BOTTOM_LEFT:
               newLeftX = paddingAdjustedLeftXPos;
               newTopY = paddingAdjustedBottomYPos;
               break;

            case OnScreenPositions.BOTTOM_CENTER:
               newLeftX = horizontalCenterX;
               newTopY = paddingAdjustedBottomYPos;
               break;

            case OnScreenPositions.BOTTOM_RIGHT:
               newLeftX = paddingAdjustedRightXPos;
               newTopY = paddingAdjustedBottomYPos;
               break;

            case OnScreenPositions.CENTER:
               newLeftX = horizontalCenterX;
               newTopY = verticalCenterY;
               break;

            default:
               return default;
         }

         var rect = new Rectangle(newLeftX, newTopY, width, height);
         return rect;
      }

      /// <summary>
      ///    Creates the on screen rect with forced side.
      /// </summary>
      /// <param name="parentViewSize">Size of the parent view.</param>
      /// <param name="widthHeight">Height of the width.</param>
      /// <param name="position">The position.</param>
      /// <param name="parentViewPadding">The parent view padding.</param>
      /// <param name="additionalTopAllowance">The additional top allowance.</param>
      /// <returns>Rectangle.</returns>
      public static Rectangle CreateOnScreenRectWithForcedSide
      (
         Size parentViewSize,
         double widthHeight,
         OnScreenPositions position,
         Thickness parentViewPadding = default,

         // Allows space for a header
         double additionalTopAllowance = 0
      )
      {
         var rect = CreateOnScreenRect(parentViewSize, widthHeight, widthHeight, position, parentViewPadding, true,
            additionalTopAllowance);
         return rect;
      }

      public static void CreateRelativeOverlay
      (
         this RelativeLayout layout,
         View viewToAdd,
         Thickness padding = default
      )
      {
         layout.Children.Add(
            viewToAdd, Constraint.Constant(padding.Left), Constraint.Constant(padding.Top),
            Constraint.RelativeToParent(parent => parent.Width - padding.Left - padding.Right),
            Constraint.RelativeToParent(parent => parent.Height - padding.Top - padding.Bottom));
      }


      public static Border CreateShapeViewBorder(
         Color?  borderColor     = null,
         double? borderThickness = null
      )
      {
         return new Border
         {
            Color     = borderColor.GetValueOrDefault(),
            Thickness = ((double)borderThickness.GetValueOrDefault()).ToRoundedInt()
         };
      }

      /// <summary>
      /// Creates the shape view style.
      /// </summary>
      /// <param name="backColor">Color of the back.</param>
      /// <param name="cornerRadius">The corner radius.</param>
      /// <param name="borderColor">Color of the border.</param>
      /// <param name="borderThickness">The border thickness.</param>
      /// <returns>Style.</returns>
      public static Style CreateShapeViewStyle(
         Color?  backColor       = null,
         double? cornerRadius    = null,
         Color?  borderColor     = null,
         double? borderThickness = null)
      {
         var retStyle = new Style(typeof(ShapeView));
         retStyle.SetShapeViewStyle(backColor, cornerRadius, borderColor, borderThickness);
         return retStyle;
      }
      
      /// <summary>
      ///    Creates the tasks.
      /// </summary>
      /// <param name="tasks">The tasks.</param>
      /// <returns>Task[].</returns>
      public static Task[] CreateTasks(params Task[] tasks)
      {
         var retTasks = new List<Task>();

         foreach (var action in tasks)
         {
            retTasks.Add(action);
         }

         return retTasks.ToArray();
      }

      public static (View, ICanBeValid, int) CreateValidatableEditorsForAttribute
      (
         IHaveValidationViewModelHelper viewModel,
         IViewModelValidationAttribute  attribute,
         double                         itemHeight,
         double                         itemWidth,
         double                         fontSize,
         int                            nextTabIndex
      )
      {
         ICanBeValid retValidator;

         if (viewModel.IsNullOrDefault())
         {
            return default;
         }

         var keyboard = GetKeyboardFromString(attribute.KeyboardName);

         var borderViewHeight = attribute.BorderViewHeight.IsNotEmpty()
            ? attribute.BorderViewHeight
            : itemHeight;
         var instructionsHeight = attribute.InstructionsHeight.IsNotEmpty()
            ? attribute.InstructionsHeight
            : ValidatableViewBase.INSTRUCTIONS_HEIGHT;
         var placeholderHeight = attribute.PlaceholderHeight.IsNotEmpty()
            ? attribute.PlaceholderHeight
            : ValidatableViewBase.PLACEHOLDER_HEIGHT;

         switch (attribute.InputType)
         {
            case InputTypes.StateInput:
               return SetUpValidatablePicker(STATIC_STATES);

            case InputTypes.MonthInput:
               return SetUpValidatablePicker(STATIC_MONTHS);

            case InputTypes.ExpirationYearInput:
               return SetUpValidatablePicker(STATIC_EXPIRATION_YEARS);

            case InputTypes.DateTimeInput:
               var dateTimePicker = new ValidatableDateTime
               (
                  viewModelPropertyName: attribute.ViewModelPropertyName,
                  bindingMode: attribute.BoundMode,
                  stringFormat: attribute.StringFormat,
                  showInstructionsOrValidations: attribute.ShowInstructionsOrValidations,
                  placeholder: attribute.PlaceholderText,
                  instructions: attribute.InstructionsText,
                  borderViewHeight: borderViewHeight,
                  instructionsHeight: instructionsHeight,
                  placeholderHeight: placeholderHeight,
                  showValidationErrorsAsInstructions: attribute.ShowValidationErrorsAsInstructions);

               nextTabIndex = AssignInternalDateTimeProperties(dateTimePicker.EditableDatePicker, itemHeight, fontSize,
                  nextTabIndex);
               retValidator = ValidatorFromView(dateTimePicker);
               dateTimePicker.WidthRequest = itemWidth;

               return (dateTimePicker, retValidator, nextTabIndex);

            case InputTypes.CheckBoxInput:
               var checkBoxPicker = new ValidatableCheckBox
               (
                  viewModelPropertyName: attribute.ViewModelPropertyName,
                  bindingMode: attribute.BoundMode,
                  stringFormat: attribute.StringFormat,
                  showInstructionsOrValidations: attribute.ShowInstructionsOrValidations,
                  placeholder: attribute.PlaceholderText,
                  instructions: attribute.InstructionsText,
                  borderViewHeight: borderViewHeight,
                  instructionsHeight: instructionsHeight,
                  placeholderHeight: placeholderHeight,
                  showValidationErrorsAsInstructions: attribute.ShowValidationErrorsAsInstructions);

               nextTabIndex = AssignInternalViewProperties(checkBoxPicker.EditableCheckBox, itemHeight, nextTabIndex);
               retValidator = ValidatorFromView(checkBoxPicker);

               checkBoxPicker.WidthRequest = itemWidth;

               return (checkBoxPicker, retValidator, nextTabIndex);

            case InputTypes.TextInput:
               var validator = 
                  ChooseEntryValidator(
                     attribute.ValidatorType,
                     attribute.ValidationType,
                     viewModel.ValidationHelper.RevalidateBehaviors);

               if (validator.IsNotNullOrDefault())
               {
                  validator.MinLength               = attribute.MinLength;
                  validator.MaxLength               = attribute.MaxLength;
                  validator.Mask                    = attribute.Mask;
                  validator.ValidationType          = attribute.ValidationType;
                  validator.DoNotForceMaskInitially = attribute.DoNotForceMaskInitially;
                  validator.ExcludedChars           = attribute.ExcludedChars;

                  if (validator is INumericEntryValidationBehavior validatorAsNumericBehavior)
                  {
                     validatorAsNumericBehavior.MinDecimalNumber = attribute.MinDecimalNumber;
                     validatorAsNumericBehavior.MaxDecimalNumber = attribute.MaxDecimalNumber;
                     validatorAsNumericBehavior.NumericType = attribute.NumericType;
                     validatorAsNumericBehavior.StringFormat = attribute.StringFormat;
                     validatorAsNumericBehavior.CharsToRightOfDecimal = attribute.CharsToRightOfDecimal;
                  }
               }

               if (attribute.NumericType == NumericTypes.NoNumericType)
               {
                  var entry = new ValidatableEntry(validator: validator,
                     entryFontSize: fontSize,
                     viewModelPropertyName: attribute.ViewModelPropertyName,
                     bindingMode: attribute.BoundMode,
                     stringFormat: attribute.StringFormat,
                     isPassword: attribute.IsPassword,
                     keyboard: keyboard,
                     showInstructionsOrValidations: attribute
                        .ShowInstructionsOrValidations,
                     placeholder: attribute.PlaceholderText,
                     instructions: attribute.InstructionsText,
                     borderViewHeight: borderViewHeight,
                     instructionsHeight: instructionsHeight,
                     placeholderHeight: placeholderHeight,
                     canUnmaskPassword: attribute.CanUnmaskPassword,
                     showValidationErrorsAsInstructions: attribute
                        .ShowValidationErrorsAsInstructions);
                  nextTabIndex = AssignInternalEntryProperties(entry.EditableEntry, itemHeight, fontSize, nextTabIndex);
                  retValidator = validator;

                  entry.WidthRequest = itemWidth;
                  
                  return (entry, retValidator, nextTabIndex);
               }
               else
               {
                  var entry = new ValidatableNumericEntry(validator: validator as INumericEntryValidationBehavior,
                     entryFontSize: fontSize,
                     viewModelPropertyName: attribute.ViewModelPropertyName,
                     bindingMode: attribute.BoundMode,
                     stringFormat: attribute.StringFormat,
                     keyboard: keyboard,
                     showInstructionsOrValidations: attribute
                        .ShowInstructionsOrValidations,
                     placeholder: attribute.PlaceholderText,
                     instructions: attribute.InstructionsText,
                     borderViewHeight: borderViewHeight,
                     instructionsHeight: instructionsHeight,
                     placeholderHeight: placeholderHeight,
                     showValidationErrorsAsInstructions: attribute
                        .ShowValidationErrorsAsInstructions);
                  nextTabIndex = AssignInternalEntryProperties(entry.EditableEntry, itemHeight, fontSize, nextTabIndex);
                  retValidator = validator;

                  entry.WidthRequest = itemWidth;
                  
                  return (entry, retValidator, nextTabIndex);
               }
         }

         return default;


         // -------------------------------------------------------------------------------------------------------
         // P R I V A T E    M E T H O D S
         // -------------------------------------------------------------------------------------------------------
         (View, ICanBeValid, int) SetUpValidatablePicker(IList items)
         {
            var picker = new ValidatablePicker
            (
               items,
               stringFormat: attribute.StringFormat,
               viewModelPropertyName: attribute.ViewModelPropertyName,
               bindingMode: attribute.BoundMode,
               showInstructionsOrValidations: attribute.ShowInstructionsOrValidations,
               placeholder: attribute.PlaceholderText,
               instructions: attribute.InstructionsText,
               borderViewHeight: borderViewHeight,
               instructionsHeight: instructionsHeight,
               placeholderHeight: placeholderHeight,
               showValidationErrorsAsInstructions: attribute.ShowValidationErrorsAsInstructions);

            picker.WidthRequest = itemWidth;
            
            nextTabIndex = AssignInternalPickerProperties(picker.EditablePicker, itemHeight, fontSize, nextTabIndex);
            retValidator = ValidatorFromView(picker);

            return (picker, retValidator, nextTabIndex);
         }
         // -------------------------------------------------------------------------------------------------------
      }

      public static IDictionary<PropertyInfo, IViewModelValidationAttribute> CreateViewModelValidationPropertiesDict(
         this Type type)
      {
         if (type.IsNullOrDefault())
         {
            return default;
         }

         var publicProperties = type.GetProperties();

         if (publicProperties.IsEmpty())
         {
            return default;
         }

         var retPropertiesDict = new ConcurrentDictionary<PropertyInfo, IViewModelValidationAttribute>();

         // Reduce the public properties down to those with the custom attribute
         // ReSharper disable once PossibleNullReferenceException
         foreach (var propInfo in publicProperties)
         {
            var customAttribute = propInfo.GetCustomAttribute<ViewModelValidationAttribute>();

            // Only add properties with a custom attribute
            if (customAttribute.IsNotNullOrDefault())
            {
               retPropertiesDict.AddOrUpdate(propInfo, customAttribute);
            }
         }

         return retPropertiesDict;
      }

      /// <summary>Enums the try parse.</summary>
      /// <typeparam name="EnumT">The type of the enum t.</typeparam>
      /// <param name="input">The input.</param>
      /// <param name="theEnum">The enum.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool EnumTryParse<EnumT>
      (
         string input,
         out EnumT theEnum
      )
      {
         foreach (var enumName in Enum.GetNames(typeof(EnumT)))
         {
            if (enumName.IsSameAs(input))
            {
               theEnum = (EnumT) Enum.Parse(typeof(EnumT), input, true);
               return true;
            }
         }

         theEnum = default;

         return false;
      }

      //   return retColor;
      //}
      /// <remarks>
      ///    This is rough and inductive.
      /// </remarks>
      public static double EstimateHeight(this Label label, double width)
      {
         if (label.IsNullOrDefault() || !width.IsGreaterThan(0))
         {
            return 0;
         }

         var length = label.Text.Count();
         var totalLength = length * label.FontSize * CHARACTERS_TO_FONT_SIZE_ESTIMATOR;
         var totalLines = totalLength / width;

         return totalLines * label.FontSize; // * (Math.Max(label.LineHeight, label.FontSize));
      }

      public static string Expanded(this string str, int spaceCount = 1)
      {
         if (str.IsEmpty())
         {
            return "";
         }

         var strBuilder = new StringBuilder();
         var lastCharIdx = str.Length - 1;

         for (var charIdx = 0; charIdx < str.Length; charIdx++)
         {
            var ch = str[charIdx];

            strBuilder.Append(ch);

            if (charIdx == lastCharIdx)
            {
               break;
            }

            // ELSE if not the last character
            for (var idx = 0; idx < spaceCount; idx++)
            {
               strBuilder.Append(SPACE_CHAR);
            }
         }

         // We end up with a trailing space
         return strBuilder.ToString().Trim();
      }

      public static
#if DEFEAT_FADES 
#elif ANIMATE_FADES
#else
         async
#endif
         void 
         FadeIn(
         this View view,
         uint fadeMilliseconds = 250,
         [CanBeNull] Easing easing = null,
         double maxOpacity = VISIBLE_OPACITY)
      {
         // Nothing to do
         if (view.Opacity.IsSameAs(maxOpacity))
         {
            return;
         }

#if DEFEAT_FADES
         view.Opacity = maxOpacity;
#elif ANIMATE_FADES
         var fadeAnimation = new Animation(f => view.Opacity = f,
                                                 view.Opacity,
                                                 maxOpacity, easing);
         fadeAnimation.Commit(view, "fadeAnimation", length:fadeMilliseconds, easing:easing);
#else
         await view.FadeTo(maxOpacity, fadeMilliseconds, easing ?? Easing.CubicIn).WithoutChangingContext();
#endif
      }

      /// <summary>
      ///    Fades the out.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="fadeMilliseconds"></param>
      /// <param name="easing"></param>
      /// <returns>Task.</returns>
      public static
#if DEFEAT_FADES
#elif ANIMATE_FADES
#else
         async
#endif
         void FadeOut(
         this View view,
         uint fadeMilliseconds = 250,
         [CanBeNull] Easing easing = null)
      {
         // Nothing to do
         if (view.Opacity.IsSameAs(NOT_VISIBLE_OPACITY))
         {
            return;
         }

#if DEFEAT_FADES
         view.Opacity = NOT_VISIBLE_OPACITY;
#elif ANIMATE_FADES
         var fadeAnimation = new Animation(f => view.Opacity = f,
                                           view.Opacity,
                                           NOT_VISIBLE_OPACITY, easing);
         fadeAnimation.Commit(view, "fadeAnimation", length:fadeMilliseconds, easing:easing);
#else
         await view.FadeTo(NOT_VISIBLE_OPACITY, fadeMilliseconds, easing).WithoutChangingContext();
#endif
      }

      public static Rectangle ForceAspect(this Rectangle rect, double aspect)
      {
         var currentAspect = rect.Width / rect.Height;
         var newWidth = rect.Width;
         var newHeight = rect.Height;

         if (currentAspect.IsSameAs(aspect))
         {
            return rect;
         }

         if (currentAspect < aspect)
         {
            // Too narrow; must shorten
            newHeight = rect.Width / aspect;
         }
         else
         {
            // ELSE currentAspect > aspect
            newWidth = rect.Height * aspect;
         }

         var heightDiff = Math.Max(0, rect.Height - newHeight);
         var widthDiff = Math.Max(0, rect.Width - newWidth);

         return new Rectangle(rect.X + widthDiff / 2, rect.Y + heightDiff / 2, newWidth, newHeight);
      }

      /// <summary>Forces the style.</summary>
      /// <param name="view">The view.</param>
      /// <param name="style">The style.</param>
      public static void ForceStyle
      (
         this View view,
         Style style
      )
      {
         if (style == null || style.Setters.IsEmpty())
         {
            return;
         }

#if DEBUG
         var viewProperties = view.GetType().GetProperties();
#endif

         for (var setterIdx = 0; setterIdx < style.Setters.Count; setterIdx++)
         {
#if DEBUG
            var viewProperty =
               viewProperties.FirstOrDefault(p => p.Name.IsSameAs(style.Setters[setterIdx].Property.PropertyName));

            if (viewProperty.IsNullOrDefault())
            {
               Debug.WriteLine(nameof(ForceStyle) +
                               ": could not find the property name ->" +
                               style.Setters[setterIdx].Property.PropertyName + "<- on view");
               continue;
            }

            var targetPropertyType = style.Setters[setterIdx].Property.ReturnType;

            // ReSharper disable once PossibleNullReferenceException
            if (viewProperty.PropertyType != targetPropertyType)
            {
               Debug.WriteLine(nameof(ForceStyle) +
                               ": view property ->" +
                               style.Setters[setterIdx].Property.PropertyName +
                               "<- shows as type ->" +
                               viewProperty.PropertyType +
                               "<- which does not match the setter property type ->" +
                               targetPropertyType + "<-");
               continue;
            }

#endif

            view.SetValue(style.Setters[setterIdx].Property, style.Setters[setterIdx].Value);
         }
      }

      /// <summary>Gets the expanding absolute layout.</summary>
      /// <returns>AbsoluteLayout.</returns>
      public static AbsoluteLayout GetExpandingAbsoluteLayout()
      {
         return new AbsoluteLayout
         {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.Transparent
         };
      }

      /// <summary>Gets the expanding grid.</summary>
      /// <returns>Grid.</returns>
      public static Grid GetExpandingGrid()
      {
         return new Grid
         {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.Transparent,
            ColumnSpacing = 0,
            RowSpacing = 0,
            Margin = 0,
            Padding = 0,
            IsClippedToBounds = true
         };
      }

      /// <summary>Gets the expanding relative layout.</summary>
      /// <returns>RelativeLayout.</returns>
      public static RelativeLayout GetExpandingRelativeLayout()
      {
         return new RelativeLayout
         {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.Transparent
         };
      }

      /// <summary>Gets the expanding scroll view.</summary>
      /// <returns>ScrollView.</returns>
      public static ScrollView GetExpandingScrollView()
      {
         return new ScrollView
         {
            VerticalOptions = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.Transparent,
            Orientation = ScrollOrientation.Vertical
         };
      }

      /// <summary>Gets the expanding stack layout.</summary>
      /// <returns>StackLayout.</returns>
      public static StackLayout GetExpandingStackLayout()
      {
         return new StackLayout
         {
            VerticalOptions = LayoutOptions.StartAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.Transparent,
            Orientation = StackOrientation.Vertical,
            Spacing = 0
         };
      }

      /// <summary>Gets the image.</summary>
      /// <param name="filePath">The file path.</param>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      /// <param name="aspect">The aspect.</param>
      /// <param name="getFromResources">if set to <c>true</c> [get from resources].</param>
      /// <param name="resourceClass">The resource class.</param>
      /// <returns>Image.</returns>
      public static Image GetImage
      (
         string filePath,
         double width = 0,
         double height = 0,
         Aspect aspect = Aspect.AspectFit,
         bool getFromResources = false,
         Type resourceClass = null
      )
      {
         var retImage =
            new Image
            {
               Aspect = aspect,
               VerticalOptions = LayoutOptions.Center,
               HorizontalOptions = LayoutOptions.Center
            };

         if (filePath.IsNotEmpty())
         {
            if (getFromResources && resourceClass != null)
            {
               retImage.Source = ImageSource.FromResource(filePath, resourceClass.GetTypeInfo().Assembly);
            }
            else
            {
               retImage.Source = ImageSource.FromFile(filePath);
            }
         }

         if (width.IsNotEmpty())
         {
            retImage.WidthRequest = width;
         }

         if (height.IsNotEmpty())
         {
            retImage.HeightRequest = height;
         }

         return retImage;
      }

      public static Keyboard GetKeyboardFromString(string attributeKeyboardName)
      {
         switch (attributeKeyboardName)
         {
            case nameof(Keyboard.Numeric):
               return Keyboard.Numeric;

            case nameof(Keyboard.Default):
               return Keyboard.Default;

            case nameof(Keyboard.Chat):
               return Keyboard.Chat;

            case nameof(Keyboard.Email):
               return Keyboard.Email;

            case nameof(Keyboard.Plain):
               return Keyboard.Plain;

            case nameof(Keyboard.Telephone):
               return Keyboard.Telephone;

            case nameof(Keyboard.Text):
               return Keyboard.Text;

            case nameof(Keyboard.Url):
               return Keyboard.Url;
         }

         return FormsConst.STANDARD_KEYBOARD;
      }

      /// <summary>
      ///    Gets the margin for runtime platform.
      /// </summary>
      /// <returns>Thickness.</returns>
      public static Thickness GetMarginForRuntimePlatform()
      {
         var top = GetStartingYForRuntimePlatform();
         return new Thickness(0, top, 0, 0);
      }

      /// <summary>Gets the shape view.</summary>
      /// <returns>ShapeView.</returns>
      public static ShapeView GetShapeView()
      {
         var retShapeView = new ShapeView();
         retShapeView.SetDefaults();
         return retShapeView;
      }

      /// <summary>Gets the simple label.</summary>
      /// <param name="labelText">The label text.</param>
      /// <param name="textColor">Color of the text.</param>
      /// <param name="textAlignment">The text alignment.</param>
      /// <param name="fontNamedSize">Size of the font named.</param>
      /// <param name="fontSize">Size of the font.</param>
      /// <param name="fontAttributes">The font attributes.</param>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      /// <param name="labelBindingPropertyName">Name of the label binding property.</param>
      /// <param name="labelBindingSource">The label binding source.</param>
      /// <param name="stringFormat"></param>
      /// <param name="breakMode">The break mode.</param>
      /// <param name="fontFamilyOverride"></param>
      /// <returns>Label.</returns>
      public static Label GetSimpleLabel
      (
         string labelText = default,
         Color textColor = default,
         TextAlignment textAlignment = TextAlignment.Center,
         NamedSize fontNamedSize = NamedSize.Medium,
         double fontSize = 0.0,
         FontAttributes fontAttributes = FontAttributes.None,
         double width = 0,
         double height = 0,
         string labelBindingPropertyName = default,
         object labelBindingSource = null,
         string stringFormat = "",
         LineBreakMode breakMode = LineBreakMode.WordWrap,
         string fontFamilyOverride = ""
      )
      {
         if (textColor.IsUnsetOrDefault())
         {
            textColor = Color.Black;
         }

         var retLabel =
            new Label
            {
               Text = labelText,
               TextColor = textColor,
               HorizontalTextAlignment = textAlignment,
               VerticalTextAlignment = TextAlignment.Center,
               HorizontalOptions = HorizontalOptionsFromTextAlignment(textAlignment),
               VerticalOptions = LayoutOptions.CenterAndExpand,
               BackgroundColor = Color.Transparent,

               FontAttributes = fontAttributes,
               FontSize =
                  fontSize.IsNotEmpty() ? fontSize : Device.GetNamedSize(fontNamedSize, typeof(Label)),
               LineBreakMode = breakMode
            };

         if (fontFamilyOverride.IsNotEmpty())
         {
            retLabel.FontFamily = fontFamilyOverride;
         }

         // Set up the label text binding (if provided)
         if (labelBindingPropertyName.IsNotEmpty())
         {
            if (stringFormat.IsNotEmpty())
            {
               if (labelBindingSource != null)
               {
                  retLabel.SetUpBinding(Label.TextProperty, labelBindingPropertyName, source: labelBindingSource,
                     stringFormat: stringFormat);
               }
               else
               {
                  retLabel.SetUpBinding(Label.TextProperty, labelBindingPropertyName, stringFormat: stringFormat);
               }
            }
            else
            {
               if (labelBindingSource != null)
               {
                  retLabel.SetUpBinding(Label.TextProperty, labelBindingPropertyName, source: labelBindingSource);
               }
               else
               {
                  retLabel.SetUpBinding(Label.TextProperty, labelBindingPropertyName);
               }
            }
         }

         if (width.IsNotEmpty())
         {
            retLabel.WidthRequest = width;
         }

         if (height.IsNotEmpty())
         {
            retLabel.HeightRequest = height;
         }

         return retLabel;
      }

      // ------------------------------------------------------------------------------------------
      /// <summary>Gets the spacer.</summary>
      /// <param name="height">The height.</param>
      /// <returns>BoxView.</returns>
      public static BoxView GetSpacer(double height)
      {
         return new BoxView
         {
            HeightRequest = height,
            BackgroundColor = Color.Transparent,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand
         };
      }

      /// <summary>
      ///    Gets the starting y for runtime platform.
      /// </summary>
      /// <returns>System.Single.</returns>
      public static double GetStartingYForRuntimePlatform()
      {
         return IsIos() ? IOS_TOP_MARGIN : 0;
      }

      /// <summary>
      ///    The bounds have become *invalid* (not changed necessarily) in relation to the last known bounds.
      /// </summary>
      /// <param name="bounds">The bounds.</param>
      /// <param name="propName">Name of the property.</param>
      /// <param name="lastBounds">The last bounds.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool HaveBecomeInvalid
      (
         this Rectangle bounds,
         string propName,
         Rectangle lastBounds
      )
      {
         return propName.IsBoundsRelatedPropertyChange() && bounds.IsNotValid() && lastBounds.IsValid();
      }

      /// <summary>The bounds have become *invalid* OR *changed* in relation to the last known bounds.</summary>
      /// <param name="bounds">The bounds.</param>
      /// <param name="propName">Name of the property.</param>
      /// <param name="lastBounds">The last bounds.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool HaveBecomeInvalidOrChangedWithoutPermission
      (
         this Rectangle bounds,
         string propName,
         Rectangle lastBounds
      )
      {
         return propName.IsBoundsRelatedPropertyChange() && lastBounds.IsValid() &&
                (bounds.IsNotValid() || bounds.IsDifferentThan(lastBounds));
      }

      /// <summary>Horizontals the options from text alignment.</summary>
      /// <param name="textAlignment">The text alignment.</param>
      /// <returns>LayoutOptions.</returns>
      public static LayoutOptions HorizontalOptionsFromTextAlignment(TextAlignment textAlignment)
      {
         switch (textAlignment)
         {
            case TextAlignment.Center:
               return LayoutOptions.Center;

            case TextAlignment.End:
               return LayoutOptions.End;

            // Covers Start and default
            default:
               return LayoutOptions.Start;
         }
      }

      /// <summary>Inserts the automatic column.</summary>
      /// <param name="grid">The grid.</param>
      /// <param name="insertionLoc">The insertion loc.</param>
      public static void InsertAutoColumn(this Grid grid, int insertionLoc)
      {
         grid.ColumnDefinitions.Insert(insertionLoc, new ColumnDefinition { Width = GridLength.Auto });
      }

      /// <summary>Determines whether [is bounds related property change] [the specified property name].</summary>
      /// <param name="propName">Name of the property.</param>
      /// <returns>
      ///    <c>true</c> if [is bounds related property change] [the specified property name]; otherwise, <c>false</c>.
      /// </returns>
      public static bool IsBoundsRelatedPropertyChange
      (
         this string propName
      )
      {
         return propName.IsSameAs(FormsConst.WIDTH_PROPERTY_NAME) ||
                propName.IsSameAs(FormsConst.HEIGHT_PROPERTY_NAME) || propName.IsSameAs(FormsConst.X_PROPERTY_NAME) ||
                propName.IsSameAs(FormsConst.Y_PROPERTY_NAME);
      }

      /// <summary>Determines whether [is different than] [the specified second color].</summary>
      /// <param name="color">The color.</param>
      /// <param name="secondColor">Color of the second.</param>
      /// <returns><c>true</c> if [is different than] [the specified second color]; otherwise, <c>false</c>.</returns>
      public static bool IsDifferentThan
      (
         this Color color,
         Color secondColor
      )
      {
         return color.IsNotAnEqualObjectTo(secondColor);
      }

      /// <summary>Determines whether [is different than] [the specified other size].</summary>
      /// <param name="size">The size.</param>
      /// <param name="otherSize">Size of the other.</param>
      /// <returns><c>true</c> if [is different than] [the specified other size]; otherwise, <c>false</c>.</returns>
      public static bool IsDifferentThan(this Size size, Size otherSize)
      {
         return !size.IsSameAs(otherSize);
      }

      /// <summary>Determines whether [is different than] [the specified other thickness].</summary>
      /// <param name="Thickness">The thickness.</param>
      /// <param name="otherThickness">The other thickness.</param>
      /// <returns><c>true</c> if [is different than] [the specified other thickness]; otherwise, <c>false</c>.</returns>
      public static bool IsDifferentThan(this Thickness Thickness, Thickness otherThickness)
      {
         return !Thickness.IsSameAs(otherThickness);
      }

      /// <summary>Determines whether [is different than] [the specified other rect].</summary>
      /// <param name="mainRect">The main rect.</param>
      /// <param name="otherRect">The other rect.</param>
      /// <returns><c>true</c> if [is different than] [the specified other rect]; otherwise, <c>false</c>.</returns>
      public static bool IsDifferentThan
      (
         this Rectangle mainRect,
         Rectangle otherRect
      )
      {
         return !mainRect.IsSameAs(otherRect);
      }

      /// <summary>Determines whether the specified size is empty.</summary>
      /// <param name="size">The size.</param>
      /// <returns><c>true</c> if the specified size is empty; otherwise, <c>false</c>.</returns>
      public static bool IsEmpty(this Size size)
      {
         return size.Width.IsLessThanOrEqualTo(0) && size.Height.IsLessThanOrEqualTo(0);
      }

      /// <summary>Determines whether the specified thickness is empty.</summary>
      /// <param name="Thickness">The thickness.</param>
      /// <returns><c>true</c> if the specified thickness is empty; otherwise, <c>false</c>.</returns>
      public static bool IsEmpty(this Thickness Thickness)
      {
         return Thickness.Bottom.IsLessThanOrEqualTo(0) && Thickness.Left.IsLessThanOrEqualTo(0) &&
                Thickness.Right.IsLessThanOrEqualTo(0) && Thickness.Top.IsLessThanOrEqualTo(0);
      }

      /// <summary>Determines whether the specified main rect is empty.</summary>
      /// <param name="mainRect">The main rect.</param>
      /// <returns><c>true</c> if the specified main rect is empty; otherwise, <c>false</c>.</returns>
      public static bool IsEmpty
      (
         this Rectangle mainRect
      )
      {
         return mainRect.X.IsLessThanOrEqualTo(0) &&
                mainRect.Y.IsLessThanOrEqualTo(0) &&
                mainRect.Width.IsLessThanOrEqualTo(0) &&
                mainRect.Height.IsLessThanOrEqualTo(0);
      }

      /// <summary>
      ///    Determines whether this instance is ios.
      /// </summary>
      /// <returns><c>true</c> if this instance is ios; otherwise, <c>false</c>.</returns>
      public static bool IsIos()
      {
         return Device.RuntimePlatform.IsSameAs(Device.iOS);
      }

      /// <summary>
      ///    Determines whether this instance is ios.
      /// </summary>
      /// <returns><c>true</c> if this instance is ios; otherwise, <c>false</c>.</returns>
      public static bool IsIOS()
      {
         return Device.RuntimePlatform.IsSameAs(Device.iOS);
      }

      public static bool IsLessThan(this string mainStr, string compareStr)
      {
         return string.CompareOrdinal(mainStr, compareStr) < 0;
      }

      /// <summary>Determines whether [is not empty] [the specified size].</summary>
      /// <param name="size">The size.</param>
      /// <returns><c>true</c> if [is not empty] [the specified size]; otherwise, <c>false</c>.</returns>
      public static bool IsNotEmpty(this Size size)
      {
         return !size.IsEmpty();
      }

      /// <summary>Determines whether [is not empty] [the specified thickness].</summary>
      /// <param name="Thickness">The thickness.</param>
      /// <returns><c>true</c> if [is not empty] [the specified thickness]; otherwise, <c>false</c>.</returns>
      public static bool IsNotEmpty(this Thickness Thickness)
      {
         return !Thickness.IsEmpty();
      }

      /// <summary>Determines whether [is not empty] [the specified main rect].</summary>
      /// <param name="mainRect">The main rect.</param>
      /// <returns><c>true</c> if [is not empty] [the specified main rect]; otherwise, <c>false</c>.</returns>
      public static bool IsNotEmpty
      (
         this Rectangle mainRect
      )
      {
         return !mainRect.IsEmpty();
      }

      /// <summary>Determines whether [is not valid] [the specified bounds].</summary>
      /// <param name="bounds">The bounds.</param>
      /// <returns><c>true</c> if [is not valid] [the specified bounds]; otherwise, <c>false</c>.</returns>
      public static bool IsNotValid(this Rectangle bounds)
      {
         return !bounds.IsValid();
      }

      //public static double IosHeightHack(bool isNested)
      //{
      //   return Device.RuntimePlatform.IsSameAs(Device.iOS) ? isNested ? 0 : IOS_TOP_MARGIN : 0;
      //}
      /// <summary>
      ///    Determines whether [is not visible] [the specified view].
      /// </summary>
      /// <param name="view">The view.</param>
      /// <returns><c>true</c> if [is not visible] [the specified view]; otherwise, <c>false</c>.</returns>
      public static bool IsNotVisible(this View view)
      {
         return view.Opacity.IsDifferentThan(VISIBLE_OPACITY);
      }

      /// <summary>Determines whether [is same as] [the specified other size].</summary>
      /// <param name="size">The size.</param>
      /// <param name="otherSize">Size of the other.</param>
      /// <returns><c>true</c> if [is same as] [the specified other size]; otherwise, <c>false</c>.</returns>
      public static bool IsSameAs(this Size size, Size otherSize)
      {
         return size.Width.IsSameAs(otherSize.Width) && size.Height.IsSameAs(otherSize.Height);
      }

      /// <summary>Determines whether [is same as] [the specified other thickness].</summary>
      /// <param name="Thickness">The thickness.</param>
      /// <param name="otherThickness">The other thickness.</param>
      /// <returns><c>true</c> if [is same as] [the specified other thickness]; otherwise, <c>false</c>.</returns>
      public static bool IsSameAs(this Thickness Thickness, Thickness otherThickness)
      {
         return Thickness.Bottom.IsSameAs(otherThickness.Bottom) && Thickness.Left.IsSameAs(otherThickness.Left) &&
                Thickness.Right.IsSameAs(otherThickness.Right) && Thickness.Top.IsSameAs(otherThickness.Top);
      }

      /// <summary>Determines whether [is same as] [the specified other rect].</summary>
      /// <param name="mainRect">The main rect.</param>
      /// <param name="otherRect">The other rect.</param>
      /// <returns><c>true</c> if [is same as] [the specified other rect]; otherwise, <c>false</c>.</returns>
      public static bool IsSameAs
      (
         this Rectangle mainRect,
         Rectangle otherRect
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

      /// <summary>Determines whether [is unset or default] [the specified color].</summary>
      /// <param name="color">The color.</param>
      /// <returns><c>true</c> if [is unset or default] [the specified color]; otherwise, <c>false</c>.</returns>
      public static bool IsUnsetOrDefault(this Color color)
      {
         return color.IsAnEqualObjectTo(default);
      }

      /// <summary>Returns true if ... is valid.</summary>
      /// <param name="bounds">The bounds.</param>
      /// <returns><c>true</c> if the specified bounds is valid; otherwise, <c>false</c>.</returns>
      public static bool IsValid(this Rectangle bounds)
      {
         return bounds.Width > 0 && bounds.Height > 0;
      }

      public static bool IsValid(this Size size)
      {
         return size.Width > 0 && size.Height > 0;
      }

      /// <summary>Returns true if ... is valid.</summary>
      /// <param name="color">The color.</param>
      /// <returns><c>true</c> if the specified color is valid; otherwise, <c>false</c>.</returns>
      public static bool IsValid(this Color color)
      {
         return !color.IsUnsetOrDefault();
      }

      //public static double LabelNamedFontSizeForDevice(NamedSize tabletSize, NamedSize phoneSize)
      //{
      //   if (Device.Idiom == TargetIdiom.Phone)
      //   {
      //      return Device.GetNamedSize(phoneSize, typeof(Label));
      //   }

      //   // ELSE
      //   return Device.GetNamedSize(tabletSize, typeof(Label));
      //}

      /// <summary>Merges the style.</summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="mainStyle">The main style.</param>
      /// <param name="newStyle">The new style.</param>
      /// <returns>Style.</returns>
      public static Style MergeStyle<T>
      (
         this Style mainStyle,
         Style newStyle
      )
      {
         if (newStyle == null || newStyle.Setters.IsEmpty())
         {
            return mainStyle;
         }

         if (mainStyle == null)
         {
            mainStyle = new Style(typeof(T));
         }

         foreach (var setter in newStyle.Setters)
         {
            var foundSetter =
               mainStyle.Setters.FirstOrDefault(s => s.Property.PropertyName.IsSameAs(setter.Property.PropertyName));
            if (foundSetter != null)
            {
               var foundSetterIdx = mainStyle.Setters.IndexOf(foundSetter);

               mainStyle.Setters[foundSetterIdx] = setter;
            }
            else
            {
               mainStyle.Setters.Add(setter);
            }
         }

         // The Style settings must also be considered in the assignment
         mainStyle.ApplyToDerivedTypes = newStyle.ApplyToDerivedTypes || mainStyle.ApplyToDerivedTypes;

         //if (newStyle.BasedOn != default(Style))
         //{
         //   mainStyle.BasedOn = newStyle.BasedOn;
         //}

         // TODO
         //mainStyle.BaseResourceKey = newStyle.BaseResourceKey;
         //mainStyle.CanCascade = newStyle.CanCascade;
         //mainStyle.Class = newStyle.Class;
         //mainStyle.Behaviors = newStyle.Behaviors;
         //mainStyle.Triggers = newStyle.Triggers;
         //mainStyle.TargetType = newStyle.TargetType;

         return mainStyle;
      }

      public static string NowToServerDateTimeStr()
      {
         return DateTime.Now.ToServerDateTimeStr();
      }

      /// <summary>
      ///    Presentables the date.
      /// </summary>
      /// <param name="dt">The dt.</param>
      /// <returns>System.String.</returns>
      public static string PresentableDate(this DateTime dt)
      {
         return $"{dt:MMM d, yyyy}";
      }

      /// <summary>
      ///    Presentables the dollar amount.
      /// </summary>
      /// <param name="dollars">The dollars.</param>
      /// <returns>System.String.</returns>
      public static string PresentableDollarAmount(this double dollars)
      {
         return $"{dollars:c}";
      }

      /// <summary>
      ///    Presentables the whole long int.
      /// </summary>
      /// <param name="lng">The LNG.</param>
      /// <returns>System.String.</returns>
      public static string PresentableWholeLongInt(this long lng)
      {
         return $"{lng:0}";
      }

      //   var retColor =
      //      Color.FromRgb
      //         (
      //            color.R.MultiplyWithMax(multiplier, MAX_COLOR_ELEMENT),
      //            color.G.MultiplyWithMax(multiplier, MAX_COLOR_ELEMENT),
      //            color.B.MultiplyWithMax(multiplier, MAX_COLOR_ELEMENT)
      //          );
      public static void SetAndForceStyle(this View view, Style style)
      {
         view.Style = style;
         view.ForceStyle(style);
      }

      public static void SetDefaults(this ShapeView shapeView)
      {
         shapeView.Color = Color.White;
         shapeView.CornerRadius = FormsConst.DEFAULT_SHAPE_VIEW_RADIUS;
         shapeView.BackgroundColor = Color.Transparent;
         shapeView.IsClippedToBounds = true;
         shapeView.HorizontalOptions = LayoutOptions.FillAndExpand;
         shapeView.VerticalOptions = LayoutOptions.FillAndExpand;
      }

      public static void SetDefaults(this ScrollView scrollView)
      {
         scrollView.SetViewDefaults();
         scrollView.Orientation = ScrollOrientation.Vertical;
      }

      public static void SetViewDefaults(this View view)
      {
         view.VerticalOptions = LayoutOptions.FillAndExpand;
         view.HorizontalOptions = LayoutOptions.FillAndExpand;
         view.BackgroundColor = Color.Transparent;
      }

      //   // Determine the maximum multiplication that allows us to keep the exact proportion of the three colors.
      //   var maxCurrentIndividualColor = Math.Max(Math.Max(color.R, color.G), color.B);
      //   var multiplier = MAX_COLOR_ELEMENT * howCloseToWhite / maxCurrentIndividualColor;
      /// <summary>Converts to enum.</summary>
      /// <typeparam name="EnumT">The type of the enum t.</typeparam>
      /// <param name="enumAsString">The enum as string.</param>
      /// <returns>EnumT.</returns>
      public static EnumT ToEnum<EnumT>(this string enumAsString)
         where EnumT : Enum
      {
         if (EnumTryParse(enumAsString, out EnumT foundEnum))
         {
            return foundEnum;
         }

         return default;
      }

      //public static Color SuperPale(this Color color, double howCloseToWhite = VERY_PALE)
      //{
      //   if (color.IsAnEqualObjectTo(default))
      //   {
      //      return Color.Transparent;
      //   }
      public static DateTime? ToNullableDateTime(this string str)
      {
         if (str.IsEmpty())
         {
            return default;
         }

         if (DateTime.TryParse(str, out var dateTime))
         {
            return dateTime;
         }

         return default;
      }

      //private static double MultiplyWithMax(this double num, double factor, double max)
      //{
      //   return Math.Min(max, (num * factor));
      //}
      /// <summary>
      ///    Converts to off screen position.
      /// </summary>
      /// <param name="stageToolbarPosition">The stage toolbar position.</param>
      /// <returns>OffScreenPositions.</returns>
      public static OffScreenPositions ToOffScreenPosition(this StageToolbarPositions stageToolbarPosition)
      {
         switch (stageToolbarPosition)
         {
            case StageToolbarPositions.BOTTOM: return OffScreenPositions.BOTTOM;

            case StageToolbarPositions.LEFT: return OffScreenPositions.LEFT;

            case StageToolbarPositions.TOP: return OffScreenPositions.TOP;

            case StageToolbarPositions.RIGHT: return OffScreenPositions.RIGHT;
         }

         return OffScreenPositions.NONE;
      }

      //private const double VERY_PALE = 0.9;
      //private const double MAX_COLOR_ELEMENT = 255;
      /// <summary>
      /// </summary>
      /// <param name="flowDirection"></param>
      /// <returns></returns>
      public static OffScreenPositions ToOffScreenPosition(this SubStageFlowDirections flowDirection)
      {
         switch (flowDirection)
         {
            case SubStageFlowDirections.BOTTOM_TO_TOP: return OffScreenPositions.BOTTOM;
            case SubStageFlowDirections.LEFT_TO_RIGHT: return OffScreenPositions.LEFT;
            case SubStageFlowDirections.TOP_TO_BOTTOM: return OffScreenPositions.TOP;
            case SubStageFlowDirections.RIGHT_TO_LEFT: return OffScreenPositions.RIGHT;
         }

         return OffScreenPositions.NONE;
      }

      /// <summary>
      ///    Converts to on-screen position.
      /// </summary>
      /// <param name="stageToolbarPosition">The stage toolbar position.</param>
      /// <returns>OnScreenPositions.</returns>
      public static OnScreenPositions ToOnScreenPosition(this StageToolbarPositions stageToolbarPosition)
      {
         switch (stageToolbarPosition)
         {
            case StageToolbarPositions.BOTTOM: return OnScreenPositions.BOTTOM_CENTER;

            case StageToolbarPositions.LEFT: return OnScreenPositions.LEFT_CENTER;

            case StageToolbarPositions.TOP: return OnScreenPositions.TOP_CENTER;

            case StageToolbarPositions.RIGHT: return OnScreenPositions.RIGHT_CENTER;
         }

         return OnScreenPositions.NONE;
      }

      /// <summary>
      ///    Converts to options.
      /// </summary>
      /// <param name="alignment">The alignment.</param>
      /// <returns>LayoutOptions.</returns>
      public static LayoutOptions ToOptions(this LayoutAlignment alignment)
      {
         // Set the child margins to compel this alignment
         switch (alignment)
         {
            case LayoutAlignment.Center:
               return LayoutOptions.Center;

            case LayoutAlignment.End:
               return LayoutOptions.End;

            case LayoutAlignment.Fill:
               return LayoutOptions.Fill;

            case LayoutAlignment.Start:
               return LayoutOptions.Start;
         }

         return default;
      }

      public static string ToServerDateTimeStr(this DateTime dateTime)
      {
         var retStr = dateTime.ToString(SERVER_DATE_FORMAT);

         return retStr;
      }

      /// <summary>
      ///    Use the current thread's culture info for conversion
      /// </summary>
      public static string ToTitleCase(this string str)
      {
         if (str.IsEmpty())
         {
            return "";
         }

         var cultureInfo = Thread.CurrentThread.CurrentCulture;
         return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
      }

      /// <summary>
      ///    Overload which uses the culture info with the specified name
      /// </summary>
      public static string ToTitleCase(this string str, string cultureInfoName)
      {
         if (str.IsEmpty())
         {
            return "";
         }

         var cultureInfo = new CultureInfo(cultureInfoName);
         return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
      }

      /// <summary>
      ///    Overload which uses the specified culture info
      /// </summary>
      public static string ToTitleCase(this string str, CultureInfo cultureInfo)
      {
         if (str.IsEmpty())
         {
            return "";
         }

         return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
      }

      //   }
      //}
      public static ICanBeValid ValidatorFromView(View view)
      {
         return view.Behaviors?.FirstOrDefault(b => b is ICanBeValid) as ICanBeValid;
      }

      /// <summary>Runs a Task without changing the context (configure await is false).</summary>
      /// <param name="task">The task.</param>
      /// <returns>Task.</returns>
      public static async Task WithoutChangingContext(this Task task) =>
#if AVOID_CONTEXT_MANAGEMENT
         await task;
#else
         await task.ConfigureAwait(false);

#endif

      /// <summary>Withouts the changing context.</summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="task">The task.</param>
      /// <returns>Task&lt;T&gt;.</returns>
      public static async Task<T> WithoutChangingContext<T>(this Task<T> task)
      {
         return await task.ConfigureAwait(false);
      }

      /// <summary>
      ///    Gets the height of the forced.
      /// </summary>
      /// <param name="height">The height.</param>
      /// <param name="forceLongSize">if set to <c>true</c> [force long size].</param>
      /// <param name="currentSize">Size of the current.</param>
      /// <returns>System.Double.</returns>
      private static double GetForcedHeight
      (
         double height,
         bool forceLongSize,
         Size currentSize
      )
      {
         height = forceLongSize ? currentSize.Height : height;
         return height;
      }

      /// <summary>
      ///    Gets the width of the forced.
      /// </summary>
      /// <param name="width">The width.</param>
      /// <param name="forceLongSize">if set to <c>true</c> [force long size].</param>
      /// <param name="currentSize">Size of the current.</param>
      /// <returns>System.Double.</returns>
      private static double GetForcedWidth
      (
         double width,
         bool forceLongSize,
         Size currentSize
      )
      {
         width = forceLongSize ? currentSize.Width : width;
         return width;
      }
      
      public static int GetAdjacentCharacterCount(this string str, int maxCharsAllowed)
      {
         if (str.IsEmpty())
         {
            return 0;
         }

         var maxViolations = 0;
         
         for (var idx = maxCharsAllowed; idx < str.Length; idx++)
         {
            // Test the idx and characters directly to its left
            // The min idx is the exact characters allowed:
            // Assuming 2 consecutive characters are allowed - 3 is a violation --
            
            // ABCDDD
            
            // The idx finally gets to the end of the string, so is at idx 5
            // We start at 4, one to the left (no need to check the idx we are on).
            // We end at 3 because that's all we need for a violation:
            // The character "D" at indexes 3, 4 and 5
            var minIdx = idx - maxCharsAllowed;

            for (var subIdx = idx - 1; subIdx >= minIdx; subIdx--)
            {
               // Not the same, so not illegal
               if (str[idx] != str[subIdx])
               {
                  break;
               }

               // All characters matched
               if (subIdx == minIdx)
               {
                  maxViolations++;
               }
               
               // Else continue checking 
            }
         }

         return maxViolations;
      }

      public static void SetShapeViewStyle(
         this Style retStyle,
         Color?     backColor       = null,
         double?    cornerRadius    = null,
         Color?     borderColor     = null,
         double?    borderThickness = null)
      {
         if (backColor.HasValue)
         {
            retStyle.Setters.Add(new Setter { Property = ShapeView.ColorProperty, Value = backColor });
         }

         if (cornerRadius.HasValue)
         {
            retStyle.Setters.Add(new Setter { Property = PancakeView.CornerRadiusProperty, Value = cornerRadius });
         }

         if (borderColor.HasValue && borderThickness.HasValue)
         {
            retStyle.Setters.Add(new Setter { Property = PancakeView.BorderProperty, Value = CreateShapeViewBorder(borderColor, borderThickness) });
         }
      }
   }
}