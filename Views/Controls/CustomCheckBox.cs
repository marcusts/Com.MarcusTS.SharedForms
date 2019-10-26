#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, CustomCheckBox.cs, is a part of a program called AccountViewMobile.
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

#endregion

// MIT License

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// ***********************************************************************
//

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Images;
   using Common.Utils;
   using SharedUtils.Utils;
   using System;
   using System.ComponentModel;
   using System.Globalization;
   using Xamarin.Forms;

   /// <summary>
   /// Interface ICustomCheckBox
   /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface ICustomCheckBox : INotifyPropertyChanged
   {
      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="ICustomCheckBox" /> is checked.
      /// </summary>
      /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
      bool IsChecked { get; set; }

      /// <summary>
      /// Gets or sets the height of the width.
      /// </summary>
      /// <value>The height of the width.</value>
      double WidthHeight { get; set; }

      /// <summary>
      /// Occurs when [checked changed].
      /// </summary>
      event EventHandler<bool> IsCheckedChanged;
   }

   /// <summary>
   /// Class CustomCheckBox.
   /// Implements the <see cref="Xamarin.Forms.Image" />
   /// Implements the <see cref="ICustomCheckBox" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ICustomCheckBox" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ICustomCheckBox" />
   /// <seealso cref="Xamarin.Forms.Image" />
   /// <seealso cref="ICustomCheckBox" />
   public class CustomCheckBox : Image, ICustomCheckBox
   {
      /// <summary>
      /// The checkbox checked image
      /// </summary>
      private const string CHECKBOX_CHECKED_IMAGE = SharedImageUtils.IMAGE_PRE_PATH + "checkbox_checked.png";

      /// <summary>
      /// The checkbox un checked image
      /// </summary>
      private const string CHECKBOX_UN_CHECKED_IMAGE = SharedImageUtils.IMAGE_PRE_PATH + "checkbox_unchecked.png";

      /// <summary>
      /// The default width height
      /// </summary>
      private const double DEFAULT_WIDTH_HEIGHT = 24;

      /// <summary>
      /// The is checked property
      /// </summary>
      public static readonly BindableProperty IsCheckedProperty =
         CreateCheckBoxBindableProperty
         (
            nameof(IsChecked),
            default(bool),
            BindingMode.TwoWay,
            (checkBox, oldVal, newVal) => { checkBox.IsCheckedChanged?.Invoke(checkBox, checkBox.IsChecked); }
         );

      /// <summary>
      /// The width height
      /// </summary>
      private double _widthHeight = DEFAULT_WIDTH_HEIGHT;

      /// <summary>
      /// Initializes a new instance of the <see cref="CustomCheckBox" /> class.
      /// </summary>
      public CustomCheckBox()
      {
         WidthRequest  = DEFAULT_WIDTH_HEIGHT;
         HeightRequest = DEFAULT_WIDTH_HEIGHT;
         Source        = CHECKBOX_UN_CHECKED_IMAGE;
         Aspect        = Aspect.AspectFit;
         var imageTapGesture = new TapGestureRecognizer();
         imageTapGesture.Tapped += ImageTapGestureOnTapped;
         GestureRecognizers.Add(imageTapGesture);
         PropertyChanged += OnPropertyChanged;

         this.SetUpBinding(SourceProperty, nameof(IsChecked),
                           converter: IsCheckedToImageSourceConverter.StaticIsCheckedToImageSourceConverter,
                           source: this);
      }

      /// <summary>
      /// The checked changed event.
      /// </summary>
      public event EventHandler<bool> IsCheckedChanged;

      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="CustomCheckBox" /> is checked.
      /// </summary>
      /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
      public bool IsChecked
      {
         get => (bool) GetValue(IsCheckedProperty);
         set => SetValue(IsCheckedProperty, value);
      }

      /// <summary>
      /// Gets or sets the height of the width.
      /// </summary>
      /// <value>The height of the width.</value>
      public double WidthHeight
      {
         get => _widthHeight;
         set
         {
            _widthHeight  = value;
            WidthRequest  = _widthHeight;
            HeightRequest = _widthHeight;
         }
      }

      /// <summary>
      /// Creates the CheckBox bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateCheckBoxBindableProperty<PropertyTypeT>
      (
         string                                               localPropName,
         PropertyTypeT                                        defaultVal     = default,
         BindingMode                                          bindingMode    = BindingMode.OneWay,
         Action<CustomCheckBox, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      /// Images the tap gesture on tapped.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="eventArgs">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void ImageTapGestureOnTapped(object sender, EventArgs eventArgs)
      {
         if (IsEnabled)
         {
            IsChecked = !IsChecked;
         }
      }

      /// <summary>
      /// Handles the <see cref="E:PropertyChanged" /> event.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
      private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         if (e.IsNotNullOrDefault() && e.PropertyName.IsSameAs(nameof(IsEnabled)))
         {
            Opacity = IsEnabled ? FormsConst.VISIBLE_OPACITY : FormsConst.VISIBLE_OPACITY / 2;
         }
      }

      /// <summary>
      /// Class IsCheckedToImageSourceConverter.
      /// Implements the <see cref="Xamarin.Forms.IValueConverter" />
      /// </summary>
      /// <seealso cref="Xamarin.Forms.IValueConverter" />
      private class IsCheckedToImageSourceConverter : IValueConverter
      {
         /// <summary>
         /// The static is checked to image source converter
         /// </summary>
         public static readonly IsCheckedToImageSourceConverter StaticIsCheckedToImageSourceConverter =
            new IsCheckedToImageSourceConverter();

         /// <summary>
         /// Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
         /// </summary>
         /// <param name="value">The value to convert.</param>
         /// <param name="targetType">The type to which to convert the value.</param>
         /// <param name="parameter">A parameter to use during the conversion.</param>
         /// <param name="culture">The culture to use during the conversion.</param>
         /// <returns>To be added.</returns>
         /// <remarks>To be added.</remarks>
         public object Convert
         (
            object      value,
            Type        targetType,
            object      parameter,
            CultureInfo culture
         )
         {
            if (value is bool b && b)
            {
               return ImageSource.FromResource(CHECKBOX_CHECKED_IMAGE, typeof(SharedImageUtils).Assembly);
            }

            return ImageSource.FromResource(CHECKBOX_UN_CHECKED_IMAGE, typeof(SharedImageUtils).Assembly);
         }

         /// <summary>
         /// Converts the back.
         /// </summary>
         /// <param name="value">The value.</param>
         /// <param name="targetType">Type of the target.</param>
         /// <param name="parameter">The parameter.</param>
         /// <param name="culture">The culture.</param>
         /// <returns>System.Object.</returns>
         public object ConvertBack
         (
            object      value,
            Type        targetType,
            object      parameter,
            CultureInfo culture
         )
         {
            return value is ImageSource valueAsImageSource &&
                   valueAsImageSource.IsAnEqualReferenceTo(
                      ImageSource.FromResource(CHECKBOX_CHECKED_IMAGE, typeof(SharedImageUtils).Assembly));
         }
      }
   }
}