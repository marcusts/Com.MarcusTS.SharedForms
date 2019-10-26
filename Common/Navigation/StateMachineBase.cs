#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, StateMachineBase.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Navigation
{
   using Interfaces;
   using Notifications;
   using SharedUtils.Utils;
   using System;
   using System.Linq;
   using Xamarin.Forms.Internals;

   /// <summary>
   /// Interface IStateMachineBase
   /// Implements the <see cref="System.IDisposable" />
   /// </summary>
   /// <seealso cref="System.IDisposable" />
   public interface IStateMachine : IDisposable
   {
      // A way of knowing the current app state, though this should not be commonly referenced.
      // string CurrentAppState { get; }

      /// <summary>
      /// Gets the menu items.
      /// </summary>
      /// <value>The menu items.</value>
      IMenuNavigationState[] MenuItems { get; }

      // The normal way of changing states
      /// <summary>
      /// Goes the state of to application.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      void GoToAppState
      (
         string newState,
         bool   preventStackPush = false
      );

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
   }

   /// <summary>
   /// A controller to manage which views and view models are shown for a given state
   /// Implements the <see cref="IStateMachine" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Navigation.IStateMachine" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Navigation.IStateMachine" />
   /// <seealso cref="IStateMachine" />
   public abstract class StateMachineBase : IStateMachine
   {
      ///// <summary>
      /////    The last page
      ///// </summary>
      //private readonly Page _lastPage;

      /// <summary>
      /// The last application state
      /// </summary>
      private string _lastAppState;

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
      public void GoToAppState
      (
         string newState,
         bool   preventStackPush
      )
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

      /// <summary>
      /// Finalizes an instance of the <see cref="StateMachineBase" /> class.
      /// </summary>
      ~StateMachineBase()
      {
         ReleaseUnmanagedResources();
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
      {
      }

      /// <summary>
      /// Responds to application state change.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="menuData">The menu data.</param>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      protected abstract void RespondToAppStateChange
      (
         string               newState,
         IMenuNavigationState menuData,
         bool                 preventStackPush
      );

      /// <summary>
      /// Class AppStartUpMessage.
      /// Implements the <see cref="NoPayloadMessage" />
      /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.NoPayloadMessage" />
      /// </summary>
      /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.NoPayloadMessage" />
      /// <seealso cref="NoPayloadMessage" />
      public class AppStartUpMessage : NoPayloadMessage
      {
      }

      /*
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
      */
      /*
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
      */
   }
}