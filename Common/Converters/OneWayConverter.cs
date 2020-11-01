// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, OneWayConverter.cs, is a part of a program called AccountViewMobile.
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
   using System.Globalization;
   using Xamarin.Forms;

   /// <summary>
   /// Class OneWayConverter.
   /// Implements the <see cref="Xamarin.Forms.IValueConverter" />
   /// </summary>
   /// <typeparam name="FromT">The type of from t.</typeparam>
   /// <typeparam name="ToT">The type of to t.</typeparam>
   /// <seealso cref="Xamarin.Forms.IValueConverter" />
   public abstract class OneWayConverter<FromT, ToT> : IValueConverter
   {
      /// <summary>
      /// The failed default value
      /// </summary>
      public ToT FailedDefaultValue = default;

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
         if (value is FromT valueAsFromT)
         {
            return Convert(valueAsFromT, parameter);
         }

         return FailedDefaultValue;
      }

      /// <summary>
      /// Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
      /// </summary>
      /// <param name="value">The value to convert.</param>
      /// <param name="targetType">The type to which to convert the value.</param>
      /// <param name="parameter">A parameter to use during the conversion.</param>
      /// <param name="culture">The culture to use during the conversion.</param>
      /// <returns>To be added.</returns>
      /// <remarks>To be added.</remarks>
      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         ErrorUtils.ThrowArgumentError(nameof(OneWayConverter<FromT, ToT>) + ": two-way bindings not supported.");

         return FailedDefaultValue;
      }

      /// <summary>
      /// Converts the easily.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <returns>System.Object.</returns>
      public object ConvertEasily(FromT value)
      {
         return Convert(value, null);
      }

      /// <summary>
      /// Converts the specified value.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <param name="parameter">The parameter.</param>
      /// <returns>ToT.</returns>
      protected abstract ToT Convert(FromT value, object parameter);
   }
}
