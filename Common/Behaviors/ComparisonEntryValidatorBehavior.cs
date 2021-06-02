// ********************************************************************************* <copyright
// file=ComparisonEntryValidatorBehavior.cs company="Marcus Technical Services, Inc."> Copyright @2019 Marcus Technical
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

namespace Com.MarcusTS.SharedForms.Common.Behaviors
{
   using System;
   using System.ComponentModel;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   public interface IComparisonEntryValidatorBehavior : IEntryValidationBehavior
   {
      Entry CompareEntry { get; set; }
   }

   /// <summary>
   ///    Can pass in an illegal character filter. Also, to be valid, the two strings *must* match. Implements the
   ///    <see
   ///       cref="EntryValidationBehavior" />
   /// </summary>
   /// <seealso cref="EntryValidationBehavior" />
   public class ComparisonEntryValidatorBehavior : EntryValidationBehavior, IComparisonEntryValidatorBehavior
   {
      /// <summary>The compare entry</summary>
      private Entry _compareEntry;

      /// <summary>Initializes a new instance of the <see cref="ComparisonEntryValidatorBehavior" /> class.</summary>
      /// <param name="onIsValidChangedAction">The on is valid changed action.</param>
      /// <param name="illegalCharFilter">The illegal character filter.</param>
      public ComparisonEntryValidatorBehavior
      (
         Action                                                     onIsValidChangedAction,
         Func<IEntryValidationBehavior, string, string, string> illegalCharFilter = null
      )
         : base(onIsValidChangedAction)
      {
      }

      protected override bool IsWholeEntryValid(IEntryValidationBehavior behavior, string currentText)
      {
         return base.IsWholeEntryValid(behavior, currentText) && ValidateCompareEntry(behavior, currentText);
      }

      /// <summary>Gets or sets the compare entry.</summary>
      /// <value>The compare entry.</value>
      public Entry CompareEntry
      {
         get => _compareEntry;

         set
         {
            if (_compareEntry != null)
            {
               // _compareEntry.PropertyChanged -= CompareEntryOnPropertyChanged;
               _compareEntry.TextChanged -= CompareEntryOnPropertyChanged;
            }

            _compareEntry = value;

            if (_compareEntry != null)
            {
               _compareEntry.TextChanged += CompareEntryOnPropertyChanged;
            }
         }
      }

      /// <summary>Compares the entry on property changed.</summary>
      /// <param name="sender">The sender.</param>
      /// <param name="textChangedEventArgs"></param>
      /// The <see cref="PropertyChangedEventArgs" /> instance containing the event data.
      /// </param>
      private void CompareEntryOnPropertyChanged
      (
         object               sender,
         TextChangedEventArgs textChangedEventArgs
      )
      {
         Revalidate();
      }

      /// <summary>Validates the compare entry.</summary>
      /// <param name="behavior">The behavior.</param>
      /// <param name="str">The string.</param>
      /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
      private bool ValidateCompareEntry
      (
         IEntryValidationBehavior behavior,
         string                       str
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