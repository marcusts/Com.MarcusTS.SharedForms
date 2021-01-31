
// *********************************************************************** Assembly : Com.MarcusTS.SharedForms Author :
// steph Created : 08-04-2019
//
// Last Modified By : steph Last Modified On : 08-08-2019
// ***********************************************************************
// <copyright file="ValidatableViewBase.cs" company="Marcus Technical Services, Inc.">
//     Copyright @2019 Marcus Technical Services, Inc.
// </copyright>
// <summary></summary>

// MIT License

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
// Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

#define SKIP_RESTORING_PLACEHOLDER
// #define USE_BACK_COLOR

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using System;
   using System.ComponentModel;
   using System.Threading.Tasks;
   using Common.Interfaces;
   using Common.Notifications;
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   ///    Interface IValidatableView Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IValidatableView : INotifyPropertyChanged
   {
      /// <summary>Gets the border view.</summary>
      /// <value>The border view.</value>
      ShapeView BorderView { get; }

      string CurrentInstructions { get; set; }

      Label InstructionsLabel { get; }

      /// <summary>Gets or sets the invalid border view style.</summary>
      /// <value>The invalid border view style.</value>
      Style InvalidBorderViewStyle { get; set; }

      /// <summary>Gets or sets the invalid instructions style.</summary>
      /// <value>The invalid instructions style.</value>
      Style InvalidInstructionsStyle { get; set; }

      /// <summary>Gets or sets the invalid placeholder style.</summary>
      /// <value>The invalid placeholder style.</value>
      Style InvalidPlaceholderStyle { get; set; }

      Label PlaceholderLabel { get; }

      /// <summary>Gets or sets the valid border view style.</summary>
      /// <value>The valid border view style.</value>
      Style ValidBorderViewStyle { get; set; }

      /// <summary>Gets or sets the valid instructions style.</summary>
      /// <value>The valid instructions style.</value>
      Style ValidInstructionsStyle { get; set; }

      /// <summary>Gets or sets the valid placeholder style.</summary>
      /// <value>The valid placeholder style.</value>
      Style ValidPlaceholderStyle { get; set; }

      void CallRevalidate();

      int SetTabIndexes(int incomingTabIndex);
   }

   /// <summary>
   ///    A UI element that includes an Entry surrounded by a border. Implements the <see cref="Xamarin.Forms.Grid" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableView" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Grid" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableView" />
   public abstract class ValidatableViewBase : Grid, IValidatableView
   {
      /// <summary>The default border view height</summary>
      public const double BORDER_VIEW_HEIGHT = 40;

      /// <summary>The default instructions height</summary>
      public const double INSTRUCTIONS_HEIGHT = 25;

      /// <summary>The default placeholder height</summary>
      public const double PLACEHOLDER_HEIGHT = 12;

      /// <summary>The default border view border width</summary>
      private const float BORDER_VIEW_BORDER_WIDTH = 1;

      /// <summary>The default border view radius</summary>
      private const float BORDER_VIEW_RADIUS = 3;

      /// <summary>The default grid single padding</summary>
      private const double GRID_SINGLE_PADDING = 6;

      /// <summary>The placeholder inset</summary>
      private const double PLACEHOLDER_INSET = 8;

      private const double PLACEHOLDER_LABEL_SIDE_MARGIN = 6;

      /// <summary>The default shape view single padding</summary>
      private const double SHAPE_VIEW_SINGLE_PADDING = 6;

      /// <summary>The invalid border view style property</summary>
      public static readonly BindableProperty InvalidBorderViewStyleProperty =
         CreateValidatableViewBindableProperty
         (
            nameof(InvalidBorderViewStyle),
            default(Style),
            BindingMode.OneWay,
            (
               view,
               oldVal,
               newVal
            ) =>
            {
               view.InvalidBorderViewStyle = newVal;
            }
         );

      /// <summary>The invalid instructions style property</summary>
      public static readonly BindableProperty InvalidInstructionsStyleProperty =
         CreateValidatableViewBindableProperty
         (
            nameof(InvalidInstructionsStyle),
            default(Style),
            BindingMode.OneWay,
            (
               view,
               oldVal,
               newVal
            ) =>
            {
               view.InvalidInstructionsStyle = newVal;
            }
         );

      /// <summary>The invalid placeholder style property</summary>
      public static readonly BindableProperty InvalidPlaceholderStyleProperty =
         CreateValidatableViewBindableProperty
         (
            nameof(InvalidPlaceholderStyle),
            default(Style),
            BindingMode.OneWay,
            (
               view,
               oldVal,
               newVal
            ) =>
            {
               view.InvalidPlaceholderStyle = newVal;
            }
         );

      /// <summary>The valid border view style property</summary>
      public static readonly BindableProperty ValidBorderViewStyleProperty =
         CreateValidatableViewBindableProperty
         (
            nameof(ValidBorderViewStyle),
            default(Style),
            BindingMode.OneWay,
            (
               view,
               oldVal,
               newVal
            ) =>
            {
               view.ValidBorderViewStyle = newVal;
            }
         );

      /// <summary>The valid instructions style property</summary>
      public static readonly BindableProperty ValidInstructionsStyleProperty =
         CreateValidatableViewBindableProperty
         (
            nameof(ValidInstructionsStyle),
            default(Style),
            BindingMode.OneWay,
            (
               view,
               oldVal,
               newVal
            ) =>
            {
               view.ValidInstructionsStyle = newVal;
            }
         );

      /// <summary>The valid placeholder style property</summary>
      public static readonly BindableProperty ValidPlaceholderStyleProperty =
         CreateValidatableViewBindableProperty
         (
            nameof(ValidPlaceholderStyle),
            default(Style),
            BindingMode.OneWay,
            (
               view,
               oldVal,
               newVal
            ) =>
            {
               view.ValidPlaceholderStyle = newVal;
            }
         );

      private const double VERTICAL_SLOP = 4;

      protected static readonly Thickness DEFAULT_BORDER_VIEW_PADDING = new Thickness(8, 0, 4, 0);

      /// <summary>The default border view border color</summary>
      private static readonly Color DEFAULT_BORDER_VIEW_BORDER_COLOR = Color.Black;

      /// <summary>The default grid padding</summary>
      private static readonly Thickness DEFAULT_GRID_PADDING =
         new Thickness(GRID_SINGLE_PADDING);

      /// <summary>The default instructions label font size</summary>
      private static readonly double DEFAULT_INSTRUCTIONS_LABEL_FONT_SIZE =
         Device.GetNamedSize(NamedSize.Micro, typeof(Label));

      /// <summary>The default placeholder back color</summary>
      private static readonly Color DEFAULT_PLACEHOLDER_BACK_COLOR = Color.White;

      /// <summary>The default placeholder label font size</summary>
      private static readonly double DEFAULT_PLACEHOLDER_LABEL_FONT_SIZE =
         Device.GetNamedSize(NamedSize.Micro, typeof(Label)) * 1.15;

      /// <summary>The default placeholder text color</summary>
      private static readonly Color DEFAULT_PLACEHOLDER_TEXT_COLOR = Color.DimGray;

      ///// <summary>The default shape view border color</summary>
      //private static readonly Color DEFAULT_SHAPE_VIEW_BORDER_COLOR = Color.Black;

      /// <summary>The default shape view padding</summary>
      private static readonly Thickness DEFAULT_SHAPE_VIEW_PADDING =
         new Thickness(SHAPE_VIEW_SINGLE_PADDING);

      /// <summary>The invalid font attributes</summary>
      private static readonly FontAttributes INVALID_FONT_ATTRIBUTES = FontAttributes.Bold | FontAttributes.Italic;

      /// <summary>The invalid text color</summary>
      private static readonly Color INVALID_TEXT_COLOR = Color.Red;

      /// <summary>The valid font attributes</summary>
      private static readonly FontAttributes VALID_FONT_ATTRIBUTES = FontAttributes.None;

      private static readonly Color            VALID_TEXT_COLOR = Color.Black;
      private readonly        BindableProperty _bindableProperty;
      private readonly        BindingMode      _bindingMode;
      private readonly        double?          _borderViewHeight;
      private readonly        IValueConverter  _converter;
      private readonly        object           _converterParameter;
      private readonly        string           _instructions;
      private readonly        double?          _instructionsHeight;
      private readonly        string           _placeholder;
      private readonly        double           _placeholderHeight;
      private readonly        bool             _showInstructionsOrValidations;
      private readonly        bool             _showValidationErrorsAsInstructions;
      private readonly        string           _stringFormat;
      private readonly        string           _viewModelPropertyName;
      private                 Style            _invalidBorderViewStyle;
      private                 Style            _invalidInstructionsStyle;
      private                 Style            _invalidPlaceholderStyle;
      private                 Rectangle        _lastBorderViewBounds;
      private                 Rectangle        _lastEditableViewContainerBounds;
      private                 Grid             _placeholderGrid;
      private                 bool             _placeholderLabelHasBeenShown;
      private                 Style            _validBorderViewStyle;
      private                 Style            _validInstructionsStyle;
      private                 Style            _validPlaceholderStyle;
      private readonly        ICanBeValid      _validator;
      private                 bool             _viewsCreated;
      private                 AbsoluteLayout   _canvas;
      private                 Style            _lastValidBorderViewStyle;
      private                 Style            _lastValidPlaceholderStyle;

      protected ValidatableViewBase
      (
         BindableProperty bindableProperty                   = null,
         double?          borderViewHeight                   = BORDER_VIEW_HEIGHT,
         BindingMode      bindingMode                        = BindingMode.TwoWay,
         IValueConverter  converter                          = null,
         object           converterParameter                 = null,
         string           fontFamilyOverride                 = "",
         string           instructions                       = "",
         double?          instructionsHeight                 = INSTRUCTIONS_HEIGHT,
         string           placeholder                        = "",
         double?          placeholderHeight                  = PLACEHOLDER_HEIGHT,
         bool             showInstructionsOrValidations      = false,
         bool             showValidationErrorsAsInstructions = true,
         string           stringFormat                       = null,
         ICanBeValid      validator                          = null,
         string           viewModelPropertyName              = ""
      )
      {
         FontFamilyOverride                  = fontFamilyOverride;
         _bindableProperty                   = bindableProperty;
         _bindingMode                        = bindingMode;
         _borderViewHeight                   = borderViewHeight;
         _converter                          = converter;
         _converterParameter                 = converterParameter;
         _instructions                       = instructions;
         _instructionsHeight                 = instructionsHeight;
         _placeholder                        = placeholder;
         _placeholderHeight                  = placeholderHeight.GetValueOrDefault();
         _showInstructionsOrValidations      = showInstructionsOrValidations;
         _showValidationErrorsAsInstructions = showValidationErrorsAsInstructions;
         _stringFormat                       = stringFormat;
         _validator                          = validator;
         _viewModelPropertyName              = viewModelPropertyName;
         HorizontalOptions = LayoutOptions.FillAndExpand;
         VerticalOptions   = LayoutOptions.FillAndExpand;
         BackgroundColor   = Color.Transparent;
         ColumnSpacing     = 0;
         RowSpacing        = 0;
      }

      // WARNING: Async void
      protected override async void OnBindingContextChanged()
      {
         base.OnBindingContextChanged();
         
         EditableView.BindingContext = BindingContext;
         
         await ResetPlaceholderPosition().WithoutChangingContext();
      }

      protected abstract bool DerivedViewIsFocused { get; }

      protected abstract View EditableView { get; }

      protected abstract View EditableViewContainer { get; }

      /// <summary>The font family override</summary>
      protected string FontFamilyOverride { get; }

      protected abstract bool        UserHasEnteredValidContent { get; }

      /// <summary>Gets the border view.</summary>
      /// <value>The border view.</value>
      public ShapeView BorderView { get; private set; }

      // ReSharper disable once UnusedAutoPropertyAccessor.Local
      /// <summary>Gets or sets the current instructions.</summary>
      /// <value>The current instructions.</value>
      public string CurrentInstructions { get; set; }

      /// <summary>Gets the instructions label.</summary>
      /// <value>The instructions label.</value>
      public Label InstructionsLabel { get; private set; }

      /// <summary>Gets or sets the invalid border view style.</summary>
      /// <value>The invalid border view style.</value>
      public Style InvalidBorderViewStyle
      {
         get => _invalidBorderViewStyle;
         set
         {
            _invalidBorderViewStyle = value;
            ResetStyles();
         }
      }

      /// <summary>Gets or sets the invalid instructions style.</summary>
      /// <value>The invalid instructions style.</value>
      public Style InvalidInstructionsStyle
      {
         get => _invalidInstructionsStyle;
         set
         {
            _invalidInstructionsStyle = value;
            ResetStyles();
         }
      }

      /// <summary>Gets or sets the invalid placeholder style.</summary>
      /// <value>The invalid placeholder style.</value>
      public Style InvalidPlaceholderStyle
      {
         get => _invalidPlaceholderStyle;
         set
         {
            _invalidPlaceholderStyle = value;
            ResetStyles();
         }
      }

      /// <summary>Gets the placeholder label.</summary>
      /// <value>The placeholder label.</value>
      public Label PlaceholderLabel { get; private set; }

      /// <summary>Gets or sets the valid border view style.</summary>
      /// <value>The valid border view style.</value>
      public Style ValidBorderViewStyle
      {
         get => _validBorderViewStyle;
         set
         {
            _validBorderViewStyle = value;
            ResetStyles();
         }
      }

      /// <summary>Gets or sets the valid instructions style.</summary>
      /// <value>The valid instructions style.</value>
      public Style ValidInstructionsStyle
      {
         get => _validInstructionsStyle;
         set
         {
            _validInstructionsStyle = value;
            ResetStyles();
         }
      }

      /// <summary>Gets or sets the valid placeholder style.</summary>
      /// <value>The valid placeholder style.</value>
      public Style ValidPlaceholderStyle
      {
         get => _validPlaceholderStyle;
         set
         {
            _validPlaceholderStyle = value;
            ResetStyles();
         }
      }

      /// <summary>Creates the validatable entry bindable property.</summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateValidatableViewBindableProperty<PropertyTypeT>
      (
         string                                                    localPropName,
         PropertyTypeT                                             defaultVal     = default,
         BindingMode                                               bindingMode    = BindingMode.OneWay,
         Action<ValidatableViewBase, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>Considers the lowering placeholder.</summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="FocusEventArgs" /> instance containing the event data.</param>
      /// <remarks>TODO Cannot avoid async void -- event handler.</remarks>
      protected async void ConsiderLoweringPlaceholder(object sender, FocusEventArgs e)
      {
         await ResetPlaceholderPosition().WithoutChangingContext();
      }

      protected void CreateBindings()
      {
         // This *must* have the current binding context.

         if (_viewModelPropertyName.IsEmpty() || _bindableProperty.IsNullOrDefault())
         {
            return;
         }

         if (_converter.IsNotNullOrDefault())
         {
            if (_stringFormat.IsNotEmpty())
            {
               EditableView.SetUpBinding(_bindableProperty, _viewModelPropertyName, _bindingMode, _converter,
                                         _converterParameter, _stringFormat);
            }
            else
            {
               EditableView.SetUpBinding(_bindableProperty, _viewModelPropertyName, _bindingMode, _converter,
                                         _converterParameter);
            }
         }
         else if (_stringFormat.IsNotEmpty())
         {
            EditableView.SetUpBinding(_bindableProperty, _viewModelPropertyName, _bindingMode,
                                      stringFormat: _stringFormat);
         }
         else
         {
            EditableView.SetUpBinding(_bindableProperty, _viewModelPropertyName, _bindingMode);
         }
      }

      /// <summary>
      /// Creates the views.
      /// </summary>
      protected virtual void CreateViews()
      {
         // WARNING: BorderView must be created before the EditableViewContainer is referenced.

         BorderView = FormsUtils.GetShapeView();
         BorderView.HorizontalOptions = LayoutOptions.FillAndExpand;
         BorderView.VerticalOptions = LayoutOptions.CenterAndExpand;

         var borderViewHeight = _borderViewHeight ?? BORDER_VIEW_HEIGHT;

         BorderView.HeightRequest = borderViewHeight;
         BorderView.CornerRadius = borderViewHeight * FormsConst.DEFAULT_CORNER_RADIUS_FACTOR;
         BorderView.Border = FormsUtils.CreateShapeViewBorder(DEFAULT_BORDER_VIEW_BORDER_COLOR, BORDER_VIEW_BORDER_WIDTH);

         // Allow for the border view's height and the vertical padding
         var totalHeight = BorderView.HeightRequest + GRID_SINGLE_PADDING * 2;

         BorderView.Content = EditableViewContainer;
         BorderView.Margin = new Thickness(0, 5, 0, 0);

         BorderView.PropertyChanged +=
            async (sender, args) =>
            {
               if (BorderView.Bounds.AreValidAndHaveChanged(args.PropertyName, _lastBorderViewBounds))
               {
                  _lastBorderViewBounds = BorderView.Bounds;
                  await ResetPlaceholderPosition().WithoutChangingContext();
               }
            };

         // Row 0 holds the shape view and entry
         this.AddFixedRow(_borderViewHeight ?? BORDER_VIEW_HEIGHT);
         Children.Add(BorderView);
         SetRow(BorderView, 0);

         // Instructions might not shw up until run-time
         if (_showInstructionsOrValidations) // && _instructions.IsNotEmpty())
         {
            InstructionsLabel =
               FormsUtils.GetSimpleLabel(_instructions, fontFamilyOverride: FontFamilyOverride, textColor: Color.Red,
                                         fontAttributes: FontAttributes.Italic | FontAttributes.Bold,
                                         fontSize: NamedSize.Micro.AdjustForOsAndDevice());
            InstructionsLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
            InstructionsLabel.VerticalOptions = LayoutOptions.Start;
            InstructionsLabel.HeightRequest = _instructionsHeight ?? INSTRUCTIONS_HEIGHT;

            var smallMargin = GRID_SINGLE_PADDING / 2;

            InstructionsLabel.Margin = new Thickness(0, smallMargin);

            // Allow for the margins
            totalHeight += InstructionsLabel.HeightRequest + smallMargin * 2;

            this.AddAutoRow();
            Children.Add(InstructionsLabel);
            SetRow(InstructionsLabel, 1);

            // The current instructions always show in this label; to change the content, change the property.
            InstructionsLabel.SetUpBinding(Label.TextProperty, nameof(CurrentInstructions), source: this);
         }

         if (_placeholder.IsNotEmpty())
         {
            PlaceholderLabel = FormsUtils.GetSimpleLabel(_placeholder, fontFamilyOverride: FontFamilyOverride);

#if FADE_PLACEHOLDER
            // Hide initially
            PlaceholderLabel.Opacity       = FormsConst.NOT_VISIBLE_OPACITY;
#endif

            PlaceholderLabel.HeightRequest = _placeholderHeight;
            PlaceholderLabel.Text = _placeholder;

            _placeholderGrid = FormsUtils.GetExpandingGrid();
            _placeholderGrid.BackgroundColor = DEFAULT_PLACEHOLDER_BACK_COLOR;
            PlaceholderLabel.BackgroundColor = DEFAULT_PLACEHOLDER_BACK_COLOR;
            _placeholderGrid.AddFixedColumn(PLACEHOLDER_LABEL_SIDE_MARGIN);
            _placeholderGrid.AddAutoColumn();
            _placeholderGrid.Children.Add(PlaceholderLabel);
            SetColumn(PlaceholderLabel, 1);
            _placeholderGrid.AddFixedColumn(PLACEHOLDER_LABEL_SIDE_MARGIN);

            // Absolute layout for _canvas position -- this overlays the other rows
            _canvas = FormsUtils.GetExpandingAbsoluteLayout();

#if USE_BACK_COLOR
            _canvas.BackgroundColor = Color.Yellow;
#endif

            // CRITICAL -- the overla wll block input !!!
            _canvas.InputTransparent = true;

            // The exact position will be set once the border view gets its bounds (and whenever those bonds change)
            _canvas.Children.Add(_placeholderGrid);

            Children.Add(_canvas);
            SetRow(_canvas, 0);

            // SetRowSpan(_canvas, 2);

            EditableView.Focused += ReportGlobalFocusAndRaisePlaceholder;
            EditableView.Unfocused += ConsiderLoweringPlaceholder;
         }

         // Validate immediately if possible
         if (_validator.IsNotNullOrDefault() && _validator is Behavior validatorAsBehavior)
         {
            if (EditableView is Entry)
            {
               // The entry is well-adapted to a hosting a behavior.
               if (!EditableView.Behaviors.Contains(validatorAsBehavior))
               {
                  EditableView.Behaviors.Add(validatorAsBehavior);
               }
            }
            else
            {
               // The view is too generalized to host a behavior.
               // The container has other information needed for validation.
               if (!Behaviors.Contains(validatorAsBehavior))
               {
                  Behaviors.Add(validatorAsBehavior);
               }
            }

            _validator.IsValidChanged += HandleIsValidChanged;
            _validator.Revalidate();
         }

         HeightRequest = totalHeight;

         _invalidBorderViewStyle = CreateDefaultInvalidBorderViewStyle();
         _invalidInstructionsStyle = CreateDefaultInvalidInstructionsStyle();
         _invalidPlaceholderStyle = CreateDefaultInvalidPlaceholderStyle();
         _validPlaceholderStyle = CreateDefaultValidPlaceholderStyle();
         _validBorderViewStyle = CreateDefaultBorderViewStyle();
         _validInstructionsStyle = CreateDefaultValidInstructionsStyle();

         EditableViewContainer.PropertyChanged +=
            async (sender, args) =>
            {
               if (EditableViewContainer.Bounds.AreValidAndHaveChanged(args.PropertyName,
                                                                       _lastEditableViewContainerBounds))
               {
                  _lastEditableViewContainerBounds = EditableViewContainer.Bounds;
                  await ResetPlaceholderPosition().WithoutChangingContext();
               }
            };
      }

      /// <summary>Raises the placeholder.</summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="FocusEventArgs" /> instance containing the event data.</param>
      /// <remarks>TODO Cannot avoid async void -- event handler.</remarks>
      protected async void ReportGlobalFocusAndRaisePlaceholder(object sender, FocusEventArgs e)
      {
         FormsMessengerUtils.Send(new ViewIsBeingEditedMessage { Payload = true });

         await ResetPlaceholderPosition().WithoutChangingContext();
      }

      /// <summary>Resets the styles.</summary>
      protected virtual void ResetStyles()
      {
         // If the validator is null, we are valid by default.
         HandleIsValidChanged(_validator.IsNullOrDefault() || _validator.IsValid.GetValueOrDefault());
      }

      protected void CallCreateViews()
      {
         // if (EditableViewContainer.IsNullOrDefault() || EditableView.IsNullOrDefault() || _viewsCreated)
         if (_viewsCreated)
         {
            return;
         }

         CreateViews();
         CreateBindings();
         ResetStyles();

         _viewsCreated = true;
      }

      /// <summary>Creates the default border view style.</summary>
      /// <returns>Style.</returns>
      private Style CreateDefaultBorderViewStyle()
      {
         return FormsUtils.CreateShapeViewStyle();
      }

      private Style CreateDefaultInvalidBorderViewStyle()
      {
         return FormsUtils.CreateShapeViewStyle(borderColor:Color.Red, borderThickness:1);
      }

      /// <summary>Creates the default invalid instructions style.</summary>
      /// <returns>Style.</returns>
      private Style CreateDefaultInvalidInstructionsStyle()
      {
         return CreateInstructionsStyle(false);
      }

      /// <summary>Creates the default invalid placeholder style.</summary>
      /// <returns>Style.</returns>
      private Style CreateDefaultInvalidPlaceholderStyle()
      {
         return CreatePlaceholderStyle();
      }

      /// <summary>Creates the default valid instructions style.</summary>
      /// <returns>Style.</returns>
      private Style CreateDefaultValidInstructionsStyle()
      {
         return CreateInstructionsStyle(true);
      }

      /// <summary>Creates the default valid placeholder style.</summary>
      /// <returns>Style.</returns>
      private Style CreateDefaultValidPlaceholderStyle()
      {
         return CreatePlaceholderStyle();
      }

      /// <summary>Creates the instructions style.</summary>
      /// <param name="isValid">if set to <c>true</c> [is valid].</param>
      /// <returns>Style.</returns>
      private Style CreateInstructionsStyle(bool isValid)
      {
         return FormsUtils.CreateLabelStyle(
            FontFamilyOverride,
            DEFAULT_INSTRUCTIONS_LABEL_FONT_SIZE,
            Color.Transparent,
            isValid ? VALID_TEXT_COLOR : INVALID_TEXT_COLOR,
            isValid ? VALID_FONT_ATTRIBUTES : INVALID_FONT_ATTRIBUTES
         );
      }

      /// <summary>Creates the placeholder style.</summary>
      /// <returns>Style.</returns>
      private Style CreatePlaceholderStyle()
      {
         return FormsUtils.CreateLabelStyle(
            FontFamilyOverride,
            DEFAULT_PLACEHOLDER_LABEL_FONT_SIZE,
            DEFAULT_PLACEHOLDER_BACK_COLOR,
            DEFAULT_PLACEHOLDER_TEXT_COLOR
         );
      }

      /// <summary>Handles the is valid changed.</summary>
      /// <param name="isValid">if set to <c>true</c> [is valid].</param>
      private void HandleIsValidChanged(bool? isValid)
      {
         // If the validator issues a validation error, show that in place of the instructions (below the border view).
         if (_validator.IsNotNullOrDefault() && _validator.LastValidationError.IsNotEmpty() &&
             _showValidationErrorsAsInstructions)
         {
            CurrentInstructions = _validator.LastValidationError;
         }

         if (!isValid.HasValue || isValid.GetValueOrDefault())
         {
            BorderView.SetAndForceStyle(_lastValidBorderViewStyle.IsNotNullOrDefault()
               ? _lastValidBorderViewStyle
               : ValidBorderViewStyle);

#if SKIP_RESTORING_PLACEHOLDER            
            PlaceholderLabel?.SetAndForceStyle(ValidPlaceholderStyle);
#else            
            PlaceholderLabel?.SetAndForceStyle(_lastValidPlaceholderStyle.IsNotNullOrDefault()
               ? _lastValidPlaceholderStyle
               : ValidBorderViewStyle);
#endif
         }
         else
         {
            // The corner radius is always uniform in or examples so randomly picking top left
            _lastValidBorderViewStyle  = FormsUtils.CreateShapeViewStyle(BorderView.Color, BorderView.CornerRadius.TopLeft, BorderView.BorderColor, BorderView.BorderThickness);

#if SKIP_RESTORING_PLACEHOLDER            
            PlaceholderLabel?.SetAndForceStyle(ValidPlaceholderStyle);
#else            
            if (PlaceholderLabel.IsNotNullOrDefault())
            {
               _lastValidPlaceholderStyle = FormsUtils.CreateLabelStyle(PlaceholderLabel.FontFamily, PlaceholderLabel.FontSize, PlaceholderLabel.BackgroundColor, PlaceholderLabel.TextColor);
            }
#endif            

            BorderView.SetAndForceStyle(InvalidBorderViewStyle);

#if !SKIP_RESTORING_PLACEHOLDER
            PlaceholderLabel?.SetAndForceStyle(InvalidPlaceholderStyle);
#endif
            
         }
      }

      /// <summary>Resets the placeholder position.</summary>
      protected async Task ResetPlaceholderPosition()
      {
         // Make sure we have a valid placeholder
         if (PlaceholderLabel.IsNullOrDefault() || PlaceholderLabel.Text.IsEmpty() || EditableView.IsNullOrDefault() || BorderView.IsNullOrDefault() || _placeholderGrid.IsNullOrDefault())
         {
            return;
         }

         var    targetX = PLACEHOLDER_INSET;
         double targetY;

         // See if the entry has focus or not
         if (EditableView.IsFocused || UserHasEnteredValidContent || DerivedViewIsFocused)
         {
            targetY = -(_placeholderHeight / 2) + VERTICAL_SLOP;
         }
         else
         {
            targetY = ((BorderView.Height - _placeholderHeight) / 2) + VERTICAL_SLOP;
         }

         _canvas.RaiseChild(_placeholderGrid);

         await _placeholderGrid.TranslateTo(targetX, targetY).WithoutChangingContext();

         if (!_placeholderLabelHasBeenShown)
         {
            PlaceholderLabel.FadeIn();
            _placeholderLabelHasBeenShown = true;
         }
      }

      public void CallRevalidate()
      {
         _validator?.Revalidate();
      }

      public virtual int SetTabIndexes(int incomingTabIndex)
      {
         BorderView.IsTabStop = false;
         BorderView.TabIndex = incomingTabIndex++;

         if (EditableViewContainer.IsNotAnEqualReferenceTo(EditableView))
         {
            EditableViewContainer.IsTabStop = false;
            EditableViewContainer.TabIndex  = incomingTabIndex++;
         }

         EditableView.IsTabStop = true;
         EditableView.TabIndex  = incomingTabIndex++;

         return incomingTabIndex;
      }

      ///// <summary>
      ///// This can be a string (default) or a number, if there is a bound numeric converter
      ///// So returning the actual bound, converted value.
      ///// </summary>
      ///// <returns></returns>
      //public object GetCurrentViewModelValue()
      //{
      //   if (BindingContext.IsNullOrDefault() || _bindableProperty.IsNullOrDefault())
      //   {
      //      return default;
      //   }

      //   return GetValue(_bindableProperty);
      //}
   }
}
