// *********************************************************************************
// Copyright @2020 Marcus Technical Services, Inc.
// <copyright
// file=StateMachineBase.cs
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

namespace Com.MarcusTS.SharedForms.Common.Navigation
{
   using Notifications;
   using SharedUtils.Interfaces;
   using SharedUtils.Utils;
   using System;
   using System.Linq;
   using System.Threading.Tasks;
   using Xamarin.Forms;
   using Xamarin.Forms.Internals;

   /// <summary>
   ///    Interface IStateMachineBase
   ///    Implements the <see cref="System.IDisposable" />
   /// </summary>
   /// <seealso cref="System.IDisposable" />
   public interface IStateMachine : IDisposable
   {
      // A way of knowing the current app state, though this should not be commonly referenced.
      // string CurrentAppState { get; }

      /// <summary>
      ///    Gets the menu items.
      /// </summary>
      /// <value>The menu items.</value>
      IMenuNavigationState[] MenuItems { get; }

      // The normal way of changing states
      /// <summary>
      ///    Goes the state of to application.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      Task GoToAppState(string newState,
         bool preventStackPush = false);

      // Goes to the default landing page; for convenience only
      /// <summary>
      ///    Goes to landing page.
      /// </summary>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      Task GoToLandingPage(bool preventStackPush = true);

      // Sets the startup state for the app on initial start (or restart).
      /// <summary>
      ///    Goes the state of to start up.
      /// </summary>
      Task GoToStartUpState();
   }

   /// <summary>
   ///    A controller to manage which views and view models are shown for a given state
   ///    Implements the <see cref="IStateMachine" />
   /// </summary>
   /// <seealso cref="IStateMachine" />
   public abstract class StateMachine : IStateMachine
   {
      /// <summary>
      ///    The last application state
      /// </summary>
      private string _lastAppState;

      /// <summary>
      ///    Gets the application states.
      /// </summary>
      /// <value>The application states.</value>
      public abstract string[] APP_STATES { get; }

      /// <summary>
      ///    Gets the state of the application start up.
      /// </summary>
      /// <value>The state of the application start up.</value>
      public abstract string AppStartUpState { get; }

      /// <summary>
      ///    Gets the menu items.
      /// </summary>
      /// <value>The menu items.</value>
      public abstract IMenuNavigationState[] MenuItems { get; }

      /// <summary>
      ///    Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         ReleaseUnmanagedResources();
         GC.SuppressFinalize(this);
      }

      /// <summary>
      ///    Goes the state of to application.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      public Task GoToAppState(string newState,
         bool preventStackPush)
      {
         if (_lastAppState.IsSameAs(newState))
         {
            return Task.CompletedTask;
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
         return RespondToAppStateChange(newState, menuData, preventStackPush);
      }

      /// <summary>
      ///    Goes to landing page.
      /// </summary>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      public abstract Task GoToLandingPage(bool preventStackPush = true);

      // public string CurrentAppState { get; private set; } Sets the startup state for the app on
      // initial start (or restart).
      /// <summary>
      ///    Goes the state of to start up.
      /// </summary>
      public Task GoToStartUpState()
      {
         FormsMessengerUtils.Send(new AppStartUpMessage());

         return GoToAppState(AppStartUpState, true);
      }

      /// <summary>
      ///    Gets the state of the menu order from application.
      /// </summary>
      /// <param name="appState">State of the application.</param>
      /// <returns>System.Int32.</returns>
      protected int GetMenuOrderFromAppState(string appState)
      {
         return APP_STATES.IndexOf(appState);
      }

      /// <summary>
      ///    Releases the unmanaged resources.
      /// </summary>
      protected virtual void ReleaseUnmanagedResources()
      {
      }

      /// <summary>
      ///    Responds to application state change.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="menuData">The menu data.</param>
      /// <param name="preventStackPush">if set to <c>true</c> [prevent stack push].</param>
      protected abstract Task RespondToAppStateChange(string newState,
         IMenuNavigationState menuData,
         bool preventStackPush);

      /// <summary>
      ///    Finalizes an instance of the <see cref="StateMachine" /> class.
      /// </summary>
      ~StateMachine()
      {
         ReleaseUnmanagedResources();
      }

      /// <summary>
      ///    Class AppStartUpMessage.
      ///    Implements the <see cref="NoPayloadMessage" />
      /// </summary>
      /// <seealso cref="NoPayloadMessage" />
      public class AppStartUpMessage : NoPayloadMessage
      {
      }
   }
}
