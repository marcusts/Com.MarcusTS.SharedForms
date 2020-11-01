// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, IProvidePageEvents.cs, is a part of a program called AccountViewMobile.
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
   using System;

   /// <summary>
   /// Interface IProvidePageEvents
   /// </summary>
   public interface IProvidePageEvents
   {
      /// <summary>
      /// Regrettable use of object; we could type-cast, but than makes it difficult to pass
      /// IProvidePageEvents at lower levels without omniscient knowledge off the parent page type.
      /// </summary>
      /// <value>The get event broadcaster.</value>
      /// <remarks>The function is better than a property when there is a chance of nesting to view inside
      /// view, etc. Whenever this property is assigned, it will always seek a legal value.
      /// Otherwise, an assignment might begin with null and then never change. The root of these
      /// events is a known, valid page that should be seekable by any deriver or nested deriver.</remarks>
      Func<object> GetEventBroadcaster { get; }
   }
}
