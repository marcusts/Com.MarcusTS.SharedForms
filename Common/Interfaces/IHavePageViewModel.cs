#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, IHavePageViewModel.cs, is a part of a program called AccountViewMobile.
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

#endregion

namespace Com.MarcusTS.SharedForms.Common.Interfaces
{
   /// <summary>
   /// The HasViewModel interface. Implements a ViewModel of type T. T must be an IAmBusy. This is
   /// basically a marker interface for some kind of object that at least has a ViewModel. Used for IoC.
   /// </summary>
   /// <typeparam name="T">the type of ViewModel</typeparam>
   public interface IHavePageViewModel<out T>
      where T : class
   {
      /// <summary>
      /// Gets the view model.
      /// </summary>
      /// <value>The view model.</value>
      T ViewModel { get; }
   }
}