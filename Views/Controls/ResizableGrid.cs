// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ResizableGrid.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   /// Class ResizableGrid.
   /// Implements the <see cref="Xamarin.Forms.Grid" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Grid" />
   public class ResizableGrid : Grid
   {
      /// <summary>
      /// Occurs when [width changed].
      /// </summary>
      public event EventUtils.GenericDelegate<double> WidthChanged;

      /// <summary>
      /// Called when [size allocated].
      /// </summary>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      protected override void OnSizeAllocated(double width, double height)
      {
         base.OnSizeAllocated(width, height);
         WidthChanged?.Invoke(width);
      }
   }
}
