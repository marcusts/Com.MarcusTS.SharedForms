// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="BindableUtils.cs" company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Utils
{
   using System;
   using Xamarin.Forms;

   /// <summary>
   /// Class BindableUtils.
   /// </summary>
   public static class BindableUtils
   {
      #region Public Methods

      /// <summary>
      /// Creates the bindable property.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <typeparam name="U"></typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateBindableProperty<T, U>(string          localPropName,
                                                                  U               defaultVal     = default(U),
                                                                  BindingMode     bindingMode    = BindingMode.OneWay,
                                                                  Action<T, U, U> callbackAction = null)
         where T : class
      {
         return BindableProperty.Create
            (
             localPropName,
             typeof(U),
             typeof(T),
             defaultVal,
             bindingMode,
             propertyChanged: (bindable,
                               oldVal,
                               newVal) =>
                              {
                                 if (callbackAction != null)
                                 {
                                    var bindableAsOverlayButton = bindable as T;
                                    if (bindableAsOverlayButton != null)
                                    {
                                       callbackAction(bindableAsOverlayButton, (U) oldVal, (U) newVal);
                                    }
                                 }
                              });
      }

      /// <summary>
      /// Creates the read only bindable property.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <typeparam name="U"></typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateReadOnlyBindableProperty<T, U>(string localPropName,
                                                                          U      defaultVal = default(U),
                                                                          BindingMode bindingMode =
                                                                             BindingMode.OneWay,
                                                                          Action<T, U, U> callbackAction = null)
         where T : class
      {
         return BindableProperty.CreateReadOnly
            (
             localPropName,
             typeof(U),
             typeof(T),
             defaultVal,
             bindingMode,
             propertyChanged: (bindable,
                               oldVal,
                               newVal) =>
                              {
                                 if (callbackAction != null)
                                 {
                                    var bindableAsOverlayButton = bindable as T;
                                    if (bindableAsOverlayButton != null)
                                    {
                                       callbackAction(bindableAsOverlayButton, (U) oldVal,
                                                      (U) newVal);
                                    }
                                 }
                              }).BindableProperty;
      }

      #endregion Public Methods
   }
}