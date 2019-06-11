// *********************************************************************************
// <copyright file=StatefulViewModelCommand.cs company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Common.Commands
{
   using Com.MarcusTS.SharedForms.Common.Notifications;
   using Com.MarcusTS.SharedForms.ViewModels;
   using Com.MarcusTS.SharedUtils.Utils;
   using System;

   /// <summary>
   ///    This command remains in state whenever there are property changes for a page.
   ///    It is also well-behaved on changes to OnNotifyPageCanBeSavedChanged.
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Commands.IViewModelCommand" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Commands.IViewModelCommand" />
   public interface IStatefulViewModelCommand : IViewModelCommand
   {
   }

   /// <summary>
   ///    Class StatefulViewModelCommand.
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Commands.ViewModelCommand" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Commands.IStatefulViewModelCommand" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Commands.ViewModelCommand" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Commands.IStatefulViewModelCommand" />
   public class StatefulViewModelCommand : ViewModelCommand, IStatefulViewModelCommand
   {
      /// <summary>
      ///    The view model
      /// </summary>
      private readonly IViewModelBase _viewModel;

      /// <summary>
      ///    Overrides how to set the command visibility:
      ///    * When not busy (as with the base class)
      ///    * When the page can be saved.
      /// </summary>
      /// <param name="action">The action.</param>
      /// <param name="viewModel">The view model.</param>
      /// <param name="canExecute">The can execute.</param>
      public StatefulViewModelCommand
      (
         Action                        action,
         IStatefulViewModel            viewModel,
         Predicate<IStatefulViewModel> canExecute = null
      )
         : base
         (
            action,
            viewModel,
            vm =>
               (canExecute == null || canExecute(viewModel))
             &&
               !(viewModel.IsBusy || viewModel.IsRefreshing)
             &&
               (
                  viewModel.ProceedWithoutPropertyChanges && viewModel.PageIsValid.IsTrue()
                ||
                  !viewModel.ProceedWithoutPropertyChanges && viewModel.PageIsValid.IsTrue() && viewModel.AnyPropertyValueHasChanged
               )
         )
      {
         _viewModel = viewModel;

         FormsMessengerUtils.Subscribe<NotifyPageCanBeSavedChangedMessage>(this, OnNotifyPageCanBeSavedChanged);
      }

      /// <summary>
      ///    Releases the unmanaged resources.
      /// </summary>
      protected override void ReleaseUnmanagedResources()
      {
         base.ReleaseUnmanagedResources();

         FormsMessengerUtils.Unsubscribe<NotifyPageCanBeSavedChangedMessage>(this);
      }

      /// <summary>
      ///    Called when [notify page can be saved changed].
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="args">The arguments.</param>
      private void OnNotifyPageCanBeSavedChanged
      (
         object                             sender,
         NotifyPageCanBeSavedChangedMessage args
      )
      {
         if (ReferenceEquals(args.Payload, _viewModel))
         {
            RefreshCanExecute();
         }
      }
   }
}