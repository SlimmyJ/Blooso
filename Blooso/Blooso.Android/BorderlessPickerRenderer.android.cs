#region

using Blooso.Controls;
using Blooso.Droid;

using Xamarin.Forms;

#endregion

[assembly: ExportRenderer(typeof(BorderlessPicker), typeof(BorderlessPickerRenderer))]

namespace Blooso.Droid
{
    #region

    using Android.Content;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    #endregion

    public class BorderlessPickerRenderer : PickerRenderer
    {
        public BorderlessPickerRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null) return;
            Control.Background = null;

            var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
            layoutParams.SetMargins(0, 0, 0, 0);
            LayoutParameters = layoutParams;
            Control.LayoutParameters = layoutParams;
            Control.SetPadding(0, 0, 0, 0);
            SetPadding(0, 0, 0, 0);
        }
    }
}