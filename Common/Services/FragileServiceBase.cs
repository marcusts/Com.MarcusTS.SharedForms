#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, FragileServiceBase.cs, is a part of a program called AccountViewMobile.
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

#define FORCE_FOREGROUND_LISTENING

namespace Com.MarcusTS.SharedForms.Common.Services
{
   using Notifications;
   using System;
   using System.Diagnostics;
   using System.Threading;
   using System.Threading.Tasks;
   using Utils;
   using Xamarin.Forms;

   /// <summary>
   /// For identifying services that can fail and might require a response by the StateMachine.
   /// </summary>
   public interface IFragileService
   {
      /// <summary>
      /// Gets a value indicating whether this instance can be forced on.
      /// </summary>
      /// <value><c>true</c> if this instance can be forced on; otherwise, <c>false</c>.</value>
      bool CanBeForcedOn { get; }

      /// <summary>
      /// Gets a value indicating whether this instance can turn on from settings.
      /// </summary>
      /// <value><c>true</c> if this instance can turn on from settings; otherwise, <c>false</c>.</value>
      bool CanTurnOnFromSettings { get; }

      /// <summary>
      /// Gets a value indicating whether this instance is available.
      /// </summary>
      /// <value><c>true</c> if this instance is available; otherwise, <c>false</c>.</value>
      bool IsAvailable { get; }

      /// <summary>
      /// Gets a value indicating whether this instance is on.
      /// </summary>
      /// <value><c>true</c> if this instance is on; otherwise, <c>false</c>.</value>
      bool IsOn { get; }

      /// <summary>
      /// Gets a value indicating whether this instance is service listening.
      /// </summary>
      /// <value><c>true</c> if this instance is service listening; otherwise, <c>false</c>.</value>
      bool IsServiceListening { get; }

      /// <summary>
      /// Gets the name of the service.
      /// </summary>
      /// <value>The name of the service.</value>
      string ServiceName { get; }

      /// <summary>
      /// Cancels all activities.
      /// </summary>
      void CancelAllActivities();

      /// <summary>
      /// Goes to settings.
      /// </summary>
      void GoToSettings();

      /// <summary>
      /// Offers to force on.
      /// </summary>
      /// <returns>Task&lt;System.Boolean&gt;.</returns>
      Task<bool> OfferToForceOn();

      /// <summary>
      /// Restarts the when possible.
      /// </summary>
      void RestartWhenPossible();

      /// <summary>
      /// Sets the state of the fail.
      /// </summary>
      void SetFailState();

      /// <summary>
      /// Starts up.
      /// </summary>
      /// <returns>Task.</returns>
      Task StartUp();
   }

   /// <summary>
   /// Class FragileServiceBase.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Services.IFragileService" />
   /// Implements the <see cref="System.IDisposable" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Services.IFragileService" />
   /// <seealso cref="System.IDisposable" />
   public abstract class FragileServiceBase : IFragileService, IDisposable
   {
      /// <summary>
      /// The wait seconds
      /// </summary>
      private const int WAIT_SECONDS = 1;

      /// <summary>
      /// The start cancellation token source
      /// </summary>
      private readonly CancellationTokenSource _startCancellationTokenSource = new CancellationTokenSource();

      /// <summary>
      /// The stop cancellation token source
      /// </summary>
      private CancellationTokenSource _stopCancellationTokenSource = new CancellationTokenSource();

      /// <summary>
      /// The stop timer
      /// </summary>
      private volatile bool _stopTimer;

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         ReleaseUnmanagedResources();
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Gets a value indicating whether this instance can be forced on.
      /// </summary>
      /// <value><c>true</c> if this instance can be forced on; otherwise, <c>false</c>.</value>
      public abstract bool CanBeForcedOn { get; }

      /// <summary>
      /// Gets a value indicating whether this instance can turn on from settings.
      /// </summary>
      /// <value><c>true</c> if this instance can turn on from settings; otherwise, <c>false</c>.</value>
      public abstract bool CanTurnOnFromSettings { get; }

      /// <summary>
      /// Gets a value indicating whether this instance is available.
      /// </summary>
      /// <value><c>true</c> if this instance is available; otherwise, <c>false</c>.</value>
      public abstract bool IsAvailable { get; }

      /// <summary>
      /// Gets a value indicating whether this instance is on.
      /// </summary>
      /// <value><c>true</c> if this instance is on; otherwise, <c>false</c>.</value>
      public abstract bool IsOn { get; }

      /// <summary>
      /// Gets a value indicating whether this instance is service listening.
      /// </summary>
      /// <value><c>true</c> if this instance is service listening; otherwise, <c>false</c>.</value>
      public abstract bool IsServiceListening { get; }

      /// <summary>
      /// Gets the name of the service.
      /// </summary>
      /// <value>The name of the service.</value>
      public abstract string ServiceName { get; }

