#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, IMenuNavigationState.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Interfaces
{
   /// <summary>
   /// A view model base for pages.
   /// </summary>
   public interface IMenuNavigationState
   {
      /// <summary>
      /// The app state to navigate to
      /// </summary>
      /// <value>The state of the application.</value>
      string AppState { get; }

      /// <summary>
      /// Gets the menu order.
      /// </summary>
      /// <value>The menu order.</value>
      int MenuOrder { get; }

      /// <summary>
      /// The menu title
      /// </summary>
      /// <value>The menu title.</value>
      string MenuTitle { get; }

      /// <summary>
      /// The page or view title (default: centered on the page)
      /// </summary>
      /// <value>The view title.</value>
      string ViewTitle { get; }
   }
}