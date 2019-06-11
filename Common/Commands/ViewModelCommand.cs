// *********************************************************************************
// <copyright file=ViewModelCommand.cs company="Marcus Technical Services, Inc.">
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
   using Com.MarcusTS.SharedForms.Common.Interfaces;
   using Com.MarcusTS.SharedUtils.Events;
   using System;
   using System.Windows.Input;
   using Xamarin.Forms;

   /// <summary>
   ///    Interface IViewModelCommand
   ///    Implements the <see cref="System.Windows.Input.ICommand" />
   ///    Implements the <see cref="System.IDisposable" />
   /// </summary>
   /// <seealso cref="System.Windows.Input.ICommand" />
   /// <seealso cref="System.IDisposable" />
   public interface IViewModelCommand : ICommand, IDisposable
   {
      /// <summary>
      ///    Refreshes the can execute.
      /// </summary>
      void RefreshCanExecute();
   }

   /// <summary>
   ///    Class ViewModelCommand.
   ///    Implements the <see cref="Xamarin.Forms.Command" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Commands.IViewModelCommand" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Command" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Commands.IViewModelCommand" />
   public class ViewModelCommand : Command, IViewModelCommand
   {
      /// <summary>
      ///    Ensures that the command is not available if the view model is busy.
      /// </summary>
      /// <param name="action">The action.</param>
      /// <param name="viewModel">The view model.</param>
      /// <param name="canExecute">The can execute.</param>
      public ViewModelCommand
      (
         Action             action,
         IAmBusy            viewModel,
         Predicate<IAmBusy> canExecute = null
      )
         : base(action, () => (canExecute == null || canExecute(viewModel)) && !viewModel.IsBusy)
      {
         CustomWeakEventManager.AddEventHandler(OnViewModelIsBusyChanged);
      }

      /// <summary>
      ///    Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         CustomWeakEventManager.RemoveEventHandler(OnViewModelIsBusyChanged);
         ReleaseUnmanagedResources();
         GC.SuppressFinalize(this);
      }

      /// <summary>
      ///    Refreshes the can execute.
      /// </summary>
      public virtual void RefreshCanExecute()
      {
         ChangeCanExecute();
      }

      /// <summary>
      ///    Releases the unmanaged resources.
      /// </summary>
      protected virtual void ReleaseUnmanagedResources()
      {
      }

      /// <summary>
      ///    Called when [view model is busy changed].
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void OnViewModelIsBusyChanged
      (
         object    sender,
         EventArgs e
      )
      {
         RefreshCanExecute();
      }
   }
}