#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, FormsMessengerUtils.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Notifications
{
   using Interfaces;
   using Services;
   using System;
   using ViewModels;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IDeviceSizeChangeMessageArgs
   /// </summary>
   public interface IDeviceSizeChangeMessageArgs
   {
      /// <summary>
      /// Gets or sets the height of the screen.
      /// </summary>
      /// <value>The height of the screen.</value>
      float ScreenHeight { get; set; }

      /// <summary>
      /// Gets or sets the width of the screen.
      /// </summary>
      /// <value>The width of the screen.</value>
      float ScreenWidth { get; set; }
   }

   /// <summary>
   /// Interface IKeyboardToggledMessageArgs
   /// </summary>
   public interface IKeyboardToggledMessageArgs
   {
      /// <summary>
      /// Gets a value indicating whether this instance is displayed.
      /// </summary>
      /// <value><c>true</c> if this instance is displayed; otherwise, <c>false</c>.</value>
      bool IsDisplayed    { get; }
      /// <summary>
      /// Gets the height of the keyboard.
      /// </summary>
      /// <value>The height of the keyboard.</value>
      double KeyboardHeight { get; }
   }

   /// <summary>
   /// Interface IMessage
   /// </summary>
   public interface IMessage
   {
   }

   /// <summary>
   /// Interface IPageLifecycleMessageArgs
   /// </summary>
   public interface IPageLifecycleMessageArgs
   {
      /// <summary>
      /// Gets or sets the page event.
      /// </summary>
      /// <value>The page event.</value>
      IProvidePageEvents SendingPage { get; set; }
   }

   /// <summary>
   /// A global static utility library to assist with Xamarin.Forms.MessagingCenter calls.
   /// </summary>
   public static class FormsMessengerUtils
   {
      /// <summary>
      /// Sends the specified message.
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
      /// Subscribes the specified subscriber.
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
      /// Unsubscribes the specified subscriber.
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
   /// Class AppStateChangedMessage.
   /// Implements the <see cref="AppStateChangeMessageArgs" />
   /// Implements the
   /// <see cref="AppStateChangeMessageArgs" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Common.Notifications.AppStateChangeMessageArgs}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Common.Notifications.AppStateChangeMessageArgs}" />
   /// <seealso cref="AppStateChangeMessageArgs" />
   /// <seealso cref="AppStateChangeMessageArgs" />
   public class AppStateChangedMessage : GenericMessageWithPayload<AppStateChangeMessageArgs>
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="AppStateChangedMessage" /> class.
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
   /// Class AppStateChangeMessageArgs.
   /// </summary>
   public class AppStateChangeMessageArgs
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="AppStateChangeMessageArgs" /> class.
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
      /// Gets or sets the old state of the application.
      /// </summary>
      /// <value>The old state of the application.</value>
      public string OldAppState { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [prevent nav stack push].
      /// </summary>
      /// <value><c>true</c> if [prevent nav stack push]; otherwise, <c>false</c>.</value>
      public bool PreventNavStackPush { get; set; }
   }

   /// <summary>
   /// Notifies the app that the device size has changed
   /// Implements the
   /// <see cref="DeviceSizeChangeMessageArgs" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Common.Notifications.DeviceSizeChangeMessageArgs}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Common.Notifications.DeviceSizeChangeMessageArgs}" />
   /// <seealso cref="DeviceSizeChangeMessageArgs" />
   /// <seealso cref="DeviceSizeChangeMessageArgs" />
   public class BroadcastDeviceSizeChangedMessage : GenericMessageWithPayload<DeviceSizeChangeMessageArgs>
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="BroadcastDeviceSizeChangedMessage" /> class.
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
   /// Class ConnectivityChangedMessage.
   /// Implements the
   /// <see cref="bool" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{System.Boolean}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{System.Boolean}" />
   /// <seealso cref="bool" />
   public class ConnectivityChangedMessage : GenericMessageWithPayload<bool>
   {
   }

   /// <summary>
   /// This message is issued as the args whenever a local platform senses a change in its orientation.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.IDeviceSizeChangeMessageArgs" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.IDeviceSizeChangeMessageArgs" />
   /// <seealso cref="IDeviceSizeChangeMessageArgs" />
   public class DeviceSizeChangeMessageArgs : IDeviceSizeChangeMessageArgs
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="DeviceSizeChangeMessageArgs" /> class.
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
      /// Gets or sets the height of the screen.
      /// </summary>
      /// <value>The height of the screen.</value>
      public float ScreenHeight { get; set; }

      /// <summary>
      /// Gets or sets the width of the screen.
      /// </summary>
      /// <value>The width of the screen.</value>
      public float ScreenWidth { get; set; }
   }

   /// <summary>
   /// Class FragileServiceFailureMessage.
   /// Implements the
   /// <see cref="IFragileService" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Common.Services.IFragileService}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Common.Services.IFragileService}" />
   /// <seealso cref="IFragileService" />
   public class FragileServiceFailureMessage : GenericMessageWithPayload<IFragileService>
   {
   }

   //public class KeyboardToggledMessage : GenericMessageWithPayload<IKeyboardToggledMessageArgs>
   //{
   //   public KeyboardToggledMessage(bool isDisplayed, double keyboardHeight)
   //   {
   //      Payload = new KeyboardToggledMessageArgs(isDisplayed, keyboardHeight);
   //   }
   //}
   /// <summary>
   /// Class FragileServiceSuccessMessage.
   /// Implements the
   /// <see cref="IFragileService" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Common.Services.IFragileService}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Common.Services.IFragileService}" />
   /// <seealso cref="IFragileService" />
   public class FragileServiceSuccessMessage : GenericMessageWithPayload<IFragileService>
   {
   }

   /// <summary>
   /// Class GenericMessageWithPayload.
   /// Implements the <see cref="IMessage" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.IMessage" />
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.IMessage" />
   /// <seealso cref="IMessage" />
   public abstract class GenericMessageWithPayload<T> : IMessage
   {
      /// <summary>
      /// Gets or sets the payload.
      /// </summary>
      /// <value>The payload.</value>
      public T Payload { get; set; }
   }

   /// <summary>
   /// Class KeyboardToggledMessageArgs.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.IKeyboardToggledMessageArgs" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.IKeyboardToggledMessageArgs" />
   public class KeyboardToggledMessageArgs : IKeyboardToggledMessageArgs
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="KeyboardToggledMessageArgs"/> class.
      /// </summary>
      /// <param name="isDisplayed">if set to <c>true</c> [is displayed].</param>
      /// <param name="keyboardHeight">Height of the keyboard.</param>
      public KeyboardToggledMessageArgs(bool isDisplayed, double keyboardHeight)
      {
         IsDisplayed    = isDisplayed;
         KeyboardHeight = keyboardHeight;
      }

      /// <summary>
      /// Gets a value indicating whether this instance is displayed.
      /// </summary>
      /// <value><c>true</c> if this instance is displayed; otherwise, <c>false</c>.</value>
      public bool   IsDisplayed    { get; }
      /// <summary>
      /// Gets the height of the keyboard.
      /// </summary>
      /// <value>The height of the keyboard.</value>
      public double KeyboardHeight { get; }
   }

   /// <summary>
   /// Notifies the orientation service that a the local device size has changed.
   /// Do *not* use for general broadcast, as it will recur!
   /// Implements the <see cref="DeviceSizeChangeMessageArgs" />
   /// Implements the
   /// <see cref="DeviceSizeChangeMessageArgs" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Common.Notifications.DeviceSizeChangeMessageArgs}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.Common.Notifications.DeviceSizeChangeMessageArgs}" />
   /// <seealso cref="DeviceSizeChangeMessageArgs" />
   /// <seealso cref="DeviceSizeChangeMessageArgs" />
   public class LocalDeviceSizeChangedMessage : GenericMessageWithPayload<DeviceSizeChangeMessageArgs>
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="LocalDeviceSizeChangedMessage" /> class.
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
   /// Class MenuLoadedMessage.
   /// Implements the <see cref="NoPayloadMessage" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.NoPayloadMessage" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.NoPayloadMessage" />
   /// <seealso cref="NoPayloadMessage" />
   public class MenuLoadedMessage : NoPayloadMessage
   {
   }

   /// <summary>
   /// Class NavBarMenuTappedMessage.
   /// Implements the <see cref="NoPayloadMessage" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.NoPayloadMessage" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.NoPayloadMessage" />
   /// <seealso cref="NoPayloadMessage" />
   public class NavBarMenuTappedMessage : NoPayloadMessage
   {
   }

   /// <summary>
   /// Class NoPayloadMessage.
   /// Implements the <see cref="IMessage" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.IMessage" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.IMessage" />
   /// <seealso cref="IMessage" />
   public class NoPayloadMessage : IMessage
   {
   }

   /// <summary>
   /// Class NotifyPageCanBeSavedChangedMessage.
   /// Implements the
   /// <see cref="IStatefulViewModel" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.ViewModels.IStatefulViewModel}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.ViewModels.IStatefulViewModel}" />
   /// <seealso cref="IStatefulViewModel" />
   public class NotifyPageCanBeSavedChangedMessage : GenericMessageWithPayload<IStatefulViewModel>
   {
   }

   /// <summary>
   /// Class ObjectDisappearingMessage.
   /// Implements the <see cref="object" />
   /// Implements the <see cref="object" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{System.Object}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{System.Object}" />
   /// <seealso cref="object" />
   /// <seealso cref="object" />
   public class ObjectDisappearingMessage : GenericMessageWithPayload<object>
   {
   }

   /// <summary>
   /// Class RefreshUIViewMessage.
   /// Implements the
   /// <see cref="IStatefulViewModel" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.ViewModels.IStatefulViewModel}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{Com.MarcusTS.SharedForms.ViewModels.IStatefulViewModel}" />
   /// <seealso cref="IStatefulViewModel" />
   public class RefreshUIViewMessage : GenericMessageWithPayload<IStatefulViewModel>
   {
   }

   /// <summary>
   /// Class ViewIsBeingEditedMessage.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{System.Boolean}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Notifications.GenericMessageWithPayload{System.Boolean}" />
   public class ViewIsBeingEditedMessage : GenericMessageWithPayload<bool>
   {
   }
}