// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="FormsUtils.cs" company="Marcus Technical Services, Inc.">
//     Copyright @2018 Marcus Technical Services, Inc.
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

namespace Com.MarcusTS.SharedForms.Utils
{
   using System.Linq;
   using System.Threading.Tasks;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   /// Class FormsUtils.
   /// </summary>
   public static class FormsUtils
   {
      #region Internal Methods

      /// <summary>
      /// Gets the expanding scroll view.
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

      #endregion Internal Methods

      #region Public Fields

      /// <summary>
      /// The false string
      /// </summary>
      public const string FALSE_STR = "false";

      /// <summary>
      /// The true string
      /// </summary>
      public const string TRUE_STR = "true";

      #endregion Public Fields

      #region Internal Fields

      /// <summary>
      /// The button radius factor
      /// </summary>
      internal const double BUTTON_RADIUS_FACTOR = 0.15f;

      /// <summary>
      /// The default text size
      /// </summary>
      internal const float DEFAULT_TEXT_SIZE = 20;

      /// <summary>
      /// The major button height
      /// </summary>
      internal static readonly double MAJOR_BUTTON_HEIGHT = 45.0;

      /// <summary>
      /// The major button width
      /// </summary>
      internal static readonly double MAJOR_BUTTON_WIDTH = 120.0;

      #endregion Internal Fields

      #region Public Methods

      /// <summary>
      /// Adds the automatic column.
      /// </summary>
      /// <param name="grid">The grid.</param>
      public static void AddAutoColumn(this Grid grid)
      {
         grid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
      }

      /// <summary>
      /// Adds the automatic row.
      /// </summary>
      /// <param name="grid">The grid.</param>
      public static void AddAutoRow(this Grid grid)
      {
         grid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
      }

      /// <summary>
      /// Adds the fixed column.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="width">The width.</param>
      public static void AddFixedColumn(this Grid grid,
                                        double    width)
      {
         grid.ColumnDefinitions.Add(new ColumnDefinition {Width = width});
      }

      /// <summary>
      /// Adds the fixed row.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="height">The height.</param>
      public static void AddFixedRow(this Grid grid,
                                     double    height)
      {
         grid.RowDefinitions.Add(new RowDefinition {Height = height});
      }

      /// <summary>
      /// Adds the star column.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="factor">The factor.</param>
      public static void AddStarColumn(this Grid grid,
                                       double    factor = 1)
      {
         grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(factor, GridUnitType.Star)});
      }

      /// <summary>
      /// Adds the star row.
      /// </summary>
      /// <param name="grid">The grid.</param>
      /// <param name="factor">The factor.</param>
      public static void AddStarRow(this Grid grid,
                                    double    factor = 1)
      {
         grid.RowDefinitions.Add(new RowDefinition {Height = new GridLength(factor, GridUnitType.Star)});
      }

      /// <summary>
      /// Creates the relative overlay.
      /// </summary>
      /// <param name="layout">The layout.</param>
      /// <param name="viewToAdd">The view to add.</param>
      public static void CreateRelativeOverlay(this RelativeLayout layout,
                                               View                viewToAdd)
      {
         layout.Children.Add(
                             viewToAdd, Constraint.Constant(0), Constraint.Constant(0),
                             Constraint.RelativeToParent(parent => parent.Width),
                             Constraint.RelativeToParent(parent => parent.Height));
      }

      /// <summary>
      /// Forces the style.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="style">The style.</param>
      public static void ForceStyle(this View view,
                                    Style     style)
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
      /// Gets the expanding absolute layout.
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
      /// Gets the expanding grid.
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
      /// Gets the expanding relative layout.
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
      /// Gets the expanding stack layout.
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
      /// Gets the image.
      /// </summary>
      /// <param name="filePath">The file path.</param>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      /// <param name="aspect">The aspect.</param>
      /// <returns>Image.</returns>
      public static Image GetImage(string filePath,
                                   double width  = 0,
                                   double height = 0,
                                   Aspect aspect = Aspect.AspectFit)
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
            retImage.Source = ImageSource.FromFile(filePath);
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
      /// Gets the simple label.
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
      public static Label GetSimpleLabel(string         labelText                = default(string),
                                         Color          textColor                = default(Color),
                                         TextAlignment  textAlignment            = TextAlignment.Center,
                                         NamedSize      fontNamedSize            = NamedSize.Medium,
                                         double         fontSize                 = 0.0,
                                         FontAttributes fontAttributes           = FontAttributes.None,
                                         double         width                    = 0,
                                         double         height                   = 0,
                                         string         labelBindingPropertyName = default(string),
                                         object         labelBindingSource       = null,
                                         LineBreakMode  breakMode                = LineBreakMode.WordWrap)
      {
         if (textColor.IsAnEqualObjectTo(default(Color)))
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
      /// Gets the spacer.
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
      /// Horizontals the options from text alignment.
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
      /// Merges the style.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="mainStyle">The main style.</param>
      /// <param name="newStyle">The new style.</param>
      /// <returns>Style.</returns>
      public static Style MergeStyle<T>(this Style mainStyle,
                                        Style      newStyle)
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
      /// Sets up binding.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="bindableProperty">The bindable property.</param>
      /// <param name="viewModelPropertyName">Name of the view model property.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="converter">The converter.</param>
      /// <param name="converterParameter">The converter parameter.</param>
      /// <param name="stringFormat">The string format.</param>
      /// <param name="source">The source.</param>
      public static void SetUpBinding(this BindableObject view,
                                      BindableProperty    bindableProperty,
                                      string              viewModelPropertyName,
                                      BindingMode         bindingMode        = BindingMode.OneWay,
                                      IValueConverter     converter          = null,
                                      object              converterParameter = null,
                                      string              stringFormat       = null,
                                      object              source             = null)
      {
         view.SetBinding(bindableProperty,
                         new Binding(viewModelPropertyName, bindingMode, converter, converterParameter, stringFormat,
                                     source));
      }

      /// <summary>
      /// Withouts the changing context.
      /// </summary>
      /// <param name="task">The task.</param>
      /// <returns>Task.</returns>
      public static async Task WithoutChangingContext(this Task task)
      {
         await task.ConfigureAwait(false);
      }

      /// <summary>
      /// Withouts the changing context.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="task">The task.</param>
      /// <returns>Task&lt;T&gt;.</returns>
      public static async Task<T> WithoutChangingContext<T>(this Task<T> task)
      {
         return await task.ConfigureAwait(false);
      }

      #endregion Public Methods
   }
}