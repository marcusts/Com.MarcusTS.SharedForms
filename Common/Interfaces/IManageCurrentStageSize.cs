// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, IManageCurrentStageSize.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Interfaces
{
   using System.Threading.Tasks;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IManageCurrentStageSize
   /// Implements the <see cref="IReceiveParentStageSizeChanges" />
   /// Implements the <see cref="IProvideChildStageBounds" />
   /// </summary>
   /// <seealso cref="IReceiveParentStageSizeChanges" />
   /// <seealso cref="IProvideChildStageBounds" />
   public interface IManageCurrentStageSize : IReceiveParentStageSizeChanges, IProvideChildStageBounds
   {
   }

   /// <summary>
   /// Interface IProvideChildStageBounds
   /// Implements the <see cref="IProvideParentSizeOrOrientationChangeEvents" />
   /// </summary>
   /// <seealso cref="IProvideParentSizeOrOrientationChangeEvents" />
   public interface IProvideChildStageBounds : IProvideParentSizeOrOrientationChangeEvents
   {
      // Function goes and gets the size, which might be coerced by the parent
      /// <summary>
      /// Provides the new child stage bounds.
      /// </summary>
      /// <param name="child">The child.</param>
      /// <returns>Rectangle.</returns>
      Rectangle ProvideNewChildStageBounds(IReceiveParentStageSizeChanges child);
   }

   /// <summary>
   /// Interface IReceiveParentStageSizeChanges
   /// </summary>
   public interface IReceiveParentStageSizeChanges
   {
      /// <summary>
      /// Gets the current stage bounds.
      /// </summary>
      /// <value>The current stage bounds.</value>
      Rectangle CurrentStageBounds { get; }

      /// <summary>
      /// Gets or sets the stage bounds provider.
      /// </summary>
      /// <value>The stage bounds provider.</value>
      IProvideChildStageBounds StageBoundsProvider { get; set; }

      /// <summary>
      /// Sets the current stage bounds.
      /// </summary>
      /// <param name="newBounds">The new bounds.</param>
      /// <returns>Task.</returns>
      Task SetCurrentStageBounds(Rectangle newBounds);
   }
}
