// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="MenuNavPageBase.cs" company="Marcus Technical Services, Inc.">
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
#region License

// MIT License
//
// Copyright (c) 2018
// Marcus Technical Services, Inc.
// http://www.marcusts.com
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
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion License

// #define STORE_PAGE_MENU_STATICALLY

namespace Com.MarcusTS.SharedForms.Views.Pages
{
   #region Imports

   using System.Threading.Tasks;
   using DeviceServices;
   using PropertyChanged;
   using SubViews;
   using Utils;
   using ViewModels;
   using Xamarin.Forms;

   #endregion Imports

   /// <summary>
   /// Interface IMenuNavPageBase
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Pages.ITypeSafePageBase" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Pages.ITypeSafePageBase" />
   public interface IMenuNavPageBase : ITypeSafePageBase
   {
      #region Public Methods

      /// <summary>
      /// Removes the menu from layout.
      /// </summary>
      void RemoveMenuFromLayout();

      #endregion Public Methods
   }

   /// <summary>
   /// A page with a navigation header.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Pages.TypeSafePageBase{InterfaceT}" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Pages.IMenuNavPageBase" />
   /// </summary>
   /// <typeparam name="InterfaceT">The type of the interface t.</typeparam>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Pages.TypeSafePageBase{InterfaceT}" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Pages.IMenuNavPageBase" />
   [AddINotifyPropertyChangedInterface]
   public abstract class MenuNavPageBase<InterfaceT> : TypeSafePageBase<InterfaceT>, IMenuNavPageBase
      where InterfaceT : class, IPageViewModelBase
   {
      #region Public Constructors

#if STORE_PAGE_MENU_STATICALLY
     static MenuNavPageBase()
     {
       var stateMachine = AppContainer.GlobalVariableContainer.Resolve<IStateMachineBase>();
       PageMenu = new MainMenu(stateMachine);
     }
#endif

      #endregion Public Constructors

      #region Protected Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="MenuNavPageBase{InterfaceT}" /> class.
      /// </summary>
      protected MenuNavPageBase()
      {
         // Do not use "BeginLifetimeScope" because it does not seem to work. Also, the menu is
         // global for the life of the app.
#if !STORE_PAGE_MENU_STATICALLY
         // PageMenu = AppContainer.GlobalVariableContainer.Resolve<IMainMenu>();
         PageMenu = new MainMenu(null);
#endif

         FormsMessengerUtils.Subscribe<NavBarMenuTappedMessage>(this, OnMainMenuItemSelected);

         BackgroundColor = Color.Transparent;

         PageMenuView.Opacity = 0;

         var controlTemplateNotSet = true;

         BindingContextChanged +=
            (sender,
             args) =>
            {
               if (controlTemplateNotSet)
               {
                  ControlTemplate       = Application.Current.Resources[NAV_BAR_CONTROL_TEMPLATE] as ControlTemplate;
                  controlTemplateNotSet = false;
               }
            };

         _canvas.InputTransparent = true;
      }

      #endregion Protected Constructors

      #region Public Methods

      /// <summary>
      /// Removes the menu from layout.
      /// </summary>
      public void RemoveMenuFromLayout()
      {
         if (_canvas != null && _canvas.Children.Contains(PageMenuView))
         {
            _canvas.Children.Remove(PageMenuView);
         }
      }

      #endregion Public Methods

      #region Private Variables

      /// <summary>
      /// The menu animate milliseconds
      /// </summary>
      private const int MENU_ANIMATE_MILLISECONDS = 400;

      /// <summary>
      /// The menu fade milliseconds
      /// </summary>
      private const int MENU_FADE_MILLISECONDS = 200;

      /// <summary>
      /// The nav bar control template
      /// </summary>
      private const string NAV_BAR_CONTROL_TEMPLATE = "NavBarControlTemplate";

      /// <summary>
      /// The canvas
      /// </summary>
      private readonly AbsoluteLayout _canvas = FormsUtils.GetExpandingAbsoluteLayout();

      /// <summary>
      /// The is page menu showing
      /// </summary>
      private volatile bool _isPageMenuShowing;

      #endregion Private Variables

      #region Protected Properties

      /// <summary>
      /// Gets or sets the page menu.
      /// </summary>
      /// <value>The page menu.</value>
      protected
#if STORE_PAGE_MENU_STATICALLY
       static
#endif
         IMainMenu PageMenu { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is page menu showing.
      /// </summary>
      /// <value><c>true</c> if this instance is page menu showing; otherwise, <c>false</c>.</value>
      protected bool IsPageMenuShowing
      {
         get => _isPageMenuShowing;
         set => Device.BeginInvokeOnMainThread
            (
             async () =>
             {
                _isPageMenuShowing = value;

                await AnimatePageMenu().WithoutChangingContext();

                // HACK to fix dead UI in the main menu, which sits on top of this canvas
                _canvas.InputTransparent = !_isPageMenuShowing;
             }
            );
      }

      /// <summary>
      /// Gets the page menu view.
      /// </summary>
      /// <value>The page menu view.</value>
      protected View PageMenuView => PageMenu as View;

      #endregion Protected Properties

      #region Protected Methods

      /// <summary>
      /// Afters the content set.
      /// </summary>
      /// <param name="layout">The layout.</param>
      protected override void AfterContentSet(RelativeLayout layout)
      {
         // No need to add it twice
         if (_canvas == null || _canvas.Children.Contains(PageMenuView))
         {
            return;
         }

         PageMenuView.Opacity = 0;

         var targetRect = CreateOfflineRectangle();

         layout.CreateRelativeOverlay(_canvas);

         // A slight cheat; using protected property
         _canvas.Children.Add(PageMenuView, targetRect);
      }

      /// <summary>
      /// Called when [disappearing].
      /// </summary>
      protected override void OnDisappearing()
      {
         base.OnDisappearing();

         FormsMessengerUtils.Unsubscribe<NavBarMenuTappedMessage>(this);

         RemoveMenuFromLayout();
      }

      #endregion Protected Methods

      #region Private Methods

      /// <summary>
      /// Creates the offline rectangle.
      /// </summary>
      /// <returns>Rectangle.</returns>
      private Rectangle CreateOfflineRectangle()
      {
         return new Rectangle(OrientationService.ScreenWidth, 0, 0, PageMenu.MenuHeight);
      }

      /// <summary>
      /// Animates the panel in our out depending on the state
      /// </summary>
      /// <returns>Task.</returns>
      private async Task AnimatePageMenu()
      {
         if (IsPageMenuShowing)
         {
            // Slide the menu up from the bottom
            var rect =
               new Rectangle
                  (
                   Width - MainMenu.MENU_GROSS_WIDTH,
                   0,
                   MainMenu.MENU_GROSS_WIDTH,
                   PageMenu.MenuHeight
                  );

            await Task.WhenAll(PageMenuView.LayoutTo(rect, MENU_ANIMATE_MILLISECONDS, Easing.CubicIn),
                               PageMenuView.FadeTo(1.0, MENU_FADE_MILLISECONDS)).WithoutChangingContext();
         }
         else
         {
            // Retract the menu
            var rect = CreateOfflineRectangle();

            await Task.WhenAll(PageMenuView.LayoutTo(rect, MENU_ANIMATE_MILLISECONDS, Easing.CubicOut),
                               PageMenuView.FadeTo(0.0, MENU_FADE_MILLISECONDS)).WithoutChangingContext();
         }
      }

      /// <summary>
      /// Called when [main menu item selected].
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="args">The arguments.</param>
      private void OnMainMenuItemSelected(object                  sender,
                                          NavBarMenuTappedMessage args)
      {
         IsPageMenuShowing = !IsPageMenuShowing;
      }

      #endregion Private Methods
   }
}