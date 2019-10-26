#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, FormsUtils.cs, is a part of a program called AccountViewMobile.
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
   using Annotations;
   using Services;
   using SharedUtils.Utils;
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using System.Threading.Tasks;
   using Views.Controls;
   using Xamarin.Essentials;
   using Xamarin.Forms;
   using Xamarin.Forms.PancakeView;

   /// <summary>
   /// Class FormsUtils.
   /// </summary>
   public static class FormsUtils
   {
      /// <summary>
      /// Adds the and set rows and columns.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="view">The view.</param>
      /// <param name="row">The row.</param>
      /// <param name="column">The column.</param>
      /// <param name="rowSpan">The row span.</param>
      /// <param name="colSpan">The col span.</param>
      public static void AddAndSetRowsAndColumns(this Grid grid, View view, int? row = default,
                                                 int?      column  = default,
                                                 int?      rowSpan = default, int? colSpan = default)
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

      /// <summary>
      /// Adds the animation and haptic feedback.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="animate">if set to <c>true</c> [animate].</param>
      /// <param name="vibrate">if set to <c>true</c> [vibrate].</param>
      /// <returns>Task.</returns>
      public static async Task AddAnimationAndHapticFeedback
      (
         this View view,
         bool      animate = true,
         bool      vibrate = true
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

      /// <summary>
      /// Adds the automatic column.
      /// </summary>
      /// <param name="grid">The grid.</param>
      public static void AddAutoColumn(this Grid grid)
      {
         grid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
      }

      /// <summary>
      /// Adds the automatic row.
      /// </summary>
      /// <param name="grid">The grid.</param>
      public static void AddAutoRow(this Grid grid)
      {
         grid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
      }

      /// <summary>
      /// Adds the fixed column.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="width">The width.</param>
      public static void AddFixedColumn
      (
         this Grid grid,
         double    width
      )
      {
         grid.ColumnDefinitions.Add(new ColumnDefinition {Width = width});
      }

      /// <summary>
      /// Adds the fixed row.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="height">The height.</param>
      public static void AddFixedRow
      (
         this Grid grid,
         double    height
      )
      {
         grid.RowDefinitions.Add(new RowDefinition {Height = height});
      }

      /// <summary>
      /// Adds the overlay based on position.
      /// </summary>
      /// <param name="layout">The layout.</param>
      /// <param name="view">The view.</param>
      /// <param name="position">The position.</param>
      /// <param name="viewWidth">Width of the view.</param>
      /// <param name="viewHeight">Height of the view.</param>
      public static void AddOverlayBasedOnPosition(this RelativeLayout          layout,
                                                   View                         view,
                                                   FormsConst.OnScreenPositions position,
                                                   double                       viewWidth, double viewHeight)
      {
         if (position == FormsConst.OnScreenPositions.NONE)
         {
            return;
         }

         switch (position)
         {
            case FormsConst.OnScreenPositions.CENTER:
               layout.Children.Add
               (
                  view,
                  GetHorizontallyCenteredConstraint(),
                  GetVerticallyCenteredConstraint()
               );
               break;

            case FormsConst.OnScreenPositions.BOTTOM_CENTER:
               layout.Children.Add
               (
                  view,
                  GetHorizontallyCenteredConstraint(),
                  GetBottomVerticalConstraint()
               );
               break;

            case FormsConst.OnScreenPositions.BOTTOM_LEFT:
               layout.Children.Add
               (
                  view,
                  GetLeftHorizontalConstraint(),
                  GetBottomVerticalConstraint()
               );
               break;

            case FormsConst.OnScreenPositions.BOTTOM_RIGHT:
               layout.Children.Add
               (
                  view,
                  GetRightHorizontalConstraint(),
                  GetBottomVerticalConstraint()
               );
               break;

            case FormsConst.OnScreenPositions.TOP_CENTER:
               layout.Children.Add
               (
                  view,
                  GetHorizontallyCenteredConstraint(),
                  GetTopVerticalConstraint()
               );
               break;

            case FormsConst.OnScreenPositions.TOP_LEFT:
               layout.Children.Add
               (
                  view,
                  GetLeftHorizontalConstraint(),
                  GetTopVerticalConstraint()
               );
               break;

            case FormsConst.OnScreenPositions.TOP_RIGHT:
               layout.Children.Add
               (
                  view,
                  GetRightHorizontalConstraint(),
                  GetTopVerticalConstraint()
               );
               break;

            case FormsConst.OnScreenPositions.LEFT_CENTER:
               layout.Children.Add
               (
                  view,
                  GetLeftHorizontalConstraint(),
                  GetVerticallyCenteredConstraint()
               );
               break;

            case FormsConst.OnScreenPositions.RIGHT_CENTER:
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

      /// <summary>
      /// Adds the content of the row.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="view">The view.</param>
      public static void AddRowContent(this Grid grid, View view)
      {
         // The count is zero-based, so before we add, the physical count is the same as the "next" count
         var nextRow = grid.Children.Count;
         grid.AddAutoRow();
         grid.Children.Add(view);
         Grid.SetRow(view, nextRow);
      }

      /// <summary>
      /// Adds the star column.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="factor">The factor.</param>
      public static void AddStarColumn
      (
         this Grid grid,
         double    factor = 1
      )
      {
         grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(factor, GridUnitType.Star)});
      }

      /// <summary>
      /// Adds the star row.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="factor">The factor.</param>
      public static void AddStarRow
      (
         this Grid grid,
         double    factor = 1
      )
      {
         grid.RowDefinitions.Add(new RowDefinition {Height = new GridLength(factor, GridUnitType.Star)});
      }

      /// <summary>
      /// Adjusteds for screen height bug.
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

      /// <summary>
      /// Animates the height change.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="nextHeight">Height of the next.</param>
      /// <param name="fadeMilliseconds">The fade milliseconds.</param>
      /// <param name="easing">The easing.</param>
      public static void AnimateHeightChange(
         this View          view,
         double             nextHeight,
         uint               fadeMilliseconds = 250,
         [CanBeNull] Easing easing           = null)
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

      /// <summary>
      /// Animates the width change.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="nextWidth">Width of the next.</param>
      /// <param name="fadeMilliseconds">The fade milliseconds.</param>
      /// <param name="easing">The easing.</param>
      public static void AnimateWidthChange(
         this View          view,
         double             nextWidth,
         uint               fadeMilliseconds = 250,
         [CanBeNull] Easing easing           = null)
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

      /// <summary>
      /// Boundses the are valid and have changed.
      /// </summary>
      /// <param name="bounds">The bounds.</param>
      /// <param name="propName">Name of the property.</param>
      /// <param name="lastBounds">The last bounds.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool AreValidAndHaveChanged
      (
         this Rectangle bounds,
         string         propName,
         Rectangle      lastBounds
      )
      {
         return propName.IsBoundsRelatedPropertyChange() && bounds.IsValid() && bounds.IsDifferentThan(lastBounds);
      }

      /// <summary>
      /// Checks the against zero.
      /// </summary>
      /// <param name="dbl">The double.</param>
      /// <returns>System.Double.</returns>
      public static double CheckAgainstZero(this double dbl)
      {
         return Math.Max(0, dbl);
      }

      /// <summary>
      /// Clears the completely.
      /// </summary>
      /// <param name="grid">The grid.</param>
      public static void ClearCompletely(this Grid grid)
      {
         grid.Children.Clear();
         grid.ColumnDefinitions.Clear();
         grid.RowDefinitions.Clear();
      }

      /// <summary>
      /// Compresseds the date.
      /// </summary>
      /// <param name="dt">The dt.</param>
      /// <returns>System.String.</returns>
      public static string CompressedDate(this DateTime dt)
      {
         return $"{dt:M/d/yy}";
      }

      /// <summary>
      /// Considers the ios nested stage height hack.
      /// </summary>
      /// <param name="isNestedStage">if set to <c>true</c> [is nested stage].</param>
      /// <returns>System.Single.</returns>
      public static float ConsiderIosNestedStageHeightHack(bool isNestedStage)
      {
         return isNestedStage && IsIos() ? FormsConst.IOS_NESTED_STAGE_REDUCTION : 0;
      }

      // public static Rectangle ConvertToInternalBounds(this Rectangle rect, double heightAdjustment)
      /// <summary>
      /// Converts to internal bounds.
      /// </summary>
      /// <param name="rect">The rect.</param>
      /// <returns>Rectangle.</returns>
      public static Rectangle ConvertToInternalBounds(this Rectangle rect) // , double heightAdjustment)
      {
         return new Rectangle(0, 0, rect.Width, rect.Height); // + heightAdjustment);
      }

      /// <summary>
      /// Creates the entry style.
      /// </summary>
      /// <param name="fontFamily">The font family.</param>
      /// <param name="fontSize">Size of the font.</param>
      /// <param name="backColor">Color of the back.</param>
      /// <param name="textColor">Color of the text.</param>
      /// <param name="fontAttributes">The font attributes.</param>
      /// <returns>Style.</returns>
      public static Style CreateEntryStyle(
         [CanBeNull] string fontFamily     = "", double? fontSize = null, Color? backColor = null,
         Color?             textColor      = null,
         FontAttributes?    fontAttributes = null)
      {
         var retStyle = new Style(typeof(Entry));

         if (fontFamily.IsNotEmpty())
         {
            retStyle.Setters.Add(new Setter {Property = Entry.FontFamilyProperty, Value = fontFamily});
         }

         if (fontSize.HasValue)
         {
            retStyle.Setters.Add(new Setter {Property = Entry.FontSizeProperty, Value = fontSize});
         }

         if (backColor != null)
         {
            retStyle.Setters.Add(new Setter {Property = VisualElement.BackgroundColorProperty, Value = backColor});
         }

         if (textColor != null)
         {
            retStyle.Setters.Add(new Setter {Property = Entry.TextColorProperty, Value = textColor});
         }

         if (fontAttributes != null)
         {
            retStyle.Setters.Add(new Setter {Property = Entry.FontAttributesProperty, Value = fontAttributes});
         }

         return retStyle;
      }

      /// <summary>
      /// Creates the label style.
      /// </summary>
      /// <param name="fontFamily">The font family.</param>
      /// <param name="fontSize">Size of the font.</param>
      /// <param name="backColor">Color of the back.</param>
      /// <param name="textColor">Color of the text.</param>
      /// <param name="fontAttributes">The font attributes.</param>
      /// <returns>Style.</returns>
      public static Style CreateLabelStyle(
         [CanBeNull] string fontFamily     = "", double? fontSize = null, Color? backColor = null,
         Color?             textColor      = null,
         FontAttributes?    fontAttributes = null)
      {
         var retStyle = new Style(typeof(Label));

         if (fontFamily.IsNotEmpty())
         {
            retStyle.Setters.Add(new Setter {Property = Label.FontFamilyProperty, Value = fontFamily});
         }

         if (fontSize.HasValue)
         {
            retStyle.Setters.Add(new Setter {Property = Label.FontSizeProperty, Value = fontSize});
         }

         if (backColor != null)
         {
            retStyle.Setters.Add(new Setter {Property = VisualElement.BackgroundColorProperty, Value = backColor});
         }

         if (textColor != null)
         {
            retStyle.Setters.Add(new Setter {Property = Label.TextColorProperty, Value = textColor});
         }

         if (fontAttributes != null)
         {
            retStyle.Setters.Add(new Setter {Property = Label.FontAttributesProperty, Value = fontAttributes});
         }

         return retStyle;
      }

      /// <summary>
      /// Creates the off screen rect.
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
         Size                          parentViewSize,
         double                        width,
         double                        height,
         FormsConst.OffScreenPositions position,
         bool                          forceLongSize = false,

         // Allows space for a header above a toolbar.
         double additionalTopAllowance = 0
      )
      {
         var adjustedHeight = forceLongSize ? 0 : parentViewSize.Height / 2 - height / 2;
         var adjustedWidth  = forceLongSize ? 0 : parentViewSize.Width  / 2 - width  / 2;

         switch (position)
         {
            case FormsConst.OffScreenPositions.LEFT:
               height = GetForcedHeight(height, forceLongSize, parentViewSize);
               return new Rectangle(-width - 1, adjustedHeight + additionalTopAllowance, width,
                                    height - additionalTopAllowance);

            case FormsConst.OffScreenPositions.TOP:
               width = GetForcedWidth(width, forceLongSize, parentViewSize);
               return new Rectangle(adjustedWidth, -height, width, height);

            case FormsConst.OffScreenPositions.RIGHT:
               height = GetForcedHeight(height, forceLongSize, parentViewSize);
               return new Rectangle(parentViewSize.Width + 1, adjustedHeight + additionalTopAllowance, width,
                                    height               - additionalTopAllowance);

            case FormsConst.OffScreenPositions.BOTTOM:
               width = GetForcedWidth(width, forceLongSize, parentViewSize);
               return new Rectangle(adjustedWidth, parentViewSize.Height + 1, width, height);

            default:
               return default;
         }
      }

      /// <summary>
      /// Creates the off screen rect with forced side.
      /// </summary>
      /// <param name="parentViewSize">Size of the parent view.</param>
      /// <param name="widthHeight">Height of the width.</param>
      /// <param name="position">The position.</param>
      /// <param name="additionalTopAllowance">The additional top allowance.</param>
      /// <returns>Rectangle.</returns>
      public static Rectangle CreateOffScreenRectWithForcedSide
      (
         Size                          parentViewSize,
         double                        widthHeight,
         FormsConst.OffScreenPositions position,

         // Allows space for a header
         double additionalTopAllowance = 0
      )
      {
         var rect = CreateOffScreenRect(parentViewSize, widthHeight, widthHeight, position, true,
                                        additionalTopAllowance);
         return rect;
      }

      /// <summary>
      /// Creates an on-screen rectangle for the current control based on its width, height and position.
      /// If forceLongSize==true, either the width or height are set to the maximum screen width or height, depending on the
      /// position.
      /// forceLongSize *only* works with basic positions --
      /// - LEFT_CENTER
      /// - TOP_CENTER
      /// - RIGHT_CENTER
      /// - BOTTOM_CENTER
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
         Size                         parentViewSize,
         double                       width,
         double                       height,
         FormsConst.OnScreenPositions position,
         Thickness                    parentViewPadding = default,
         bool                         forceLongSize     = false,

         // Allows space for a header
         double additionalTopAllowance = 0
      )
      {
         ErrorUtils.ConsiderArgumentError
         (
            forceLongSize                                         &&
            position != FormsConst.OnScreenPositions.LEFT_CENTER  &&
            position != FormsConst.OnScreenPositions.TOP_CENTER   &&
            position != FormsConst.OnScreenPositions.RIGHT_CENTER &&
            position != FormsConst.OnScreenPositions.BOTTOM_CENTER,
            nameof(CreateOnScreenRect) + ": illegal position when requesting 'forceLongSize' ->" + position + "<-"
         );

         if (parentViewPadding.IsAnEqualObjectTo(default(Thickness)))
         {
            parentViewPadding = new Thickness(0);
         }

         // Initially adjust the width and height if we have a forced long side (as with a toolbar)
         //    This can only be done on one side (not both width and height)
         if (position == FormsConst.OnScreenPositions.TOP_CENTER ||
             position == FormsConst.OnScreenPositions.BOTTOM_CENTER)
         {
            width = GetForcedWidth(width, forceLongSize, parentViewSize);
         }
         else if (position == FormsConst.OnScreenPositions.LEFT_CENTER ||
                  position == FormsConst.OnScreenPositions.RIGHT_CENTER)
         {
            height = GetForcedHeight(height, forceLongSize, parentViewSize);
         }

         // Then adjust according to the padding
         width = Math.Min(width, parentViewSize.Width - parentViewPadding.Left - parentViewPadding.Right);
         height = Math.Min(
            height, parentViewSize.Height - parentViewPadding.Top - parentViewPadding.Bottom - additionalTopAllowance);

         // Set up the adjusted X & Y positions
         var paddingAdjustedLeftXPos   = parentViewPadding.Left;
         var paddingAdjustedRightXPos  = parentViewSize.Width - width   - parentViewPadding.Right;
         var paddingAdjustedTopYPos    = parentViewPadding.Top          + additionalTopAllowance;
         var paddingAdjustedBottomYPos = parentViewSize.Height - height - parentViewPadding.Bottom;

         // Ignores padding, as it splits the difference in order to center
         var horizontalCenterX = (parentViewSize.Width - width) / 2;
         var verticalCenterY   = additionalTopAllowance + (parentViewSize.Height - height - additionalTopAllowance) / 2;

         double newLeftX;
         double newTopY;

         switch (position)
         {
            case FormsConst.OnScreenPositions.LEFT_CENTER:
               newLeftX = paddingAdjustedLeftXPos;
               newTopY  = verticalCenterY;
               break;

            case FormsConst.OnScreenPositions.TOP_LEFT:
               newLeftX = paddingAdjustedLeftXPos;
               newTopY  = paddingAdjustedTopYPos;
               break;

            case FormsConst.OnScreenPositions.TOP_CENTER:
               newLeftX = horizontalCenterX;
               newTopY  = paddingAdjustedTopYPos;
               break;

            case FormsConst.OnScreenPositions.TOP_RIGHT:
               newLeftX = paddingAdjustedRightXPos;
               newTopY  = paddingAdjustedTopYPos;
               break;

            case FormsConst.OnScreenPositions.RIGHT_CENTER:
               newLeftX = paddingAdjustedRightXPos;
               newTopY  = verticalCenterY;
               break;

            case FormsConst.OnScreenPositions.BOTTOM_LEFT:
               newLeftX = paddingAdjustedLeftXPos;
               newTopY  = paddingAdjustedBottomYPos;
               break;

            case FormsConst.OnScreenPositions.BOTTOM_CENTER:
               newLeftX = horizontalCenterX;
               newTopY  = paddingAdjustedBottomYPos;
               break;

            case FormsConst.OnScreenPositions.BOTTOM_RIGHT:
               newLeftX = paddingAdjustedRightXPos;
               newTopY  = paddingAdjustedBottomYPos;
               break;

            case FormsConst.OnScreenPositions.CENTER:
               newLeftX = horizontalCenterX;
               newTopY  = verticalCenterY;
               break;

            default:
               return default;
         }

         var rect = new Rectangle(newLeftX, newTopY, width, height);
         return rect;
      }

      /// <summary>
      /// Creates the on screen rect with forced side.
      /// </summary>
      /// <param name="parentViewSize">Size of the parent view.</param>
      /// <param name="widthHeight">Height of the width.</param>
      /// <param name="position">The position.</param>
      /// <param name="parentViewPadding">The parent view padding.</param>
      /// <param name="additionalTopAllowance">The additional top allowance.</param>
      /// <returns>Rectangle.</returns>
      public static Rectangle CreateOnScreenRectWithForcedSide
      (
         Size                         parentViewSize,
         double                       widthHeight,
         FormsConst.OnScreenPositions position,
         Thickness                    parentViewPadding = default,

         // Allows space for a header
         double additionalTopAllowance = 0
      )
      {
         var rect = CreateOnScreenRect(parentViewSize, widthHeight, widthHeight, position, parentViewPadding, true,
                                       additionalTopAllowance);
         return rect;
      }

      /// <summary>
      /// Creates the relative overlay.
      /// </summary>
      /// <param name="layout">The layout.</param>
      /// <param name="viewToAdd">The view to add.</param>
      /// <param name="padding">The padding.</param>
      public static void CreateRelativeOverlay
      (
         this RelativeLayout layout,
         View                viewToAdd,
         Thickness           padding = default
      )
      {
         layout.Children.Add(
            viewToAdd, Constraint.Constant(padding.Left), Constraint.Constant(padding.Top),
            Constraint.RelativeToParent(parent => parent.Width - padding.Left - padding.Right),
            Constraint.RelativeToParent(parent => parent.Height - padding.Top - padding.Bottom));
      }

      /// <summary>
      /// Creates the shape view style.
      /// </summary>
      /// <param name="backColor">Color of the back.</param>
      /// <param name="cornerRadius">The corner radius.</param>
      /// <param name="borderColor">Color of the border.</param>
      /// <param name="BorderThickness">The border thickness.</param>
      /// <returns>Style.</returns>
      public static Style CreateShapeViewStyle(
         Color?  backColor       = null, double? cornerRadius = null, Color? borderColor = null,
         double? BorderThickness = null)
      {
         var retStyle = new Style(typeof(ShapeView));

         if (backColor != null)
         {
            retStyle.Setters.Add(new Setter {Property = ShapeView.ColorProperty, Value = backColor});
         }

         if (cornerRadius.HasValue)
         {
            retStyle.Setters.Add(new Setter {Property = PancakeView.CornerRadiusProperty, Value = cornerRadius});
         }

         if (borderColor != null)
         {
            retStyle.Setters.Add(new Setter {Property = PancakeView.BorderColorProperty, Value = borderColor});
         }

         if (BorderThickness.HasValue)
         {
            retStyle.Setters.Add(new Setter {Property = PancakeView.BorderThicknessProperty, Value = BorderThickness});
         }

         return retStyle;
      }

      /// <summary>
      /// Creates the tasks.
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

      /// <summary>
      /// Enums the try parse.
      /// </summary>
      /// <typeparam name="EnumT">The type of the enum t.</typeparam>
      /// <param name="input">The input.</param>
      /// <param name="theEnum">The enum.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool EnumTryParse<EnumT>
      (
         string    input,
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
      /// <summary>
      /// Estimates the height.
      /// </summary>
      /// <param name="label">The label.</param>
      /// <param name="width">The width.</param>
      /// <returns>System.Double.</returns>
      /// <remarks>This is rough and inductive.</remarks>
      public static double EstimateHeight(this Label label, double width)
      {
         if (label.IsNullOrDefault() || !width.IsGreaterThan(0))
         {
            return 0;
         }

         var length      = label.Text.Count();
         var totalLength = length * label.FontSize * FormsConst.CHARACTERS_TO_FONT_SIZE_ESTIMATOR;
         var totalLines  = totalLength             / width;

         return totalLines * label.FontSize; // * (Math.Max(label.LineHeight, label.FontSize));
      }

      /// <summary>
      /// Expandeds the specified space count.
      /// </summary>
      /// <param name="str">The string.</param>
      /// <param name="spaceCount">The space count.</param>
      /// <returns>System.String.</returns>
      public static string Expanded(this string str, int spaceCount = 1)
      {
         if (str.IsEmpty())
         {
            return "";
         }

         var strBuilder  = new StringBuilder();
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
               strBuilder.Append(FormsConst.SPACE_CHAR);
            }
         }

         // We end up with a trailing space
         return strBuilder.ToString().Trim();
      }

      /// <summary>
      /// Fades the in.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="fadeMilliseconds">The fade milliseconds.</param>
      /// <param name="easing">The easing.</param>
      /// <param name="maxOpacity">The maximum opacity.</param>
      public static void FadeIn(
         this View          view,
         uint               fadeMilliseconds = 250,
         [CanBeNull] Easing easing           = null,
         double             maxOpacity       = FormsConst.VISIBLE_OPACITY)
      {
         // Nothing to do
         if (view.Opacity.IsSameAs(maxOpacity))
         {
            return;
         }

         var fadeAnimation = new Animation(f => view.Opacity = f,
                                           view.Opacity,
                                           maxOpacity, easing);
         fadeAnimation.Commit(view, "fadeAnimation", length: fadeMilliseconds, easing: easing);
      }

      /// <summary>
      /// Fades the out.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="fadeMilliseconds">The fade milliseconds.</param>
      /// <param name="easing">The easing.</param>
      /// <returns>Task.</returns>
      public static void FadeOut(
         this View          view,
         uint               fadeMilliseconds = 250,
         [CanBeNull] Easing easing           = null)
      {
         // Nothing to do
         if (view.Opacity.IsSameAs(FormsConst.NOT_VISIBLE_OPACITY))
         {
            return;
         }

         var fadeAnimation = new Animation(f => view.Opacity = f,
                                           view.Opacity, FormsConst.NOT_VISIBLE_OPACITY, easing);
         fadeAnimation.Commit(view, "fadeAnimation", length: fadeMilliseconds, easing: easing);
      }

      /// <summary>
      /// Fixes the negative dimensions.
      /// </summary>
      /// <param name="rect">The rect.</param>
      /// <returns>Rectangle.</returns>
      public static Rectangle FixNegativeDimensions(this Rectangle rect)
      {
         return new Rectangle(CheckAgainstZero(rect.X), CheckAgainstZero(rect.Y), CheckAgainstZero(rect.Width),
                              CheckAgainstZero(rect.Height));
      }

      /// <summary>
      /// Forces the aspect.
      /// </summary>
      /// <param name="rect">The rect.</param>
      /// <param name="aspect">The aspect.</param>
      /// <returns>Rectangle.</returns>
      public static Rectangle ForceAspect(this Rectangle rect, double aspect)
      {
         var currentAspect = rect.Width / rect.Height;
         var newWidth      = rect.Width;
         var newHeight     = rect.Height;

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
         var widthDiff  = Math.Max(0, rect.Width  - newWidth);

         return new Rectangle(rect.X + widthDiff / 2, rect.Y + heightDiff / 2, newWidth, newHeight);
      }

      /// <summary>
      /// Forces the style.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="style">The style.</param>
      public static void ForceStyle
      (
         this View view,
         Style     style
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
               Debug.WriteLine(nameof(ForceStyle)                             +
                               ": could not find the property name ->"        +
                               style.Setters[setterIdx].Property.PropertyName + "<- on view");
               continue;
            }

            var targetPropertyType = style.Setters[setterIdx].Property.ReturnType;

            // ReSharper disable once PossibleNullReferenceException
            if (viewProperty.PropertyType != targetPropertyType)
            {
               Debug.WriteLine(nameof(ForceStyle)                                    +
                               ": view property ->"                                  +
                               style.Setters[setterIdx].Property.PropertyName        +
                               "<- shows as type ->"                                 +
                               viewProperty.PropertyType                             +
                               "<- which does not match the setter property type ->" +
                               targetPropertyType                                    + "<-");
               continue;
            }

