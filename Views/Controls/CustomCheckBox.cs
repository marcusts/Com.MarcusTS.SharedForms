// ***********************************************************************
// Assembly         : Com.MarcusTS.LifesAStage
// Author           : steph
// Created          : 08-06-2019
//
// Last Modified By : steph
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="CustomCheckBox.cs" company="Com.MarcusTS.LifesAStage">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>

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
   using System;
   using System.ComponentModel;
   using System.Globalization;
   using Common.Images;
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   /// Interface ICustomCheckBox
   /// </summary>
   public interface ICustomCheckBox : INotifyPropertyChanged
   {
      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="ICustomCheckBox"/> is checked.
      /// </summary>
      /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
      bool IsChecked { get; set; }
      /// <summary>
      /// Occurs when [checked changed].
      /// </summary>
      event EventHandler<bool> IsCheckedChanged;

      double WidthHeight { get; set; }
   }

   /// <summary>
   /// Class CustomCheckBox.
   /// Implements the <see cref="Xamarin.Forms.Image" />
   /// Implements the <see cref="ICustomCheckBox" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Image" />
   /// <seealso cref="ICustomCheckBox" />
   public class CustomCheckBox : Image, ICustomCheckBox
   {
      private const double DEFAULT_WIDTH_HEIGHT = 24;

      /// <summary>
      /// The checkbox checked image
      /// </summary>
      private const string CHECKBOX_CHECKED_IMAGE    = SharedImageUtils.IMAGE_PRE_PATH + "checkbox_checked.png";
      /// <summary>
      /// The checkbox un checked image
      /// </summary>
      private const string CHECKBOX_UN_CHECKED_IMAGE = SharedImageUtils.IMAGE_PRE_PATH + "checkbox_unchecked.png";

      public static readonly BindableProperty IsCheckedProperty =
         CreateCheckBoxBindableProperty
         (
            nameof(IsChecked),
            default(bool),
            BindingMode.TwoWay,
            (checkBox, oldVal, newVal )=>
            {
               checkBox.IsCheckedChanged?.Invoke(checkBox, checkBox.IsChecked);
            }
         );

      private double _widthHeight = DEFAULT_WIDTH_HEIGHT;

      /// <summary>
      /// Initializes a new instance of the <see cref="CustomCheckBox"/> class.
      /// </summary>
      public CustomCheckBox()
      {
         WidthRequest  = DEFAULT_WIDTH_HEIGHT;
         HeightRequest = DEFAULT_WIDTH_HEIGHT;
         Source        = CHECKBOX_UN_CHECKED_IMAGE;
         Aspect = Aspect.AspectFit;
         var imageTapGesture = new TapGestureRecognizer();
         imageTapGesture.Tapped += ImageTapGestureOnTapped;
         GestureRecognizers.Add(imageTapGesture);
         PropertyChanged += OnPropertyChanged;

         this.SetUpBinding(Image.SourceProperty, nameof(IsChecked), converter:IsCheckedToImageSourceConverter.StaticIsCheckedToImageSourceConverter, source:this);
      }

      /// <summary>
      /// The checked changed event.
      /// </summary>
      public event EventHandler<bool> IsCheckedChanged;

      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="CustomCheckBox"/> is checked.
      /// </summary>
      /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
      public bool IsChecked
      {
         get => (bool) GetValue(IsCheckedProperty);
         set => SetValue(IsCheckedProperty, value);
      }

      /// <summary>
      /// Images the tap gesture on tapped.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
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
      /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
      private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         if (e.IsNotNullOrDefault() && e.PropertyName.IsSameAs(nameof(IsEnabled)))
         {
            Opacity = IsEnabled ? FormsUtils.VISIBLE_OPACITY : FormsUtils.VISIBLE_OPACITY / 2;
         }
      }

      public static BindableProperty CreateCheckBoxBindableProperty<PropertyTypeT>
      (
         string                                                 localPropName,
         PropertyTypeT                                          defaultVal     = default,
         BindingMode                                            bindingMode    = BindingMode.OneWay,
         Action<CustomCheckBox, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      public double WidthHeight
      {
         get => _widthHeight;
         set
         {
            _widthHeight = value;
            WidthRequest  = _widthHeight;
            HeightRequest = _widthHeight;
         }
      }

      private class IsCheckedToImageSourceConverter : IValueConverter
      {
         public static readonly IsCheckedToImageSourceConverter StaticIsCheckedToImageSourceConverter =
            new IsCheckedToImageSourceConverter();

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

         public object ConvertBack
         (
            object      value,
            Type        targetType,
            object      parameter,
            CultureInfo culture
         )
         {
            return (value is ImageSource valueAsImageSource &&
                    valueAsImageSource.IsAnEqualReferenceTo(
                       ImageSource.FromResource(CHECKBOX_CHECKED_IMAGE, typeof(SharedImageUtils).Assembly)));
         }
      }
   }
}
