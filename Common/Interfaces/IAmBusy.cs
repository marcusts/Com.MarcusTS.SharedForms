// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, IAmBusy.cs, is a part of a program called AccountViewMobile.
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

   /// <summary>
   /// The IsBusy interface.
   /// </summary>
   public interface IAmBusy
   {
      /// <summary>
      /// Occurs when [is busy changed].
      /// </summary>
      event EventUtils.GenericDelegate<IAmBusy> IsBusyChanged;

      /// <summary>
      /// Gets a value indicating whether is busy.
      /// </summary>
      /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
      bool IsBusy { get; }

      /// <summary>
      /// Gets the is busy message.
      /// </summary>
      /// <value>The is busy message.</value>
      string IsBusyMessage { get; }
   }
}
