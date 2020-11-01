// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ShapeView.cs, is a part of a program called AccountViewMobile.
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
   using System;
   using Xamarin.Forms;
   using Xamarin.Forms.PancakeView;

   /// <summary>
   /// Class ShapeView.
   /// Implements the <see cref="Xamarin.Forms.PancakeView.PancakeView" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.PancakeView.PancakeView" />
   public class ShapeView : PancakeView
   {
      /// <summary>
      /// The color property
      /// </summary>
      public static readonly BindableProperty ColorProperty =
         CreateValidatableViewBindableProperty
         (
            nameof(Color),
            default(Color),
            BindingMode.OneWay,
            (view, oldVal, newVal) =>
            {
               // view.Color                        = newVal;
               //view.BackgroundGradientStartColor = newVal;
               //view.BackgroundGradientEndColor   = newVal;
               view.BackgroundColor = newVal;
               view.IsClippedToBounds = true;
            }
         );

      /// <summary>
      /// Initializes a new instance of the <see cref="ShapeView" /> class.
      /// </summary>
      public ShapeView()
      {
         Margin = 0;
         Padding = 0;
         //BackgroundColor = Color.Transparent;
         //Color = Color.White;
      }

      public new Color BorderColor => Border?.Color ?? default;

      public new float BorderThickness => Border?.Thickness ?? default;

      /// <summary>
      /// Gets or sets the color.
      /// </summary>
      /// <value>The color.</value>
      public Color Color
      {
         get => (Color)GetValue(ColorProperty);

         set => SetValue(ColorProperty, value);
      }

      /// <summary>
      /// Creates the validatable view bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateValidatableViewBindableProperty<PropertyTypeT>
    (
       string localPropName,
       PropertyTypeT defaultVal = default,
       BindingMode bindingMode = BindingMode.OneWay,
       Action<ShapeView, PropertyTypeT, PropertyTypeT> callbackAction = null
    )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }
   }
}
