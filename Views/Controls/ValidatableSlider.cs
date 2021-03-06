﻿// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=ValidatableSlider.cs
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

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   public interface IValidatableSlider
   {
      Slider EditableSlider { get; }
      Grid   SliderGrid     { get; }
   }

   public class ValidatableSlider : ValidatableViewBase, IValidatableSlider
   {
      private const    double         MARGIN_SIDE    = 5;
      private const    double         REMARKS_HEIGHT = 20;
      private const    double         SLIDER_HEIGHT  = 20;
      private readonly string         _endingRemarks;
      private readonly Color          _maxColor;
      private readonly double         _maxValue;
      private readonly Color          _minColor;
      private readonly double         _minValue;
      private readonly FontAttributes _remarksFontAttributes;
      private readonly double         _remarksFontSize;
      private readonly Color          _remarksTextColor;
      private readonly string         _startingRemarks;
      private readonly double         _step;
      private readonly Color          _thumbColor;
      private          Slider         _editableSlider;
      private          Color          _sliderColor;
      private          Grid           _sliderGrid;

      public ValidatableSlider
      (
         string         endingRemarks,
         Color?         maxColor,
         double         maxValue,
         Color?         minColor,
         double         minValue,
         FontAttributes remarksFontAttributes,
         double?        remarksFontSize,
         Color?         remarksTextColor,
         Color?         sliderColor,
         string         startingRemarks,
         double         step,
         Color?         thumbColor,
         bool           asleepInitially = false
      )
      : base(Slider.ValueProperty, asleepInitially: asleepInitially)
      {
         ErrorUtils.IssueArgumentErrorIfTrue(!maxValue.IsGreaterThan(minValue),
            nameof(ValidatableSlider) +
            ": The minimum values must be less than the maximum value");

         // Counter-intuitive
         _maxColor = maxColor ?? Color.Green;

         _endingRemarks         = endingRemarks;
         _maxValue              = maxValue;
         _minValue              = minValue;
         _remarksFontAttributes = remarksFontAttributes;
         _remarksFontSize       = remarksFontSize ?? Device.GetNamedSize(NamedSize.Small, typeof(Label));
         _remarksTextColor      = remarksTextColor ?? Color.DimGray;

         // Counter-intuitive
         _minColor = minColor ?? Color.Red;

         _sliderColor     = sliderColor ?? Color.Gray;
         _startingRemarks = startingRemarks;
         _step            = step;
         _thumbColor      = thumbColor ?? Color.Black;

         if (!IsConstructing)
         {
            RecreateAllViewsBindingsAndStyles();
         }
      }

      protected override bool DerivedViewIsFocused => false;

      protected override View EditableView => EditableSlider;

      protected override View EditableViewContainer => SliderGrid;

      protected override bool UserHasEnteredValidContent => true;

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
                     breakMode: LineBreakMode.NoWrap
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
                     breakMode: LineBreakMode.NoWrap
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