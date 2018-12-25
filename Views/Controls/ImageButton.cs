// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="ImageButton.cs" company="Marcus Technical Services, Inc.">
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

namespace SharedForms.Views.Controls
{
   using System;
   using Com.MarcusTS.SharedForms.Utils;
   using Com.MarcusTS.SharedForms.Views.Controls;
   using Com.MarcusTS.SharedUtils.Interfaces;
   using Com.MarcusTS.SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IImageButton
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IGenericViewButtonBase{Xamarin.Forms.Image}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IGenericViewButtonBase{Xamarin.Forms.Image}" />
   public interface IImageButton : IGenericViewButtonBase<Image>
   {
      #region Public Properties

      /// <summary>
      /// Gets or sets the image file name root.
      /// </summary>
      /// <value>The image file name root.</value>
      string ImageFileNameRoot { get; set; }

      /// <summary>
      /// Gets or sets the height of the image.
      /// </summary>
      /// <value>The height of the image.</value>
      double ImageHeight { get; set; }
      /// <summary>
      /// Gets or sets the width of the image.
      /// </summary>
      /// <value>The width of the image.</value>
      double ImageWidth  { get; set; }

      #endregion Public Properties
   }

   /// <summary>
   /// Class ImageButton.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.GenericViewButtonBase{Xamarin.Forms.Image}" />
   /// Implements the <see cref="SharedForms.Views.Controls.IImageButton" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.GenericViewButtonBase{Xamarin.Forms.Image}" />
   /// <seealso cref="SharedForms.Views.Controls.IImageButton" />
   public class ImageButton : GenericViewButtonBase<Image>, IImageButton
   {
      #region Public Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="ImageButton" /> class.
      /// </summary>
      public ImageButton()
      {
         SetStyle();
      }

      #endregion Public Constructors

      #region Public Methods

      /// <summary>
      /// Creates the image button bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateImageButtonBindableProperty<PropertyTypeT>(string localPropName,
                                                                                      PropertyTypeT defaultVal =
                                                                                         default(PropertyTypeT),
                                                                                      BindingMode bindingMode =
                                                                                         BindingMode.OneWay,
                                                                                      Action<ImageButton, PropertyTypeT,
                                                                                            PropertyTypeT>
                                                                                         callbackAction =
                                                                                         null)
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      #endregion Public Methods

      #region Protected Methods

      /// <summary>
      /// Sets the style.
      /// </summary>
      protected override void SetStyle()
      {
         if (_setStyleEntered)
         {
            return;
         }

         _setStyleEntered = true;

         base.SetStyle();

         CallRecreateImageSafely();

         _setStyleEntered = false;
      }

      #endregion Protected Methods

      #region Public Fields

      /// <summary>
      /// The image file name root property
      /// </summary>
      public static readonly BindableProperty ImageFileNameRootProperty =
         CreateImageButtonBindableProperty
            (
             nameof(ImageFileNameRoot),
             default(string),
             BindingMode.OneWay,
             (imageButton,
              oldVal,
              newVal) =>
             {
                imageButton.ImageFileNameRoot = newVal;
             }
            );

      //---------------------------------------------------------------------------------------------------------------
      // CONSTRUCTOR
      //---------------------------------------------------------------------------------------------------------------
      //---------------------------------------------------------------------------------------------------------------
      // CONSTANTS
      //---------------------------------------------------------------------------------------------------------------
      /// <summary>
      /// The image height property
      /// </summary>
      public static readonly BindableProperty ImageHeightProperty =
         CreateImageButtonBindableProperty
            (
             nameof(ImageHeight),
             default(double),
             BindingMode.OneWay,
             (imageButton,
              oldVal,
              newVal) =>
             {
                imageButton.ImageHeight = newVal;
             }
            );

      /// <summary>
      /// The image width property
      /// </summary>
      public static readonly BindableProperty ImageWidthProperty =
         CreateImageButtonBindableProperty
            (
             nameof(ImageWidth),
             default(double),
             BindingMode.OneWay,
             (imageButton,
              oldVal,
              newVal) =>
             {
                imageButton.ImageWidth = newVal;
             }
            );

      #endregion Public Fields

      #region Private Fields

