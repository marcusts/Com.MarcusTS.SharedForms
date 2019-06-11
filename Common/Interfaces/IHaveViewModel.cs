﻿// *********************************************************************************
// <copyright file=IHaveViewModel.cs company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Common.Interfaces
{
   using Com.MarcusTS.SharedForms.ViewModels;

   /// <summary>
   ///    Implements a ViewModel of type T. Unlike <see cref="IHavePageViewModel{T}" />,
   ///    this one only requires that the declaring view has a view model that itself implements IViewModel.
   /// </summary>
   /// <typeparam name="T">the type of ViewModel</typeparam>
   public interface IHaveViewModel<out T>
      where T : class, IViewModelBase
   {
      /// <summary>
      ///    Gets the view model.
      /// </summary>
      /// <value>The view model.</value>
      T ViewModel { get; }
   }
}