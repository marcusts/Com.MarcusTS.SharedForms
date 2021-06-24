
namespace Com.MarcusTS.SharedForms.Common.Converters
{
   using System;
   using System.Globalization;
   using Behaviors;
   using SharedUtils.Utils;
   using ViewModels;
   using Xamarin.Forms;

   public class NotEmptyStringToBoolConverter : OneWayConverter<string, bool>
   {
      public static readonly NotEmptyStringToBoolConverter Instance = new NotEmptyStringToBoolConverter();

      protected override bool Convert(string value, object parameter)
      {
         return value.IsNotEmpty();
      }
   }
}