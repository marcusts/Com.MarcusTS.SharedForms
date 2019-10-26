#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, StringToDateTimeConverter.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Converters
{
   using System;

   /// <summary>
   /// Class StringToDateTimeConverter.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Converters.OneWayConverter{System.String, System.DateTime?}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Converters.OneWayConverter{System.String, System.DateTime?}" />
   public class StringToDateTimeConverter : OneWayConverter<string, DateTime?>
   {
      /// <summary>
      /// The string to date time converter static
      /// </summary>
      public static readonly StringToDateTimeConverter
         StringToDateTimeConverterStatic = new StringToDateTimeConverter();

      /// <summary>
      /// Converts the specified value.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <param name="parameter">The parameter.</param>
      /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
      protected override DateTime? Convert(string value, object parameter)
      {
         if (DateTime.TryParse(value, out var dateTime))
         {
            return dateTime;
         }

         return null;
      }
   }
}