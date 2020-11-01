// *********************************************************************************
// <copyright file=ShapeType.cs company="Marcus Technical Services, Inc.">
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
   /// <summary>
   ///    Represents pre-defined shape types
   /// </summary>
   public enum ShapeType
   {
      /// <summary>
      ///    A 4-sides shape (square/rectangle) - can have rounded corners
      /// </summary>
      Box,

      /// <summary>
      ///    A circle shape with a radius equals to the minimum value between height &amp; width
      /// </summary>
      Circle,

      /// <summary>
      ///    A star shape for which you can define the number of points and the radius ratio
      /// </summary>
      Star,

      /// <summary>
      ///    A triangle shape - the appearance depends on the height/width ratio
      /// </summary>
      Triangle,

      /// <summary>
      ///    An oval shape - the appearance depends on the height/width ratio
      /// </summary>
      Oval,

      /// <summary>
      ///    A diamond shape - 4-sides - the same you can find in a card deck - the appearance depends on the height/width ratio
      /// </summary>
      Diamond,

      /// <summary>
      ///    A heart shape - the appearance depends on the minimum value between height &amp; width
      /// </summary>
      Heart,

      /// <summary>
      ///    A progress circle shape acting like a progress bar with a radius equals to the minimum value between height &amp;
      ///    width
      /// </summary>
      ProgressCircle,

      /// <summary>
      ///    A custom path shape defined by a list of points
      /// </summary>
      Path
   }
}
