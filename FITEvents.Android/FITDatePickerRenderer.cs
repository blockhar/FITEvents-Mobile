using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;
using FITEvents;
using FITEvents.Droid;

[assembly: ExportRenderer(typeof(DatePicker), typeof(FITDatePickerRenderer))]
namespace FITEvents.Droid
{
    class FITDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
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