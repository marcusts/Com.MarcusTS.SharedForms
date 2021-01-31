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

using Com.MarcusTS.SharedUtils.Interfaces;
using Xamarin.Forms;

namespace Com.MarcusTS.SharedForms.Common.Utils
{
   using Notifications;
   using ViewModels;

   /// <summary>
   ///    Enum PageLifecycleEvents
   /// </summary>
   public enum PageLifecycleEvents
   {
      /// <summary>
      ///    The before constructing
      /// </summary>
      BeforeConstructing,

      /// <summary>
      ///    The after constructing
      /// </summary>
      AfterConstructing,

      /// <summary>
      ///    The before appearing
      /// </summary>
      BeforeAppearing,

      /// <summary>
      ///    The after appearing
      /// </summary>
      AfterAppearing,

      /// <summary>
      ///    The before disappearing
      /// </summary>
      BeforeDisappearing,

      /// <summary>
      ///    The after disappearing
      /// </summary>
      AfterDisappearing
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
      PageLifecycleEvents PageEvent { get; set; }

      /// <summary>
      ///    Gets or sets the sending page.
      /// </summary>
      /// <value>The sending page.</value>
      IProvidePageEvents SendingPage { get; set; }
   }

   /// <summary>
   ///    Class PageLifecycleMessage.
   ///    Implements the
   ///    <see
   ///       cref="IPageLifecycleMessageArgs" />
   /// </summary>
   /// <seealso
   ///    cref="IPageLifecycleMessageArgs" />
   public class PageLifecycleMessage : GenericMessageWithPayload<IPageLifecycleMessageArgs>
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="PageLifecycleMessage" /> class.
      /// </summary>
      /// <param name="sendingPage">The sending page.</param>
      /// <param name="pageEvent">The page event.</param>
      public PageLifecycleMessage(IProvidePageEvents sendingPage,
         PageLifecycleEvents pageEvent)
      {
         Payload = new PageLifecycleMessageArgs(sendingPage, pageEvent);
      }
   }

   /// <summary>
   ///    Class PageLifecycleMessageArgs.
   ///    Implements the <see cref="IPageLifecycleMessageArgs" />
   /// </summary>
   /// <seealso cref="IPageLifecycleMessageArgs" />
   public class PageLifecycleMessageArgs : IPageLifecycleMessageArgs
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="PageLifecycleMessageArgs" /> class.
      /// </summary>
      /// <param name="sendingPage">The sending page.</param>
      /// <param name="pageEvent">The page event.</param>
      public PageLifecycleMessageArgs(IProvidePageEvents sendingPage,
         PageLifecycleEvents pageEvent)
      {
         SendingPage = sendingPage;
         PageEvent = pageEvent;
      }

      /// <summary>
      ///    Gets or sets the page event.
      /// </summary>
      /// <value>The page event.</value>
      public PageLifecycleEvents PageEvent { get; set; }

      /// <summary>
      ///    Gets or sets the sending page.
      /// </summary>
      /// <value>The sending page.</value>
      public IProvidePageEvents SendingPage { get; set; }
   }
}
