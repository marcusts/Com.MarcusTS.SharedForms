// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="LabelButton.cs" company="Marcus Technical Services, Inc.">
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
#region License

// MIT License
//
// Copyright (c) 2018 Marcus Technical Services, Inc. http://www.marcusts.com
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
// associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT
// OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion License

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   #region Imports

   using System;
   using SharedUtils.Interfaces;
   using Utils;
   using Xamarin.Forms;

   #endregion Imports

   /// <summary>
   /// Interface ILabelButton
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IGenericViewButtonBase{Xamarin.Forms.Label}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IGenericViewButtonBase{Xamarin.Forms.Label}" />
   public interface ILabelButton : IGenericViewButtonBase<Label>
   { }

   /// <summary>
   /// Class LabelButton.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.GenericViewButtonBase{Xamarin.Forms.Label}" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ILabelButton" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.GenericViewButtonBase{Xamarin.Forms.Label}" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ILabelButton" />
   public class LabelButton : GenericViewButtonBase<Label>, ILabelButton
   {
      /// <summary>
      /// The deselected label button style
      /// </summary>
      private Style _deselectedLabelButtonStyle;
      /// <summary>
      /// The disabled label button style
      /// </summary>
      private Style _disabledLabelButtonStyle;
      /// <summary>
      /// The selected label button style
      /// </summary>
      private Style _selectedLabelButtonStyle;

      /// <summary>
      /// Initializes a new instance of the <see cref="LabelButton" /> class.
      /// </summary>
      /// <param name="label">The label.</param>
      public LabelButton(Label label)
      {
         if (label == null)
         {
            label = new Label();
         }

         label.InputTransparent = true;

         InternalView = label;

         // The label always has a transparent background
         BackgroundColor = Color.Transparent;

         // Applies to the base control only
         InputTransparent = false;

         // Force-refresh the label styles; this will configure the label properly
         SetStyle();
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="LabelButton" /> class.
      /// </summary>
      public LabelButton()
         : this(null)
      { }

      /// <summary>
      /// Gets or sets the selected label style.
      /// </summary>
      /// <value>The selected label style.</value>
      public Style SelectedLabelStyle
      {
         get => _selectedLabelButtonStyle;
         set
         {
            _selectedLabelButtonStyle = value;
            SetStyle();
         }
      }

      /// <summary>
      /// Gets or sets the deselected label style.
      /// </summary>
      /// <value>The deselected label style.</value>
      public Style DeselectedLabelStyle
      {
         get => _deselectedLabelButtonStyle;
         set
         {
            _deselectedLabelButtonStyle = value;
            SetStyle();
         }
      }

      /// <summary>
      /// Gets or sets the disabled label style.
      /// </summary>
      /// <value>The disabled label style.</value>
      public Style DisabledLabelStyle
      {
         get => _disabledLabelButtonStyle;
         set
         {
            _disabledLabelButtonStyle = value;
            SetStyle();
         }
      }

      /// <summary>
      /// Sets the style.
      /// </summary>
      protected override void SetStyle()
      {
         base.SetStyle();

         if (InternalView == null)
         {
            return;
         }

         Style newStyle = null;

         // Set the style based on being enabled/disabled
         if (ButtonState == ButtonStates.Disabled)
         {
            newStyle = DisabledLabelStyle ?? DeselectedLabelStyle;
         }
         else if (ButtonState == ButtonStates.Selected)
         {
            newStyle = SelectedLabelStyle ?? DeselectedLabelStyle;
         }
         else
         {
            newStyle = DeselectedLabelStyle;
         }

         // Can't call Equal comparisons on list-style records
         //if (newStyle != null && (InternalView.Style == null || InternalView.Style.IsNotAnEqualObjectTo(newStyle)))
         //{
#if MERGE_STYLES
       InternalView.Style = InternalView.Style.MergeStyle<LabelButton>(newStyle);
#else
         InternalView.Style = newStyle;
#endif

         // This library is not working well with styles, so forcing all settings manually
         InternalView.ForceStyle(newStyle);
         //}
      }

      /// <summary>
      /// Creates the label style.
      /// </summary>
      /// <param name="textColor">Color of the text.</param>
      /// <param name="fontSize">Size of the font.</param>
      /// <param name="fontAttributes">The font attributes.</param>
      /// <returns>Style.</returns>
      public static Style CreateLabelStyle(Color          textColor,
                                           double         fontSize,
                                           FontAttributes fontAttributes = FontAttributes.None)
      {
         return new Style(typeof(Label))
                {
                   Setters =
                   {
                      // The text color is now the background color -- should be white
                      new Setter {Property = Label.TextColorProperty, Value = textColor},

                      // The label is always transparent
                      new Setter {Property = BackgroundColorProperty, Value = Color.Transparent},

                      new Setter {Property = Label.FontAttributesProperty, Value = fontAttributes},
                      new Setter {Property = Label.FontSizeProperty, Value       = fontSize}
                   }
                };
      }

      /// <summary>
      /// Creates the label button bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateLabelButtonBindableProperty<PropertyTypeT>(string localPropName,
                                                                                      PropertyTypeT defaultVal =
                                                                                         default(PropertyTypeT),
                                                                                      BindingMode bindingMode =
                                                                                         BindingMode.OneWay,
                                                                                      Action<LabelButton, PropertyTypeT,
                                                                                            PropertyTypeT>
                                                                                         callbackAction =
                                                                                         null)
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }
   }
}