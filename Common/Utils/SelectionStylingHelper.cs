// *********************************************************************************
// <copyright
//    file=SelectionAndAlternateHelper.cs
//    company="Marcus Technical Services, Inc.">
//    Copyright 2019 Marcus Technical Services, Inc.
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
//  *********************************************************************************

namespace Com.MarcusTS.LifesAStage.Views.Controls.Helpers
{
   using System;
   using System.Diagnostics;
   using SharedForms.Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   public interface ICanOverrideSelectionAndAlternation : ICanBeSelected, ICanAlternate
   { 
      event EventUtils.GenericDelegate<ICanOverrideSelectionAndAlternation> ConsiderSelectionOrAlterationOverride;
   }

   public interface IHaveSelectionAndAlternationProvider
   {
      ICanOverrideSelectionAndAlternation SelectionAndAlternationProvider { get; }
   }

   public interface IHaveSelectionProvider
   {
      ICanBeSelected SelectionProvider { get; }
   }

   public interface IHaveSelectionStylingHelper
   {
      ISelectionStylingHelper StylingHelper { get; set; }

      void AfterStyleApplied();
   }

   public interface ICanAlternate
   {
      // Set internally only
      bool                                            IsAnAlternate { get; set; }
      event EventUtils.GenericDelegate<ICanAlternate> IsAlternateChanged;
   }

   public interface ICanBeSelected
   {
      bool IsSelected { get; set; }

      /// <summary>True if the user attempts t select but wee are prevented from selection by various rules.</summary>
      bool IsTryingToBeSelected { get; set; }

      event EventUtils.GenericDelegate<ICanBeSelected> IsSelectedChanged;

      void AfterSelectionChange(ICanBeSelected item);

      bool CanSelectionBeMade(bool isSelected);
   }

   public interface ISelectionStylingHelper
   {
      bool CanAttach(View hostView);

      Style AlternateDeselectedStyle { get; set; }

      Style DeselectedStyle          { get; set; }

      Style SelectedStyle            { get; set; }
   }

   public class SelectionStylingHelper : ISelectionStylingHelper
   {
      private ICanAlternate  _alternateProvider;
      private ICanBeSelected _selectionProvider;
      private View           _hostView;
      private Style          _alternateDeselectedStyle;
      private Style          _deselectedStyle;
      private Style          _selectedStyle;

      public static readonly BindableProperty AlternateDeselectedStyleProperty =
         CreateSelectionStylingHelperProperty
         (
            nameof(AlternateDeselectedStyle),
            default(Style),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.AlternateDeselectedStyle = newVal;
            }
         );

      public static readonly BindableProperty DeselectedStyleProperty =
         CreateSelectionStylingHelperProperty
         (
            nameof(DeselectedStyle),
            default(Style),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.DeselectedStyle = newVal;
            }
         );

      public static readonly BindableProperty SelectedStyleProperty =
         CreateSelectionStylingHelperProperty
         (
            nameof(SelectedStyle),
            default(Style),
            BindingMode.OneWay,
            (
               shapeView,
               oldVal,
               newVal
            ) =>
            {
               shapeView.SelectedStyle = newVal;
            }
         );

      private IHaveSelectionStylingHelper _stylingHost;
      private ICanOverrideSelectionAndAlternation _overrideProvider;

      public Style AlternateDeselectedStyle
      {
         get => _alternateDeselectedStyle;
         set
         {
            _alternateDeselectedStyle = value;
            AssignCurrentStyle();
         }
      }

      public Style DeselectedStyle
      {
         get => _deselectedStyle;
         set
         {
            _deselectedStyle = value;
            AssignCurrentStyle();
         }
      }

      public Style SelectedStyle
      {
         get => _selectedStyle;
         set
         {
            _selectedStyle = value;
            AssignCurrentStyle();
         }
      }

      private void AssignCurrentStyle()
      {
         if (_selectionProvider.IsNullOrDefault())
         {
            return;
         }

         if (_selectionProvider.IsSelected || (_overrideProvider.IsNotNullOrDefault() && _overrideProvider.IsSelected))
         {
            _hostView.Style = SelectedStyle;
         }
         else if ((_alternateProvider.IsNotNullOrDefault() && AlternateDeselectedStyle.IsNotNullOrDefault()) || (_overrideProvider.IsNotNullOrDefault() && _overrideProvider.IsAnAlternate))
         {
            _hostView.Style = AlternateDeselectedStyle;
         }
         else
         {
            _hostView.Style = DeselectedStyle;
         }

         _hostView.ForceStyle(_hostView.Style);

         _stylingHost?.AfterStyleApplied();
      }

      public static BindableProperty CreateSelectionStylingHelperProperty<PropertyTypeT>
      (
         string localPropName,
         PropertyTypeT defaultVal =
            default,
         BindingMode bindingMode =
            BindingMode.OneWay,
         Action<SelectionStylingHelper, PropertyTypeT, PropertyTypeT> callbackAction = null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      public bool CanAttach(View hostView)
      {
         if (hostView is ICanBeSelected hostAsSelectable)
         {
            _selectionProvider = hostAsSelectable;
         }
         else if (hostView is IHaveSelectionProvider hostAsHavingSelectionProvider)
         {
            _selectionProvider = hostAsHavingSelectionProvider.SelectionProvider;
         }
         else
         {
            Debug.WriteLine(nameof(SelectionStylingHelper) + " constructor: " + nameof(hostView) + " must be either selectable or hostView a view that is selectable.");
            return false;
         }

         // Corner case: override cases
         if (hostView is IHaveSelectionAndAlternationProvider
                selectionProviderAsHavingSelectionAndAlternationProvider)
         {
            _overrideProvider =
               selectionProviderAsHavingSelectionAndAlternationProvider.SelectionAndAlternationProvider;

            if (_overrideProvider != null)
            {
               _overrideProvider.ConsiderSelectionOrAlterationOverride += (provider) => { AssignCurrentStyle(); };
            }
         }

         _hostView = hostView;

         // SUCCESS 
         if (_selectionProvider is IHaveSelectionStylingHelper selectionProviderAsStylingHost)
         {
            _stylingHost = selectionProviderAsStylingHost;
         }

         // Optional
         _alternateProvider = _selectionProvider as ICanAlternate;

         AssignCurrentStyle();

         _selectionProvider.IsSelectedChanged += isSelected => { AssignCurrentStyle(); };

         if (_alternateProvider.IsNotNullOrDefault())
         {
            // ReSharper disable once PossibleNullReferenceException
            _alternateProvider.IsAlternateChanged += isAlternate => { AssignCurrentStyle(); };
         }

         return true;
      }
   }
}
