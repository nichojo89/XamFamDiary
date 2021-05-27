using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(XamFamDiary.Renderers.EntryNoBorder), typeof(XamFamDiary.iOS.Renderers.EntryNoBorder))]
namespace XamFamDiary.iOS.Renderers
{
    public class EntryNoBorder : EntryRenderer
    {
        /// <summary>
        /// Removes entry from border
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            //Disable Border
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}