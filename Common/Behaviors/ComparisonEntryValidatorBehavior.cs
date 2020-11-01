// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ComparisonEntryValidatorBehavior.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using SharedUtils.Utils;
   using System;
   using System.ComponentModel;
   using Xamarin.Forms;

   /// <summary>
   /// Can pass in an illegal character filter. Also, to be valid, the two strings *must* match. Implements the
   /// <see cref="EntryValidationBehavior" />
   /// Implements the <see cref="EntryValidationBehavior" />
   /// </summary>
   /// <seealso cref="EntryValidationBehavior" />
   /// <seealso cref="EntryValidationBehavior" />
   public class ComparisonEntryValidatorBehavior : EntryValidationBehavior
   {
      /// <summary>
      /// The compare entry
      /// </summary>
      private Entry _compareEntry;

      /// <summary>
      /// Initializes a new instance of the <see cref="ComparisonEntryValidatorBehavior" /> class.
      /// </summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      /// <param name="illegalCharFilter">The illegal character filter.</param>
      public ComparisonEntryValidatorBehavior
      (
         Action onIsValidChangedAction,
         Func<IEntryValidationBehavior, string, string, string> illegalCharFilter = null
      )
         : base(onIsValidChangedAction)
      {
      }

      /// <summary>
      /// Gets or sets the compare entry.
      /// </summary>
      /// <value>The compare entry.</value>
      public Entry CompareEntry
      {
         get => _compareEntry;

         set
         {
            if (_compareEntry != null)
            {
               _compareEntry.PropertyChanged -= CompareEntryOnPropertyChanged;
            }

            _compareEntry = value;

            if (_compareEntry != null)
            {
               _compareEntry.PropertyChanged += CompareEntryOnPropertyChanged;
            }
         }
      }

      /// <summary>
      /// Determines whether [is whole entry valid] [the specified behavior].
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="currentText">The current text.</param>
      /// <returns><c>true</c> if [is whole entry valid] [the specified behavior]; otherwise, <c>false</c>.</returns>
      protected override bool IsWholeEntryValid(IEntryValidationBehavior behavior, string currentText)
      {
         return base.IsWholeEntryValid(behavior, currentText) && ValidateCompareEntry(behavior, currentText);
      }

      /// <summary>
      /// Compares the entry on property changed.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="propertyChangedEventArgs">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
      private void CompareEntryOnPropertyChanged
      (
         object sender,
         PropertyChangedEventArgs propertyChangedEventArgs
      )
      {
         RevalidateEditorText();
      }

      /// <summary>
      /// Validates the compare entry.
      /// </summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="str">The string.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      private bool ValidateCompareEntry
      (
         IEntryValidationBehavior behavior,
         string str
      )
      {
         return CompareEntry != null
              &&
                str.IsNotEmpty()
              &&
                CompareEntry.Text.IsSameAs(str);
      }
   }
}
