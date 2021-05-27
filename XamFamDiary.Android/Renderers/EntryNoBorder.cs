using Xamarin.Forms;
using Android.Content;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(XamFamDiary.Renderers.EntryNoBorder), typeof(XamFamDiary.Droid.Renderers.EntryNoBorder))]
namespace XamFamDiary.Droid.Renderers
{
    public class EntryNoBorder : EntryRenderer
    {
        public EntryNoBorder(Context context) : base(context)
        {
        }

        /// <summary>
        /// Removes border from entry
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Control == null)
                return;

            //Disable Border
            Control.Background = null;
        }
    }
}