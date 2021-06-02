// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=AnimatedStackLayout.cs
// company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.SharedForms.Views.SubViews
{
   using System.Collections.Generic;
   using System.Linq;
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   public interface IAnimatedStackLayout
   {
      int         AnimateInDelayMilliseconds        { get; set; }
      bool        AskChildrenToAnimateIn            { get; set; }
      bool        LoadForwards                      { get; set; }
      bool        LoadOnceOnlyUnlessChildrenChanged { get; set; }
      IList<View> SourceViews                       { get; set; }
      double      ViewSpacing                       { get; set; }

      void AnimateIn();
   }

   public class AnimatedStackLayout : StackLayout, IAnimatedStackLayout
   {
      private const int                 DEFAULT_ANIMATE_IN_DELAY_MILLISECONDS = 25;
      private static readonly Thickness DEFAULT_STACK_LAYOUT_MARGIN           = new Thickness(10.0.AdjustForOsAndDevice());
      private static readonly double    DEFAULT_STACK_LAYOUT_SPACING          = 10.0.AdjustForOsAndDevice();
      private bool                      _animateInEntered;
      private bool                      _hasAnimatedOnce;

      public AnimatedStackLayout()
      {
         this.SetDefaults();
         Margin  = DEFAULT_STACK_LAYOUT_MARGIN;
         Spacing = DEFAULT_STACK_LAYOUT_SPACING;
      }

      public int AnimateInDelayMilliseconds { get; set; } = DEFAULT_ANIMATE_IN_DELAY_MILLISECONDS;

      public bool AskChildrenToAnimateIn { get; set; }

      public bool LoadForwards { get; set; }

      public bool LoadOnceOnlyUnlessChildrenChanged { get; set; }

      public IList<View> SourceViews { get; set; } = new List<View>();

      public double ViewSpacing { get; set; } = DEFAULT_STACK_LAYOUT_SPACING;

      /// <summary>Void because iOS objects to this being run as a task inside of Task.Run.</summary>
      public void AnimateIn()
      {
         if (_animateInEntered || _hasAnimatedOnce && LoadOnceOnlyUnlessChildrenChanged)
         {
            return;
         }

         _animateInEntered = true;

         try
         {
            Children.Clear();
            _hasAnimatedOnce = true;

            if (SourceViews.IsAnEmptyList())
            {
               return;
            }

            if (LoadForwards)
            {
               // Insert backwards at position 0; creates a cheap-thrills animation.
               foreach (var view in SourceViews.ToArray())
               {
                  Children.Add(view);
                  ConsiderChildAnimation(view);
               }
            }
            else
            {
               // Insert backwards at position 0; creates a cheap-thrills animation.
               foreach (var view in SourceViews.Reverse().ToArray())
               {
                  Children.Insert(0, view);
                  ConsiderChildAnimation(view);
               }
            }
         }
         finally
         {
            _animateInEntered = false;
         }
      }

      protected override void OnAdded(View view)
      {
         base.OnAdded(view);

         _hasAnimatedOnce = false;
      }

      protected override void OnRemoved(View view)
      {
         base.OnRemoved(view);

         _hasAnimatedOnce = false;
      }

      private void ConsiderChildAnimation(View view)
      {
         if (AskChildrenToAnimateIn && view is IAnimatedStackLayout viewAsAnimatedStackLayout)
         {
            viewAsAnimatedStackLayout.AnimateIn();
         }
      }
   }
}