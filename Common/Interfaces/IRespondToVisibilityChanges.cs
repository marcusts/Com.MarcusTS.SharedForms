﻿// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, IRespondToVisibilityChanges.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Interfaces
{
   using SharedUtils.Utils;
   using System.Threading.Tasks;

   /// <summary>
   /// Interface IRespondToVisibilityChanges
   /// </summary>
   public interface IRespondToVisibilityChanges
   {
      /// <summary>
      /// Occurs when [is visible to user changed].
      /// </summary>
      event EventUtils.GenericDelegate<bool> IsVisibleToUserChanged;

      /// <summary>
      /// Gets a value indicating whether this instance is visible to user.
      /// </summary>
      /// <value><c>true</c> if this instance is visible to user; otherwise, <c>false</c>.</value>
      bool IsVisibleToUser { get; }

      /// <summary>
      /// Afters the user visibility changed.
      /// </summary>
      /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
      /// <returns>Task.</returns>
      Task AfterUserVisibilityChanged(bool isVisible);

      /// <summary>
      /// Sets the is visible to user.
      /// </summary>
      /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
      /// <param name="forceSet">if set to <c>true</c> [force set].</param>
      /// <returns>Task.</returns>
      Task SetIsVisibleToUser(bool isVisible, bool forceSet = false);
   }
}
