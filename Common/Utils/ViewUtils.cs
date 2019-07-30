// *********************************************************************************
// <copyright file=ViewUtils.cs company="Marcus Technical Services, Inc.">
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
   using Com.MarcusTS.SharedUtils.Utils;
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Threading.Tasks;
   using Xamarin.Forms;

   /// <summary>
   ///    Class ViewUtils.
   /// </summary>
   public static class ViewUtils
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

      /// <summary>
      ///    The ios top margin
      /// </summary>
      public const float IOS_TOP_MARGIN = 40;

      /// <summary>
      ///    The not visible opacity
      /// </summary>
      public const double NOT_VISIBLE_OPACITY = 0;

      /// <summary>
      ///    The visible opacity
      /// </summary>
      public const double VISIBLE_OPACITY = 1;

      /// <summary>
      ///    The ios nested stage reduction
      /// </summary>
      private const float IOS_NESTED_STAGE_REDUCTION = -IOS_TOP_MARGIN;

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
               return originalHeight - 62;

            case Device.Android:
               return originalHeight - 24;
         }

         return originalHeight;
      }

      /// <summary>
      ///    Considers the ios nested stage height hack.
      /// </summary>
      /// <param name="isNestedStage">if set to <c>true</c> [is nested stage].</param>
      /// <returns>System.Single.</returns>
      public static float ConsiderIOSNestedStageHeightHack(bool isNestedStage)
      {
         return isNestedStage && IsIOS() ? IOS_NESTED_STAGE_REDUCTION : 0;
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
         Size               parentViewSize,
         double             width,
         double             height,
         OffScreenPositions position,
         bool               forceLongSize = false,

         // Allows space for a header above a toolbar.
         double additionalTopAllowance = 0
      )
      {
         var adjustedHeight = forceLongSize ? 0 : parentViewSize.Height / 2 - height / 2;
         var adjustedWidth  = forceLongSize ? 0 : parentViewSize.Width  / 2 - width  / 2;

         switch (position)
         {
            case OffScreenPositions.LEFT:
               height = GetForcedHeight(height, forceLongSize, parentViewSize);
               return new Rectangle(-width - 1, adjustedHeight + additionalTopAllowance, width, height - additionalTopAllowance);

            case OffScreenPositions.TOP:
               width = GetForcedWidth(width, forceLongSize, parentViewSize);
               return new Rectangle(adjustedWidth, -height, width, height);

            case OffScreenPositions.RIGHT:
               height = GetForcedHeight(height, forceLongSize, parentViewSize);
               return new Rectangle(parentViewSize.Width + 1, adjustedHeight + additionalTopAllowance, width, height - additionalTopAllowance);

            case OffScreenPositions.BOTTOM:
               width = GetForcedWidth(width, forceLongSize, parentViewSize);
               return new Rectangle(adjustedWidth, parentViewSize.Height + 1, width, height);

            default:
               return default;
         }
      }

      /// <summary>
      ///    Creates the off screen rect with forced side.
      /// </summary>
      /// <param name="parentViewSize">Size of the parent view.</param>
      /// <param name="widthHeight">Height of the width.</param>
      /// <param name="position">The position.</param>
      /// <param name="additionalTopAllowance">The additional top allowance.</param>
      /// <returns>Rectangle.</returns>
      public static Rectangle CreateOffScreenRectWithForcedSide
      (
         Size               parentViewSize,
         double             widthHeight,
         OffScreenPositions position,

         // Allows space for a header
         double additionalTopAllowance = 0
      )
      {
         var rect = CreateOffScreenRect(parentViewSize, widthHeight, widthHeight, position, true, additionalTopAllowance);
         return rect;
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
         Size              parentViewSize,
         double            width,
         double            height,
         OnScreenPositions position,
         Thickness         parentViewPadding = default,
         bool              forceLongSize     = false,

         // Allows space for a header
         double additionalTopAllowance = 0
      )
      {
         Debug.Assert
         (
            !forceLongSize                             ||
            position == OnScreenPositions.LEFT_CENTER  ||
            position == OnScreenPositions.TOP_CENTER   ||
            position == OnScreenPositions.RIGHT_CENTER ||
            position == OnScreenPositions.BOTTOM_CENTER,
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
         width  = Math.Min(width, parentViewSize.Width - parentViewPadding.Left                             - parentViewPadding.Right);
         height = Math.Min(height, parentViewSize.Height - parentViewPadding.Top - parentViewPadding.Bottom - additionalTopAllowance);

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
            case OnScreenPositions.LEFT_CENTER:
               newLeftX = paddingAdjustedLeftXPos;
               newTopY  = verticalCenterY;
               break;

            case OnScreenPositions.TOP_LEFT:
               newLeftX = paddingAdjustedLeftXPos;
               newTopY  = paddingAdjustedTopYPos;
               break;

            case OnScreenPositions.TOP_CENTER:
               newLeftX = horizontalCenterX;
               newTopY  = paddingAdjustedTopYPos;
               break;

            case OnScreenPositions.TOP_RIGHT:
               newLeftX = paddingAdjustedRightXPos;
               newTopY  = paddingAdjustedTopYPos;
               break;

            case OnScreenPositions.RIGHT_CENTER:
               newLeftX = paddingAdjustedRightXPos;
               newTopY  = verticalCenterY;
               break;

            case OnScreenPositions.BOTTOM_LEFT:
               newLeftX = paddingAdjustedLeftXPos;
               newTopY  = paddingAdjustedBottomYPos;
               break;

            case OnScreenPositions.BOTTOM_CENTER:
               newLeftX = horizontalCenterX;
               newTopY  = paddingAdjustedBottomYPos;
               break;

            case OnScreenPositions.BOTTOM_RIGHT:
               newLeftX = paddingAdjustedRightXPos;
               newTopY  = paddingAdjustedBottomYPos;
               break;

            case OnScreenPositions.CENTER:
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
         Size              parentViewSize,
         double            widthHeight,
         OnScreenPositions position,
         Thickness         parentViewPadding = default,

         // Allows space for a header
         double additionalTopAllowance = 0
      )
      {
         var rect = CreateOnScreenRect(parentViewSize, widthHeight, widthHeight, position, parentViewPadding, true, additionalTopAllowance);
         return rect;
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

      /// <summary>
      ///    Fades the in.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <returns>Task.</returns>
      public static async Task FadeIn(this View view)
      {
         await view.FadeTo(VISIBLE_OPACITY).WithoutChangingContext();
      }

      /// <summary>
      ///    Fades the out.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <returns>Task.</returns>
      public static async Task FadeOut(this View view)
      {
         await view.FadeTo(NOT_VISIBLE_OPACITY).WithoutChangingContext();
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

      /// <summary>
      ///    Gets the starting y for runtime platform.
      /// </summary>
      /// <returns>System.Single.</returns>
      public static float GetStartingYForRuntimePlatform()
      {
         return IsIOS() ? IOS_TOP_MARGIN : 0;
      }

      /// <summary>
      ///    Ioses the height hack.
      /// </summary>
      /// <param name="isNested">if set to <c>true</c> [is nested].</param>
      /// <returns>System.Double.</returns>
      public static double IOSHeightHack(bool isNested)
      {
         return Device.RuntimePlatform.IsSameAs(Device.iOS) ? isNested ? 0 : IOS_TOP_MARGIN : 0;
      }

      /// <summary>
      ///    Determines whether this instance is ios.
      /// </summary>
      /// <returns><c>true</c> if this instance is ios; otherwise, <c>false</c>.</returns>
      public static bool IsIOS()
      {
         return Device.RuntimePlatform.IsSameAs(Device.iOS);
      }

      /// <summary>
      ///    Determines whether [is not visible] [the specified view].
      /// </summary>
      /// <param name="view">The view.</param>
      /// <returns><c>true</c> if [is not visible] [the specified view]; otherwise, <c>false</c>.</returns>
      public static bool IsNotVisible(this View view)
      {
         return view.Opacity.IsDifferentThan(VISIBLE_OPACITY);
      }

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

      /// <summary>
      /// 
      /// </summary>
      /// <param name="flowDirection"></param>
      /// <returns></returns>
      public static OffScreenPositions ToOffScreenPosition(this SubStageFlowDirections flowDirection)
      {
         switch (flowDirection)
         {
            case SubStageFlowDirections.BOTTOM_TO_TOP: return OffScreenPositions.BOTTOM;
            case SubStageFlowDirections.LEFT_TO_RIGHT:   return OffScreenPositions.LEFT;
            case SubStageFlowDirections.TOP_TO_BOTTOM:    return OffScreenPositions.TOP;
            case SubStageFlowDirections.RIGHT_TO_LEFT:  return OffScreenPositions.RIGHT;
         }

         return OffScreenPositions.NONE;
      }

      /// <summary>
      ///    Converts to onscreenposition.
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
      ///    Gets the height of the forced.
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
      ///    Gets the width of the forced.
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
   }
}