// *********************************************************************************
// <copyright file=FormsMessengerUtils.cs company="Marcus Technical Services, Inc.">
//     Copyright @2019 Marcus Technical Services, Inc.
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

namespace Com.MarcusTS.SharedForms.Common.Notifications
{
   using Com.MarcusTS.SharedForms.Common.Interfaces;
   using Com.MarcusTS.SharedForms.Common.Services;
   using Com.MarcusTS.SharedForms.ViewModels;
   using System;
   using Xamarin.Forms;

   /// <summary>
   ///    Interface IDeviceSizeChangeMessageArgs
   /// </summary>
   public interface IDeviceSizeChangeMessageArgs
   {
      /// <summary>
      ///    Gets or sets the height of the screen.
      /// </summary>
      /// <value>The height of the screen.</value>
      float ScreenHeight { get; set; }

      /// <summary>
      ///    Gets or sets the width of the screen.
      /// </summary>
      /// <value>The width of the screen.</value>
      float ScreenWidth { get; set; }
   }

   /// <summary>
   ///    Interface IMessage
   /// </summary>
   public interface IMessage
   {
   }

   /// <summary>
   ///    Interface IPageLifecycleMessageArgs
   /// </summary>
   public interface IPageLifecycleMessageArgs
   {
      /// <summary>
      ///    Gets or sets the page event.
      /// </summary>
      /// <value>The page event.</value>
      IProvidePageEvents SendingPage { get; set; }
   }

   /// <summary>
   ///    A global static utility library to assist with Xamarin.Forms.MessagingCenter calls.
   /// </summary>
   public static class FormsMessengerUtils
   {
      /// <summary>
      ///    Sends the specified message.
      /// </summary>
      /// <typeparam name="TMessage">The type of the t message.</typeparam>
      /// <param name="message">The message.</param>
      /// <param name="sender">The sender.</param>
      public static void Send<TMessage>
      (
         TMessage message,
         object   sender = null
      )
         where TMessage : IMessage
      {
         if (sender == null)
         {
            sender = new object();
         }

         MessagingCenter.Send(sender, typeof(TMessage).FullName, message);
      }

      /// <summary>
      ///    Subscribes the specified subscriber.
      /// </summary>
      /// <typeparam name="TMessage">The type of the t message.</typeparam>
      /// <param name="subscriber">The subscriber.</param>
      /// <param name="callback">The callback.</param>
      public static void Subscribe<TMessage>
      (
         object                   subscriber,
         Action<object, TMessage> callback
      )
         where TMessage : IMessage
      {
         MessagingCenter.Subscribe(subscriber, typeof(TMessage).FullName, callback);
      }

      /// <summary>
      ///    Unsubscribes the specified subscriber.
      /// </summary>
      /// <typeparam name="TMessage">The type of the t message.</typeparam>
      /// <param name="subscriber">The subscriber.</param>
      public static void Unsubscribe<TMessage>(object subscriber)
         where TMessage : IMessage
      {
         MessagingCenter.Unsubscribe<object, TMessage>(subscriber, typeof(TMessage).FullName);
      }
   }

