#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ImageLabelButtonBase.cs, is a part of a program called AccountViewMobile.
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

// #define SHOW_LABEL_BACK_COLOR
// #define TEST_FONT_SIZES
// #define SHOW_IMAGE_BACKGROUND
// #define TEST_LOST_BORDERS
// #define FORCE_CURRENT_STYLE_COPY
// #define DOUBLE_LAYOUT_IMAGE
// #define HACK_BUTTON_IMAGE_BOUNDS
// #define DOUBLE_LAYOUT_LABEL
// #define MANAGE_BUTTON_LABEL_PROPERTY_CHANGED
// #define SHOW_LABEL_BACK_COLOR

#define NO_THREADS

// #define AVOID_BEGIN_INVOKE

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Utils;
   using SharedUtils.Utils;
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Linq;
   using Xamarin.Forms;

   /// <summary>
   /// Enum SelectionStyles
   /// </summary>
   public enum ImageLabelButtonSelectionStyles
   {
      /// <summary>
      /// The no selection
      /// </summary>
      NoSelection,

      /// <summary>
      /// The selection but no toggle as first two styles
      /// </summary>
      SelectionButNoToggleAsFirstTwoStyles,

      /// <summary>
      /// Toggles selection between the first and second styles ONLY.
      /// </summary>
      ToggleSelectionAsFirstTwoStyles,

      /// <summary>
      /// Toggles selection through all styles.
      /// </summary>
      ToggleSelectionThroughAllStyles
   }

   /// <summary>
   /// Interface IImageLabelButton Implements the <see cref="System.IDisposable" /> Implements the
   /// <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IReportButtonStateChanges" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IReportButtonStateChanges" />
   /// <seealso cref="System.IDisposable" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IImageLabelButton : IReportButtonStateChanges, IDisposable, INotifyPropertyChanged
   {
      /// <summary>
      /// Gets or sets a value indicating whether [animate button].
      /// </summary>
      /// <value><c>true</c> if [animate button]; otherwise, <c>false</c>.</value>
      bool AnimateButton { get; set; }

      /// <summary>
      /// Gets or sets the button command.
      /// </summary>
      /// <value>The button command.</value>
      Command ButtonCommand { get; set; }

      /// <summary>
      /// Gets or sets the name of the button command binding.
      /// </summary>
      /// <value>The name of the button command binding.</value>
      string ButtonCommandBindingName { get; set; }

      /// <summary>
      /// Gets or sets the button command converter.
      /// </summary>
      /// <value>The button command converter.</value>
      IValueConverter ButtonCommandConverter { get; set; }

      /// <summary>
      /// Gets or sets the button command converter parameter.
      /// </summary>
      /// <value>The button command converter parameter.</value>
      object ButtonCommandConverterParameter { get; set; }

      /// <summary>
      /// Gets or sets the button command source.
      /// </summary>
      /// <value>The button command source.</value>
      object ButtonCommandSource { get; set; }

      /// <summary>
      /// Gets or sets the button command string format.
      /// </summary>
      /// <value>The button command string format.</value>
      string ButtonCommandStringFormat { get; set; }

      /// <summary>
      /// Gets or sets the button corner radius factor.
      /// </summary>
      /// <value>The button corner radius factor.</value>
      double? ButtonCornerRadiusFactor { get; set; }

      /// <summary>
      /// Gets or sets the button corner radius fixed.
      /// </summary>
      /// <value>The button corner radius fixed.</value>
      double? ButtonCornerRadiusFixed { get; set; }

      /// <summary>
      /// Gets or sets the button image.
      /// </summary>
      /// <value>The button image.</value>
      Image ButtonImage { get; set; }

      /// <summary>
      /// Gets or sets the button label.
      /// </summary>
      /// <value>The button label.</value>
      Label ButtonLabel { get; set; }

      /// <summary>
      /// Gets or sets the state of the button.
      /// </summary>
      /// <value>The state of the button.</value>
      string ButtonState { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [cannot tap].
      /// </summary>
      /// <value><c>true</c> if [cannot tap]; otherwise, <c>false</c>.</value>
      bool CannotTap { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this instance can tap on disabled.
      /// </summary>
      /// <value><c>true</c> if this instance can tap on disabled; otherwise, <c>false</c>.</value>
      bool CanTapOnDisabled { get; set; }

      /// <summary>
      /// Gets or sets the current style.
      /// </summary>
      /// <value>The current style.</value>
      ImageLabelButtonStyle CurrentStyle { get; set; }

      /// <summary>
      /// Gets the image label button styles.
      /// </summary>
      /// <value>The image label button styles.</value>
      IList<ImageLabelButtonStyle> ImageLabelButtonStyles { get; }

      /// <summary>
      /// Gets or sets the image position.
      /// </summary>
      /// <value>The image position.</value>
      FormsConst.OnScreenPositions ImagePos { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [include haptic feedback].
      /// </summary>
      /// <value><c>true</c> if [include haptic feedback]; otherwise, <c>false</c>.</value>
      bool IncludeHapticFeedback { get; set; }

      /// <summary>
      /// Gets a value indicating whether this instance is selected.
      /// </summary>
      /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
      bool IsSelected { get; }

      /// <summary>
      /// Gets or sets the label position.
      /// </summary>
      /// <value>The label position.</value>
      FormsConst.OnScreenPositions LabelPos { get; set; }

      /// <summary>
      /// Gets or sets the selection group.
      /// </summary>
      /// <value>The selection group.</value>
      int SelectionGroup { get; set; }

      /// <summary>
      /// Gets or sets the selection style.
      /// </summary>
      /// <value>The selection style.</value>
      ImageLabelButtonSelectionStyles SelectionStyle { get; set; }

      /// <summary>
      /// Gets a value indicating whether [update button text from style].
      /// </summary>
      /// <value><c>true</c> if [update button text from style]; otherwise, <c>false</c>.</value>
      bool UpdateButtonTextFromStyle { get; }

      /// <summary>
      /// Occurs when [button state changed].
      /// </summary>
      event EventUtils.NoParamsDelegate ImageLabelButtonPressed;

      /// <summary>
      /// Occurs when [is enabled changed].
      /// </summary>
      event EventUtils.GenericDelegate<bool> IsEnabledChanged;
   }

   /// <summary>
   /// Interface IReportButtonStateChanges
   /// </summary>
   public interface IReportButtonStateChanges
   {
      /// <summary>
      /// Occurs when [button state changed].
      /// </summary>
      event EventUtils.GenericDelegate<IImageLabelButton> ButtonStateChanged;
   }

   /// <summary>
   /// A button that can contain either an image and/or a label. Implements the
   /// <see cref="Com.MarcusTS.SharedForms.Views.Controls.ShapeView" />
   /// Implements the
   /// <see cref="Com.MarcusTS.SharedForms.Views.Controls.IImageLabelButton" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ShapeView" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IImageLabelButton" />
   public abstract class ImageLabelButtonBase : ShapeView, IImageLabelButton
   {
      /// <summary>
      /// The default button radius factor
      /// </summary>
      public const float DEFAULT_BUTTON_RADIUS_FACTOR = 0.12f;

      /// <summary>
      /// The animate button property
      /// </summary>
      public static readonly BindableProperty AnimateButtonProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(AnimateButton),
            true,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.AnimateButton = newVal;
            }
         );

      /// <summary>
      /// The button command binding name property
      /// </summary>
      public static readonly BindableProperty ButtonCommandBindingNameProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ButtonCommandBindingName),
            default(string),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonCommandBindingName = newVal;
            }
         );

      /// <summary>
      /// The button command converter parameter property
      /// </summary>
      public static readonly BindableProperty ButtonCommandConverterParameterProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ButtonCommandConverterParameter),
            default(object),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonCommandConverterParameter = newVal;
            }
         );

      /// <summary>
      /// The button command converter property
      /// </summary>
      public static readonly BindableProperty ButtonCommandConverterProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ButtonCommandConverter),
            default(IValueConverter),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonCommandConverter = newVal;
            }
         );

      /// <summary>
      /// The button command property
      /// </summary>
      public static readonly BindableProperty ButtonCommandProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ButtonCommand),
            default(Command),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonCommand = newVal;
            }
         );

      /// <summary>
      /// The button command string format property
      /// </summary>
      public static readonly BindableProperty ButtonCommandStringFormatProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ButtonCommandStringFormat),
            default(string),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonCommandStringFormat = newVal;
            }
         );

      /// <summary>
      /// The button image property
      /// </summary>
      public static readonly BindableProperty ButtonImageProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ButtonImage),
            default(Image),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonImage = newVal;
            }
         );

      /// <summary>
      /// The button label property
      /// </summary>
      public static readonly BindableProperty ButtonLabelProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ButtonLabel),
            default(Label),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonLabel = newVal;
            }
         );

      /// <summary>
      /// The button state property
      /// </summary>
      public static readonly BindableProperty ButtonStateProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ButtonState),
            default(string),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonState = newVal;
            }
         );

      /// <summary>
      /// The corner radius factor property
      /// </summary>
      public static readonly BindableProperty CornerRadiusFactorProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ButtonCornerRadiusFactor),
            default(double?),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonCornerRadiusFactor = newVal;
            }
         );

      /// <summary>
      /// The corner radius fixed property
      /// </summary>
      public static readonly BindableProperty CornerRadiusFixedProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ButtonCornerRadiusFixed),
            default(double?),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ButtonCornerRadiusFixed = newVal;
            }
         );

      /// <summary>
      /// The current style property
      /// </summary>
      public static readonly BindableProperty CurrentStyleProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(CurrentStyle),
            default(ImageLabelButtonStyle),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.CurrentStyle = newVal;
            }
         );

      /// <summary>
      /// The image height property
      /// </summary>
      public static readonly BindableProperty ImageHeightProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ImageHeight),
            default(double),
            BindingMode.OneWay,
            (
               imageButton,
               oldVal,
               newVal
            ) =>
            {
               imageButton.ImageHeight = newVal;
            }
         );

      /// <summary>
      /// The image position property
      /// </summary>
      public static readonly BindableProperty ImagePosProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ImagePos),
            default(FormsConst.OnScreenPositions),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.ImagePos = newVal;
            }
         );

      /// <summary>
      /// The image width property
      /// </summary>
      public static readonly BindableProperty ImageWidthProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ImageWidth),
            default(double),
            BindingMode.OneWay,
            (
               imageButton,
               oldVal,
               newVal
            ) =>
            {
               imageButton.ImageWidth = newVal;
            }
         );

      /// <summary>
      /// The include haptic feedback property
      /// </summary>
      public static readonly BindableProperty IncludeHapticFeedbackProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(IncludeHapticFeedback),
            true,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.IncludeHapticFeedback = newVal;
            }
         );

      /// <summary>
      /// The label position property
      /// </summary>
      public static readonly BindableProperty LabelPosProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(LabelPos),
            default(FormsConst.OnScreenPositions),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.LabelPos = newVal;
            }
         );

      /// <summary>
      /// The selection group property
      /// </summary>
      public static readonly BindableProperty SelectionGroupProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(SelectionGroup),
            default(int),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.SelectionGroup = newVal;
            }
         );

      /// <summary>
      /// The selection style property
      /// </summary>
      public static readonly BindableProperty SelectionStyleProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(SelectionStyle),
            default(ImageLabelButtonSelectionStyles),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.SelectionStyle = newVal;
            }
         );

      /// <summary>
      /// The layout
      /// </summary>
      private readonly RelativeLayout _layout = FormsUtils.GetExpandingRelativeLayout();

      /// <summary>
      /// The tap gesture
      /// </summary>
      private readonly TapGestureRecognizer _tapGesture = new TapGestureRecognizer();

      /// <summary>
      /// The button command
      /// </summary>
      private Command _buttonCommand;

      /// <summary>
      /// The button command binding name
      /// </summary>
      private string _buttonCommandBindingName;

      /// <summary>
      /// The button command converter
      /// </summary>
      private IValueConverter _buttonCommandConverter;

      /// <summary>
      /// The button command converter parameter
      /// </summary>
      private object _buttonCommandConverterParameter;

      /// <summary>
      /// The button command source
      /// </summary>
      private object _buttonCommandSource;

      /// <summary>
      /// The button image
      /// </summary>
      private Image _buttonImage;

      /// <summary>
      /// The button label
      /// </summary>
      private Label _buttonLabel;

      /// <summary>
      /// The button state
      /// </summary>
      private string _buttonState;

      /// <summary>
      /// The button state assigned from style
      /// </summary>
      private bool _buttonStateAssignedFromStyle;

      /// <summary>
      /// The corner radius factor
      /// </summary>
      private double? _cornerRadiusFactor;

      /// <summary>
      /// The corner radius fixed
      /// </summary>
      private double? _cornerRadiusFixed;

      /// <summary>
      /// The current style
      /// </summary>
      private ImageLabelButtonStyle _currentStyle;

      /// <summary>
      /// The image height
      /// </summary>
      private double _imageHeight;

      /// <summary>
      /// The image position
      /// </summary>
      private FormsConst.OnScreenPositions _imagePos;

      /// <summary>
      /// The image width
      /// </summary>
      private double _imageWidth;

      /// <summary>
      /// The is instantiating
      /// </summary>
      private bool _isInstantiating;

      /// <summary>
      /// The label position
      /// </summary>
      private FormsConst.OnScreenPositions _labelPos;

      /// <summary>
      /// The last bounds
      /// </summary>
      private Rectangle _lastBounds;

      /// <summary>
      /// The selection group
      /// </summary>
      private int _selectionGroup;

      /// <summary>
      /// The selection style
      /// </summary>
      private ImageLabelButtonSelectionStyles _selectionStyle;

      /// <summary>
      /// The tapped listener entered
      /// </summary>
      private volatile bool _tappedListenerEntered;

      /// <summary>
      /// Initializes a new instance of the <see cref="ImageLabelButtonBase" /> class.
      /// </summary>
      protected ImageLabelButtonBase()
      {
         CallStartup();
      }

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

      /// <summary>
      /// Gets a value indicating whether this instance is disabled.
      /// </summary>
      /// <value><c>true</c> if this instance is disabled; otherwise, <c>false</c>.</value>
      protected abstract bool IsDisabled { get; }

      /// <summary>
      /// Gets the current size best guess.
      /// </summary>
      /// <value>The current size best guess.</value>
      private Size CurrentSizeBestGuess => new Size(Math.Max(Width, WidthRequest), Math.Max(Height, HeightRequest));

      // BUGS REQUIRE THIS
      /// <summary>
      /// Gets or sets the margin for the view.
      /// </summary>
      /// <value>To be added.</value>
      /// <remarks>To be added.</remarks>
      private new Thickness Margin { get; set; }

      // BUGS REQUIRE THIS
      /// <summary>
      /// Gets or sets the inner padding of the Layout.
      /// </summary>
      /// <value>The Thickness values for the layout. The default value is a Thickness with all values set to 0.</value>
      /// <remarks><para>
      /// The padding is the space between the bounds of a layout and the bounding region into which its children should be arranged into.
      /// </para>
      /// <para>
      /// The following example shows setting the padding of a Layout to inset its children.
      /// </para>
      /// <example>
      ///   <code lang="csharp lang-csharp"><![CDATA[
      /// var stackLayout = new StackLayout {
      /// Padding = new Thickness (10, 10, 10, 20),
      /// Children = {
      /// new Label {Text = "Hello"},
      /// new Label {Text = "World"}
      /// }
      /// }
      /// ]]></code>
      /// </example></remarks>
      private new Thickness Padding { get; set; }

      /// <summary>
      /// Occurs when [button state changed].
      /// </summary>
      public event EventUtils.GenericDelegate<IImageLabelButton> ButtonStateChanged;

      /// <summary>
      /// Occurs when [image label button pressed].
      /// </summary>
      public event EventUtils.NoParamsDelegate ImageLabelButtonPressed;

      /// <summary>
      /// Occurs when [is enabled changed].
      /// </summary>
      public event EventUtils.GenericDelegate<bool> IsEnabledChanged;

      /// <summary>
      /// Gets or sets a value indicating whether [animate button].
      /// </summary>
      /// <value><c>true</c> if [animate button]; otherwise, <c>false</c>.</value>
      public bool AnimateButton { get; set; } = true;

      /// <summary>
      /// Gets or sets the button command.
      /// </summary>
      /// <value>The button command.</value>
      public Command ButtonCommand
      {
         get => _buttonCommand;
         set
         {
            RemoveButtonCommandEventListener();

            _buttonCommand = value;

            if (ButtonCommand != null)
            {
               ButtonCommand.CanExecuteChanged += HandleButtonCommandCanExecuteChanged;

               // Force-fire the initial state
               ButtonCommand.ChangeCanExecute();

               OnButtonCommandCreated();
            }
         }
      }

      /// <summary>
      /// Gets or sets the name of the button command binding.
      /// </summary>
      /// <value>The name of the button command binding.</value>
      public string ButtonCommandBindingName
      {
         get => _buttonCommandBindingName;
         set
         {
            if (_buttonCommandBindingName.IsDifferentThan(value))
            {
               _buttonCommandBindingName = value;
               SetUpCompleteButtonCommandBinding();
            }
         }
      }

      /// <summary>
      /// Gets or sets the button command converter.
      /// </summary>
      /// <value>The button command converter.</value>
      public IValueConverter ButtonCommandConverter
      {
         get => _buttonCommandConverter;
         set
         {
            _buttonCommandConverter = value;
            SetUpCompleteButtonCommandBinding();
         }
      }

      /// <summary>
      /// Gets or sets the button command converter parameter.
      /// </summary>
      /// <value>The button command converter parameter.</value>
      public object ButtonCommandConverterParameter
      {
         get => _buttonCommandConverterParameter;
         set
         {
            _buttonCommandConverterParameter = value;
            SetUpCompleteButtonCommandBinding();
         }
      }

      /// <summary>
      /// Gets or sets the button command source.
      /// </summary>
      /// <value>The button command source.</value>
      public object ButtonCommandSource
      {
         get => _buttonCommandSource;
         set
         {
            _buttonCommandSource = value;
            SetUpCompleteButtonCommandBinding();
         }
      }

      /// <summary>
      /// Gets or sets the button command string format.
      /// </summary>
      /// <value>The button command string format.</value>
      public string ButtonCommandStringFormat { get; set; }

      /// <summary>
      /// Gets or sets the button corner radius factor.
      /// </summary>
      /// <value>The button corner radius factor.</value>
      public double? ButtonCornerRadiusFactor
      {
         get => _cornerRadiusFactor;
         set
         {
            _cornerRadiusFactor = value;
            SetCornerRadius();
         }
      }

      /// <summary>
      /// Gets or sets the button corner radius fixed.
      /// </summary>
      /// <value>The button corner radius fixed.</value>
      public double? ButtonCornerRadiusFixed
      {
         get => _cornerRadiusFixed;
         set
         {
            _cornerRadiusFixed = value;
            SetCornerRadius();
         }
      }

      /// <summary>
      /// Gets or sets the button image.
      /// </summary>
      /// <value>The button image.</value>
      public Image ButtonImage
      {
         get => _buttonImage;
         set
         {
            if (value.IsNotNullOrDefault())
            {
               if (_layout.Children.Contains(value))
               {
                  _layout.Children.Remove(value);
               }
            }

            _buttonImage = value;

            if (_buttonImage != null)
            {
               ButtonImage.BindingContext = BindingContext;

               _buttonImage.InputTransparent = true;

               if (ButtonLabel.IsNullOrDefault())
               {
                  _layout.CreateRelativeOverlay(_buttonImage, Padding);
               }
               else
               {
                  ShareButtonAndLabelPositions();
               }
            }
         }
      }

      /// <summary>
      /// Gets or sets the button label.
      /// </summary>
      /// <value>The button label.</value>
      public Label ButtonLabel
      {
         get => _buttonLabel;
         set
         {
            if (value.IsNotNullOrDefault())
            {
               if (_layout.Children.Contains(value))
               {
                  _layout.Children.Remove(value);
               }
            }

            _buttonLabel = value;

            if (_buttonLabel != null)
            {
               ButtonLabel.BindingContext = BindingContext;

               _buttonLabel.InputTransparent = true;

               if (ButtonImage.IsNullOrDefault())
               {
                  _layout.CreateRelativeOverlay(ButtonLabel, Padding);
               }
               else
               {
                  ShareButtonAndLabelPositions();
               }

               SetLabelStyle();
            }
         }
      }

      /// <summary>
      /// Gets or sets the state of the button.
      /// </summary>
      /// <value>The state of the button.</value>
      public string ButtonState
      {
         get => _buttonState;
         set
         {
            if (_buttonStateAssignedFromStyle)
            {
               if (_buttonState != value)
               {
                  _buttonState = value;
                  AfterButtonStateChanged();
                  BroadcastIfSelected();
                  ButtonStateChanged?.Invoke(this);
               }
            }
            else
            {
               UpdateCurrentStyleFromButtonState(value);
            }
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether [cannot tap].
      /// </summary>
      /// <value><c>true</c> if [cannot tap]; otherwise, <c>false</c>.</value>
      public bool CannotTap { get; set; } = false;

      /// <summary>
      /// Gets or sets a value indicating whether this instance can tap on disabled.
      /// </summary>
      /// <value><c>true</c> if this instance can tap on disabled; otherwise, <c>false</c>.</value>
      public bool CanTapOnDisabled { get; set; } = false;

      /// <summary>
      /// Gets or sets the current style.
      /// </summary>
      /// <value>The current style.</value>
      public ImageLabelButtonStyle CurrentStyle
      {
         get
         {
            if (_currentStyle.IsNullOrDefault())
            {
               if (ImageLabelButtonStyles.IsNotEmpty())
               {
                  _currentStyle = ImageLabelButtonStyles[0];
               }
            }

            return _currentStyle;
         }
         set
         {
            _currentStyle = LastCheckBeforeAssigningStyle(value);

            _buttonStateAssignedFromStyle = true;
            ButtonState                   = _currentStyle.InternalButtonState;
            UpdateButtonText();
            SetAllStyles();
            _buttonStateAssignedFromStyle = false;
         }
      }

      /// <summary>
      /// Gets the image label button styles.
      /// </summary>
      /// <value>The image label button styles.</value>
      public abstract IList<ImageLabelButtonStyle> ImageLabelButtonStyles { get; }

      /// <summary>
      /// Gets or sets the image position.
      /// </summary>
      /// <value>The image position.</value>
      public FormsConst.OnScreenPositions ImagePos
      {
         get => _imagePos;
         set
         {
            if (_imagePos != value)
            {
               _imagePos = value;

               if (ButtonImage.IsNotNullOrDefault())
               {
                  // Tricky, but works
                  ButtonImage = ButtonImage;
               }
            }
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether [include haptic feedback].
      /// </summary>
      /// <value><c>true</c> if [include haptic feedback]; otherwise, <c>false</c>.</value>
      public bool IncludeHapticFeedback { get; set; } = true;

      /// <summary>
      /// Gets a value indicating whether this instance is selected.
      /// </summary>
      /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
      public abstract bool IsSelected { get; }

      /// <summary>
      /// Gets or sets the label position.
      /// </summary>
      /// <value>The label position.</value>
      public FormsConst.OnScreenPositions LabelPos
      {
         get => _labelPos;
         set
         {
            if (_labelPos != value)
            {
               _labelPos = value;

               if (ButtonLabel.IsNotNullOrDefault())
               {
                  // Tricky, but works
                  ButtonLabel = ButtonLabel;
               }
            }
         }
      }

      /// <summary>
      /// Gets or sets the selection group.
      /// </summary>
      /// <value>The selection group.</value>
      public int SelectionGroup
      {
         get => _selectionGroup;
         set
         {
            if (_selectionGroup != value)
            {
               _selectionGroup = value;
               BroadcastIfSelected();
            }
         }
      }

      /// <summary>
      /// Gets or sets the selection style.
      /// </summary>
      /// <value>The selection style.</value>
      public ImageLabelButtonSelectionStyles SelectionStyle
      {
         get => _selectionStyle;
         set
         {
            _selectionStyle = value;
            SetAllStyles();
         }
      }

      /// <summary>
      /// Gets a value indicating whether [update button text from style].
      /// </summary>
      /// <value><c>true</c> if [update button text from style]; otherwise, <c>false</c>.</value>
      public abstract bool UpdateButtonTextFromStyle { get; }

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         ReleaseUnmanagedResources();
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Finalizes an instance of the <see cref="ImageLabelButtonBase" /> class.
      /// </summary>
      ~ImageLabelButtonBase()
      {
         ReleaseUnmanagedResources();
      }

      /// <summary>
      /// Occurs when [i am selected static].
      /// </summary>
      protected static event EventUtils.GenericDelegate<IImageLabelButton> IAmSelectedStatic;

      /// <summary>
      /// Creates the button style.
      /// </summary>
      /// <param name="backColor">Color of the back.</param>
      /// <param name="BorderThickness">Width of the border.</param>
      /// <param name="borderColor">Color of the border.</param>
      /// <returns>Style.</returns>
      public static Style CreateButtonStyle
      (
         Color? backColor       = null,
         float? BorderThickness = null,
         Color? borderColor     = default
      )
      {
         var newStyle = new Style(typeof(ImageLabelButtonBase));

         if (backColor.HasValue)
         {
            newStyle.Setters.Add(new Setter {Property = ColorProperty, Value = backColor});
         }

         if (BorderThickness.HasValue)
         {
            newStyle.Setters.Add(new Setter
            {
               Property = BorderThicknessProperty, Value = BorderThickness.GetValueOrDefault()
            });
         }

         if (borderColor.HasValue)
         {
            newStyle.Setters.Add(new Setter {Property = BorderColorProperty, Value = borderColor});
         }

         return newStyle;
      }

      /// <summary>
      /// Creates the label style.
      /// </summary>
      /// <param name="textColor">Color of the text.</param>
      /// <param name="fontSize">Size of the font.</param>
      /// <param name="fontAttributes">The font attributes.</param>
      /// <returns>Style.</returns>
      public static Style CreateLabelStyle
      (
         Color?          textColor      = null,
         double?         fontSize       = null,
         FontAttributes? fontAttributes = null
      )
      {
         // var newStyle = new Style(typeof(ImageLabelButtonBase));
         var newStyle = new Style(typeof(Label));

         if (textColor.HasValue)
         {
            newStyle.Setters.Add(new Setter {Property = Label.TextColorProperty, Value = textColor});
         }

         // The label is always transparent
         newStyle.Setters.Add(new Setter {Property = BackgroundColorProperty, Value = Color.Transparent});

         if (fontSize.HasValue)
         {
            newStyle.Setters.Add(new Setter {Property = Label.FontSizeProperty, Value = fontSize});
         }

         if (fontAttributes.HasValue)
         {
            newStyle.Setters.Add(new Setter {Property = Label.FontAttributesProperty, Value = fontAttributes});
         }

         return newStyle;
      }

      /// <summary>
      /// Creates the toggle image label button bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateToggleImageLabelButtonBindableProperty<PropertyTypeT>
      (
         string                                                     localPropName,
         PropertyTypeT                                              defaultVal     = default,
         BindingMode                                                bindingMode    = BindingMode.OneWay,
         Action<ImageLabelButtonBase, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      /// Releases unmanaged and - optionally - managed resources.
      /// </summary>
      /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
      public void Dispose(bool disposing)
      {
         ReleaseUnmanagedResources();
         if (disposing)
         {
         }
      }

      /// <summary>
      /// Afters the button state changed.
      /// </summary>
      protected virtual void AfterButtonStateChanged()
      {
      }

      /// <summary>
      /// Afters the content set.
      /// </summary>
      protected virtual void AfterContentSet()
      {
      }

      /// <summary>
      /// Called when [button state changed].
      /// </summary>
      /// <param name="newButtonState">New state of the button.</param>
      protected virtual void BeforeButtonStateChangedFromStyle(ref string newButtonState)
      {
      }

      /// <summary>
      /// Buttons the state index found.
      /// </summary>
      /// <param name="buttonStateToFind">The button state to find.</param>
      /// <param name="styleIdx">Index of the style.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      protected bool ButtonStateIndexFound(string buttonStateToFind, out int styleIdx)
      {
         styleIdx = -1;

         if (ImageLabelButtonStyles.IsEmpty() || buttonStateToFind.IsEmpty())
         {
            return false;
         }

         var foundStyle =
            ImageLabelButtonStyles.FirstOrDefault(style => style.InternalButtonState.IsSameAs(buttonStateToFind));
         styleIdx = ImageLabelButtonStyles.IndexOf(foundStyle);

         // Should never occur due to constraints set up at this class's constructor
         if (styleIdx < 0 || ImageLabelButtonStyles.Count < styleIdx)
         {
            return false;
         }

         return true;
      }

      /// <summary>
      /// Calls the recreate image safely.
      /// </summary>
      protected void CallRecreateImageSafely()
      {
#if NO_THREADS
         try
         {
            RecreateImage();
         }
         catch (Exception)
         {
            Debug.WriteLine(nameof(ImageLabelButtonBase) + ": " + nameof(CallRecreateImageSafely) +
                            ": COULD NOT CREATE AN IMAGE");
         }
#else
         if (ThreadHelper.IsOnMainThread)
         {
            RecreateImage();
         }
         else
         {
            Device.BeginInvokeOnMainThread(RecreateImage);
         }
#endif
      }

      /// <summary>
      /// Deselects this instance.
      /// </summary>
      protected abstract void Deselect();

      /// <summary>
      /// Handles the tap gesture tapped.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      /// <remarks>TODO Cannot avoid async void -- event handler.</remarks>
      protected async void HandleTapGestureTapped
      (
         object    sender,
         EventArgs e
      )
      {
         if (_tappedListenerEntered           ||
             CannotTap                        ||
             IsDisabled && !CanTapOnDisabled  ||
             ImageLabelButtonStyles.IsEmpty() ||
             CurrentStyle.IsNullOrDefault()   ||
             CurrentStyle.InternalButtonState.IsEmpty())
         {
            return;
         }

         _tappedListenerEntered = true;

         ImageLabelButtonPressed?.Invoke();

         if (SelectionStyle == ImageLabelButtonSelectionStyles.ToggleSelectionAsFirstTwoStyles ||
             SelectionStyle == ImageLabelButtonSelectionStyles.ToggleSelectionThroughAllStyles)
         {
            ToggleCurrentStyle();
         }

         // If a command exists, fire it and reset our selected status to false; otherwise, leave the selected state as
         // it is.
         if (ButtonCommand != null)
         {
#if USE_BEGIN_INVOKE
            Device.BeginInvokeOnMainThread
            (
               async () =>
               {
                  await this.AddAnimationAndHapticFeedback(AnimateButton, IncludeHapticFeedback).WithoutChangingContext();

                  ButtonCommand.Execute(this);

                  _tappedListenerEntered = false;
               }
            );
#else
            await this.AddAnimationAndHapticFeedback(AnimateButton, IncludeHapticFeedback).WithoutChangingContext();

            ButtonCommand.Execute(this);

            _tappedListenerEntered = false;
#endif
         }
         else
         {
            _tappedListenerEntered = false;
         }
      }

      /// <summary>
      /// Lasts the check before assigning style.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <returns>ImageLabelButtonStyle.</returns>
      protected virtual ImageLabelButtonStyle LastCheckBeforeAssigningStyle(ImageLabelButtonStyle value)
      {
         return value;
      }

      /// <summary>
      /// Called when [button command created].
      /// </summary>
      protected virtual void OnButtonCommandCreated()
      {
      }

      /// <summary>
      /// Called when [is enabled changed].
      /// </summary>
      protected virtual void OnIsEnabledChanged()
      {
      }

      /// <summary>
      /// Sets all styles.
      /// </summary>
      protected void SetAllStyles()
      {
         SetButtonStyle();
         SetLabelStyle();
         CallRecreateImageSafely();
      }

      /// <summary>
      /// Sets the button style.
      /// </summary>
      protected void SetButtonStyle()
      {
         if (!CurrentStyleIndexFound(out var styleIdx))
         {
            return;
         }

         // Should never occur due to constraints set up at this class's constructor
         if (styleIdx < 0 || ImageLabelButtonStyles.Count < styleIdx)
         {
            return;
         }

         var newStyle = ImageLabelButtonStyles[styleIdx].ButtonStyle;

         // The button is the shape view ("this")
         Style = newStyle;

         // This library is not working well with styles, so forcing all settings manually
         this.ForceStyle(newStyle);
      }

      /// <summary>
      /// Sets the current style by button text.
      /// </summary>
      /// <param name="buttonText">The button text.</param>
      protected void SetCurrentStyleByButtonText(string buttonText)
      {
         if (ImageLabelButtonStyles.IsEmpty())
         {
            return;
         }

         var foundStyle = ImageLabelButtonStyles.FirstOrDefault(s => s.ButtonText.IsSameAs(buttonText));
         if (foundStyle.IsNotNullOrDefault())
         {
            CurrentStyle = foundStyle;
         }
      }

      /// <summary>
      /// Sets the label style.
      /// </summary>
      protected void SetLabelStyle()
      {
         if (ButtonLabel.IsNullOrDefault() || !CurrentStyleIndexFound(out var styleIdx))
         {
            return;
         }

         var newStyle = ImageLabelButtonStyles[styleIdx].LabelStyle;

#if TEST_FONT_SIZES
         if (newStyle != null && newStyle.Setters.Count == 4 && ButtonLabel != null && ButtonLabel.Text.IsSameAs("D A S H B O A R D"))
         {
            if (newStyle.Setters[3].Value.ToString().IsDifferentThan(16.ToString()))
            {
               Debug.WriteLine(nameof(ImageLabelButtonBase) + ": " + nameof (SetLabelStyle) + ": font size HAS CHANGED suddenly.");
            }
            else
            {
               Debug.WriteLine(nameof(ImageLabelButtonBase) + ": " + nameof (SetLabelStyle) + ": font size is 16, as expected.");
            }
         }
#endif

         ButtonLabel.Style = newStyle;

         // This library is not working well with styles, so forcing all settings manually
         ButtonLabel.ForceStyle(newStyle);

#if SHOW_LABEL_BACK_COLOR
         ButtonLabel.BackgroundColor = Color.Cyan;
#else
         ButtonLabel.BackgroundColor = Color.Transparent;
#endif
      }

      /// <summary>
      /// Starts up.
      /// </summary>
      protected virtual void StartUp()
      {
         _isInstantiating = true;

         // BUGS REQUIRE THIS
         base.Padding = default;
         base.Margin  = default;

         IAmSelectedStatic += HandleStaticSelectionChanges;

         GestureRecognizers.Add(_tapGesture);
         _tapGesture.Tapped += HandleTapGestureTapped;

         // Applies to the base control only
         InputTransparent = false;

         PropertyChanged += (sender, args) =>
                            {
                               if (!_isInstantiating && Bounds.AreValidAndHaveChanged(args.PropertyName, _lastBounds))
                               {
                                  if (ButtonCornerRadiusFactor.HasValue)
                                  {
                                     SetCornerRadius();
                                  }

                                  //_layout.ForceLayout();
                                  //_imageGrid.ForceLayout();
                                  //_labelGrid.ForceLayout();

                                  _lastBounds = Bounds;
                               }
                            };

         BindingContextChanged += (sender, args) =>
                                  {
                                     if (ButtonLabel.IsNotNullOrDefault())
                                     {
                                        ButtonLabel.BindingContext = BindingContext;
                                     }

                                     if (ButtonImage.IsNotNullOrDefault())
                                     {
                                        ButtonImage.BindingContext = BindingContext;
                                     }
                                  };

         Content = _layout;

         _isInstantiating = false;
      }

      /// <summary>
      /// Converts toggle current style.
      /// </summary>
      protected virtual void ToggleCurrentStyle()
      {
         // Corner case: cannot manually deselect if selected and if the SelectionGroup is set
         if (IsSelected && SelectionGroup > 0)
         {
            return;
         }

         switch (SelectionStyle)
         {
            case ImageLabelButtonSelectionStyles.NoSelection:
            case ImageLabelButtonSelectionStyles.SelectionButNoToggleAsFirstTwoStyles:
               break;

            case ImageLabelButtonSelectionStyles.ToggleSelectionAsFirstTwoStyles:

               // Toggle between ButtonStates[0] and ButtonStates[1]
               CurrentStyle = ImageLabelButtonStyles.Count >= 2
                                 ? CurrentStyle.InternalButtonState.IsSameAs(
                                      ImageLabelButtonStyles[0].InternalButtonState)
                                      ? ImageLabelButtonStyles[1]
                                      : ImageLabelButtonStyles[0]
                                 : ImageLabelButtonStyles.IsNotEmpty()
                                    ? ImageLabelButtonStyles[0]
                                    : default;
               break;

            case ImageLabelButtonSelectionStyles.ToggleSelectionThroughAllStyles:

               // Find the current button state; Increment it; If beyond the end of the button states, go back to 0.
               var foundStyle =
                  ImageLabelButtonStyles.FirstOrDefault(
                     style => style.InternalButtonState.IsSameAs(CurrentStyle.InternalButtonState));
               var buttonStateIdx = ImageLabelButtonStyles.IndexOf(foundStyle);
               if (buttonStateIdx < 0)
               {
                  CurrentStyle = ImageLabelButtonStyles.IsNotEmpty() ? ImageLabelButtonStyles[0] : default;
               }
               else
               {
                  buttonStateIdx++;

                  // ReSharper disable once PossibleNullReferenceException
                  CurrentStyle = ImageLabelButtonStyles.Count <= buttonStateIdx
                                    ? ImageLabelButtonStyles[0]
                                    : ImageLabelButtonStyles[buttonStateIdx];
               }

               break;
         }
      }

      /// <summary>
      /// Updates the state of the current style from button.
      /// </summary>
      /// <param name="currentState">State of the current.</param>
      protected void UpdateCurrentStyleFromButtonState(string currentState)
      {
         if (ImageLabelButtonStyles.IsEmpty())
         {
            return;
         }

         var newStyle = ImageLabelButtonStyles.FirstOrDefault(s => s.InternalButtonState.IsSameAs(currentState));

         if (newStyle.IsNotNullOrDefault())
         {
            CurrentStyle = newStyle;

#if FORCE_CURRENT_STYLE_COPY
            CurrentStyle.ButtonStyle = newStyle.ButtonStyle;
            CurrentStyle.ButtonText = newStyle.ButtonText;
            CurrentStyle.GetImageFromResource = newStyle.GetImageFromResource;
            CurrentStyle.ImageFilePath = newStyle.ImageFilePath;
            CurrentStyle.ImageResourceClassType = newStyle.ImageResourceClassType;
            CurrentStyle.LabelStyle = newStyle.LabelStyle;
#endif
         }
      }

      /// <summary>
      /// Broadcasts if selected.
      /// </summary>
      private void BroadcastIfSelected()
      {
         if (SelectionGroup > 0 && IsSelected)
         {
            // Raise a static event to notify others in this selection group that they should be *deselected*
            IAmSelectedStatic?.Invoke(this);
         }
      }

      /// <summary>
      /// Calls the startup.
      /// </summary>
      private void CallStartup()
      {
         StartUp();
      }

      /// <summary>
      /// Currents the style index found.
      /// </summary>
      /// <param name="styleIdx">Index of the style.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      private bool CurrentStyleIndexFound(out int styleIdx)
      {
         return ButtonStateIndexFound(CurrentStyle?.InternalButtonState, out styleIdx);
      }

      /// <summary>
      /// Handles the button command can execute changed.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void HandleButtonCommandCanExecuteChanged
      (
         object    sender,
         EventArgs e
      )
      {
         var newCanExecute = sender is Command senderAsCommand && senderAsCommand.CanExecute(this);

         if (IsEnabled != newCanExecute)
         {
            IsEnabled = newCanExecute;
            OnIsEnabledChanged();
            IsEnabledChanged?.Invoke(IsEnabled);
         }

         // The control is not issuing a property change when we manually set IsEnabled, so handling that case here.
         // Cannot listen to property changes generally in this case.
         SetAllStyles();
      }

      /// <summary>
      /// Handles the static selection changes.
      /// </summary>
      /// <param name="button">The button.</param>
      private void HandleStaticSelectionChanges(IImageLabelButton button)
      {
         // Do not recur onto our own broadcast; also only respond to the same selection group.
         if (button.SelectionGroup == SelectionGroup && !ReferenceEquals(button, this) &&
             (SelectionStyle == ImageLabelButtonSelectionStyles.ToggleSelectionAsFirstTwoStyles ||
              SelectionStyle == ImageLabelButtonSelectionStyles.ToggleSelectionThroughAllStyles && button.IsSelected))
         {
            Deselect();
         }
      }

      /// <summary>
      /// Recreates the image.
      /// </summary>
      private void RecreateImage()
      {
         if (!CurrentStyleIndexFound(out var styleIdx))
         {
            return;
         }

         var imageFileName = ImageLabelButtonStyles[styleIdx].ImageFilePath;

         if (imageFileName.IsEmpty())
         {
            return;
         }

         if (CurrentStyle.GetImageFromResource && CurrentStyle.ImageResourceClassType.IsNullOrDefault())
         {
            return;
         }

         ButtonImage =
            FormsUtils.GetImage(imageFileName, ImageWidth, ImageHeight,
                                getFromResources: CurrentStyle.GetImageFromResource,
                                resourceClass: CurrentStyle.ImageResourceClassType);

         // The image always has a transparent background
         ButtonImage.BackgroundColor = Color.Transparent;
      }

      /// <summary>
      /// Releases the unmanaged resources.
      /// </summary>
      private void ReleaseUnmanagedResources()
      {
         // Global static, so remove the handler
         IAmSelectedStatic -= HandleStaticSelectionChanges;

         _tapGesture.Tapped -= HandleTapGestureTapped;

         RemoveButtonCommandEventListener();
      }

      /// <summary>
      /// Removes the button command event listener.
      /// </summary>
      private void RemoveButtonCommandEventListener()
      {
         if (ButtonCommand != null)
         {
            ButtonCommand.CanExecuteChanged -= HandleButtonCommandCanExecuteChanged;
         }
      }

      /// <summary>
      /// Sets the corner radius.
      /// </summary>
      private void SetCornerRadius()
      {
         if (ButtonCornerRadiusFactor.HasValue && Bounds.IsValid())
         {
            CornerRadius =
               Convert.ToSingle(Math.Min(Bounds.Width, Bounds.Height) * ButtonCornerRadiusFactor.GetValueOrDefault());
         }
         else if (ButtonCornerRadiusFixed.HasValue)
         {
            CornerRadius = Convert.ToSingle(ButtonCornerRadiusFixed);
         }
         else
         {
            CornerRadius = Convert.ToSingle(FormsConst.DEFAULT_SHAPE_VIEW_RADIUS);
         }
      }

      /// <summary>
      /// Sets up complete button command binding.
      /// </summary>
      private void SetUpCompleteButtonCommandBinding()
      {
         if (ButtonCommandBindingName.IsEmpty())
         {
            RemoveBinding(ButtonCommandProperty);
         }
         else
         {
            this.SetUpBinding
            (
               ButtonCommandProperty,
               ButtonCommandBindingName,
               BindingMode.OneWay,
               ButtonCommandConverter,
               ButtonCommandConverterParameter,
               null,
               ButtonCommandSource
            );
         }
      }

      /// <summary>
      /// Shares the button and label positions.
      /// </summary>
      private void ShareButtonAndLabelPositions()
      {
         // Callers must verify this
         if (ButtonLabel.IsNullOrDefault() || ButtonImage.IsNullOrDefault())
         {
            return;
         }

         _layout.Children.Clear();

         _layout.AddOverlayBasedOnPosition(ButtonImage, ImagePos, ImageWidth,               ImageHeight);
         _layout.AddOverlayBasedOnPosition(ButtonLabel, LabelPos, ButtonLabel.WidthRequest, ButtonLabel.HeightRequest);
      }

      /// <summary>
      /// Updates the button text.
      /// </summary>
      private void UpdateButtonText()
      {
         if (!UpdateButtonTextFromStyle || ButtonLabel == null || CurrentStyle.IsNullOrDefault())
         {
            return;
         }

         ButtonLabel.Text = CurrentStyle.ButtonText;
      }
   }
}