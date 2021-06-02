// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=ValidatableEntry.cs
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

// #define SHOW_BACK_COLOR

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using System;
   using System.Runtime.CompilerServices;
   using System.Threading.Tasks;
   using Com.MarcusTS.SharedForms.ViewModels;
   using Common.Images;
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Essentials;
   using Xamarin.Forms;

   /// <summary>
   ///    Interface IValidatableEntry Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IValidatableEntry : IValidatableView
   {
      Entry EditableEntry { get; }
   }

   /// <summary>
   ///    A UI element that includes an Entry surrounded by a border. Implements the <see cref="Xamarin.Forms.Grid" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableEntry" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Grid" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IValidatableEntry" />
   public class ValidatableEntry : ValidatableViewBase, IValidatableEntry
   {
      /// <summary>The hide password image</summary>
      private const string HIDE_PASSWORD_IMAGE = SharedImageUtils.IMAGE_PRE_PATH + "hide_password.png";

      /// <summary>The show password image</summary>
      private const string SHOW_PASSWORD_IMAGE = SharedImageUtils.IMAGE_PRE_PATH + "show_password.png";

      protected static readonly Thickness DEFAULT_EDITABLE_ENTRY_MARGIN = new Thickness(8, 0, 4, 0);

      public static readonly BindableProperty EditableEntryMarginProperty = 
         CreateValidatableEntryBindableProperty
            (
               nameof(EditableEntryMargin),
               DEFAULT_EDITABLE_ENTRY_MARGIN,
               BindingMode.OneWay,
               (
                  view,
                  oldVal,
                  newVal
               ) =>
               {
                  // Fore reconstruction
                  view._editableEntry = default;
               }
            );

      private readonly bool     _canUnmaskPassword;
      private readonly bool     _isPassword;
      private readonly Keyboard _keyboard;
      private          Entry    _editableEntry;
      private          View     _editableViewContainer;
      private          bool     _isPasswordShowing;
      private          Image    _showHideImage;
      private readonly double   _entryFontSize;

      public ValidatableEntry
      (
         bool     canUnmaskPassword         = true,
         double   editableEntryMarginBottom = 0,
         double   editableEntryMarginLeft   = 0,
         double   editableEntryMarginRight  = 0,
         double   editableEntryMarginTop    = 0,
         double?  entryFontSize             = null,
         bool     isPassword                = false,
         Keyboard keyboard                  = null,
         bool asleepInitially = false
      )
         : base(Entry.TextProperty, asleepInitially: asleepInitially)
         //   : base (default, asleepInitially:asleepInitially)
      {
         _keyboard          = keyboard;
         _isPassword        = isPassword;
         _canUnmaskPassword = canUnmaskPassword;
         _entryFontSize      = entryFontSize ?? FormsConst.EDITABLE_VIEW_FONT_SIZE;
         EditableEntryMargin = new Thickness(editableEntryMarginLeft.GetUSetValueOrDefault(),
                                             editableEntryMarginTop.GetUSetValueOrDefault(),
                                             editableEntryMarginRight.GetUSetValueOrDefault(),
                                             editableEntryMarginBottom.GetUSetValueOrDefault());

         if (!IsConstructing)
         {
            RecreateAllViewsBindingsAndStyles();
         }
      }

      public Thickness EditableEntryMargin
      {
         get => (Thickness) GetValue(EditableEntryMarginProperty);
         set => SetValue(EditableEntryMarginProperty, value);
      }

      protected override bool DerivedViewIsFocused => _showHideImage.IsNotNullOrDefault() && _showHideImage.IsFocused;

      protected override View EditableView => EditableEntry;

      protected override View EditableViewContainer
      {
         get
         {
            // CRITICAL BorderView must be created before the EditableViewContainer is referenced.
            if (_editableViewContainer.IsNullOrDefault() && !IsConstructing && BorderView.IsNotNullOrDefault())
            {
               if (_isPassword && _canUnmaskPassword)
               {
                  // Put the editable entry in a grid with the password unmask button
                  var editGrid = FormsUtils.GetExpandingGrid();

                  editGrid.AddStarColumn();
                  editGrid.AddAutoColumn();
                  editGrid.Children.Add(EditableEntry);
                  SetColumn(EditableEntry, 0);

                  var showHideImageWidthHeight = BorderView.HeightRequest * 0.85;

                  _showHideImage = FormsUtils.GetImage("", showHideImageWidthHeight, showHideImageWidthHeight,
                     Aspect.AspectFit, getFromResources: true, resourceClass: GetType());
                  _showHideImage.Margin           = new Thickness(0, 0, 5, 0);
                  _showHideImage.InputTransparent = false;
                  var tapGesture = new TapGestureRecognizer();

                  tapGesture.Tapped += async (sender, args) =>
                                       {
                                          var cursorIdx = EditableEntry.CursorPosition;
                                          
                                          IsPasswordShowing = !IsPasswordShowing;

                                          await Task.Delay(100).WithoutChangingContext();

                                          MainThread.BeginInvokeOnMainThread(() =>
                                                                             {
                                                                                // Go back to editing
                                                                                EditableEntry.Focus();
                                                                                EditableEntry.CursorPosition = cursorIdx;
                                                                             });
                                       };
                  _showHideImage.GestureRecognizers.Add(tapGesture);
                  SetShowHideImageSource();

                  _showHideImage.Focused   -= ReportGlobalFocusAndRaisePlaceholder;
                  _showHideImage.Focused   += ReportGlobalFocusAndRaisePlaceholder;
                  _showHideImage.Unfocused -= ConsiderLoweringPlaceholder;
                  _showHideImage.Unfocused += ConsiderLoweringPlaceholder;

                  editGrid.Children.Add(_showHideImage);
                  SetColumn(_showHideImage, 1);
                  editGrid.RaiseChild(_showHideImage);

                  _editableViewContainer = editGrid;
               }
               else
               {
                  _editableViewContainer = EditableEntry;
               }
            }

            return _editableViewContainer;
         }
      }

      protected override bool UserHasEnteredValidContent => EditableEntry.Text.IsNotEmpty();

      /// <summary>Gets or sets a value indicating whether this instance is password showing.</summary>
      /// <value><c>true</c> if this instance is password showing; otherwise, <c>false</c>.</value>
      private bool IsPasswordShowing
      {
         get => _isPasswordShowing;
         set
         {
            if (_isPasswordShowing == value)
            {
               return;
            }

            _isPasswordShowing = value;

            EditableEntry.IsPassword = !_isPasswordShowing;

            // BUG - HACK - Sometimes fails to refresh
            EditableEntry.IsPassword = _isPasswordShowing;
            EditableEntry.IsPassword = !_isPasswordShowing;

            SetShowHideImageSource();
         }
      }

      public Entry EditableEntry
      {
         get
         {
            if (_editableEntry.IsNullOrDefault() && !IsConstructing)
            {
               _editableEntry = new CustomEntry
               {
                  // Cannot use non-standard keyboards when there is a mask
                  Keyboard = _keyboard,
                  
#if SHOW_BACK_COLOR  
                  BackgroundColor = Color.Yellow,
#else                     
                  BackgroundColor = Color.Transparent,
#endif
                  FontSize = _entryFontSize,
                  IsEnabled = true,
                  IsReadOnly = false,
                  MaxLength = int.MaxValue,
                  IsPassword = _isPassword,
                  TextColor = Color.Black,
                  HorizontalOptions = LayoutOptions.FillAndExpand,
                  VerticalOptions = LayoutOptions.FillAndExpand,
                  Margin = DEFAULT_EDITABLE_ENTRY_MARGIN
               };
            }

            return _editableEntry;
         }
      }

      public static BindableProperty CreateValidatableEntryBindableProperty<PropertyTypeT>
      (
         string                                                 localPropName,
         PropertyTypeT                                          defaultVal     = default,
         BindingMode                                            bindingMode    = BindingMode.OneWay,
         Action<ValidatableEntry, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      private void SetShowHideImageSource()
      {
         if (_showHideImage.IsNotNullOrDefault())
         {
            // ReSharper disable once RedundantAssignment
            var imageResourceStr = string.Empty;

            imageResourceStr = _isPasswordShowing ? SHOW_PASSWORD_IMAGE : HIDE_PASSWORD_IMAGE;

            _showHideImage.Source = ImageSource.FromResource(imageResourceStr, GetType().Assembly);
         }
      }
   }
}