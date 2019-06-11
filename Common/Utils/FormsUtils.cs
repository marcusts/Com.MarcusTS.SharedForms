// *********************************************************************************
// <copyright file=FormsUtils.cs company="Marcus Technical Services, Inc.">
//     Copyright @2019 Marcus Technical Services, Inc.
// </copyright>
//
// MIT License
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// *********************************************************************************

namespace Com.MarcusTS.SharedForms.Common.Utils
{
   using Com.MarcusTS.SharedUtils.Utils;
   using Plugin.Vibrate;
   using System;
   using System.Linq;
   using System.Reflection;
   using System.Threading.Tasks;
   using Xamarin.Forms;

   /// <summary>
   ///    Class FormsUtils.
   /// </summary>
   public static class FormsUtils
   {
      /// <summary>
      ///    The false string
      /// </summary>
      public const string FALSE_STR = "false";

      /// <summary>
      ///    The true string
      /// </summary>
      public const string TRUE_STR = "true";

      /// <summary>
      ///    The button radius factor
      /// </summary>
      internal const double BUTTON_RADIUS_FACTOR = 0.15f;

      /// <summary>
      ///    The default text size
      /// </summary>
      internal const float DEFAULT_TEXT_SIZE = 20;

      /// <summary>
      ///    The haptic vibration milliseconds
      /// </summary>
      private const float HAPTIC_VIBRATION_MILLISECONDS = 250;

      /// <summary>
      ///    The height property name
      /// </summary>
      private const string HEIGHT_PROPERTY_NAME = "Height";

      /// <summary>
      ///    The normal button font size
      /// </summary>
      private const double NORMAL_BUTTON_FONT_SIZE = 20;

      /// <summary>
      ///    The selected button font size
      /// </summary>
      private const double SELECTED_BUTTON_FONT_SIZE = NORMAL_BUTTON_FONT_SIZE * 1.1;

      // Xamarin bug -- calculation is pristine, but the pages do not align properly on-screen
      /// <summary>
      ///    The slop
      /// </summary>
      private const double SLOP = 6;

      /// <summary>
      ///    The width property name
      /// </summary>
      private const string WIDTH_PROPERTY_NAME = "Width";

      /// <summary>
      ///    The x property name
      /// </summary>
      private const string X_PROPERTY_NAME = "X";

      /// <summary>
      ///    The y property name
      /// </summary>
      private const string Y_PROPERTY_NAME = "Y";

      /// <summary>
      ///    The major button height
      /// </summary>
      internal static readonly double MAJOR_BUTTON_HEIGHT = 45.0;

      /// <summary>
      ///    The major button width
      /// </summary>
      internal static readonly double MAJOR_BUTTON_WIDTH = 120.0;

      /// <summary>
      ///    The selected image button border width
      /// </summary>
      private static readonly double SELECTED_IMAGE_BUTTON_BORDER_WIDTH = 2;

      /// <summary>
      ///    Adds the animation and haptic feedback.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="animate">if set to <c>true</c> [animate].</param>
      /// <param name="vibrate">if set to <c>true</c> [vibrate].</param>
      /// <returns>Task.</returns>
      public static async Task AddAnimationAndHapticFeedback
      (
         this View view,
         bool      animate = true,
         bool      vibrate = true
      )
      {
         if (animate)
         {
            await view.ScaleTo(0.95, 50, Easing.CubicOut).WithoutChangingContext();
            await view.ScaleTo(1, 50, Easing.CubicIn).WithoutChangingContext();
         }

         if (vibrate)
         {
            var v = CrossVibrate.Current;
            v.Vibration(TimeSpan.FromMilliseconds(HAPTIC_VIBRATION_MILLISECONDS));
         }
      }

      /// <summary>
      ///    Adds the automatic column.
      /// </summary>
      /// <param name="grid">The grid.</param>
      public static void AddAutoColumn(this Grid grid)
      {
         grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
      }

