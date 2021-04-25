﻿#region

using Blooso.Droid;
using Blooso.Views.Controls;

using Xamarin.Forms;

#endregion

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]

namespace Blooso.Droid
{
    #region

    using Android.Content;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    #endregion

    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }
    }
}