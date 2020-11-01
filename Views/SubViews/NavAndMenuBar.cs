// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="NavAndMenuBar.cs" company="Marcus Technical Services, Inc.">
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

using Com.MarcusTS.SharedForms.Common.Navigation;

namespace Com.MarcusTS.SharedForms.Views.SubViews
{
   using Common.Notifications;
   using Common.Utils;
   using Controls;
   using Pages;
   using SharedUtils.Utils;
   using System;
   using System.Linq;
   using ViewModels;
   using Xamarin.Forms;

   /// <summary>
   /// Interface INavAndMenuBar
   /// Implements the <see cref="System.IDisposable" />
   /// </summary>
   /// <seealso cref="System.IDisposable" />
   public interface INavAndMenuBar : IDisposable
   {
      /// <summary>
      /// Gets or sets the hosting page.
      /// </summary>
      /// <value>The hosting page.</value>
      Page HostingPage { get; set; }
   }

   /// <summary>
   /// Class NavAndMenuBar.
   /// Implements the <see cref="Xamarin.Forms.ContentView" />
   /// Implements the <see cref="INavAndMenuBar" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.ContentView" />
   /// <seealso cref="INavAndMenuBar" />
   public class NavAndMenuBar : ContentView, INavAndMenuBar
   {
      /// <summary>
      /// The hosting page property
      /// </summary>
      public static readonly BindableProperty HostingPageProperty =
         BindableProperty.Create(nameof(HostingPage), typeof(Page), typeof(NavAndMenuBar), default(Page),
                                 propertyChanged: OnHostingPageChanged);

      /// <summary>
      /// The overall height
      /// </summary>
      public static readonly double OVERALL_HEIGHT = 45.0;

      /// <summary>
      /// The back image
      /// </summary>
      private const string BACK_IMAGE = "left_arrow_with_shadow_512.png";

      /// <summary>
      /// The hamburger image
      /// </summary>
      private const string HAMBURGER_IMAGE = "hamburger_with_shadow_512.png";

      /// <summary>
      /// The application state back button stack
      /// </summary>
      private static readonly FlexibleStack<string> _appStateBackButtonStack = new FlexibleStack<string>();

      /// <summary>
      /// The button height
      /// </summary>
      private static readonly double BUTTON_HEIGHT = 30.0;

      /// <summary>
      /// The ios margin
      /// </summary>
      private static readonly Thickness IOS_MARGIN = new Thickness(0, 20, 0, 0);

      /// <summary>
      /// The state machine
      /// </summary>
      private static IStateMachine _stateMachine;

      /// <summary>
      /// The back button
      /// </summary>
      private Image _backButton;

      /// <summary>
      /// The hosting page
      /// </summary>
      private Page _hostingPage;

      /// <summary>
      /// The menu button
      /// </summary>
      private Image _menuButton;

      /// <summary>
      /// The menu button entered
      /// </summary>
      private bool _menuButtonEntered;

      /// <summary>
      /// The title label
      /// </summary>
      private Label _titleLabel;

