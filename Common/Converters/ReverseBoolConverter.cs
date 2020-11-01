// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ReverseBoolConverter.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Converters
{
   /// <summary>
   /// Class ReverseBoolConverter.
   /// Implements the <see cref="OneWayConverter{FromT,ToT}.Boolean, System.Boolean}" />
   /// </summary>
   /// <seealso cref="OneWayConverter{FromT,ToT}.Boolean, System.Boolean}" />
   public class ReverseBoolConverter : OneWayConverter<bool, bool>
   {
      /// <summary>
      /// The instance
      /// </summary>
      public static readonly ReverseBoolConverter INSTANCE = new ReverseBoolConverter();

      /// <summary>
      /// Converts the specified value.
      /// </summary>
      /// <param name="value">if set to <c>true</c> [value].</param>
      /// <param name="parameter">The parameter.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      protected override bool Convert(bool value, object parameter)
      {
         return !value;
      }
   }
}
