#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, CompleteCommand.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Commands
{
   using System;
   using System.Windows.Input;
   using Xamarin.Forms;

   /// <summary>
   /// Interface ICompleteCommand
   /// Implements the <see cref="System.Windows.Input.ICommand" />
   /// </summary>
   /// <seealso cref="System.Windows.Input.ICommand" />
   public interface ICompleteCommand : ICommand
   {
      /// <summary>
      /// Changes the can execute.
      /// </summary>
      void ChangeCanExecute();
   }

   /// <summary>
   /// Class CompleteCommand.
   /// Implements the <see cref="Xamarin.Forms.Command" />
   /// Implements the <see cref="Com.MarcusTS.SharedForms.Common.Commands.ICompleteCommand" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Command" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Commands.ICompleteCommand" />
   public class CompleteCommand : Command, ICompleteCommand
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="CompleteCommand" /> class.
      /// </summary>
      /// <param name="execute">The execute.</param>
      public CompleteCommand(Action<object> execute) : base(execute)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="CompleteCommand" /> class.
      /// </summary>
      /// <param name="execute">The execute.</param>
      public CompleteCommand(Action execute) : base(execute)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="CompleteCommand" /> class.
      /// </summary>
      /// <param name="execute">The execute.</param>
      /// <param name="canExecute">The can execute.</param>
      public CompleteCommand(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="CompleteCommand" /> class.
      /// </summary>
      /// <param name="execute">The execute.</param>
      /// <param name="canExecute">The can execute.</param>
      public CompleteCommand(Action execute, Func<bool> canExecute) : base(execute, canExecute)
      {
      }
   }
}