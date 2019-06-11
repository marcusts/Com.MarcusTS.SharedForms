// *********************************************************************************
// <copyright file=NotificationUtils.cs company="Marcus Technical Services, Inc.">
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
   /// <summary>
   ///    Class NotificationUtils.
   /// </summary>
   public static class NotificationUtils
   {
      /// <summary>
      ///    The body
      /// </summary>
      public const string BODY = "body";

      /// <summary>
      ///    The identifier
      /// </summary>
      public const string ID = "id";

      /// <summary>
      ///    Sets various flags based on the id so the app can start and go to a given page.
      /// </summary>
      /// <param name="id">The identifier.</param>
      public static void HandleBackgroundNotification(string id)
      {
      }

      /// <summary>
      ///    Determine if this is a normal push notification or an error.
      /// </summary>
      /// <param name="id">The identifier.</param>
      /// <param name="body">The body.</param>
      public static void HandleForegroundNotification
      (
         string id,
         string body
      )
      {
      }

      //public static void GoToMainMenuPage<T>()
      //   where T : Page
      //{
      //   // If already on the desired page, we are done.
      //   var mainPage = Application.Current.MainPage as MasterDetailPage;

      //   // If we are on the target page, then we are done.
      //   if ((mainPage?.Detail as CustomNavigationPage)?.CurrentPage is T)
      //   {
      //      return;
      //   }

      //   // Must construct and open a new target page
      //   var targetPage = Activator.CreateInstance<T>();
      //   var customNavContainer = new CustomNavigationPage(targetPage);
      //   ((MasterDetailPage) Application.Current.MainPage).Detail = customNavContainer;
      //   ((MasterDetailPage) Application.Current.MainPage).IsPresented = false;
      //}
   }
}