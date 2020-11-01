// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="GenericViewButtonBase.cs" company="Marcus Technical Services, Inc.">
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

// #define STOP_PROPERTY_CHANGED

using System.ComponentModel;
using Xamarin.Forms.PancakeView;

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Utils;
   using SharedUtils.Interfaces;
   using SharedUtils.Utils;
   using System;
   using System.Diagnostics;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IGenericViewButtonBase
   /// Implements the <see cref="Com.MarcusTS.SharedUtils.Interfaces.IHaveButtonState" />
   /// Implements the <see cref="System.IDisposable" />
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <seealso cref="Com.MarcusTS.SharedUtils.Interfaces.IHaveButtonState" />
   /// <seealso cref="System.IDisposable" />
   public interface IGenericViewButtonBase<T> : IHaveButtonState, IDisposable, INotifyPropertyChanged, IProvideButtonStyles
      where T : View
   {
      /// <summary>
      /// Gets or sets the color of the back.
      /// </summary>
      /// <value>The color of the back.</value>
      Color BackColor { get; set; }

      /// <summary>
      /// Gets or sets the button command.
      /// </summary>
      /// <value>The button command.</value>
      Command ButtonCommand { get; set; }

      /// <summary>
      /// Gets or sets the name of the button command binding.
      /// </summary>
      /// <value>The name of the button command binding.</value>
      string ButtonCommandBindingName { get; set; }

      /// <summary>
      /// Gets or sets the button command converter.
      /// </summary>
      /// <value>The button command converter.</value>
      IValueConverter ButtonCommandConverter { get; set; }

      /// <summary>
      /// Gets or sets the button command converter parameter.
      /// </summary>
      /// <value>The button command converter parameter.</value>
      object ButtonCommandConverterParameter { get; set; }

      /// <summary>
      /// Gets or sets the button command source.
      /// </summary>
      /// <value>The button command source.</value>
      object ButtonCommandSource { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this instance can select.
      /// </summary>
      /// <value><c>true</c> if this instance can select; otherwise, <c>false</c>.</value>
      bool CanSelect { get; set; }

      /// <summary>
      /// Gets or sets the corner radius factor.
      /// </summary>
      /// <value>The corner radius factor.</value>
      double? CornerRadiusFactor { get; set; }

      /// <summary>
      /// Gets or sets the corner radius fixed.
      /// </summary>
      /// <value>The corner radius fixed.</value>
      double? CornerRadiusFixed { get; set; }

      /// <summary>
      /// Gets or sets the internal view.
      /// </summary>
      /// <value>The internal view.</value>
      T InternalView { get; set; }

      /// <summary>
      /// Converts to ggleselection.
      /// </summary>
      /// <value><c>true</c> if [toggle selection]; otherwise, <c>false</c>.</value>
      bool ToggleSelection { get; set; }
   }

   public interface IProvideButtonStyles
   {
      /// <summary>
      /// Gets or sets the deselected button style.
      /// </summary>
      /// <value>The deselected button style.</value>
      Style DeselectedButtonStyle { get; set; }

      /// <summary>
      /// Gets or sets the disabled button style.
      /// </summary>
      /// <value>The disabled button style.</value>
      Style DisabledButtonStyle { get; set; }

      /// <summary>
      /// Gets or sets the selected button style.
      /// </summary>
      /// <value>The selected button style.</value>
      Style SelectedButtonStyle { get; set; }
   }

   /// <summary>
   /// A button that takes a view to lay on top of it -- can be a label image, etc. NOTE that we need
   /// our own property change handling, since we have no view model. ISSUES:
   /// * On any setting or style change, we need to re-render !!!
   /// * Rendering needs to take the various appearance settings into account, especially radius,
   /// background color, etc. NEEDS:
   /// * Tap effects -- to grow or shake or vibrate on tap -- ?
   /// * Border Thickness
   /// * Border Color
   /// * Shadow Properties
   /// Implements the <see cref="ShapeView" />
   /// Implements the <see cref="IGenericViewButtonBase{T}" />
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <seealso cref="ShapeView" />
   /// <seealso cref="IGenericViewButtonBase{T}" />
   public abstract class GenericViewButtonBase<T> : ShapeView, IGenericViewButtonBase<T>
      where T : View
   {
      /// <summary>
      /// The back color property
      /// </summary>
      public static readonly BindableProperty BackColorProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(BackColor),
             default(Color),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.BackColor = newVal;
             }
            );

      /// <summary>
      /// The button command binding name property
      /// </summary>
      public static readonly BindableProperty ButtonCommandBindingNameProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(ButtonCommandBindingName),
             default(string),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.ButtonCommandBindingName = newVal;
             }
            );

      /// <summary>
      /// The button command converter parameter property
      /// </summary>
      public static readonly BindableProperty ButtonCommandConverterParameterProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(ButtonCommandConverterParameter),
             default(object),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.ButtonCommandConverterParameter = newVal;
             }
            );

      /// <summary>
      /// The button command converter property
      /// </summary>
      public static readonly BindableProperty ButtonCommandConverterProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(ButtonCommandConverter),
             default(IValueConverter),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.ButtonCommandConverter = newVal;
             }
            );

      /// <summary>
      /// The button command property
      /// </summary>
      public static readonly BindableProperty ButtonCommandProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(ButtonCommand),
             default(Command),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.ButtonCommand = newVal;
             }
            );

      /// <summary>
      /// The button state property
      /// </summary>
      public static readonly BindableProperty ButtonStateProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(ButtonState),
             default(ButtonStates),
             BindingMode.TwoWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.ButtonState = newVal;
                viewButton.HandleButtonStateChanged();
             }
            );

      /// <summary>
      /// The can select property
      /// </summary>
      public static readonly BindableProperty CanSelectProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(CanSelect),
             default(bool),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.CanSelect = newVal;
             }
            );

      /// <summary>
      /// The corner radius factor property
      /// </summary>
      public static readonly BindableProperty CornerRadiusFactorProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(CornerRadiusFactor),
             default(double?),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.CornerRadiusFactor = newVal;
             }
            );

      /// <summary>
      /// The corner radius fixed property
      /// </summary>
      public static readonly BindableProperty CornerRadiusFixedProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(CornerRadiusFixed),
             default(double?),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.CornerRadiusFixed = newVal;
             }
            );

      /// <summary>
      /// The deselected button style property
      /// </summary>
      public static readonly BindableProperty DeselectedButtonStyleProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(DeselectedButtonStyle),
             default(Style),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.DeselectedButtonStyle = newVal;
             }
            );

      /// <summary>
      /// The disabled button style property
      /// </summary>
      public static readonly BindableProperty DisabledButtonStyleProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(DisabledButtonStyle),
             default(Style),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.DisabledButtonStyle = newVal;
             }
            );

      /// <summary>
      /// The selected button style property
      /// </summary>
      public static readonly BindableProperty SelectedButtonStyleProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(SelectedButtonStyle),
             default(Style),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.SelectedButtonStyle = newVal;
             }
            );

      /// <summary>
      /// The selection group property
      /// </summary>
      public static readonly BindableProperty SelectionGroupProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(SelectionGroup),
             default(int),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.SelectionGroup = newVal;
             }
            );

      /// <summary>
      /// Converts to ggleselectionproperty.
      /// </summary>
      public static readonly BindableProperty ToggleSelectionProperty =
         CreateGenericViewButtonBindableProperty
            (
             nameof(ToggleSelection),
             default(bool),
             BindingMode.OneWay,
             (viewButton,
              oldVal,
              newVal) =>
             {
                viewButton.ToggleSelection = newVal;
             }
            );

      //---------------------------------------------------------------------------------------------------------------
      // VARIABLES
      //---------------------------------------------------------------------------------------------------------------

      /// <summary>
      /// The tap gesture
      /// </summary>
      private readonly TapGestureRecognizer _tapGesture = new TapGestureRecognizer();

      /// <summary>
      /// The button command
      /// </summary>
      private Command _buttonCommand;

      /// <summary>
      /// The button command binding name
      /// </summary>
      private string _buttonCommandBindingName;

      /// <summary>
      /// The button command converter
      /// </summary>
      private IValueConverter _buttonCommandConverter;

      /// <summary>
      /// The button command converter parameter
      /// </summary>
      private object _buttonCommandConverterParameter;

      /// <summary>
      /// The button command source
      /// </summary>
      private object _buttonCommandSource;

      /// <summary>
      /// The corner radius factor
      /// </summary>
      private double? _cornerRadiusFactor;

      /// <summary>
      /// The corner radius fixed
      /// </summary>
      private double? _cornerRadiusFixed;

      /// <summary>
      /// The deselected style
      /// </summary>
      private Style _deselectedStyle;

      /// <summary>
      /// The disabled style
      /// </summary>
      private Style _disabledStyle;

      /// <summary>
      /// The internal view entered
      /// </summary>
      private volatile bool _internalViewEntered;

      /// <summary>
      /// The is releasing
      /// </summary>
      private volatile bool _isReleasing;

      /// <summary>
      /// The selected style
      /// </summary>
      private Style _selectedStyle;

      /// <summary>
      /// The selection group
      /// </summary>
      private int _selectionGroup;

      /// <summary>
      /// The tapped listener entered
      /// </summary>
      private volatile bool _tappedListenerEntered;

      //---------------------------------------------------------------------------------------------------------------
      // CONSTRUCTOR
      //---------------------------------------------------------------------------------------------------------------

      /// <summary>
      /// Initializes a new instance of the <see cref="GenericViewButtonBase{T}" /> class.
      /// </summary>
      protected GenericViewButtonBase()
      {
         IAmSelectedStatic += HandleStaticSelectionChanges;

         GestureRecognizers.Add(_tapGesture);
         _tapGesture.Tapped += HandleTapGestureTapped;

         // ShapeType = ShapeType.Box;

         SetStyle();

#if !STOP_PROPERTY_CHANGED
         // We could specify the properties, but there are *many* that affect style
         PropertyChanged +=
         (
            sender,
            args
         ) =>
         {
            SetStyle();
         };
#endif
      }

      /// <summary>
      /// Occurs when [button state changed].
      /// </summary>
      public event EventHandler<ButtonStates> ButtonStateChanged;

      /// <summary>
      /// Occurs when [view button pressed].
      /// </summary>
      public event EventUtils.NoParamsDelegate ViewButtonPressed;

      /// <summary>
      /// Occurs when [i am selected static].
      /// </summary>
      protected static event EventUtils.GenericDelegate<IGenericViewButtonBase<T>> IAmSelectedStatic;

      /// <summary>
      /// Gets or sets the color of the back.
      /// </summary>
      /// <value>The color of the back.</value>
      public Color BackColor
      {
         get => Color;
         set => Color = value;
      }

      /// <summary>
      /// Gets or sets the button command.
      /// </summary>
      /// <value>The button command.</value>
      public Command ButtonCommand
      {
         get => _buttonCommand;
         set
         {
            RemoveButtonCommandEventListener();

            _buttonCommand = value;

            if (ButtonCommand != null)
            {
               ButtonCommand.CanExecuteChanged += HandleButtonCommandCanExecuteChanged;

               // Force-fire the initial state
               ButtonCommand.ChangeCanExecute();
            }
         }
      }

      /// <summary>
      /// Gets or sets the name of the button command binding.
      /// </summary>
      /// <value>The name of the button command binding.</value>
      public string ButtonCommandBindingName
      {
         get => _buttonCommandBindingName;
         set
         {
            _buttonCommandBindingName = value;

            SetUpCompleteViewButtonCommandBinding();
         }
      }

      /// <summary>
      /// Gets or sets the button command converter.
      /// </summary>
      /// <value>The button command converter.</value>
      public IValueConverter ButtonCommandConverter
      {
         get => _buttonCommandConverter;
         set
         {
            _buttonCommandConverter = value;
            SetUpCompleteViewButtonCommandBinding();
         }
      }

      /// <summary>
      /// Gets or sets the button command converter parameter.
      /// </summary>
      /// <value>The button command converter parameter.</value>
      public object ButtonCommandConverterParameter
      {
         get => _buttonCommandConverterParameter;
         set
         {
            _buttonCommandConverterParameter = value;
            SetUpCompleteViewButtonCommandBinding();
         }
      }

      /// <summary>
      /// Gets or sets the button command source.
      /// </summary>
      /// <value>The button command source.</value>
      public object ButtonCommandSource
      {
         get => _buttonCommandSource;
         set
         {
            _buttonCommandSource = value;
            SetUpCompleteViewButtonCommandBinding();
         }
      }

      /// <summary>
      /// Gets or sets the state of the button.
      /// </summary>
      /// <value>The state of the button.</value>
      public ButtonStates ButtonState
      {
         get => (ButtonStates)GetValue(ButtonStateProperty);
         set => SetValue(ButtonStateProperty, value);
      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance can select.
      /// </summary>
      /// <value><c>true</c> if this instance can select; otherwise, <c>false</c>.</value>
      public bool CanSelect { get; set; }

      /// <summary>
      /// Gets or sets the corner radius factor.
      /// </summary>
      /// <value>The corner radius factor.</value>
      public double? CornerRadiusFactor
      {
         get => _cornerRadiusFactor;
         set
         {
            _cornerRadiusFactor = value;

            SetCornerRadius();
         }
      }

      /// <summary>
      /// Gets or sets the corner radius fixed.
      /// </summary>
      /// <value>The corner radius fixed.</value>
      public double? CornerRadiusFixed
      {
         get => _cornerRadiusFixed;
         set
         {
            _cornerRadiusFixed = value;

            SetCornerRadius();
         }
      }

      /// <summary>
      /// Gets or sets the deselected button style.
      /// </summary>
      /// <value>The deselected button style.</value>
      public Style DeselectedButtonStyle
      {
         get => _deselectedStyle;
         set
         {
            _deselectedStyle = value;
            SetStyle();
         }
      }

      /// <summary>
      /// Gets or sets the disabled button style.
      /// </summary>
      /// <value>The disabled button style.</value>
      public Style DisabledButtonStyle
      {
         get => _disabledStyle;
         set
         {
            _disabledStyle = value;
            SetStyle();
         }
      }

      /// <summary>
      /// Gets or sets the internal view.
      /// </summary>
      /// <value>The internal view.</value>
      public T InternalView
      {
         get => Content as T;
         set
         {
            if (_internalViewEntered || _isReleasing)
            {
               return;
            }

            _internalViewEntered = true;

            try
            {
               Content = value;

               AfterInternalViewSet();
            }
            catch (Exception e)
            {
               Debug.WriteLine("INTERNAL VIEW ASSIGNMENT ERROR ->" + e.Message + "<-");
            }

            _internalViewEntered = false;
         }
      }

      /// <summary>
      /// Gets or sets the selected button style.
      /// </summary>
      /// <value>The selected button style.</value>
      public Style SelectedButtonStyle
      {
         get => _selectedStyle;
         set
         {
            _selectedStyle = value;
            SetStyle();
         }
      }

      /// <summary>
      /// Leave at 0 if multiple selection is OK
      /// </summary>
      /// <value>The selection group.</value>
      public int SelectionGroup
      {
         get => _selectionGroup;
         set
         {
            _selectionGroup = value;
            BroadcastIfSelected();
         }
      }

      //---------------------------------------------------------------------------------------------------------------
      // PROPERTIES (Public)
      //---------------------------------------------------------------------------------------------------------------
      /// <summary>
      /// Toggles the selection.
      /// </summary>
      /// <value><c>true</c> if [toggle selection]; otherwise, <c>false</c>.</value>
      public bool ToggleSelection { get; set; }

      /// <summary>
      /// Creates the generic view button bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateGenericViewButtonBindableProperty<PropertyTypeT>(string localPropName,
                                                                                            PropertyTypeT defaultVal =
                                                                                               default(PropertyTypeT),
                                                                                            BindingMode bindingMode =
                                                                                               BindingMode.OneWay,
                                                                                            Action<GenericViewButtonBase
                                                                                                  <T>, PropertyTypeT,
                                                                                                  PropertyTypeT>
                                                                                               callbackAction = null)
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      /// Creates the view button style.
      /// </summary>
      /// <param name="backColor">Color of the back.</param>
      /// <param name="borderWidth">Width of the border.</param>
      /// <param name="borderColor">Color of the border.</param>
      /// <returns>Style.</returns>
      public static Style CreateViewButtonStyle(Color? backColor,
                                                double? borderWidth = null,
                                                Color? borderColor = null)
      {
         var retStyle = new Style(typeof(GenericViewButtonBase<T>));

         if (backColor.HasValue)
         {
            retStyle.Setters.Add(new Setter { Property = ColorProperty, Value = backColor });
         }

         if (borderColor.HasValue && borderWidth.HasValue)
         {
            retStyle.Setters.Add(new Setter { Property = PancakeView.BorderProperty, Value = FormsUtils.CreateShapeViewBorder(borderColor, borderWidth) });
         }

         return retStyle;
      }

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      //---------------------------------------------------------------------------------------------------------------
      // EVENTS
      //---------------------------------------------------------------------------------------------------------------
      //---------------------------------------------------------------------------------------------------------------
      // METHODS - Protected
      //---------------------------------------------------------------------------------------------------------------

      /// <summary>
      /// Afters the internal view set.
      /// </summary>
      protected virtual void AfterInternalViewSet()
      { }

      /// <summary>
      /// Releases unmanaged and - optionally - managed resources.
      /// </summary>
      /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
      protected virtual void Dispose(bool disposing)
      {
         ReleaseUnmanagedResources();
         if (disposing)
         { }
      }

      /// <summary>
      /// Handles the tap gesture tapped.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      protected void HandleTapGestureTapped(object sender,
                                            EventArgs e)
      {
         if (_tappedListenerEntered || ButtonState == ButtonStates.Disabled)
         {
            return;
         }

         _tappedListenerEntered = true;

         ViewButtonPressed?.Invoke();

         if (CanSelect)
         {
            if (ToggleSelection)
            {
               ButtonState = ButtonState != ButtonStates.Selected ? ButtonStates.Selected : ButtonStates.Deselected;
            }
            else
            {
               ButtonState = ButtonStates.Selected;
            }
         }

         // If a command exists, fire it and reset our selected status to false; otherwise, leave the
         // selected state as it is.
         if (ButtonCommand != null)
         {
            Device.BeginInvokeOnMainThread
               (
#if ANIMATE
            async
#endif
                () =>
                {
#if ANIMATE
                if (InternalView != null)
                {
                  await InternalView.ScaleTo(0.95, 50, Easing.CubicOut).WithoutChangingContext();
                  await InternalView.ScaleTo(1, 50, Easing.CubicIn).WithoutChangingContext();
                }
#endif

                   ButtonCommand.Execute(this);

                   // This means that we do not intend to maintain the button state.
                   if (SelectionGroup == 0)
                   {
                      // Revert the state to its default setting.
                      ButtonState = ButtonStates.Deselected;
                   }
                }
               );
         }

         _tappedListenerEntered = false;
      }

      /// <summary>
      /// Sets the style.
      /// </summary>
      protected virtual void SetStyle()
      {
         Style newStyle;

         // Set the style based on being enabled/disabled
         if (ButtonState == ButtonStates.Disabled)
         {
            newStyle = DisabledButtonStyle ?? DeselectedButtonStyle;
         }
         else if (ButtonState == ButtonStates.Selected)
         {
            newStyle = SelectedButtonStyle ?? DeselectedButtonStyle;
         }
         else
         {
            newStyle = DeselectedButtonStyle;
         }

         // Cannot compare lists of objects using Equal
         //if (newStyle != null && (Style == null || Style.IsNotAnEqualObjectTo(newStyle)))
         //{
#if MERGE_STYLES
       Style = Style.MergeStyle<GenericViewButtonBase<T>>(newStyle);
#else
         Style = newStyle;
#endif

         // This library is not working well with styles, so forcing all settings manually
         this.ForceStyle(Style);
         //}
      }

      /// <summary>
      /// Broadcasts if selected.
      /// </summary>
      private void BroadcastIfSelected()
      {
         if (ButtonState == ButtonStates.Selected && SelectionGroup > 0)
         {
            // Raise a static event to notify others in this selection group that they should be *deselected*
            IAmSelectedStatic?.Invoke(this);
         }
      }

      //---------------------------------------------------------------------------------------------------------------
      // EVENT HANDLERS
      //---------------------------------------------------------------------------------------------------------------
      /// <summary>
      /// Handles the button command can execute changed.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void HandleButtonCommandCanExecuteChanged(object sender,
                                                        EventArgs e)
      {
         var newCanExecute = sender is Command senderAsCommand && senderAsCommand.CanExecute(this);

         IsEnabled = newCanExecute;

         // The control is not issuing a property change when we manually set IsEnabled, so handling
         // that case here. Cannot listen to property changes generally in this case.
         SetStyle();
      }

      /// <summary>
      /// Handles the button state changed.
      /// </summary>
      private void HandleButtonStateChanged()
      {
         SetStyle();
         BroadcastIfSelected();
         ButtonStateChanged?.Invoke(this, ButtonState);
      }

      /// <summary>
      /// Handles the static selection changes.
      /// </summary>
      /// <param name="sender">The sender.</param>
      private void HandleStaticSelectionChanges(IGenericViewButtonBase<T> sender)
      {
         // Do not recur onto our own broadcast; also only respond to the same selection group.
         if (sender.SelectionGroup == SelectionGroup && !ReferenceEquals(sender, this) &&
             ButtonState == ButtonStates.Selected)
         {
            ButtonState = ButtonStates.Deselected;
         }
      }

      /// <summary>
      /// Releases the unmanaged resources.
      /// </summary>
      private void ReleaseUnmanagedResources()
      {
         _isReleasing = true;

         // Global static, so remove the handler
         IAmSelectedStatic -= HandleStaticSelectionChanges;

         _tapGesture.Tapped -= HandleTapGestureTapped;

         RemoveButtonCommandEventListener();
      }

      /// <summary>
      /// Removes the button command event listener.
      /// </summary>
      private void RemoveButtonCommandEventListener()
      {
         if (ButtonCommand != null)
         {
            ButtonCommand.CanExecuteChanged -= HandleButtonCommandCanExecuteChanged;
         }
      }

      /// <summary>
      /// Sets the corner radius.
      /// </summary>
      private void SetCornerRadius()
      {
         if (CornerRadiusFactor.HasNoValue())
         {
            CornerRadiusFactor = FormsUtils.BUTTON_RADIUS_FACTOR;
         }

         if (CornerRadiusFactor.HasValue)
         {
            CornerRadius = Convert.ToSingle(Math.Min(Bounds.Width, Bounds.Height) * _cornerRadiusFactor);
         }
         else if (CornerRadiusFixed.HasValue)
         {
            CornerRadius = Convert.ToSingle(CornerRadiusFixed);
         }
         else
         {
            CornerRadius = Convert.ToSingle(FormsUtils.BUTTON_RADIUS_FACTOR);
         }
      }

      /// <summary>
      /// Sets up complete view button command binding.
      /// </summary>
      private void SetUpCompleteViewButtonCommandBinding()
      {
         if (ButtonCommandBindingName.IsEmpty())
         {
            RemoveBinding(ButtonCommandProperty);
         }
         else
         {
            this.SetUpBinding
               (
                ButtonCommandProperty,
                ButtonCommandBindingName,
                BindingMode.OneWay,
                ButtonCommandConverter,
                ButtonCommandConverterParameter,
                null,
                ButtonCommandSource
               );
         }
      }

      /*
      /// <summary>
      /// This method is called when the size of the element is set during a layout cycle. This method is called directly before the <see cref="E:Xamarin.Forms.VisualElement.SizeChanged" /> event is emitted. Implement this method to add class handling for this event.
      /// </summary>
      /// <param name="width">The new width of the element.</param>
      /// <param name="height">The new height of the element.</param>
      /// <remarks>This method has a default implementation which triggers the layout cycle of the Layout to begin.</remarks>
      protected override void OnSizeAllocated(double width,
                                              double height)
      {
         base.OnSizeAllocated(width, height);

         SetCornerRadius();
      }
      */

      //---------------------------------------------------------------------------------------------------------------
      // STATIC READ ONLY VARIABLES & METHODS
      //---------------------------------------------------------------------------------------------------------------
      //---------------------------------------------------------------------------------------------------------------
      // BINDABLE PROPERTIES
      //---------------------------------------------------------------------------------------------------------------
      //---------------------------------------------------------------------------------------------------------------
      // D I S P O S A L
      //---------------------------------------------------------------------------------------------------------------
      /// <summary>
      /// Finalizes an instance of the <see cref="GenericViewButtonBase{T}" /> class.
      /// </summary>
      ~GenericViewButtonBase()
      {
         Dispose(false);
      }
   }
}
