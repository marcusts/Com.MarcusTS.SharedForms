// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, NotificationUtils.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Notifications
{
   /// <summary>
   /// Class NotificationUtils.
   /// </summary>
   public static class NotificationUtils
   {
      /// <summary>
      /// The body
      /// </summary>
      public const string BODY = "body";

      /// <summary>
      /// The identifier
      /// </summary>
      public const string ID = "id";

      /// <summary>
      /// Sets various flags based on the id so the app can start and go to a given page.
      /// </summary>
      /// <param name="id">The identifier.</param>
      public static void HandleBackgroundNotification(string id)
      {
      }

      /// <summary>
      /// Determine if this is a normal push notification or an error.
      /// </summary>
      /// <param name="id">The identifier.</param>
      /// <param name="body">The body.</param>
      public static void HandleForegroundNotification
      (
         string id,
         string body
      )
      {
      }
   }
}