   /// <summary>
   ///    Class AppStateChangedMessage.
   ///    Implements the <see cref="AppStateChangeMessageArgs" />
   ///    Implements the
   ///    <see
   ///       cref="AppStateChangeMessageArgs" />
   /// </summary>
   /// <seealso
   ///    cref="AppStateChangeMessageArgs" />
   /// <seealso cref="AppStateChangeMessageArgs" />
   public class AppStateChangedMessage : GenericMessageWithPayload<AppStateChangeMessageArgs>
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="AppStateChangedMessage" /> class.
      /// </summary>
      /// <param name="oldAppState">Old state of the application.</param>
      /// <param name="preventNavStackPush">if set to <c>true</c> [prevent nav stack push].</param>
      public AppStateChangedMessage
      (
         string oldAppState,
         bool   preventNavStackPush
      )
      {
         Payload = new AppStateChangeMessageArgs(oldAppState, preventNavStackPush);
      }
   }

   /// <summary>
   ///    Class AppStateChangeMessageArgs.
   /// </summary>
   public class AppStateChangeMessageArgs
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="AppStateChangeMessageArgs" /> class.
      /// </summary>
      /// <param name="oldAppState">Old state of the application.</param>
      /// <param name="preventNavStackPush">if set to <c>true</c> [prevent nav stack push].</param>
      public AppStateChangeMessageArgs
      (
         string oldAppState,
         bool   preventNavStackPush
      )
      {
         OldAppState         = oldAppState;
         PreventNavStackPush = preventNavStackPush;
      }

      /// <summary>
      ///    Gets or sets the old state of the application.
      /// </summary>
      /// <value>The old state of the application.</value>
      public string OldAppState { get; set; }

      /// <summary>
      ///    Gets or sets a value indicating whether [prevent nav stack push].
      /// </summary>
      /// <value><c>true</c> if [prevent nav stack push]; otherwise, <c>false</c>.</value>
      public bool PreventNavStackPush { get; set; }
   }

   /// <summary>
   ///    Notifies the app that the device size has changed
   ///    Implements the
   ///    <see
   ///       cref="DeviceSizeChangeMessageArgs" />
   /// </summary>
   /// <seealso
   ///    cref="DeviceSizeChangeMessageArgs" />
   /// <seealso cref="DeviceSizeChangeMessageArgs" />
   public class BroadcastDeviceSizeChangedMessage : GenericMessageWithPayload<DeviceSizeChangeMessageArgs>
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="BroadcastDeviceSizeChangedMessage" /> class.
      /// </summary>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      public BroadcastDeviceSizeChangedMessage
      (
         float width,
         float height
      )
      {
         Payload = new DeviceSizeChangeMessageArgs(width, height);
      }
   }

   /// <summary>
   ///    Class ConnectivityChangedMessage.
   ///    Implements the
   ///    <see cref="bool" />
   /// </summary>
   /// <seealso cref="bool" />
   public class ConnectivityChangedMessage : GenericMessageWithPayload<bool>
   {
   }

   /// <summary>
   ///    This message is issued as the args whenever a local platform senses a change in its orientation.
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.IDeviceSizeChangeMessageArgs" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.IDeviceSizeChangeMessageArgs" />
   /// <seealso cref="IDeviceSizeChangeMessageArgs" />
   public class DeviceSizeChangeMessageArgs : IDeviceSizeChangeMessageArgs
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="DeviceSizeChangeMessageArgs" /> class.
      /// </summary>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      public DeviceSizeChangeMessageArgs
      (
         float width,
         float height
      )
      {
         ScreenWidth  = width;
         ScreenHeight = height;
      }

      /// <summary>
      ///    Gets or sets the height of the screen.
      /// </summary>
      /// <value>The height of the screen.</value>
      public float ScreenHeight { get; set; }

      /// <summary>
      ///    Gets or sets the width of the screen.
      /// </summary>
      /// <value>The width of the screen.</value>
      public float ScreenWidth { get; set; }
   }

   /// <summary>
   ///    Class FragileServiceFailureMessage.
   ///    Implements the
   ///    <see
   ///       cref="IFragileService" />
   /// </summary>
   /// <seealso
   ///    cref="IFragileService" />
   public class FragileServiceFailureMessage : GenericMessageWithPayload<IFragileService>
   {
   }

   /// <summary>
   ///    Class FragileServiceSuccessMessage.
   ///    Implements the
   ///    <see
   ///       cref="IFragileService" />
   /// </summary>
   /// <seealso
   ///    cref="IFragileService" />
   public class FragileServiceSuccessMessage : GenericMessageWithPayload<IFragileService>
   {
   }

   /// <summary>
   ///    Class GenericMessageWithPayload.
   ///    Implements the <see cref="IMessage" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.IMessage" />
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.IMessage" />
   /// <seealso cref="IMessage" />
   public abstract class GenericMessageWithPayload<T> : IMessage
   {
      /// <summary>
      ///    Gets or sets the payload.
      /// </summary>
      /// <value>The payload.</value>
      public T Payload { get; set; }
   }

   /// <summary>
   ///    Notifies the orientation service that a the local device size has changed.
   ///    Do *not* use for general broadcast, as it will recur!
   ///    Implements the <see cref="DeviceSizeChangeMessageArgs" />
   ///    Implements the
   ///    <see
   ///       cref="DeviceSizeChangeMessageArgs" />
   /// </summary>
   /// <seealso
   ///    cref="DeviceSizeChangeMessageArgs" />
   /// <seealso cref="DeviceSizeChangeMessageArgs" />
   public class LocalDeviceSizeChangedMessage : GenericMessageWithPayload<DeviceSizeChangeMessageArgs>
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="LocalDeviceSizeChangedMessage" /> class.
      /// </summary>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      public LocalDeviceSizeChangedMessage
      (
         float width,
         float height
      )
      {
         Payload = new DeviceSizeChangeMessageArgs(width, height);
      }
   }

   /// <summary>
   ///    Class MainPageBindingContextChangeRequestMessage.
   ///    Implements the <see cref="IViewModelBase" />
   ///    Implements the
   ///    <see
   ///       cref="IViewModelBase" />
   /// </summary>
   /// <seealso
   ///    cref="IViewModelBase" />
   /// <seealso cref="IViewModelBase" />
   public class MainPageBindingContextChangeRequestMessage : GenericMessageWithPayload<IViewModelBase>
   {
      /// <summary>
      ///    Gets or sets a value indicating whether [prevent nav stack push].
      /// </summary>
      /// <value><c>true</c> if [prevent nav stack push]; otherwise, <c>false</c>.</value>
      public bool PreventNavStackPush { get; set; }
   }

   /// <summary>
   ///    Class MainPageChangeRequestMessage.
   ///    Implements the <see cref="GenericMessageWithPayload{T}.Forms.Page}" />
   ///    Implements the
   ///    <see cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Xamarin.Forms.Page}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Xamarin.Forms.Page}" />
   /// <seealso cref="GenericMessageWithPayload{T}.Forms.Page}" />
   public class MainPageChangeRequestMessage : GenericMessageWithPayload<Page>
   {
      /// <summary>
      ///    Gets or sets a value indicating whether [prevent nav stack push].
      /// </summary>
      /// <value><c>true</c> if [prevent nav stack push]; otherwise, <c>false</c>.</value>
      public bool PreventNavStackPush { get; set; }
   }

   /// <summary>
   ///    Class MenuLoadedMessage.
   ///    Implements the <see cref="NoPayloadMessage" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.NoPayloadMessage" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.NoPayloadMessage" />
   /// <seealso cref="NoPayloadMessage" />
   public class MenuLoadedMessage : NoPayloadMessage
   {
   }

   /// <summary>
   ///    Class NavBarMenuTappedMessage.
   ///    Implements the <see cref="NoPayloadMessage" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.NoPayloadMessage" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.NoPayloadMessage" />
   /// <seealso cref="NoPayloadMessage" />
   public class NavBarMenuTappedMessage : NoPayloadMessage
   {
   }

   /// <summary>
   ///    Class NoPayloadMessage.
   ///    Implements the <see cref="IMessage" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.IMessage" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.IMessage" />
   /// <seealso cref="IMessage" />
   public class NoPayloadMessage : IMessage
   {
   }

   /// <summary>
   ///    Class NotifyPageCanBeSavedChangedMessage.
   ///    Implements the
   ///    <see
   ///       cref="IStatefulViewModel" />
   /// </summary>
   /// <seealso
   ///    cref="IStatefulViewModel" />
   public class NotifyPageCanBeSavedChangedMessage : GenericMessageWithPayload<IStatefulViewModel>
   {
   }

   /// <summary>
   ///    Class ObjectDisappearingMessage.
   ///    Implements the <see cref="object" />
   ///    Implements the <see cref="object" />
   /// </summary>
   /// <seealso cref="object" />
   /// <seealso cref="object" />
   public class ObjectDisappearingMessage : GenericMessageWithPayload<object>
   {
   }

   /*
   /// <summary>
   /// Class PageLifecycleMessage.
   /// Implements the <see cref="IPageLifecycleMessageArgs" />
   /// </summary>
   /// <seealso cref="IPageLifecycleMessageArgs" />
   public class PageLifecycleMessage : GenericMessageWithPayload<IPageLifecycleMessageArgs>
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="PageLifecycleMessage" /> class.
      /// </summary>
      /// <param name="sendingPage">The sending page.</param>
      /// <param name="pageEvent">The page event.</param>
      //public PageLifecycleMessage(IProvidePageEvents  sendingPage,
      //                            PageLifecycleEventsEnum pageEvent)
      //{
      //   Payload = new PageLifecycleMessageArgs(sendingPage, pageEvent);
      //}
   }

   /// <summary>
   /// Class PageLifecycleMessageArgs.
   /// Implements the <see cref="IPageLifecycleMessageArgs" />
   /// </summary>
   /// <seealso cref="IPageLifecycleMessageArgs" />
   public class PageLifecycleMessageArgs : IPageLifecycleMessageArgs
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="PageLifecycleMessageArgs" /> class.
      /// </summary>
      /// <param name="sendingPage">The sending page.</param>
      /// <param name="pageEvent">The page event.</param>
      public PageLifecycleMessageArgs(IProvidePageEvents  sendingPage,
                                      PageLifecycleEventsEnum pageEvent)
      {
         SendingPage = sendingPage;
         PageEvent   = pageEvent;
      }

      /// <summary>
      /// Gets or sets the page event.
      /// </summary>
      /// <value>The page event.</value>
      public PageLifecycleEventsEnum PageEvent   { get; set; }
      /// <summary>
      /// Gets or sets the sending page.
      /// </summary>
      /// <value>The sending page.</value>
      public IProvidePageEvents  SendingPage { get; set; }
   }
   */

   /// <summary>
   ///    Class RefreshUIViewMessage.
   ///    Implements the
   ///    <see
   ///       cref="IStatefulViewModel" />
   /// </summary>
   /// <seealso
   ///    cref="IStatefulViewModel" />
   public class RefreshUIViewMessage : GenericMessageWithPayload<IStatefulViewModel>
   {
   }
}