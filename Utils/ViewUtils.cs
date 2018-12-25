// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="ViewUtils.cs" company="Marcus Technical Services, Inc.">
//     Copyright @2018 Marcus Technical Services, Inc.
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

namespace Com.MarcusTS.SharedForms.Utils
{
   using Views.Controls;
   using Xamarin.Forms;

   /// <summary>
   /// Class ViewUtils.
   /// </summary>
   public static class ViewUtils
   {
      #region Private Fields

      /// <summary>
      /// The normal button font size
      /// </summary>
      private const double NORMAL_BUTTON_FONT_SIZE = 20;

      /// <summary>
      /// The selected button font size
      /// </summary>
      private const double SELECTED_BUTTON_FONT_SIZE = NORMAL_BUTTON_FONT_SIZE * 1.1;

      // Xamarin bug -- calculation is pristine, but the pages do not align properly on-screen
      /// <summary>
      /// The slop
      /// </summary>
      private const double SLOP = 6;

      /// <summary>
      /// The selected image button border width
      /// </summary>
      private static readonly double SELECTED_IMAGE_BUTTON_BORDER_WIDTH = 2;

      #endregion Private Fields

      #region Public Methods

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
      /// Gets the width of the adjusted screen.
      /// </summary>
      /// <param name="viewIdx">Index of the view.</param>
      /// <param name="currentWidth">Width of the current.</param>
      /// <returns>System.Double.</returns>
      public static double GetAdjustedScreenWidth(int    viewIdx,
                                                  double currentWidth)
      {
         var properX = -(viewIdx * currentWidth);
         var sloppyX = properX - viewIdx * SLOP;

         return sloppyX;
      }

      /// <summary>
      /// Sets the border selection styles.
      /// </summary>
      /// <param name="retButton">The ret button.</param>
      public static void SetBorderSelectionStyles(this CustomImageButton retButton)
      {
         // No disabled image treatment as of yet

         retButton.DeselectedButtonStyle = CustomImageButton.CreateViewButtonStyle(Color.Transparent);
         retButton.SelectedButtonStyle =
            CustomImageButton.CreateViewButtonStyle(Color.Transparent, SELECTED_IMAGE_BUTTON_BORDER_WIDTH);
         retButton.DisabledButtonStyle = CustomImageButton.CreateViewButtonStyle(Color.Transparent);
      }

      /// <summary>
      /// Sets the reverse styles.
      /// </summary>
      /// <param name="retButton">The ret button.</param>
      public static void SetReverseStyles(this LabelButton retButton)
      {
         retButton.DeselectedLabelStyle =
            LabelButton.CreateLabelStyle(Color.Black, NORMAL_BUTTON_FONT_SIZE, FontAttributes.None);
         retButton.SelectedLabelStyle =
            LabelButton.CreateLabelStyle(Color.White, SELECTED_BUTTON_FONT_SIZE, FontAttributes.Bold);
         retButton.DisabledLabelStyle =
            LabelButton.CreateLabelStyle(Color.Gray, NORMAL_BUTTON_FONT_SIZE, FontAttributes.None);

         retButton.DeselectedButtonStyle = LabelButton.CreateViewButtonStyle(Color.Transparent);
         retButton.SelectedButtonStyle   = LabelButton.CreateViewButtonStyle(Color.Black);
         retButton.DisabledButtonStyle   = LabelButton.CreateViewButtonStyle(Color.Transparent);
      }

      #endregion Public Methods
   }
}