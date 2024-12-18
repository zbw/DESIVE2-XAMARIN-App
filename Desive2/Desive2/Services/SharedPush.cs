using Com.CleverPush;
using Desive2.Models;
using System.Diagnostics;
using Xamarin.Essentials;

namespace Desive2.Services
{
    public static class SharedPush
    {
        public static async void Initialize()
        {
            string id = await Database.GetCleverpushChannelId(Preferences.Get("loginToken", null));

            if(id != null)
            {

                CleverPush.Current.StartInit(id)
                   .HandleNotificationOpened((result) =>
                   {
                       Debug.WriteLine("CleverPush HandleNotificationOpened: {0}", result.notification.title);
                   })
                  .HandleNotificationReceived((result) =>
                  {

                  })
                  .HandleSubscribed((subscriptionId) =>
                  {
                      Upload(subscriptionId);
                  })
                  .EndInit();
            }

        }

        public static async void Subscribe()
        {
          
            await Database.SetPushNotif(1, Preferences.Get("loginToken", null));
            Preferences.Set("receivesPush", true);

        }

        public static async void Unsubscribe()
        {
            await Database.SetPushNotif(0, Preferences.Get("loginToken", null));
            Preferences.Set("receivesPush", false);

        }
        private static async void Upload(string subscriptionId)
        {
            await Database.UploadRegristrationId(subscriptionId, Preferences.Get("loginToken", null));
            //Subscribe();
           
        }

    }
}
