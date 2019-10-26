﻿#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, YesNoToggleImageLabelButton.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   /// <summary>
   /// Enum YesNo
   /// </summary>
   public enum YesNo
   {
      /// <summary>
      /// The no
      /// </summary>
      No,
      /// <summary>
      /// The yes
      /// </summary>
      Yes
   }

   /// <summary>
   /// Interface IYesNoToggleImageLabelButton
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IEnumToggleImageLabelButton{Com.MarcusTS.SharedForms.Views.Controls.YesNo}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IEnumToggleImageLabelButton{Com.MarcusTS.SharedForms.Views.Controls.YesNo}" />
   public interface IYesNoToggleImageLabelButton : IEnumToggleImageLabelButton<YesNo>
   {
   }

   /// <summary>
   /// Class YesNoToggleImageLabelButton.
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.EnumToggleImageLabelButtonBase{Com.MarcusTS.SharedForms.Views.Controls.YesNo}" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.EnumToggleImageLabelButtonBase{Com.MarcusTS.SharedForms.Views.Controls.YesNo}" />
   public class YesNoToggleImageLabelButton : EnumToggleImageLabelButtonBase<YesNo>
   {
   }
}