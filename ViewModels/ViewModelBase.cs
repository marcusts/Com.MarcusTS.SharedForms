// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ViewModelBase.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.ViewModels
{
   using Common.Annotations;
   using Common.Interfaces;
   using SharedUtils.Utils;
   using System.ComponentModel;
   using System.Runtime.CompilerServices;

   /// <summary>
   /// Interface IViewModelBase
   /// Implements the <see cref="IAmBusy" />
   /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="IAmBusy" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IViewModelBase : IAmBusy, INotifyPropertyChanged
   {
   }

   /// <summary>
   /// The base class for all view models
   /// Implements the <see cref="IViewModelBase" />
   /// </summary>
   /// <seealso cref="IViewModelBase" />
   public abstract class ViewModelBase : IViewModelBase
   {
      /// <summary>
      /// The is busy
      /// </summary>
      private bool _isBusy;

      /// <summary>
      /// Occurs when [is busy changed].
      /// </summary>
      public event EventUtils.GenericDelegate<IAmBusy> IsBusyChanged;

      /// <summary>
      /// Occurs when [property changed].
      /// </summary>
      public event PropertyChangedEventHandler PropertyChanged;

      /// <summary>
      /// Gets or sets a value indicating whether this instance is busy.
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
      /// Gets the is busy message.
      /// </summary>
      /// <value>The is busy message.</value>
      public string IsBusyMessage { get; }

      /// <summary>
      /// Called when [property changed].
      /// </summary>
      /// <param name="propertyName">Name of the property.</param>
      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}
