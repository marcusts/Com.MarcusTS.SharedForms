#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ICanBeValid.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Interfaces
{
   using SharedUtils.Utils;

   /// <summary>
   /// Implements IsValid and IsInvalid booleans.
   /// </summary>
   public interface ICanBeValid
   {
      /// <summary>
      /// Returns true if ... is valid.
      /// </summary>
      /// <value><c>null</c> if [is valid] contains no value, <c>true</c> if [is valid]; otherwise, <c>false</c>.</value>
      bool? IsValid { get; }

      /// <summary>
      /// Gets or sets the last validation error.
      /// </summary>
      /// <value>The last validation error.</value>
      string LastValidationError { get; set; }

      /// <summary>
      /// Occurs when [is valid changed].
      /// </summary>
      event EventUtils.GenericDelegate<bool?> IsValidChanged;

      /// <summary>
      /// Revalidates this instance.
      /// </summary>
      void Revalidate();
   }
}