      /// <summary>
      /// The disabled suffix
      /// </summary>
      private const string DISABLED_SUFFIX = "_disabled";

      /// <summary>
      /// The PNG suffix
      /// </summary>
      private const string PNG_SUFFIX = ".png";

      /// <summary>
      /// The selected suffix
      /// </summary>
      private const string SELECTED_SUFFIX = "_selected";

      /// <summary>
      /// The image file name root
      /// </summary>
      private string _imageFileNameRoot;

      //---------------------------------------------------------------------------------------------------------------
      // PROPERTIES - Public
      //---------------------------------------------------------------------------------------------------------------
      /// <summary>
      /// The image height
      /// </summary>
      private double _imageHeight;

      /// <summary>
      /// The image width
      /// </summary>
      private double _imageWidth;

      /// <summary>
      /// The last image file name
      /// </summary>
      private string _lastImageFileName;

      /// <summary>
      /// The set style entered
      /// </summary>
      private bool _setStyleEntered;

      #endregion Private Fields

      #region Public Properties

      //---------------------------------------------------------------------------------------------------------------
      // VARIABLES
      //---------------------------------------------------------------------------------------------------------------
      /// <summary>
      /// Gets or sets the image file name root.
      /// </summary>
      /// <value>The image file name root.</value>
      public string ImageFileNameRoot
      {
         get => _imageFileNameRoot;
         set
         {
            if (_imageFileNameRoot.IsDifferentThan(value))
            {
               _imageFileNameRoot = value;
               CallRecreateImageSafely();
            }
         }
      }

      //---------------------------------------------------------------------------------------------------------------
      // METHODS - Protected
      //---------------------------------------------------------------------------------------------------------------
      /// <summary>
      /// Gets or sets the height of the image.
      /// </summary>
      /// <value>The height of the image.</value>
      public double ImageHeight
      {
         get => _imageHeight;
         set
         {
            if (_imageHeight.IsDifferentThan(value))
            {
               _imageHeight = value;
               CallRecreateImageSafely();
            }
         }
      }

      /// <summary>
      /// Gets or sets the width of the image.
      /// </summary>
      /// <value>The width of the image.</value>
      public double ImageWidth
      {
         get => _imageWidth;
         set
         {
            if (_imageWidth.IsDifferentThan(value))
            {
               _imageWidth = value;
               CallRecreateImageSafely();
            }
         }
      }

      #endregion Public Properties

      //---------------------------------------------------------------------------------------------------------------
      // METHODS - Private
      //---------------------------------------------------------------------------------------------------------------

      #region Private Methods

      /// <summary>
      /// Calls the recreate image safely.
      /// </summary>
      private void CallRecreateImageSafely()
      {
         if (ThreadHelper.IsOnMainThread)
         {
            RecreateImage();
         }
         else
         {
            Device.BeginInvokeOnMainThread(RecreateImage);
         }
      }

      /// <summary>
      /// Recreates the image.
      /// </summary>
      private void RecreateImage()
      {
         if (ImageWidth.IsEmpty() && ImageHeight.IsEmpty())
         {
            return;
         }

         var imageFileName = _imageFileNameRoot;

         // If no selection, just use the root file name.
         if (CanSelect)
         {
            //Determine the current file name
            switch (ButtonState)
            {
               case ButtonStates.Selected:
                  imageFileName += SELECTED_SUFFIX;
                  break;

               case ButtonStates.Disabled:
                  imageFileName += DISABLED_SUFFIX;
                  break;
            }
         }

         if (imageFileName.IsEmpty())
         {
            return;
         }

         // InternalView = null;

         if (!imageFileName.EndsWith(PNG_SUFFIX))
         {
            imageFileName += PNG_SUFFIX;
         }

         if (imageFileName.IsSameAs(_lastImageFileName))
         {
            return;
         }

         InternalView = FormsUtils.GetImage(imageFileName, ImageWidth, ImageHeight);

         // The image always has a transparent background
         InternalView.BackgroundColor = Color.Transparent;

         InternalView.InputTransparent = true;

         _lastImageFileName = imageFileName;
      }

      #endregion Private Methods

      //---------------------------------------------------------------------------------------------------------------
      // BINDABLE PROPERTIES
      //---------------------------------------------------------------------------------------------------------------
   }
}