// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, WebViewWithCloseButton.cs, is a part of a program called AccountViewMobile.
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
   using Common.Images;
   using Common.Utils;
   using SharedUtils.Utils;
   using System;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IWebViewWithCloseButton
   /// </summary>
   public interface IWebViewWithCloseButton
   {
      /// <summary>
      /// Gets or sets the close command.
      /// </summary>
      /// <value>The close command.</value>
      Command CloseCommand { get; set; }

      /// <summary>
      /// Gets or sets the color of the theme.
      /// </summary>
      /// <value>The color of the theme.</value>
      Color ThemeColor { get; set; }

      /// <summary>
      /// Gets or sets the theme font family.
      /// </summary>
      /// <value>The theme font family.</value>
      string ThemeFontFamily { get; set; }

      /// <summary>
      /// Gets or sets the web URL.
      /// </summary>
      /// <value>The web URL.</value>
      string WebUrl { get; set; }
   }

   /// <summary>
   /// Class WebViewWithCloseButton.
   /// Implements the <see cref="ShapeView" />
   /// Implements the <see cref="IWebViewWithCloseButton" />
   /// </summary>
   /// <seealso cref="ShapeView" />
   /// <seealso cref="IWebViewWithCloseButton" />
   public class WebViewWithCloseButton : ShapeView, IWebViewWithCloseButton
   {
      /// <summary>
      /// The close command property
      /// </summary>
      public static readonly BindableProperty CloseCommandProperty =
         CreateDataAwareFlowableCollectionCanvasBindableProperty
         (
            nameof(CloseCommand),
            default(Command),
            BindingMode.OneWay,
            (
               webView,
               oldVal,
               newVal
            ) =>
            {
               webView.CloseCommand = newVal;
            }
         );

      /// <summary>
      /// The theme color property
      /// </summary>
      public static readonly BindableProperty ThemeColorProperty =
         CreateDataAwareFlowableCollectionCanvasBindableProperty
         (
            nameof(ThemeColor),
            default(Color),
            BindingMode.OneWay,
            (
               webView,
               oldVal,
               newVal
            ) =>
            {
               webView.ThemeColor = newVal;
            }
         );

      /// <summary>
      /// The theme font family property
      /// </summary>
      public static readonly BindableProperty ThemeFontFamilyProperty =
         CreateDataAwareFlowableCollectionCanvasBindableProperty
         (
            nameof(ThemeFontFamily),
            default(string),
            BindingMode.OneWay,
            (
               webView,
               oldVal,
               newVal
            ) =>
            {
               webView.ThemeFontFamily = newVal;
            }
         );

      /// <summary>
      /// The web URL property
      /// </summary>
      public static readonly BindableProperty WebUrlProperty =
         CreateDataAwareFlowableCollectionCanvasBindableProperty
         (
            nameof(WebUrl),
            default(string),
            BindingMode.OneWay,
            (
               webView,
               oldVal,
               newVal
            ) =>
            {
               webView.WebUrl = newVal;
            }
         );

      /// <summary>
      /// The close button image path
      /// </summary>
      private const string CLOSE_BUTTON_IMAGE_PATH = SharedImageUtils.IMAGE_PRE_PATH + "close_button.png";

      /// <summary>
      /// The close button margin
      /// </summary>
      private const double CLOSE_BUTTON_MARGIN = 4;

      /// <summary>
      /// The close button width height
      /// </summary>
      private const double CLOSE_BUTTON_WIDTH_HEIGHT = HEADER_HEIGHT - 2 * CLOSE_BUTTON_MARGIN;

      /// <summary>
      /// The header height
      /// </summary>
      private const double HEADER_HEIGHT = 48;

      ///// <summary>
      ///// The web view corner radius
      ///// </summary>
      //private const float  WEB_VIEW_CORNER_RADIUS    = 6;

      /// <summary>
      /// The web view padding
      /// </summary>
      private static readonly double WEB_VIEW_PADDING = 6.0.AdjustForOsAndDevice();

      /// <summary>
      /// The web view
      /// </summary>
      private readonly WebView _webView;

      /// <summary>
      /// The theme color
      /// </summary>
      private Color _themeColor = Color.Black;

      /// <summary>
      /// The web URL
      /// </summary>
      private string _webUrl;

      /// <summary>
      /// Initializes a new instance of the <see cref="WebViewWithCloseButton" /> class.
      /// </summary>
      public WebViewWithCloseButton()
      {
         var masterGrid = FormsUtils.GetExpandingGrid();

         masterGrid.AddStarColumn();
         masterGrid.AddFixedRow(HEADER_HEIGHT);
         masterGrid.AddStarRow();

         var headerGrid = FormsUtils.GetExpandingGrid();
         headerGrid.AddStarColumn();
         headerGrid.AddFixedColumn(HEADER_HEIGHT);

         var closeButtonImage = FormsUtils.GetImage(CLOSE_BUTTON_IMAGE_PATH, CLOSE_BUTTON_WIDTH_HEIGHT,
                                                    getFromResources: true,
                                                    resourceClass: typeof(SharedImageUtils));
         var tapGesture = new TapGestureRecognizer();
         tapGesture.Tapped += (sender, args) =>
                              {
                                 if (CloseCommand.IsNotNullOrDefault())
                                 {
                                    CloseCommand.Execute(null);
                                 }
                              };
         closeButtonImage.GestureRecognizers.Add(tapGesture);
         closeButtonImage.HorizontalOptions = LayoutOptions.Center;
         closeButtonImage.VerticalOptions = LayoutOptions.Center;
         headerGrid.Children.Add(closeButtonImage);
         Grid.SetColumn(closeButtonImage, 1);

         masterGrid.Children.Add(headerGrid);
         Grid.SetRow(headerGrid, 0);

         _webView = new WebView { BackgroundColor = Color.Transparent, Margin = WEB_VIEW_PADDING };

         masterGrid.Children.Add(_webView);
         Grid.SetRow(_webView, 1);

         BackgroundColor = Color.Transparent;
         CornerRadius = FormsConst.DEFAULT_CORNER_RADIUS_FIXED;
         IsClippedToBounds = true;

         Content = masterGrid;

         ApplyThemeColor();
      }

      /// <summary>
      /// Gets or sets the close command.
      /// </summary>
      /// <value>The close command.</value>
      public Command CloseCommand { get; set; }

      /// <summary>
      /// Gets or sets the color of the theme.
      /// </summary>
      /// <value>The color of the theme.</value>
      public Color ThemeColor
      {
         get => _themeColor;
         set
         {
            _themeColor = value;

            ApplyThemeColor();
         }
      }

      /// <summary>
      /// Gets or sets the theme font family.
      /// </summary>
      /// <value>The theme font family.</value>
      public string ThemeFontFamily { get; set; }

      /// <summary>
      /// Gets or sets the web URL.
      /// </summary>
      /// <value>The web URL.</value>
      public string WebUrl
      {
         get => _webUrl;
         set
         {
            _webUrl = value;

            if (_webView.IsNullOrDefault())
            {
               return;
            }

            _webView.Source = new UrlWebViewSource { Url = _webUrl };
         }
      }

      /// <summary>
      /// Creates the data aware flowable collection canvas bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateDataAwareFlowableCollectionCanvasBindableProperty<PropertyTypeT>
      (
         string localPropName,
         PropertyTypeT defaultVal =
            default,
         BindingMode bindingMode =
            BindingMode.OneWay,
         Action<WebViewWithCloseButton, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      /// Applies the color of the theme.
      /// </summary>
      private void ApplyThemeColor()
      {
         Color = ThemeColor;
      }
   }
}
