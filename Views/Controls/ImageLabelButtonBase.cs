// *********************************************************************************
// <copyright file=ImageLabelButtonBase.cs company="Marcus Technical Services, Inc.">
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

// #define USE_BACK_COLOR

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Com.MarcusTS.SharedForms.Common.Utils;
   using Com.MarcusTS.SharedUtils.Utils;
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using Xamarin.Forms;

   /// <summary>
   ///    Enum SelectionStyles
   /// </summary>
   public enum SelectionStyles
   {
      /// <summary>
      ///    The no selection
      /// </summary>
      NoSelection,

      /// <summary>
      ///    The selection but no toggle as first two styles
      /// </summary>
      SelectionButNoToggleAsFirstTwoStyles,

      /// <summary>
      ///    Converts to ggleselectionasfirsttwostyles.
      /// </summary>
      ToggleSelectionAsFirstTwoStyles,

      /// <summary>
      ///    Converts to ggleselectionthroughallstyles.
      /// </summary>
      ToggleSelectionThroughAllStyles
   }

   /// <summary>
   ///    Interface IImageLabelButton
   ///    Implements the <see cref="System.IDisposable" />
   ///    Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="System.IDisposable" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IImageLabelButton : IDisposable, INotifyPropertyChanged
   {
      /// <summary>
      ///    Gets or sets a value indicating whether [animate button].
      /// </summary>
      /// <value><c>true</c> if [animate button]; otherwise, <c>false</c>.</value>
      bool AnimateButton { get; set; }

      /// <summary>
      ///    Gets or sets the button command.
      /// </summary>
      /// <value>The button command.</value>
      Command ButtonCommand { get; set; }

      /// <summary>
      ///    Gets or sets the name of the button command binding.
      /// </summary>
      /// <value>The name of the button command binding.</value>
      string ButtonCommandBindingName { get; set; }

      /// <summary>
      ///    Gets or sets the button command converter.
      /// </summary>
      /// <value>The button command converter.</value>
      IValueConverter ButtonCommandConverter { get; set; }

      /// <summary>
      ///    Gets or sets the button command converter parameter.
      /// </summary>
      /// <value>The button command converter parameter.</value>
      object ButtonCommandConverterParameter { get; set; }

      /// <summary>
      ///    Gets or sets the button command source.
      /// </summary>
      /// <value>The button command source.</value>
      object ButtonCommandSource { get; set; }

      /// <summary>
      ///    Gets or sets the button command string format.
      /// </summary>
      /// <value>The button command string format.</value>
      string ButtonCommandStringFormat { get; set; }

      /// <summary>
      ///    Gets or sets the button corner radius factor.
      /// </summary>
      /// <value>The button corner radius factor.</value>
      double? ButtonCornerRadiusFactor { get; set; }

      /// <summary>
      ///    Gets or sets the button corner radius fixed.
      /// </summary>
      /// <value>The button corner radius fixed.</value>
      double? ButtonCornerRadiusFixed { get; set; }

      /// <summary>
      ///    Gets or sets the button image.
      /// </summary>
      /// <value>The button image.</value>
      Image ButtonImage { get; set; }

      /// <summary>
      ///    Gets or sets the button label.
      /// </summary>
      /// <value>The button label.</value>
      Label ButtonLabel { get; set; }

      /// <summary>
      ///    Gets or sets the state of the button.
      /// </summary>
      /// <value>The state of the button.</value>
      string ButtonState { get; set; }

      /// <summary>
      ///    Gets or sets the current style.
      /// </summary>
      /// <value>The current style.</value>
      ImageLabelButtonStyle CurrentStyle { get; set; }

      /// <summary>
      ///    Gets or sets the height of the image.
      /// </summary>
      /// <value>The height of the image.</value>
      double ImageHeight { get; set; }

      /// <summary>
      ///    Gets the image label button styles.
      /// </summary>
      /// <value>The image label button styles.</value>
      IList<ImageLabelButtonStyle> ImageLabelButtonStyles { get; }

      /// <summary>
      ///    Gets or sets the image position.
      /// </summary>
      /// <value>The image position.</value>
      ViewUtils.OnScreenPositions ImagePos { get; set; }

      /// <summary>
      ///    Gets or sets the width of the image.
      /// </summary>
      /// <value>The width of the image.</value>
      double ImageWidth { get; set; }

      /// <summary>
      ///    Gets or sets a value indicating whether [include haptic feedback].
      /// </summary>
      /// <value><c>true</c> if [include haptic feedback]; otherwise, <c>false</c>.</value>
      bool IncludeHapticFeedback { get; set; }

      /// <summary>
      ///    Gets or sets a value indicating whether this instance is instantiating.
      /// </summary>
      /// <value><c>true</c> if this instance is instantiating; otherwise, <c>false</c>.</value>
      bool IsInstantiating { get; set; }

      /// <summary>
      ///    Gets a value indicating whether this instance is selected.
      /// </summary>
      /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
      bool IsSelected { get; }

      /// <summary>
      ///    Gets or sets the height of the label.
      /// </summary>
      /// <value>The height of the label.</value>
      double LabelHeight { get; set; }

      /// <summary>
      ///    Gets or sets the label position.
      /// </summary>
      /// <value>The label position.</value>
      ViewUtils.OnScreenPositions LabelPos { get; set; }

      /// <summary>
      ///    Gets or sets the width of the label.
      /// </summary>
      /// <value>The width of the label.</value>
      double LabelWidth { get; set; }

      /// <summary>
      ///    Gets or sets the pseudo padding.
      /// </summary>
      /// <value>The pseudo padding.</value>
      Thickness PseudoPadding { get; set; }

      /// <summary>
      ///    Gets or sets the selection group.
      /// </summary>
      /// <value>The selection group.</value>
      int SelectionGroup { get; set; }

      /// <summary>
      ///    Gets or sets the selection style.
      /// </summary>
      /// <value>The selection style.</value>
      SelectionStyles SelectionStyle { get; set; }

      /// <summary>
      ///    Gets a value indicating whether [update button text from style].
      /// </summary>
      /// <value><c>true</c> if [update button text from style]; otherwise, <c>false</c>.</value>
      bool UpdateButtonTextFromStyle { get; }

      /// <summary>
      ///    Occurs when [button state changed].
      /// </summary>
      event EventUtils.GenericDelegate<string> ButtonStateChanged;

      /// <summary>
      ///    Occurs when [image label button pressed].
      /// </summary>
      event EventUtils.NoParamsDelegate ImageLabelButtonPressed;
   }

   /// <summary>
   ///    A button that can contain either an image and/or a label.
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ShapeView" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IImageLabelButton" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ShapeView" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IImageLabelButton" />
   public abstract class ImageLabelButtonBase : ShapeView, IImageLabelButton
   {
      /// <summary>
      ///    The default button radius factor
      /// </summary>
      public const float DEFAULT_BUTTON_RADIUS_FACTOR = 0.12f;

      /// <summary>
      ///    The default pseudo padding
      /// </summary>
      protected const float DEFAULT_PSEUDO_PADDING = 5;

      /// <summary>
      ///    The animate button property
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
      ///    The button command binding name property
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
      ///    The button command converter parameter property
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
      ///    The button command converter property
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
      ///    The button command property
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
      ///    The button command string format property
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
      ///    The button image property
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
      ///    The button label property
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
      ///    The corner radius factor property
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
      ///    The corner radius fixed property
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
      ///    The current style property
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
      ///    The image height property
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
      ///    The image position property
      /// </summary>
      public static readonly BindableProperty ImagePosProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(ImagePos),
            default(ViewUtils.OnScreenPositions),
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
      ///    The image width property
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
      ///    The include haptic feedback property
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
      ///    The label position property
      /// </summary>
      public static readonly BindableProperty LabelPosProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(LabelPos),
            default(ViewUtils.OnScreenPositions),
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
      ///    The selection group property
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
      ///    The selection style property
      /// </summary>
      public static readonly BindableProperty SelectionStyleProperty =
         CreateToggleImageLabelButtonBindableProperty
         (
            nameof(SelectionStyle),
            default(SelectionStyles),
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
      ///    The layout
      /// </summary>
      private readonly AbsoluteLayout _layout = new AbsoluteLayout { BackgroundColor = Color.Transparent };

      /// <summary>
      ///    The tap gesture
      /// </summary>
      private readonly TapGestureRecognizer _tapGesture = new TapGestureRecognizer();

      /// <summary>
      ///    The button command
      /// </summary>
      private Command _buttonCommand;

      /// <summary>
      ///    The button command binding name
      /// </summary>
      private string _buttonCommandBindingName;

      /// <summary>
      ///    The button command converter
      /// </summary>
      private IValueConverter _buttonCommandConverter;

      /// <summary>
      ///    The button command converter parameter
      /// </summary>
      private object _buttonCommandConverterParameter;

      /// <summary>
      ///    The button command source
      /// </summary>
      private object _buttonCommandSource;

      /// <summary>
      ///    The button image
      /// </summary>
      private Image _buttonImage;

      /// <summary>
      ///    The button label
      /// </summary>
      private Label _buttonLabel;

      /// <summary>
      ///    The button state
      /// </summary>
      private string _buttonState;

      /// <summary>
      ///    The button state assigned from style
      /// </summary>
      private bool _buttonStateAssignedFromStyle;

      /// <summary>
      ///    The corner radius factor
      /// </summary>
      private double? _cornerRadiusFactor;

      /// <summary>
      ///    The corner radius fixed
      /// </summary>
      private double? _cornerRadiusFixed;

      /// <summary>
      ///    The current style
      /// </summary>
      private ImageLabelButtonStyle _currentStyle;

      /// <summary>
      ///    The image height
      /// </summary>
      private double _imageHeight;

      /// <summary>
      ///    The image position
      /// </summary>
      private ViewUtils.OnScreenPositions _imagePos;

      /// <summary>
      ///    The image width
      /// </summary>
      private double _imageWidth;

      /// <summary>
      ///    The is instantiating
      /// </summary>
      private volatile bool _isInstantiating;

      /// <summary>
      ///    The is releasing
      /// </summary>
      private volatile bool _isReleasing;

      /// <summary>
      ///    The label height
      /// </summary>
      private double _labelHeight;

      /// <summary>
      ///    The label position
      /// </summary>
      private ViewUtils.OnScreenPositions _labelPos;

      /// <summary>
      ///    The label width
      /// </summary>
      private double _labelWidth;

      /// <summary>
      ///    The last bounds
      /// </summary>
      private Rectangle _lastBounds;

      /// <summary>
      ///    The last height
      /// </summary>
      private double _lastHeight;

      /// <summary>
      ///    The last image file name
      /// </summary>
      private string _lastImageFileName;

      /// <summary>
      ///    The last width
      /// </summary>
      private double _lastWidth;

      /// <summary>
      ///    The pseudo padding
      /// </summary>
      private Thickness _pseudoPadding = new Thickness(DEFAULT_PSEUDO_PADDING);

      /// <summary>
      ///    The rearrange content entered
      /// </summary>
      private volatile bool _rearrangeContentEntered;

      /// <summary>
      ///    The selection group
      /// </summary>
      private int _selectionGroup;

      /// <summary>
      ///    The selection style
      /// </summary>
      private SelectionStyles _selectionStyle;

      /// <summary>
      ///    The tapped listener entered
      /// </summary>
      private volatile bool _tappedListenerEntered;

      /// <summary>
      ///    Initializes a new instance of the <see cref="ImageLabelButtonBase" /> class.
      /// </summary>
      protected ImageLabelButtonBase()
      {
         CallStartup();

         PropertyChanged +=
            (
               sender,
               args
            ) =>
            {
               if (
                  ButtonCornerRadiusFactor.HasValue
                &&
                  (
                     args.PropertyName.IsSameAs("Width") && Bounds.Width.IsDifferentThan(_lastWidth)
                   ||
                     args.PropertyName.IsSameAs("Height") && Bounds.Width.IsDifferentThan(_lastHeight)
                  )
               )
               {
                  SetCornerRadius();

                  _lastWidth  = Bounds.Width;
                  _lastHeight = Bounds.Height;
               }
            };

#if USE_BACK_COLOR
         Color = Color.Yellow;
#endif
      }

      /// <summary>
      ///    Gets a value indicating whether this instance is disabled.
      /// </summary>
      /// <value><c>true</c> if this instance is disabled; otherwise, <c>false</c>.</value>
      protected abstract bool IsDisabled { get; }

      /// <summary>
      ///    Gets the current size best guess.
      /// </summary>
      /// <value>The current size best guess.</value>
      private Size CurrentSizeBestGuess => HeightRequest.IsNotEmpty() && WidthRequest.IsNotEmpty() ? new Size(WidthRequest, HeightRequest) : Bounds.Size;

      // BUGS REQUIRE THIS
      /// <summary>
      ///    Gets or sets the margin for the view.
      /// </summary>
      /// <value>To be added.</value>
      /// <remarks>To be added.</remarks>
      private new Thickness Margin { get; set; }

      // BUGS REQUIRE THIS
      /// <summary>
      ///    Gets or sets the inner padding of the Layout.
      /// </summary>
      /// <value>The Thickness values for the layout. The default value is a Thickness with all values set to 0.</value>
      /// <remarks>
      ///    <para>
      ///       The padding is the space between the bounds of a layout and the bounding region into which its children should be
      ///       arranged into.
      ///    </para>
      ///    <para>
      ///       The following example shows setting the padding of a Layout to inset its children.
      ///    </para>
      ///    <example>
      ///       <code lang="csharp lang-csharp"><![CDATA[
      /// var stackLayout = new StackLayout {
      /// Padding = new Thickness (10, 10, 10, 20),
      /// Children = {
      /// new Label {Text = "Hello"},
      /// new Label {Text = "World"}
      /// }
      /// }
      /// ]]></code>
      ///    </example>
      /// </remarks>
      private new Thickness Padding { get; set; }

      /// <summary>
      ///    Occurs when [button state changed].
      /// </summary>
      public event EventUtils.GenericDelegate<string> ButtonStateChanged;

      /// <summary>
      ///    Occurs when [image label button pressed].
      /// </summary>
      public event EventUtils.NoParamsDelegate ImageLabelButtonPressed;

      /// <summary>
      ///    Gets or sets a value indicating whether [animate button].
      /// </summary>
      /// <value><c>true</c> if [animate button]; otherwise, <c>false</c>.</value>
      public bool AnimateButton { get; set; } = true;

      /// <summary>
      ///    Gets or sets the button command.
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
      ///    Gets or sets the name of the button command binding.
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
      ///    Gets or sets the button command converter.
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
      ///    Gets or sets the button command converter parameter.
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
      ///    Gets or sets the button command source.
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
      ///    Gets or sets the button command string format.
      /// </summary>
      /// <value>The button command string format.</value>
      public string ButtonCommandStringFormat { get; set; }

      /// <summary>
      ///    Gets or sets the button corner radius factor.
      /// </summary>
      /// <value>The button corner radius factor.</value>
      public double? ButtonCornerRadiusFactor
      {
         get => _cornerRadiusFactor;
         set
         {
            if (_cornerRadiusFactor.IsNotAnEqualObjectTo(value))
            {
               _cornerRadiusFactor = value;
               SetCornerRadius();
            }
         }
      }

      /// <summary>
      ///    Gets or sets the button corner radius fixed.
      /// </summary>
      /// <value>The button corner radius fixed.</value>
      public double? ButtonCornerRadiusFixed
      {
         get => _cornerRadiusFixed;
         set
         {
            if (_cornerRadiusFixed.IsNotAnEqualObjectTo(value))
            {
               _cornerRadiusFixed = value;
               SetCornerRadius();
            }
         }
      }

      /// <summary>
      ///    Gets or sets the button image.
      /// </summary>
      /// <value>The button image.</value>
      public Image ButtonImage
      {
         get => _buttonImage;
         set
         {
            if (_buttonImage != null)
            {
               if (_layout.Children.Contains(_buttonImage))
               {
                  _layout.Children.Remove(_buttonImage);
               }
            }

            _buttonImage = value;

            CallRecreateImageSafely();

            if (_buttonImage != null)
            {
               _layout.Children.Add(_buttonImage);
            }
         }
      }

      /// <summary>
      ///    Gets or sets the button label.
      /// </summary>
      /// <value>The button label.</value>
      public Label ButtonLabel
      {
         get => _buttonLabel;
         set
         {
            if (_buttonLabel != null)
            {
               if (_layout.Children.Contains(_buttonLabel))
               {
                  _layout.Children.Remove(_buttonLabel);
               }
            }

            _buttonLabel = value;

            if (_buttonLabel != null)
            {
               _layout.Children.Add(_buttonLabel);
            }

            SetLabelStyle();
            RearrangeContent();
         }
      }

      /// <summary>
      ///    Gets or sets the state of the button.
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
                  BroadcastIfSelected();
                  ButtonStateChanged?.Invoke(ButtonState);
                  OnButtonStateChanged();
               }
            }
            else
            {
               // Change the style and let it change the button state.
               var newStyle = ImageLabelButtonStyles.FirstOrDefault(s => s.InternalButtonState.IsSameAs(value));

               if (newStyle.IsNotAnEqualObjectTo(default(ImageLabelButtonStyle)))
               {
                  CurrentStyle = newStyle;
               }
            }
         }
      }

      /// <summary>
      ///    Gets or sets the current style.
      /// </summary>
      /// <value>The current style.</value>
      public ImageLabelButtonStyle CurrentStyle
      {
         get
         {
            if (_currentStyle.IsAnEqualObjectTo(default(ImageLabelButtonStyle)))
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
            if (_currentStyle.IsNotAnEqualObjectTo(value))
            {
               _currentStyle = value;

               _buttonStateAssignedFromStyle = true;
               ButtonState                   = _currentStyle.InternalButtonState;
               UpdateButtonText();
               SetAllStyles();
               _buttonStateAssignedFromStyle = false;
            }
         }
      }

      /// <summary>
      ///    Gets or sets the height of the image.
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
      ///    Gets the image label button styles.
      /// </summary>
      /// <value>The image label button styles.</value>
      public abstract IList<ImageLabelButtonStyle> ImageLabelButtonStyles { get; }

      /// <summary>
      ///    Gets or sets the image position.
      /// </summary>
      /// <value>The image position.</value>
      public ViewUtils.OnScreenPositions ImagePos
      {
         get => _imagePos;
         set
         {
            if (_imagePos != value)
            {
               _imagePos = value;
               RearrangeContent();
            }
         }
      }

      /// <summary>
      ///    Gets or sets the width of the image.
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
      ///    Gets or sets a value indicating whether [include haptic feedback].
      /// </summary>
      /// <value><c>true</c> if [include haptic feedback]; otherwise, <c>false</c>.</value>
      public bool IncludeHapticFeedback { get; set; } = true;

      /// <summary>
      ///    Gets or sets a value indicating whether this instance is instantiating.
      /// </summary>
      /// <value><c>true</c> if this instance is instantiating; otherwise, <c>false</c>.</value>
      public bool IsInstantiating
      {
         get => _isInstantiating;
         set
         {
            _isInstantiating = value;
            RearrangeContent();
         }
      }

      /// <summary>
      ///    Gets a value indicating whether this instance is selected.
      /// </summary>
      /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
      public abstract bool IsSelected { get; }

      /// <summary>
      ///    Gets or sets the height of the label.
      /// </summary>
      /// <value>The height of the label.</value>
      public double LabelHeight
      {
         get => _labelHeight;
         set
         {
            if (_labelHeight.IsDifferentThan(value))
            {
               _labelHeight = value;
               RearrangeContent();
            }
         }
      }

      /// <summary>
      ///    Gets or sets the label position.
      /// </summary>
      /// <value>The label position.</value>
      public ViewUtils.OnScreenPositions LabelPos
      {
         get => _labelPos;
         set
         {
            if (_labelPos != value)
            {
               _labelPos = value;
               RearrangeContent();
            }
         }
      }

      /// <summary>
      ///    Gets or sets the width of the label.
      /// </summary>
      /// <value>The width of the label.</value>
      public double LabelWidth
      {
         get => _labelWidth;
         set
         {
            if (_labelWidth.IsDifferentThan(value))
            {
               _labelWidth = value;
               RearrangeContent();
            }
         }
      }

      /// <summary>
      ///    Gets or sets the pseudo padding.
      /// </summary>
      /// <value>The pseudo padding.</value>
      public Thickness PseudoPadding
      {
         get => _pseudoPadding;
         set
         {
            _pseudoPadding = value;
            RearrangeContent();
         }
      }

      /// <summary>
      ///    Gets or sets the selection group.
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
      ///    Gets or sets the selection style.
      /// </summary>
      /// <value>The selection style.</value>
      public SelectionStyles SelectionStyle
      {
         get => _selectionStyle;
         set
         {
            _selectionStyle = value;
            SetAllStyles();
         }
      }

      /// <summary>
      ///    Gets a value indicating whether [update button text from style].
      /// </summary>
      /// <value><c>true</c> if [update button text from style]; otherwise, <c>false</c>.</value>
      public abstract bool UpdateButtonTextFromStyle { get; }

      /// <summary>
      ///    Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         ReleaseUnmanagedResources();
         GC.SuppressFinalize(this);
      }

      /// <summary>
      ///    Finalizes an instance of the <see cref="ImageLabelButtonBase" /> class.
      /// </summary>
      ~ImageLabelButtonBase()
      {
         ReleaseUnmanagedResources();
      }

      /// <summary>
      ///    Occurs when [i am selected static].
      /// </summary>
      protected static event EventUtils.GenericDelegate<IImageLabelButton> IAmSelectedStatic;

      /// <summary>
      ///    Creates the button style.
      /// </summary>
      /// <param name="backColor">Color of the back.</param>
      /// <param name="borderWidth">Width of the border.</param>
      /// <param name="borderColor">Color of the border.</param>
      /// <returns>Style.</returns>
      public static Style CreateButtonStyle
      (
         Color   backColor,
         double? borderWidth = null,
         Color   borderColor = default
      )
      {
         return new Style(typeof(ImageLabelButtonBase))
         {
            Setters =
            {
               new Setter { Property = BorderColorProperty, Value = borderColor },
               new Setter { Property = BorderWidthProperty, Value = borderWidth.GetValueOrDefault() },
               new Setter { Property = ColorProperty, Value       = backColor }
            }
         };
      }

      /// <summary>
      ///    Creates the label style.
      /// </summary>
      /// <param name="textColor">Color of the text.</param>
      /// <param name="fontSize">Size of the font.</param>
      /// <param name="fontAttributes">The font attributes.</param>
      /// <returns>Style.</returns>
      public static Style CreateLabelStyle
      (
         Color          textColor,
         double         fontSize,
         FontAttributes fontAttributes = FontAttributes.None
      )
      {
         return new Style(typeof(Label))
         {
            Setters =
            {
               // The text color is now the background color -- should be white
               new Setter { Property = Label.TextColorProperty, Value = textColor },

               // The label is always transparent
               new Setter { Property = BackgroundColorProperty, Value = Color.Transparent },

               new Setter { Property = Label.FontAttributesProperty, Value = fontAttributes },
               new Setter { Property = Label.FontSizeProperty, Value       = fontSize }
            }
         };
      }

      /// <summary>
      ///    Creates the toggle image label button bindable property.
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
      ///    Releases unmanaged and - optionally - managed resources.
      /// </summary>
      /// <param name="disposing">
      ///    <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
      ///    unmanaged resources.
      /// </param>
      public void Dispose(bool disposing)
      {
         ReleaseUnmanagedResources();
         if (disposing)
         {
         }
      }

      /// <summary>
      ///    Afters the content set.
      /// </summary>
      protected virtual void AfterContentSet()
      {
      }

      /// <summary>
      ///    Calls the recreate image safely.
      /// </summary>
      protected void CallRecreateImageSafely()
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
      ///    Deselects this instance.
      /// </summary>
      protected abstract void Deselect();

      /// <summary>
      ///    Handles the tap gesture tapped.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      protected void HandleTapGestureTapped
      (
         object    sender,
         EventArgs e
      )
      {
         if (_tappedListenerEntered || IsDisabled || ImageLabelButtonStyles.IsEmpty() || CurrentStyle.IsNullOrDefault() || CurrentStyle.InternalButtonState.IsEmpty())
         {
            return;
         }

         _tappedListenerEntered = true;

         ImageLabelButtonPressed?.Invoke();

         ToggleCurrentStyle();

         // If a command exists, fire it and reset our selected status to false; otherwise, leave the
         // selected state as it is.
         if (ButtonCommand != null && CurrentStyle.FireCommand)
         {
            Device.BeginInvokeOnMainThread
            (
               async () =>
               {
                  await this.AddAnimationAndHapticFeedback(AnimateButton, IncludeHapticFeedback);

                  ButtonCommand.Execute(this);

                  _tappedListenerEntered = false;
               }
            );
         }
         else
         {
            _tappedListenerEntered = false;
         }
      }

      /// <summary>
      ///    Called when [button command created].
      /// </summary>
      protected virtual void OnButtonCommandCreated()
      {
      }

      /// <summary>
      ///    Called when [button state changed].
      /// </summary>
      protected virtual void OnButtonStateChanged()
      {
      }

      /// <summary>
      ///    Sets all styles.
      /// </summary>
      protected void SetAllStyles()
      {
         SetButtonStyle();
         SetLabelStyle();
         CallRecreateImageSafely();
      }

      /// <summary>
      ///    Sets the button style.
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

         // The style belongs to "this"
         Style = newStyle;

         // This library is not working well with styles, so forcing all settings manually
         this.ForceStyle(newStyle);
      }

      /// <summary>
      ///    Sets the current style by button text.
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
      ///    Sets the label style.
      /// </summary>
      protected void SetLabelStyle()
      {
         if (!CurrentStyleIndexFound(out var styleIdx) || ButtonLabel == null)
         {
            return;
         }

         var newStyle = ImageLabelButtonStyles[styleIdx].LabelStyle;

         // The style belongs to "this"
         ButtonLabel.Style = newStyle;

         // This library is not working well with styles, so forcing all settings manually
         ButtonLabel.ForceStyle(newStyle);

         // Must be transparent
         ButtonLabel.BackgroundColor  = Color.Transparent;
         ButtonLabel.InputTransparent = true;
      }

      /// <summary>
      ///    Starts up.
      /// </summary>
      protected virtual void StartUp()
      {
         IsInstantiating = true;

         // BUGS REQUIRE THIS
         base.Padding = default;
         base.Margin  = default;

         IAmSelectedStatic += HandleStaticSelectionChanges;

         GestureRecognizers.Add(_tapGesture);
         _tapGesture.Tapped += HandleTapGestureTapped;

         ShapeType = ShapeType.Box;

         // Applies to the base control only
         InputTransparent = false;

         PropertyChanged += HandlePropertyChanged;

         // CurrentStyle = ImageLabelButtonStyles?[0];

         IsInstantiating = false;
      }

      /// <summary>
      ///    Broadcasts if selected.
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
      ///    Calls the startup.
      /// </summary>
      private void CallStartup()
      {
         StartUp();
      }

      /// <summary>
      ///    Currents the style index found.
      /// </summary>
      /// <param name="styleIdx">Index of the style.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      private bool CurrentStyleIndexFound(out int styleIdx)
      {
         styleIdx = -1;

         if (ImageLabelButtonStyles.IsEmpty())
         {
            return false;
         }

         styleIdx = ImageLabelButtonStyles.IndexOf(CurrentStyle);

         // Should never occur due to constraints set up at this class's constructor
         if (styleIdx < 0 || ImageLabelButtonStyles.Count < styleIdx)
         {
            return false;
         }

         return true;
      }

      /// <summary>
      ///    Handles the button command can execute changed.
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

         IsEnabled = newCanExecute;

         // The control is not issuing a property change when we manually set IsEnabled, so handling
         //    that case here. Cannot listen to property changes generally in this case.
         SetAllStyles();
      }

      /// <summary>
      ///    Handles the property changed.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
      private void HandlePropertyChanged
      (
         object                   sender,
         PropertyChangedEventArgs e
      )
      {
         if (!IsInstantiating && Bounds.Width > 0 && Bounds.Height > 0 && (Bounds.Width.IsDifferentThan(_lastBounds.Width) || Bounds.Height.IsDifferentThan(_lastBounds.Height)))
         {
            RearrangeContent();
            _lastBounds = Bounds;
         }
      }

      /// <summary>
      ///    Handles the static selection changes.
      /// </summary>
      /// <param name="button">The button.</param>
      private void HandleStaticSelectionChanges(IImageLabelButton button)
      {
         // Do not recur onto our own broadcast; also only respond to the same selection group.
         if (button.SelectionGroup == SelectionGroup && !ReferenceEquals(button, this) && (SelectionStyle == SelectionStyles.ToggleSelectionAsFirstTwoStyles || SelectionStyle == SelectionStyles.ToggleSelectionThroughAllStyles) && button.IsSelected)
         {
            Deselect();
         }
      }

      /// <summary>
      ///    Rearranges the content.
      /// </summary>
      private void RearrangeContent()
      {
         if (_rearrangeContentEntered || _isReleasing)
         {
            return;
         }

         _rearrangeContentEntered = true;

         try
         {
            var sizeGuess = CurrentSizeBestGuess;

            if (sizeGuess.Width <= 0 || sizeGuess.Height < 0)
            {
               return;
            }

            // Do the image first, as it might underlie the label
            if (ButtonImage != null && ImagePos != ViewUtils.OnScreenPositions.NONE)
            {
               // Padding is ignored by Xamarin because we are using an animated approach, so it can only be enforced manually in the layout call below.
               var imageOnScreenRect = ViewUtils.CreateOnScreenRect(sizeGuess, ImageWidth, ImageHeight, ImagePos, PseudoPadding);

               AbsoluteLayout.SetLayoutBounds(ButtonImage, imageOnScreenRect);
            }

            if (ButtonLabel != null && LabelPos != ViewUtils.OnScreenPositions.NONE)
            {
               // Padding is ignored by Xamarin because we are using an animated approach, so it can only be enforced manually in the layout call below.
               var labelOnScreenRect = ViewUtils.CreateOnScreenRect(sizeGuess, LabelWidth, LabelHeight, LabelPos, PseudoPadding);

               AbsoluteLayout.SetLayoutBounds(ButtonLabel, labelOnScreenRect);
            }

            Content = _layout;

            AfterContentSet();
         }
         finally
         {
            _rearrangeContentEntered = false;
         }
      }

      /// <summary>
      ///    Recreates the image.
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

         if (ButtonImage != null && imageFileName.IsSameAs(_lastImageFileName))
         {
            return;
         }

         ButtonImage =
            FormsUtils.GetImage(imageFileName, ImageWidth, ImageHeight, getFromResources: CurrentStyle.GetImageFromResource, resourceClass: CurrentStyle.ImageResourceClassType);

         // The image always has a transparent background
         ButtonImage.BackgroundColor = Color.Transparent;

         ButtonImage.InputTransparent = true;

         _lastImageFileName = imageFileName;

         RearrangeContent();
      }

      /// <summary>
      ///    Releases the unmanaged resources.
      /// </summary>
      private void ReleaseUnmanagedResources()
      {
         _isReleasing = true;

         // Global static, so remove the handler
         IAmSelectedStatic -= HandleStaticSelectionChanges;

         _tapGesture.Tapped -= HandleTapGestureTapped;

         RemoveButtonCommandEventListener();
      }

      /// <summary>
      ///    Removes the button command event listener.
      /// </summary>
      private void RemoveButtonCommandEventListener()
      {
         if (ButtonCommand != null)
         {
            ButtonCommand.CanExecuteChanged -= HandleButtonCommandCanExecuteChanged;
         }
      }

      /// <summary>
      ///    Sets the corner radius.
      /// </summary>
      private void SetCornerRadius()
      {
         if (ButtonCornerRadiusFactor.HasNoValue())
         {
            ButtonCornerRadiusFactor = FormsUtils.BUTTON_RADIUS_FACTOR;
         }

         if (ButtonCornerRadiusFactor.HasValue)
         {
            CornerRadius = Convert.ToSingle(Math.Min(Bounds.Width, Bounds.Height) * _cornerRadiusFactor);
         }
         else if (ButtonCornerRadiusFixed.HasValue)
         {
            CornerRadius = Convert.ToSingle(ButtonCornerRadiusFixed);
         }
         else
         {
            CornerRadius = Convert.ToSingle(FormsUtils.BUTTON_RADIUS_FACTOR);
         }
      }

      /// <summary>
      ///    Sets up complete button command binding.
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
      ///    Converts to gglecurrentstyle.
      /// </summary>
      private void ToggleCurrentStyle()
      {
         switch (SelectionStyle)
         {
            case SelectionStyles.NoSelection:
               break;

            case SelectionStyles.ToggleSelectionAsFirstTwoStyles:
            case SelectionStyles.SelectionButNoToggleAsFirstTwoStyles:
               // Toggle between ButtonStates[0] and ButtonStates[1]
               CurrentStyle = ImageLabelButtonStyles.Count >= 2 ? CurrentStyle.IsAnEqualObjectTo(ImageLabelButtonStyles[0]) ? ImageLabelButtonStyles[1] : ImageLabelButtonStyles[0] : ImageLabelButtonStyles.Any() ? ImageLabelButtonStyles[0] : default;
               break;

            case SelectionStyles.ToggleSelectionThroughAllStyles:
               // Find the current button state;
               // Increment it;
               // If beyond the end of the button states, go back to 0.
               var buttonStateIdx = ImageLabelButtonStyles.IndexOf(CurrentStyle);
               if (buttonStateIdx < 0)
               {
                  CurrentStyle = ImageLabelButtonStyles.Any() ? ImageLabelButtonStyles[0] : default;
               }
               else
               {
                  buttonStateIdx++;

                  // ReSharper disable once PossibleNullReferenceException
                  CurrentStyle = ImageLabelButtonStyles.Count <= buttonStateIdx ? ImageLabelButtonStyles[0] : ImageLabelButtonStyles[buttonStateIdx];
               }

               break;
         }
      }

      /// <summary>
      ///    Updates the button text.
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