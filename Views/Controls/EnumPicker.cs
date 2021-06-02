// ********************************************************************************* <copyright
// file=EnumToggleImageLabelButtonBase.cs company="Marcus Technical Services, Inc."> Copyright @2019 Marcus Technical
// Services, Inc. </copyright>
//
// MIT License
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
// Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// *********************************************************************************

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using Common.Behaviors;
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   public interface IEnumPicker
   {
      object CurrentEnum { get; set; }
   }

   public class EnumPicker : Picker, IEnumPicker
   {
      public static readonly BindableProperty CurrentEnumProperty = CreateEnumPickerBindableProperty
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

      private readonly Type _enumType;

      public EnumPicker(
         Type     enumType,
         string   currentEnumBindingPropertyName,
         string   currentEnumStrBindingPropertyName,
         Behavior validator = null)
      {
         // ErrorUtils.IssueArgumentErrorIfTrue(enumType.IsEnum, nameof(EnumPicker) + ": Must supply an enum type.");

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

      public object CurrentEnum
      {
         get => GetValue(CurrentEnumProperty);
         set => SetValue(CurrentEnumProperty, value);
      }

      public static BindableProperty CreateEnumPickerBindableProperty<PropertyTypeT>
      (
         string                                           localPropName,
         PropertyTypeT                                    defaultVal     = default,
         BindingMode                                      bindingMode    = BindingMode.OneWay,
         Action<EnumPicker, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

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
            viewValidator.Revalidate();
         }
      }

      private void ResetSelectedIndex()
      {
         SelectedIndex = Enum.GetNames(_enumType).ToList().IndexOf(Enum.GetName(_enumType, CurrentEnum));
      }
   }
}