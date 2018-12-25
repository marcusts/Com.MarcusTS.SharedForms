// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="ShapeView.cs" company="Marcus Technical Services, Inc.">
//     Copyright @2018 Marcus Technical Services, Inc.
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
   using System.Collections.ObjectModel;
   using Xamarin.Forms;

   /// <summary>
   /// This class allows you to draw different kind of shapes in your Xamarin.Forms PCL
   /// Implements the <see cref="Xamarin.Forms.ContentView" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.ContentView" />
   public class ShapeView : ContentView
   {
      /// <summary>
      /// Gets or sets the shape type - default value is ShapeType.Box
      /// </summary>
      /// <value>The type of the shape.</value>
      public ShapeType ShapeType
      {
         get => (ShapeType) GetValue(ShapeTypeProperty);
         set => SetValue(ShapeTypeProperty, value);
      }

      /// <summary>
      /// Gets or sets the fill color - default value is Color.Default
      /// </summary>
      /// <value>The color.</value>
      public Color Color
      {
         get => (Color) GetValue(ColorProperty);
         set => SetValue(ColorProperty, value);
      }

      /// <summary>
      /// Gets or sets the border color (ignored if fully transparent or BorderColor &lt;= 0) - default value is Color.Black
      /// </summary>
      /// <value>The color of the border.</value>
      public Color BorderColor
      {
         get => (Color) GetValue(BorderColorProperty);
         set => SetValue(BorderColorProperty, value);
      }

      /// <summary>
      /// Gets or sets the border width (ignored if value is &lt; 0 or BorderColor is fully transparent) - default value is 0
      /// </summary>
      /// <value>The width of the border.</value>
      public float BorderWidth
      {
         get => (float) GetValue(BorderWidthProperty);
         set => SetValue(BorderWidthProperty, value);
      }

      /// <summary>
      /// Gets or sets the corner radius - (ignored if &lt;=0) - default value is 0
      /// </summary>
      /// <value>The corner radius.</value>
      public float CornerRadius
      {
         get => (float) GetValue(CornerRadiusProperty);
         set => SetValue(CornerRadiusProperty, value);
      }

      #region Path

      /// <summary>
      /// Gets or sets the points describing the path - (ignored if null or empty) - only for Path shape - default value is
      /// null
      /// </summary>
      /// <value>The points.</value>
      public ObservableCollection<Point> Points
      {
         get => (ObservableCollection<Point>) GetValue(PointsProperty);
         set => SetValue(PointsProperty, value);
      }

      #endregion Path

#pragma warning disable 1591

      /// <summary>
      /// The shape type property
      /// </summary>
      public static readonly BindableProperty ShapeTypeProperty =
         BindableProperty.Create(nameof(ShapeType), typeof(ShapeType), typeof(ShapeView), ShapeType.Box);

      /// <summary>
      /// The border color property
      /// </summary>
      public static readonly BindableProperty BorderColorProperty =
         BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ShapeView), Color.Black);

      /// <summary>
      /// The border width property
      /// </summary>
      public static readonly BindableProperty BorderWidthProperty =
         BindableProperty.Create(nameof(BorderWidth), typeof(float), typeof(ShapeView), 0f);

      /// <summary>
      /// The corner radius property
      /// </summary>
      public static readonly BindableProperty CornerRadiusProperty =
         BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(ShapeView), 0f);

      /// <summary>
      /// The progress property
      /// </summary>
      public static readonly BindableProperty ProgressProperty =
         BindableProperty.Create(nameof(Progress), typeof(float), typeof(ShapeView), 0f);

      /// <summary>
      /// The number of points property
      /// </summary>
      public static readonly BindableProperty NumberOfPointsProperty =
         BindableProperty.Create(nameof(NumberOfPoints), typeof(int), typeof(ShapeView), 5);

      /// <summary>
      /// The progress border color property
      /// </summary>
      public static readonly BindableProperty ProgressBorderColorProperty =
         BindableProperty.Create(nameof(ProgressBorderColor), typeof(Color), typeof(ShapeView), Color.Black);

      /// <summary>
      /// The progress border width property
      /// </summary>
      public static readonly BindableProperty ProgressBorderWidthProperty =
         BindableProperty.Create(nameof(ProgressBorderWidth), typeof(float), typeof(ShapeView), 3f);

      /// <summary>
      /// The radius ratio property
      /// </summary>
      public static readonly BindableProperty RadiusRatioProperty =
         BindableProperty.Create(nameof(RadiusRatio), typeof(float), typeof(ShapeView), 0.5f);

      /// <summary>
      /// The color property
      /// </summary>
      public static readonly BindableProperty ColorProperty =
         BindableProperty.Create(nameof(Color), typeof(Color), typeof(ShapeView), Color.Default);

      /// <summary>
      /// The points property
      /// </summary>
      public static readonly BindableProperty PointsProperty =
         BindableProperty.Create(nameof(Points), typeof(ObservableCollection<Point>), typeof(ShapeView), null);

#pragma warning restore 1591

      #region Star

      /// <summary>
      /// Gets or sets the ratio between inner radius and outer radius (outer = inner * RadiusRatio) - only for Star shape -
      /// default value is 0.5
      /// </summary>
      /// <value>The radius ratio.</value>
      public float RadiusRatio
      {
         get => (float) GetValue(RadiusRatioProperty);
         set => SetValue(RadiusRatioProperty, value);
      }

      /// <summary>
      /// Gets or sets the number of points of a star - only for Star shape - default value is 5
      /// </summary>
      /// <value>The number of points.</value>
      public int NumberOfPoints
      {
         get => (int) GetValue(NumberOfPointsProperty);
         set => SetValue(NumberOfPointsProperty, value);
      }

      #endregion Star

      #region CircleProgress

      /// <summary>
      /// Gets or sets the progress value - range from 0 to 100 - only for CircleProgress shape - default value is 0
      /// </summary>
      /// <value>The progress.</value>
      public float Progress
      {
         get => (float) GetValue(ProgressProperty);
         set => SetValue(ProgressProperty, value);
      }

      /// <summary>
      /// Gets or sets the progress border width - only for CircleProgress shape - default value is 3
      /// </summary>
      /// <value>The width of the progress border.</value>
      public float ProgressBorderWidth
      {
         get => (float) GetValue(ProgressBorderWidthProperty);
         set => SetValue(ProgressBorderWidthProperty, value);
      }

      /// <summary>
      /// Gets or sets the progress border color (ignored if fully transparent or ProgressBorderWidth &lt;= 0) - default value
      /// is Color.Black
      /// </summary>
      /// <value>The color of the progress border.</value>
      public Color ProgressBorderColor
      {
         get => (Color) GetValue(ProgressBorderColorProperty);
         set => SetValue(ProgressBorderColorProperty, value);
      }

      #endregion CircleProgress
   }
}