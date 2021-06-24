// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=ValidatableViewBase.cs
// company="Marcus Technical Services, Inc.">
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

// #define SUPPRESS_PROP_CHANGED

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using System;
   using System.ComponentModel;
   using System.Threading.Tasks;
   using Common.Interfaces;
   using Common.Notifications;
   using Common.Utils;
   using SharedUtils.Utils;
   using ViewModels;
   using Xamarin.Forms;

   public interface IValidatableView : IValidatableViewCommon
   {
      ShapeView BorderView { get; }

      ICanBeValid Validator { get; set; }

      void CallRevalidate();

      void StopConstructionAndRefresh();
   }

   public interface IValidatableViewCommon : IViewModelValidationAttributeBase
   {
      Color            BorderViewBorderColor    { get; set; }
      BindableProperty BoundProperty            { get; set; }
      IValueConverter  Converter                { get; set; }
      object           ConverterParameter       { get; set; }
      Label            InstructionsLabel        { get; }
      Color            InstructionsTextColor    { get; set; }
      Style            InvalidBorderViewStyle   { get; set; }
      Style            InvalidInstructionsStyle { get; set; }
      Style            InvalidPlaceholderStyle  { get; set; }
      Color            InvalidTextColor         { get; set; }
      Color            PlaceholderBackColor     { get; set; }
      Label            PlaceholderLabel         { get; }
      Color            PlaceholderTextColor     { get; set; }
      Style            ValidBorderViewStyle     { get; set; }
      Style            ValidInstructionsStyle   { get; set; }
      Style            ValidPlaceholderStyle    { get; set; }
      Color            ValidTextColor           { get; set; }

      int SetTabIndexes(int incomingTabIndex);
   }

   public abstract class ValidatableViewBase : Grid, IValidatableView
   {
      public const double DEFAULT_BORDER_VIEW_HEIGHT = 0;

      // unset
      public const double DEFAULT_GRID_SINGLE_PADDING = 8;

      public const FontAttributes DEFAULT_INSTRUCTIONS_FONT_ATTRIBUTES =
         FontAttributes.Italic | FontAttributes.Bold;

      public const double DEFAULT_INSTRUCTIONS_HEIGHT                 = 30;
      public const double DEFAULT_INSTRUCTIONS_LABEL_FONT_SIZE_FACTOR = 2 / 3d;
      public const double DEFAULT_VALIDATION_FONT_SIZE_FACTOR         = 2 / 3d;

      public const FontAttributes DEFAULT_INVALID_FONT_ATTRIBUTES =
         FontAttributes.Bold | FontAttributes.Italic;

      public const double DEFAULT_PLACEHOLDER_HEIGHT                 = 20;
      public const double DEFAULT_PLACEHOLDER_INSET                  = 8;
      public const double DEFAULT_PLACEHOLDER_LABEL_FONT_SIZE_FACTOR = 2 / 3d;
      public const double DEFAULT_PLACEHOLDER_LABEL_SIDE_MARGIN      = 8;
      public const double DEFAULT_PLACEHOLDER_TOP_MARGIN_ADJUSTMENT  = -4;

      public static readonly double DEFAULT_BORDER_VIEW_BORDER_WIDTH =
         (FormsUtils.IsIos() ? 1.25 : 1.75).AdjustForOsAndDevice();

      public static readonly Color DEFAULT_INSTRUCTIONS_TEXT_COLOR = Color.Purple;
      public static readonly Color DEFAULT_VALID_TEXT_COLOR        = Color.Black;

      private static readonly Color DEFAULT_BORDER_VIEW_BORDER_COLOR = Color.Black;
      private static readonly Color DEFAULT_INVALID_TEXT_COLOR       = Color.Red;
      private static readonly Color DEFAULT_PLACEHOLDER_BACK_COLOR   = Color.White;
      private static readonly Color DEFAULT_PLACEHOLDER_TEXT_COLOR   = Color.DimGray;

      public static readonly BindableProperty BorderViewBorderColorProperty =
         CreateValidatableViewBindablePropertyAndRespondByRefreshingStyles
            (
             nameof(BorderViewBorderColor),
             DEFAULT_BORDER_VIEW_BORDER_COLOR
            );

      public static readonly BindableProperty BorderViewBorderWidthProperty =
         CreateValidatableViewBindablePropertyAndRespondByRefreshingStyles
            (
             nameof(BorderViewBorderWidth),
             DEFAULT_BORDER_VIEW_BORDER_WIDTH
            );

      public static readonly BindableProperty BorderViewCornerRadiusFactorProperty =
         CreateValidatableViewBindablePropertyAndRespondByRefreshingStyles
            (
             nameof(BorderViewCornerRadiusFactor),
             (double) FormsConst.DEFAULT_CORNER_RADIUS_FACTOR
            );

      public static readonly BindableProperty BorderViewHeightProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(BorderViewHeight),
             DEFAULT_BORDER_VIEW_HEIGHT
            );

      public static readonly BindableProperty BoundModeProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(BoundMode),
             BindingMode.TwoWay
            );

      public static readonly BindableProperty BoundPropertyProperty =
         // CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
         CreateValidatableViewBindableProperty
            (
             nameof(BoundProperty),
             default(BindableProperty)
            );

      public static readonly BindableProperty ConverterParameterProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(ConverterParameter),
             default(object)
            );

      public static readonly BindableProperty ConverterProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(Converter),
             default(IValueConverter)
            );

      // No impact on anything
      public static readonly BindableProperty CurrentInstructionsProperty =
         CreateValidatableViewBindableProperty
            (
             nameof(CurrentInstructions),
             default(string)
            );

      public static readonly BindableProperty FontFamilyOverrideProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(FontFamilyOverride),
             Font.SystemFontOfSize(1d).FontFamily
            );

      public static readonly BindableProperty GridSinglePaddingProperty =
         CreateValidatableViewBindablePropertyAndRespondByRefreshingStyles
            (
             nameof(GridSinglePadding),
             DEFAULT_GRID_SINGLE_PADDING
            );

      public static readonly BindableProperty InstructionsFontAttributesProperty =
         CreateValidatableViewBindablePropertyAndRespondByRefreshingStyles
            (
             nameof(InstructionsFontAttributes),
             DEFAULT_INSTRUCTIONS_FONT_ATTRIBUTES
            );

      public static readonly BindableProperty InstructionsHeightProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(InstructionsHeight),
             DEFAULT_INSTRUCTIONS_HEIGHT
            );

      public static readonly BindableProperty InstructionsLabelFontSizeFactorProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(InstructionsLabelFontSizeFactor),
             DEFAULT_INSTRUCTIONS_LABEL_FONT_SIZE_FACTOR
            );

      public static readonly BindableProperty InstructionsTextColorProperty =
         CreateValidatableViewBindableProperty
            (
             nameof(InstructionsTextColor),
             DEFAULT_INSTRUCTIONS_TEXT_COLOR
            );

      // No impact on anything
      public static readonly BindableProperty InstructionsTextProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(InstructionsText),
             default(string)
            );

      public static readonly BindableProperty InvalidBorderViewStyleProperty =
         CreateValidatableViewBindableProperty
            (
             nameof(InvalidBorderViewStyle),
             default(Style)
            );

      public static readonly BindableProperty InvalidFontAttributesProperty =
         CreateValidatableViewBindablePropertyAndRespondByRefreshingStyles
            (
             nameof(InvalidFontAttributes),
             DEFAULT_INVALID_FONT_ATTRIBUTES
            );

      public static readonly BindableProperty InvalidInstructionsStyleProperty =
         CreateValidatableViewBindableProperty
            (
             nameof(InvalidInstructionsStyle),
             default(Style)
            );

      public static readonly BindableProperty InvalidPlaceholderStyleProperty =
         CreateValidatableViewBindableProperty
            (
             nameof(InvalidPlaceholderStyle),
             default(Style)
            );

      public static readonly BindableProperty InvalidTextColorProperty =
         CreateValidatableViewBindablePropertyAndRespondByRefreshingStyles
            (
             nameof(InvalidTextColor),
             DEFAULT_INVALID_TEXT_COLOR
            );

      public static readonly BindableProperty PlaceholderBackColorProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(PlaceholderBackColor),
             DEFAULT_PLACEHOLDER_BACK_COLOR
            );

      public static readonly BindableProperty PlaceholderHeightProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(PlaceholderHeight),
             DEFAULT_PLACEHOLDER_HEIGHT
            );

      public static readonly BindableProperty PlaceholderInsetProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(PlaceholderInset),
             DEFAULT_PLACEHOLDER_INSET
            );

      public static readonly BindableProperty PlaceholderLabelFontSizeFactorProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(PlaceholderLabelFontSizeFactor),
             DEFAULT_PLACEHOLDER_LABEL_FONT_SIZE_FACTOR
            );

      public static readonly BindableProperty PlaceholderLabelSideMarginProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(PlaceholderLabelSideMargin),
             DEFAULT_PLACEHOLDER_LABEL_SIDE_MARGIN
            );

      public static readonly BindableProperty PlaceholderTextColorProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(PlaceholderTextColor),
             DEFAULT_PLACEHOLDER_TEXT_COLOR
            );

      public static readonly BindableProperty PlaceholderTextProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(PlaceholderText),
             default(string)
            );

      public static readonly BindableProperty PlaceholderTopMarginAdjustmentProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(PlaceholderTopMarginAdjustment),
             DEFAULT_PLACEHOLDER_TOP_MARGIN_ADJUSTMENT
            );

      public static readonly BindableProperty ShowInstructionsProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(ShowInstructions),
             default(int)
            );

      public static readonly BindableProperty ShowValidationErrorsAsInstructionsProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(ShowValidationErrorsWithInstructions),
             default(int)
            );

      public static readonly BindableProperty ShowValidationErrorsProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(ShowValidationErrors),
             default(int)
            );

      public static readonly BindableProperty StringFormatProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(StringFormat),
             default(string)
            );

      public static readonly FontAttributes VALID_FONT_ATTRIBUTES = FontAttributes.None;

      public static readonly BindableProperty ValidatorProperty =
         CreateValidatableViewBindablePropertyAndRespondByRefreshingStyles
            (
             nameof(Validator),
             default(ICanBeValid)
            );

      public static readonly BindableProperty ValidationFontSizeFactorProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(ValidationFontSizeFactor),
             DEFAULT_VALIDATION_FONT_SIZE_FACTOR
            );

      public static readonly BindableProperty ValidBorderViewStyleProperty =
         CreateValidatableViewBindableProperty
            (
             nameof(ValidBorderViewStyle),
             default(Style)
            );

      public static readonly BindableProperty ValidFontAttributesProperty =
         CreateValidatableViewBindablePropertyAndRespondByRefreshingStyles
            (
             nameof(ValidFontAttributes),
             VALID_FONT_ATTRIBUTES
            );

      public static readonly BindableProperty ValidInstructionsStyleProperty =
         CreateValidatableViewBindableProperty
            (
             nameof(ValidInstructionsStyle),
             default(Style)
            );

      public static readonly BindableProperty ValidPlaceholderStyleProperty =
         CreateValidatableViewBindableProperty
            (
             nameof(ValidPlaceholderStyle),
             default(Style)
            );

      public static readonly BindableProperty ValidTextColorProperty =
         CreateValidatableViewBindablePropertyAndRespondByRefreshingStyles
            (
             nameof(ValidTextColor),
             DEFAULT_VALID_TEXT_COLOR
            );

      public static readonly BindableProperty ViewModelPropertyNameProperty =
         CreateValidatableViewBindablePropertyAndRespondByRecreatingViews
            (
             nameof(ViewModelPropertyName),
             default(string)
            );

      private                 AbsoluteLayout _canvas;
      private                 Rectangle      _lastBorderViewBounds;
      private                 Rectangle      _lastEditableViewContainerBounds;
      private                 Style          _lastValidBorderViewStyle;
      private                 Grid           _placeholderGrid;
      private                 bool           _placeholderLabelHasBeenShown;
      private                 bool           _recreateAllViewsBindingsAndStylesEntered;

      protected ValidatableViewBase(BindableProperty boundProp,
                                    ICanBeValid      validator       = default,
                                    bool             asleepInitially = false)
      {
         this.SetDefaults();

         BoundProperty   = boundProp;
         Validator       = validator;
         BackgroundColor = Color.Transparent;
         ColumnSpacing   = 0;
         RowSpacing      = 0;

         IsConstructing = asleepInitially;
      }

      public string StringFormat
      {
         get => (string) GetValue(StringFormatProperty);
         set => SetValue(StringFormatProperty, value);
      }

      protected abstract bool DerivedViewIsFocused       { get; }
      protected abstract View EditableView               { get; }
      protected abstract View EditableViewContainer      { get; }
      protected          bool IsConstructing             { get; private set; }
      protected abstract bool UserHasEnteredValidContent { get; }

      private double InstructionsFactoredFontSize => GetFactoredFontSize(InstructionsLabelFontSizeFactor);

      private double PlaceholderFactoredFontSize => GetFactoredFontSize(PlaceholderLabelFontSizeFactor);

      /// <summary>Gets the border view.</summary>
      /// <value>The border view.</value>
      public ShapeView BorderView { get; private set; }

      public Color BorderViewBorderColor
      {
         get => (Color) GetValue(BorderViewBorderColorProperty);
         set => SetValue(BorderViewBorderColorProperty, value);
      }

      public double BorderViewBorderWidth
      {
         get => (double) GetValue(BorderViewBorderWidthProperty);
         set => SetValue(BorderViewBorderWidthProperty, value);
      }

      public double BorderViewCornerRadiusFactor
      {
         get => (double) GetValue(BorderViewCornerRadiusFactorProperty);
         set => SetValue(BorderViewCornerRadiusFactorProperty, value);
      }

      public double BorderViewHeight
      {
         get => (double) GetValue(BorderViewHeightProperty);
         set => SetValue(BorderViewHeightProperty, value);
      }

      public BindingMode BoundMode
      {
         get => (BindingMode) GetValue(BoundModeProperty);
         set => SetValue(BoundModeProperty, value);
      }

      public BindableProperty BoundProperty
      {
         get => (BindableProperty) GetValue(BoundPropertyProperty);
         set => SetValue(BoundPropertyProperty, value);
      }

      public IValueConverter Converter
      {
         get => (IValueConverter) GetValue(ConverterProperty);
         set => SetValue(ConverterProperty, value);
      }

      public object ConverterParameter
      {
         get => GetValue(ConverterParameterProperty);
         set => SetValue(ConverterParameterProperty, value);
      }

      public string CurrentInstructions
      {
         get => (string) GetValue(CurrentInstructionsProperty);
         set => SetValue(CurrentInstructionsProperty, value);
      }

      public string FontFamilyOverride
      {
         get => (string) GetValue(FontFamilyOverrideProperty);
         set => SetValue(FontFamilyOverrideProperty, value);
      }

      public double GridSinglePadding
      {
         get => (double) GetValue(GridSinglePaddingProperty);
         set => SetValue(GridSinglePaddingProperty, value);
      }

      public FontAttributes InstructionsFontAttributes
      {
         get => (FontAttributes) GetValue(InstructionsFontAttributesProperty);
         set => SetValue(InstructionsFontAttributesProperty, value);
      }

      public double InstructionsHeight
      {
         get => (double) GetValue(InstructionsHeightProperty);
         set => SetValue(InstructionsHeightProperty, value);
      }

      public Label InstructionsLabel { get; private set; }

      public double InstructionsLabelFontSizeFactor
      {
         get => (double) GetValue(InstructionsLabelFontSizeFactorProperty);
         set => SetValue(InstructionsLabelFontSizeFactorProperty, value);
      }

      public string InstructionsText
      {
         get => (string) GetValue(InstructionsTextProperty);
         set => SetValue(InstructionsTextProperty, value);
      }

      public Color InstructionsTextColor
      {
         get => (Color) GetValue(InstructionsTextColorProperty);
         set => SetValue(InstructionsTextColorProperty, value);
      }

      /// <summary>Gets or sets the invalid border view style.</summary>
      /// <value>The invalid border view style.</value>
      public Style InvalidBorderViewStyle
      {
         get => (Style) GetValue(InvalidBorderViewStyleProperty);
         set => SetValue(InvalidBorderViewStyleProperty, value);
      }

      public FontAttributes InvalidFontAttributes
      {
         get => (FontAttributes) GetValue(InvalidFontAttributesProperty);
         set => SetValue(InvalidFontAttributesProperty, value);
      }

      public Style InvalidInstructionsStyle
      {
         get => (Style) GetValue(InvalidInstructionsStyleProperty);
         set => SetValue(InvalidInstructionsStyleProperty, value);
      }

      public Style InvalidPlaceholderStyle
      {
         get => (Style) GetValue(InvalidPlaceholderStyleProperty);
         set => SetValue(InvalidPlaceholderStyleProperty, value);
      }

      public Color InvalidTextColor
      {
         get => (Color) GetValue(InvalidTextColorProperty);
         set => SetValue(InvalidTextColorProperty, value);
      }

      public Color PlaceholderBackColor
      {
         get => (Color) GetValue(PlaceholderBackColorProperty);
         set => SetValue(PlaceholderBackColorProperty, value);
      }

      public double PlaceholderHeight
      {
         get => (double) GetValue(PlaceholderHeightProperty);
         set => SetValue(PlaceholderHeightProperty, value);
      }

      public double PlaceholderInset
      {
         get => (double) GetValue(PlaceholderInsetProperty);
         set => SetValue(PlaceholderInsetProperty, value);
      }

      public Label PlaceholderLabel { get; private set; }

      public double PlaceholderLabelFontSizeFactor
      {
         get => (double) GetValue(PlaceholderLabelFontSizeFactorProperty);
         set => SetValue(PlaceholderLabelFontSizeFactorProperty, value);
      }

      public double PlaceholderLabelSideMargin
      {
         get => (double) GetValue(PlaceholderLabelSideMarginProperty);
         set => SetValue(PlaceholderLabelSideMarginProperty, value);
      }

      public string PlaceholderText
      {
         get => (string) GetValue(PlaceholderTextProperty);
         set => SetValue(PlaceholderTextProperty, value);
      }

      public Color PlaceholderTextColor
      {
         get => (Color) GetValue(PlaceholderTextColorProperty);
         set => SetValue(PlaceholderTextColorProperty, value);
      }

      public double PlaceholderTopMarginAdjustment
      {
         get => (double) GetValue(PlaceholderTopMarginAdjustmentProperty);
         set => SetValue(PlaceholderTopMarginAdjustmentProperty, value);
      }

      public int ShowInstructions
      {
         get => (int) GetValue(ShowInstructionsProperty);
         set => SetValue(ShowInstructionsProperty, value);
      }

      public int ShowValidationErrors
      {
         get => (int) GetValue(ShowValidationErrorsProperty);
         set => SetValue(ShowValidationErrorsProperty, value);
      }

      public int ShowValidationErrorsWithInstructions
      {
         get => (int) GetValue(ShowValidationErrorsAsInstructionsProperty);
         set => SetValue(ShowValidationErrorsAsInstructionsProperty, value);
      }

      public double ValidationFontSizeFactor
      {
         get => (double)GetValue(ValidationFontSizeFactorProperty);
         set => SetValue(ValidationFontSizeFactorProperty, value);
      }

      public ICanBeValid Validator
      {
         get => (ICanBeValid) GetValue(ValidatorProperty);
         set => SetValue(ValidatorProperty, value);
      }

      public Style ValidBorderViewStyle
      {
         get => (Style) GetValue(ValidBorderViewStyleProperty);
         set => SetValue(ValidBorderViewStyleProperty, value);
      }

      public FontAttributes ValidFontAttributes
      {
         get => (FontAttributes) GetValue(ValidFontAttributesProperty);
         set => SetValue(ValidFontAttributesProperty, value);
      }

      public Style ValidInstructionsStyle
      {
         get => (Style) GetValue(ValidInstructionsStyleProperty);
         set => SetValue(ValidInstructionsStyleProperty, value);
      }

      public Style ValidPlaceholderStyle
      {
         get => (Style) GetValue(ValidPlaceholderStyleProperty);
         set => SetValue(ValidPlaceholderStyleProperty, value);
      }

      public Color ValidTextColor
      {
         get => (Color) GetValue(ValidTextColorProperty);
         set => SetValue(ValidTextColorProperty, value);
      }

      public string ViewModelPropertyName
      {
         get => (string) GetValue(ViewModelPropertyNameProperty);
         set => SetValue(ViewModelPropertyNameProperty, value);
      }

      public void CallRevalidate()
      {
         Validator?.Revalidate();
      }

      public virtual int SetTabIndexes(int incomingTabIndex)
      {
         BorderView.IsTabStop = false;
         // BorderView.TabIndex = incomingTabIndex++;

         if (EditableViewContainer.IsNotAnEqualReferenceTo(EditableView))
         {
            EditableViewContainer.IsTabStop = false;
            // EditableViewContainer.TabIndex = incomingTabIndex++;
         }

         EditableView.IsTabStop = true;
         EditableView.TabIndex  = incomingTabIndex++;

         return incomingTabIndex;
      }

      public void StopConstructionAndRefresh()
      {
         IsConstructing = false;
         RecreateAllViewsBindingsAndStyles();
      }

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

      public static BindableProperty CreateValidatableViewBindablePropertyAndRespondByRecreatingViews<PropertyTypeT>
      (
         string        localPropName,
         PropertyTypeT defaultVal  = default,
         BindingMode   bindingMode = BindingMode.OneWay
      )
      {
         return CreateValidatableViewBindableProperty(localPropName, defaultVal, bindingMode,
                                                      (
                                                         view,
                                                         oldVal,
                                                         newVal
                                                      ) =>
                                                      {
                                                         // Force reconstruction
                                                         view.RecreateAllViewsBindingsAndStyles();
                                                      });
      }

      public static BindableProperty CreateValidatableViewBindablePropertyAndRespondByRefreshingStyles<PropertyTypeT>
      (
         string        localPropName,
         PropertyTypeT defaultVal  = default,
         BindingMode   bindingMode = BindingMode.OneWay
      )
      {
         return CreateValidatableViewBindableProperty(localPropName, defaultVal, bindingMode,
                                                      (
                                                         view,
                                                         oldVal,
                                                         newVal
                                                      ) =>
                                                      {
                                                         // Request style refresh
                                                         view.ReapplyStyles();
                                                      });
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
         if (ViewModelPropertyName.IsEmpty() || BoundProperty.IsNullOrDefault() || EditableView.IsNullOrDefault())
         {
            return;
         }

         // Avoid redundant bindings
         EditableView.RemoveBinding(BoundProperty);

         if (Converter.IsNotNullOrDefault())
         {
            if (StringFormat.IsNotEmpty())
            {
               EditableView.SetUpBinding(BoundProperty, ViewModelPropertyName, BoundMode, Converter,
                                         ConverterParameter, StringFormat);
            }
            else
            {
               EditableView.SetUpBinding(BoundProperty, ViewModelPropertyName, BoundMode, Converter,
                                         ConverterParameter);
            }
         }
         else if (StringFormat.IsNotEmpty())
         {
            EditableView.SetUpBinding(BoundProperty, ViewModelPropertyName, BoundMode,
                                      stringFormat: StringFormat);
         }
         else
         {
            EditableView.SetUpBinding(BoundProperty, ViewModelPropertyName, BoundMode);
         }
      }

      /// <summary>Creates the views.</summary>
      protected virtual void CreateViews()
      {
         this.ClearCompletely();

         BorderView               = FormsUtils.GetShapeView();
         BorderView.HeightRequest = BorderViewHeight + GridSinglePadding;
         BorderView.CornerRadius  = BorderViewHeight * BorderViewCornerRadiusFactor;
         BorderView.Content       = EditableViewContainer;
         BorderView.Margin        = new Thickness(0, GridSinglePadding, 0, 0);
         BorderView.Border =
            FormsUtils.CreateShapeViewBorder(BorderViewBorderColor, BorderViewBorderWidth);

         // Allow for the border view's height and the vertical padding var totalHeight = BorderViewHeight + GridSinglePadding;

#if !SUPPRESS_PROP_CHANGED
         // BorderView is newly created, so the += subscription is not redundant
         BorderView.PropertyChanged +=
            async (sender, args) =>
            {
               if (BorderView.Bounds.AreValidAndHaveChanged(args.PropertyName, _lastBorderViewBounds))
               {
                  _lastBorderViewBounds = BorderView.Bounds;
                  await ResetPlaceholderPosition().WithoutChangingContext();
               }
            };
#endif

         // Row 0 holds the border view and entry
         this.AddFixedRow(BorderViewHeight);
         this.AddAndSetRowsAndColumns(BorderView, 0);

         // Row 1 (optional) holds the instructions InstructionsText might not show up until run-time
         if (ShowInstructions.IsTrue())
         {
            InstructionsLabel =
               FormsUtils.GetSimpleLabel(
                                         InstructionsText,
                                         fontFamilyOverride: FontFamilyOverride,
                                         textColor: InstructionsTextColor,
                                         fontAttributes: InstructionsFontAttributes,
                                         fontSize: InstructionsFactoredFontSize,
                                         labelBindingPropertyName: nameof(CurrentInstructions),
                                         labelBindingSource: this);
            InstructionsLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
            InstructionsLabel.VerticalOptions   = LayoutOptions.StartAndExpand;
            // InstructionsLabel.HeightRequest = InstructionsHeight;

            var smallMargin = GridSinglePadding / 2;

            InstructionsLabel.Margin = new Thickness(0, smallMargin);

            this.AddAutoRow();
            this.AddAndSetRowsAndColumns(InstructionsLabel, 1);

            //// The current instructions always show in this label; to change the content, change the property.
            //InstructionsLabel.SetUpBinding(Label.TextProperty, nameof(CurrentInstructions), source: this);
         }
         else
         {
            InstructionsLabel = default;
         }

         // Placeholder floats on the canvas
         if (PlaceholderText.IsNotEmpty())
         {
            PlaceholderLabel = FormsUtils.GetSimpleLabel(PlaceholderText, fontFamilyOverride: FontFamilyOverride,
                                                         textColor: PlaceholderTextColor,
                                                         fontSize: PlaceholderFactoredFontSize);

            PlaceholderLabel.HeightRequest = PlaceholderHeight;
            PlaceholderLabel.Text          = PlaceholderText;

            _placeholderGrid                 = FormsUtils.GetExpandingGrid();
            _placeholderGrid.BackgroundColor = PlaceholderBackColor;
            PlaceholderLabel.BackgroundColor = PlaceholderBackColor;
            _placeholderGrid.AddAutoRow();
            _placeholderGrid.AddFixedColumn(PlaceholderLabelSideMargin);
            _placeholderGrid.AddAutoColumn();
            _placeholderGrid.AddAndSetRowsAndColumns(PlaceholderLabel, 0, 1);
            _placeholderGrid.AddFixedColumn(PlaceholderLabelSideMargin);

            // Absolute layout for _canvas position -- this overlays the other rows
            _canvas = FormsUtils.GetExpandingAbsoluteLayout();

            // IMPORTANT -- the canvas is on top of other things, so must let user input through
            _canvas.InputTransparent = true;

            // The exact position will be set once the border view gets its bounds (and whenever those bonds change)
            _canvas.Children.Add(_placeholderGrid);

            // The canvas has to be able to float higher than the top border
            this.AddAndSetRowsAndColumns(_canvas, 0);
            RaiseChild(_canvas);

            EditableView.Focused   -= ReportGlobalFocusAndRaisePlaceholder;
            EditableView.Focused   += ReportGlobalFocusAndRaisePlaceholder;
            EditableView.Unfocused -= ConsiderLoweringPlaceholder;
            EditableView.Unfocused += ConsiderLoweringPlaceholder;
         }
         else
         {
            PlaceholderLabel = default;
         }

         // Validate immediately if possible
         if (Validator.IsNotNullOrDefault() && Validator is Behavior validatorAsBehavior)
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
               // The view is too generalized to host a behavior. The container has other information needed for validation.
               if (!Behaviors.Contains(validatorAsBehavior))
               {
                  Behaviors.Add(validatorAsBehavior);
               }
            }

            Validator.IsValidChanged -= HandleIsValidChanged;
            Validator.IsValidChanged += HandleIsValidChanged;
            Validator.Revalidate();
         }

         // HeightRequest = totalHeight;

