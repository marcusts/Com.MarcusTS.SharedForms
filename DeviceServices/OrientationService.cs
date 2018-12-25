// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="OrientationService.cs" company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.DeviceServices
{
   using Utils;

   /// <summary>
   /// Class OrientationService.
   /// </summary>
   public static class OrientationService
   {
      #region Public Methods

      /// <summary>
      /// Handles the device size changed.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="message">The message.</param>
      public static void HandleDeviceSizeChanged(object                        sender,
                                                 LocalDeviceSizeChangedMessage message)
      {
         // Need the initial orientation
         ScreenWidth  = message.Payload.ScreenWidth;
         ScreenHeight = message.Payload.ScreenHeight;
         IsLandscape  = ScreenWidth > ScreenHeight;

         // Notify the app classes about this change.
         FormsMessengerUtils.Send(new BroadcastDeviceSizeChangedMessage(ScreenWidth, ScreenHeight));
      }

      #endregion Public Methods

      #region Public Properties

      /// <summary>
      /// Gets or sets a value indicating whether this instance is landscape.
      /// </summary>
      /// <value><c>true</c> if this instance is landscape; otherwise, <c>false</c>.</value>
      public static bool IsLandscape { get; set; }

      /// <summary>
      /// Gets or sets the height of the screen.
      /// </summary>
      /// <value>The height of the screen.</value>
      public static float ScreenHeight { get; set; }

      /// <summary>
      /// Gets or sets the width of the screen.
      /// </summary>
      /// <value>The width of the screen.</value>
      public static float ScreenWidth { get; set; }

      #endregion Public Properties
   }
}