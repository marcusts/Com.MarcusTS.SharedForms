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
   using System;
   using Com.MarcusTS.SharedForms.Common.Interfaces;
   using System.Linq;
   using System.Threading.Tasks;
   using Com.MarcusTS.SharedUtils.Controls;
   using Com.MarcusTS.SharedForms.Common.Utils;
   using Com.MarcusTS.SharedUtils.Utils;
   using Xamarin.Forms;

   public interface IAnimatedStackLayout : ICanAnimate
   {
      bool LoadForwards { get; set; }
   }

   /// <summary>
   /// AnimateInDelayMilliseconds is now ignored; it is not compatible with threading
   /// </summary>
   [Obsolete]
   public class AnimatedStackLayout : StackLayout, IAnimatedStackLayout
   {
      private                 bool      _animateInEntered;
      private                 bool      _hasAnimatedOnce;

      public AnimatedStackLayout()
      {
         this.SetDefaults();
         Margin  = FormsConst.DEFAULT_STACK_LAYOUT_MARGIN;
         Spacing = FormsConst.DEFAULT_STACK_LAYOUT_SPACING;
      }

      public int AnimateInDelayMilliseconds { get; set; } = FormsConst.DEFAULT_ANIMATE_IN_DELAY_MILLISECONDS;
      public bool AutoReloadOnAnySourceViewChange { get; set; }

      public bool CascadeChildAnimations { get; set; }

      public bool LoadForwards { get; set; }

      public bool LoadOnceOnlyUnlessChildrenChanged { get; set; }

      public IBetterObservableCollection<View> SourceViews { get; private set; } = new BetterObservableCollection<View>();

      public Task SetSourceViews(View[] views)
      {
         SourceViews = new BetterObservableCollection<View>(views);

         return Task.CompletedTask;
      }

      public async Task AnimateIn()
      {
         if (_animateInEntered || (_hasAnimatedOnce && LoadOnceOnlyUnlessChildrenChanged))
         {
            return;
         }

         _animateInEntered = true;

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
            }
         }
         else
         {
            // Insert backwards at position 0; creates a cheap-thrills animation.
            foreach (var view in SourceViews.Reverse().ToArray())
            {
               Children.Insert(0, view);
            }
         }

         foreach (var view in SourceViews.ToArray())
         {
            await ConsiderChildAnimation(view).WithoutChangingContext();
         }

         _animateInEntered = false;
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

      private async Task ConsiderChildAnimation(View view)
      {
         // If the view is itself a container that implements ICanAnimate, request that automatically.
         if (CascadeChildAnimations && view is ICanAnimate viewAsAnimatable)
         {
            await viewAsAnimatable.AnimateIn().WithoutChangingContext();
         }
      }
   }
}
