
namespace Com.MarcusTS.SharedForms.Common.Converters
{
   using System;
   using System.Globalization;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   public abstract class OneWayConverter<FromT, ToT> : IValueConverter
   {
      public ToT FailedDefaultValue = default;

      protected abstract ToT Convert(FromT value, object parameter);

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value is FromT valueAsFromT)
         {
            return Convert(valueAsFromT, parameter);                 
         }

         return FailedDefaultValue;
      }

      public object ConvertEasily(FromT value)
      {
         return Convert(value, null);
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         ErrorUtils.ThrowArgumentError(nameof(OneWayConverter<FromT, ToT>) + ": two-way bindings not supported.");

         return FailedDefaultValue;
      }
   }
}
