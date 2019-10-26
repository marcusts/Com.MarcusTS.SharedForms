#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, StringToNumericConverter.cs, is a part of a program called AccountViewMobile.
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

#define FORCE_NULLABLE_DOUBLE

namespace Com.MarcusTS.SharedForms.Common.Converters
{
   using Behaviors;
   using SharedUtils.Utils;
   using System;
   using System.Globalization;
   using Xamarin.Forms;

   /// <summary>
   /// Class StringToNumericConverter.
   /// Implements the <see cref="Xamarin.Forms.IValueConverter" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.IValueConverter" />
   public class StringToNumericConverter : IValueConverter
   {
      /// <summary>
      /// Gets or sets the convert back function.
      /// </summary>
      /// <value>The convert back function.</value>
      public Func<string, object> ConvertBackFunc { get; set; }
      /// <summary>
      /// Gets or sets the string format.
      /// </summary>
      /// <value>The string format.</value>
      public string               StringFormat    { get; set; }
      /// <summary>
      /// Gets or sets the type of the validation.
      /// </summary>
      /// <value>The type of the validation.</value>
      public ValidationTypes      ValidationType  { get; set; }

      // From numeric to string (for editing)
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
         return ConvertValueToString(value);
      }

      // From string to numeric (for the view model)
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
#if FORCE_NULLABLE_DOUBLE
         var valueEntered = ConvertValueToString(value);
         if (valueEntered.IsNotEmpty() && double.TryParse(valueEntered, out var valueAsDouble))
         {
            return valueAsDouble as double?;
         }
#else
         if (ConvertBackFunc.IsNotNullOrDefault())
         {
            return ConvertBackFunc(ConvertValueToString(value));
         }
#endif

         return default;
      }

      /// <summary>
      /// Converts the value to string.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <returns>System.String.</returns>
      private string ConvertValueToString(object value)
      {
         return value.IsNullOrDefault()
                   ? ""
                   : NumericEntryValidationBehavior.StripStringFormatCharacters(
                      value.ToString(), StringFormat, ValidationType);
      }
   }
}