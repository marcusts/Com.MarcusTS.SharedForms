#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ValidatableViewBase.cs, is a part of a program called AccountViewMobile.
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

// #define USE_BACK_COLOR

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Interfaces;
   using Common.Notifications;
   using Common.Utils;
   using SharedUtils.Utils;
   using System;
   using System.ComponentModel;
   using System.Threading.Tasks;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IValidatableView Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IValidatableView : INotifyPropertyChanged
   {
      /// <summary>
      /// Gets the border view.
      /// </summary>
      /// <value>The border view.</value>
      ShapeView BorderView { get; }

      /// <summary>
      /// Gets or sets the current instructions.
      /// </summary>
      /// <value>The current instructions.</value>
      string CurrentInstructions { get; set; }

      /// <summary>
      /// Gets the instructions label.
      /// </summary>
      /// <value>The instructions label.</value>
      Label InstructionsLabel { get; }

      /// <summary>
      /// Gets or sets the invalid border view style.
      /// </summary>
      /// <value>The invalid border view style.</value>
      Style InvalidBorderViewStyle { get; set; }

      /// <summary>
      /// Gets or sets the invalid instructions style.
      /// </summary>
      /// <value>The invalid instructions style.</value>
      Style InvalidInstructionsStyle { get; set; }

      /// <summary>
      /// Gets or sets the invalid placeholder style.
      /// </summary>
      /// <value>The invalid placeholder style.</value>
      Style InvalidPlaceholderStyle { get; set; }

      /// <summary>
      /// Gets the placeholder label.
      /// </summary>
      /// <value>The placeholder label.</value>
      Label PlaceholderLabel { get; }

      /// <summary>
      /// Gets or sets the valid border view style.
      /// </summary>
      /// <value>The valid border view style.</value>
      Style ValidBorderViewStyle { get; set; }

      /// <summary>
      /// Gets or sets the valid instructions style.
      /// </summary>
      /// <value>The valid instructions style.</value>
      Style ValidInstructionsStyle { get; set; }

      /// <summary>
      /// Gets or sets the valid placeholder style.
      /// </summary>
      /// <value>The valid placeholder style.</value>
      Style ValidPlaceholderStyle { get; set; }

      /// <summary>
      /// Calls the revalidate.
      /// </summary>
      void CallRevalidate();

      /// <summary>
      /// Sets the tab indexes.
      /// </summary>
      /// <param name="incomingTabIndex">Index of the incoming tab.</param>
      /// <returns>System.Int32.</returns>
      int SetTabIndexes(int incomingTabIndex);
   }

   /// <summary>
   /// A UI element that includes an Entry surrounded by a border. Implements the <see cref="Xamarin.Forms.Grid" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableView" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Grid" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableView" />
   public abstract class ValidatableViewBase : Grid, IValidatableView
   {
      /// <summary>
      /// The default border view height
      /// </summary>
      public const double BORDER_VIEW_HEIGHT = 40;

      /// <summary>
      /// The default instructions height
      /// </summary>
      public const double INSTRUCTIONS_HEIGHT = 25;

      /// <summary>
      /// The default placeholder height
      /// </summary>
      public const double PLACEHOLDER_HEIGHT = 12;

      /// <summary>
      /// The default border view border width
      /// </summary>
      private const float BORDER_VIEW_BORDER_WIDTH = 1;

      ///// <summary>
      ///// The default border view radius
      ///// </summary>
      //private const float BORDER_VIEW_RADIUS = 3;

      /// <summary>
      /// The default grid single padding
      /// </summary>
      private const double GRID_SINGLE_PADDING = 6;

      /// <summary>
      /// The placeholder inset
      /// </summary>
      private const double PLACEHOLDER_INSET = 8;

      /// <summary>
      /// The placeholder label side margin
      /// </summary>
      private const double PLACEHOLDER_LABEL_SIDE_MARGIN = 6;

      /// <summary>
      /// The default shape view single padding
      /// </summary>
      private const double SHAPE_VIEW_SINGLE_PADDING = 6;

      /// <summary>
      /// The vertical slop
      /// </summary>
      private const double VERTICAL_SLOP = 4;

      /// <summary>
      /// The invalid border view style property
      /// </summary>
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

      /// <summary>
      /// The invalid instructions style property
      /// </summary>
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

      /// <summary>
      /// The invalid placeholder style property
      /// </summary>
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

      /// <summary>
      /// The valid border view style property
      /// </summary>
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

      /// <summary>
      /// The valid instructions style property
      /// </summary>
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

      /// <summary>
      /// The valid placeholder style property
      /// </summary>
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

      /// <summary>
      /// The default border view padding
      /// </summary>
      protected static readonly Thickness DEFAULT_BORDER_VIEW_PADDING = new Thickness(8, 0, 4, 0);

      /// <summary>
      /// The default border view border color
      /// </summary>
      private static readonly Color DEFAULT_BORDER_VIEW_BORDER_COLOR = Color.Black;

      /// <summary>
      /// The default grid padding
      /// </summary>
      private static readonly Thickness DEFAULT_GRID_PADDING =
         new Thickness(GRID_SINGLE_PADDING);

      /// <summary>
      /// The default instructions label font size
      /// </summary>
      private static readonly double DEFAULT_INSTRUCTIONS_LABEL_FONT_SIZE =
         Device.GetNamedSize(NamedSize.Micro, typeof(Label));

      /// <summary>
      /// The default placeholder back color
      /// </summary>
      private static readonly Color DEFAULT_PLACEHOLDER_BACK_COLOR = Color.White;

      /// <summary>
      /// The default placeholder label font size
      /// </summary>
      private static readonly double DEFAULT_PLACEHOLDER_LABEL_FONT_SIZE =
         Device.GetNamedSize(NamedSize.Micro, typeof(Label)) * 1.15;

      /// <summary>
      /// The default placeholder text color
      /// </summary>
      private static readonly Color DEFAULT_PLACEHOLDER_TEXT_COLOR = Color.DimGray;

      ///// <summary>The default shape view border color</summary>
      //private static readonly Color DEFAULT_SHAPE_VIEW_BORDER_COLOR = Color.Black;

      /// <summary>
      /// The default shape view padding
      /// </summary>
      private static readonly Thickness DEFAULT_SHAPE_VIEW_PADDING =
         new Thickness(SHAPE_VIEW_SINGLE_PADDING);

      /// <summary>
      /// The invalid font attributes
      /// </summary>
      private static readonly FontAttributes INVALID_FONT_ATTRIBUTES = FontAttributes.Bold | FontAttributes.Italic;

      /// <summary>
      /// The invalid text color
      /// </summary>
      private static readonly Color INVALID_TEXT_COLOR = Color.Red;

      /// <summary>
      /// The valid font attributes
      /// </summary>
      private static readonly FontAttributes VALID_FONT_ATTRIBUTES = FontAttributes.None;

      /// <summary>
      /// The valid text color
      /// </summary>
      private static readonly Color            VALID_TEXT_COLOR = Color.Black;
      /// <summary>
      /// The bindable property
      /// </summary>
      private readonly        BindableProperty _bindableProperty;
      /// <summary>
      /// The binding mode
      /// </summary>
      private readonly        BindingMode      _bindingMode;
      /// <summary>
      /// The border view height
      /// </summary>
      private readonly        double?          _borderViewHeight;
      /// <summary>
      /// The converter
      /// </summary>
      private readonly        IValueConverter  _converter;
      /// <summary>
      /// The converter parameter
      /// </summary>
      private readonly        object           _converterParameter;
      /// <summary>
      /// The instructions
      /// </summary>
      private readonly        string           _instructions;
      /// <summary>
      /// The instructions height
      /// </summary>
      private readonly        double?          _instructionsHeight;
      /// <summary>
      /// The placeholder
      /// </summary>
      private readonly        string           _placeholder;
      /// <summary>
      /// The placeholder height
      /// </summary>
      private readonly        double           _placeholderHeight;
      /// <summary>
      /// The show instructions or validations
      /// </summary>
      private readonly        bool             _showInstructionsOrValidations;
      /// <summary>
      /// The show validation errors as instructions
      /// </summary>
      private readonly        bool             _showValidationErrorsAsInstructions;
      /// <summary>
      /// The string format
      /// </summary>
      private readonly        string           _stringFormat;
      /// <summary>
      /// The validator
      /// </summary>
      private readonly        ICanBeValid      _validator;
      /// <summary>
      /// The view model property name
      /// </summary>
      private readonly        string           _viewModelPropertyName;
      /// <summary>
      /// The canvas
      /// </summary>
      private AbsoluteLayout   _canvas;
      /// <summary>
      /// The invalid border view style
      /// </summary>
      private Style            _invalidBorderViewStyle;
      /// <summary>
      /// The invalid instructions style
      /// </summary>
      private Style            _invalidInstructionsStyle;
      /// <summary>
      /// The invalid placeholder style
      /// </summary>
      private Style            _invalidPlaceholderStyle;
      /// <summary>
      /// The last border view bounds
      /// </summary>
      private Rectangle        _lastBorderViewBounds;
      /// <summary>
      /// The last editable view container bounds
      /// </summary>
      private Rectangle        _lastEditableViewContainerBounds;
      /// <summary>
      /// The placeholder grid
      /// </summary>
      private Grid             _placeholderGrid;
      /// <summary>
      /// The placeholder label has been shown
      /// </summary>
      private bool             _placeholderLabelHasBeenShown;
      /// <summary>
      /// The valid border view style
      /// </summary>
      private Style            _validBorderViewStyle;
      /// <summary>
      /// The valid instructions style
      /// </summary>
      private Style            _validInstructionsStyle;
      /// <summary>
      /// The valid placeholder style
      /// </summary>
      private Style            _validPlaceholderStyle;
      /// <summary>
      /// The views created
      /// </summary>
      private bool             _viewsCreated;

      /// <summary>
      /// Initializes a new instance of the <see cref="ValidatableViewBase" /> class.
      /// </summary>
      /// <param name="bindableProperty">The bindable property.</param>
      /// <param name="borderViewHeight">Height of the border view.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="converter">The converter.</param>
      /// <param name="converterParameter">The converter parameter.</param>
      /// <param name="fontFamilyOverride">The font family override.</param>
      /// <param name="instructions">The instructions.</param>
      /// <param name="instructionsHeight">Height of the instructions.</param>
      /// <param name="placeholder">The placeholder.</param>
      /// <param name="placeholderHeight">Height of the placeholder.</param>
      /// <param name="showInstructionsOrValidations">if set to <c>true</c> [show instructions or validations].</param>
      /// <param name="showValidationErrorsAsInstructions">if set to <c>true</c> [show validation errors as instructions].</param>
      /// <param name="stringFormat">The string format.</param>
      /// <param name="validator">The validator.</param>
      /// <param name="viewModelPropertyName">Name of the view model property.</param>
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
         HorizontalOptions                   = LayoutOptions.FillAndExpand;
         VerticalOptions                     = LayoutOptions.FillAndExpand;
         BackgroundColor                     = Color.Transparent;
         ColumnSpacing                       = 0;
         RowSpacing                          = 0;

         //if (_stringFormat.IsNotEmpty() && _validator is IHaveStringFormat validatorAsHavingStringFormat)
         //{
         //   validatorAsHavingStringFormat.StringFormat = _stringFormat;
         //}

         // CallCreateViews();

         BindingContextChanged += async (sender, args) =>
                                  {
                                     EditableView.BindingContext = BindingContext;
                                     await ResetPlaceholderPosition().WithoutChangingContext();
                                  };
      }

      /// <summary>
      /// Gets a value indicating whether [derived view is focused].
      /// </summary>
      /// <value><c>true</c> if [derived view is focused]; otherwise, <c>false</c>.</value>
      protected abstract bool DerivedViewIsFocused { get; }

      /// <summary>
      /// Gets the editable view.
      /// </summary>
      /// <value>The editable view.</value>
      protected abstract View EditableView { get; }

      /// <summary>
      /// Gets the editable view container.
      /// </summary>
      /// <value>The editable view container.</value>
      protected abstract View EditableViewContainer { get; }

      /// <summary>
      /// The font family override
      /// </summary>
      /// <value>The font family override.</value>
      protected string FontFamilyOverride { get; }

      /// <summary>
      /// Gets a value indicating whether [user has entered valid content].
      /// </summary>
      /// <value><c>true</c> if [user has entered valid content]; otherwise, <c>false</c>.</value>
      protected abstract bool UserHasEnteredValidContent { get; }

      /// <summary>
      /// Gets the border view.
      /// </summary>
      /// <value>The border view.</value>
      public ShapeView BorderView { get; private set; }

      // ReSharper disable once UnusedAutoPropertyAccessor.Local
      /// <summary>
      /// Gets or sets the current instructions.
      /// </summary>
      /// <value>The current instructions.</value>
      public string CurrentInstructions { get; set; }

      /// <summary>
      /// Gets the instructions label.
      /// </summary>
      /// <value>The instructions label.</value>
      public Label InstructionsLabel { get; private set; }

      /// <summary>
      /// Gets or sets the invalid border view style.
      /// </summary>
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

      /// <summary>
      /// Gets or sets the invalid instructions style.
      /// </summary>
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

      /// <summary>
      /// Gets or sets the invalid placeholder style.
      /// </summary>
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

      /// <summary>
      /// Gets the placeholder label.
      /// </summary>
      /// <value>The placeholder label.</value>
      public Label PlaceholderLabel { get; private set; }

      /// <summary>
      /// Gets or sets the valid border view style.
      /// </summary>
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

      /// <summary>
      /// Gets or sets the valid instructions style.
      /// </summary>
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

      /// <summary>
      /// Gets or sets the valid placeholder style.
      /// </summary>
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

      /// <summary>
      /// Calls the revalidate.
      /// </summary>
      public void CallRevalidate()
      {
         _validator?.Revalidate();
      }

      /// <summary>
      /// Sets the tab indexes.
      /// </summary>
      /// <param name="incomingTabIndex">Index of the incoming tab.</param>
      /// <returns>System.Int32.</returns>
      public virtual int SetTabIndexes(int incomingTabIndex)
      {
         BorderView.IsTabStop = false;
         BorderView.TabIndex  = incomingTabIndex++;

         if (EditableViewContainer.IsNotAnEqualReferenceTo(EditableView))
         {
            EditableViewContainer.IsTabStop = false;
            EditableViewContainer.TabIndex  = incomingTabIndex++;
         }

         EditableView.IsTabStop = true;
         EditableView.TabIndex  = incomingTabIndex++;

         return incomingTabIndex;
      }

      /// <summary>
      /// Creates the validatable entry bindable property.
      /// </summary>
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

      /// <summary>
      /// Calls the create views.
      /// </summary>
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

      /// <summary>
      /// Considers the lowering placeholder.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="FocusEventArgs" /> instance containing the event data.</param>
      /// <remarks>TODO Cannot avoid async void -- event handler.</remarks>
      protected async void ConsiderLoweringPlaceholder(object sender, FocusEventArgs e)
      {
         await ResetPlaceholderPosition().WithoutChangingContext();
      }

      /// <summary>
      /// Creates the bindings.
      /// </summary>
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

         BorderView                   = FormsUtils.GetShapeView();
         BorderView.HorizontalOptions = LayoutOptions.FillAndExpand;
         BorderView.VerticalOptions   = LayoutOptions.CenterAndExpand;

         var borderViewHeight = _borderViewHeight ?? BORDER_VIEW_HEIGHT;

         BorderView.HeightRequest   = borderViewHeight;
         BorderView.BorderThickness = BORDER_VIEW_BORDER_WIDTH;
         BorderView.BorderColor     = DEFAULT_BORDER_VIEW_BORDER_COLOR;
         BorderView.CornerRadius    = borderViewHeight * FormsConst.DEFAULT_CORNER_RADIUS_FACTOR;

         // Allow for the border view's height and the vertical padding
         var totalHeight = BorderView.HeightRequest + GRID_SINGLE_PADDING * 2;

         BorderView.Content = EditableViewContainer;
         BorderView.Margin  = new Thickness(0, 5, 0, 0);

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

         if (_showInstructionsOrValidations && _instructions.IsNotEmpty())
         {
            InstructionsLabel =
               FormsUtils.GetSimpleLabel(_instructions, fontFamilyOverride: FontFamilyOverride);
            InstructionsLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
            InstructionsLabel.VerticalOptions   = LayoutOptions.Center;
            InstructionsLabel.HeightRequest     = _instructionsHeight ?? INSTRUCTIONS_HEIGHT;

            var smallMargin = GRID_SINGLE_PADDING / 2;

            InstructionsLabel.Margin = new Thickness(0, smallMargin);

            // Allow for the margins
            totalHeight += InstructionsLabel.HeightRequest + smallMargin * 2;

            this.AddAutoRow();
            Children.Add(InstructionsLabel);
            SetRow(InstructionsLabel, 1);

            // The current instructions always show in thislabel; to change the content, change the property.
            InstructionsLabel.SetUpBinding(Label.TextProperty, nameof(CurrentInstructions), source: this);
         }

         if (_placeholder.IsNotEmpty())
         {
            PlaceholderLabel = FormsUtils.GetSimpleLabel(_placeholder, fontFamilyOverride: FontFamilyOverride);

            // Hide initially
            PlaceholderLabel.Opacity       = FormsConst.NOT_VISIBLE_OPACITY;
            PlaceholderLabel.HeightRequest = _placeholderHeight;
            PlaceholderLabel.Text          = _placeholder;

            _placeholderGrid                 = FormsUtils.GetExpandingGrid();
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

            EditableView.Focused   += ReportGlobalFocusAndRaisePlaceholder;
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

         _invalidBorderViewStyle   = CreateDefaultInvalidBorderViewStyle();
         _invalidInstructionsStyle = CreateDefaultInvalidInstructionsStyle();
         _invalidPlaceholderStyle  = CreateDefaultInvalidPlaceholderStyle();
         _validPlaceholderStyle    = CreateDefaultValidPlaceholderStyle();
         _validBorderViewStyle     = CreateDefaultBorderViewStyle();
         _validInstructionsStyle   = CreateDefaultValidInstructionsStyle();

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

      /// <summary>
      /// Raises the placeholder.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="FocusEventArgs" /> instance containing the event data.</param>
      /// <remarks>TODO Cannot avoid async void -- event handler.</remarks>
      protected async void ReportGlobalFocusAndRaisePlaceholder(object sender, FocusEventArgs e)
      {
         FormsMessengerUtils.Send(new ViewIsBeingEditedMessage {Payload = true});

         await ResetPlaceholderPosition().WithoutChangingContext();
      }

      /// <summary>
      /// Resets the placeholder position.
      /// </summary>
      protected async Task ResetPlaceholderPosition()
      {
         // Make sure we have a valid placeholder
         if (PlaceholderLabel.IsNullOrDefault() || PlaceholderLabel.Text.IsEmpty() || EditableView.IsNullOrDefault() ||
             BorderView.IsNullOrDefault()       || _placeholderGrid.IsNullOrDefault())
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
            targetY = (BorderView.Height - _placeholderHeight) / 2 + VERTICAL_SLOP;
         }

         _canvas.RaiseChild(_placeholderGrid);

         await _placeholderGrid.TranslateTo(targetX, targetY).WithoutChangingContext();

         if (!_placeholderLabelHasBeenShown)
         {
            PlaceholderLabel.FadeIn();
            _placeholderLabelHasBeenShown = true;
         }
      }

      /// <summary>
      /// Resets the styles.
      /// </summary>
      protected virtual void ResetStyles()
      {
         // If the validator is null, we are valid by default.
         HandleIsValidChanged(_validator.IsNullOrDefault() || _validator.IsValid.GetValueOrDefault());
      }

      /// <summary>
      /// Creates the default border view style.
      /// </summary>
      /// <returns>Style.</returns>
      private Style CreateDefaultBorderViewStyle()
      {
         return FormsUtils.CreateShapeViewStyle();
      }

      /// <summary>
      /// Creates the default invalid border view style.
      /// </summary>
      /// <returns>Style.</returns>
      private Style CreateDefaultInvalidBorderViewStyle()
      {
         return FormsUtils.CreateShapeViewStyle(borderColor: Color.Red, BorderThickness: 1);
      }

      /// <summary>
      /// Creates the default invalid instructions style.
      /// </summary>
      /// <returns>Style.</returns>
      private Style CreateDefaultInvalidInstructionsStyle()
      {
         return CreateInstructionsStyle(false);
      }

      /// <summary>
      /// Creates the default invalid placeholder style.
      /// </summary>
      /// <returns>Style.</returns>
      private Style CreateDefaultInvalidPlaceholderStyle()
      {
         return CreatePlaceholderStyle();
      }

      /// <summary>
      /// Creates the default valid instructions style.
      /// </summary>
      /// <returns>Style.</returns>
      private Style CreateDefaultValidInstructionsStyle()
      {
         return CreateInstructionsStyle(true);
      }

      /// <summary>
      /// Creates the default valid placeholder style.
      /// </summary>
      /// <returns>Style.</returns>
      private Style CreateDefaultValidPlaceholderStyle()
      {
         return CreatePlaceholderStyle();
      }

      /// <summary>
      /// Creates the instructions style.
      /// </summary>
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

      /// <summary>
      /// Creates the placeholder style.
      /// </summary>
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

      /// <summary>
      /// Handles the is valid changed.
      /// </summary>
      /// <param name="isValid">if set to <c>true</c> [is valid].</param>
      private void HandleIsValidChanged(bool? isValid)
      {
         // If the validator issues a validation eror, show that in place of the instructions (below the border view).
         if (_validator.IsNotNullOrDefault() && _validator.LastValidationError.IsNotEmpty() &&
             _showValidationErrorsAsInstructions)
         {
            CurrentInstructions = _validator.LastValidationError;
         }

         if (!isValid.HasValue || isValid.GetValueOrDefault())
         {
            BorderView.SetAndForceStyle(ValidBorderViewStyle);
            PlaceholderLabel?.SetAndForceStyle(ValidPlaceholderStyle);
         }
         else
         {
            BorderView.SetAndForceStyle(InvalidBorderViewStyle);
            PlaceholderLabel?.SetAndForceStyle(InvalidPlaceholderStyle);
         }
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