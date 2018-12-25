// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="FormsMessengerUtils.cs" company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Utils
{
   using System;
   using SharedUtils.Interfaces;
   using ViewModels;
   using Xamarin.Forms;

   /// <summary>
   /// Enum PageLifecycleEvents
   /// </summary>
   public enum PageLifecycleEvents
   {
      /// <summary>
      /// The before constructing
      /// </summary>
      BeforeConstructing,
      /// <summary>
      /// The after constructing
      /// </summary>
      AfterConstructing,
      /// <summary>
      /// The before appearing
      /// </summary>
      BeforeAppearing,
      /// <summary>
      /// The after appearing
      /// </summary>
      AfterAppearing,
      /// <summary>
      /// The before disappearing
      /// </summary>
      BeforeDisappearing,
      /// <summary>
      /// The after disappearing
      /// </summary>
      AfterDisappearing
   }


   /// <summary>
   /// Interface IDeviceSizeChangeMessageArgs
   /// </summary>
   public interface IDeviceSizeChangeMessageArgs
   {
      #region Public Properties

      /// <summary>
      /// Gets or sets the height of the screen.
      /// </summary>
      /// <value>The height of the screen.</value>
      float ScreenHeight { get; set; }
      /// <summary>
      /// Gets or sets the width of the screen.
      /// </summary>
      /// <value>The width of the screen.</value>
      float ScreenWidth  { get; set; }

      #endregion Public Properties
   }

   /// <summary>
   /// Interface IMessage
   /// </summary>
   public interface IMessage
   { }

   /// <summary>
   /// Interface IPageLifecycleMessageArgs
   /// </summary>
   public interface IPageLifecycleMessageArgs
   {
      #region Public Properties

      /// <summary>
      /// Gets or sets the page event.
      /// </summary>
      /// <value>The page event.</value>
      PageLifecycleEvents PageEvent   { get; set; }
      /// <summary>
      /// Gets or sets the sending page.
      /// </summary>
      /// <value>The sending page.</value>
      IProvidePageEvents SendingPage { get; set; }

      #endregion Public Properties
   }

   /// <summary>
   /// A global static utilty library to assist with Xamarin.Forms.MessagingCenter calls.
   /// </summary>
   public static class FormsMessengerUtils
   {
      #region Public Methods

      /// <summary>
      /// Sends the specified message.
      /// </summary>
      /// <typeparam name="TMessage">The type of the t message.</typeparam>
      /// <param name="message">The message.</param>
      /// <param name="sender">The sender.</param>
      public static void Send<TMessage>(TMessage message,
                                        object   sender = null) where TMessage : IMessage
      {
         if (sender == null)
         {
            sender = new object();
         }

         MessagingCenter.Send(sender, typeof(TMessage).FullName, message);
      }

      /// <summary>
      /// Subscribes the specified subscriber.
      /// </summary>
      /// <typeparam name="TMessage">The type of the t message.</typeparam>
      /// <param name="subscriber">The subscriber.</param>
      /// <param name="callback">The callback.</param>
      public static void Subscribe<TMessage>(object                   subscriber,
                                             Action<object, TMessage> callback)
         where TMessage : IMessage
      {
         MessagingCenter.Subscribe(subscriber, typeof(TMessage).FullName, callback);
      }

      /// <summary>
      /// Unsubscribes the specified subscriber.
      /// </summary>
      /// <typeparam name="TMessage">The type of the t message.</typeparam>
      /// <param name="subscriber">The subscriber.</param>
      public static void Unsubscribe<TMessage>(object subscriber) where TMessage : IMessage
      {
         MessagingCenter.Unsubscribe<object, TMessage>(subscriber, typeof(TMessage).FullName);
      }

      #endregion Public Methods
   }

   /// <summary>
   /// Class AppStateChangedMessage.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Utils.AppStateChangeMessageArgs}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Utils.AppStateChangeMessageArgs}" />
   public class AppStateChangedMessage : GenericMessageWithPayload<AppStateChangeMessageArgs>
   {
      #region Public Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="AppStateChangedMessage" /> class.
      /// </summary>
      /// <param name="oldAppState">Old state of the application.</param>
      /// <param name="preventNavStackPush">if set to <c>true</c> [prevent nav stack push].</param>
      public AppStateChangedMessage(string oldAppState,
                                    bool   preventNavStackPush)
      {
         Payload = new AppStateChangeMessageArgs(oldAppState, preventNavStackPush);
      }

      #endregion Public Constructors
   }

   /// <summary>
   /// Class AppStateChangeMessageArgs.
   /// </summary>
   public class AppStateChangeMessageArgs
   {
      #region Public Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="AppStateChangeMessageArgs" /> class.
      /// </summary>
      /// <param name="oldAppState">Old state of the application.</param>
      /// <param name="preventNavStackPush">if set to <c>true</c> [prevent nav stack push].</param>
      public AppStateChangeMessageArgs(string oldAppState,
                                       bool   preventNavStackPush)
      {
         OldAppState         = oldAppState;
         PreventNavStackPush = preventNavStackPush;
      }

      #endregion Public Constructors

      #region Public Properties

      /// <summary>
      /// Gets or sets the old state of the application.
      /// </summary>
      /// <value>The old state of the application.</value>
      public string OldAppState { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [prevent nav stack push].
      /// </summary>
      /// <value><c>true</c> if [prevent nav stack push]; otherwise, <c>false</c>.</value>
      public bool PreventNavStackPush { get; set; }

      #endregion Public Properties
   }

   /// <summary>
   /// Notifies the app that the device size has changed
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Utils.DeviceSizeChangeMessageArgs}" />
   public class BroadcastDeviceSizeChangedMessage : GenericMessageWithPayload<DeviceSizeChangeMessageArgs>
   {
      #region Public Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="BroadcastDeviceSizeChangedMessage" /> class.
      /// </summary>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      public BroadcastDeviceSizeChangedMessage(float width,
                                               float height)
      {
         Payload = new DeviceSizeChangeMessageArgs(width, height);
      }

      #endregion Public Constructors
   }

   /// <summary>
   /// This message is issued as the args whenever a local platform senses a change in its orientation.
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.IDeviceSizeChangeMessageArgs" />
   public class DeviceSizeChangeMessageArgs : IDeviceSizeChangeMessageArgs
   {
      #region Public Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="DeviceSizeChangeMessageArgs" /> class.
      /// </summary>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      public DeviceSizeChangeMessageArgs(float width,
                                         float height)
      {
         ScreenWidth  = width;
         ScreenHeight = height;
      }

      #endregion Public Constructors

      #region Public Properties

      /// <summary>
      /// Gets or sets the height of the screen.
      /// </summary>
      /// <value>The height of the screen.</value>
      public float ScreenHeight { get; set; }
      /// <summary>
      /// Gets or sets the width of the screen.
      /// </summary>
      /// <value>The width of the screen.</value>
      public float ScreenWidth  { get; set; }

      #endregion Public Properties
   }

   /// <summary>
   /// Class GenericMessageWithPayload.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Utils.IMessage" />
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.IMessage" />
   public abstract class GenericMessageWithPayload<T> : IMessage
   {
      #region Public Properties

      /// <summary>
      /// Gets or sets the payload.
      /// </summary>
      /// <value>The payload.</value>
      public T Payload { get; set; }

      #endregion Public Properties
   }

   /// <summary>
   /// Notifies the orientation service that a the local device size has changed.
   /// Do *not* use for general broadcast, as it will recur!
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Utils.DeviceSizeChangeMessageArgs}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Utils.DeviceSizeChangeMessageArgs}" />
   public class LocalDeviceSizeChangedMessage : GenericMessageWithPayload<DeviceSizeChangeMessageArgs>
   {
      #region Public Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="LocalDeviceSizeChangedMessage" /> class.
      /// </summary>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      public LocalDeviceSizeChangedMessage(float width,
                                           float height)
      {
         Payload = new DeviceSizeChangeMessageArgs(width, height);
      }

      #endregion Public Constructors
   }

   /// <summary>
   /// Class MainPageBindingContextChangeRequestMessage.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{Com.MarcusTS.SharedForms.ViewModels.IViewModelBase}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{Com.MarcusTS.SharedForms.ViewModels.IViewModelBase}" />
   public class MainPageBindingContextChangeRequestMessage : GenericMessageWithPayload<IViewModelBase>
   {
      #region Public Properties

      /// <summary>
      /// Gets or sets a value indicating whether [prevent nav stack push].
      /// </summary>
      /// <value><c>true</c> if [prevent nav stack push]; otherwise, <c>false</c>.</value>
      public bool PreventNavStackPush { get; set; }

      #endregion Public Properties
   }

   /// <summary>
   /// Class MainPageChangeRequestMessage.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{Xamarin.Forms.Page}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{Xamarin.Forms.Page}" />
   public class MainPageChangeRequestMessage : GenericMessageWithPayload<Page>
   {
      #region Public Properties

      /// <summary>
      /// Gets or sets a value indicating whether [prevent nav stack push].
      /// </summary>
      /// <value><c>true</c> if [prevent nav stack push]; otherwise, <c>false</c>.</value>
      public bool PreventNavStackPush { get; set; }

      #endregion Public Properties
   }

   /// <summary>
   /// Class MenuLoadedMessage.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Utils.NoPayloadMessage" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.NoPayloadMessage" />
   public class MenuLoadedMessage : NoPayloadMessage
   { }

   /// <summary>
   /// Class NavBarMenuTappedMessage.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Utils.NoPayloadMessage" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.NoPayloadMessage" />
   public class NavBarMenuTappedMessage : NoPayloadMessage
   { }

   /// <summary>
   /// Class NoPayloadMessage.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Utils.IMessage" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.IMessage" />
   public class NoPayloadMessage : IMessage
   { }

   /// <summary>
   /// Class ObjectDisappearingMessage.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{System.Object}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{System.Object}" />
   public class ObjectDisappearingMessage : GenericMessageWithPayload<object>
   { }

   /// <summary>
   /// Class PageLifecycleMessage.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Utils.IPageLifecycleMessageArgs}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Utils.IPageLifecycleMessageArgs}" />
   public class PageLifecycleMessage : GenericMessageWithPayload<IPageLifecycleMessageArgs>
   {
      #region Public Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="PageLifecycleMessage" /> class.
      /// </summary>
      /// <param name="sendingPage">The sending page.</param>
      /// <param name="pageEvent">The page event.</param>
      public PageLifecycleMessage(IProvidePageEvents  sendingPage,
                                  PageLifecycleEvents pageEvent)
      {
         Payload = new PageLifecycleMessageArgs(sendingPage, pageEvent);
      }

      #endregion Public Constructors
   }

   /// <summary>
   /// Class PageLifecycleMessageArgs.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Utils.IPageLifecycleMessageArgs" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Utils.IPageLifecycleMessageArgs" />
   public class PageLifecycleMessageArgs : IPageLifecycleMessageArgs
   {
      #region Public Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="PageLifecycleMessageArgs" /> class.
      /// </summary>
      /// <param name="sendingPage">The sending page.</param>
      /// <param name="pageEvent">The page event.</param>
      public PageLifecycleMessageArgs(IProvidePageEvents  sendingPage,
                                      PageLifecycleEvents pageEvent)
      {
         SendingPage = sendingPage;
         PageEvent   = pageEvent;
      }

      #endregion Public Constructors

      #region Public Properties

      /// <summary>
      /// Gets or sets the page event.
      /// </summary>
      /// <value>The page event.</value>
      public PageLifecycleEvents PageEvent   { get; set; }
      /// <summary>
      /// Gets or sets the sending page.
      /// </summary>
      /// <value>The sending page.</value>
      public IProvidePageEvents  SendingPage { get; set; }

      #endregion Public Properties
   }
}