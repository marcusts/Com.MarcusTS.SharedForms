// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="PageViewModelBase.cs" company="Marcus Technical Services, Inc.">
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

using Com.MarcusTS.SharedForms.Common.Navigation;
using Com.MarcusTS.SharedForms.Common.Utils;

namespace Com.MarcusTS.SharedForms.ViewModels
{
   using Common.Notifications;
   using PropertyChanged;
   using SharedUtils.Interfaces;
   using SharedUtils.Utils;

   /// <summary>
   /// Interface IPageViewModelBase
   /// Implements the <see cref="IViewModelBase" />
   /// Implements the <see cref="Com.MarcusTS.SharedUtils.Interfaces.IReceivePageEvents" />
   /// </summary>
   /// <seealso cref="IViewModelBase" />
   /// <seealso cref="Com.MarcusTS.SharedUtils.Interfaces.IReceivePageEvents" />
   public interface IPageViewModelBase : IViewModelBase, IReceivePageEvents
   {
      /// <summary>
      /// Copied from the menu item to this page (at least for now)
      /// </summary>
      /// <value>The page title.</value>
      string PageTitle { get; set; }
   }

   /// <summary>
   /// Class PageViewModelBase.
   /// Implements the <see cref="IPageViewModelBase" />
   /// </summary>
   /// <seealso cref="IPageViewModelBase" />
   [DoNotNotify]
   public abstract class PageViewModelBase : ViewModelBase, IPageViewModelBase
   {
      /// <summary>
      /// The machine
      /// </summary>
      protected readonly IStateMachine Machine;

      /// <summary>
      /// Initializes a new instance of the <see cref="PageViewModelBase" /> class.
      /// </summary>
      /// <param name="stateMachine">The state machine.</param>
      /// <param name="pageEventProvider">The page event provider.</param>
      protected PageViewModelBase(IStateMachine stateMachine,
                                  IProvidePageEvents pageEventProvider = null)
      {
         // Request the global interface type so the code is more share-able.
         Machine = stateMachine;

         // Also share the page event provider so that derivers know about OnAppearing,
         // OnDisappearing, etc.
         PageEventProvider = pageEventProvider;

         if (PageEventProvider?.GetEventBroadcaster != null)
         {
            FormsMessengerUtils.Subscribe<PageLifecycleMessage>(this, HandlePageLifecycleChanged);
         }
      }

      /// <summary>
      /// Gets or sets the page event provider.
      /// </summary>
      /// <value>The page event provider.</value>
      public IProvidePageEvents PageEventProvider { get; set; }

      /// <summary>
      /// Copied from the menu item to this page (at least for now)
      /// </summary>
      /// <value>The page title.</value>
      public string PageTitle { get; set; }

      /// <summary>
      /// Make this page lifecycle event visible to derivers
      /// </summary>
      /// <param name="args">The arguments.</param>
      protected virtual void OnPageLifecycleChanged(IPageLifecycleMessageArgs args)
      { }

      /// <summary>
      /// Handles the page lifecycle changed.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="args">The arguments.</param>
      private void HandlePageLifecycleChanged(object sender,
                                              PageLifecycleMessage args)
      {
         // Make sure the sender is our page
         if (!sender.IsAnEqualReferenceTo(PageEventProvider?.GetEventBroadcaster?.Invoke()))
         {
            return;
         }

         OnPageLifecycleChanged(args.Payload);
      }
   }
}
