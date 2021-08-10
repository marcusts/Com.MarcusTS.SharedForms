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

      public const float DEFAULT_CORNER_RADIUS_FACTOR = 0.06f;
      
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

      public const           int       DEFAULT_ANIMATE_IN_DELAY_MILLISECONDS = 25;
      public static readonly double    MARGIN_SPACING_SINGLE_FACTOR          = 10.0.AdjustForOsAndDevice();
      public static readonly double MARGIN_SPACING_DOUBLE_FACTOR = MARGIN_SPACING_SINGLE_FACTOR * 2;
      public static readonly Thickness DEFAULT_STACK_LAYOUT_MARGIN           = new Thickness(MARGIN_SPACING_SINGLE_FACTOR);
      public static readonly double    DEFAULT_STACK_LAYOUT_SPACING          = MARGIN_SPACING_SINGLE_FACTOR;

      /// <summary>
      /// The not visible opacity
      /// </summary>
      public const double NOT_VISIBLE_OPACITY = 0;

      /// <summary>
      /// The visible opacity
      /// </summary>
      public const double VISIBLE_OPACITY = 1;

      public const double MODERATE_OPACITY = 1.0/2.0;

      public static readonly Color PALE_GRAY = Color.FromRgb(240, 240, 240);
   }
}
