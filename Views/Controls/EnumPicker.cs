// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, EnumPicker.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using Common.Behaviors;
   using Common.Utils;
   using SharedUtils.Utils;
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IEnumPicker
   /// </summary>
   public interface IEnumPicker
   {
      /// <summary>
      /// Gets or sets the current enum.
      /// </summary>
      /// <value>The current enum.</value>
      object CurrentEnum { get; set; }
   }

   /// <summary>
   /// Class EnumPicker.
   /// Implements the <see cref="Xamarin.Forms.Picker" />
   /// Implements the <see cref="IEnumPicker" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Picker" />
   /// <seealso cref="IEnumPicker" />
   public class EnumPicker : Picker, IEnumPicker
   {
      /// <summary>
      /// The current enum property
      /// </summary>
      public static BindableProperty CurrentEnumProperty = CreateEnumPickerBindableProperty
      (
         nameof(CurrentEnum),
         true,
         BindingMode.TwoWay,
         (enumPicker, oldVal, newVal) =>
         {
            // Reset the selected index.
            enumPicker.ResetSelectedIndex();
         }
      );

      /// <summary>
      /// The enum type
      /// </summary>
      private readonly Type _enumType;

      /// <summary>
      /// Initializes a new instance of the <see cref="EnumPicker" /> class.
      /// </summary>
      /// <param name="enumType">Type of the enum.</param>
      /// <param name="currentEnumBindingPropertyName">Name of the current enum binding property.</param>
      /// <param name="currentEnumStrBindingPropertyName">Name of the current enum string binding property.</param>
      /// <param name="validator">The validator.</param>
      public EnumPicker(
         Type enumType,
         string currentEnumBindingPropertyName,
         string currentEnumStrBindingPropertyName,
         Behavior validator = null)
      {
         // ErrorUtils.ConsiderArgumentError(enumType.IsEnum, nameof(EnumPicker) + ": Must supply an enum type.");

         _enumType = enumType;

         if (currentEnumStrBindingPropertyName.IsNotEmpty())
         {
            this.SetUpBinding(SelectedItemProperty, currentEnumStrBindingPropertyName, BindingMode.TwoWay);
         }
         else if (currentEnumBindingPropertyName.IsNotEmpty())
         {
            this.SetUpBinding(CurrentEnumProperty, currentEnumBindingPropertyName, BindingMode.TwoWay);
         }

         if (validator.IsNotNullOrDefault())
         {
            Behaviors.Add(validator);
         }

         ItemsSource = new List<string>(Enum.GetNames(_enumType));

         SelectedIndexChanged += HandleSelectedIndexChanged;

         // If no valid selection, assert one
         if (SelectedIndex < 0)
         {
            SelectedIndex = 0;
         }
      }

      /// <summary>
      /// Gets or sets the current enum.
      /// </summary>
      /// <value>The current enum.</value>
      public object CurrentEnum
      {
         get => GetValue(CurrentEnumProperty);
         set => SetValue(CurrentEnumProperty, value);
      }

      /// <summary>
      /// Creates the enum picker bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateEnumPickerBindableProperty<PropertyTypeT>
      (
         string localPropName,
         PropertyTypeT defaultVal = default,
         BindingMode bindingMode = BindingMode.OneWay,
         Action<EnumPicker, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      /// Handles the selected index changed.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void HandleSelectedIndexChanged(object sender, EventArgs e)
      {
         if (SelectedIndex < 0 || SelectedItem == null)
         {
            return;
         }

         CurrentEnum = Enum.Parse(_enumType, SelectedItem.ToString(), true);

         var viewValidator = Behaviors.OfType<IViewValidationBehavior>().FirstOrDefault();

         if (viewValidator.IsNotNullOrDefault())
         {
            // ReSharper disable once PossibleNullReferenceException
            viewValidator.RevalidateEditorText();
         }
      }

      /// <summary>
      /// Resets the index of the selected.
      /// </summary>
      private void ResetSelectedIndex()
      {
         SelectedIndex = Enum.GetNames(_enumType).ToList().IndexOf(Enum.GetName(_enumType, CurrentEnum));
      }
   }
}
