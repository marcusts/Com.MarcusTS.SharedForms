// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, StatefulViewModelCommand.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Commands
{
   using Notifications;
   using SharedUtils.Utils;
   using System;
   using ViewModels;

   /// <summary>
   /// This command remains in state whenever there are property changes for a page.
   /// It is also well-behaved on changes to OnNotifyPageCanBeSavedChanged.
   /// Implements the <see cref="IViewModelCommand" />
   /// </summary>
   /// <seealso cref="IViewModelCommand" />
   public interface IStatefulViewModelCommand : IViewModelCommand
   {
   }

   /// <summary>
   /// Class StatefulViewModelCommand.
   /// Implements the <see cref="ViewModelCommand" />
   /// Implements the <see cref="IStatefulViewModelCommand" />
   /// </summary>
   /// <seealso cref="ViewModelCommand" />
   /// <seealso cref="IStatefulViewModelCommand" />
   public class StatefulViewModelCommand : ViewModelCommand, IStatefulViewModelCommand
   {
      /// <summary>
      /// The view model
      /// </summary>
      private readonly IViewModelBase _viewModel;

      /// <summary>
      /// Overrides how to set the command visibility:
      /// * When not busy (as with the base class)
      /// * When the page can be saved.
      /// </summary>
      /// <param name="action">The action.</param>
      /// <param name="viewModel">The view model.</param>
      /// <param name="canExecute">The can execute.</param>
      public StatefulViewModelCommand
      (
         Action action,
         IStatefulViewModel viewModel,
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
                  !viewModel.ProceedWithoutPropertyChanges && viewModel.PageIsValid.IsTrue() &&
                  viewModel.AnyPropertyValueHasChanged
               )
         )
      {
         _viewModel = viewModel;

         FormsMessengerUtils.Subscribe<NotifyPageCanBeSavedChangedMessage>(this, OnNotifyPageCanBeSavedChanged);
      }

      /// <summary>
      /// Releases the unmanaged resources.
      /// </summary>
      protected override void ReleaseUnmanagedResources()
      {
         base.ReleaseUnmanagedResources();

         FormsMessengerUtils.Unsubscribe<NotifyPageCanBeSavedChangedMessage>(this);
      }

      /// <summary>
      /// Called when [notify page can be saved changed].
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="args">The arguments.</param>
      private void OnNotifyPageCanBeSavedChanged
      (
         object sender,
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