      /// <summary>
      /// Cancels all activities.
      /// </summary>
      public void CancelAllActivities()
      {
         // Stop Listening
         StopListeningInBackground();

         // Cancel all tokens
         _startCancellationTokenSource.Cancel();
         _stopTimer = true;

         // This one will get canceled inside the stop listening method
         // _stopCancellationTokenSource.Cancel();
      }

      /// <summary>
      /// Goes to settings.
      /// </summary>
      public abstract void GoToSettings();

      /// <summary>
      /// Offers to force on.
      /// </summary>
      /// <returns>Task&lt;System.Boolean&gt;.</returns>
      public abstract Task<bool> OfferToForceOn();

      /// <summary>
      /// 1. Set a timer
      /// 2. On tick see if the target service is available and can be started
      /// 3. When  it is, start
      /// </summary>
      public void RestartWhenPossible() =>

         // ELSE
         Device.StartTimer
         (
            TimeSpan.FromSeconds(WAIT_SECONDS),
            () =>
            {
               if (_stopTimer)
               {
                  return false;
               }

               if (IsOn)
               {
#if FORCE_FOREGROUND_LISTENING
                  Device.BeginInvokeOnMainThread
                  (
                     async () => { await StartListening().WithoutChangingContext(); }
                  );
#else
                  StartListeningInBackground();
#endif
                  return false;
               }

               // Continue waiting
               return true;
            }
         );

      /// <summary>
      /// Sets the state of the fail.
      /// </summary>
      public void SetFailState()
      {
         // Stop listening -- await for safety
         StopListeningInBackground();

         FormsMessengerUtils.Send(new FragileServiceFailureMessage {Payload = this});
      }

      /// <summary>
      /// Starts up.
      /// </summary>
      /// <returns>Task.</returns>
      public async Task StartUp()
      {
         // If the Fragile is available, start now
         if (IsOn)
         {
#if FORCE_FOREGROUND_LISTENING
            await StartListening().WithoutChangingContext();
#else // WARNING: This is on a background thread.
            StartListeningInBackground();
#endif
         }

         // ELSE something is wrong.

         // Warn the StateMachine that they need to change the state to handle this situation
         SetFailState();
      }

      /// <summary>
      /// Finalizes an instance of the <see cref="FragileServiceBase" /> class.
      /// </summary>
      ~FragileServiceBase()
      {
         ReleaseUnmanagedResources();
      }

      /// <summary>
      /// Adds the listeners.
      /// </summary>
      protected abstract void AddListeners();

      /// <summary>
      /// Removes the listeners.
      /// </summary>
      protected abstract void RemoveListeners();

      /// <summary>
      /// Starts the listening to service.
      /// </summary>
      /// <returns>Task.</returns>
      protected abstract Task StartListeningToService();

      /// <summary>
      /// Stops the listening to service.
      /// </summary>
      /// <returns>Task.</returns>
      protected abstract Task StopListeningToService();

      /// <summary>
      /// Releases the unmanaged resources.
      /// </summary>
      private void ReleaseUnmanagedResources()
      {
         StopListeningInBackground();
      }

      /// <summary>
      /// Starts the listening.
      /// </summary>
      /// <returns>Task.</returns>
      private async Task StartListening()
      {
         await StartListeningToService().WithoutChangingContext();

         // A precaution in case we have dangling or recursive cases
         RemoveListeners();

         // Assign the listeners
         AddListeners();

         // Notify of success or failure
         Device.BeginInvokeOnMainThread
         (
            () =>
            {
               if (IsServiceListening)
               {
                  FormsMessengerUtils.Send(new FragileServiceSuccessMessage {Payload = this});
               }
               else
               {
                  FormsMessengerUtils.Send(new FragileServiceFailureMessage {Payload = this});
               }
            }
         );
      }

      /// <summary>
      /// Stops the listening.
      /// </summary>
      /// <returns>Task.</returns>
      private async Task StopListening()
      {
         // A precaution to handle the case where we start and stop immediately.
         _stopCancellationTokenSource.Cancel(false);

         RemoveListeners();

         if (!IsServiceListening)
         {
            return;
         }

         await StopListeningToService().WithoutChangingContext();
      }

      /// <summary>
      /// Stops the listening in background.
      /// </summary>
      private void StopListeningInBackground()
      {
         if (!IsServiceListening)
         {
            return;
         }

         try
         {
            _stopCancellationTokenSource = new CancellationTokenSource();

            // No await, but can be canceled if it takes too long
            Task.Run(async () => { await StopListening().WithoutChangingContext(); },
                     _stopCancellationTokenSource.Token);
         }
         catch (Exception ex)
         {
            Debug.WriteLine(ex.Message);
         }
      }
   }
}