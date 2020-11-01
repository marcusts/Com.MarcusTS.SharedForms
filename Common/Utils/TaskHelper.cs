// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, TaskHelper.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.SharedForms.Common.Utils
{
   using System;
   using System.Diagnostics;
   using System.Threading.Tasks;
   using Xamarin.Forms;

   /// <summary>
   /// Class TaskHelper.
   /// </summary>
   public static class TaskHelper
   {
      /// <summary>
      /// Begins the invoke on main thread asynchronous.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="a">a.</param>
      /// <returns>Task&lt;T&gt;.</returns>
      public static Task<T> BeginInvokeOnMainThreadAsync<T>(Func<T> a)
      {
         var tcs = new TaskCompletionSource<T>();

         Device.BeginInvokeOnMainThread(() =>
                                        {
                                           try
                                           {
                                              var result = a();
                                              tcs.SetResult(result);
                                           }
                                           catch (Exception ex)
                                           {
                                              tcs.SetException(ex);
                                           }
                                        });

         return tcs.Task;
      }

      /// <summary>
      /// Begins the invoke on main thread asynchronous.
      /// </summary>
      /// <param name="a">a.</param>
      /// <returns>Task.</returns>
      public static Task BeginInvokeOnMainThreadAsync(Action a)
      {
         var tcs = new TaskCompletionSource<bool>();
         Device.BeginInvokeOnMainThread(() =>
                                        {
                                           try
                                           {
                                              a();
                                              tcs.SetResult(true);
                                           }
                                           catch (Exception ex)
                                           {
                                              tcs.SetException(ex);
                                           }
                                        });
         return tcs.Task;
      }

      /// <summary>
      /// Runs the parallel.
      /// </summary>
      /// <param name="task">The task.</param>
      /// <param name="taskCallback">The task callback.</param>
      /// <param name="actionCallback">The action callback.</param>
      public static void RunParallel
      (
         Task task,
         Task taskCallback = null,
         Action actionCallback = null
      )
      {
         try
         {
            Task.Run
            (
               async () =>
               {
                  await task.WithoutChangingContext();

                  if (taskCallback != null)
                  {
                     Device.BeginInvokeOnMainThread
                     (
                        async () => { await taskCallback.WithoutChangingContext(); }
                     );
                  }

                  if (actionCallback != null)
                  {
                     Device.BeginInvokeOnMainThread
                     (
                        actionCallback.Invoke
                     );
                  }
               }
            );
         }
         catch (Exception ex)
         {
            Debug.WriteLine(nameof(RunParallel) + " error ->" + ex.Message + "<-");
         }
      }
   }
}
