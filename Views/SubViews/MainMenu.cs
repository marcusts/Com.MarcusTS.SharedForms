// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="MainMenu.cs" company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Views.SubViews
{
   using Navigation;
   using SharedUtils.Interfaces;
   using Utils;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IMainMenu
   /// </summary>
   public interface IMainMenu
   {
      /// <summary>
      /// Gets a value indicating whether this instance is menu loaded.
      /// </summary>
      /// <value><c>true</c> if this instance is menu loaded; otherwise, <c>false</c>.</value>
      bool IsMenuLoaded { get; }

      /// <summary>
      /// Gets the height of the menu.
      /// </summary>
      /// <value>The height of the menu.</value>
      double MenuHeight { get; }
   }

   /// <summary>
   /// Class MainMenu.
   /// Implements the <see cref="Xamarin.Forms.ContentView" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.SubViews.IMainMenu" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.ContentView" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.SubViews.IMainMenu" />
   public class MainMenu : ContentView, IMainMenu
   {
      /// <summary>
      /// The allow event tunneling
      /// </summary>
      private const          bool      ALLOW_EVENT_TUNNELING      = false;
      /// <summary>
      /// The menu gross width
      /// </summary>
      public static readonly double    MENU_GROSS_WIDTH           = MENU_ITEM_WIDTH + 2 * MENU_OUTSIDE_SINGLE_MARGIN;
      /// <summary>
      /// The menu inside single margin
      /// </summary>
      public static readonly double    MENU_INSIDE_SINGLE_MARGIN  = MENU_OUTSIDE_SINGLE_MARGIN / 2;
      /// <summary>
      /// The menu item width
      /// </summary>
      public static readonly double    MENU_ITEM_WIDTH            = 120.0;
      /// <summary>
      /// The menu outside margin
      /// </summary>
      public static readonly Thickness MENU_OUTSIDE_MARGIN        = new Thickness(MENU_OUTSIDE_SINGLE_MARGIN);
      /// <summary>
      /// The menu outside single margin
      /// </summary>
      public static readonly double    MENU_OUTSIDE_SINGLE_MARGIN = 15.0;

      /// <summary>
      /// The main menu opacity
      /// </summary>
      private static readonly double MAIN_MENU_OPACITY = 0.95;

      /// <summary>
      /// The menu item height
      /// </summary>
      private static readonly double MENU_ITEM_HEIGHT = 40.0;

      /// <summary>
      /// The state machine
      /// </summary>
      private readonly IStateMachineBase _stateMachine;

      /// <summary>
      /// The is menu loaded
      /// </summary>
      private bool _isMenuLoaded;

      /// <summary>
      /// Initializes a new instance of the <see cref="MainMenu" /> class.
      /// </summary>
      /// <param name="stateMachine">The state machine.</param>
      public MainMenu(IStateMachineBase stateMachine)
      {
         _stateMachine = stateMachine;

         // Not really used
         BindingContext = this;

         VerticalOptions   = LayoutOptions.StartAndExpand;
         HorizontalOptions = LayoutOptions.CenterAndExpand;

         BackgroundColor = ColorUtils.HEADER_AND_TOOLBAR_COLOR;
         Opacity         = MAIN_MENU_OPACITY;

         InputTransparent = ALLOW_EVENT_TUNNELING;

         LoadMenuFromStateMachine();
      }

      /// <summary>
      /// Gets a value indicating whether this instance is menu loaded.
      /// </summary>
      /// <value><c>true</c> if this instance is menu loaded; otherwise, <c>false</c>.</value>
      public bool IsMenuLoaded
      {
         get => _isMenuLoaded;
         private set
         {
            _isMenuLoaded = value;

            FormsMessengerUtils.Send(new MenuLoadedMessage());
         }
      }

      /// <summary>
      /// Gets the height of the menu.
      /// </summary>
      /// <value>The height of the menu.</value>
      public double MenuHeight { get; private set; }

      /// <summary>
      /// Creates the menu item button.
      /// </summary>
      /// <param name="menuData">The menu data.</param>
      /// <returns>Button.</returns>
      private Button CreateMenuItemButton(IMenuNavigationState menuData)
      {
         var retButton =
            new Button
            {
               Text              = menuData.MenuTitle,
               WidthRequest      = MENU_ITEM_WIDTH,
               HeightRequest     = MENU_ITEM_HEIGHT,
               HorizontalOptions = LayoutOptions.Center,
               VerticalOptions   = LayoutOptions.Center,
               InputTransparent  = ALLOW_EVENT_TUNNELING
            };

         retButton.Clicked += (s,
                               e) =>
                              {
                                 // Ask to close the menu as if the user tapped the hamburger icon.
                                 FormsMessengerUtils.Send(new NavBarMenuTappedMessage());

                                 _stateMachine.GoToAppState(menuData.AppState, false);
                              };

         return retButton;
      }

      /// <summary>
      /// Loads the menu from state machine.
      /// </summary>
      private void LoadMenuFromStateMachine()
      {
         // A grid to handle the entire menu
         var menuStack = FormsUtils.GetExpandingStackLayout();
         menuStack.VerticalOptions   = LayoutOptions.StartAndExpand;
         menuStack.HorizontalOptions = LayoutOptions.CenterAndExpand;
         menuStack.Margin            = MENU_OUTSIDE_MARGIN;
         menuStack.Spacing           = MENU_INSIDE_SINGLE_MARGIN;
         menuStack.InputTransparent  = ALLOW_EVENT_TUNNELING;

         var singleMenuItemHeight = MENU_ITEM_HEIGHT + MENU_INSIDE_SINGLE_MARGIN;

         // Allow for the top and bottom margins, etc.
         MenuHeight = 2 * MENU_OUTSIDE_SINGLE_MARGIN;

         foreach (var menuData in _stateMachine.MenuItems)
         {
            menuStack.Children.Add(CreateMenuItemButton(menuData));
            MenuHeight += singleMenuItemHeight;
         }

         HeightRequest = MenuHeight;
         WidthRequest  = MENU_GROSS_WIDTH;

         var scroller = FormsUtils.GetExpandingScrollView();
         scroller.InputTransparent = ALLOW_EVENT_TUNNELING;
         scroller.Content          = menuStack;

         Content = scroller;

         IsMenuLoaded = true;
      }
   }
}