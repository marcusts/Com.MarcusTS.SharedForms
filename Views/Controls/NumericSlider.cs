// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, NumericSlider.cs, is a part of a program called AccountViewMobile.
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

// #define USE_PROPERTY_CHANGED_BOUNDS_HACK
// #define SHOW_SLIDER_GRID_BACK_COLOR
// #define SHOW_REMARKS_BACK_COLOR
// #define USE_SHAPE_VIEW_FOR_SLIDER
// #define SHOW_THUMB_CANVAS_BACKGROUND
// #define DEFEAT_SETTING_CURRENT_VALUE

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Interfaces;
   using Common.Utils;
   using SharedUtils.Utils;
   using System;
   using System.Diagnostics;
   using Xamarin.Forms;
   using Xamarin.Forms.PancakeView;

   /// <summary>
   /// Interface INumericSlider
   /// Implements the <see cref="IValidatableView" />
   /// </summary>
   /// <seealso cref="IValidatableView" />
   public interface INumericSlider : IValidatableView
   {
      /// <summary>
      /// Gets or sets the current value.
      /// </summary>
      /// <value>The current value.</value>
      double CurrentValue { get; set; }

      /// <summary>
      /// Gets the thumb.
      /// </summary>
      /// <value>The thumb.</value>
      ShapeView Thumb { get; }
   }

   /// <summary>
   /// Class NumericSlider.
   /// Implements the <see cref="ValidatableViewBase" />
   /// Implements the <see cref="INumericSlider" />
   /// </summary>
   /// <seealso cref="ValidatableViewBase" />
   /// <seealso cref="INumericSlider" />
   /// <remarks>This class is not described as validatable because it performs its own self-validation.
   /// It is also never out-of-range.</remarks>
   public class NumericSlider : ValidatableViewBase, INumericSlider
   {
      /// <summary>
      /// The current value property
      /// </summary>
      public static readonly BindableProperty CurrentValueProperty =
         ValidatableNumericSliderBindableProperty
         (
            nameof(CurrentValue),
            default(double),
            BindingMode.TwoWay,
            (view, oldVal, newVal) => { view.VerifyAndSetCurrentValue(newVal); }
         );

      /// <summary>
      /// The margin side
      /// </summary>
      private const double MARGIN_SIDE = 5;

      /// <summary>
      /// The minimum possible width
      /// </summary>
      private const double MINIMUM_POSSIBLE_WIDTH = 200;

      /// <summary>
      /// The remarks height
      /// </summary>
      private const double REMARKS_HEIGHT = 20;

      /// <summary>
      /// The slider height
      /// </summary>
      private const double SLIDER_HEIGHT = 10;

      /// <summary>
      /// The thumb height
      /// </summary>
      private const double THUMB_HEIGHT = 30;

      /// <summary>
      /// The thumb width
      /// </summary>
      private const double THUMB_WIDTH = 45;

      /// <summary>
      /// The end color
      /// </summary>
      private readonly Color _endColor;

      /// <summary>
      /// The ending remarks
      /// </summary>
      private readonly string _endingRemarks;

      /// <summary>
      /// The font family
      /// </summary>
      private readonly string _fontFamily;

      /// <summary>
      /// The maximum value
      /// </summary>
      private readonly double _maxValue;

      /// <summary>
      /// The minimum value
      /// </summary>
      private readonly double _minValue;

      /// <summary>
      /// The number format
      /// </summary>
      private readonly string _numberFormat;

      /// <summary>
      /// The remarks font attributes
      /// </summary>
      private readonly FontAttributes _remarksFontAttributes;

      /// <summary>
      /// The remarks font size
      /// </summary>
      private readonly double _remarksFontSize;

      /// <summary>
      /// The remarks text color
      /// </summary>
      private readonly Color _remarksTextColor;

      /// <summary>
      /// The start color
      /// </summary>
      private readonly Color _startColor;

      /// <summary>
      /// The starting remarks
      /// </summary>
      private readonly string _startingRemarks;

      /// <summary>
      /// The step
      /// </summary>
      private readonly double _step;

      /// <summary>
      /// The thumb color
      /// </summary>
      private readonly Color _thumbColor;

      /// <summary>
      /// The thumb font attributes
      /// </summary>
      private readonly FontAttributes _thumbFontAttributes;

      /// <summary>
      /// The thumb font size
      /// </summary>
      private readonly double _thumbFontSize;

      /// <summary>
      /// The thumb text color
      /// </summary>
      private readonly Color _thumbTextColor;

      /// <summary>
      /// The go to next step entered
      /// </summary>
      private volatile bool _goToNextStepEntered;

      /// <summary>
      /// The handle thumb movement entered
      /// </summary>
      private volatile bool _handleThumbMovementEntered;

      /// <summary>
      /// The last thumb position
      /// </summary>
      private double _lastThumbPosition;

      /// <summary>
      /// The last value
      /// </summary>
      private double _lastVal;

      /// <summary>
      /// The last width change
      /// </summary>
      private DateTime _lastWidthChange;

      /// <summary>
      /// The slider
      /// </summary>
      private PancakeView _slider;

      /// <summary>
      /// The slider grid
      /// </summary>
      private ResizableGrid _sliderGrid;

      /// <summary>
      /// The thumb canvas
      /// </summary>
      private AbsoluteLayout _thumbCanvas;

      /// <summary>
      /// The thumb x
      /// </summary>
      private double _thumbX;

      /// <summary>
      /// The verify legal current value entered
      /// </summary>
      private volatile bool _verifyLegalCurrentValueEntered;

      /// <summary>
      /// Initializes a new instance of the <see cref="NumericSlider" /> class.
      /// </summary>
      /// <param name="endColor">The end color.</param>
      /// <param name="endingRemarks">The ending remarks.</param>
      /// <param name="fontFamily">The font family.</param>
      /// <param name="maxValue">The maximum value.</param>
      /// <param name="minValue">The minimum value.</param>
      /// <param name="numberFormat">The number format.</param>
      /// <param name="remarksFontAttributes">The remarks font attributes.</param>
      /// <param name="remarksFontSize">Size of the remarks font.</param>
      /// <param name="remarksTextColor">Color of the remarks text.</param>
      /// <param name="startColor">The start color.</param>
      /// <param name="startingRemarks">The starting remarks.</param>
      /// <param name="step">The step.</param>
      /// <param name="thumbColor">Color of the thumb.</param>
      /// <param name="thumbFontAttributes">The thumb font attributes.</param>
      /// <param name="thumbFontSize">Size of the thumb font.</param>
      /// <param name="thumbTextColor">Color of the thumb text.</param>
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
      public NumericSlider
      (
         Color endColor,
         string endingRemarks,
         string fontFamily,
         double maxValue,
         double minValue,
         string numberFormat,
         FontAttributes remarksFontAttributes,
         double remarksFontSize,
         Color remarksTextColor,
         Color startColor,
         string startingRemarks,
         double step,
         Color thumbColor,
         FontAttributes thumbFontAttributes,
         double thumbFontSize,
         Color thumbTextColor,
         double? borderViewHeight = BORDER_VIEW_HEIGHT,
         BindingMode bindingMode = BindingMode.TwoWay,
         IValueConverter converter = null,
         object converterParameter = null,
         string fontFamilyOverride = "",
         string instructions = "",
         double? instructionsHeight = INSTRUCTIONS_HEIGHT,
         string placeholder = "",
         double? placeholderHeight = PLACEHOLDER_HEIGHT,
         bool showInstructionsOrValidations = false,
         bool showValidationErrorsAsInstructions = true,
         string stringFormat = null,
         ICanBeValid validator = null,
         string viewModelPropertyName = ""
      )
         : base
         (
            CurrentValueProperty,
            borderViewHeight,
            bindingMode,
            converter,
            converterParameter,
            fontFamilyOverride,
            instructions,
            instructionsHeight,
            placeholder,
            placeholderHeight,
            showInstructionsOrValidations,
            showValidationErrorsAsInstructions,
            stringFormat,
            validator,
            viewModelPropertyName
         )
      {
         _endColor = endColor;
         _endingRemarks = endingRemarks;
         _fontFamily = fontFamily;
         _maxValue = maxValue;
         _minValue = minValue;
         _numberFormat = numberFormat;
         _remarksFontAttributes = remarksFontAttributes;
         _remarksFontSize = remarksFontSize;
         _remarksTextColor = remarksTextColor;
         _startColor = startColor;
         _startingRemarks = startingRemarks;
         _step = step;
         _thumbColor = thumbColor;
         _thumbFontAttributes = thumbFontAttributes;
         _thumbFontSize = thumbFontSize;
         _thumbTextColor = thumbTextColor;

         //Padding = SLIDER_PADDING;

         ErrorUtils.ConsiderArgumentError(!maxValue.IsGreaterThan(minValue),
                                          nameof(NumericSlider) +
                                          ": The minimum values must be less than the maximum value");

         CallCreateViews();
      }

      /// <summary>
      /// Gets or sets the current value.
      /// </summary>
      /// <value>The current value.</value>
      public double CurrentValue
      {
         get => (double)GetValue(CurrentValueProperty);
         set => SetValue(CurrentValueProperty, value);
      }

      /// <summary>
      /// Gets the editable numeric slider.
      /// </summary>
      /// <value>The editable numeric slider.</value>
      public ResizableGrid EditableNumericSlider
      {
         get
         {
            if (_sliderGrid.IsNullOrDefault())
            {
               _sliderGrid = new ResizableGrid
               {
                  BackgroundColor = Color.Transparent,
                  VerticalOptions = LayoutOptions.CenterAndExpand,
                  HorizontalOptions = LayoutOptions.FillAndExpand,
                  InputTransparent = false
               };

               _sliderGrid.GestureRecognizers.Clear();
               _sliderGrid.GestureRecognizers.Add(new TapGestureRecognizer());

               _sliderGrid.AddFixedRow(MARGIN_SIDE);
               _sliderGrid.AddFixedRow(THUMB_HEIGHT);
               _sliderGrid.AddStarRow();

               _sliderGrid.AddFixedColumn(MARGIN_SIDE);
               _sliderGrid.AddStarColumn(0.5);
               _sliderGrid.AddStarColumn(0.5);
               _sliderGrid.AddFixedColumn(MARGIN_SIDE);

               _slider = new PancakeView
               {
                  // LOST THIS WHEN PANCAKE VIEW 2.0 CAME OUT
                  //BackgroundGradientStartColor = _startColor,
                  //BackgroundGradientEndColor   = _endColor,
                  //BackgroundGradientAngle      = 270,
                  CornerRadius = 3,
                  Sides = 4,
                  HeightRequest = SLIDER_HEIGHT,
                  VerticalOptions = LayoutOptions.Center,
                  HorizontalOptions = LayoutOptions.FillAndExpand,
                  InputTransparent = false
               };

               _slider.GestureRecognizers.Clear();
               _slider.GestureRecognizers.Add(new TapGestureRecognizer());

               Thumb = FormsUtils.GetShapeView();

               Thumb.Color = _thumbColor;

               var thumbLabel =
                  FormsUtils.GetSimpleLabel
                  (
                     "",
                     _thumbTextColor,
                     TextAlignment.Center,
                     fontSize: _thumbFontSize,
                     fontAttributes: _thumbFontAttributes,
                     width: THUMB_WIDTH,
                     height: THUMB_HEIGHT,
                     labelBindingPropertyName: nameof(CurrentValue),
                     labelBindingSource: this,
                     stringFormat: _numberFormat,
                     breakMode: LineBreakMode.NoWrap,
                     fontFamilyOverride: _fontFamily
                  );

               thumbLabel.BackgroundColor = Color.Black;

               Thumb.Content = thumbLabel;

               var panGesture = new PanGestureRecognizer();
               panGesture.PanUpdated += HandleThumbMovement;
               Thumb.GestureRecognizers.Add(panGesture);

               _thumbCanvas = FormsUtils.GetExpandingAbsoluteLayout();
               _thumbCanvas.InputTransparent = false;
               _thumbCanvas.GestureRecognizers.Clear();
               _thumbCanvas.GestureRecognizers.Add(new TapGestureRecognizer());
               _thumbCanvas.Children.Add(Thumb);

               _sliderGrid.AddAndSetRowsAndColumns(_slider, 1, 1, colSpan: 2);
               _sliderGrid.AddAndSetRowsAndColumns(_thumbCanvas, 1, 1, colSpan: 2);

               var startingRemarksLabel =
                  FormsUtils.GetSimpleLabel
                  (
                     _startingRemarks,
                     _remarksTextColor,
                     TextAlignment.Start,
                     fontSize: _remarksFontSize,
                     fontAttributes: _remarksFontAttributes,
                     breakMode: LineBreakMode.NoWrap,
                     fontFamilyOverride: _fontFamily,
                     height: REMARKS_HEIGHT,
                     width: MINIMUM_POSSIBLE_WIDTH
                  );

               startingRemarksLabel.VerticalTextAlignment = TextAlignment.Start;
               startingRemarksLabel.VerticalOptions = LayoutOptions.Start;
               _sliderGrid.AddAndSetRowsAndColumns(startingRemarksLabel, 2, 1);

               var endingRemarksLabel =
                  FormsUtils.GetSimpleLabel
                  (
                     _endingRemarks,
                     _remarksTextColor,
                     TextAlignment.End,
                     fontSize: _remarksFontSize,
                     fontAttributes: _remarksFontAttributes,
                     breakMode: LineBreakMode.NoWrap,
                     fontFamilyOverride: _fontFamily,
                     height: REMARKS_HEIGHT,
                     width: MINIMUM_POSSIBLE_WIDTH
                  );

               endingRemarksLabel.VerticalTextAlignment = TextAlignment.Start;
               endingRemarksLabel.VerticalOptions = LayoutOptions.Start;
               _sliderGrid.AddAndSetRowsAndColumns(endingRemarksLabel, 2, 2);
            }

            _sliderGrid.WidthChanged +=
               val =>
               {
                  if (val > MINIMUM_POSSIBLE_WIDTH && DateTime.Now - _lastWidthChange > TimeSpan.FromMilliseconds(333))
                  {
                     if (val.IsDifferentThan(_lastVal))
                     {
                        Debug.WriteLine("Slider grid width is now ->" + val + "<-");

                        // var canvasRect = new Rectangle(0, 0, val, CANVAS_HEIGHT);
                        _slider.WidthRequest = val -
                                               _sliderGrid.Padding.HorizontalThickness -
                                               _slider.Margin.HorizontalThickness;

                        // await _slider.LayoutTo()
                        _thumbCanvas.WidthRequest = val -
                                                    _sliderGrid.Padding.HorizontalThickness -
                                                    _thumbCanvas.Margin.HorizontalThickness;
                        _lastVal = val;
                     }
                  }

                  _lastWidthChange = DateTime.Now;
               };

            return _sliderGrid;
         }
      }

      /// <summary>
      /// Gets the thumb.
      /// </summary>
      /// <value>The thumb.</value>
      public ShapeView Thumb { get; private set; }

      /// <summary>
      /// Gets a value indicating whether [derived view is focused].
      /// </summary>
      /// <value><c>true</c> if [derived view is focused]; otherwise, <c>false</c>.</value>
      protected override bool DerivedViewIsFocused => false;

      /// <summary>
      /// Gets the editable view.
      /// </summary>
      /// <value>The editable view.</value>
      protected override View EditableView => EditableNumericSlider; // Thumb;

      /// <summary>
      /// Gets the editable view container.
      /// </summary>
      /// <value>The editable view container.</value>
      protected override View EditableViewContainer => EditableNumericSlider;

      // The slider forces the current value
      /// <summary>
      /// Gets a value indicating whether [user has entered valid content].
      /// </summary>
      /// <value><c>true</c> if [user has entered valid content]; otherwise, <c>false</c>.</value>
      protected override bool UserHasEnteredValidContent => true;

      /// <summary>
      /// Gets a step range.
      /// </summary>
      /// <value>a step range.</value>
      private double A_StepRange => _maxValue - _minValue + _step;

      /// <summary>
      /// Gets the b net range without thumb.
      /// </summary>
      /// <value>The b net range without thumb.</value>
      private double B_NetRangeWithoutThumb => _thumbCanvas.Bounds.Width - THUMB_WIDTH;

      /// <summary>
      /// Gets the c each step as pixels.
      /// </summary>
      /// <value>The c each step as pixels.</value>
      private double C_EachStepAsPixels => B_NetRangeWithoutThumb / A_StepRange;

      /// <summary>
      /// Validatables the numeric slider bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty ValidatableNumericSliderBindableProperty<PropertyTypeT>
      (
         string localPropName,
         PropertyTypeT defaultVal = default,
         BindingMode bindingMode = BindingMode.OneWay,
         Action<NumericSlider, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      /// Goes to next step.
      /// </summary>
      /// <param name="proposedTranslationX">The proposed translation x.</param>
      private void GoToNextStep(double proposedTranslationX)
      {
         if (_goToNextStepEntered)
         {
            return;
         }

         _goToNextStepEntered = true;

         if (proposedTranslationX.IsSameAs(Thumb.TranslationX) || _step.IsEmpty())
         {
            return;
         }

         var nextPercent = proposedTranslationX / B_NetRangeWithoutThumb;
         var nextRoughStep = nextPercent * A_StepRange;
         var truncatedStep = Math.Truncate(nextRoughStep);
         var partialStep = nextRoughStep - truncatedStep;

         var increasing = partialStep.IsGreaterThan(0);

         var nextStep =
            increasing
               ? Math.Min(_maxValue, truncatedStep + _step)
               : Math.Max(_step, truncatedStep - _step);

         VerifyAndSetCurrentValue(nextStep);

         SetThumbPositionBasedOnCurrentValue();

         _goToNextStepEntered = false;
      }

      /// <summary>
      /// Handles the thumb movement.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="PanUpdatedEventArgs" /> instance containing the event data.</param>
      private void HandleThumbMovement(object sender, PanUpdatedEventArgs e)
      {
         switch (e.StatusType)
         {
            case GestureStatus.Running:

               if (_handleThumbMovementEntered)
               {
                  return;
               }

               _handleThumbMovementEntered = true;

               // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
               GoToNextStep
               (
                  Math.Min(Math.Max(0, _thumbX + e.TotalX),
                           _thumbCanvas.Bounds.Width - THUMB_WIDTH)
               );

               _handleThumbMovementEntered = false;

               break;

            case GestureStatus.Completed:

               // Store the translation applied during the pan
               _thumbX = Thumb.TranslationX;

               break;
         }
      }

      /// <summary>
      /// Sets the thumb position based on current value.
      /// </summary>
      private void SetThumbPositionBasedOnCurrentValue()
      {
         var nextThumbPosition = CurrentValue * C_EachStepAsPixels;

         if (nextThumbPosition.IsDifferentThan(_lastThumbPosition))
         {
            Thumb.TranslationX = nextThumbPosition;

            _lastThumbPosition = nextThumbPosition;
         }
      }

      /// <summary>
      /// Verifies the and set current value.
      /// </summary>
      /// <param name="newValue">The new value.</param>
      private void VerifyAndSetCurrentValue(double newValue)
      {
         if (_verifyLegalCurrentValueEntered || newValue.IsSameAs(CurrentValue))
         {
            return;
         }

#if !DEFEAT_SETTING_CURRENT_VALUE

         _verifyLegalCurrentValueEntered = true;

         if (!CurrentValue.IsGreaterThanOrEqualTo(_minValue))
         {
            CurrentValue = _minValue;
         }
         else if (CurrentValue.IsGreaterThanOrEqualTo(_maxValue))
         {
            CurrentValue = _maxValue;
         }

         // ELSE
         CurrentValue = newValue;

         SetThumbPositionBasedOnCurrentValue();

         _verifyLegalCurrentValueEntered = false;

#endif
      }
   }
}
