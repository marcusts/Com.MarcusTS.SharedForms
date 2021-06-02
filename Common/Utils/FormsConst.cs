namespace Com.MarcusTS.SharedForms.Common.Utils
{
   using Xamarin.Forms;

   public static class FormsConst
   {
      public const string SPACE = " ";

      /// <summary>The haptic vibration milliseconds</summary>
      public const float HAPTIC_VIBRATION_MILLISECONDS = 250;

      /// <summary>The height property name</summary>
      public const string HEIGHT_PROPERTY_NAME = "Height";


      /// <summary>The width property name</summary>
      public const string WIDTH_PROPERTY_NAME = "Width";

      /// <summary>The x property name</summary>
      public const string X_PROPERTY_NAME = "X";

      /// <summary>The y property name</summary>
      public const string Y_PROPERTY_NAME = "Y";

      public const int STANDARD_KEYBOARD_NUMBER = 0;

      public static readonly Keyboard STANDARD_KEYBOARD = Keyboard.Create(STANDARD_KEYBOARD_NUMBER);
      public static readonly uint BUTTON_BOUNCE_MILLISECONDS = 75;
      
      public static readonly double EDITABLE_VIEW_FONT_SIZE = Device.GetNamedSize(NamedSize.Small, typeof(View));

      /// <summary>
      /// <summary>
      /// The default corner radius factor
      /// </summary>
      public const float DEFAULT_CORNER_RADIUS_FACTOR = 0.04f;
      /// The default corner radius fixed
      /// </summary>
      public static float DEFAULT_CORNER_RADIUS_FIXED = 6;
      public const double DEFAULT_SHAPE_VIEW_RADIUS = 6;

      /// <summary>
      /// The button radius factor
      /// </summary>
      public const double BUTTON_RADIUS_FACTOR = 0.15f;

      public const double NEUTRAL_WIDTH_HEIGHT = -1;

      /// <summary>
      ///    The default image suffix
      /// </summary>
      public const string DEFAULT_IMAGE_SUFFIX = ".png";
   }
}
