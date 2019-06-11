// *********************************************************************************
// <copyright file=TaskHelper.cs company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Common.Utils
{
   using System;
   using System.Diagnostics;
   using System.Threading.Tasks;
   using Xamarin.Forms;

   /// <summary>
   ///    Class TaskHelper.
   /// </summary>
   public static class TaskHelper
   {
      /// <summary>
      ///    Begins the invoke on main thread asynchronous.
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
      ///    Begins the invoke on main thread asynchronous.
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
      ///    Runs the parallel.
      /// </summary>
      /// <param name="task">The task.</param>
      /// <param name="taskCallback">The task callback.</param>
      /// <param name="actionCallback">The action callback.</param>
      public static void RunParallel
      (
         Task   task,
         Task   taskCallback   = null,
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