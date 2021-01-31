// *********************************************************************************
// *********************************************************************************
// <copyright file=SimpleImageLabelButton.cs company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Utils;
   using System;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   ///    Interface ISimpleImageLabelButton
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ITriStateImageLabelButton" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ITriStateImageLabelButton" />
   public interface ISimpleImageLabelButton : ITriStateImageLabelButton
   {
      /// <summary>
      ///    Gets or sets the color of the button back.
      /// </summary>
      /// <value>The color of the button back.</value>
      Color ButtonBackColor { get; set; }

      /// <summary>
      ///    Gets or sets the color of the button border.
      /// </summary>
      /// <value>The color of the button border.</value>
      Color ButtonBorderColor { get; set; }

      /// <summary>
      ///    Gets or sets the width of the button border.
      /// </summary>
      /// <value>The width of the button border.</value>
      float ButtonBorderWidth { get; set; }

      /// <summary>
      ///    Gets or sets the image file path.
      /// </summary>
      /// <value>The image file path.</value>
      string ImageFilePath { get; set; }

      /// <summary>
      ///    Gets or sets the label font attributes.
      /// </summary>
      /// <value>The label font attributes.</value>
      FontAttributes LabelFontAttributes { get; set; }

      /// <summary>
      ///    Gets or sets the size of the label font.
      /// </summary>
      /// <value>The size of the label font.</value>
      double LabelFontSize { get; set; }

      /// <summary>
      ///    Gets or sets the label text.
      /// </summary>
      /// <value>The label text.</value>
      string LabelText { get; set; }

      /// <summary>
      ///    Gets or sets the color of the label text.
      /// </summary>
      /// <value>The color of the label text.</value>
      Color LabelTextColor { get; set; }
   }

   /// <summary>
   ///    A button that can contain either an image and/or a label.
   ///    This button is not selectable or toggle-able.
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.TriStateImageLabelButton" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ISimpleImageLabelButton" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.TriStateImageLabelButton" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ISimpleImageLabelButton" />
   public class SimpleImageLabelButton : TriStateImageLabelButton, ISimpleImageLabelButton
   {
      /// <summary>
      ///    The button back color property
      /// </summary>
      public static readonly BindableProperty ButtonBackColorProperty =
         CreateSimpleImageLabelButtonBindableProperty
         (
            nameof(ButtonBackColor),
            default(Color),
            BindingMode.OneWay,
            (
               simpleButton,
               oldVal,
               newVal
            ) =>
            {
               simpleButton.ButtonBackColor = newVal;
            }
         );

      /// <summary>
      ///    The button border color property
      /// </summary>
      public static readonly BindableProperty ButtonBorderColorProperty =
         CreateSimpleImageLabelButtonBindableProperty
         (
            nameof(ButtonBorderColor),
            default(Color),
            BindingMode.OneWay,
            (
               simpleButton,
               oldVal,
               newVal
            ) =>
            {
               simpleButton.ButtonBorderColor = newVal;
            }
         );

      /// <summary>
      ///    The button border width property
      /// </summary>
      public static readonly BindableProperty ButtonBorderWidthProperty =
         CreateSimpleImageLabelButtonBindableProperty
         (
            nameof(ButtonBorderWidth),
            default(float),
            BindingMode.OneWay,
            (
               simpleButton,
               oldVal,
               newVal
            ) =>
            {
               simpleButton.ButtonBorderWidth = newVal;
            }
         );

      /// <summary>
      ///    The image file path property
      /// </summary>
      public static readonly BindableProperty ImageFilePathProperty =
         CreateSimpleImageLabelButtonBindableProperty
         (
            nameof(ImageFilePath),
            default(string),
            BindingMode.OneWay,
            (
               simpleButton,
               oldVal,
               newVal
            ) =>
            {
               simpleButton.ImageFilePath = newVal;
            }
         );

      /// <summary>
      ///    The label font attributes property
      /// </summary>
      public static readonly BindableProperty LabelFontAttributesProperty =
         CreateSimpleImageLabelButtonBindableProperty
         (
            nameof(LabelFontAttributes),
            default(FontAttributes),
            BindingMode.OneWay,
            (
               simpleButton,
               oldVal,
               newVal
            ) =>
            {
               simpleButton.LabelFontAttributes = newVal;
            }
         );

      /// <summary>
      ///    The label font size property
      /// </summary>
      public static readonly BindableProperty LabelFontSizeProperty =
         CreateSimpleImageLabelButtonBindableProperty
         (
            nameof(LabelFontSize),
            default(double),
            BindingMode.OneWay,
            (
               simpleButton,
               oldVal,
               newVal
            ) =>
            {
               simpleButton.LabelFontSize = newVal;
            }
         );

      /// <summary>
      ///    The label text color property
      /// </summary>
      public static readonly BindableProperty LabelTextColorProperty =
         CreateSimpleImageLabelButtonBindableProperty
         (
            nameof(LabelTextColor),
            default(Color),
            BindingMode.OneWay,
            (
               simpleButton,
               oldVal,
               newVal
            ) =>
            {
               simpleButton.LabelTextColor = newVal;
            }
         );

      /// <summary>
      ///    The label text property
      /// </summary>
      public static readonly BindableProperty LabelTextProperty =
         CreateSimpleImageLabelButtonBindableProperty
         (
            nameof(LabelText),
            default(string),
            BindingMode.OneWay,
            (
               simpleButton,
               oldVal,
               newVal
            ) =>
            {
               simpleButton.LabelText = newVal;
            }
         );

      /// <summary>
      ///    The button back color
      /// </summary>
      private Color _buttonBackColor;

      /// <summary>
      ///    The button border color
      /// </summary>
      private Color _buttonBorderColor;

      /// <summary>
      ///    The button border width
      /// </summary>
      private float _buttonBorderWidth;

      /// <summary>
      ///    The label font attributes
      /// </summary>
      private FontAttributes _labelFontAttributes;

      /// <summary>
      ///    The label font size
      /// </summary>
      private double _labelFontSize;

      /// <summary>
      ///    The label text
      /// </summary>
      private string _labelText;

      /// <summary>
      ///    The label text color
      /// </summary>
      private Color _labelTextColor;

      /// <summary>
      ///    Initializes a new instance of the <see cref="SimpleImageLabelButton" /> class.
      /// </summary>
      public SimpleImageLabelButton(double labelWidth, double labelHeight, string fontFamilyOverride = "")
      {
         CanSelect             = false;
         ButtonToggleSelection = false;
         LabelPos              = FormsUtils.OnScreenPositions.CENTER;

         ButtonLabel           = FormsUtils.GetSimpleLabel("", width:labelWidth, height:labelHeight, fontFamilyOverride:fontFamilyOverride);

         SetAllStyles();
      }

      /// <summary>
      ///    Gets a value indicating whether this instance is disabled.
      /// </summary>
      /// <value><c>true</c> if this instance is disabled; otherwise, <c>false</c>.</value>
      protected override bool IsDisabled => false;

      /// <summary>
      ///    Gets or sets the color of the button back.
      /// </summary>
      /// <value>The color of the button back.</value>
      public Color ButtonBackColor
      {
         get => _buttonBackColor;
         set
         {
            _buttonBackColor = value;
            ResetButtonStyle();
         }
      }

      /// <summary>
      ///    Gets or sets the color of the button border.
      /// </summary>
      /// <value>The color of the button border.</value>
      public Color ButtonBorderColor
      {
         get => _buttonBorderColor;
         set
         {
            _buttonBorderColor = value;
            ResetButtonStyle();
         }
      }

      /// <summary>
      ///    Gets or sets the width of the button border.
      /// </summary>
      /// <value>The width of the button border.</value>
      public float ButtonBorderWidth
      {
         get => _buttonBorderWidth;
         set
         {
            _buttonBorderWidth = value;
            ResetButtonStyle();
         }
      }

      /// <summary>
      ///    Gets or sets the image file path.
      /// </summary>
      /// <value>The image file path.</value>
      public string ImageFilePath
      {
         get => ImageDeselectedFilePath;
         set => ImageDeselectedFilePath = value;
      }

      /// <summary>
      ///    Gets a value indicating whether this instance is selected.
      /// </summary>
      /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
      public override bool IsSelected => false;

      /// <summary>
      ///    Gets or sets the label font attributes.
      /// </summary>
      /// <value>The label font attributes.</value>
      public FontAttributes LabelFontAttributes
      {
         get => _labelFontAttributes;
         set
         {
            _labelFontAttributes = value;
            ResetLabelStyle();
         }
      }

      /// <summary>
      ///    Gets or sets the size of the label font.
      /// </summary>
      /// <value>The size of the label font.</value>
      public double LabelFontSize
      {
         get => _labelFontSize;
         set
         {
            _labelFontSize = value;
            ResetLabelStyle();
         }
      }

      /// <summary>
      ///    Gets or sets the label text.
      /// </summary>
      /// <value>The label text.</value>
      public string LabelText
      {
         get => _labelText;
         set
         {
            _labelText = value;
            ButtonLabel.Text = _labelText;
         }
      }

      /// <summary>
      ///    Gets or sets the color of the label text.
      /// </summary>
      /// <value>The color of the label text.</value>
      public Color LabelTextColor
      {
         get => _labelTextColor;
         set
         {
            _labelTextColor = value;
            ResetLabelStyle();
         }
      }

      /// <summary>
      ///    Creates the simple image label button.
      /// </summary>
      /// <param name="labelText">The label text.</param>
      /// <param name="labelTextColor">Color of the label text.</param>
      /// <param name="labelFontSize">Size of the label font.</param>
      /// <param name="widthRequest">The width request.</param>
      /// <param name="heightRequest">The height request.</param>
      /// <param name="bindingContext">The binding context.</param>
      /// <param name="buttonBackColor">Color of the button back.</param>
      /// <param name="buttonBorderColor">Color of the button border.</param>
      /// <param name="horizontalOptions">The horizontal options.</param>
      /// <param name="verticalOptions">The vertical options.</param>
      /// <param name="fontFamilyOverride"></param>
      /// <param name="imageFilePath">The image file path.</param>
      /// <param name="buttonBorderWidth">Width of the button border.</param>
      /// <param name="labelFontAttributes">The label font attributes.</param>
      /// <param name="cornerRadiusFactor">The corner radius factor.</param>
      /// <param name="buttonCommandBindingName">Name of the button command binding.</param>
      /// <param name="animateButton"></param>
      /// <param name="includeHapticFeedback"></param>
      /// <returns>ISimpleImageLabelButton.</returns>
      public static ISimpleImageLabelButton CreateSimpleImageLabelButton
      (
         string         labelText,
         Color          labelTextColor,
         double         labelFontSize,
         double         widthRequest,
         double         heightRequest,
         object         bindingContext,
         Color          buttonBackColor,
         Color          buttonBorderColor,
         LayoutOptions  horizontalOptions,
         LayoutOptions  verticalOptions,
         string fontFamilyOverride = "",
         string imageFilePath            = "",
         float        buttonBorderWidth        = default,
         FontAttributes labelFontAttributes      = default,
         float          cornerRadiusFactor       = DEFAULT_BUTTON_RADIUS_FACTOR,
         string         buttonCommandBindingName = "",
         bool animateButton = true,
         bool includeHapticFeedback = true
      )
      {
         var newSimpleImageLabelButton = new SimpleImageLabelButton(widthRequest, heightRequest)
         {
            LabelText                = labelText,
            LabelTextColor           = labelTextColor,
            LabelFontSize            = labelFontSize,
            WidthRequest             = widthRequest,
            HeightRequest            = heightRequest,
            BindingContext           = bindingContext,
            ButtonBackColor          = buttonBackColor,
            ButtonBorderColor        = buttonBorderColor,
            HorizontalOptions        = horizontalOptions,
            VerticalOptions          = verticalOptions,
            ImageFilePath            = imageFilePath,
            ButtonBorderWidth        = buttonBorderWidth,
            LabelFontAttributes      = labelFontAttributes,
            ButtonCornerRadiusFactor = cornerRadiusFactor,
            ButtonCommandBindingName = buttonCommandBindingName
         };

         if (fontFamilyOverride.IsNotEmpty())
         {
            newSimpleImageLabelButton.ButtonLabel.FontFamily = fontFamilyOverride;
         }

         if (newSimpleImageLabelButton.ButtonLabel != null)
         {
            newSimpleImageLabelButton.ButtonLabel.BindingContext = bindingContext;
         }

         if (newSimpleImageLabelButton.ButtonImage != null)
         {
            newSimpleImageLabelButton.ButtonImage.BindingContext = bindingContext;
         }

         newSimpleImageLabelButton.AnimateButton         = animateButton;
         newSimpleImageLabelButton.IncludeHapticFeedback = includeHapticFeedback;
         
         return newSimpleImageLabelButton;
      }
      
      /// <summary>
      ///    Creates the simple image label button bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateSimpleImageLabelButtonBindableProperty<PropertyTypeT>
      (
         string                                                       localPropName,
         PropertyTypeT                                                defaultVal     = default,
         BindingMode                                                  bindingMode    = BindingMode.OneWay,
         Action<SimpleImageLabelButton, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      ///    Deselects this instance.
      /// </summary>
      protected override void Deselect()
      {
         // Do nothing
      }

      /// <summary>
      ///    Resets the button style.
      /// </summary>
      private void ResetButtonStyle()
      {
         ButtonDeselectedStyle = CreateButtonStyle(ButtonBackColor, ButtonBorderWidth, ButtonBorderColor);
      }

      /// <summary>
      ///    Resets the label style.
      /// </summary>
      private void ResetLabelStyle()
      {
         LabelDeselectedStyle = CreateLabelStyle(LabelTextColor, LabelFontSize, LabelFontAttributes);
      }
   }
}