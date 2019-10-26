#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, MeasurableStylableShapeView.cs, is a part of a program called AccountViewMobile.
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
   using System;
   using System.ComponentModel;
   using Xamarin.Forms;

   /// <summary>
   /// Determines if a row or column is the 1st, 3rd, 5th, etc. -- an "odd" numbered row/column. MUST BE RESET each time
   /// the view model list is reorganized (for rows) or if the collumns are ever re-ordered.
   /// </summary>
   public interface ICanMeasure
   {
      /// <summary>
      /// Gets or sets a value indicating whether [automatic calculate height].
      /// </summary>
      /// <value><c>true</c> if [automatic calculate height]; otherwise, <c>false</c>.</value>
      bool AutoCalcHeight   { get; set; }
      /// <summary>
      /// Gets or sets a value indicating whether [automatic calculate width].
      /// </summary>
      /// <value><c>true</c> if [automatic calculate width]; otherwise, <c>false</c>.</value>
      bool AutoCalcWidth    { get; set; }
      /// <summary>
      /// Gets or sets the height of the fixed.
      /// </summary>
      /// <value>The height of the fixed.</value>
      double FixedHeight      { get; set; }
      /// <summary>
      /// Gets or sets the width of the fixed.
      /// </summary>
      /// <value>The width of the fixed.</value>
      double FixedWidth       { get; set; }
      /// <summary>
      /// Gets or sets the maximum height of the measure.
      /// </summary>
      /// <value>The maximum height of the measure.</value>
      double MaxMeasureHeight { get; set; }
      /// <summary>
      /// Gets or sets the maximum width of the measure.
      /// </summary>
      /// <value>The maximum width of the measure.</value>
      double MaxMeasureWidth  { get; set; }
      /// <summary>
      /// Gets or sets the minimum height of the measure.
      /// </summary>
      /// <value>The minimum height of the measure.</value>
      double MinMeasureHeight { get; set; }
      /// <summary>
      /// Gets or sets the minimum width of the measure.
      /// </summary>
      /// <value>The minimum width of the measure.</value>
      double MinMeasureWidth  { get; set; }

      /// <summary>
      /// Occurs when [measured content size changed].
      /// </summary>
      event EventUtils.GenericDelegate<Size> MeasuredContentSizeChanged;

      /// <summary>
      /// Remeasures the size.
      /// </summary>
      void RemeasureSize();
   }

   /// <summary>
   /// Interface IHaveMeasuredContentSize
   /// </summary>
   public interface IHaveMeasuredContentSize
   {
      /// <summary>
      /// Gets or sets the size of the measured content.
      /// </summary>
      /// <value>The size of the measured content.</value>
      Size MeasuredContentSize { get; set; }
   }

   /// <summary>
   /// Interface IMeasurableStylableShapeView
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ICanMeasure" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Interfaces.IHaveCurrentMeasuredContentSizeRequester" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IHaveSelectionStylingHelper" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ICanAlternate" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ICanBeSelected" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IHaveMeasuredContentSize" />
   /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ICanMeasure" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Interfaces.IHaveCurrentMeasuredContentSizeRequester" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IHaveSelectionStylingHelper" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ICanAlternate" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ICanBeSelected" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IHaveMeasuredContentSize" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IMeasurableStylableShapeView :
      ICanMeasure,
      IHaveCurrentMeasuredContentSizeRequester,
      IHaveSelectionStylingHelper,
      ICanAlternate,
      ICanBeSelected,
      IHaveMeasuredContentSize,
      INotifyPropertyChanged
   {
   }

   /// <summary>
   /// Class MeasurableStylableShapeView.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.ShapeView" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IMeasurableStylableShapeView" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.ShapeView" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IMeasurableStylableShapeView" />
   public class MeasurableStylableShapeView : ShapeView, IMeasurableStylableShapeView
   {
      /// <summary>
      /// The automatic calculate height property
      /// </summary>
      public static readonly BindableProperty AutoCalcHeightProperty =
         CreateMeasurableShapeViewProperty
         (
            nameof(AutoCalcHeight),
            default(bool),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.AutoCalcHeight = newVal;
            }
         );

      /// <summary>
      /// The automatic calculate width property
      /// </summary>
      public static readonly BindableProperty AutoCalcWidthProperty =
         CreateMeasurableShapeViewProperty
         (
            nameof(AutoCalcWidth),
            default(bool),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.AutoCalcWidth = newVal;
            }
         );

      /// <summary>
      /// The fixed height property
      /// </summary>
      public static readonly BindableProperty FixedHeightProperty =
         CreateMeasurableShapeViewProperty
         (
            nameof(FixedHeight),
            default(double),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.FixedHeight = newVal;
            }
         );

      /// <summary>
      /// The fixed width property
      /// </summary>
      public static readonly BindableProperty FixedWidthProperty =
         CreateMeasurableShapeViewProperty
         (
            nameof(FixedWidth),
            default(double),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.FixedWidth = newVal;
            }
         );

      /// <summary>
      /// The is selected property
      /// </summary>
      public static readonly BindableProperty IsSelectedProperty =
         CreateMeasurableShapeViewProperty
         (
            nameof(IsSelected),
            default(bool),
            BindingMode.OneWay,
            (shapeView, oldVal, newVal) => { shapeView.IsSelected = newVal; }
         );

      /// <summary>
      /// The maximum measure height property
      /// </summary>
      public static readonly BindableProperty MaxMeasureHeightProperty =
         CreateMeasurableShapeViewProperty
         (
            nameof(MaxMeasureHeight),
            default(double),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.MaxMeasureHeight = newVal;
            }
         );

      /// <summary>
      /// The maximum measure width property
      /// </summary>
      public static readonly BindableProperty MaxMeasureWidthProperty =
         CreateMeasurableShapeViewProperty
         (
            nameof(MaxMeasureWidth),
            default(double),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.MaxMeasureWidth = newVal;
            }
         );

      /// <summary>
      /// The minimum measure height property
      /// </summary>
      public static readonly BindableProperty MinMeasureHeightProperty =
         CreateMeasurableShapeViewProperty
         (
            nameof(MinMeasureHeight),
            default(double),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.MinMeasureHeight = newVal;
            }
         );

      /// <summary>
      /// The minimum measure width property
      /// </summary>
      public static readonly BindableProperty MinMeasureWidthProperty =
         CreateMeasurableShapeViewProperty
         (
            nameof(MinMeasureWidth),
            default(double),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.MinMeasureWidth = newVal;
            }
         );

      /// <summary>
      /// The selection and alternating style helper property
      /// </summary>
      public static readonly BindableProperty SelectionAndAlternatingStyleHelperProperty =
         CreateMeasurableShapeViewProperty
         (
            nameof(StylingHelper),
            default(ISelectionStylingHelper),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.StylingHelper = newVal;
            }
         );

      /// <summary>
      /// The am an alternate
      /// </summary>
      private bool                               _amAnAlternate;
      /// <summary>
      /// The current measured content size requester
      /// </summary>
      private IRequestCurrentMeasuredContentSize _currentMeasuredContentSizeRequester;
      /// <summary>
      /// The fixed height
      /// </summary>
      private double                             _fixedHeight;
      /// <summary>
      /// The fixed width
      /// </summary>
      private double                             _fixedWidth;
      /// <summary>
      /// The is selected
      /// </summary>
      private bool                               _isSelected;
      /// <summary>
      /// The maximum measure height
      /// </summary>
      private double                             _maxMeasureHeight;
      /// <summary>
      /// The maximum measure width
      /// </summary>
      private double                             _maxMeasureWidth;
      /// <summary>
      /// The measured content size
      /// </summary>
      private Size                               _measuredContentSize;
      /// <summary>
      /// The minimum measure height
      /// </summary>
      private double                             _minMeasureHeight;
      /// <summary>
      /// The minimum measure width
      /// </summary>
      private double                             _minMeasureWidth;

      /// <summary>
      /// The styling helper
      /// </summary>
      private ISelectionStylingHelper _stylingHelper;

      /// <summary>
      /// Initializes a new instance of the <see cref="MeasurableStylableShapeView"/> class.
      /// </summary>
      public MeasurableStylableShapeView()
      {
         PropertyChanged +=
            (sender, args) =>
            {
               if (args.PropertyName.IsSameAs(nameof(Content)) && (AutoCalcWidth || AutoCalcHeight))
               {
                  RemeasureSize();
               }
               else
               {
                  if ((args.PropertyName.IsSameAs(nameof(Width)) || args.PropertyName.IsSameAs(nameof(Height))) &&
                      MeasuredContentSize.IsEmpty())
                  {
                     MeasuredContentSize = new Size(Width, Height);
                  }
               }
            };
      }

      /// <summary>
      /// Occurs when [is alternate changed].
      /// </summary>
      public event EventUtils.GenericDelegate<ICanAlternate> IsAlternateChanged;

      /// <summary>
      /// Occurs when [is selected changed].
      /// </summary>
      public event EventUtils.GenericDelegate<ICanBeSelected> IsSelectedChanged;

      /// <summary>
      /// Occurs when [measured content size changed].
      /// </summary>
      public event EventUtils.GenericDelegate<Size> MeasuredContentSizeChanged;

      /// <summary>
      /// Gets or sets a value indicating whether [automatic calculate height].
      /// </summary>
      /// <value><c>true</c> if [automatic calculate height]; otherwise, <c>false</c>.</value>
      public bool AutoCalcHeight { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [automatic calculate width].
      /// </summary>
      /// <value><c>true</c> if [automatic calculate width]; otherwise, <c>false</c>.</value>
      public bool AutoCalcWidth { get; set; }

      /// <summary>
      /// Gets or sets the current measured content size requester.
      /// </summary>
      /// <value>The current measured content size requester.</value>
      public IRequestCurrentMeasuredContentSize CurrentMeasuredContentSizeRequester
      {
         get => _currentMeasuredContentSizeRequester;
         set
         {
            if (_currentMeasuredContentSizeRequester.IsNotNullOrDefault())
            {
               _currentMeasuredContentSizeRequester.RequestCurrentMeasuredContentSize -=
                  HandleRequestCurrentMeasuredContentSize;
            }

            _currentMeasuredContentSizeRequester = value;

            if (_currentMeasuredContentSizeRequester.IsNotNullOrDefault())
            {
               _currentMeasuredContentSizeRequester.RequestCurrentMeasuredContentSize +=
                  HandleRequestCurrentMeasuredContentSize;
            }
         }
      }

      /// <summary>
      /// Gets or sets the height of the fixed.
      /// </summary>
      /// <value>The height of the fixed.</value>
      public double FixedHeight
      {
         get => _fixedHeight;
         set
         {
            if (value.IsNotEmpty() && _fixedHeight.IsDifferentThan(value))
            {
               _fixedHeight        = value;
               MeasuredContentSize = new Size(MeasuredContentSize.Width, _fixedHeight);
            }
         }
      }

      /// <summary>
      /// Gets or sets the width of the fixed.
      /// </summary>
      /// <value>The width of the fixed.</value>
      public double FixedWidth
      {
         get => _fixedWidth;
         set
         {
            if (value.IsNotEmpty() && _fixedWidth.IsDifferentThan(value))
            {
               _fixedWidth = value;

               MeasuredContentSize = new Size(_fixedWidth, MeasuredContentSize.Height);
            }
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is an alternate.
      /// </summary>
      /// <value><c>true</c> if this instance is an alternate; otherwise, <c>false</c>.</value>
      public bool IsAnAlternate
      {
         get => _amAnAlternate;
         set
         {
            if (_amAnAlternate != value)
            {
               _amAnAlternate = value;
               IsAlternateChanged?.Invoke(this);

               // ResetStyle();
            }
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is selected.
      /// </summary>
      /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
      public bool IsSelected
      {
         get => _isSelected;
         set
         {
            if (_isSelected != value)
            {
               // Deriver can override
               if (CanSelectionBeMade(_isSelected))
               {
                  _isSelected = value;

                  // The styling helper applies its styles on this view externally

                  AfterSelectionChange(this);
                  IsSelectedChanged?.Invoke(this);
               }
            }
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is trying to be selected.
      /// </summary>
      /// <value><c>true</c> if this instance is trying to be selected; otherwise, <c>false</c>.</value>
      public bool IsTryingToBeSelected { get; set; }

      /// <summary>
      /// Gets or sets the maximum height of the measure.
      /// </summary>
      /// <value>The maximum height of the measure.</value>
      public double MaxMeasureHeight
      {
         get => _maxMeasureHeight;
         set
         {
            if (_maxMeasureHeight.IsDifferentThan(value))
            {
               _maxMeasureHeight = value;

               // Will result in zero, so not ideal.
               if (MinMeasureHeight > _maxMeasureHeight)
               {
                  MinMeasureHeight = _maxMeasureHeight;
               }

               RemeasureSize();
            }
         }
      }

      /// <summary>
      /// Gets or sets the maximum width of the measure.
      /// </summary>
      /// <value>The maximum width of the measure.</value>
      public double MaxMeasureWidth
      {
         get => _maxMeasureWidth;
         set
         {
            if (_maxMeasureWidth.IsDifferentThan(value))
            {
               _maxMeasureWidth = value;

               // Will result in zero, so not ideal.
               if (MinMeasureWidth > _maxMeasureWidth)
               {
                  MinMeasureWidth = _maxMeasureWidth;
               }

               RemeasureSize();
            }
         }
      }

      /// <summary>
      /// Gets or sets the size of the measured content.
      /// </summary>
      /// <value>The size of the measured content.</value>
      public Size MeasuredContentSize
      {
         get => _measuredContentSize;
         set
         {
            if (_measuredContentSize.IsDifferentThan(value))
            {
               _measuredContentSize = value;

               WidthRequest  = _measuredContentSize.Width;
               HeightRequest = _measuredContentSize.Height;

               OnMeasuredContentSizeChanged(_measuredContentSize);
               MeasuredContentSizeChanged?.Invoke(_measuredContentSize);
            }
         }
      }

      /// <summary>
      /// Gets or sets the minimum height of the measure.
      /// </summary>
      /// <value>The minimum height of the measure.</value>
      public double MinMeasureHeight
      {
         get => _minMeasureHeight;
         set
         {
            if (_minMeasureHeight.IsDifferentThan(value))
            {
               _minMeasureHeight = value;

               // Will result in zero, so not ideal.
               if (MaxMeasureHeight < _minMeasureHeight)
               {
                  MaxMeasureHeight = _minMeasureHeight;
               }

               RemeasureSize();
            }
         }
      }

      /// <summary>
      /// Gets or sets the minimum width of the measure.
      /// </summary>
      /// <value>The minimum width of the measure.</value>
      public double MinMeasureWidth
      {
         get => _minMeasureWidth;
         set
         {
            if (_minMeasureWidth.IsDifferentThan(value))
            {
               _minMeasureWidth = value;

               // Will result in zero, so not ideal.
               if (MaxMeasureWidth < _minMeasureWidth)
               {
                  MaxMeasureWidth = _minMeasureWidth;
               }

               RemeasureSize();
            }
         }
      }

      /// <summary>
      /// Gets or sets the styling helper.
      /// </summary>
      /// <value>The styling helper.</value>
      public ISelectionStylingHelper StylingHelper
      {
         get => _stylingHelper;
         set => _stylingHelper = value.CanAttach(this) ? value : default;
      }

      /// <summary>
      /// Afters the selection change.
      /// </summary>
      /// <param name="item">The item.</param>
      public virtual void AfterSelectionChange(ICanBeSelected item)
      {
      }

      /// <summary>
      /// Afters the style applied.
      /// </summary>
      public virtual void AfterStyleApplied()
      {
         RemeasureSize();
      }

      /// <summary>
      /// Determines whether this instance [can selection be made] the specified is selected.
      /// </summary>
      /// <param name="isSelected">if set to <c>true</c> [is selected].</param>
      /// <returns><c>true</c> if this instance [can selection be made] the specified is selected; otherwise, <c>false</c>.</returns>
      public virtual bool CanSelectionBeMade(bool isSelected)
      {
         return true;
      }

      /// <summary>
      /// Remeasures the size.
      /// </summary>
      public virtual void RemeasureSize()
      {
         if (Content.IsNullOrDefault())
         {
            MeasuredContentSize = default;
            return;
         }

         if (!AutoCalcHeight && !AutoCalcWidth)
         {
            if (FixedWidth.IsNotEmpty())
            {
               MeasuredContentSize = new Size(FixedWidth, MeasuredContentSize.Height);
            }

            if (FixedHeight.IsNotEmpty())
            {
               MeasuredContentSize = new Size(MeasuredContentSize.Width, FixedHeight);
            }

            return;
         }

         var measuredSize = Content.Measure(MaxMeasureWidth, MaxMeasureHeight);
         MeasuredContentSize = measuredSize.Minimum;
      }

      /// <summary>
      /// Creates the measurable shape view property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateMeasurableShapeViewProperty<PropertyTypeT>
      (
         string localPropName,
         PropertyTypeT defaultVal =
            default,
         BindingMode bindingMode =
            BindingMode.OneWay,
         Action<MeasurableStylableShapeView, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      /// Creates the measurable shape view style.
      /// </summary>
      /// <param name="backColor">Color of the back.</param>
      /// <param name="BorderThickness">The border thickness.</param>
      /// <param name="borderColor">Color of the border.</param>
      /// <param name="cornerRadius">The corner radius.</param>
      /// <returns>Style.</returns>
      public static Style CreateMeasurableShapeViewStyle
      (
         Color? backColor       = default,
         double BorderThickness = 0,
         Color? borderColor     = default,
         double cornerRadius    = 0
      )
      {
         var retStyle = new Style(typeof(MeasurableStylableShapeView));

         if (backColor.HasValue)
         {
            retStyle.Setters.Add(BackgroundColorProperty, backColor.GetValueOrDefault());
         }
         else
         {
            retStyle.Setters.Add(BackgroundColorProperty, Color.Transparent);
         }

         if (BorderThickness.IsLessThanOrEqualTo(0))
         {
            retStyle.Setters.Add(BorderThicknessProperty, 0);
         }
         else
         {
            retStyle.Setters.Add(BorderThicknessProperty, BorderThickness);
         }

         if (borderColor.HasValue)
         {
            retStyle.Setters.Add(BorderColorProperty, borderColor.GetValueOrDefault());
         }
         else
         {
            retStyle.Setters.Add(BorderColorProperty, Color.Transparent);
         }

         if (cornerRadius.IsLessThanOrEqualTo(0))
         {
            retStyle.Setters.Add(CornerRadiusProperty, 0);
         }
         else
         {
            retStyle.Setters.Add(CornerRadiusProperty, cornerRadius);
         }

         return retStyle;
      }

      /// <summary>
      /// Called when [measured content size changed].
      /// </summary>
      /// <param name="measuredContentSize">Size of the measured content.</param>
      protected virtual void OnMeasuredContentSizeChanged(Size measuredContentSize)
      {
      }

      /// <summary>
      /// Handles the size of the request current measured content.
      /// </summary>
      private void HandleRequestCurrentMeasuredContentSize()
      {
         RemeasureSize();
      }
   }
}