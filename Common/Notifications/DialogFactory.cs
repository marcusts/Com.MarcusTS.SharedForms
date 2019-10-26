#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, DialogFactory.cs, is a part of a program called AccountViewMobile.
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
   using Acr.UserDialogs;
   using SharedUtils.Utils;
   using System;
   using System.Threading.Tasks;
   using Utils;
   using Xamarin.Forms;

   /// <summary>
   /// Class DialogFactory.
   /// </summary>
   public static class DialogFactory
   {
      /// <summary>
      /// The toast dissolve seconds
      /// </summary>
      private const int TOAST_DISSOLVE_SECONDS = 7;
      /// <summary>
      /// The wait forever
      /// </summary>
      private const int WAIT_FOREVER           = 1000000000;

      /// <summary>
      /// The decision toast background color
      /// </summary>
      private static readonly Color DECISION_TOAST_BACKGROUND_COLOR = Color.Black;

      /// <summary>
      /// The error toast background color
      /// </summary>
      private static readonly Color ERROR_TOAST_BACKGROUND_COLOR = Color.Black;

      // All backgrounds are now black
      /// <summary>
      /// The information toast background color
      /// </summary>
      private static readonly Color INFO_TOAST_BACKGROUND_COLOR = Color.Black;

      /// <summary>
      /// An important toast with an action that takes the user somewhere in response.
      /// </summary>
      /// <param name="toastStr">The toast message.</param>
      /// <param name="action">The action to take once the user taps the toast.  *Required*.</param>
      /// <param name="useTimeout">Whether to remove the toast after a timeout.  Not normal for this scenario: defaults to
      /// *false*.</param>
      public static void ShowDecisionToast
      (
         string toastStr,
         Action action,
         bool   useTimeout = false
      )
      {
         ShowToastInternal(toastStr, useTimeout: useTimeout, backgroundColor: DECISION_TOAST_BACKGROUND_COLOR,
                           action: action);
      }

      /// <summary>
      /// An error toast to notify the user that something went wrong.
      /// </summary>
      /// <param name="toastStr">The toast message.</param>
      /// <param name="actionText">The button text</param>
      /// <param name="useTimeout">Whether to remove the toast after a timeout.  Not normal for this scenario: defaults to
      /// *false*.</param>
      /// <param name="action">The action to take once the user taps the toast.  Optional.</param>
      public static void ShowErrorToast
      (
         string toastStr,
         string actionText = "OK",
         bool   useTimeout = false,
         Action action     = null
      )
      {
         ShowToastInternal(toastStr, actionText, useTimeout: useTimeout, backgroundColor: ERROR_TOAST_BACKGROUND_COLOR,
                           action: action);
      }

      /// <summary>
      /// An error toast to notify the user that an error has occurred.
      /// </summary>
      /// <param name="toastPrefix">The first part of the toast message.
      /// The second part will be added by this method (see below).</param>
      /// <param name="ex">The exception.</param>
      /// <param name="useTimeout">Whether to remove the toast after a timeout.  Not normal for this scenario: defaults to
      /// *false*.</param>
      /// <param name="action">The action to take once the user taps the toast.  Optional.</param>
      public static void ShowExceptionToast
      (
         string    toastPrefix,
         Exception ex,
         bool      useTimeout = false,
         Action    action     = null
      )
      {
         // Add the error details to the toast prefix.
         var finalToastStr = toastPrefix + " (Error details: " + ex.Message + ").";

         ShowErrorToast(finalToastStr, useTimeout: useTimeout, action: action);
      }

      /// <summary>
      /// A passive toast in a benign color to indicate information only;
      /// does not normally trigger an action, but can.
      /// </summary>
      /// <param name="toastStr">The toast message.</param>
      /// <param name="actionText">The action text.</param>
      /// <param name="useTimeout">Whether to remove the toast after a timeout.  Defaults to *true*.</param>
      /// <param name="action">The action to take if the user taps the toast.</param>
      public static void ShowInfoToast
      (
         string toastStr,
         string actionText = "OK",
         bool   useTimeout = true,
         Action action     = null
      )
      {
         ShowToastInternal(toastStr, actionText, useTimeout: useTimeout, backgroundColor: INFO_TOAST_BACKGROUND_COLOR,
                           action: action);
      }

      /// <summary>
      /// Can call directly.
      /// </summary>
      /// <param name="toastStr">The toast string.</param>
      /// <param name="actionText">The action text.</param>
      /// <param name="backgroundColor">Color of the background.</param>
      /// <param name="messageTextColor">Color of the message text.</param>
      /// <param name="actionTextColor">Color of the action text.</param>
      /// <param name="useTimeout">if set to <c>true</c> [use timeout].</param>
      /// <param name="toastDissolveSeconds">The toast dissolve seconds.</param>
      /// <param name="action">The action.</param>
      public static void ShowToastInternal
      (
         string toastStr,
         string actionText           = "OK",
         Color  backgroundColor      = default,
         Color  messageTextColor     = default,
         Color  actionTextColor      = default,
         bool   useTimeout           = true,
         int    toastDissolveSeconds = TOAST_DISSOLVE_SECONDS,
         Action action               = null
      )
      {
         if (toastStr.IsEmpty())
         {
            return;
         }

         var newConfig =
            new ToastConfig(toastStr).SetMessageTextColor
            (
               messageTextColor.IsUnsetOrDefault()
                  ? Color.White
                  : messageTextColor
            );

         if (backgroundColor.IsValid())
         {
            newConfig.SetBackgroundColor(backgroundColor);
         }

         newConfig.SetDuration(
            TimeSpan.FromSeconds(useTimeout && toastDissolveSeconds > 0 ? toastDissolveSeconds : WAIT_FOREVER));

         var newAction = new ToastAction();

         // Must add "SetAction"
         if (action != null || actionText.IsNotEmpty())
         {
            if (action != null)
            {
               newAction.SetAction(action);
            }

            if (actionText.IsNotEmpty())
            {
               newAction.SetText(actionText);
               newAction.SetTextColor(actionTextColor.IsAnEqualObjectTo(default(Color))
                                         ? Color.White
                                         : actionTextColor);
            }

            newConfig.SetAction(newAction);
         }

         // ELSE skip "SetAction"

         UserDialogs.Instance.Toast(newConfig);
      }

      /// <summary>
      /// Shows the yes no dialog.
      /// </summary>
      /// <param name="title">The title.</param>
      /// <param name="message">The message.</param>
      /// <param name="okText">The ok text.</param>
      /// <param name="cancelText">The cancel text.</param>
      /// <returns>Task&lt;System.Boolean&gt;.</returns>
      public static async Task<bool> ShowYesNoDialog
      (
         string title,
         string message,
         string okText     = "Yes",
         string cancelText = "No"
      )
      {
         return await UserDialogs.Instance.ConfirmAsync(message, title, okText, cancelText).WithoutChangingContext();
      }
   }
}