#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, OrientationService.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Services
{
   using Notifications;

   /// <summary>
   /// Class OrientationService.
   /// </summary>
   public static class OrientationService
   {
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

      /// <summary>
      /// Handles the device size changed.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="message">The message.</param>
      public static void HandleDeviceSizeChanged
      (
         object                        sender,
         LocalDeviceSizeChangedMessage message
      )
      {
         // Need the initial orientation
         ScreenWidth  = message.Payload.ScreenWidth;
         ScreenHeight = message.Payload.ScreenHeight;
         IsLandscape  = ScreenWidth > ScreenHeight;

         // Notify the app classes about this change.
         FormsMessengerUtils.Send(new BroadcastDeviceSizeChangedMessage(ScreenWidth, ScreenHeight));
      }
   }
}