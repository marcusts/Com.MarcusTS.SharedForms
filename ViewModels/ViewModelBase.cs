// *********************************************************************************
// <copyright file=ViewModelBase.cs company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.ViewModels
{
   using Com.MarcusTS.SharedForms.Annotations;
   using Com.MarcusTS.SharedForms.Common.Interfaces;
   using Com.MarcusTS.SharedUtils.Utils;
   using System.ComponentModel;
   using System.Runtime.CompilerServices;

   /// <summary>
   ///    Interface IViewModelBase
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Interfaces.IAmBusy" />
   ///    Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Interfaces.IAmBusy" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IViewModelBase : IAmBusy, INotifyPropertyChanged
   {
   }

   /// <summary>
   ///    The base class for all view models
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.ViewModels.IViewModelBase" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.ViewModels.IViewModelBase" />
   public abstract class ViewModelBase : IViewModelBase
   {
      /// <summary>
      ///    The is busy
      /// </summary>
      private bool _isBusy;

      /// <summary>
      ///    Occurs when [is busy changed].
      /// </summary>
      public event EventUtils.GenericDelegate<IAmBusy> IsBusyChanged;

      /// <summary>
      ///    Occurs when [property changed].
      /// </summary>
      public event PropertyChangedEventHandler PropertyChanged;

      /// <summary>
      ///    Gets or sets a value indicating whether this instance is busy.
      /// </summary>
      /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
      public bool IsBusy
      {
         get => _isBusy;
         set
         {
            if (_isBusy != value)
            {
               _isBusy = value;
               IsBusyChanged?.Invoke(this);
            }
         }
      }

      /// <summary>
      ///    Gets the is busy message.
      /// </summary>
      /// <value>The is busy message.</value>
      public string IsBusyMessage { get; }

      /// <summary>
      ///    Called when [property changed].
      /// </summary>
      /// <param name="propertyName">Name of the property.</param>
      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}