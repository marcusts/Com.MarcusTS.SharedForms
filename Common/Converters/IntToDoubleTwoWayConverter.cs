#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, IntToDoubleTwoWayConverter.cs, is a part of a program called AccountViewMobile.
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
   using SharedUtils.Utils;
   using System;
   using System.Globalization;
   using Xamarin.Forms;

   /// <summary>
   /// Class IntToDoubleTwoWayConverter.
   /// Implements the <see cref="Xamarin.Forms.IValueConverter" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.IValueConverter" />
   public class IntToDoubleTwoWayConverter : IValueConverter
   {
      /// <summary>
      /// The instance
      /// </summary>
      public static readonly IntToDoubleTwoWayConverter INSTANCE = new IntToDoubleTwoWayConverter();

      // From int (view model) to double (view)
      /// <summary>
      /// Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
      /// </summary>
      /// <param name="value">The value to convert.</param>
      /// <param name="targetType">The type to which to convert the value.</param>
      /// <param name="parameter">A parameter to use during the conversion.</param>
      /// <param name="culture">The culture to use during the conversion.</param>
      /// <returns>To be added.</returns>
      /// <remarks>To be added.</remarks>
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value is int valueAsInt)
         {
            return (double) valueAsInt;
         }

         return default;
      }

      // From double (view) to int (view model)
      /// <summary>
      /// Converts the back.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <param name="targetType">Type of the target.</param>
      /// <param name="parameter">The parameter.</param>
      /// <param name="culture">The culture.</param>
      /// <returns>System.Object.</returns>
      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value is double valueAsDouble)
         {
            return valueAsDouble.ToRoundedInt();
         }

         return default;
      }
   }
}