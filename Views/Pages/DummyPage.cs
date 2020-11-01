// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 01-01-2019
//
// <copyright file="DummyPage.cs" company="Marcus Technical Services, Inc.">
//     Copyright @2018 Marcus Technical Services, Inc.
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

using Com.MarcusTS.SharedForms.Common.Interfaces;

namespace Com.MarcusTS.SharedForms.Views.Pages
{
   using Common.Utils;
   using Xamarin.Forms;

   /// <summary>
   /// Class DummyPage.
   /// Implements the <see cref="IDummyPageViewModel" />
   /// </summary>
   /// <seealso cref="IDummyPageViewModel" />
   public class DummyPage : MenuNavPageBase<IDummyPageViewModel>
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="DummyPage" /> class.
      /// </summary>
      public DummyPage()
      {
         BackgroundColor = ColorUtils.MAIN_PAGE_BACKGROUND_COLOR;
      }

      /// <summary>
      /// Constructs the page view.
      /// </summary>
      /// <returns>View.</returns>
      protected override View ConstructPageView()
      {
         return new ContentView();
      }
   }
}
