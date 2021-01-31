
#define LOAD_BACKWARDS

namespace Com.MarcusTS.SharedForms.Views.SubViews
{
   using System.Collections.Generic;
   using System.Linq;
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   public interface IAnimatedStackLayout
   {
      int         AnimateInDelayMilliseconds { get; }
      IList<View> SourceViews                { get; set; }

      void AnimateIn();
   }

   public class AnimatedStackLayout : StackLayout, IAnimatedStackLayout
   {
      private const int DEFAULT_ANIMATE_IN_DELAY_MILLISECONDS = 25;

      public AnimatedStackLayout()
      {
         this.SetViewDefaults();
         Orientation = StackOrientation.Vertical;
         Margin      = new Thickness(20.0.AdjustForOsAndDevice());
      }

      public int         AnimateInDelayMilliseconds { get; }      = DEFAULT_ANIMATE_IN_DELAY_MILLISECONDS;
      public IList<View> SourceViews                { get; set; } = new List<View>();

      /// <summary>
      ///    Void because iOS objects to this being run as a task inside of Task.Run.
      ///    NOTE the async inside of MainThread.BeginInvokeOnMainThread.
      /// </summary>
      public void AnimateIn()
      {
         Children.Clear();

         if (SourceViews.IsAnEmptyList())
         {
            return;
         }

#if LOAD_BACKWARDS
         // Insert backwards at position 0; creates a cheap-thrills animation.
         foreach (var view in SourceViews.Reverse().ToArray())
         {
            Children.Insert(0, view);
         }
#else
         // Insert backwards at position 0; creates a cheap-thrills animation.
         foreach (var view in SourceViews.ToArray())
         {
            Children.Add(view);
         }
#endif
      }
   }
}