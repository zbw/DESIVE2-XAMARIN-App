using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using AndroidX.Core.Content;
using AndroidX.Core.App;
using Android;
using Android.Views;
using Android.Gms.Common;
using Android.Util;
using Firebase;
using Firebase.Messaging;
using Desive2.Services;

namespace Desive2.Droid
{
    [Activity(Label = "DESIVE²", ScreenOrientation = ScreenOrientation.Portrait ,Theme = "@style/MyTheme.Splash", NoHistory =false, MainLauncher = true, Icon = "@mipmap/launcher_foreground")]
    public class MainActivity : FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        public static readonly int PickImageId = 1000;
        internal static readonly int NOTIFICATION_ID = 100;
        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;
            base.OnCreate(savedInstanceState);       
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);      
            LoadApplication(new App());
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }
    }
}