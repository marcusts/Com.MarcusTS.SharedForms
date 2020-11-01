// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ICanRequestRemeasure.cs, is a part of a program called AccountViewMobile.
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
   /// <summary>
   /// Interface ICanRequestRemeasure
   /// </summary>
   public interface ICanRequestRemeasure
   {
      /// <summary>
      /// Forces the cell to recalculate its length and girth. Since one of these might be auto-calc, that value can
      /// easily become inaccurate over time because we constantly accept the maximum of any new value. If a row or
      /// column is deleted, this value will definitely be in question.
      /// </summary>
      void RequestRemeasure();
   }
}
