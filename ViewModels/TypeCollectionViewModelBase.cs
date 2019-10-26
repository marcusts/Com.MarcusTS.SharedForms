#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, TypeCollectionViewModelBase.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.ViewModels
{
   using SharedUtils.Utils;
   using System;
   using System.Collections;

   /// <summary>
   /// Interface ITypeCollectionViewModel
   /// Implements the <see cref="Com.MarcusTS.SharedForms.ViewModels.IBindableViewModel" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.ViewModels.IHaveValidationViewModelHelper" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.ViewModels.IBindableViewModel" />
   /// <seealso cref="Com.MarcusTS.SharedForms.ViewModels.IHaveValidationViewModelHelper" />
   public interface ITypeCollectionViewModel : IBindableViewModel, IHaveValidationViewModelHelper
   {
      /// <summary>
      /// Gets the deselected icon path.
      /// </summary>
      /// <value>The deselected icon path.</value>
      string DeselectedIconPath    { get; }
      /// <summary>
      /// Creates new recordbuttontext.
      /// </summary>
      /// <value>The new record button text.</value>
      string NewRecordButtonText   { get; }
      /// <summary>
      /// Gets the name of the record text binding.
      /// </summary>
      /// <value>The name of the record text binding.</value>
      string RecordTextBindingName { get; }
      /// <summary>
      /// Gets the selected icon path.
      /// </summary>
      /// <value>The selected icon path.</value>
      string SelectedIconPath      { get; }
      /// <summary>
      /// Gets or sets the view models.
      /// </summary>
      /// <value>The view models.</value>
      IList ViewModels            { get; set; }

      /// <summary>
      /// Occurs when [record added or deleted].
      /// </summary>
      event EventUtils.NoParamsDelegate RecordAddedOrDeleted;

      /// <summary>
      /// Adds the new record.
      /// </summary>
      /// <returns>IHaveValidationViewModelHelper.</returns>
      IHaveValidationViewModelHelper AddNewRecord();

      /// <summary>
      /// Deletes the record.
      /// </summary>
      /// <param name="viewModel">The view model.</param>
      void DeleteRecord(object                            viewModel);

      /// <summary>
      /// Imports the records.
      /// </summary>
      /// <param name="records">The records.</param>
      void ImportRecords(IHaveValidationViewModelHelper[] records);

      /// <summary>
      /// Sorts the collection.
      /// </summary>
      void SortTheCollection();
   }

   /// <summary>
   /// Class TypeCollectionViewModelBase.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.ViewModels.BindableViewModel" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.ViewModels.ITypeCollectionViewModel" />
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <seealso cref="Com.MarcusTS.SharedForms.ViewModels.BindableViewModel" />
   /// <seealso cref="Com.MarcusTS.SharedForms.ViewModels.ITypeCollectionViewModel" />
   public abstract class TypeCollectionViewModelBase<T> : BindableViewModel, ITypeCollectionViewModel
      where T : class, IHaveValidationViewModelHelper
   {
      /// <summary>
      /// Occurs when [record added or deleted].
      /// </summary>
      public event EventUtils.NoParamsDelegate RecordAddedOrDeleted;

      /// <summary>
      /// Gets the deselected icon path.
      /// </summary>
      /// <value>The deselected icon path.</value>
      public abstract string                     DeselectedIconPath    { get; }
      /// <summary>
      /// Creates new recordbuttontext.
      /// </summary>
      /// <value>The new record button text.</value>
      public abstract string                     NewRecordButtonText   { get; }
      /// <summary>
      /// Gets the name of the record text binding.
      /// </summary>
      /// <value>The name of the record text binding.</value>
      public abstract string                     RecordTextBindingName { get; }
      /// <summary>
      /// Gets the selected icon path.
      /// </summary>
      /// <value>The selected icon path.</value>
      public abstract string                     SelectedIconPath      { get; }
      /// <summary>
      /// Gets or sets the validation helper.
      /// </summary>
      /// <value>The validation helper.</value>
      public IValidationViewModelHelper ValidationHelper      { get; set; } = new ValidationViewModelHelper();
      /// <summary>
      /// Gets or sets the view models.
      /// </summary>
      /// <value>The view models.</value>
      public abstract IList                      ViewModels            { get; set; }

      /// <summary>
      /// Adds the new record.
      /// </summary>
      /// <returns>IHaveValidationViewModelHelper.</returns>
      public IHaveValidationViewModelHelper AddNewRecord()
      {
         var newRecord = CreateNewRecord();

         AddRecordToViewModel(newRecord);

         ValidationHelper.RevalidateBehaviors();

         SortTheCollection();

         return newRecord;
      }

      /// <summary>
      /// Deletes the record.
      /// </summary>
      /// <param name="viewModel">The view model.</param>
      public void DeleteRecord(object viewModel)
      {
         if (ViewModels.IsNotAnEmptyList() && ViewModels.Contains(viewModel))
         {
            if (viewModel is IHaveValidationViewModelHelper viewModelAsHaveValidationViewModelHelper)
            {
               ValidationHelper.RemoveSubViewModelHelpers(new[] {viewModelAsHaveValidationViewModelHelper});
               viewModelAsHaveValidationViewModelHelper.ValidationHelper.PageIsValidChanged -= ResetPageIsValid();
            }

            ViewModels.Remove(viewModel);

            RecordAddedOrDeleted?.Invoke();
         }
      }

      /// <summary>
      /// Imports the records.
      /// </summary>
      /// <param name="records">The records.</param>
      public void ImportRecords(IHaveValidationViewModelHelper[] records)
      {
         foreach (var record in records)
         {
            AddRecordToViewModel(record as T);
         }

         ValidationHelper.RevalidateBehaviors();
         SortTheCollection();
      }

      /// <summary>
      /// Sorts the collection.
      /// </summary>
      public abstract void SortTheCollection();

      /// <summary>
      /// Creates the new record.
      /// </summary>
      /// <returns>T.</returns>
      protected virtual T CreateNewRecord()
      {
         return Activator.CreateInstance<T>();
      }

      /// <summary>
      /// Adds the record to view model.
      /// </summary>
      /// <param name="newRecord">The new record.</param>
      private void AddRecordToViewModel(T newRecord)
      {
         ViewModels.Add(newRecord);

         ValidationHelper.AddSubViewModelHelpers(new IHaveValidationViewModelHelper[] {newRecord});
         newRecord.ValidationHelper.PageIsValidChanged += ResetPageIsValid();

         RecordAddedOrDeleted?.Invoke();
      }

      /// <summary>
      /// Resets the page is valid.
      /// </summary>
      /// <returns>EventUtils.GenericDelegate&lt;System.Boolean&gt;.</returns>
      private EventUtils.GenericDelegate<bool> ResetPageIsValid()
      {
         return isValid => ValidationHelper.RevalidateBehaviors();
      }
   }
}