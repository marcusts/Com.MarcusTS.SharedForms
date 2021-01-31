// *********************************************************************** Assembly : Com.MarcusTS.SharedForms Author :
// steph Created : 08-04-2019
//
// Last Modified By : steph Last Modified On : 08-08-2019
// ***********************************************************************
// <copyright file="ValidatableEntry.cs" company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Images;
   using Common.Interfaces;
   using Common.Utils;
   using SharedUtils.Utils;
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

      private readonly bool     _canUnmaskPassword;
      private readonly bool     _isPassword;
      private readonly Keyboard _keyboard;
      private          Entry    _editableEntry;
      private          View     _editableViewContainer;
      private          bool     _isPasswordShowing;
      private          Image    _showHideImage;

      public ValidatableEntry
      (
         double?         borderViewHeight                   = BORDER_VIEW_HEIGHT,
         bool            canUnmaskPassword                  = true,
         BindingMode     bindingMode                        = BindingMode.TwoWay,
         IValueConverter converter                          = null,
         object          converterParameter                 = null,
         double?         entryFontSize                      = null,
         string          fontFamilyOverride                 = "",
         string          instructions                       = "",
         double?         instructionsHeight                 = INSTRUCTIONS_HEIGHT,
         bool            isPassword                         = false,
         Keyboard        keyboard                           = null,
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
            Entry.TextProperty,
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
         _keyboard          = keyboard;
         _isPassword        = isPassword;
         _canUnmaskPassword = canUnmaskPassword;

         // The editable entry gets created too soon for these assignments to work, so repeating them here.
         if (EditableEntry.IsNotNullOrDefault())
         {
            EditableEntry.Keyboard   = _keyboard;
            EditableEntry.IsPassword = isPassword;
            EditableEntry.FontSize   = entryFontSize ?? Device.GetNamedSize(NamedSize.Small, typeof(Entry));
         }

         CallCreateViews();
      }

      protected override bool DerivedViewIsFocused => _showHideImage.IsNotNullOrDefault() && _showHideImage.IsFocused;

      protected override View EditableView => EditableEntry;

      protected override View EditableViewContainer
      {
         get
         {
            if (_editableViewContainer.IsNullOrDefault())
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
                     Aspect.AspectFit, true, GetType());
                  _showHideImage.Margin = new Thickness(0, 0, 5, 0);
                  var tapGesture = new TapGestureRecognizer();

                  tapGesture.Tapped += (sender, args) => { IsPasswordShowing = !IsPasswordShowing; };
                  _showHideImage.GestureRecognizers.Add(tapGesture);
                  SetShowHideImageSource();

                  _showHideImage.Focused   += ReportGlobalFocusAndRaisePlaceholder;
                  _showHideImage.Unfocused += ConsiderLoweringPlaceholder;

                  editGrid.Children.Add(_showHideImage);
                  SetColumn(_showHideImage, 1);

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
            if (_editableEntry.IsNullOrDefault())
            {
               _editableEntry = new CustomEntry
               {
                  // Cannot use non-standard keyboards when there is a mask
                  Keyboard          = _keyboard,
                  IsEnabled         = true,
                  IsReadOnly        = false,
                  MaxLength         = int.MaxValue,
                  IsPassword        = _isPassword,
                  TextColor         = Color.Black,
                  BackgroundColor   = Color.Transparent,
                  HorizontalOptions = LayoutOptions.FillAndExpand,
                  VerticalOptions   = LayoutOptions.FillAndExpand,
                  Margin            = DEFAULT_BORDER_VIEW_PADDING
               };
            }

            return _editableEntry;
         }
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