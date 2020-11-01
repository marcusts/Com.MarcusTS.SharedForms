// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, EnumToggleImageLabelButtonBase.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Utils;
   using SharedUtils.Utils;
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IEnumToggleImageLabelButton
   /// </summary>
   /// <typeparam name="EnumT">The type of the enum t.</typeparam>
   public interface IEnumToggleImageLabelButton<EnumT>
      where EnumT : Enum
   {
      /// <summary>
      /// Gets or sets the color of the button back.
      /// </summary>
      /// <value>The color of the button back.</value>
      Color ButtonBackColor { get; set; }

      /// <summary>
      /// Gets or sets the color of the button border.
      /// </summary>
      /// <value>The color of the button border.</value>
      Color ButtonBorderColor { get; set; }

      /// <summary>
      /// Gets or sets the width of the button border.
      /// </summary>
      /// <value>The width of the button border.</value>
      float? ButtonBorderWidth { get; set; }

      /// <summary>
      /// Gets or sets the current selection.
      /// </summary>
      /// <value>The current selection.</value>
      EnumT CurrentSelection { get; set; }

      /// <summary>
      /// Gets or sets the label font attributes.
      /// </summary>
      /// <value>The label font attributes.</value>
      FontAttributes LabelFontAttributes { get; set; }

      /// <summary>
      /// Gets or sets the size of the label font.
      /// </summary>
      /// <value>The size of the label font.</value>
      double LabelFontSize { get; set; }

      /// <summary>
      /// Gets or sets the color of the label text.
      /// </summary>
      /// <value>The color of the label text.</value>
      Color LabelTextColor { get; set; }
   }

   /// <summary>
   /// Class EnumToggleImageLabelButtonBase.
   /// Implements the <see cref="SelectionImageLabelButtonBase" />
   /// Implements the <see cref="IEnumToggleImageLabelButton{EnumT}" />
   /// </summary>
   /// <typeparam name="EnumT">The type of the enum t.</typeparam>
   /// <seealso cref="SelectionImageLabelButtonBase" />
   /// <seealso cref="IEnumToggleImageLabelButton{EnumT}" />
   public class EnumToggleImageLabelButtonBase<EnumT> : SelectionImageLabelButtonBase,
                                                        IEnumToggleImageLabelButton<EnumT>
      where EnumT : Enum
   {
      /// <summary>
      /// The button back color property
      /// </summary>
      public static readonly BindableProperty ButtonBackColorProperty =
         EnumToggleImageLabelButtonBaseBindableProperty
         (
            nameof(ButtonBackColor),
            DEFAULT_BUTTON_BACK_COLOR,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonBackColor = newVal;
            }
         );

      /// <summary>
      /// The button border color property
      /// </summary>
      public static readonly BindableProperty ButtonBorderColorProperty =
         EnumToggleImageLabelButtonBaseBindableProperty
         (
            nameof(ButtonBorderColor),
            DEFAULT_BUTTON_BORDER_COLOR,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonBorderColor = newVal;
            }
         );

      /// <summary>
      /// The button border width property
      /// </summary>
      public static readonly BindableProperty ButtonBorderWidthProperty =
         EnumToggleImageLabelButtonBaseBindableProperty
         (
            nameof(ButtonBorderWidth),
            DEFAULT_BUTTON_BORDER_WIDTH,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonBorderWidth = newVal;
            }
         );

      /// <summary>
      /// The current selection property
      /// </summary>
      public static readonly BindableProperty CurrentSelectionProperty =
         EnumToggleImageLabelButtonBaseBindableProperty
         (
            nameof(CurrentSelection),
            default(EnumT),
            BindingMode.TwoWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.SetCurrentStyleByEnum(newVal);
            }
         );

      /// <summary>
      /// The label font attributes property
      /// </summary>
      public static readonly BindableProperty LabelFontAttributesProperty =
         EnumToggleImageLabelButtonBaseBindableProperty
         (
            nameof(LabelFontAttributes),
            DEFAULT_LABEL_FONT_ATTRIBUTES,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.LabelFontAttributes = newVal;
            }
         );

      /// <summary>
      /// The label font size property
      /// </summary>
      public static readonly BindableProperty LabelFontSizeProperty =
         EnumToggleImageLabelButtonBaseBindableProperty
         (
            nameof(LabelFontSize),
            DEFAULT_LABEL_FONT_SIZE,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.LabelFontSize = newVal;
            }
         );

      /// <summary>
      /// The label text color property
      /// </summary>
      public static readonly BindableProperty LabelTextColorProperty =
         EnumToggleImageLabelButtonBaseBindableProperty
         (
            nameof(LabelTextColor),
            DEFAULT_LABEL_TEXT_COLOR,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.LabelTextColor = newVal;
            }
         );

      /// <summary>
      /// The default image suffix
      /// </summary>
      private const string DEFAULT_IMAGE_SUFFIX = ".png";

      /// <summary>
      /// The default button back color
      /// </summary>
      private static readonly Color DEFAULT_BUTTON_BACK_COLOR = Color.FromRgb(25, 25, 25);

      /// <summary>
      /// The default button border color
      /// </summary>
      private static readonly Color DEFAULT_BUTTON_BORDER_COLOR = Color.Transparent;

      /// <summary>
      /// The default button border width
      /// </summary>
      private static readonly float? DEFAULT_BUTTON_BORDER_WIDTH = 0;

      /// <summary>
      /// The default label font attributes
      /// </summary>
      private static readonly FontAttributes DEFAULT_LABEL_FONT_ATTRIBUTES = FontAttributes.Bold;

      /// <summary>
      /// The default label font size
      /// </summary>
      private static readonly double DEFAULT_LABEL_FONT_SIZE = Device.GetNamedSize(NamedSize.Small, typeof(Label));

      /// <summary>
      /// The default label text color
      /// </summary>
      private static readonly Color DEFAULT_LABEL_TEXT_COLOR = Color.White;

      /// <summary>
      /// The is initializing
      /// </summary>
      private readonly bool _isInitializing;

      /// <summary>
      /// The button back color
      /// </summary>
      private Color _buttonBackColor;

      /// <summary>
      /// The button border color
      /// </summary>
      private Color _buttonBorderColor;

      /// <summary>
      /// The button border width
      /// </summary>
      private float? _buttonBorderWidth;

      /// <summary>
      /// The image label button styles
      /// </summary>
      private IList<ImageLabelButtonStyle> _imageLabelButtonStyles;

      /// <summary>
      /// The label font attributes
      /// </summary>
      private FontAttributes _labelFontAttributes;

      /// <summary>
      /// The label font size
      /// </summary>
      private double _labelFontSize;

      /// <summary>
      /// The label text color
      /// </summary>
      private Color _labelTextColor;

      /// <summary>
      /// Initializes a new instance of the <see cref="EnumToggleImageLabelButtonBase{EnumT}" /> class.
      /// </summary>
      public EnumToggleImageLabelButtonBase()
      {
         _isInitializing = true;

         ButtonBackColor = DEFAULT_BUTTON_BACK_COLOR;
         ButtonBorderColor = DEFAULT_BUTTON_BORDER_COLOR;
         ButtonBorderWidth = DEFAULT_BUTTON_BORDER_WIDTH;
         LabelFontAttributes = DEFAULT_LABEL_FONT_ATTRIBUTES;
         LabelFontSize = DEFAULT_LABEL_FONT_SIZE;
         LabelTextColor = DEFAULT_LABEL_TEXT_COLOR;

         // LabelWidth               = 85;
         // LabelHeight              = 35;
         ButtonCornerRadiusFactor = FormsConst.DEFAULT_CORNER_RADIUS_FACTOR;

         _isInitializing = false;

         RefreshImageLabelButtonStyles();
      }

      /// <summary>
      /// Gets or sets the color of the button back.
      /// </summary>
      /// <value>The color of the button back.</value>
      public Color ButtonBackColor
      {
         get => _buttonBackColor;
         set
         {
            if (_buttonBackColor.IsDifferentThan(value))
            {
               _buttonBackColor = value;
               RefreshImageLabelButtonStyles();
            }
         }
      }

      /// <summary>
      /// Gets or sets the color of the button border.
      /// </summary>
      /// <value>The color of the button border.</value>
      public Color ButtonBorderColor
      {
         get => _buttonBorderColor;
         set
         {
            if (_buttonBorderColor.IsDifferentThan(value))
            {
               _buttonBorderColor = value;
               RefreshImageLabelButtonStyles();
            }
         }
      }

      /// <summary>
      /// Gets or sets the width of the button border.
      /// </summary>
      /// <value>The width of the button border.</value>
      public float? ButtonBorderWidth
      {
         get => _buttonBorderWidth;
         set
         {
            if (_buttonBorderWidth.IsNotAnEqualObjectTo(value))
            {
               _buttonBorderWidth = value;
               RefreshImageLabelButtonStyles();
            }
         }
      }

      /// <summary>
      /// Gets or sets the current selection.
      /// </summary>
      /// <value>The current selection.</value>
      public EnumT CurrentSelection
      {
         get => (EnumT)GetValue(CurrentSelectionProperty);

         set => SetValue(CurrentSelectionProperty, value);
      }

      /// <summary>
      /// Gets the image label button styles.
      /// </summary>
      /// <value>The image label button styles.</value>
      public override IList<ImageLabelButtonStyle> ImageLabelButtonStyles =>
         _imageLabelButtonStyles ?? (_imageLabelButtonStyles = CreateImageLabelButtonStyles());

      /// <summary>
      /// Gets or sets the label font attributes.
      /// </summary>
      /// <value>The label font attributes.</value>
      public FontAttributes LabelFontAttributes
      {
         get => _labelFontAttributes;
         set
         {
            if (_labelFontAttributes.IsNotAnEqualObjectTo(value))
            {
               _labelFontAttributes = value;
               RefreshImageLabelButtonStyles();
            }
         }
      }

      /// <summary>
      /// Gets or sets the size of the label font.
      /// </summary>
      /// <value>The size of the label font.</value>
      public double LabelFontSize
      {
         get => _labelFontSize;
         set
         {
            if (_labelFontSize.IsDifferentThan(value))
            {
               _labelFontSize = value;
               RefreshImageLabelButtonStyles();
            }
         }
      }

      /// <summary>
      /// Gets or sets the color of the label text.
      /// </summary>
      /// <value>The color of the label text.</value>
      public Color LabelTextColor
      {
         get => _labelTextColor;
         set
         {
            if (_labelTextColor.IsDifferentThan(value))
            {
               _labelTextColor = value;
               RefreshImageLabelButtonStyles();
            }
         }
      }

      /// <summary>
      /// Required by this case; each style has its own text.
      /// </summary>
      /// <value><c>true</c> if [update button text from style]; otherwise, <c>false</c>.</value>
      public override bool UpdateButtonTextFromStyle => true;

      /// <summary>
      /// Enums the toggle image label button base bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty EnumToggleImageLabelButtonBaseBindableProperty<PropertyTypeT>
      (
         string localPropName,
         PropertyTypeT defaultVal = default,
         BindingMode bindingMode =
            BindingMode.OneWay,
         Action<EnumToggleImageLabelButtonBase<EnumT>, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      /// Called when [button state changed].
      /// </summary>
      protected override void AfterButtonStateChanged()
      {
         base.AfterButtonStateChanged();

         CurrentSelection = ButtonState.ToEnum<EnumT>();
      }

      /// <summary>
      /// Creates the button style.
      /// </summary>
      /// <param name="enumVal">The enum value.</param>
      /// <param name="enumString">The enum string.</param>
      /// <returns>Style.</returns>
      protected virtual Style CreateButtonStyle
      (
         EnumT enumVal,
         string enumString
      )
      {
         return CreateButtonStyle(ButtonBackColor, ButtonBorderWidth, ButtonBorderColor);
      }

      /// <summary>
      /// Creates the image file path.
      /// </summary>
      /// <param name="enumVal">The enum value.</param>
      /// <param name="enumString">The enum string.</param>
      /// <returns>System.String.</returns>
      protected virtual string CreateImageFilePath
      (
         EnumT enumVal,
         string enumString
      )
      {
         return enumString.ToLower() + DEFAULT_IMAGE_SUFFIX;
      }

      /// <summary>
      /// Creates the label button text.
      /// </summary>
      /// <param name="enumVal">The enum value.</param>
      /// <param name="enumString">The enum string.</param>
      /// <returns>System.String.</returns>
      protected virtual string CreateLabelButtonText
      (
         EnumT enumVal,
         string enumString
      )
      {
         return enumString;
      }

      /// <summary>
      /// Creates the label style.
      /// </summary>
      /// <param name="enumVal">The enum value.</param>
      /// <param name="enumString">The enum string.</param>
      /// <returns>Style.</returns>
      protected virtual Style CreateLabelStyle
      (
         EnumT enumVal,
         string enumString
      )
      {
         return CreateLabelStyle(LabelTextColor, LabelFontSize, LabelFontAttributes);
      }

      /// <summary>
      /// Creates the image label button styles.
      /// </summary>
      /// <returns>IList&lt;ImageLabelButtonStyle&gt;.</returns>
      private IList<ImageLabelButtonStyle> CreateImageLabelButtonStyles()
      {
         var newImageLabelButtonStyles = new List<ImageLabelButtonStyle>();
         var enumStrings = Enum.GetNames(typeof(EnumT));
         var enumValues = Enum.GetValues(typeof(EnumT));

         var idx = 0;
         foreach (var enumVal in enumValues.OfType<EnumT>())
         {
            var enumStr = enumStrings[idx];

            newImageLabelButtonStyles.Add
            (
               new ImageLabelButtonStyle
               {
                  ButtonStyle = CreateButtonStyle(enumVal, enumStr),
                  ButtonText = CreateLabelButtonText(enumVal, enumStr).ToUpper(),
                  GetImageFromResource = GetImageFromResource,
                  ImageFilePath = CreateImageFilePath(enumVal, enumStr),
                  ImageResourceClassType = ImageResourceClassType,
                  InternalButtonState = enumStr,
                  LabelStyle = CreateLabelStyle(enumVal, enumStr)
               }
            );

            idx++;
         }

         return newImageLabelButtonStyles;
      }

      /// <summary>
      /// Refreshes the image label button styles.
      /// </summary>
      private void RefreshImageLabelButtonStyles()
      {
         if (_isInitializing)
         {
            return;
         }

         var currentStyleIdx = _imageLabelButtonStyles.IndexOf(CurrentStyle);
         _imageLabelButtonStyles = null;
         _imageLabelButtonStyles = CreateImageLabelButtonStyles();
         CurrentStyle =
            ImageLabelButtonStyles.IsNotEmpty() && ImageLabelButtonStyles.Count > currentStyleIdx
               ? ImageLabelButtonStyles[currentStyleIdx]
               : ImageLabelButtonStyles.IsNotEmpty()
                  ? ImageLabelButtonStyles[0]
                  : default;

         // Refreshes the current style in the base class
         SetAllStyles();
      }

      /// <summary>
      /// Sets the current style by enum.
      /// </summary>
      /// <param name="newVal">The new value.</param>
      private void SetCurrentStyleByEnum(EnumT newVal)
      {
         var buttonText = Enum.GetName(typeof(EnumT), newVal);

         if (buttonText.IsNotEmpty())
         {
            SetCurrentStyleByButtonText(buttonText);
         }
      }
   }
}
