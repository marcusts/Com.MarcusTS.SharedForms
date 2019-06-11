// *********************************************************************************
// <copyright file=ThreadSafeAccessor.cs company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Views.Controls
{
   using System.Threading;

   /// <summary>
   ///    Interface IThreadSafeAccessor
   /// </summary>
   public interface IThreadSafeAccessor
   {
      /// <summary>
      ///    Reads the stored value.
      /// </summary>
      /// <returns>System.Object.</returns>
      object ReadStoredValue();

      /// <summary>
      ///    Writes the stored value.
      /// </summary>
      /// <param name="valueToStore">The value to store.</param>
      void WriteStoredValue(object valueToStore);
   }

   /// <summary>
   ///    Class ThreadSafeAccessor.
   ///    Implements the <see cref="SharedForms.Views.Controls.IThreadSafeAccessor" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Controls.IThreadSafeAccessor" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Controls.IThreadSafeAccessor" />
   /// <seealso cref="SharedForms.Views.Controls.IThreadSafeAccessor" />
   public class ThreadSafeAccessor : IThreadSafeAccessor
   {
      /// <summary>
      ///    The stored value
      /// </summary>
      private object _storedValue;

      /// <summary>
      ///    Initializes a new instance of the <see cref="ThreadSafeAccessor" /> class.
      /// </summary>
      /// <param name="storedValue">The stored value.</param>
      public ThreadSafeAccessor(object storedValue = null)
      {
         if (storedValue != null)
         {
            WriteStoredValue(storedValue);
         }
      }

      /// <summary>
      ///    Reads the stored value.
      /// </summary>
      /// <returns>System.Object.</returns>
      public object ReadStoredValue()
      {
         return Interlocked.CompareExchange(ref _storedValue, 0, 0);
      }

      /// <summary>
      ///    Writes the stored value.
      /// </summary>
      /// <param name="valueToStore">The value to store.</param>
      public void WriteStoredValue(object valueToStore)
      {
         Interlocked.Exchange(ref _storedValue, valueToStore);
      }
   }
}