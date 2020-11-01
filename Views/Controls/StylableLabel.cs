// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, StylableLabel.cs, is a part of a program called AccountViewMobile.
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
   using Xamarin.Forms;

   /// <summary>
   /// Interface IStylableLabel
   /// Implements the <see cref="IHaveSelectionStylingHelper" />
   /// Implements the <see cref="IHaveSelectionProvider" />
   /// Implements the <see cref="IHaveSelectionAndAlternationProvider" />
   /// </summary>
   /// <seealso cref="IHaveSelectionStylingHelper" />
   /// <seealso cref="IHaveSelectionProvider" />
   /// <seealso cref="IHaveSelectionAndAlternationProvider" />
   public interface IStylableLabel : IHaveSelectionStylingHelper, IHaveSelectionProvider,
                                     IHaveSelectionAndAlternationProvider
   {
   }

   /// <summary>
   /// Class StylableLabel.
   /// Implements the <see cref="Xamarin.Forms.Label" />
   /// Implements the <see cref="IStylableLabel" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Label" />
   /// <seealso cref="IStylableLabel" />
   public class StylableLabel : Label, IStylableLabel
   {
      /// <summary>
      /// The selection and alternating style helper property
      /// </summary>
      public static readonly BindableProperty SelectionAndAlternatingStyleHelperProperty =
         CreateStylableLabelProperty
         (
            nameof(StylingHelper),
            default(ISelectionStylingHelper),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.StylingHelper = newVal;
            }
         );

      /// <summary>
      /// The am an alternate overridden
      /// </summary>
      private bool _amAnAlternateOverridden;

      /// <summary>
      /// The is selected overridden
      /// </summary>
      private bool _isSelectedOverridden;

      /// <summary>
      /// The selection style helper
      /// </summary>
      private ISelectionStylingHelper _selectionStyleHelper;

      /// <summary>
      /// Initializes a new instance of the <see cref="StylableLabel" /> class.
      /// </summary>
      /// <param name="selectionProvider">The selection provider.</param>
      public StylableLabel(ICanBeSelected selectionProvider)
      {
         SelectionProvider = selectionProvider;
      }

      /// <summary>
      /// Occurs when [i am an alternate overridden changed].
      /// </summary>
      public event EventUtils.GenericDelegate<bool> IAmAnAlternateOverriddenChanged;

      /// <summary>
      /// Occurs when [is selected overridden changed].
      /// </summary>
      public event EventUtils.GenericDelegate<bool> IsSelectedOverriddenChanged;

      /// <summary>
      /// Gets the default alternate deselected stylable label style.
      /// </summary>
      /// <value>The default alternate deselected stylable label style.</value>
      public static Style DefaultAlternateDeselectedStylableLabelStyle => DefaultDeselectedStylableLabelStyle;

      /// <summary>
      /// Gets the default deselected stylable label style.
      /// </summary>
      /// <value>The default deselected stylable label style.</value>
      public static Style DefaultDeselectedStylableLabelStyle =>
         CreateStylableLabelStyle(textAlignment: TextAlignment.Start);

      /// <summary>
      /// Gets the default selected stylable label style.
      /// </summary>
      /// <value>The default selected stylable label style.</value>
      public static Style DefaultSelectedStylableLabelStyle =>
         CreateStylableLabelStyle(textAlignment: TextAlignment.Start, fontAttributes: FontAttributes.Bold);

      /// <summary>
      /// Gets or sets a value indicating whether [i am an alternate overridden].
      /// </summary>
      /// <value><c>true</c> if [i am an alternate overridden]; otherwise, <c>false</c>.</value>
      public bool IAmAnAlternateOverridden
      {
         get => _amAnAlternateOverridden;
         set
         {
            _amAnAlternateOverridden = value;
            IAmAnAlternateOverriddenChanged?.Invoke(_amAnAlternateOverridden);
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is selected overridden.
      /// </summary>
      /// <value><c>true</c> if this instance is selected overridden; otherwise, <c>false</c>.</value>
      public bool IsSelectedOverridden
      {
         get => _isSelectedOverridden;
         set
         {
            _isSelectedOverridden = value;
            IsSelectedOverriddenChanged?.Invoke(_isSelectedOverridden);
         }
      }

      /// <summary>
      /// Gets or sets the selection and alternation provider.
      /// </summary>
      /// <value>The selection and alternation provider.</value>
      public ICanOverrideSelectionAndAlternation SelectionAndAlternationProvider { get; set; }

      /// <summary>
      /// Gets or sets the selection provider.
      /// </summary>
      /// <value>The selection provider.</value>
      public ICanBeSelected SelectionProvider { get; set; }

      /// <summary>
      /// Gets or sets the styling helper.
      /// </summary>
      /// <value>The styling helper.</value>
      public ISelectionStylingHelper StylingHelper
      {
         get => _selectionStyleHelper;
         set
         {
            if (value.CanAttach(this))
            {
               _selectionStyleHelper = value;
            }
         }
      }

      /// <summary>
      /// Creates the stylable label property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateStylableLabelProperty<PropertyTypeT>
      (
         string localPropName,
         PropertyTypeT defaultVal =
            default,
         BindingMode bindingMode =
            BindingMode.OneWay,
         Action<StylableLabel, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      /// Creates the stylable label style.
      /// </summary>
      /// <param name="textColor">Color of the text.</param>
      /// <param name="backColor">Color of the back.</param>
      /// <param name="textAlignment">The text alignment.</param>
      /// <param name="fontNamedSize">Size of the font named.</param>
      /// <param name="fontSize">Size of the font.</param>
      /// <param name="fontAttributes">The font attributes.</param>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      /// <param name="breakMode">The break mode.</param>
      /// <returns>Style.</returns>
      public static Style CreateStylableLabelStyle
      (
         Color? textColor = default,
         Color? backColor = default,
         TextAlignment textAlignment = TextAlignment.Center,
         NamedSize? fontNamedSize = default,
         double fontSize = 0.0,
         FontAttributes fontAttributes = FontAttributes.None,
         double width = 0,
         double height = 0,
         LineBreakMode breakMode = LineBreakMode.WordWrap
      )
      {
         var retStyle = new Style(typeof(StylableLabel));

         if (textColor.HasValue)
         {
            retStyle.Setters.Add(TextColorProperty, textColor.GetValueOrDefault());
         }
         else
         {
            retStyle.Setters.Add(TextColorProperty, Color.Black);
         }

         if (backColor.HasValue)
         {
            retStyle.Setters.Add(BackgroundColorProperty, backColor.GetValueOrDefault());
         }
         else
         {
            retStyle.Setters.Add(BackgroundColorProperty, Color.Transparent);
         }

         if (textAlignment.IsAnEqualObjectTo(default(TextAlignment)))
         {
            retStyle.Setters.Add(HorizontalTextAlignmentProperty, TextAlignment.Center);
         }
         else
         {
            retStyle.Setters.Add(HorizontalTextAlignmentProperty, textAlignment);
         }

         if (fontNamedSize.HasValue)
         {
            retStyle.Setters.Add(FontSizeProperty,
                                 Device.GetNamedSize(fontNamedSize.GetValueOrDefault(), typeof(Label)));
         }
         else if (fontSize.IsNotEmpty())
         {
            retStyle.Setters.Add(FontSizeProperty, fontSize);
         }
         else
         {
            retStyle.Setters.Add(FontSizeProperty, Device.GetNamedSize(NamedSize.Small, typeof(Label)));
         }

         if (fontAttributes.IsNotAnEqualObjectTo(default(FontAttributes)))
         {
            retStyle.Setters.Add(FontAttributesProperty, fontAttributes);
         }

         if (width.IsNotEmpty())
         {
            retStyle.Setters.Add(WidthProperty, width);
         }

         if (height.IsNotEmpty())
         {
            retStyle.Setters.Add(HeightProperty, height);
         }

         if (breakMode.IsAnEqualObjectTo(default(LineBreakMode)))
         {
            retStyle.Setters.Add(LineBreakModeProperty, LineBreakMode.WordWrap);
         }
         else
         {
            retStyle.Setters.Add(LineBreakModeProperty, breakMode);
         }

         return retStyle;
      }

      /// <summary>
      /// Afters the style applied.
      /// </summary>
      public virtual void AfterStyleApplied()
      {
      }
   }
}
