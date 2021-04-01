#define NO_THREADS
// #define FORCE_CURRENT_STYLE_COPY

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Linq;
   using Common.Interfaces;
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>Enum SelectionStyles</summary>
   public enum ImageLabelButtonSelectionStyles
   {
      /// <summary>The no selection</summary>
      NoSelection,

      /// <summary>The selection but no toggle as first two styles</summary>
      SelectionButNoToggleAsFirstTwoStyles,

      /// <summary>Toggles selection between the first and second styles ONLY.</summary>
      ToggleSelectionAsFirstTwoStyles,

      /// <summary>Toggles selection through all styles.</summary>
      ToggleSelectionThroughAllStyles
   }

   /// <summary>
   ///    Interface IImageLabelButton Implements the <see cref="System.IDisposable" /> Implements the
   ///    <see
   ///       cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="System.IDisposable" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IImageLabelButton : IHaveButtonState, IDisposable, INotifyPropertyChanged
   {
      /// <summary>Gets or sets a value indicating whether [animate button].</summary>
      /// <value><c>true</c> if [animate button]; otherwise, <c>false</c>.</value>
      bool AnimateButton { get; set; }

      /// <summary>Gets or sets the button command.</summary>
      /// <value>The button command.</value>
      Command ButtonCommand { get; set; }

      /// <summary>Gets or sets the name of the button command binding.</summary>
      /// <value>The name of the button command binding.</value>
      string ButtonCommandBindingName { get; set; }

      /// <summary>Gets or sets the button command converter.</summary>
      /// <value>The button command converter.</value>
      IValueConverter ButtonCommandConverter { get; set; }

      /// <summary>Gets or sets the button command converter parameter.</summary>
      /// <value>The button command converter parameter.</value>
      object ButtonCommandConverterParameter { get; set; }

      /// <summary>Gets or sets the button command source.</summary>
      /// <value>The button command source.</value>
      object ButtonCommandSource { get; set; }

      /// <summary>Gets or sets the button command string format.</summary>
      /// <value>The button command string format.</value>
      string ButtonCommandStringFormat { get; set; }

      /// <summary>Gets or sets the button corner radius factor.</summary>
      /// <value>The button corner radius factor.</value>
      double? ButtonCornerRadiusFactor { get; set; }

      /// <summary>Gets or sets the button corner radius fixed.</summary>
      /// <value>The button corner radius fixed.</value>
      double? ButtonCornerRadiusFixed { get; set; }

      /// <summary>Gets or sets the button image.</summary>
      /// <value>The button image.</value>
      Image ButtonImage { get; set; }

      /// <summary>Gets or sets the button label.</summary>
      /// <value>The button label.</value>
      Label ButtonLabel { get; set; }

      bool CannotTap { get; set; }

      bool CanTapOnDisabled { get; set; }

      /// <summary>Gets or sets the current style.</summary>
      /// <value>The current style.</value>
      ImageLabelButtonStyle CurrentStyle { get; set; }

      double ImageHeight { get; set; }

      LayoutOptions ImageHorizontalAlign   { get; set; }
      LayoutOptions ImageHorizontalOptions { get; set; }

      /// <summary>Gets the image label button styles.</summary>
      /// <value>The image label button styles.</value>
      IList<ImageLabelButtonStyle> ImageLabelButtonStyles { get; }

      Thickness     ImageMargin          { get; set; }
      LayoutOptions ImageVerticalAlign   { get; set; }
      LayoutOptions ImageVerticalOptions { get; set; }

      double ImageWidth { get; set; }

      /// <summary>Gets or sets a value indicating whether [include haptic feedback].</summary>
      /// <value><c>true</c> if [include haptic feedback]; otherwise, <c>false</c>.</value>
      bool IncludeHapticFeedback { get; set; }

      /// <summary>Gets a value indicating whether this instance is selected.</summary>
      /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
      bool IsSelected { get; }

      /// <summary>Gets or sets the selection style.</summary>
      /// <value>The selection style.</value>
      ImageLabelButtonSelectionStyles SelectionStyle { get; set; }

      /// <summary>Gets a value indicating whether [update button text from style].</summary>
      /// <value><c>true</c> if [update button text from style]; otherwise, <c>false</c>.</value>
      bool UpdateButtonTextFromStyle { get; }

      /// <summary>Occurs when [button state changed].</summary>
      /// <summary>Occurs when [image label button pressed].</summary>
      event EventUtils.NoParamsDelegate ImageLabelButtonPressed;

      event EventUtils.GenericDelegate<bool> IsEnabledChanged;
   }

   /// <summary>
   ///    A button that can contain either an image and/or a label. Implements the
   ///    <see
   ///       cref="Com.MarcusTS.SharedForms.Views.Controls.ShapeView" />
   ///    Implements the
   ///    <see
   ///       cref="Com.MarcusTS.SharedForms.Views.Controls.IImageLabelButton" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ShapeView" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IImageLabelButton" />
   public abstract class ImageLabelButtonBase : ShapeView, IImageLabelButton
   {
      /// <summary>The default button radius factor</summary>
      public const float DEFAULT_BUTTON_RADIUS_FACTOR = 0.12f;

      /// <summary>The animate button property</summary>
      public static readonly BindableProperty AnimateButtonProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(AnimateButton),
            true
         );

      /// <summary>The button command binding name property</summary>
      public static readonly BindableProperty ButtonCommandBindingNameProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ButtonCommandBindingName),
            default(string)
            ,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.SetUpCompleteButtonCommandBinding();
            }
         );

      /// <summary>The button command converter parameter property</summary>
      public static readonly BindableProperty ButtonCommandConverterParameterProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ButtonCommandConverterParameter),
            default(object)
            ,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.SetUpCompleteButtonCommandBinding();
            }
         );

      /// <summary>The button command converter property</summary>
      public static readonly BindableProperty ButtonCommandConverterProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ButtonCommandConverter),
            default(IValueConverter)
            ,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.SetUpCompleteButtonCommandBinding();
            }
         );

      /// <summary>The button command property</summary>
      public static readonly BindableProperty ButtonCommandProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ButtonCommand),
            default(Command)
            ,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.RemoveButtonCommandEventListener();

               if (viewButton.ButtonCommand != null)
               {
                  viewButton.ButtonCommand.CanExecuteChanged += viewButton.HandleButtonCommandCanExecuteChanged;

                  // Force-fire the initial state
                  viewButton.ButtonCommand.ChangeCanExecute();

                  viewButton.OnButtonCommandCreated();
               }
            }
         );

      /// <summary>The button command converter property</summary>
      public static readonly BindableProperty ButtonCommandSourceProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ButtonCommandSource),
            default(IValueConverter)
            ,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.SetUpCompleteButtonCommandBinding();
            }
         );

      /// <summary>The button command string format property</summary>
      public static readonly BindableProperty ButtonCommandStringFormatProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ButtonCommandStringFormat),
            default(string)
            ,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.SetUpCompleteButtonCommandBinding();
            }
         );

      /// <summary>The corner radius factor property</summary>
      public static readonly BindableProperty ButtonCornerRadiusFactorProperty =
         CreateImageLabelButtonBaseBindableProperty
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
               viewButton.SetCornerRadius();
            }
         );

      /// <summary>The corner radius fixed property</summary>
      public static readonly BindableProperty ButtonCornerRadiusFixedProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ButtonCornerRadiusFixed),
            FormsConst.DEFAULT_SHAPE_VIEW_RADIUS,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.SetCornerRadius();
            }
         );

      /// <summary>The button image property</summary>
      public static readonly BindableProperty ButtonImageProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ButtonImage),
            default(Image)
            ,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               if (oldVal.IsNotNullOrDefault())
               {
                  if (viewButton._grid.Children.Contains(oldVal))
                  {
                     viewButton._grid.Children.Remove(oldVal);
                  }
               }

               // Already set before entering this method
               // viewButton._buttonImage = newVal;

               if (viewButton.ButtonImage != null)
               {
                  viewButton.ButtonImage.BindingContext = viewButton.BindingContext;

                  if (!viewButton._grid.Children.Contains(newVal))
                  {
                     viewButton._grid.AddAndSetRowsAndColumns(newVal, 0, 0);

                     if (viewButton.ButtonLabel.IsNotNullOrDefault())
                     {
                        // The label is always on top
                        viewButton._grid.RaiseChild(viewButton.ButtonLabel);
                     }
                  }
               }
            }
         );

      /// <summary>The button label property</summary>
      public static readonly BindableProperty ButtonLabelProperty =
         CreateImageLabelButtonBaseBindableProperty
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
               if (oldVal.IsNotNullOrDefault())
               {
                  if (viewButton._grid.Children.Contains(oldVal))
                  {
                     viewButton._grid.Children.Remove(oldVal);
                  }
               }

               // Already set before entering this method.
               // _buttonLabel = value;

               if (viewButton.ButtonLabel != null)
               {
                  viewButton.ButtonLabel.BindingContext   = viewButton.BindingContext;
                  viewButton.ButtonLabel.InputTransparent = true;

                  if (!viewButton._grid.Children.Contains(newVal))
                  {
                     viewButton._grid.AddAndSetRowsAndColumns(newVal, 0, 0);

                     // The label is always on top
                     viewButton._grid.RaiseChild(viewButton.ButtonLabel);
                  }

                  viewButton.SetLabelStyle();
               }
            }
         );

      public static readonly BindableProperty ButtonStateProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ButtonState),
            default(string)
            ,
            BindingMode.TwoWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               if (newVal == oldVal)
               {
                  // Nothing to do
                  return;
               }

               // Already done before entering this method.
               // _buttonState = value;

               viewButton.AfterButtonStateChanged();
               viewButton.BroadcastIfSelected();
               viewButton.ButtonStateChanged?.Invoke(newVal);
               viewButton.UpdateCurrentStyleFromButtonState(newVal);
            }
         );

      public static readonly BindableProperty CannotTapProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(CannotTap),
            default(bool)
         );

      public static readonly BindableProperty CanTapOnDisabledProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(CanTapOnDisabled),
            default(bool)
         );

      /// <summary>The current style property</summary>
      public static readonly BindableProperty CurrentStyleProperty =
         CreateImageLabelButtonBaseBindableProperty
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
               if (_currentStylePropChangedEntered)
               {
                  // CRITICAL 
                  viewButton.SetAllStyles();
                  return;
               }

               _currentStylePropChangedEntered = true;

               try
               {
                  newVal = viewButton.LastCheckBeforeAssigningStyle(newVal);

                  if (newVal.IsNotNullOrDefault() && oldVal.IsNotNullOrDefault() &&
                     oldVal.IsAnEqualReferenceTo(newVal))
                  {
                     // No change
                     return;
                  }

                  if (viewButton.CurrentStyle.IsNotAnEqualReferenceTo(newVal))
                  {
                     viewButton.CurrentStyle = newVal;
                  }   
                  
                  viewButton.ButtonState  = viewButton.CurrentStyle.InternalButtonState;
                  viewButton.UpdateButtonText();
                  viewButton.SetAllStyles();
               }
               finally
               {
                  _currentStylePropChangedEntered = false;
               }
            },
            (viewButton, newVal) =>
            {
               if (_currentStyleCoerceEntered)
               {
                  return newVal;
               }

               _currentStyleCoerceEntered = true;

               try
               {
                  if (newVal.IsNullOrDefault() && viewButton.CurrentStyle.IsNullOrDefault() && viewButton.ImageLabelButtonStyles.IsNotEmpty())
                  {
                     viewButton.CurrentStyle = viewButton.ImageLabelButtonStyles[0];
                  }

                  // ELSE
                  return newVal;
               }
               finally
               {
                  _currentStyleCoerceEntered = false;
               }
            }
         );

      /// <summary>The image height property</summary>
      public static readonly BindableProperty ImageHeightProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ImageHeight),
            default(double)
         );

      public static readonly BindableProperty ImageHorizontalAlignProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ImageHorizontalAlign),
            LayoutOptions.Center,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.CallRecreateImageSafely();
            }
         );

      public static readonly BindableProperty ImageHorizontalOptionsProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ImageHorizontalOptions),
            LayoutOptions.Center
         );

      public static readonly BindableProperty ImageMarginProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ImageMargin),
            default(Thickness),
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.CallRecreateImageSafely();
            }
         );

      public static readonly BindableProperty ImageVerticalAlignProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ImageVerticalAlign),
            LayoutOptions.Center,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.CallRecreateImageSafely();
            }
         );

      public static readonly BindableProperty ImageVerticalOptionsProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ImageVerticalOptions),
            LayoutOptions.Center
         );

      public static readonly BindableProperty ImageWidthProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(ImageWidth),
            default(double)
         );

      /// <summary>The include haptic feedback property</summary>
      public static readonly BindableProperty IncludeHapticFeedbackProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(IncludeHapticFeedback),
            true
         );

      /// <summary>The selection group property</summary>
      public static readonly BindableProperty SelectionGroupProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(SelectionGroup),
            default(int)
            ,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.BroadcastIfSelected();
            }
         );

      /// <summary>The selection style property</summary>
      public static readonly BindableProperty SelectionStyleProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(SelectionStyle),
            default(ImageLabelButtonSelectionStyles)
            ,
            BindingMode.OneWay,
            (
               viewButton,
               oldVal,
               newVal
            ) =>
            {
               viewButton.SetAllStyles();
            }
         );

      public static readonly BindableProperty UpdateButtonTextFromStyleProperty =
         CreateImageLabelButtonBaseBindableProperty
         (
            nameof(UpdateButtonTextFromStyle),
            default(bool)
         );

      private static bool _currentStyleCoerceEntered;

      private static bool _currentStylePropChangedEntered;

      /// <summary>The layout</summary>
      private readonly Grid _grid = FormsUtils.GetExpandingGrid();

      /// <summary>The tap gesture</summary>
      private readonly TapGestureRecognizer _tapGesture = new TapGestureRecognizer();

      private bool _isInstantiating;

      /// <summary>The last bounds</summary>
      private Rectangle _lastBounds;

      /// <summary>The tapped listener entered</summary>
      private volatile bool _tappedListenerEntered;

      /// <summary>Initializes a new instance of the <see cref="ImageLabelButtonBase" /> class.</summary>
      protected ImageLabelButtonBase()
      {
         CallStartup();
      }

      /// <summary>Gets a value indicating whether this instance is disabled.</summary>
      /// <value><c>true</c> if this instance is disabled; otherwise, <c>false</c>.</value>
      protected abstract bool IsDisabled { get; }

      /// <summary>Occurs when [button state changed].</summary>
      public event EventUtils.GenericDelegate<string> ButtonStateChanged;

      /// <summary>Occurs when [image label button pressed].</summary>
      public event EventUtils.NoParamsDelegate ImageLabelButtonPressed;

      public event EventUtils.GenericDelegate<bool> IsEnabledChanged;

      /// <summary>Gets or sets a value indicating whether [animate button].</summary>
      /// <value><c>true</c> if [animate button]; otherwise, <c>false</c>.</value>
      public bool AnimateButton
      {
         get => (bool) GetValue(AnimateButtonProperty);
         set => SetValue(AnimateButtonProperty, value);
      }

      /// <summary>Gets or sets the button command.</summary>
      /// <value>The button command.</value>
      public Command ButtonCommand
      {
         get => (Command) GetValue(ButtonCommandProperty);
         set => SetValue(ButtonCommandProperty, value);
      }

      /// <summary>Gets or sets the name of the button command binding.</summary>
      /// <value>The name of the button command binding.</value>
      public string ButtonCommandBindingName
      {
         get => (string) GetValue(ButtonCommandBindingNameProperty);
         set => SetValue(ButtonCommandBindingNameProperty, value);
      }

      /// <summary>Gets or sets the button command converter.</summary>
      /// <value>The button command converter.</value>
      public IValueConverter ButtonCommandConverter
      {
         get => (IValueConverter) GetValue(ButtonCommandConverterProperty);
         set => SetValue(ButtonCommandConverterProperty, value);
      }

      /// <summary>Gets or sets the button command converter parameter.</summary>
      /// <value>The button command converter parameter.</value>
      public object ButtonCommandConverterParameter
      {
         get => (IValueConverter) GetValue(ButtonCommandConverterParameterProperty);
         set => SetValue(ButtonCommandConverterParameterProperty, value);
      }

      /// <summary>Gets or sets the button command source.</summary>
      /// <value>The button command source.</value>
      public object ButtonCommandSource
      {
         get => (IValueConverter) GetValue(ButtonCommandSourceProperty);
         set => SetValue(ButtonCommandSourceProperty, value);
      }

      /// <summary>Gets or sets the button command string format.</summary>
      /// <value>The button command string format.</value>
      public string ButtonCommandStringFormat
      {
         get => (string) GetValue(ButtonCommandStringFormatProperty);
         set => SetValue(ButtonCommandStringFormatProperty, value);
      }

      /// <summary>Gets or sets the button corner radius factor.</summary>
      /// <value>The button corner radius factor.</value>
      public double? ButtonCornerRadiusFactor
      {
         get => (double?) GetValue(ButtonCornerRadiusFactorProperty);
         set => SetValue(ButtonCornerRadiusFactorProperty, value);
      }

      /// <summary>Gets or sets the button corner radius fixed.</summary>
      /// <value>The button corner radius fixed.</value>
      public double? ButtonCornerRadiusFixed
      {
         get => (double?) GetValue(ButtonCornerRadiusFixedProperty);
         set => SetValue(ButtonCornerRadiusFixedProperty, value);
      }

      /// <summary>Gets or sets the button image.</summary>
      /// <value>The button image.</value>
      public Image ButtonImage
      {
         get => (Image) GetValue(ButtonImageProperty);
         set => SetValue(ButtonImageProperty, value);
      }

      /// <summary>Gets or sets the button label.</summary>
      /// <value>The button label.</value>
      public Label ButtonLabel
      {
         get => (Label) GetValue(ButtonLabelProperty);
         set => SetValue(ButtonLabelProperty, value);
      }

      /// <summary>Gets or sets the state of the button.</summary>
      /// <value>The state of the button.</value>
      public string ButtonState
      {
         get => (string) GetValue(ButtonStateProperty);
         set => SetValue(ButtonStateProperty, value);
      }

      public bool CannotTap
      {
         get => (bool) GetValue(CannotTapProperty);
         set => SetValue(CannotTapProperty, value);
      }

      public bool CanTapOnDisabled
      {
         get => (bool) GetValue(CanTapOnDisabledProperty);
         set => SetValue(CanTapOnDisabledProperty, value);
      }

      /// <summary>Gets or sets the current style.</summary>
      /// <value>The current style.</value>
      public ImageLabelButtonStyle CurrentStyle
      {
         get => (ImageLabelButtonStyle) GetValue(CurrentStyleProperty);
         set => SetValue(CurrentStyleProperty, value);
      }

      public double ImageHeight
      {
         get => (double) GetValue(ImageHeightProperty);
         set => SetValue(ImageHeightProperty, value);
      }

      public LayoutOptions ImageHorizontalAlign
      {
         get => (LayoutOptions) GetValue(ImageHorizontalAlignProperty);
         set => SetValue(ImageHorizontalAlignProperty, value);
      }

      public LayoutOptions ImageHorizontalOptions
      {
         get => (LayoutOptions) GetValue(ImageHorizontalOptionsProperty);
         set => SetValue(ImageHorizontalOptionsProperty, value);
      }

      /// <summary>Gets the image label button styles.</summary>
      /// <value>The image label button styles.</value>
      public abstract IList<ImageLabelButtonStyle> ImageLabelButtonStyles { get; }

      public Thickness ImageMargin
      {
         get => (Thickness) GetValue(ImageMarginProperty);
         set => SetValue(ImageMarginProperty, value);
      }

      public LayoutOptions ImageVerticalAlign
      {
         get => (LayoutOptions) GetValue(ImageVerticalAlignProperty);
         set => SetValue(ImageVerticalAlignProperty, value);
      }

      public LayoutOptions ImageVerticalOptions
      {
         get => (LayoutOptions) GetValue(ImageVerticalOptionsProperty);
         set => SetValue(ImageVerticalOptionsProperty, value);
      }

      public double ImageWidth
      {
         get => (double) GetValue(ImageWidthProperty);
         set => SetValue(ImageWidthProperty, value);
      }

      /// <summary>Gets or sets a value indicating whether [include haptic feedback].</summary>
      /// <value><c>true</c> if [include haptic feedback]; otherwise, <c>false</c>.</value>
      public bool IncludeHapticFeedback
      {
         get => (bool) GetValue(IncludeHapticFeedbackProperty);
         set => SetValue(IncludeHapticFeedbackProperty, value);
      }

      /// <summary>Gets a value indicating whether this instance is selected.</summary>
      /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
      public abstract bool IsSelected { get; }

      /// <summary>Gets or sets the selection group.</summary>
      /// <value>The selection group.</value>
      public int SelectionGroup
      {
         get => (int) GetValue(SelectionGroupProperty);
         set => SetValue(SelectionGroupProperty, value);
      }

      /// <summary>Gets or sets the selection style.</summary>
      /// <value>The selection style.</value>
      public ImageLabelButtonSelectionStyles SelectionStyle
      {
         get => (ImageLabelButtonSelectionStyles) GetValue(SelectionStyleProperty);
         set => SetValue(SelectionStyleProperty, value);
      }

      /// <summary>Gets a value indicating whether [update button text from style].</summary>
      /// <value><c>true</c> if [update button text from style]; otherwise, <c>false</c>.</value>
      public bool UpdateButtonTextFromStyle
      {
         get => (bool) GetValue(UpdateButtonTextFromStyleProperty);
         set => SetValue(UpdateButtonTextFromStyleProperty, value);
      }

      /// <summary>
      ///    Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         ReleaseUnmanagedResources();
         GC.SuppressFinalize(this);
      }

      /// <summary>Occurs when [i am selected static].</summary>
      protected static event EventUtils.GenericDelegate<IImageLabelButton> IAmSelectedStatic;

      /// <summary>
      ///    Creates the button style.
      /// </summary>
      /// <param name="backColor">Color of the back.</param>
      /// <param name="borderThickness">Width of the border.</param>
      /// <param name="borderColor">Color of the border.</param>
      /// <param name="cornerRadius"></param>
      /// <returns>Style.</returns>
      public static Style CreateButtonStyle
      (
         Color?  backColor       = null,
         float?  borderThickness = null,
         Color?  borderColor     = default,
         double? cornerRadius    = null
      )
      {
         var newStyle = new Style(typeof(ImageLabelButtonBase));
         newStyle.SetShapeViewStyle(backColor, cornerRadius, borderColor, borderThickness);
         return newStyle;
      }

      /// <summary>Creates the toggle image label button bindable property.</summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <param name="coerceValueDelegate"></param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateImageLabelButtonBaseBindableProperty<PropertyTypeT>
      (
         string                                                     localPropName,
         PropertyTypeT                                              defaultVal          = default,
         BindingMode                                                bindingMode         = BindingMode.OneWay,
         Action<ImageLabelButtonBase, PropertyTypeT, PropertyTypeT> callbackAction      = null,
         Func<ImageLabelButtonBase, PropertyTypeT, PropertyTypeT>   coerceValueDelegate = default
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction,
            coerceValueDelegate);
      }

      /// <summary>Creates the label style.</summary>
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
            newStyle.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = textColor });
         }

         // The label is always transparent
         newStyle.Setters.Add(new Setter { Property = BackgroundColorProperty, Value = Color.Transparent });

         if (fontSize.HasValue)
         {
            newStyle.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = fontSize });
         }

         if (fontAttributes.HasValue)
         {
            newStyle.Setters.Add(new Setter { Property = Label.FontAttributesProperty, Value = fontAttributes });
         }

         return newStyle;
      }

      /// <summary>Releases unmanaged and - optionally - managed resources.</summary>
      /// <param name="disposing">
      ///    <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
      /// </param>
      public void Dispose(bool disposing)
      {
         ReleaseUnmanagedResources();
         if (disposing)
         {
         }
      }

      protected virtual void AfterButtonStateChanged()
      {
      }

      /// <summary>Afters the content set.</summary>
      protected virtual void AfterContentSet()
      {
      }

      /// <summary>Called when [button state changed].</summary>
      protected virtual void BeforeButtonStateChangedFromStyle(ref string newButtonState)
      {
      }

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

      /// <summary>Calls the recreate image safely.</summary>
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

      /// <summary>Deselects this instance.</summary>
      protected abstract void Deselect();

      /// <summary>Handles the tap gesture tapped.</summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      /// <remarks>TODO Cannot avoid async void -- event handler.</remarks>
      protected async void HandleTapGestureTapped
      (
         object    sender,
         EventArgs e
      )
      {
         if (_tappedListenerEntered ||
            CannotTap ||
            IsDisabled && !CanTapOnDisabled ||
            ImageLabelButtonStyles.IsEmpty() ||
            CurrentStyle.IsNullOrDefault() ||
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

      protected virtual ImageLabelButtonStyle LastCheckBeforeAssigningStyle(ImageLabelButtonStyle value)
      {
         return value;
      }

      /// <summary>Called when [button command created].</summary>
      protected virtual void OnButtonCommandCreated()
      {
      }

      protected virtual void OnIsEnabledChanged()
      {
      }

      /// <summary>Sets all styles.</summary>
      protected void SetAllStyles()
      {
         SetButtonStyle();
         SetLabelStyle();
         CallRecreateImageSafely();
      }

      /// <summary>Sets the button style.</summary>
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

      /// <summary>Sets the current style by button text.</summary>
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

      /// <summary>Sets the label style.</summary>
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

      /// <summary>Starts up.</summary>
      protected virtual void StartUp()
      {
         _isInstantiating = true;

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

         _grid.AddStarColumn();
         _grid.AddStarRow();

         Content = _grid;

         _isInstantiating = false;
      }

      /// <summary>Converts toggle current style.</summary>
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

      /// <summary>Broadcasts if selected.</summary>
      private void BroadcastIfSelected()
      {
         if (SelectionGroup > 0 && IsSelected)
         {
            // Raise a static event to notify others in this selection group that they should be *deselected*
            IAmSelectedStatic?.Invoke(this);
         }
      }

      /// <summary>Calls the startup.</summary>
      private void CallStartup()
      {
         StartUp();
      }

      /// <summary>Currents the style index found.</summary>
      /// <param name="styleIdx">Index of the style.</param>
      /// <returns><c>true</c> if the button state was found, otherwise <c>false</c> .</returns>
      private bool CurrentStyleIndexFound(out int styleIdx)
      {
         return ButtonStateIndexFound(CurrentStyle?.InternalButtonState, out styleIdx);
      }

      /// <summary>Handles the button command can execute changed.</summary>
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

      /// <summary>Handles the static selection changes.</summary>
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

      /// <summary>Recreates the image.</summary>
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
            FormsUtils.GetImage(
               imageFileName, 
               ImageWidth, 
               ImageHeight,
               Aspect.AspectFit, 
               ImageHorizontalAlign, 
               ImageVerticalAlign, 
               ImageMargin,  
               CurrentStyle.GetImageFromResource,
               CurrentStyle.ImageResourceClassType);
      }

      /// <summary>Releases the unmanaged resources.</summary>
      private void ReleaseUnmanagedResources()
      {
         // Global static, so remove the handler
         IAmSelectedStatic -= HandleStaticSelectionChanges;

         _tapGesture.Tapped -= HandleTapGestureTapped;

         RemoveButtonCommandEventListener();
      }

      /// <summary>Removes the button command event listener.</summary>
      private void RemoveButtonCommandEventListener()
      {
         if (ButtonCommand != null)
         {
            ButtonCommand.CanExecuteChanged -= HandleButtonCommandCanExecuteChanged;
         }
      }

      /// <summary>Sets the corner radius.</summary>
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

      /// <summary>Sets up complete button command binding.</summary>
      private void SetUpCompleteButtonCommandBinding()
      {
         if (ButtonCommandBindingName.IsEmpty())
         {
            RemoveBinding(ButtonCommandProperty);
         }
         else
         {
            // NOTE Extremely reactive code below; see ValidatableViewBase.CreateBindings
            if (ButtonCommandConverter.IsNotNullOrDefault())
            {
               if (ButtonCommandSource.IsNotNullOrDefault())
               {
                  this.SetUpBinding(ButtonCommandProperty, ButtonCommandBindingName, converter: ButtonCommandConverter,
                     converterParameter: ButtonCommandConverterParameter, source: ButtonCommandSource);
               }
               else
               {
                  this.SetUpBinding(ButtonCommandProperty, ButtonCommandBindingName, converter: ButtonCommandConverter,
                     converterParameter: ButtonCommandConverterParameter);
               }
            }
            else
            {
               if (ButtonCommandSource.IsNotNullOrDefault())
               {
                  this.SetUpBinding(ButtonCommandProperty, ButtonCommandBindingName, source: ButtonCommandSource);
               }
               else
               {
                  this.SetUpBinding(ButtonCommandProperty, ButtonCommandBindingName);
               }
            }
         }
      }

      /// <summary>Updates the button text.</summary>
      private void UpdateButtonText()
      {
         if (!UpdateButtonTextFromStyle || ButtonLabel == null || CurrentStyle.IsNullOrDefault())
         {
            return;
         }

         ButtonLabel.Text = CurrentStyle.ButtonText;
      }

      /// <summary>Finalizes an instance of the <see cref="ImageLabelButtonBase" /> class.</summary>
      ~ImageLabelButtonBase()
      {
         ReleaseUnmanagedResources();
      }
   }
}