      /// <summary>
      ///    Adds the automatic row.
      /// </summary>
      /// <param name="grid">The grid.</param>
      public static void AddAutoRow(this Grid grid)
      {
         grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
      }

      /// <summary>
      ///    Adds the fixed column.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="width">The width.</param>
      public static void AddFixedColumn
      (
         this Grid grid,
         double    width
      )
      {
         grid.ColumnDefinitions.Add(new ColumnDefinition { Width = width });
      }

      /// <summary>
      ///    Adds the fixed row.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="height">The height.</param>
      public static void AddFixedRow
      (
         this Grid grid,
         double    height
      )
      {
         grid.RowDefinitions.Add(new RowDefinition { Height = height });
      }

      /// <summary>
      ///    Adds the star column.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="factor">The factor.</param>
      public static void AddStarColumn
      (
         this Grid grid,
         double    factor = 1
      )
      {
         grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(factor, GridUnitType.Star) });
      }

      /// <summary>
      ///    Adds the star row.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="factor">The factor.</param>
      public static void AddStarRow
      (
         this Grid grid,
         double    factor = 1
      )
      {
         grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(factor, GridUnitType.Star) });
      }

      /// <summary>
      ///    Boundses the are valid and have changed.
      /// </summary>
      /// <param name="bounds">The bounds.</param>
      /// <param name="propName">Name of the property.</param>
      /// <param name="lastBounds">The last bounds.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool BoundsAreValidAndHaveChanged
      (
         this Rectangle bounds,
         string         propName,
         Rectangle      lastBounds
      )
      {
         return (propName.IsSameAs(WIDTH_PROPERTY_NAME) || propName.IsSameAs(HEIGHT_PROPERTY_NAME) || propName.IsSameAs(X_PROPERTY_NAME) || propName.IsSameAs(Y_PROPERTY_NAME)) && bounds.Width > 0 && bounds.Height > 0 && bounds.IsDifferentThan(lastBounds);
      }

      /// <summary>
      ///    Clears the completely.
      /// </summary>
      /// <param name="grid">The grid.</param>
      public static void ClearCompletely(this Grid grid)
      {
         grid.Children.Clear();
         grid.ColumnDefinitions.Clear();
         grid.RowDefinitions.Clear();
      }

      /// <summary>
      ///    Creates the relative overlay.
      /// </summary>
      /// <param name="layout">The layout.</param>
      /// <param name="viewToAdd">The view to add.</param>
      public static void CreateRelativeOverlay
      (
         this RelativeLayout layout,
         View                viewToAdd
      )
      {
         layout.Children.Add(
            viewToAdd, Constraint.Constant(0), Constraint.Constant(0),
            Constraint.RelativeToParent(parent => parent.Width),
            Constraint.RelativeToParent(parent => parent.Height));
      }

      /// <summary>
      ///    Enums the try parse.
      /// </summary>
      /// <typeparam name="EnumT">The type of the enum t.</typeparam>
      /// <param name="input">The input.</param>
      /// <param name="theEnum">The enum.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      public static bool EnumTryParse<EnumT>
      (
         string    input,
         out EnumT theEnum
      )
      {
         foreach (var enumName in Enum.GetNames(typeof(EnumT)))
         {
            if (enumName.IsSameAs(input))
            {
               theEnum = (EnumT) Enum.Parse(typeof(EnumT), input, true);
               return true;
            }
         }

         theEnum = default;

         return false;
      }

      /// <summary>
      ///    Forces the style.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="style">The style.</param>
      public static void ForceStyle
      (
         this View view,
         Style     style
      )
      {
         if (style == null || style.Setters.IsEmpty())
         {
            return;
         }

         for (var setterIdx = 0; setterIdx < style.Setters.Count; setterIdx++)
         {
            view.SetValue(style.Setters[setterIdx].Property, style.Setters[setterIdx].Value);
         }
      }

      /// <summary>
      ///    Gets the width of the adjusted screen.
      /// </summary>
      /// <param name="viewIdx">Index of the view.</param>
      /// <param name="currentWidth">Width of the current.</param>
      /// <returns>System.Double.</returns>
      public static double GetAdjustedScreenWidth
      (
         int    viewIdx,
         double currentWidth
      )
      {
         var properX = -(viewIdx * currentWidth);
         var sloppyX = properX - viewIdx * SLOP;

         return sloppyX;
      }

      /// <summary>
      ///    Gets the expanding absolute layout.
      /// </summary>
      /// <returns>AbsoluteLayout.</returns>
      public static AbsoluteLayout GetExpandingAbsoluteLayout()
      {
         return new AbsoluteLayout
         {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions   = LayoutOptions.FillAndExpand,
            BackgroundColor   = Color.Transparent
         };
      }

      /// <summary>
      ///    Gets the expanding grid.
      /// </summary>
      /// <returns>Grid.</returns>
      public static Grid GetExpandingGrid()
      {
         return new Grid
         {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions   = LayoutOptions.FillAndExpand,
            BackgroundColor   = Color.Transparent
         };
      }

      /// <summary>
      ///    Gets the expanding relative layout.
      /// </summary>
      /// <returns>RelativeLayout.</returns>
      public static RelativeLayout GetExpandingRelativeLayout()
      {
         return new RelativeLayout
         {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions   = LayoutOptions.FillAndExpand,
            BackgroundColor   = Color.Transparent
         };
      }

      /// <summary>
      ///    Gets the expanding stack layout.
      /// </summary>
      /// <returns>StackLayout.</returns>
      public static StackLayout GetExpandingStackLayout()
      {
         return new StackLayout
         {
            VerticalOptions   = LayoutOptions.StartAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor   = Color.Transparent,
            Orientation       = StackOrientation.Vertical
         };
      }

      /// <summary>
      ///    Gets the image.
      /// </summary>
      /// <param name="filePath">The file path.</param>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      /// <param name="aspect">The aspect.</param>
      /// <param name="getFromResources">if set to <c>true</c> [get from resources].</param>
      /// <param name="resourceClass">The resource class.</param>
      /// <returns>Image.</returns>
      public static Image GetImage
      (
         string filePath,
         double width            = 0,
         double height           = 0,
         Aspect aspect           = Aspect.AspectFit,
         bool   getFromResources = false,
         Type   resourceClass    = null
      )
      {
         var retImage =
            new Image
            {
               Aspect            = aspect,
               VerticalOptions   = LayoutOptions.Center,
               HorizontalOptions = LayoutOptions.Center
            };

         if (filePath.IsNotEmpty())
         {
            if (getFromResources && resourceClass != null)
            {
               retImage.Source = ImageSource.FromResource(filePath, resourceClass.GetTypeInfo().Assembly);
            }
            else
            {
               retImage.Source = ImageSource.FromFile(filePath);
            }
         }

         if (width.IsNotEmpty())
         {
            retImage.WidthRequest = width;
         }

         if (height.IsNotEmpty())
         {
            retImage.HeightRequest = height;
         }

         return retImage;
      }

      /// <summary>
      ///    Gets the simple label.
      /// </summary>
      /// <param name="labelText">The label text.</param>
      /// <param name="textColor">Color of the text.</param>
      /// <param name="textAlignment">The text alignment.</param>
      /// <param name="fontNamedSize">Size of the font named.</param>
      /// <param name="fontSize">Size of the font.</param>
      /// <param name="fontAttributes">The font attributes.</param>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      /// <param name="labelBindingPropertyName">Name of the label binding property.</param>
      /// <param name="labelBindingSource">The label binding source.</param>
      /// <param name="breakMode">The break mode.</param>
      /// <returns>Label.</returns>
      public static Label GetSimpleLabel
      (
         string         labelText                = default,
         Color          textColor                = default,
         TextAlignment  textAlignment            = TextAlignment.Center,
         NamedSize      fontNamedSize            = NamedSize.Medium,
         double         fontSize                 = 0.0,
         FontAttributes fontAttributes           = FontAttributes.None,
         double         width                    = 0,
         double         height                   = 0,
         string         labelBindingPropertyName = default,
         object         labelBindingSource       = null,
         LineBreakMode  breakMode                = LineBreakMode.WordWrap
      )
      {
         if (textColor.IsUnsetOrDefault())
         {
            textColor = Color.Black;
         }

         var retLabel =
            new Label
            {
               Text                    = labelText,
               TextColor               = textColor,
               HorizontalTextAlignment = textAlignment,
               VerticalTextAlignment   = TextAlignment.Center,
               HorizontalOptions       = HorizontalOptionsFromTextAlignment(textAlignment),
               VerticalOptions         = LayoutOptions.CenterAndExpand,
               BackgroundColor         = Color.Transparent,
               InputTransparent        = true,
               FontAttributes          = fontAttributes,
               FontSize =
                  fontSize.IsNotEmpty() ? fontSize : Device.GetNamedSize(fontNamedSize, typeof(Label)),
               LineBreakMode = breakMode
            };

         // Set up the label text binding (if provided)
         if (labelBindingPropertyName.IsNotEmpty())
         {
            if (labelBindingSource != null)
            {
               retLabel.SetUpBinding(Label.TextProperty, labelBindingPropertyName, source: labelBindingSource);
            }
            else
            {
               retLabel.SetUpBinding(Label.TextProperty, labelBindingPropertyName);
            }
         }

         if (width.IsNotEmpty())
         {
            retLabel.WidthRequest = width;
         }

         if (height.IsNotEmpty())
         {
            retLabel.HeightRequest = height;
         }

         return retLabel;
      }

      // ------------------------------------------------------------------------------------------
      /// <summary>
      ///    Gets the spacer.
      /// </summary>
      /// <param name="height">The height.</param>
      /// <returns>BoxView.</returns>
      public static BoxView GetSpacer(double height)
      {
         return new BoxView
         {
            HeightRequest     = height,
            BackgroundColor   = Color.Transparent,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions   = LayoutOptions.FillAndExpand
         };
      }

      /// <summary>
      ///    Horizontals the options from text alignment.
      /// </summary>
      /// <param name="textAlignment">The text alignment.</param>
      /// <returns>LayoutOptions.</returns>
      public static LayoutOptions HorizontalOptionsFromTextAlignment(TextAlignment textAlignment)
      {
         switch (textAlignment)
         {
            case TextAlignment.Center:
               return LayoutOptions.Center;

            case TextAlignment.End:
               return LayoutOptions.End;

            // Covers Start and default
            default:
               return LayoutOptions.Start;
         }
      }

      /// <summary>
      ///    Determines whether [is different than] [the specified second color].
      /// </summary>
      /// <param name="color">The color.</param>
      /// <param name="secondColor">Color of the second.</param>
      /// <returns><c>true</c> if [is different than] [the specified second color]; otherwise, <c>false</c>.</returns>
      public static bool IsDifferentThan
      (
         this Color color,
         Color      secondColor
      )
      {
         return color.IsNotAnEqualObjectTo(secondColor);
      }

      /// <summary>
      ///    Determines whether [is unset or default] [the specified color].
      /// </summary>
      /// <param name="color">The color.</param>
      /// <returns><c>true</c> if [is unset or default] [the specified color]; otherwise, <c>false</c>.</returns>
      public static bool IsUnsetOrDefault(this Color color)
      {
         return color.IsAnEqualObjectTo(default);
      }

      /// <summary>
      ///    Returns true if ... is valid.
      /// </summary>
      /// <param name="color">The color.</param>
      /// <returns><c>true</c> if the specified color is valid; otherwise, <c>false</c>.</returns>
      public static bool IsValid(this Color color)
      {
         return !color.IsUnsetOrDefault();
      }

      /// <summary>
      ///    Merges the style.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="mainStyle">The main style.</param>
      /// <param name="newStyle">The new style.</param>
      /// <returns>Style.</returns>
      public static Style MergeStyle<T>
      (
         this Style mainStyle,
         Style      newStyle
      )
      {
         if (newStyle == null || newStyle.Setters.IsEmpty())
         {
            return mainStyle;
         }

         if (mainStyle == null)
         {
            mainStyle = new Style(typeof(T));
         }

         foreach (var setter in newStyle.Setters)
         {
            var foundSetter =
               mainStyle.Setters.FirstOrDefault(s => s.Property.PropertyName.IsSameAs(setter.Property.PropertyName));
            if (foundSetter != null)
            {
               var foundSetterIdx = mainStyle.Setters.IndexOf(foundSetter);

               mainStyle.Setters[foundSetterIdx] = setter;
            }
            else
            {
               mainStyle.Setters.Add(setter);
            }
         }

         // The Style settings must also be considered in the assignment
         mainStyle.ApplyToDerivedTypes = newStyle.ApplyToDerivedTypes || mainStyle.ApplyToDerivedTypes;

         //if (newStyle.BasedOn != default(Style))
         //{
         //   mainStyle.BasedOn = newStyle.BasedOn;
         //}

         // TODO
         //mainStyle.BaseResourceKey = newStyle.BaseResourceKey;
         //mainStyle.CanCascade = newStyle.CanCascade;
         //mainStyle.Class = newStyle.Class;
         //mainStyle.Behaviors = newStyle.Behaviors;
         //mainStyle.Triggers = newStyle.Triggers;
         //mainStyle.TargetType = newStyle.TargetType;

         return mainStyle;
      }

      /// <summary>
      ///    Converts to enum.
      /// </summary>
      /// <typeparam name="EnumT">The type of the enum t.</typeparam>
      /// <param name="enumAsString">The enum as string.</param>
      /// <returns>EnumT.</returns>
      public static EnumT ToEnum<EnumT>(this string enumAsString)
         where EnumT : Enum
      {
         /*
         if (enumAsString.IsEmpty())
         {
            return default(EnumT);
         }

         return ((EnumT)Enum.Parse(typeof(EnumT), enumAsString, true));
         */

         if (EnumTryParse(enumAsString, out EnumT foundEnum))
         {
            return foundEnum;
         }

         return default;
      }

      /// <summary>
      ///    Converts to options.
      /// </summary>
      /// <param name="alignment">The alignment.</param>
      /// <returns>LayoutOptions.</returns>
      public static LayoutOptions ToOptions(this LayoutAlignment alignment)
      {
         // Set the child margins to compel this alignment
         switch (alignment)
         {
            case LayoutAlignment.Center:
               return LayoutOptions.Center;

            case LayoutAlignment.End:
               return LayoutOptions.End;

            case LayoutAlignment.Fill:
               return LayoutOptions.Fill;

            case LayoutAlignment.Start:
               return LayoutOptions.Start;
         }

         return default;
      }

      /// <summary>
      ///    Runs a Task without changing the context (configure await is false).
      /// </summary>
      /// <param name="task">The task.</param>
      /// <returns>Task.</returns>
      public static async Task WithoutChangingContext(this Task task) =>
#if AVOID_CONTEXT_MANAGEMENT
         await task;
#else
         await task.ConfigureAwait(false);

#endif

      /// <summary>
      ///    Withouts the changing context.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="task">The task.</param>
      /// <returns>Task&lt;T&gt;.</returns>
      public static async Task<T> WithoutChangingContext<T>(this Task<T> task)
      {
         return await task.ConfigureAwait(false);
      }

      /// <summary>
      ///    Gets the expanding scroll view.
      /// </summary>
      /// <returns>ScrollView.</returns>
      internal static ScrollView GetExpandingScrollView()
      {
         return new ScrollView
         {
            VerticalOptions   = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor   = Color.Transparent,
            Orientation       = ScrollOrientation.Vertical
         };
      }
   }
}