#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, MainMenu.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Views.SubViews
{
   using Common.Interfaces;
   using Common.Navigation;
   using Common.Notifications;
   using Common.Utils;
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
      private const bool ALLOW_EVENT_TUNNELING = false;

      /// <summary>
      /// The menu item width
      /// </summary>
      public static readonly double A_MENU_ITEM_WIDTH = 120.0;

      /// <summary>
      /// The menu outside single margin
      /// </summary>
      public static readonly double D_MENU_OUTSIDE_SINGLE_MARGIN = 15.0;

      /// <summary>
      /// The menu gross width
      /// </summary>
      public static readonly double E_MENU_GROSS_WIDTH = A_MENU_ITEM_WIDTH + 2 * D_MENU_OUTSIDE_SINGLE_MARGIN;

      /// <summary>
      /// The menu outside margin
      /// </summary>
      public static readonly Thickness F_MENU_OUTSIDE_MARGIN = new Thickness(D_MENU_OUTSIDE_SINGLE_MARGIN);

      /// <summary>
      /// The menu inside single margin
      /// </summary>
      public static readonly double G_MENU_INSIDE_SINGLE_MARGIN = D_MENU_OUTSIDE_SINGLE_MARGIN / 2;

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
      private readonly IStateMachine _stateMachine;

      /// <summary>
      /// The is menu loaded
      /// </summary>
      private bool _isMenuLoaded;

      /// <summary>
      /// Initializes a new instance of the <see cref="MainMenu" /> class.
      /// </summary>
      /// <param name="stateMachine">The state machine.</param>
      public MainMenu(IStateMachine stateMachine)
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
               WidthRequest      = A_MENU_ITEM_WIDTH,
               HeightRequest     = MENU_ITEM_HEIGHT,
               HorizontalOptions = LayoutOptions.Center,
               VerticalOptions   = LayoutOptions.Center,
               InputTransparent  = ALLOW_EVENT_TUNNELING
            };

         retButton.Clicked +=
            (
               s,
               e
            ) =>
            {
               // Ask to close the menu as if the user tapped the hamburger icon.
               FormsMessengerUtils.Send(new NavBarMenuTappedMessage());

               _stateMachine.GoToAppState(menuData.AppState);
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
         menuStack.Margin            = F_MENU_OUTSIDE_MARGIN;
         menuStack.Spacing           = G_MENU_INSIDE_SINGLE_MARGIN;
         menuStack.InputTransparent  = ALLOW_EVENT_TUNNELING;

         var singleMenuItemHeight = MENU_ITEM_HEIGHT + G_MENU_INSIDE_SINGLE_MARGIN;

         // Allow for the top and bottom margins, etc.
         MenuHeight = 2 * D_MENU_OUTSIDE_SINGLE_MARGIN;

         foreach (var menuData in _stateMachine.MenuItems)
         {
            menuStack.Children.Add(CreateMenuItemButton(menuData));
            MenuHeight += singleMenuItemHeight;
         }

         HeightRequest = MenuHeight;
         WidthRequest  = E_MENU_GROSS_WIDTH;

         var scroller = FormsUtils.GetExpandingScrollView();
         scroller.InputTransparent = ALLOW_EVENT_TUNNELING;
         scroller.Content          = menuStack;

         Content = scroller;

         IsMenuLoaded = true;
      }
   }
}