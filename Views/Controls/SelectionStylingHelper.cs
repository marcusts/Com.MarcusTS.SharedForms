// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, SelectionStylingHelper.cs, is a part of a program called AccountViewMobile.
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
   using Common.Utils;
   using SharedUtils.Utils;
   using System;
   using System.Diagnostics;
   using Xamarin.Forms;

   /// <summary>
   /// Interface ICanAlternate
   /// </summary>
   public interface ICanAlternate
   {
      /// <summary>
      /// Occurs when [is alternate changed].
      /// </summary>
      event EventUtils.GenericDelegate<ICanAlternate> IsAlternateChanged;

      // Set internally only
      /// <summary>
      /// Gets or sets a value indicating whether this instance is an alternate.
      /// </summary>
      /// <value><c>true</c> if this instance is an alternate; otherwise, <c>false</c>.</value>
      bool IsAnAlternate { get; set; }
   }

   /// <summary>
   /// Interface ICanBeSelected
   /// </summary>
   public interface ICanBeSelected
   {
      /// <summary>
      /// Occurs when [is selected changed].
      /// </summary>
      event EventUtils.GenericDelegate<ICanBeSelected> IsSelectedChanged;

      /// <summary>
      /// Gets or sets a value indicating whether this instance is selected.
      /// </summary>
      /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
      bool IsSelected { get; set; }

      /// <summary>
      /// True if the user attempts t select but wee are prevented from selection by various rules.
      /// </summary>
      /// <value><c>true</c> if this instance is trying to be selected; otherwise, <c>false</c>.</value>
      bool IsTryingToBeSelected { get; set; }

      /// <summary>
      /// Afters the selection change.
      /// </summary>
      /// <param name="item">The item.</param>
      void AfterSelectionChange(ICanBeSelected item);

      /// <summary>
      /// Determines whether this instance [can selection be made] the specified is selected.
      /// </summary>
      /// <param name="isSelected">if set to <c>true</c> [is selected].</param>
      /// <returns><c>true</c> if this instance [can selection be made] the specified is selected; otherwise, <c>false</c>.</returns>
      bool CanSelectionBeMade(bool isSelected);
   }

   /// <summary>
   /// Interface ICanOverrideSelectionAndAlternation
   /// Implements the <see cref="ICanBeSelected" />
   /// Implements the <see cref="ICanAlternate" />
   /// </summary>
   /// <seealso cref="ICanBeSelected" />
   /// <seealso cref="ICanAlternate" />
   public interface ICanOverrideSelectionAndAlternation : ICanBeSelected, ICanAlternate
   {
      /// <summary>
      /// Occurs when [consider selection or alteration override].
      /// </summary>
      event EventUtils.GenericDelegate<ICanOverrideSelectionAndAlternation> ConsiderSelectionOrAlterationOverride;
   }

   /// <summary>
   /// Interface IHaveSelectionAndAlternationProvider
   /// </summary>
   public interface IHaveSelectionAndAlternationProvider
   {
      /// <summary>
      /// Gets the selection and alternation provider.
      /// </summary>
      /// <value>The selection and alternation provider.</value>
      ICanOverrideSelectionAndAlternation SelectionAndAlternationProvider { get; }
   }

   /// <summary>
   /// Interface IHaveSelectionProvider
   /// </summary>
   public interface IHaveSelectionProvider
   {
      /// <summary>
      /// Gets the selection provider.
      /// </summary>
      /// <value>The selection provider.</value>
      ICanBeSelected SelectionProvider { get; }
   }

   /// <summary>
   /// Interface IHaveSelectionStylingHelper
   /// </summary>
   public interface IHaveSelectionStylingHelper
   {
      /// <summary>
      /// Gets or sets the styling helper.
      /// </summary>
      /// <value>The styling helper.</value>
      ISelectionStylingHelper StylingHelper { get; set; }

      /// <summary>
      /// Afters the style applied.
      /// </summary>
      void AfterStyleApplied();
   }

   /// <summary>
   /// Interface ISelectionStylingHelper
   /// </summary>
   public interface ISelectionStylingHelper
   {
      /// <summary>
      /// Gets or sets the alternate deselected style.
      /// </summary>
      /// <value>The alternate deselected style.</value>
      Style AlternateDeselectedStyle { get; set; }

      /// <summary>
      /// Gets or sets the deselected style.
      /// </summary>
      /// <value>The deselected style.</value>
      Style DeselectedStyle { get; set; }

      /// <summary>
      /// Gets or sets the selected style.
      /// </summary>
      /// <value>The selected style.</value>
      Style SelectedStyle { get; set; }

      /// <summary>
      /// Determines whether this instance can attach the specified host view.
      /// </summary>
      /// <param name="hostView">The host view.</param>
      /// <returns><c>true</c> if this instance can attach the specified host view; otherwise, <c>false</c>.</returns>
      bool CanAttach(View hostView);
   }

   /// <summary>
   /// Class SelectionStylingHelper.
   /// Implements the <see cref="ISelectionStylingHelper" />
   /// </summary>
   /// <seealso cref="ISelectionStylingHelper" />
   public class SelectionStylingHelper : ISelectionStylingHelper
   {
      /// <summary>
      /// The alternate deselected style property
      /// </summary>
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

      /// <summary>
      /// The deselected style property
      /// </summary>
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

      /// <summary>
      /// The selected style property
      /// </summary>
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

      /// <summary>
      /// The alternate deselected style
      /// </summary>
      private Style _alternateDeselectedStyle;

      /// <summary>
      /// The alternate provider
      /// </summary>
      private ICanAlternate _alternateProvider;

      /// <summary>
      /// The deselected style
      /// </summary>
      private Style _deselectedStyle;

      /// <summary>
      /// The host view
      /// </summary>
      private View _hostView;

      /// <summary>
      /// The override provider
      /// </summary>
      private ICanOverrideSelectionAndAlternation _overrideProvider;

      /// <summary>
      /// The selected style
      /// </summary>
      private Style _selectedStyle;

      /// <summary>
      /// The selection provider
      /// </summary>
      private ICanBeSelected _selectionProvider;

      /// <summary>
      /// The styling host
      /// </summary>
      private IHaveSelectionStylingHelper _stylingHost;

      /// <summary>
      /// Gets or sets the alternate deselected style.
      /// </summary>
      /// <value>The alternate deselected style.</value>
      public Style AlternateDeselectedStyle
      {
         get => _alternateDeselectedStyle;
         set
         {
            _alternateDeselectedStyle = value;
            AssignCurrentStyle();
         }
      }

      /// <summary>
      /// Gets or sets the deselected style.
      /// </summary>
      /// <value>The deselected style.</value>
      public Style DeselectedStyle
      {
         get => _deselectedStyle;
         set
         {
            _deselectedStyle = value;
            AssignCurrentStyle();
         }
      }

      /// <summary>
      /// Gets or sets the selected style.
      /// </summary>
      /// <value>The selected style.</value>
      public Style SelectedStyle
      {
         get => _selectedStyle;
         set
         {
            _selectedStyle = value;
            AssignCurrentStyle();
         }
      }

      /// <summary>
      /// Creates the selection styling helper property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
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

      /// <summary>
      /// Determines whether this instance can attach the specified host view.
      /// </summary>
      /// <param name="hostView">The host view.</param>
      /// <returns><c>true</c> if this instance can attach the specified host view; otherwise, <c>false</c>.</returns>
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
            Debug.WriteLine(nameof(SelectionStylingHelper) + " constructor: " + nameof(hostView) +
                            " must be either selectable or hostView a view that is selectable.");
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
               _overrideProvider.ConsiderSelectionOrAlterationOverride += provider => { AssignCurrentStyle(); };
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

      /// <summary>
      /// Assigns the current style.
      /// </summary>
      private void AssignCurrentStyle()
      {
         if (_selectionProvider.IsNullOrDefault())
         {
            return;
         }

         if (_selectionProvider.IsSelected || _overrideProvider.IsNotNullOrDefault() && _overrideProvider.IsSelected)
         {
            _hostView.Style = SelectedStyle;
         }
         else if (_alternateProvider.IsNotNullOrDefault() && AlternateDeselectedStyle.IsNotNullOrDefault() ||
                  _overrideProvider.IsNotNullOrDefault() && _overrideProvider.IsAnAlternate)
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
   }
}
