#define FORCE_NULLABLE_DOUBLE

namespace Com.MarcusTS.SharedForms.Common.Converters
{
   using System;
   using System.Globalization;
   using Behaviors;
   using SharedUtils.Utils;
   using ViewModels;
   using Xamarin.Forms;

   public class StringToNumericConverter : IValueConverter
   {
      public Func<string, object> ConvertBackFunc { get; set; }
      public string StringFormat { get; set; }
      public ValidationTypes ValidationType { get; set; }

      // From numeric to string (for editing)
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return ConvertValueToString(value);
      }

      // From string to numeric (for the view model)
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

      private string ConvertValueToString(object value)
      {
         return value.IsNullOrDefault() ? "" : NumericEntryValidationBehavior.StripStringFormatCharacters(value.ToString(), StringFormat, ValidationType);
      }
   }
}