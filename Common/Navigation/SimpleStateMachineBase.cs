// *********************************************************************************
// <copyright file=SimpleStateMachineBase.cs company="Marcus Technical Services, Inc.">
//     Copyright @2019 Marcus Technical Services, Inc.
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
// *********************************************************************************

namespace Com.MarcusTS.SharedForms.Common.Navigation
{
   using Com.MarcusTS.SharedForms.Common.Utils;
   using Com.MarcusTS.SharedUtils.Utils;
   using Com.MarcusTS.SmartDI;
   using System.Collections.Generic;
   using System.Threading.Tasks;

   /// <summary>
   ///    Interface IStateMachineBase
   /// </summary>
   public interface ISimpleStateMachine
   {
      /// <summary>
      ///    Gets the default state.
      /// </summary>
      /// <value>The default state.</value>
      string DefaultState { get; }

      /// <summary>
      ///    Gets or sets the di container.
      /// </summary>
      /// <value>The di container.</value>
      ISmartDIContainer DIContainer { get; set; }

      /// <summary>
      ///    Gets the start up state.
      /// </summary>
      /// <value>The start up state.</value>
      string StartUpState { get; }

      /// <summary>
      ///    Goes the state of to application.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="forceState">Ignores the comparison with the last app state; this change must occur.</param>
      /// <returns>Task.</returns>
      Task GoToAppState
      (
         string newState,
         bool   forceState = false
      );

      /// <summary>
      ///    Goes to default state.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <returns>Task.</returns>
      Task GoToDefaultState(string newState);

      /// <summary>
      ///    Goes the state of to start up.
      /// </summary>
      /// <returns>Task.</returns>
      Task GoToStartUpState();
   }

   /// <summary>
   ///    Class StateMachineBase.
   ///    Implements the <see cref="ISimpleStateMachine" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Common.Navigation.ISimpleStateMachine" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Common.Navigation.ISimpleStateMachine" />
   /// <seealso cref="ISimpleStateMachine" />
   public abstract class SimpleStateMachineBase : ISimpleStateMachine
   {
      /// <summary>
      ///    The last application state
      /// </summary>
      private string _lastAppState;

      /// <summary>
      ///    Gets the default state.
      /// </summary>
      /// <value>The default state.</value>
      public abstract string DefaultState { get; }

      /// <summary>
      ///    Gets or sets the di container.
      /// </summary>
      /// <value>The di container.</value>
      public ISmartDIContainer DIContainer { get; set; } = new SmartDIContainer();

      /// <summary>
      ///    Gets the start up state.
      /// </summary>
      /// <value>The start up state.</value>
      public abstract string StartUpState { get; }

      /// <summary>
      ///    Goes the state of to application.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <param name="forceState">Ignores the comparison with the last app state; this change must occur.</param>
      /// <returns>Task.</returns>
      public async Task GoToAppState
      (
         string newState,
         bool   forceState = false
      )
      {
         if (!forceState && _lastAppState.IsSameAs(newState))
         {
            return;
         }

         // Done early to prevent recursion
         _lastAppState = newState;

         await RespondToAppStateChange(newState).WithoutChangingContext();
      }

      /// <summary>
      ///    Goes to default state.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <returns>Task.</returns>
      public async Task GoToDefaultState(string newState)
      {
         await GoToAppState(DefaultState).WithoutChangingContext();
      }

      /// <summary>
      ///    Goes the state of to start up.
      /// </summary>
      /// <returns>Task.</returns>
      public async Task GoToStartUpState()
      {
         await GoToAppState(StartUpState).WithoutChangingContext();
      }

      /// <summary>
      ///    Goes to a new state and then continues digging through nested states until the nested paths have been exhausted.
      /// </summary>
      /// <param name="nestedPaths">The nested paths.</param>
      /// <returns>Task.</returns>
      public static async Task GoToAppStateWithAdditionalPaths(StateMachineSubPath[] nestedPaths)
      {
         if (nestedPaths.IsEmpty())
         {
            return;
         }

         // Strip off the leading path
         await nestedPaths[0].StateMachine.GoToAppState(nestedPaths[0].AppState, true);

         if (nestedPaths.Length == 1)
         {
            return;
         }

         var newlyNestedPaths = new List<StateMachineSubPath>();
         for (var pathIdx = 1; pathIdx < nestedPaths.Length; pathIdx++)
         {
            newlyNestedPaths.Add(new StateMachineSubPath { StateMachine = nestedPaths[pathIdx].StateMachine, AppState = nestedPaths[pathIdx].AppState });
         }

         var newlyNestedPathArray = newlyNestedPaths.ToArray();

         // Recur until finished
         await GoToAppStateWithAdditionalPaths(newlyNestedPathArray);
      }

      /// <summary>
      ///    Responds to application state change.
      /// </summary>
      /// <param name="newState">The new state.</param>
      /// <returns>Task.</returns>
      protected abstract Task RespondToAppStateChange(string newState);
   }
}