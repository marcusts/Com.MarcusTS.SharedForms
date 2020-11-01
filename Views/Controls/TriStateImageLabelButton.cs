// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, TriStateImageLabelButton.cs, is a part of a program called AccountViewMobile.
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
   using Xamarin.Forms;

   /// <summary>
   /// Interface ITriStateImageLabelButton Implements the
   /// <see cref="IImageLabelButton" />
   /// </summary>
   /// <seealso cref="IImageLabelButton" />
   public interface ITriStateImageLabelButton : IImageLabelButton
   {
      // Can deselect when the selection group is 0.
      /// <summary>
      /// Gets or sets a value indicating whether [allow coerced deselection].
      /// </summary>
      /// <value><c>true</c> if [allow coerced deselection]; otherwise, <c>false</c>.</value>
      bool AllowCoercedDeselection { get; set; }

      /// <summary>
      /// Gets or sets the button deselected style.
      /// </summary>
      /// <value>The button deselected style.</value>
      Style ButtonDeselectedStyle { get; set; }

      /// <summary>
      /// Gets or sets the button disabled style.
      /// </summary>
      /// <value>The button disabled style.</value>
      Style ButtonDisabledStyle { get; set; }

      /// <summary>
      /// Gets or sets the button selected style.
      /// </summary>
      /// <value>The button selected style.</value>
      Style ButtonSelectedStyle { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [button toggle selection].
      /// </summary>
      /// <value><c>true</c> if [button toggle selection]; otherwise, <c>false</c>.</value>
      bool ButtonToggleSelection { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this instance can disable.
      /// </summary>
      /// <value><c>true</c> if this instance can disable; otherwise, <c>false</c>.</value>
      bool CanDisable { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this instance can select.
      /// </summary>
      /// <value><c>true</c> if this instance can select; otherwise, <c>false</c>.</value>
      bool CanSelect { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [get image from resource].
      /// </summary>
      /// <value><c>true</c> if [get image from resource]; otherwise, <c>false</c>.</value>
      bool GetImageFromResource { get; set; }

      /// <summary>
      /// Gets or sets the image deselected file path.
      /// </summary>
      /// <value>The image deselected file path.</value>
      string ImageDeselectedFilePath { get; set; }

      /// <summary>
      /// Gets or sets the image disabled file path.
      /// </summary>
      /// <value>The image disabled file path.</value>
      string ImageDisabledFilePath { get; set; }

      /// <summary>
      /// Gets or sets the type of the image resource class.
      /// </summary>
      /// <value>The type of the image resource class.</value>
      Type ImageResourceClassType { get; set; }

      /// <summary>
      /// Gets or sets the image selected file path.
      /// </summary>
      /// <value>The image selected file path.</value>
      string ImageSelectedFilePath { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is completed.
      /// </summary>
      /// <value><c>true</c> if this instance is completed; otherwise, <c>false</c>.</value>
      bool IsCompleted { get; set; }

      /// <summary>
      /// Gets or sets the label deselected style.
      /// </summary>
      /// <value>The label deselected style.</value>
      Style LabelDeselectedStyle { get; set; }

      /// <summary>
      /// Gets or sets the label disabled style.
      /// </summary>
      /// <value>The label disabled style.</value>
      Style LabelDisabledStyle { get; set; }

      /// <summary>
      /// Gets or sets the label selected style.
      /// </summary>
      /// <value>The label selected style.</value>
      Style LabelSelectedStyle { get; set; }

      /// <summary>
      /// Forces the on is enabled changed.
      /// </summary>
      void ForceOnIsEnabledChanged();
   }

   /// <summary>
   /// A button that can contain either an image and/or a label. Implements the
   /// <see cref="ImageLabelButtonBase" />
   /// Implements the
   /// <see cref="ITriStateImageLabelButton" />
   /// </summary>
   /// <seealso cref="ImageLabelButtonBase" />
   /// <seealso cref="ITriStateImageLabelButton" />
   public class TriStateImageLabelButton : ImageLabelButtonBase, ITriStateImageLabelButton
   {
      /// <summary>
      /// The deselected button state
      /// </summary>
      public const string DESELECTED_BUTTON_STATE = "Deselected";

      /// <summary>
      /// The disabled button state
      /// </summary>
      public const string DISABLED_BUTTON_STATE = "Disabled";

      /// <summary>
      /// The selected button state
      /// </summary>
      public const string SELECTED_BUTTON_STATE = "Selected";

      /// <summary>
      /// The button deselected style property
      /// </summary>
      public static readonly BindableProperty ButtonDeselectedStyleProperty =
         CreateImageLabelButtonBindableProperty
         (
            nameof(ButtonDeselectedStyle),
            default(Style),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonDeselectedStyle = newVal;
            }
         );

      /// <summary>
      /// The can select property
      /// </summary>
      public static readonly BindableProperty CanSelectProperty =
         CreateImageLabelButtonBindableProperty
         (
            nameof(CanSelect),
            default(bool),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.CanSelect = newVal;
            }
         );

      /// <summary>
      /// The disabled button style property
      /// </summary>
      public static readonly BindableProperty DisabledButtonStyleProperty =
         CreateImageLabelButtonBindableProperty
         (
            nameof(ButtonDisabledStyle),
            default(Style),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonDisabledStyle = newVal;
            }
         );

      /// <summary>
      /// The get image from resource property
      /// </summary>
      public static readonly BindableProperty GetImageFromResourceProperty =
         CreateImageLabelButtonBindableProperty
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
      /// The image deselected file path property
      /// </summary>
      public static readonly BindableProperty ImageDeselectedFilePathProperty =
         CreateImageLabelButtonBindableProperty
         (
            nameof(ImageDeselectedFilePath),
            default(string),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ImageDeselectedFilePath = newVal;
            }
         );

      /// <summary>
      /// The image disabled file path property
      /// </summary>
      public static readonly BindableProperty ImageDisabledFilePathProperty =
         CreateImageLabelButtonBindableProperty
         (
            nameof(ImageDisabledFilePath),
            default(string),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ImageDisabledFilePath = newVal;
            }
         );

      /// <summary>
      /// The image resource class type property
      /// </summary>
      public static readonly BindableProperty ImageResourceClassTypeProperty =
         CreateImageLabelButtonBindableProperty
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
      /// The image selected file path property
      /// </summary>
      public static readonly BindableProperty ImageSelectedFilePathProperty =
         CreateImageLabelButtonBindableProperty
         (
            nameof(ImageSelectedFilePath),
            default(string),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ImageSelectedFilePath = newVal;
            }
         );

      /// <summary>
      /// The is completed property
      /// </summary>
      public static readonly BindableProperty IsCompletedProperty =
         CreateImageLabelButtonBindableProperty
         (
            nameof(IsCompleted),
            default(bool),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.IsCompleted = newVal;
            }
         );

      /// <summary>
      /// The label deselected style property
      /// </summary>
      public static readonly BindableProperty LabelDeselectedStyleProperty =
         CreateImageLabelButtonBindableProperty
         (
            nameof(LabelDeselectedStyle),
            default(Style),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.LabelDeselectedStyle = newVal;
            }
         );

      /// <summary>
      /// The label disabled style property
      /// </summary>
      public static readonly BindableProperty LabelDisabledStyleProperty =
         CreateImageLabelButtonBindableProperty
         (
            nameof(LabelDisabledStyle),
            default(Style),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.LabelDisabledStyle = newVal;
            }
         );

      /// <summary>
      /// The label selected style property
      /// </summary>
      public static readonly BindableProperty LabelSelectedStyleProperty =
         CreateImageLabelButtonBindableProperty
         (
            nameof(LabelSelectedStyle),
            default(Style),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.LabelSelectedStyle = newVal;
            }
         );

      /// <summary>
      /// The selected button style property
      /// </summary>
      public static readonly BindableProperty SelectedButtonStyleProperty =
         CreateImageLabelButtonBindableProperty
         (
            nameof(ButtonSelectedStyle),
            default(Style),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonSelectedStyle = newVal;
            }
         );

      /// <summary>
      /// Converts to ggleselectionproperty.
      /// </summary>
      public static readonly BindableProperty ToggleSelectionProperty =
         CreateImageLabelButtonBindableProperty
         (
            nameof(ButtonToggleSelection),
            default(bool),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonToggleSelection = newVal;
            }
         );

      /// <summary>
      /// The button deselected style
      /// </summary>
      private Style _buttonDeselectedStyle;

      /// <summary>
      /// The button disabled style
      /// </summary>
      private Style _buttonDisabledStyle;

      /// <summary>
      /// The button selected style
      /// </summary>
      private Style _buttonSelectedStyle;

      /// <summary>
      /// The button toggle selection
      /// </summary>
      private bool _buttonToggleSelection;

      /// <summary>
      /// The can disable
      /// </summary>
      private bool _canDisable;

      /// <summary>
      /// The can select
      /// </summary>
      private bool _canSelect;

      /// <summary>
      /// The get image from resource
      /// </summary>
      private bool _getImageFromResource = true;

      /// <summary>
      /// The image deselected file path
      /// </summary>
      private string _imageDeselectedFilePath;

      /// <summary>
      /// The image disabled file path
      /// </summary>
      private string _imageDisabledFilePath;

      /// <summary>
      /// The image label button styles
      /// </summary>
      private IList<ImageLabelButtonStyle> _imageLabelButtonStyles;

      /// <summary>
      /// The image resource class type
      /// </summary>
      private Type _imageResourceClassType;

      /// <summary>
      /// The image selected file path
      /// </summary>
      private string _imageSelectedFilePath;

      /// <summary>
      /// The is completed
      /// </summary>
      private bool _isCompleted;

      /// <summary>
      /// The label deselected style
      /// </summary>
      private Style _labelDeselectedStyle;

      /// <summary>
      /// The label disabled style
      /// </summary>
      private Style _labelDisabledStyle;

      /// <summary>
      /// The label selected button style
      /// </summary>
      private Style _labelSelectedButtonStyle;

      /// <summary>
      /// Gets or sets a value indicating whether [allow coerced deselection].
      /// </summary>
      /// <value><c>true</c> if [allow coerced deselection]; otherwise, <c>false</c>.</value>
      public bool AllowCoercedDeselection { get; set; }

      /// <summary>
      /// Gets or sets the button deselected style.
      /// </summary>
      /// <value>The button deselected style.</value>
      public Style ButtonDeselectedStyle
      {
         get => _buttonDeselectedStyle;
         set
         {
            _buttonDeselectedStyle = value;
            CreateOrRefreshImageLabelButtonStyles();
         }
      }

      /// <summary>
      /// Gets or sets the button disabled style.
      /// </summary>
      /// <value>The button disabled style.</value>
      public Style ButtonDisabledStyle
      {
         get => _buttonDisabledStyle;
         set
         {
            _buttonDisabledStyle = value;
            CreateOrRefreshImageLabelButtonStyles();
         }
      }

      /// <summary>
      /// Gets or sets the button selected style.
      /// </summary>
      /// <value>The button selected style.</value>
      public Style ButtonSelectedStyle
      {
         get => _buttonSelectedStyle;
         set
         {
            _buttonSelectedStyle = value;
            CreateOrRefreshImageLabelButtonStyles();
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether [button toggle selection].
      /// </summary>
      /// <value><c>true</c> if [button toggle selection]; otherwise, <c>false</c>.</value>
      public bool ButtonToggleSelection
      {
         get => _buttonToggleSelection;
         set
         {
            if (_buttonToggleSelection != value)
            {
               _buttonToggleSelection = value;
               ResetSelectionStyle();
            }
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance can disable.
      /// </summary>
      /// <value><c>true</c> if this instance can disable; otherwise, <c>false</c>.</value>
      public bool CanDisable
      {
         get => _canDisable;
         set
         {
            if (_canDisable != value)
            {
               _canDisable = value;

               // Corner case -- get off of disabled if illegal
               if (IsDisabled && !_canDisable)
               {
                  ButtonState = DESELECTED_BUTTON_STATE;
               }

               OnIsEnabledChanged();

               ResetSelectionStyle();
            }
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance can select.
      /// </summary>
      /// <value><c>true</c> if this instance can select; otherwise, <c>false</c>.</value>
      public bool CanSelect
      {
         get => _canSelect;
         set
         {
            if (_canSelect != value)
            {
               _canSelect = value;

               ResetSelectionStyle();

               // Corner case
               if (IsSelected && !_canSelect)
               {
                  ButtonState = DESELECTED_BUTTON_STATE;
               }
            }
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether [get image from resource].
      /// </summary>
      /// <value><c>true</c> if [get image from resource]; otherwise, <c>false</c>.</value>
      public bool GetImageFromResource
      {
         get => _getImageFromResource;
         set
         {
            _getImageFromResource = value;

            // MUST FORCE
            CreateOrRefreshImageLabelButtonStyles(true);
         }
      }

      /// <summary>
      /// Gets or sets the image deselected file path.
      /// </summary>
      /// <value>The image deselected file path.</value>
      public string ImageDeselectedFilePath
      {
         get => _imageDeselectedFilePath;
         set
         {
            if (_imageDeselectedFilePath.IsDifferentThan(value))
            {
               _imageDeselectedFilePath = value;
               CreateOrRefreshImageLabelButtonStyles();
            }
         }
      }

      /// <summary>
      /// Gets or sets the image disabled file path.
      /// </summary>
      /// <value>The image disabled file path.</value>
      public string ImageDisabledFilePath
      {
         get => _imageDisabledFilePath;
         set
         {
            if (_imageDisabledFilePath.IsDifferentThan(value))
            {
               _imageDisabledFilePath = value;
               CreateOrRefreshImageLabelButtonStyles();
            }
         }
      }

      /// <summary>
      /// Gets the image label button styles.
      /// </summary>
      /// <value>The image label button styles.</value>
      public override IList<ImageLabelButtonStyle> ImageLabelButtonStyles => _imageLabelButtonStyles;

      /// <summary>
      /// Gets or sets the type of the image resource class.
      /// </summary>
      /// <value>The type of the image resource class.</value>
      public Type ImageResourceClassType
      {
         get => _imageResourceClassType;
         set
         {
            if (_imageResourceClassType != value)
            {
               _imageResourceClassType = value;

               // MUST FORCE
               CreateOrRefreshImageLabelButtonStyles(true);
            }
         }
      }

      /// <summary>
      /// Gets or sets the image selected file path.
      /// </summary>
      /// <value>The image selected file path.</value>
      public string ImageSelectedFilePath
      {
         get => _imageSelectedFilePath;
         set
         {
            if (_imageSelectedFilePath.IsDifferentThan(value))
            {
               _imageSelectedFilePath = value;
               CreateOrRefreshImageLabelButtonStyles();
            }
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is completed.
      /// </summary>
      /// <value><c>true</c> if this instance is completed; otherwise, <c>false</c>.</value>
      public bool IsCompleted
      {
         get => _isCompleted;
         set
         {
            _isCompleted = value;

            // If not selected, change the button state to disabled is that is legal
            if (CanDisable && !IsSelected)
            {
               ButtonState = DISABLED_BUTTON_STATE;
            }
         }
      }

      /// <summary>
      /// Gets a value indicating whether this instance is selected.
      /// </summary>
      /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
      public override bool IsSelected => ButtonState.IsSameAs(SELECTED_BUTTON_STATE);

      /// <summary>
      /// Gets or sets the label deselected style.
      /// </summary>
      /// <value>The label deselected style.</value>
      public Style LabelDeselectedStyle
      {
         get => _labelDeselectedStyle;
         set
         {
            _labelDeselectedStyle = value;
            CreateOrRefreshImageLabelButtonStyles();
         }
      }

      /// <summary>
      /// Gets or sets the label disabled style.
      /// </summary>
      /// <value>The label disabled style.</value>
      public Style LabelDisabledStyle
      {
         get => _labelDisabledStyle;
         set
         {
            _labelDisabledStyle = value;
            CreateOrRefreshImageLabelButtonStyles();
         }
      }

      /// <summary>
      /// Gets or sets the label selected style.
      /// </summary>
      /// <value>The label selected style.</value>
      public Style LabelSelectedStyle
      {
         get => _labelSelectedButtonStyle;
         set
         {
            _labelSelectedButtonStyle = value;
            CreateOrRefreshImageLabelButtonStyles();
         }
      }

      /// <summary>
      /// Leave the button intact; it is not intended to change with selections.
      /// </summary>
      /// <value><c>true</c> if [update button text from style]; otherwise, <c>false</c>.</value>
      public override bool UpdateButtonTextFromStyle => false;

      /// <summary>
      /// Gets a value indicating whether this instance is disabled.
      /// </summary>
      /// <value><c>true</c> if this instance is disabled; otherwise, <c>false</c>.</value>
      protected override bool IsDisabled => ButtonState.IsSameAs(DISABLED_BUTTON_STATE);

      /// <summary>
      /// Creates the image label button bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateImageLabelButtonBindableProperty<PropertyTypeT>
      (
         string localPropName,
         PropertyTypeT defaultVal = default,
         BindingMode bindingMode = BindingMode.OneWay,
         Action<TriStateImageLabelButton, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      /// Forces the on is enabled changed.
      /// </summary>
      public void ForceOnIsEnabledChanged()
      {
         OnIsEnabledChanged();
      }

      /// <summary>
      /// Deselects this instance.
      /// </summary>
      protected override void Deselect()
      {
         if (IsCompleted && CanDisable)
         {
            // "Disabled" is the same as "completed"
            ButtonState = DISABLED_BUTTON_STATE;
         }
         else
         {
            ButtonState = DESELECTED_BUTTON_STATE;
         }
      }

      /// <summary>
      /// Lasts the check before assigning style.
      /// </summary>
      /// <param name="style">The style.</param>
      /// <returns>ImageLabelButtonStyle.</returns>
      protected override ImageLabelButtonStyle LastCheckBeforeAssigningStyle(ImageLabelButtonStyle style)
      {
         if (IsCompleted && CanDisable && style.InternalButtonState.IsSameAs(DESELECTED_BUTTON_STATE))
         {
            if (ButtonStateIndexFound(DISABLED_BUTTON_STATE, out var disabledStyleIdx))
            {
               return ImageLabelButtonStyles[disabledStyleIdx];
            }
         }

         // ELSE let the base do the work
         return base.LastCheckBeforeAssigningStyle(style);
      }

      /// <summary>
      /// Called when [button command created].
      /// </summary>
      protected override void OnButtonCommandCreated()
      {
         CreateOrRefreshImageLabelButtonStyles();
      }

      /// <summary>
      /// Called when [is enabled changed].
      /// </summary>
      protected override void OnIsEnabledChanged()
      {
         if (!IsEnabled && CanDisable)
         {
            ButtonState = DISABLED_BUTTON_STATE;
         }
         else if (IsEnabled)
         {
            ButtonState = IsSelected ? SELECTED_BUTTON_STATE : DESELECTED_BUTTON_STATE;
         }

         ResetSelectionStyle();
      }

      /// <summary>
      /// Assigns the styles if not null.
      /// </summary>
      /// <param name="imageLabelButtonStyle">The image label button style.</param>
      /// <param name="buttonStyle">The button style.</param>
      /// <param name="imageFilePath">The image file path.</param>
      /// <param name="labelStyle">The label style.</param>
      private void AssignStylesIfNotNull
      (
         ImageLabelButtonStyle imageLabelButtonStyle,
         Style buttonStyle,
         string imageFilePath,
         Style labelStyle
      )
      {
         if (imageLabelButtonStyle == null)
         {
            return;
         }

         imageLabelButtonStyle.ButtonStyle = buttonStyle;
         imageLabelButtonStyle.ImageFilePath = imageFilePath;
         imageLabelButtonStyle.GetImageFromResource = GetImageFromResource;
         imageLabelButtonStyle.ImageResourceClassType = ImageResourceClassType;
         imageLabelButtonStyle.LabelStyle = labelStyle;
      }

      /// <summary>
      /// Creates the or refresh image label button styles.
      /// </summary>
      /// <param name="forceCreate">if set to <c>true</c> [force create].</param>
      private void CreateOrRefreshImageLabelButtonStyles(bool forceCreate = false)
      {
         var deselectedStyle = new ImageLabelButtonStyle();
         var selectedStyle = new ImageLabelButtonStyle();
         var disabledStyle = new ImageLabelButtonStyle();

         _imageLabelButtonStyles = new List<ImageLabelButtonStyle>();

         deselectedStyle = new ImageLabelButtonStyle
         {
            InternalButtonState = DESELECTED_BUTTON_STATE,
            GetImageFromResource = GetImageFromResource,
            ImageResourceClassType = ImageResourceClassType
         };

         ImageLabelButtonStyles.Add(deselectedStyle);

         if (CanSelect || ButtonToggleSelection)
         {
            selectedStyle = new ImageLabelButtonStyle
            {
               InternalButtonState = SELECTED_BUTTON_STATE,
               GetImageFromResource = GetImageFromResource,
               ImageResourceClassType = ImageResourceClassType
            };

            ImageLabelButtonStyles.Add(selectedStyle);
         }

         if (CanDisable)
         {
            disabledStyle = new ImageLabelButtonStyle
            {
               InternalButtonState = DISABLED_BUTTON_STATE,
               GetImageFromResource = GetImageFromResource,
               ImageResourceClassType = ImageResourceClassType
            };

            ImageLabelButtonStyles.Add(disabledStyle);
         }

         // Now update these from our proprietary styles ON refresh, there will be no creation, so this step is
         // necessary.
         AssignStylesIfNotNull(deselectedStyle, ButtonDeselectedStyle, ImageDeselectedFilePath, LabelDeselectedStyle);
         AssignStylesIfNotNull(selectedStyle, ButtonSelectedStyle, ImageSelectedFilePath, LabelSelectedStyle);
         AssignStylesIfNotNull(disabledStyle, ButtonDisabledStyle, ImageDisabledFilePath, LabelDisabledStyle);

         FixNullButtonState();

         // Update the variable "CurrentStyle" with these changes
         UpdateCurrentStyleFromButtonState(ButtonState);

         SetAllStyles();
      }

      /// <summary>
      /// Fixes the state of the null button.
      /// </summary>
      private void FixNullButtonState()
      {
         if (ButtonState.IsEmpty() && ImageLabelButtonStyles.Count > 0)
         {
            ButtonState = DESELECTED_BUTTON_STATE;
         }
      }

      /// <summary>
      /// Resets the selection style.
      /// </summary>
      private void ResetSelectionStyle()
      {
         SelectionStyle = CanSelect
                             ? ButtonToggleSelection
                                  ? ImageLabelButtonSelectionStyles.ToggleSelectionAsFirstTwoStyles
                                  : ImageLabelButtonSelectionStyles.SelectionButNoToggleAsFirstTwoStyles
                             : ImageLabelButtonSelectionStyles.NoSelection;

         CreateOrRefreshImageLabelButtonStyles(true);
      }
   }
}
