using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Desive2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Desive2.Droid.DependencyServices
{
    public class CloseApplication : ICloseApplication
    {
        public void Close()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}