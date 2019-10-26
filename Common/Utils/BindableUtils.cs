#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, BindableUtils.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Utils
{
   using System;
   using Xamarin.Forms;

   /// <summary>
   /// Class BindableUtils.
   /// </summary>
   public static class BindableUtils
   {
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
      /// Creates the read only bindable property.
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
      /// Sets up binding.
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