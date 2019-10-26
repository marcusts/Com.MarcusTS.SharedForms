#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ValidatableSlider.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Interfaces;
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IValidatableSlider
   /// </summary>
   public interface IValidatableSlider
   {
      /// <summary>
      /// Gets the editable slider.
      /// </summary>
      /// <value>The editable slider.</value>
      Slider EditableSlider { get; }
      /// <summary>
      /// Gets the slider grid.
      /// </summary>
      /// <value>The slider grid.</value>
      Grid SliderGrid     { get; }
   }

   /// <summary>
   /// Class ValidatableSlider.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ValidatableViewBase" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableSlider" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ValidatableViewBase" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableSlider" />
   public class ValidatableSlider : ValidatableViewBase, IValidatableSlider
   {
      /// <summary>
      /// The margin side
      /// </summary>
      private const    double         MARGIN_SIDE    = 5;
      /// <summary>
      /// The remarks height
      /// </summary>
      private const    double         REMARKS_HEIGHT = 20;
      /// <summary>
      /// The slider height
      /// </summary>
      private const    double         SLIDER_HEIGHT  = 20;
      /// <summary>
      /// The ending remarks
      /// </summary>
      private readonly string         _endingRemarks;
      /// <summary>
      /// The font family
      /// </summary>
      private readonly string         _fontFamily;
      /// <summary>
      /// The maximum color
      /// </summary>
      private readonly Color          _maxColor;
      /// <summary>
      /// The maximum value
      /// </summary>
      private readonly double         _maxValue;
      /// <summary>
      /// The minimum color
      /// </summary>
      private readonly Color          _minColor;
      /// <summary>
      /// The minimum value
      /// </summary>
      private readonly double         _minValue;
      /// <summary>
      /// The remarks font attributes
      /// </summary>
      private readonly FontAttributes _remarksFontAttributes;
      /// <summary>
      /// The remarks font size
      /// </summary>
      private readonly double         _remarksFontSize;
      /// <summary>
      /// The remarks text color
      /// </summary>
      private readonly Color          _remarksTextColor;
      /// <summary>
      /// The starting remarks
      /// </summary>
      private readonly string         _startingRemarks;
      /// <summary>
      /// The step
      /// </summary>
      private readonly double         _step;
      /// <summary>
      /// The thumb color
      /// </summary>
      private readonly Color          _thumbColor;
      /// <summary>
      /// The editable slider
      /// </summary>
      private Slider         _editableSlider;
      /// <summary>
      /// The slider color
      /// </summary>
      private Color          _sliderColor;
      /// <summary>
      /// The slider grid
      /// </summary>
      private Grid           _sliderGrid;

      /// <summary>
      /// Initializes a new instance of the <see cref="ValidatableSlider"/> class.
      /// </summary>
      /// <param name="endingRemarks">The ending remarks.</param>
      /// <param name="fontFamily">The font family.</param>
      /// <param name="maxColor">The maximum color.</param>
      /// <param name="maxValue">The maximum value.</param>
      /// <param name="minColor">The minimum color.</param>
      /// <param name="minValue">The minimum value.</param>
      /// <param name="remarksFontAttributes">The remarks font attributes.</param>
      /// <param name="remarksFontSize">Size of the remarks font.</param>
      /// <param name="remarksTextColor">Color of the remarks text.</param>
      /// <param name="sliderColor">Color of the slider.</param>
      /// <param name="startingRemarks">The starting remarks.</param>
      /// <param name="step">The step.</param>
      /// <param name="thumbColor">Color of the thumb.</param>
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
      public ValidatableSlider
      (
         string          endingRemarks,
         string          fontFamily,
         Color?          maxColor,
         double          maxValue,
         Color?          minColor,
         double          minValue,
         FontAttributes  remarksFontAttributes,
         double?         remarksFontSize,
         Color?          remarksTextColor,
         Color?          sliderColor,
         string          startingRemarks,
         double          step,
         Color?          thumbColor,
         double?         borderViewHeight                   = BORDER_VIEW_HEIGHT,
         BindingMode     bindingMode                        = BindingMode.TwoWay,
         IValueConverter converter                          = null,
         object          converterParameter                 = null,
         string          fontFamilyOverride                 = "",
         string          instructions                       = "",
         double?         instructionsHeight                 = INSTRUCTIONS_HEIGHT,
         string          placeholder                        = "",
         double?         placeholderHeight                  = PLACEHOLDER_HEIGHT,
         bool            showInstructionsOrValidations      = false,
         bool            showValidationErrorsAsInstructions = true,
         string          stringFormat                       = null,
         ICanBeValid     validator                          = null,
         string          viewModelPropertyName              = ""
      )
         : base
         (
            Slider.ValueProperty,
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
         ErrorUtils.ConsiderArgumentError(!maxValue.IsGreaterThan(minValue),
                                          nameof(NumericSlider) +
                                          ": The minimum values must be less than the maximum value");

         // Counter-intuitive
         _maxColor = maxColor ?? Color.Green;

         _endingRemarks         = endingRemarks;
         _fontFamily            = fontFamily;
         _maxValue              = maxValue;
         _minValue              = minValue;
         _remarksFontAttributes = remarksFontAttributes;
         _remarksFontSize       = remarksFontSize  ?? Device.GetNamedSize(NamedSize.Small, typeof(Label));
         _remarksTextColor      = remarksTextColor ?? Color.DimGray;

         // Counter-intuitive
         _minColor = minColor ?? Color.Red;

         _sliderColor     = sliderColor ?? Color.Gray;
         _startingRemarks = startingRemarks;
         _step            = step;
         _thumbColor      = thumbColor ?? Color.Black;

         CallCreateViews();
      }

      /// <summary>
      /// Gets a value indicating whether [derived view is focused].
      /// </summary>
      /// <value><c>true</c> if [derived view is focused]; otherwise, <c>false</c>.</value>
      protected override bool DerivedViewIsFocused => false;

      /// <summary>
      /// Gets the editable view.
      /// </summary>
      /// <value>The editable view.</value>
      protected override View EditableView => EditableSlider;

      /// <summary>
      /// Gets the editable view container.
      /// </summary>
      /// <value>The editable view container.</value>
      protected override View EditableViewContainer => SliderGrid;

      /// <summary>
      /// Gets a value indicating whether [user has entered valid content].
      /// </summary>
      /// <value><c>true</c> if [user has entered valid content]; otherwise, <c>false</c>.</value>
      protected override bool UserHasEnteredValidContent => true;

      /// <summary>
      /// Gets the editable slider.
      /// </summary>
      /// <value>The editable slider.</value>
      public Slider EditableSlider
      {
         get
         {
            if (_editableSlider.IsNullOrDefault())
            {
               _editableSlider = new Slider
               {
                  BackgroundColor   = Color.Transparent,
                  InputTransparent  = false,
                  MaximumTrackColor = _maxColor,
                  MinimumTrackColor = _minColor,
                  Maximum           = _maxValue,
                  Minimum           = _minValue,
                  ThumbColor        = _thumbColor,
                  HorizontalOptions = LayoutOptions.FillAndExpand,
                  VerticalOptions   = LayoutOptions.End,
                  Margin            = new Thickness(-7.5, 0)
               };

               //_editableSlider.BindingContextChanged +=
               //   (sender, args) =>
               //   {
               //   };
            }

            return _editableSlider;
         }
      }

      /// <summary>
      /// Gets the slider grid.
      /// </summary>
      /// <value>The slider grid.</value>
      public Grid SliderGrid
      {
         get
         {
            if (_sliderGrid.IsNullOrDefault())
            {
               _sliderGrid = new Grid
               {
                  BackgroundColor   = Color.Transparent,
                  VerticalOptions   = LayoutOptions.CenterAndExpand,
                  HorizontalOptions = LayoutOptions.FillAndExpand,
                  InputTransparent  = false
               };

               _sliderGrid.GestureRecognizers.Clear();
               _sliderGrid.GestureRecognizers.Add(new TapGestureRecognizer());

               _sliderGrid.AddFixedRow(MARGIN_SIDE);
               _sliderGrid.AddFixedRow(SLIDER_HEIGHT);
               _sliderGrid.AddFixedRow(REMARKS_HEIGHT);
               _sliderGrid.AddStarRow();

               _sliderGrid.AddFixedColumn(MARGIN_SIDE);
               _sliderGrid.AddStarColumn(0.5);
               _sliderGrid.AddFixedColumn(MARGIN_SIDE);

               _sliderGrid.AddAndSetRowsAndColumns(EditableSlider, 1, 1);

               var startingRemarksLabel =
                  FormsUtils.GetSimpleLabel
                  (
                     _startingRemarks,
                     _remarksTextColor,
                     TextAlignment.Start,
                     fontSize: _remarksFontSize,
                     fontAttributes: _remarksFontAttributes,
                     breakMode: LineBreakMode.NoWrap,
                     fontFamilyOverride: _fontFamily
                  );

               startingRemarksLabel.VerticalTextAlignment = TextAlignment.Start;
               startingRemarksLabel.VerticalOptions       = LayoutOptions.Start;
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
                     fontFamilyOverride: _fontFamily
                  );

               endingRemarksLabel.VerticalTextAlignment = TextAlignment.Start;
               endingRemarksLabel.VerticalOptions       = LayoutOptions.Start;
               _sliderGrid.AddAndSetRowsAndColumns(endingRemarksLabel, 2, 1);
            }

            return _sliderGrid;
         }
      }
   }
}