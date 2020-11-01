// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, DateTimeToPlainEnglishConverter.cs, is a part of a program called AccountViewMobile.
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
   using SharedUtils.Utils;
   using System;

   /// <summary>
   /// Class DateTimeToPlainEnglishConverter.
   /// Implements the <see cref="string" />
   /// </summary>
   /// <seealso cref="string" />
   public class DateTimeToPlainEnglishConverter : OneWayConverter<DateTime?, string>
   {
      /// <summary>
      /// The instance
      /// </summary>
      public static readonly DateTimeToPlainEnglishConverter INSTANCE = new DateTimeToPlainEnglishConverter();

      /// <summary>
      /// The ago
      /// </summary>
      private const string AGO = " ago";

      /// <summary>
      /// Converts the specified value.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <param name="parameter">The parameter.</param>
      /// <returns>System.String.</returns>
      protected override string Convert(DateTime? value, object parameter)
      {
         if (!value.HasValue)
         {
            return "";
         }

         var timePassed = DateTime.Now.Subtract(value.GetValueOrDefault());

         // Start with the broadest cases
         if (timePassed.Days > 0)
         {
            var yearsRaw = timePassed.Days / 365.25;
            var years = Math.Floor(yearsRaw).ToRoundedInt();
            var months = Math.Floor(yearsRaw * 12).ToRoundedInt();

            if (years > 0)
            {
               return $"{years:0} year(s)" + AGO;
            }

            if (months > 0)
            {
               return $"{months:0} month(s)" + AGO;
            }

            return $"{timePassed.Days:0} day(s)" + AGO;
         }

         if (timePassed.Hours > 0)
         {
            return $"{timePassed.Hours:0} hour(s)" + AGO;
         }

         if (timePassed.Minutes > 0)
         {
            return $"{timePassed.Minutes:0} minutes(s)" + AGO;
         }

         return "Just now";
      }
   }
}