#if !SUPPRESS_PROP_CHANGED
         if (EditableViewContainer.IsNotNullOrDefault())
         {
            EditableViewContainer.PropertyChanged -= EditableViewContainerOnPropertyChanged();
            EditableViewContainer.PropertyChanged += EditableViewContainerOnPropertyChanged();
         }
#endif
      }

      // WARNING: Async void
      protected override async void OnBindingContextChanged()
      {
         base.OnBindingContextChanged();

         EditableView.BindingContext = BindingContext;

         await ResetPlaceholderPosition().WithoutChangingContext();
      }

      /// <summary>Resets the styles.</summary>
      protected virtual void ReapplyStyles()
      {
         if (IsConstructing)
         {
            return;
         }

         // HACK
         Padding = new Thickness(0, 0, 0, -GridSinglePadding);

         ValidPlaceholderStyle    = CreatePlaceholderStyle();
         InvalidPlaceholderStyle  = CreatePlaceholderStyle();
         InvalidInstructionsStyle = CreateInstructionsStyle(false);
         ValidInstructionsStyle   = CreateInstructionsStyle(true);
         InvalidBorderViewStyle =
            FormsUtils.CreateShapeViewStyle(borderColor: InvalidTextColor, borderThickness: BorderViewBorderWidth);
         ValidBorderViewStyle =
            FormsUtils.CreateShapeViewStyle(borderColor: ValidTextColor, borderThickness: BorderViewBorderWidth);

         // If the validator is null, we are valid by default.
         HandleIsValidChanged(Validator.IsNullOrDefault() || Validator.IsValid.GetValueOrDefault());
      }

      protected void RecreateAllViewsBindingsAndStyles()
      {
         if (_recreateAllViewsBindingsAndStylesEntered || IsConstructing)
         {
            return;
         }

         _recreateAllViewsBindingsAndStylesEntered = true;

         try
         {
            CreateViews();
            CreateBindings();
            ReapplyStyles();
         }
         finally
         {
            _recreateAllViewsBindingsAndStylesEntered = false;
         }
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

      /// <summary>Resets the placeholder position.</summary>
      protected async Task ResetPlaceholderPosition()
      {
         // Make sure we have a valid placeholder
         if (PlaceholderLabel.IsNullOrDefault() || PlaceholderLabel.Text.IsEmpty() || EditableView.IsNullOrDefault() ||
             BorderView.IsNullOrDefault() || !BorderView.Height.IsGreaterThan(0) || _placeholderGrid.IsNullOrDefault())
         {
            return;
         }

         var    targetX = PlaceholderInset;
         double targetY;

         // See if the entry has focus or not
         if (EditableView.IsFocused || UserHasEnteredValidContent || DerivedViewIsFocused)
         {
            targetY = PlaceholderTopMarginAdjustment;
         }
         else
         {
            targetY = (BorderViewHeight + GridSinglePadding - PlaceholderHeight) / 2;
         }

         _canvas.RaiseChild(_placeholderGrid);

         await _placeholderGrid.TranslateTo(targetX, targetY).WithoutChangingContext();

         if (!_placeholderLabelHasBeenShown)
         {
            if (PlaceholderLabel.Opacity.IsLessThan(FormsUtils.VISIBLE_OPACITY))
            {
               PlaceholderLabel.FadeIn();
            }

            _placeholderLabelHasBeenShown = true;
         }
      }

      private Style CreateInstructionsStyle(bool isValid)
      {
         return FormsUtils.CreateLabelStyle(
                                            FontFamilyOverride,
                                            InstructionsFactoredFontSize,
                                            Color.Transparent,
                                            InstructionsTextColor,
                                            FontAttributes.None
                                           );
      }

      private Style CreatePlaceholderStyle()
      {
         return FormsUtils.CreateLabelStyle(
                                            FontFamilyOverride,
                                            PlaceholderFactoredFontSize,
                                            PlaceholderBackColor,
                                            PlaceholderTextColor
                                           );
      }

      private PropertyChangedEventHandler EditableViewContainerOnPropertyChanged()
      {
         return async (sender, args) =>
                {
                   if (EditableViewContainer.Bounds.AreValidAndHaveChanged(args.PropertyName,
                                                                           _lastEditableViewContainerBounds))
                   {
                      _lastEditableViewContainerBounds = EditableViewContainer.Bounds;
                      await ResetPlaceholderPosition().WithoutChangingContext();
                   }
                };
      }

      private double GetFactoredFontSize(double factor)
      {
         // TODO Very bad idea to randomly assign the font size.
         return EditableView is Entry editableViewAsEntry
                   ? editableViewAsEntry.FontSize * factor
                   : Device.GetNamedSize(NamedSize.Small, typeof(Label));
      }

      /// <summary>Handles the is valid changed.</summary>
      /// <param name="isValid">if set to <c>true</c> [is valid].</param>
      private void HandleIsValidChanged(bool? isValid)
      {
         // If the validator issues a validation error, show that in place of the instructions (below the border view).
         if (Validator.IsNotNullOrDefault() && Validator.LastValidationError.IsNotEmpty() &&
             ShowValidationErrors.IsTrue()  && ShowValidationErrorsWithInstructions.IsTrue())
         {
            CurrentInstructions = Validator.LastValidationError;
         }
         else
         {
            CurrentInstructions = "";
         }

         if (ShowValidationErrors.IsTrue() && !isValid.HasValue || isValid.GetValueOrDefault())
         {
            BorderView?.SetAndForceStyle(_lastValidBorderViewStyle.IsNotNullOrDefault()
                                            ? _lastValidBorderViewStyle
                                            : ValidBorderViewStyle);

            PlaceholderLabel?.SetAndForceStyle(ValidPlaceholderStyle);
            InstructionsLabel?.SetAndForceStyle(ValidInstructionsStyle);
         }
         else
         {
            // The corner radius is always uniform in or examples so randomly picking top left
            _lastValidBorderViewStyle = FormsUtils.CreateShapeViewStyle(BorderView?.BackgroundColor,
                                                                        BorderView?.CornerRadius.TopLeft,
                                                                        BorderView?.BorderColor,
                                                                        BorderView?.BorderThickness);

            PlaceholderLabel?.SetAndForceStyle(InvalidPlaceholderStyle);
            BorderView?.SetAndForceStyle(InvalidBorderViewStyle);
            InstructionsLabel?.SetAndForceStyle(InvalidInstructionsStyle);
         }
      }
   }
}