      /// <summary>
      /// Initializes a new instance of the <see cref="NavAndMenuBar" /> class.
      /// </summary>
      /// <param name="stateMachine">The state machine.</param>
      /// <remarks>Not used in the run-time app but can be called for unit testing.</remarks>
      public NavAndMenuBar(IStateMachine stateMachine)
      {
         _stateMachine = stateMachine;

         BackgroundColor = ColorUtils.HEADER_AND_TOOLBAR_COLOR_DEEP;

         if (Device.RuntimePlatform.IsSameAs(Device.iOS))
         {
            Margin = IOS_MARGIN;
         }

         // Listen for the static page change
         AskToSetBackButtonVisiblity += SetBackButtonVisiblity;

         // These message are subscribed but never unsubscribed. The menu is global static, so
         // persists throughout the life of the app. There is no reason to unsubscribe them under
         // these circumstances.
         FormsMessengerUtils.Subscribe<MenuLoadedMessage>(this, OnMenuLoaded);
         FormsMessengerUtils.Subscribe<AppStateChangedMessage>(this, OnAppStateChanged);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="NavAndMenuBar" /> class.
      /// </summary>
      /// <remarks>Must be parameterless due to the XAML page control template at app.xaml. This defeats the
      /// flexibility of menu unit testing, as the injected dependency is hard-coded below.</remarks>
      public NavAndMenuBar() :
         //this(AppContainer.GlobalVariableContainer.Resolve<IStateMachineBase>())
         this(null)
      { }

      /// <summary>
      /// Occurs when [ask to set back button visiblity].
      /// </summary>
      public static event EventUtils.NoParamsDelegate AskToSetBackButtonVisiblity;

      /// <summary>
      /// Gets or sets the hosting page.
      /// </summary>
      /// <value>The hosting page.</value>
      public Page HostingPage
      {
         get => _hostingPage;
         set
         {
            // Remove the old event handler, if any.
            RemoveHostingPageBindingContextChangedHandler();

            _hostingPage = value;

            _hostingPage.BindingContextChanged += OnHostingPageBindingContextChanged;

            // Add the left-side back and right-side hamburger
            var grid = FormsUtils.GetExpandingGrid();
            grid.HeightRequest = OVERALL_HEIGHT;
            grid.VerticalOptions = LayoutOptions.CenterAndExpand;
            grid.AddStarRow();
            grid.AddFixedColumn(15);
            grid.AddFixedColumn(OVERALL_HEIGHT);
            grid.AddStarColumn();
            grid.AddFixedColumn(OVERALL_HEIGHT);
            grid.AddFixedColumn(15);

            // Wire into navigation, if available
            if (_hostingPage is IMenuNavPageBase)
            {
               _backButton = CreateNavBarButton(BACK_IMAGE, BackButtonTapped);
               _backButton.BindingContext = this;
               _backButton.HorizontalOptions = LayoutOptions.Start;
               grid.Children.Add(_backButton, 1, 0);

               // Initial setting
               SetBackButtonVisiblity();

               // Add the menu if that is allowed.
               _menuButton = CreateNavBarButton(HAMBURGER_IMAGE, MenuButtonTapped);
               _menuButton.HorizontalOptions = LayoutOptions.End;
               _menuButton.SetUpBinding(IsVisibleProperty, nameof(IsMenuLoaded));
               grid.Children.Add(_menuButton, 3, 0);

               // Bind the title, center with margins and overlay Currently depends on the page being navigable
               _titleLabel =
                  FormsUtils.GetSimpleLabel
                     (
                      textColor: Color.White,
                      fontNamedSize: NamedSize.Large,
                      fontAttributes: FontAttributes.Bold,
                      textAlignment: TextAlignment.Start,
                      labelBindingPropertyName: nameof(IPageViewModelBase.PageTitle)
                     );
               _titleLabel.BackgroundColor = Color.Transparent;
               _titleLabel.HorizontalOptions = LayoutOptions.Center;
               _titleLabel.LineBreakMode = LineBreakMode.WordWrap;

               grid.Children.Add(_titleLabel, 2, 0);
            }

            Content = grid;

            // In case the binding context already exists
            SetUpHostingBindingContexts();
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is menu loaded.
      /// </summary>
      /// <value><c>true</c> if this instance is menu loaded; otherwise, <c>false</c>.</value>
      private bool IsMenuLoaded { get; set; }

      /// <summary>
      /// Gets a value indicating whether this instance is navigation available.
      /// </summary>
      /// <value><c>true</c> if this instance is navigation available; otherwise, <c>false</c>.</value>
      private bool IsNavigationAvailable => _appStateBackButtonStack.IsNotEmpty();

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         ReleaseUnmanagedResources();
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Creates the nav bar button.
      /// </summary>
      /// <param name="imagePath">The image path.</param>
      /// <param name="menuButtonTapped">The menu button tapped.</param>
      /// <returns>Image.</returns>
      private static Image CreateNavBarButton(string imagePath,
                                              EventHandler menuButtonTapped)
      {
         var retImage = FormsUtils.GetImage(imagePath, height: BUTTON_HEIGHT);

         var imageTap = new TapGestureRecognizer();
         imageTap.Tapped += menuButtonTapped;
         retImage.GestureRecognizers.Add(imageTap);
         retImage.VerticalOptions = LayoutOptions.Center;

         return retImage;
      }

      /// <summary>
      /// Called when [application state changed].
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="appStateChangedMessage">The application state changed message.</param>
      private static void OnAppStateChanged(object sender,
                                            AppStateChangedMessage appStateChangedMessage)
      {
         // If no old state, do nothing
         if (appStateChangedMessage.Payload.OldAppState.IsEmpty())
         {
            // Wipe out the stack and restart
            _appStateBackButtonStack.Clear();
            return;
         }

         // Get rid of the old app state -- it might be disorganized in the stack
         _appStateBackButtonStack.RemoveIfPresent
            (
             appStateChangedMessage.Payload.OldAppState,
             appState => appState.IsSameAs(appStateChangedMessage.Payload.OldAppState)
            );

         // Push the old app state to the top of the stack so it so navigation makes sense
         if (!appStateChangedMessage.Payload.PreventNavStackPush)
         {
            _appStateBackButtonStack.Push(appStateChangedMessage.Payload.OldAppState);
         }

         // Reset the back button as needed
         AskToSetBackButtonVisiblity?.Invoke();
      }

      /// <summary>
      /// Called when [hosting page changed].
      /// </summary>
      /// <param name="bindable">The bindable.</param>
      /// <param name="oldvalue">The oldvalue.</param>
      /// <param name="newvalue">The newvalue.</param>
      private static void OnHostingPageChanged(BindableObject bindable,
                                               object oldvalue,
                                               object newvalue)
      {
         if (bindable is NavAndMenuBar bindableAsNavAndMenuBar)
         {
            bindableAsNavAndMenuBar.HostingPage = newvalue as Page;
         }
      }

      /// <summary>
      /// Removes the button tapped listeners.
      /// </summary>
      /// <param name="imageButton">The image button.</param>
      /// <param name="buttonTapped">The button tapped.</param>
      private static void RemoveButtonTappedListeners(Image imageButton,
                                                      EventHandler buttonTapped)
      {
         if (imageButton.GestureRecognizers.IsNotEmpty())
         {
            var tappableGesture = imageButton.GestureRecognizers.OfType<TapGestureRecognizer>().FirstOrDefault();
            if (tappableGesture != null)
            {
               tappableGesture.Tapped -= buttonTapped;
            }
         }
      }

      /// <summary>
      /// Backs the button tapped.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="eventArgs">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void BackButtonTapped(object sender,
                                    EventArgs eventArgs)
      {
         // Navigate back if possible --
         // the button will be disabled if we cannot go back
         if (IsNavigationAvailable)
         {
            // Remove the top app state the stack
            var nextAppState = _appStateBackButtonStack.Pop();

            // Get the app state; Do not add to the back stack, since we are going backwards
            _stateMachine.GoToAppState(nextAppState, true);
         }

         SetBackButtonVisiblity();
      }

      /// <summary>
      /// Menus the button tapped.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void MenuButtonTapped(object sender,
                                    EventArgs e)
      {
         if (_menuButtonEntered)
         {
            return;
         }

         _menuButtonEntered = true;

         // Notify the host page so it can close the menu. Ask to close the menu as if the user
         // tapped the hamburger icon.
         FormsMessengerUtils.Send(new NavBarMenuTappedMessage());

         _menuButtonEntered = false;
      }

      /// <summary>
      /// Handles the <see cref="E:HostingPageBindingContextChanged" /> event.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void OnHostingPageBindingContextChanged(object sender,
                                                      EventArgs e)
      {
         SetUpHostingBindingContexts();
      }

