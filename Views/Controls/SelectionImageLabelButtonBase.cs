#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, SelectionImageLabelButtonBase.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Utils;
   using System;
   using Xamarin.Forms;

   /// <summary>
   /// Interface ISelectionImageLabelButton
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IImageLabelButton" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IImageLabelButton" />
   public interface ISelectionImageLabelButton : IImageLabelButton
   {
      /// <summary>
      /// Gets or sets a value indicating whether [get image from resource].
      /// </summary>
      /// <value><c>true</c> if [get image from resource]; otherwise, <c>false</c>.</value>
      bool GetImageFromResource { get; set; }

      /// <summary>
      /// Gets or sets the type of the image resource class.
      /// </summary>
      /// <value>The type of the image resource class.</value>
      Type ImageResourceClassType { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [multi select allowed].
      /// </summary>
      /// <value><c>true</c> if [multi select allowed]; otherwise, <c>false</c>.</value>
      bool MultiSelectAllowed { get; set; }
   }

   /// <summary>
   /// A button that can select an item among a master collection of items.
   /// Multi-select is supported.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ImageLabelButtonBase" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ISelectionImageLabelButton" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ImageLabelButtonBase" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ISelectionImageLabelButton" />
   public abstract class SelectionImageLabelButtonBase : ImageLabelButtonBase, ISelectionImageLabelButton
   {
      /// <summary>
      /// The get image from resource property
      /// </summary>
      public static readonly BindableProperty GetImageFromResourceProperty =
         CreateSelectionImageLabelButtonBindableProperty
         (
            nameof(GetImageFromResource),
            default(bool),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.GetImageFromResource = newVal;
            }
         );

      /// <summary>
      /// The image resource class type property
      /// </summary>
      public static readonly BindableProperty ImageResourceClassTypeProperty =
         CreateSelectionImageLabelButtonBindableProperty
         (
            nameof(ImageResourceClassType),
            default(Type),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ImageResourceClassType = newVal;
            }
         );

      /// <summary>
      /// The multi select allowed property
      /// </summary>
      public static readonly BindableProperty MultiSelectAllowedProperty =
         CreateSelectionImageLabelButtonBindableProperty
         (
            nameof(MultiSelectAllowed),
            default(bool),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.MultiSelectAllowed = newVal;
            }
         );

      /// <summary>
      /// Initializes a new instance of the <see cref="SelectionImageLabelButtonBase" /> class.
      /// </summary>
      protected SelectionImageLabelButtonBase()
      {
         ImageResourceClassType = GetType();
         SelectionStyle         = ImageLabelButtonSelectionStyles.ToggleSelectionThroughAllStyles;
         ButtonLabel            = FormsUtils.GetSimpleLabel();
         LabelPos               = FormsConst.OnScreenPositions.CENTER;
         ImagePos               = FormsConst.OnScreenPositions.NONE;
      }

      /// <summary>
      /// Gets a value indicating whether this instance is disabled.
      /// </summary>
      /// <value><c>true</c> if this instance is disabled; otherwise, <c>false</c>.</value>
      protected override bool IsDisabled => false;

      /// <summary>
      /// Gets or sets a value indicating whether [get image from resource].
      /// </summary>
      /// <value><c>true</c> if [get image from resource]; otherwise, <c>false</c>.</value>
      public bool GetImageFromResource { get; set; } = true;

      /// <summary>
      /// Gets or sets the type of the image resource class.
      /// </summary>
      /// <value>The type of the image resource class.</value>
      public Type ImageResourceClassType { get; set; }

      /// <summary>
      /// Gets a value indicating whether this instance is selected.
      /// </summary>
      /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
      public override bool IsSelected => false;

      /// <summary>
      /// Gets or sets a value indicating whether [multi select allowed].
      /// </summary>
      /// <value><c>true</c> if [multi select allowed]; otherwise, <c>false</c>.</value>
      public bool MultiSelectAllowed { get; set; }

      /// <summary>
      /// Required by this case; each style has its own text.
      /// </summary>
      /// <value><c>true</c> if [update button text from style]; otherwise, <c>false</c>.</value>
      public override bool UpdateButtonTextFromStyle => true;

      /// <summary>
      /// Creates the selection image label button bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateSelectionImageLabelButtonBindableProperty<PropertyTypeT>
      (
         string                                                              localPropName,
         PropertyTypeT                                                       defaultVal     = default,
         BindingMode                                                         bindingMode    = BindingMode.OneWay,
         Action<SelectionImageLabelButtonBase, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      /// Deselects this instance.
      /// </summary>
      protected override void Deselect()
      {
         // Do nothing
      }

      /// <summary>
      /// Called when [button command created].
      /// </summary>
      protected override void OnButtonCommandCreated()
      {
         // Do Nothing
      }
   }
}