#endif

            view.SetValue(style.Setters[setterIdx].Property, style.Setters[setterIdx].Value);
         }
      }

      /// <summary>
      /// Gets the expanding absolute layout.
      /// </summary>
      /// <returns>AbsoluteLayout.</returns>
      public static AbsoluteLayout GetExpandingAbsoluteLayout()
      {
         return new AbsoluteLayout
         {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions   = LayoutOptions.FillAndExpand,
            BackgroundColor   = Color.Transparent
         };
      }

      /// <summary>
      /// Gets the expanding grid.
      /// </summary>
      /// <returns>Grid.</returns>
      public static Grid GetExpandingGrid()
      {
         return new Grid
         {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions   = LayoutOptions.FillAndExpand,
            BackgroundColor   = Color.Transparent,
            ColumnSpacing     = 0,
            RowSpacing        = 0,
            Margin            = 0,
            Padding           = 0,
            IsClippedToBounds = true
         };
      }

      /// <summary>
      /// Gets the expanding relative layout.
      /// </summary>
      /// <returns>RelativeLayout.</returns>
      public static RelativeLayout GetExpandingRelativeLayout()
      {
         return new RelativeLayout
         {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions   = LayoutOptions.FillAndExpand,
            BackgroundColor   = Color.Transparent
         };
      }

      /// <summary>
      /// Gets the expanding scroll view.
      /// </summary>
      /// <returns>ScrollView.</returns>
      public static ScrollView GetExpandingScrollView()
      {
         return new ScrollView
         {
            VerticalOptions   = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor   = Color.Transparent,
            Orientation       = ScrollOrientation.Vertical
         };
      }

      /// <summary>
      /// Gets the expanding stack layout.
      /// </summary>
      /// <returns>StackLayout.</returns>
      public static StackLayout GetExpandingStackLayout()
      {
         return new StackLayout
         {
            VerticalOptions   = LayoutOptions.StartAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor   = Color.Transparent,
            Orientation       = StackOrientation.Vertical
         };
      }

      /// <summary>
      /// Gets the image.
      /// </summary>
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
         double width            = 0,
         double height           = 0,
         Aspect aspect           = Aspect.AspectFit,
         bool   getFromResources = false,
         Type   resourceClass    = null
      )
      {
         var retImage =
            new Image
            {
               Aspect            = aspect,
               VerticalOptions   = LayoutOptions.Center,
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

      /// <summary>
      /// Gets the keyboard from string.
      /// </summary>
      /// <param name="attributeKeyboardName">Name of the attribute keyboard.</param>
      /// <returns>Keyboard.</returns>
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
      /// Gets the margin for runtime platform.
      /// </summary>
      /// <returns>Thickness.</returns>
      public static Thickness GetMarginForRuntimePlatform()
      {
         var top = GetStartingYForRuntimePlatform();
         return new Thickness(0, top, 0, 0);
      }

      /// <summary>
      /// Gets the shape view.
      /// </summary>
      /// <returns>ShapeView.</returns>
      public static ShapeView GetShapeView()
      {
         var retShapeView = new ShapeView();
         retShapeView.SetDefaults();
         return retShapeView;
      }

      /// <summary>
      /// Gets the simple label.
      /// </summary>
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
      /// <param name="stringFormat">The string format.</param>
      /// <param name="breakMode">The break mode.</param>
      /// <param name="fontFamilyOverride">The font family override.</param>
      /// <returns>Label.</returns>
      public static Label GetSimpleLabel
      (
         string         labelText                = default,
         Color          textColor                = default,
         TextAlignment  textAlignment            = TextAlignment.Center,
         NamedSize      fontNamedSize            = NamedSize.Medium,
         double         fontSize                 = 0.0,
         FontAttributes fontAttributes           = FontAttributes.None,
         double         width                    = 0,
         double         height                   = 0,
         string         labelBindingPropertyName = default,
         object         labelBindingSource       = null,
         string         stringFormat             = "",
         LineBreakMode  breakMode                = LineBreakMode.WordWrap,
         string         fontFamilyOverride       = ""
      )
      {
         if (textColor.IsUnsetOrDefault())
         {
            textColor = Color.Black;
         }

         var retLabel =
            new Label
            {
               Text                    = labelText,
               TextColor               = textColor,
               HorizontalTextAlignment = textAlignment,
               VerticalTextAlignment   = TextAlignment.Center,
               HorizontalOptions       = HorizontalOptionsFromTextAlignment(textAlignment),
               VerticalOptions         = LayoutOptions.CenterAndExpand,
               BackgroundColor         = Color.Transparent,

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
      /// <summary>
      /// Gets the spacer.
      /// </summary>
      /// <param name="height">The height.</param>
      /// <returns>BoxView.</returns>
      public static BoxView GetSpacer(double height)
      {
         return new BoxView
         {
            HeightRequest     = height,
            BackgroundColor   = Color.Transparent,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions   = LayoutOptions.FillAndExpand
         };
      }

      /// <summary>
      /// Gets the starting y for runtime platform.
      /// </summary>
      /// <returns>System.Single.</returns>
      public static double GetStartingYForRuntimePlatform()
      {
         return IsIos() ? FormsConst.IOS_TOP_MARGIN : 0;
      }

      /// <summary>
      /// The bounds have become *invalid* (not changed necessarily) in relation to the last known bounds.
      /// </summary>
      /// <param name="bounds">The bounds.</param>
      /// <param name="propName">Name of the property.</param>
      /// <param name="lastBounds">The last bounds.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool HaveBecomeInvalid
      (
         this Rectangle bounds,
         string         propName,
         Rectangle      lastBounds
      )
      {
         return propName.IsBoundsRelatedPropertyChange() && bounds.IsNotValid() && lastBounds.IsValid();
      }

      /// <summary>
      /// The bounds have become *invalid* OR *changed* in relation to the last known bounds.
      /// </summary>
      /// <param name="bounds">The bounds.</param>
      /// <param name="propName">Name of the property.</param>
      /// <param name="lastBounds">The last bounds.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool HaveBecomeInvalidOrChangedWithoutPermission
      (
         this Rectangle bounds,
         string         propName,
         Rectangle      lastBounds
      )
      {
         return propName.IsBoundsRelatedPropertyChange() && lastBounds.IsValid() &&
                (bounds.IsNotValid() || bounds.IsDifferentThan(lastBounds));
      }

      /// <summary>
      /// Heights the height of from percent of screen.
      /// </summary>
      /// <param name="percentDesired">The percent desired.</param>
      /// <returns>System.Double.</returns>
      public static double HeightFromPercentOfScreenHeight(this double percentDesired)
      {
         return OrientationService.ScreenHeight * percentDesired;
      }

      /// <summary>
      /// Horizontals the options from text alignment.
      /// </summary>
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

      /// <summary>
      /// Inserts the automatic column.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="insertionLoc">The insertion loc.</param>
      public static void InsertAutoColumn(this Grid grid, int insertionLoc)
      {
         grid.ColumnDefinitions.Insert(insertionLoc, new ColumnDefinition {Width = GridLength.Auto});
      }

      /// <summary>
      /// Determines whether [is bounds related property change] [the specified property name].
      /// </summary>
      /// <param name="propName">Name of the property.</param>
      /// <returns><c>true</c> if [is bounds related property change] [the specified property name]; otherwise, <c>false</c>.</returns>
      public static bool IsBoundsRelatedPropertyChange
      (
         this string propName
      )
      {
         return propName.IsSameAs(FormsConst.WIDTH_PROPERTY_NAME)  ||
                propName.IsSameAs(FormsConst.HEIGHT_PROPERTY_NAME) || propName.IsSameAs(FormsConst.X_PROPERTY_NAME) ||
                propName.IsSameAs(FormsConst.Y_PROPERTY_NAME);
      }

      /// <summary>
      /// Determines whether [is different than] [the specified second color].
      /// </summary>
      /// <param name="color">The color.</param>
      /// <param name="secondColor">Color of the second.</param>
      /// <returns><c>true</c> if [is different than] [the specified second color]; otherwise, <c>false</c>.</returns>
      public static bool IsDifferentThan
      (
         this Color color,
         Color      secondColor
      )
      {
         return color.IsNotAnEqualObjectTo(secondColor);
      }

      /// <summary>
      /// Determines whether [is different than] [the specified other size].
      /// </summary>
      /// <param name="size">The size.</param>
      /// <param name="otherSize">Size of the other.</param>
      /// <returns><c>true</c> if [is different than] [the specified other size]; otherwise, <c>false</c>.</returns>
      public static bool IsDifferentThan(this Size size, Size otherSize)
      {
         return !size.IsSameAs(otherSize);
      }

      /// <summary>
      /// Determines whether [is different than] [the specified other thickness].
      /// </summary>
      /// <param name="Thickness">The thickness.</param>
      /// <param name="otherThickness">The other thickness.</param>
      /// <returns><c>true</c> if [is different than] [the specified other thickness]; otherwise, <c>false</c>.</returns>
      public static bool IsDifferentThan(this Thickness Thickness, Thickness otherThickness)
      {
         return !Thickness.IsSameAs(otherThickness);
      }

      /// <summary>
      /// Determines whether [is different than] [the specified other rect].
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
      /// Determines whether the specified size is empty.
      /// </summary>
      /// <param name="size">The size.</param>
      /// <returns><c>true</c> if the specified size is empty; otherwise, <c>false</c>.</returns>
      public static bool IsEmpty(this Size size)
      {
         return size.Width.IsLessThanOrEqualTo(0) && size.Height.IsLessThanOrEqualTo(0);
      }

      /// <summary>
      /// Determines whether the specified thickness is empty.
      /// </summary>
      /// <param name="Thickness">The thickness.</param>
      /// <returns><c>true</c> if the specified thickness is empty; otherwise, <c>false</c>.</returns>
      public static bool IsEmpty(this Thickness Thickness)
      {
         return Thickness.Bottom.IsLessThanOrEqualTo(0) && Thickness.Left.IsLessThanOrEqualTo(0) &&
                Thickness.Right.IsLessThanOrEqualTo(0)  && Thickness.Top.IsLessThanOrEqualTo(0);
      }

      /// <summary>
      /// Determines whether the specified main rect is empty.
      /// </summary>
      /// <param name="mainRect">The main rect.</param>
      /// <returns><c>true</c> if the specified main rect is empty; otherwise, <c>false</c>.</returns>
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

      /// <summary>
      /// Determines whether this instance is ios.
      /// </summary>
      /// <returns><c>true</c> if this instance is ios; otherwise, <c>false</c>.</returns>
      public static bool IsIos()
      {
         return Device.RuntimePlatform.IsSameAs(Device.iOS);
      }

      /// <summary>
      /// Determines whether [is not empty] [the specified size].
      /// </summary>
      /// <param name="size">The size.</param>
      /// <returns><c>true</c> if [is not empty] [the specified size]; otherwise, <c>false</c>.</returns>
      public static bool IsNotEmpty(this Size size)
      {
         return !size.IsEmpty();
      }

      /// <summary>
      /// Determines whether [is not empty] [the specified thickness].
      /// </summary>
      /// <param name="Thickness">The thickness.</param>
      /// <returns><c>true</c> if [is not empty] [the specified thickness]; otherwise, <c>false</c>.</returns>
      public static bool IsNotEmpty(this Thickness Thickness)
      {
         return !Thickness.IsEmpty();
      }

      /// <summary>
      /// Determines whether [is not empty] [the specified main rect].
      /// </summary>
      /// <param name="mainRect">The main rect.</param>
      /// <returns><c>true</c> if [is not empty] [the specified main rect]; otherwise, <c>false</c>.</returns>
      public static bool IsNotEmpty
      (
         this Rectangle mainRect
      )
      {
         return !mainRect.IsEmpty();
      }

      /// <summary>
      /// Determines whether [is not valid] [the specified bounds].
      /// </summary>
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
      /// Determines whether [is not visible] [the specified view].
      /// </summary>
      /// <param name="view">The view.</param>
      /// <returns><c>true</c> if [is not visible] [the specified view]; otherwise, <c>false</c>.</returns>
      public static bool IsNotVisible(this View view)
      {
         return view.Opacity.IsDifferentThan(FormsConst.VISIBLE_OPACITY);
      }

      /// <summary>
      /// Determines whether [is same as] [the specified other size].
      /// </summary>
      /// <param name="size">The size.</param>
      /// <param name="otherSize">Size of the other.</param>
      /// <returns><c>true</c> if [is same as] [the specified other size]; otherwise, <c>false</c>.</returns>
      public static bool IsSameAs(this Size size, Size otherSize)
      {
         return size.Width.IsSameAs(otherSize.Width) && size.Height.IsSameAs(otherSize.Height);
      }

      /// <summary>
      /// Determines whether [is same as] [the specified other thickness].
      /// </summary>
      /// <param name="Thickness">The thickness.</param>
      /// <param name="otherThickness">The other thickness.</param>
      /// <returns><c>true</c> if [is same as] [the specified other thickness]; otherwise, <c>false</c>.</returns>
      public static bool IsSameAs(this Thickness Thickness, Thickness otherThickness)
      {
         return Thickness.Bottom.IsSameAs(otherThickness.Bottom) && Thickness.Left.IsSameAs(otherThickness.Left) &&
                Thickness.Right.IsSameAs(otherThickness.Right)   && Thickness.Top.IsSameAs(otherThickness.Top);
      }

      /// <summary>
      /// Determines whether [is same as] [the specified other rect].
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

      /// <summary>
      /// Determines whether [is unset or default] [the specified color].
      /// </summary>
      /// <param name="color">The color.</param>
      /// <returns><c>true</c> if [is unset or default] [the specified color]; otherwise, <c>false</c>.</returns>
      public static bool IsUnsetOrDefault(this Color color)
      {
         return color.IsAnEqualObjectTo(default);
      }

      /// <summary>
      /// Returns true if ... is valid.
      /// </summary>
      /// <param name="bounds">The bounds.</param>
      /// <returns><c>true</c> if the specified bounds is valid; otherwise, <c>false</c>.</returns>
      public static bool IsValid(this Rectangle bounds)
      {
         return bounds.Width > 0 && bounds.Height > 0;
      }

      /// <summary>
      /// Returns true if ... is valid.
      /// </summary>
      /// <param name="size">The size.</param>
      /// <returns><c>true</c> if the specified size is valid; otherwise, <c>false</c>.</returns>
      public static bool IsValid(this Size size)
      {
         return size.Width > 0 && size.Height > 0;
      }

      /// <summary>
      /// Returns true if ... is valid.
      /// </summary>
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

      /// <summary>
      /// Merges the style.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="mainStyle">The main style.</param>
      /// <param name="newStyle">The new style.</param>
      /// <returns>Style.</returns>
      public static Style MergeStyle<T>
      (
         this Style mainStyle,
         Style      newStyle
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

      /// <summary>
      /// Presentables the date.
      /// </summary>
      /// <param name="dt">The dt.</param>
      /// <returns>System.String.</returns>
      public static string PresentableDate(this DateTime dt)
      {
         return $"{dt:MMM d, yyyy}";
      }

      /// <summary>
      /// Presentables the dollar amount.
      /// </summary>
      /// <param name="dollars">The dollars.</param>
      /// <returns>System.String.</returns>
      public static string PresentableDollarAmount(this double dollars)
      {
         return $"{dollars:c}";
      }

      /// <summary>
      /// Presentables the whole long int.
      /// </summary>
      /// <param name="lng">The LNG.</param>
      /// <returns>System.String.</returns>
      public static string PresentableWholeLongInt(this long lng)
      {
         return $"{lng:0}";
      }

      /// <summary>
      /// Replaces the layout to.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="nextBounds">The next bounds.</param>
      /// <param name="fadeMilliseconds">The fade milliseconds.</param>
      /// <param name="easing">The easing.</param>
      public static async Task ReplaceLayoutTo(
         this View          view,
         Rectangle          nextBounds,
         uint               fadeMilliseconds = 250,
         [CanBeNull] Easing easing           = null)
      {
         // Nothing to do
         if (view.Bounds.IsSameAs(nextBounds))
         {
            return;
         }

         //var tasks = new List<Task>();

         //if (view.Bounds.X.IsDifferentThan(nextBounds.X) || view.Bounds.Y.IsDifferentThan(nextBounds.Y))
         //{
         //   tasks.Add(view.TranslateTo(nextBounds.X - view.Bounds.X, nextBounds.Y - view.Bounds.Y, fadeMilliseconds,
         //                              easing).WithoutChangingContext());
         //}

         //if (view.Bounds.Width.IsDifferentThan(nextBounds.Width))
         //{
         //   // view.WidthRequest = nextBounds.Width;
         //   tasks.Add(new Task(() => view.AnimateWidthChange(nextBounds.Width, fadeMilliseconds, easing)).WithoutChangingContext());
         //}

         //if (view.Bounds.Height.IsDifferentThan(nextBounds.Height))
         //{
         //   // view.HeightRequest = nextBounds.Height;
         //   tasks.Add(new Task(() => view.AnimateHeightChange(nextBounds.Height, fadeMilliseconds, easing)).WithoutChangingContext());
         //}

         if (view.Bounds.X.IsDifferentThan(nextBounds.X) || view.Bounds.Y.IsDifferentThan(nextBounds.Y))
         {
            await view.TranslateTo(nextBounds.X - view.Bounds.X, nextBounds.Y - view.Bounds.Y, fadeMilliseconds,
                                   easing).WithoutChangingContext();
         }

         if (view.Bounds.Width.IsDifferentThan(nextBounds.Width))
         {
            // view.WidthRequest = nextBounds.Width;
            view.AnimateWidthChange(nextBounds.Width, fadeMilliseconds, easing);
         }

         if (view.Bounds.Height.IsDifferentThan(nextBounds.Height))
         {
            // view.HeightRequest = nextBounds.Height;
            view.AnimateHeightChange(nextBounds.Height, fadeMilliseconds, easing);
         }
      }

      /// <summary>
      /// Scales the in.
      /// </summary>
      /// <param name="view">The view.</param>
      public static async Task ScaleIn(this View view)
      {
         await view.ScaleTo(FormsConst.NORMAL_SCALE).WithoutChangingContext();
      }

      /// <summary>
      /// Scales the out.
      /// </summary>
      /// <param name="view">The view.</param>
      public static async Task ScaleOut(this View view)
      {
         await view.ScaleTo(FormsConst.NO_SCALE).WithoutChangingContext();
      }

      /// <summary>
      /// Sets the and force style.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="style">The style.</param>
      public static void SetAndForceStyle(this View view, Style style)
      {
         view.Style = style;
         view.ForceStyle(style);
      }

      /// <summary>
      /// This method assumes on entry, the first view has a gesture recognizer (else exits).
      /// From there, the child view Input Transparent Booeans are set so the first view's gesture recognizer is active at all
      /// times, regardless of where the user taps.
      /// </summary>
      /// <param name="view">The view.</param>
      public static void SetChildViewInputTransparencies(this View view)
      {
         if (view.IsNullOrDefault())
         {
            return;
         }

         if (view is ContentView viewAsContentView)
         {
            view.InputTransparent = false;
            SetChildViewInputTransparencies(viewAsContentView.Content);
         }
         else if (view is ScrollView viewAsScrollView)
         {
            view.InputTransparent = false;
            SetChildViewInputTransparencies(viewAsScrollView.Content);
         }
         else if (view is Layout<View> viewAsLayoutView)
         {
            view.InputTransparent = false;

            foreach (var child in viewAsLayoutView.Children)
            {
               SetChildViewInputTransparencies(child);
            }
         }
         else if (view is InputView viewAsInputView)
         {
            viewAsInputView.InputTransparent = false;
         }
         else
         {
            // Add a tap listener to reach top" view
            view.InputTransparent = true;
         }
      }

      /// <summary>
      /// Sets the defaults.
      /// </summary>
      /// <param name="shapeView">The shape view.</param>
      public static void SetDefaults(this ShapeView shapeView)
      {
         shapeView.Color             = Color.White;
         shapeView.CornerRadius      = FormsConst.DEFAULT_SHAPE_VIEW_RADIUS;
         shapeView.BackgroundColor   = Color.Transparent;
         shapeView.IsClippedToBounds = true;
         shapeView.HorizontalOptions = LayoutOptions.FillAndExpand;
         shapeView.VerticalOptions   = LayoutOptions.FillAndExpand;
      }

      /// <summary>
      /// Converts to enum.
      /// </summary>
      /// <typeparam name="EnumT">The type of the enum t.</typeparam>
      /// <param name="enumAsString">The enum as string.</param>
      /// <returns>EnumT.</returns>
      public static EnumT ToEnum<EnumT>(this string enumAsString)
         where EnumT : Enum
      {
         /*
         if (enumAsString.IsEmpty())
         {
            return default(EnumT);
         }

         return ((EnumT)Enum.Parse(typeof(EnumT), enumAsString, true));
         */

         if (EnumTryParse(enumAsString, out EnumT foundEnum))
         {
            return foundEnum;
         }

         return default;
      }

      /// <summary>
      /// Converts to off screen position.
      /// </summary>
      /// <param name="stageToolbarPosition">The stage toolbar position.</param>
      /// <returns>OffScreenPositions.</returns>
      public static FormsConst.OffScreenPositions ToOffScreenPosition(
         this FormsConst.StageToolbarPositions stageToolbarPosition)
      {
         switch (stageToolbarPosition)
         {
            case FormsConst.StageToolbarPositions.BOTTOM: return FormsConst.OffScreenPositions.BOTTOM;

            case FormsConst.StageToolbarPositions.LEFT: return FormsConst.OffScreenPositions.LEFT;

            case FormsConst.StageToolbarPositions.TOP: return FormsConst.OffScreenPositions.TOP;

            case FormsConst.StageToolbarPositions.RIGHT: return FormsConst.OffScreenPositions.RIGHT;
         }

         return FormsConst.OffScreenPositions.NONE;
      }

      /// <summary>
      /// Converts to offscreenposition.
      /// </summary>
      /// <param name="flowDirection">The flow direction.</param>
      /// <returns>FormsConst.OffScreenPositions.</returns>
      public static FormsConst.OffScreenPositions ToOffScreenPosition(
         this FormsConst.SubStageFlowDirections flowDirection)
      {
         switch (flowDirection)
         {
            case FormsConst.SubStageFlowDirections.BOTTOM_TO_TOP: return FormsConst.OffScreenPositions.BOTTOM;
            case FormsConst.SubStageFlowDirections.LEFT_TO_RIGHT: return FormsConst.OffScreenPositions.LEFT;
            case FormsConst.SubStageFlowDirections.TOP_TO_BOTTOM: return FormsConst.OffScreenPositions.TOP;
            case FormsConst.SubStageFlowDirections.RIGHT_TO_LEFT: return FormsConst.OffScreenPositions.RIGHT;
         }

         return FormsConst.OffScreenPositions.NONE;
      }

      /// <summary>
      /// Converts to onscreenposition.
      /// </summary>
      /// <param name="stageToolbarPosition">The stage toolbar position.</param>
      /// <returns>OnScreenPositions.</returns>
      public static FormsConst.OnScreenPositions ToOnScreenPosition(
         this FormsConst.StageToolbarPositions stageToolbarPosition)
      {
         switch (stageToolbarPosition)
         {
            case FormsConst.StageToolbarPositions.BOTTOM: return FormsConst.OnScreenPositions.BOTTOM_CENTER;

            case FormsConst.StageToolbarPositions.LEFT: return FormsConst.OnScreenPositions.LEFT_CENTER;

            case FormsConst.StageToolbarPositions.TOP: return FormsConst.OnScreenPositions.TOP_CENTER;

            case FormsConst.StageToolbarPositions.RIGHT: return FormsConst.OnScreenPositions.RIGHT_CENTER;
         }

         return FormsConst.OnScreenPositions.NONE;
      }

      /// <summary>
      /// Converts to options.
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

      /// <summary>
      /// Widthes the width of from percent of screen.
      /// </summary>
      /// <param name="percentDesired">The percent desired.</param>
      /// <returns>System.Double.</returns>
      public static double WidthFromPercentOfScreenWidth(this double percentDesired)
      {
         return OrientationService.ScreenWidth * percentDesired;
      }

      /// <summary>
      /// Runs a Task without changing the context (configure await is false).
      /// </summary>
      /// <param name="task">The task.</param>
      /// <returns>Task.</returns>
      public static async Task WithoutChangingContext(this Task task) =>
#if AVOID_CONTEXT_MANAGEMENT
         await task;
#else
         await task.ConfigureAwait(false);

#endif

      /// <summary>
      /// Withouts the changing context.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="task">The task.</param>
      /// <returns>Task&lt;T&gt;.</returns>
      public static async Task<T> WithoutChangingContext<T>(this Task<T> task)
      {
         return await task.ConfigureAwait(false);
      }

      /// <summary>
      /// Gets the height of the forced.
      /// </summary>
      /// <param name="height">The height.</param>
      /// <param name="forceLongSize">if set to <c>true</c> [force long size].</param>
      /// <param name="currentSize">Size of the current.</param>
      /// <returns>System.Double.</returns>
      private static double GetForcedHeight
      (
         double height,
         bool   forceLongSize,
         Size   currentSize
      )
      {
         height = forceLongSize ? currentSize.Height : height;
         return height;
      }

      /// <summary>
      /// Gets the width of the forced.
      /// </summary>
      /// <param name="width">The width.</param>
      /// <param name="forceLongSize">if set to <c>true</c> [force long size].</param>
      /// <param name="currentSize">Size of the current.</param>
      /// <returns>System.Double.</returns>
      private static double GetForcedWidth
      (
         double width,
         bool   forceLongSize,
         Size   currentSize
      )
      {
         width = forceLongSize ? currentSize.Width : width;
         return width;
      }

      //public static string StripLeadingZeroes(this string str)
      //{
      //   if (str.IsEmpty())
      //   {
      //      return str;
      //   }

      //   var newStartIdx = 0;

      //   for (var charIdx = 0; charIdx < str.Length; charIdx++)
      //   {
      //      if (str[charIdx] != ZERO_CHAR)
      //      {
      //         newStartIdx = charIdx;
      //         break;
      //      }
      //   }

      //   return str.Substring(newStartIdx);
      //}

      //public static string StripTrailingNumbers(this string str, int maxDecimalChars)
      //{
      //   var decimalPos = str.PositionOfDecimal();

      //   if (decimalPos >= 0)
      //   {
      //   }
      //}
   }
}