// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ViewModelCommand.cs, is a part of a program called AccountViewMobile.
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
   using Interfaces;
   using SharedUtils.Events;
   using System;
   using System.Windows.Input;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IViewModelCommand
   /// Implements the <see cref="System.Windows.Input.ICommand" />
   /// Implements the <see cref="System.IDisposable" />
   /// </summary>
   /// <seealso cref="System.Windows.Input.ICommand" />
   /// <seealso cref="System.IDisposable" />
   public interface IViewModelCommand : ICommand, IDisposable
   {
      /// <summary>
      /// Refreshes the can execute.
      /// </summary>
      void RefreshCanExecute();
   }

   /// <summary>
   /// Class ViewModelCommand.
   /// Implements the <see cref="Xamarin.Forms.Command" />
   /// Implements the <see cref="IViewModelCommand" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Command" />
   /// <seealso cref="IViewModelCommand" />
   public class ViewModelCommand : Command, IViewModelCommand
   {
      /// <summary>
      /// Ensures that the command is not available if the view model is busy.
      /// </summary>
      /// <param name="action">The action.</param>
      /// <param name="viewModel">The view model.</param>
      /// <param name="canExecute">The can execute.</param>
      public ViewModelCommand
      (
         Action action,
         IAmBusy viewModel,
         Predicate<IAmBusy> canExecute = null
      )
         : base(action, () => (canExecute == null || canExecute(viewModel)) && !viewModel.IsBusy)
      {
         CustomWeakEventManager.AddEventHandler(OnViewModelIsBusyChanged);
      }

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         CustomWeakEventManager.RemoveEventHandler(OnViewModelIsBusyChanged);
         ReleaseUnmanagedResources();
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Refreshes the can execute.
      /// </summary>
      public virtual void RefreshCanExecute()
      {
         ChangeCanExecute();
      }

      /// <summary>
      /// Releases the unmanaged resources.
      /// </summary>
      protected virtual void ReleaseUnmanagedResources()
      {
      }

      /// <summary>
      /// Called when [view model is busy changed].
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void OnViewModelIsBusyChanged
      (
         object sender,
         EventArgs e
      )
      {
         RefreshCanExecute();
      }
   }
}
