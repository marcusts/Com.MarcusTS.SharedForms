// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="StateMachineBase.cs" company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Navigation
{
   using System;
   using System.Linq;
   using SharedUtils.Interfaces;
   using SharedUtils.Utils;
   using Utils;
   using ViewModels;
   using Views.Pages;
   using Xamarin.Forms;
   using Xamarin.Forms.Internals;

   /// <summary>
   /// Interface IStateMachineBase
   /// Implements the <see cref="System.IDisposable" />
   /// </summary>
   /// <seealso cref="System.IDisposable" />
   public interface IStateMachineBase : IDisposable
   {
      // A way of knowing the current app state, though this should not be commonly referenced.
      // string CurrentAppState { get; }

      #region Public Properties

      /// <summary>
      /// Gets the menu items.
      /// </summary>
      /// <value>The menu items.</value>
      IMenuNavigationState[] MenuItems { get; }

      #endregion Public Properties

      #region Public Methods

      // The normal way of changing states
      /// <summary>
      /// Goes the state of to application.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      void GoToAppState(string newState,
                        bool   preventStackPush = false);

      // Goes to the default landing page; for convenience only
      /// <summary>
      /// Goes to landing page.
      /// </summary>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      void GoToLandingPage(bool preventStackPush = true);

      // Sets the startup state for the app on initial start (or restart).
      /// <summary>
      /// Goes the state of to start up.
      /// </summary>
      void GoToStartUpState();

      #endregion Public Methods
   }

   /// <summary>
   /// A controller to manage which views and view models are shown for a given state
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Navigation.IStateMachineBase" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Navigation.IStateMachineBase" />
   public abstract class StateMachineBase : IStateMachineBase
   {
      #region Private Destructors

      /// <summary>
      /// Finalizes an instance of the <see cref="StateMachineBase" /> class.
      /// </summary>
      ~StateMachineBase()
      {
         ReleaseUnmanagedResources();
      }

      #endregion Private Destructors

      #region Private Methods

      /// <summary>
      /// Seeks the page event provider.
      /// </summary>
      /// <param name="viewModelCreator">The view model creator.</param>
      /// <param name="page">The page.</param>
      /// <returns>IViewModelBase.</returns>
      private static IViewModelBase SeekPageEventProvider(Func<IViewModelBase> viewModelCreator,
                                                          Page                 page)
      {
         var viewModel = viewModelCreator?.Invoke();

         if (viewModel != null)
         {
            // Corner case: hard to pass along the page as page event provider when the page is
            // created in an expression, so assigning it here.
            if (viewModel is IReceivePageEvents viewModelAsPageEventsReceiver &&
                page is IProvidePageEvents pageAsPageEventsProvider)
            {
               viewModelAsPageEventsReceiver.PageEventProvider = pageAsPageEventsProvider;
            }
         }

         return viewModel;
      }

      #endregion Private Methods

      #region Public Classes

      /// <summary>
      /// Class AppStartUpMessage.
      /// Implements the <see cref="Com.MarcusTS.SharedForms.Utils.NoPayloadMessage" />
      /// </summary>
      /// <seealso cref="Com.MarcusTS.SharedForms.Utils.NoPayloadMessage" />
      public class AppStartUpMessage : NoPayloadMessage
      { }

      #endregion Public Classes

      #region Private Fields

      /// <summary>
      /// The last application state
      /// </summary>
      private string _lastAppState;

      /// <summary>
      /// The last page
      /// </summary>
      private Page _lastPage;

      #endregion Private Fields

      #region Public Properties

      /// <summary>
      /// Gets the application states.
      /// </summary>
      /// <value>The application states.</value>
      public abstract string[] APP_STATES { get; }

      /// <summary>
      /// Gets the state of the application start up.
      /// </summary>
      /// <value>The state of the application start up.</value>
      public abstract string AppStartUpState { get; }

      /// <summary>
      /// Gets the menu items.
      /// </summary>
      /// <value>The menu items.</value>
      public abstract IMenuNavigationState[] MenuItems { get; }

      #endregion Public Properties

      #region Public Methods

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         ReleaseUnmanagedResources();
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Goes the state of to application.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      public void GoToAppState(string newState,
                               bool   preventStackPush)
      {
         if (_lastAppState.IsSameAs(newState))
         {
            return;
         }

         // Raise an event to notify the nav bar that the back-stack requires modification. Send in
         // the last app state, *not* the new one.
         FormsMessengerUtils.Send(new AppStateChangedMessage(_lastAppState, preventStackPush));

         // CurrentAppState = newState;
         _lastAppState = newState;

         // match the menu data t the app state
         var menuData = MenuItems.FirstOrDefault(mi => mi.AppState.IsSameAs(newState));

         // Not awaiting here because we do not directly change the Application.Current.MainPage.
         // That is done through a message.
         RespondToAppStateChange(newState, menuData, preventStackPush);
      }

      /// <summary>
      /// Goes to landing page.
      /// </summary>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      public abstract void GoToLandingPage(bool preventStackPush = true);

      // public string CurrentAppState { get; private set; } Sets the startup state for the app on
      // initial start (or restart).
      /// <summary>
      /// Goes the state of to start up.
      /// </summary>
      public void GoToStartUpState()
      {
         FormsMessengerUtils.Send(new AppStartUpMessage());

         GoToAppState(AppStartUpState, true);
      }

      #endregion Public Methods

      #region Protected Methods

      /// <summary>
      /// Checks the against last page.
      /// </summary>
      /// <param name="pageType">Type of the page.</param>
      /// <param name="pageCreator">The page creator.</param>
      /// <param name="viewModelCreator">The view model creator.</param>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      protected void CheckAgainstLastPage(Type                 pageType,
                                          Func<Page>           pageCreator,
                                          Func<IViewModelBase> viewModelCreator,
                                          bool                 preventStackPush)
      {
         // If the same page, keep it
         if (_lastPage != null && _lastPage.GetType() == pageType)
         {
            var viewModel = SeekPageEventProvider(viewModelCreator, _lastPage);

            FormsMessengerUtils.Send
               (
                new MainPageBindingContextChangeRequestMessage
                {
                   Payload             = viewModelCreator?.Invoke(),
                   PreventNavStackPush = preventStackPush
                }
               );
            return;
         }

         // ELSE create both the page and view model

         // HACK to keep the static menu from being added as a child element to two separate containers.
         if (_lastPage is IMenuNavPageBase lastPageAsMenuNavPage)
         {
            lastPageAsMenuNavPage.RemoveMenuFromLayout();
         }

         // When  the page is created, it adds the menu to its own canvas
         var page = pageCreator?.Invoke();

         if (page != null)
         {
            var viewModel = SeekPageEventProvider(viewModelCreator, page);

            // Unconditional; null is a legal setting
            page.BindingContext = viewModel;

            FormsMessengerUtils.Send(new MainPageChangeRequestMessage
                                     {
                                        Payload             = page,
                                        PreventNavStackPush = preventStackPush
                                     });

            _lastPage = page;
         }
      }

      /// <summary>
      /// Gets the state of the menu order from application.
      /// </summary>
      /// <param name="appState">State of the application.</param>
      /// <returns>System.Int32.</returns>
      protected int GetMenuOrderFromAppState(string appState)
      {
         return APP_STATES.IndexOf(appState);
      }

      /// <summary>
      /// Releases the unmanaged resources.
      /// </summary>
      protected virtual void ReleaseUnmanagedResources()
      { }

      /// <summary>
      /// Responds to application state change.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="menuData">The menu data.</param>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      protected abstract void RespondToAppStateChange(string               newState,
                                                      IMenuNavigationState menuData,
                                                      bool                 preventStackPush);

      #endregion Protected Methods
   }
}