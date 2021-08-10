namespace Com.MarcusTS.SharedForms.Common.Interfaces
{
   using Com.MarcusTS.SharedUtils.Controls;
   using System.Threading.Tasks;
   using Xamarin.Forms;

   public interface ICanAnimate
   {
      int AnimateInDelayMilliseconds { get; set; }
      bool AutoReloadOnAnySourceViewChange { get; set; }
      bool CascadeChildAnimations { get; set; }
      IBetterObservableCollection<View> SourceViews { get; }
      Task SetSourceViews(View[] views);
      Task AnimateIn();
   }
}
