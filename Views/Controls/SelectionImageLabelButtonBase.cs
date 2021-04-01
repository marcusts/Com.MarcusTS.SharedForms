// *********************************************************************************
// <copyright file=SelectionImageLabelButtonBase.cs company="Marcus Technical Services, Inc.">
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
   using System;
   using Common.Utils;
   using Xamarin.Forms;

   /// <summary>
   ///    Interface ISelectionImageLabelButton
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IImageLabelButton" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IImageLabelButton" />
   public interface ISelectionImageLabelButton : IImageLabelButton
   {
      /// <summary>
      ///    Gets or sets a value indicating whether [get image from resource].
      /// </summary>
      /// <value><c>true</c> if [get image from resource]; otherwise, <c>false</c>.</value>
      bool GetImageFromResource { get; set; }

      /// <summary>
      ///    Gets or sets the type of the image resource class.
      /// </summary>
      /// <value>The type of the image resource class.</value>
      Type ImageResourceClassType { get; set; }

      /// <summary>
      ///    Gets or sets a value indicating whether [multi select allowed].
      /// </summary>
      /// <value><c>true</c> if [multi select allowed]; otherwise, <c>false</c>.</value>
      bool MultiSelectAllowed { get; set; }
   }

   /// <summary>
   ///    A button that can select an item among a master collection of items.
   ///    Multi-select is supported.
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ImageLabelButtonBase" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ISelectionImageLabelButton" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ImageLabelButtonBase" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ISelectionImageLabelButton" />
   public abstract class SelectionImageLabelButtonBase : ImageLabelButtonBase, ISelectionImageLabelButton
   {
      /// <summary>
      ///    The get image from resource property
      /// </summary>
      public static readonly BindableProperty GetImageFromResourceProperty =
         CreateSelectionImageLabelButtonBindableProperty
         (
            nameof(GetImageFromResource),
            true
         );

      /// <summary>
      ///    The image resource class type property
      /// </summary>
      public static readonly BindableProperty ImageResourceClassTypeProperty =
         CreateSelectionImageLabelButtonBindableProperty
         (
            nameof(ImageResourceClassType),
            default(Type)
         );

      /// <summary>
      ///    The multi select allowed property
      /// </summary>
      public static readonly BindableProperty MultiSelectAllowedProperty =
         CreateSelectionImageLabelButtonBindableProperty
         (
            nameof(MultiSelectAllowed),
            default(bool)
         );

      /// <summary>
      ///    Initializes a new instance of the <see cref="SelectionImageLabelButtonBase" /> class.
      /// </summary>
      protected SelectionImageLabelButtonBase()
      {
         ImageResourceClassType    = GetType();
         SelectionStyle            = ImageLabelButtonSelectionStyles.ToggleSelectionThroughAllStyles;
         ButtonLabel               = FormsUtils.GetSimpleLabel();
         UpdateButtonTextFromStyle = true;
      }

      /// <summary>
      ///    Gets a value indicating whether this instance is disabled.
      /// </summary>
      /// <value><c>true</c> if this instance is disabled; otherwise, <c>false</c>.</value>
      protected override bool IsDisabled => false;

      /// <summary>
      ///    Gets or sets a value indicating whether [get image from resource].
      /// </summary>
      /// <value><c>true</c> if [get image from resource]; otherwise, <c>false</c>.</value>
      public bool GetImageFromResource
      {
         get => (bool) GetValue(GetImageFromResourceProperty);
         set => SetValue(GetImageFromResourceProperty, value);
      }

      /// <summary>
      ///    Gets or sets the type of the image resource class.
      /// </summary>
      /// <value>The type of the image resource class.</value>
      public Type ImageResourceClassType
      {
         get => (Type) GetValue(ImageResourceClassTypeProperty);
         set => SetValue(ImageResourceClassTypeProperty, value);
      }

      /// <summary>
      ///    Gets a value indicating whether this instance is selected.
      /// </summary>
      /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
      public override bool IsSelected => false;

      /// <summary>
      ///    Gets or sets a value indicating whether [multi select allowed].
      /// </summary>
      /// <value><c>true</c> if [multi select allowed]; otherwise, <c>false</c>.</value>
      public bool MultiSelectAllowed
      {
         get => (bool) GetValue(MultiSelectAllowedProperty);
         set => SetValue(MultiSelectAllowedProperty, value);
      }

      /// <summary>
      ///    Creates the selection image label button bindable property.
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
      ///    Deselects this instance.
      /// </summary>
      protected override void Deselect()
      {
         // Do nothing
      }

      /// <summary>
      ///    Called when [button command created].
      /// </summary>
      protected override void OnButtonCommandCreated()
      {
         // Do Nothing
      }
   }
}