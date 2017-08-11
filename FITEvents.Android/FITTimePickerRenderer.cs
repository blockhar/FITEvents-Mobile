using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;
using FITEvents;
using FITEvents.Droid;


[assembly: ExportRenderer(typeof(TimePicker), typeof(FITTimePickerRenderer))]
namespace FITEvents.Droid
{
    class FITTimePickerRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
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