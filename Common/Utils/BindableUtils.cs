﻿// *********************************************************************************
// <copyright file=BindableUtils.cs company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Common.Utils
{
   using System;
   using Xamarin.Forms;

   /// <summary>
   ///    Class BindableUtils.
   /// </summary>
   public static class BindableUtils
   {
      /// <summary>
      ///    Creates the bindable property.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <typeparam name="U"></typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateBindableProperty<T, U>
      (
         string          localPropName,
         U               defaultVal     = default,
         BindingMode     bindingMode    = BindingMode.OneWay,
         Action<T, U, U> callbackAction = null
      )
         where T : class
      {
         return BindableProperty.Create
         (
            localPropName,
            typeof(U),
            typeof(T),
            defaultVal,
            bindingMode,
            propertyChanged:
            (
               bindable,
               oldVal,
               newVal
            ) =>
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
      ///    Creates the read only bindable property.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <typeparam name="U"></typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateReadOnlyBindableProperty<T, U>
      (
         string localPropName,
         U      defaultVal = default,
         BindingMode bindingMode =
            BindingMode.OneWay,
         Action<T, U, U> callbackAction = null
      )
         where T : class
      {
         return BindableProperty.CreateReadOnly
         (
            localPropName,
            typeof(U),
            typeof(T),
            defaultVal,
            bindingMode,
            propertyChanged:
            (
               bindable,
               oldVal,
               newVal
            ) =>
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

      /// <summary>
      ///    Sets up binding.
      /// </summary>
      /// <param name="view">The view.</param>
      /// <param name="bindableProperty">The bindable property.</param>
      /// <param name="viewModelPropertyName">Name of the view model property.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="converter">The converter.</param>
      /// <param name="converterParameter">The converter parameter.</param>
      /// <param name="stringFormat">The string format.</param>
      /// <param name="source">The source.</param>
      public static void SetUpBinding
      (
         this BindableObject view,
         BindableProperty    bindableProperty,
         string              viewModelPropertyName,
         BindingMode         bindingMode        = BindingMode.OneWay,
         IValueConverter     converter          = null,
         object              converterParameter = null,
         string              stringFormat       = null,
         object              source             = null
      )
      {
         view.SetBinding(bindableProperty,
                         new Binding(viewModelPropertyName, bindingMode, converter, converterParameter, stringFormat,
                                     source));
      }
   }
}