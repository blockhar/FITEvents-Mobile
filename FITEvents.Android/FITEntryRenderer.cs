using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;
using FITEvents;
using FITEvents.Droid;

[assembly: ExportRenderer(typeof(Entry), typeof(FITEntryRenderer))]
namespace FITEvents.Droid
{
    class FITEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    Control.BackgroundTintList = ColorStateList.ValueOf(Color.White.ToAndroid());
                else
                    Control.Background.SetColorFilter(Color.White.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcAtop);
            }
        }
    }
}