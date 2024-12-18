using Desive2.Models;

using Xamarin.Essentials;


namespace Desive2.Services
{
    public class PushTokenHandler
    {
        /// <summary>
        /// Subscribes the user to push notifications by setting preferences and updating the database.
        /// </summary>
        public static async void Subscribe()
        {
            try
            {
                // Set the subscription flag to true in preferences.
                Preferences.Set("subscribedToToken", true);

                // Update the database to indicate the user is subscribed to push notifications.
                await Database.SetPushNotif(1, Preferences.Get("loginToken", null));
            }
            catch
            {
                // Show an alert if there is an error with the subscription.
                await App.Current.MainPage.DisplayAlert("Fehler", "Bitte überprüfen Sie Ihre Internetverbindung.", "Okay");
            }
        }

        /// <summary>
        /// Performs database operations to handle the push notification registration ID.
        /// </summary>
        /// <param name="token">The push notification token.</param>
        private async static void DatabaseOperations(string token)
        {
            // Upload the user's registration ID to the database.
            await Database.UploadRegristrationId(Preferences.Get("idUser", null), token);

            // Set the push notification status to active in the database.
            await Database.SetPushNotif(1, Preferences.Get("loginToken", null));
        }

        /// <summary>
        /// Unsubscribes the user from push notifications by resetting preferences and updating the database.
        /// </summary>
        public static async void Unsubscribe()
        {
            try
            {
                // Set the subscription flag to false in preferences.
                Preferences.Set("subscribedToToken", false);

                // Update the database to indicate the user is unsubscribed from push notifications.
                await Database.SetPushNotif(0, Preferences.Get("loginToken", null));
            }
            catch
            {
                // Optionally handle any error during the unsubscription process.
                // No alert is shown here in case of an error.
            }
        }
    }

