using System;
using Android.App;
using Android.OS;
using Android.Runtime;



namespace Desive2.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) :
            base(handle, transer)
        {
        }

        public override void OnCreate()
        {
//            base.OnCreate();
//            //Set the default notification channel for your app when running Android Oreo
//            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
//            {
//                //Change for your default notification channel id here
//                FirebasePushNotificationManager.DefaultNotificationChannelId = "Erinnerung";

//                //Change for your Default notification channel name here
//                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";

//                FirebasePushNotificationManager.DefaultNotificationChannelImportance = NotificationImportance.Max;
//            }

//            //If debug you should reset the token each time
//#if DEBUG
//            FirebasePushNotificationManager.Initialize(this, true);
//#else
//            FirebasePushNotificationManager.Initialize(this, false);
//#endif
//            //Handle notification when app is close here
//            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
//            {

//            };
        }
    }
}