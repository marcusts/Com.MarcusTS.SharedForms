// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ImageLabelButtonStyle.cs, is a part of a program called AccountViewMobile.
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
   using System;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IImageLabelButtonStyle
   /// </summary>
   public interface IImageLabelButtonStyle
   {
      /// <summary>
      /// Gets or sets the button style.
      /// </summary>
      /// <value>The button style.</value>
      Style ButtonStyle { get; set; }

      /// <summary>
      /// Gets or sets the button text.
      /// </summary>
      /// <value>The button text.</value>
      string ButtonText { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [get image from resource].
      /// </summary>
      /// <value><c>true</c> if [get image from resource]; otherwise, <c>false</c>.</value>
      bool GetImageFromResource { get; set; }

      /// <summary>
      /// Gets or sets the image file path.
      /// </summary>
      /// <value>The image file path.</value>
      string ImageFilePath { get; set; }

      /// <summary>
      /// Gets or sets the type of the image resource class.
      /// </summary>
      /// <value>The type of the image resource class.</value>
      Type ImageResourceClassType { get; set; }

      /// <summary>
      /// Gets or sets the state of the internal button.
      /// </summary>
      /// <value>The state of the internal button.</value>
      string InternalButtonState { get; set; }

      /// <summary>
      /// Gets or sets the label style.
      /// </summary>
      /// <value>The label style.</value>
      Style LabelStyle { get; set; }
   }

   /// <summary>
   /// Class ImageLabelButtonStyle.
   /// Implements the <see cref="IImageLabelButtonStyle" />
   /// </summary>
   /// <seealso cref="IImageLabelButtonStyle" />
   public class ImageLabelButtonStyle : IImageLabelButtonStyle
   {
      /// <summary>
      /// Gets or sets the button style.
      /// </summary>
      /// <value>The button style.</value>
      public Style ButtonStyle { get; set; }

      /// <summary>
      /// Gets or sets the button text.
      /// </summary>
      /// <value>The button text.</value>
      public string ButtonText { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [get image from resource].
      /// </summary>
      /// <value><c>true</c> if [get image from resource]; otherwise, <c>false</c>.</value>
      public bool GetImageFromResource { get; set; }

      /// <summary>
      /// Gets or sets the image file path.
      /// </summary>
      /// <value>The image file path.</value>
      public string ImageFilePath { get; set; }

      /// <summary>
      /// Gets or sets the type of the image resource class.
      /// </summary>
      /// <value>The type of the image resource class.</value>
      public Type ImageResourceClassType { get; set; }

      /// <summary>
      /// Gets or sets the state of the internal button.
      /// </summary>
      /// <value>The state of the internal button.</value>
      public string InternalButtonState { get; set; }

      /// <summary>
      /// Gets or sets the label style.
      /// </summary>
      /// <value>The label style.</value>
      public Style LabelStyle { get; set; }
   }
}