      /// <summary>
      /// Called when [menu loaded].
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="args">The arguments.</param>
      private void OnMenuLoaded(object sender,
                                MenuLoadedMessage args)
      {
         IsMenuLoaded = true;
      }

      /// <summary>
      /// Releases the unmanaged resources.
      /// </summary>
      private void ReleaseUnmanagedResources()
      {
         AskToSetBackButtonVisiblity -= SetBackButtonVisiblity;

         RemoveButtonTappedListeners(_backButton, BackButtonTapped);

         RemoveButtonTappedListeners(_menuButton, MenuButtonTapped);
      }

      /// <summary>
      /// Removes the hosting page binding context changed handler.
      /// </summary>
      private void RemoveHostingPageBindingContextChangedHandler()
      {
         if (_hostingPage != null)
         {
            _hostingPage.BindingContextChanged -= OnHostingPageBindingContextChanged;
         }
      }

      /// <summary>
      /// Sets the back button visiblity.
      /// </summary>
      private void SetBackButtonVisiblity()
      {
         if (_backButton == null)
         {
            return;
         }

         _backButton.IsVisible = IsNavigationAvailable;
      }

      /// <summary>
      /// Sets up hosting binding contexts.
      /// </summary>
      private void SetUpHostingBindingContexts()
      {
         BindingContext = _hostingPage.BindingContext;
         _menuButton.BindingContext = this;
         _titleLabel.BindingContext = _hostingPage.BindingContext;
      }

      /// <summary>
      /// Finalizes an instance of the <see cref="NavAndMenuBar" /> class.
      /// </summary>
      ~NavAndMenuBar()
      {
         ReleaseUnmanagedResources();
      }
   }
}
