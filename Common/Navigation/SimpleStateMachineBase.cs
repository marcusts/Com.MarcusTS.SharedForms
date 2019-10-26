#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, SimpleStateMachineBase.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Navigation
{
   using SharedUtils.Utils;
   using SmartDI;
   using System.Collections.Generic;
   using System.Threading.Tasks;
   using Utils;

   /// <summary>
   /// Interface IStateMachineBase
   /// </summary>
   public interface ISimpleStateMachine
   {
      /// <summary>
      /// Gets the default state.
      /// </summary>
      /// <value>The default state.</value>
      string DefaultState { get; }

      /// <summary>
      /// Gets or sets the di container.
      /// </summary>
      /// <value>The di container.</value>
      ISmartDIContainer DIContainer { get; set; }

      /// <summary>
      /// Gets the start up state.
      /// </summary>
      /// <value>The start up state.</value>
      string StartUpState { get; }

      /// <summary>
      /// Goes the state of to application.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="forceState">Ignores the comparison with the last app state; this change must occur.</param>
      /// <param name="andRebuildToolbars">if set to <c>true</c> [and rebuild toolbars].</param>
      /// <returns>Task.</returns>
      Task GoToAppState
      (
         string newState,
         bool   forceState         = false,
         bool   andRebuildToolbars = false
      );

      /// <summary>
      /// Goes to default state.
      /// </summary>
      /// <param name="forceAppState">if set to <c>true</c> [force application state].</param>
      /// <param name="andRebuildStageToolbars">if set to <c>true</c> [and rebuild stage toolbars].</param>
      /// <returns>Task.</returns>
      Task GoToDefaultState(bool forceAppState = false, bool andRebuildStageToolbars = false);

      /// <summary>
      /// Goes the state of to last application.
      /// </summary>
      /// <returns>Task.</returns>
      Task GoToLastAppState();

      /// <summary>
      /// Goes the state of to start up.
      /// </summary>
      /// <returns>Task.</returns>
      Task GoToStartUpState();
   }

   /// <summary>
   /// Class StateMachineBase. Implements the <see cref="ISimpleStateMachine" /> Implements the
   /// <see cref="Com.MarcusTS.SharedForms.Common.Navigation.ISimpleStateMachine" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Navigation.ISimpleStateMachine" />
   /// <seealso cref="ISimpleStateMachine" />
   public abstract class SimpleStateMachineBase : ISimpleStateMachine
   {
      /// <summary>
      /// The last application state
      /// </summary>
      private string _lastAppState;

      /// <summary>
      /// Gets the default state.
      /// </summary>
      /// <value>The default state.</value>
      public abstract string DefaultState { get; }

      /// <summary>
      /// Gets or sets the di container.
      /// </summary>
      /// <value>The di container.</value>
      public ISmartDIContainer DIContainer { get; set; } = new SmartDIContainer();

      /// <summary>
      /// Gets the start up state.
      /// </summary>
      /// <value>The start up state.</value>
      public abstract string StartUpState { get; }

      /// <summary>
      /// Goes the state of to application.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="forceState">Ignores the comparison with the last app state; this change must occur.</param>
      /// <param name="andRebuildToolbars">if set to <c>true</c> [and rebuild toolbars].</param>
      /// <returns>Task.</returns>
      public async Task GoToAppState
      (
         string newState,
         bool   forceState         = false,
         bool   andRebuildToolbars = false
      )
      {
         if (!forceState && _lastAppState.IsSameAs(newState))
         {
            return;
         }

         // Done early to prevent recursion
         _lastAppState = newState;

         await RespondToAppStateChange(newState, andRebuildToolbars).WithoutChangingContext();
      }

      /// <summary>
      /// Goes to default state.
      /// </summary>
      /// <param name="forceAppState">if set to <c>true</c> [force application state].</param>
      /// <param name="andRebuildStageToolbars">if set to <c>true</c> [and rebuild stage toolbars].</param>
      /// <returns>Task.</returns>
      public async Task GoToDefaultState(bool forceAppState = false, bool andRebuildStageToolbars = false)
      {
         await GoToAppState(DefaultState, forceAppState, andRebuildStageToolbars).WithoutChangingContext();
      }

      /// <summary>
      /// Goes the state of to last application.
      /// </summary>
      public async Task GoToLastAppState()
      {
         await GoToAppState(_lastAppState, true).WithoutChangingContext();
      }

      /// <summary>
      /// Goes the state of to start up.
      /// </summary>
      /// <returns>Task.</returns>
      public async Task GoToStartUpState()
      {
         await GoToAppState(StartUpState).WithoutChangingContext();
      }

      /// <summary>
      /// Goes to a new state and then continues digging through nested states until the nested paths have been
      /// exhausted.
      /// </summary>
      /// <param name="nestedPaths">The nested paths.</param>
      /// <returns>Task.</returns>
      public static async Task GoToAppStateWithAdditionalPaths(StateMachineSubPath[] nestedPaths)
      {
         if (nestedPaths.IsEmpty())
         {
            return;
         }

         // Go to the first path
         await nestedPaths[0].StateMachine.GoToAppState(nestedPaths[0].AppState, true).WithoutChangingContext();

         if (nestedPaths.Length == 1)
         {
            return;
         }

         var newlyNestedPaths = new List<StateMachineSubPath>();
         for (var pathIdx = 1; pathIdx < nestedPaths.Length; pathIdx++)
         {
            newlyNestedPaths.Add(new StateMachineSubPath
            {
               StateMachine = nestedPaths[pathIdx].StateMachine,
               AppState     = nestedPaths[pathIdx].AppState
            });
         }

         var newlyNestedPathArray = newlyNestedPaths.ToArray();

         // Recur until finished
         await GoToAppStateWithAdditionalPaths(newlyNestedPathArray).WithoutChangingContext();
      }

      /// <summary>
      /// Responds to application state change.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="andRebuildToolbars">if set to <c>true</c> [and rebuild toolbars].</param>
      /// <returns>Task.</returns>
      protected abstract Task RespondToAppStateChange(string newState, bool andRebuildToolbars = false);
   }
}