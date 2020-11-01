// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, BindableViewModelbase.cs, is a part of a program called AccountViewMobile.
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
   using System.ComponentModel;
   using System.Runtime.CompilerServices;

   /// <summary>
   /// Interface IBindableViewModel
   /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// </summary>
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   public interface IBindableViewModel : INotifyPropertyChanged
   {
      /// <summary>
      /// Raises the property changed.
      /// </summary>
      /// <param name="propertyName">Name of the property.</param>
      void RaisePropertyChanged(string propertyName);
   }

   /// <summary>
   /// Class BindableViewModel.
   /// Implements the <see cref="IBindableViewModel" />
   /// </summary>
   /// <seealso cref="IBindableViewModel" />
   public abstract class BindableViewModel : IBindableViewModel
   {
      /// <summary>
      /// Occurs when a property value changes.
      /// </summary>
      /// <returns></returns>
      public event PropertyChangedEventHandler PropertyChanged;

      /// <summary>
      /// Raises the property changed.
      /// </summary>
      /// <param name="propertyName">Name of the property.</param>
      public void RaisePropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

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
