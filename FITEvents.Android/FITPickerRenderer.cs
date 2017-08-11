using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;
using FITEvents;
using FITEvents.Droid;


[assembly: ExportRenderer(typeof(Picker), typeof(FITPickerRenderer))]
namespace FITEvents.Droid
{
    class FITPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
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