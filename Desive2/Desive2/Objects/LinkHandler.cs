using Android.Preferences;
using Xamarin.Essentials;

namespace Desive2.Objects
{
    namespace Desive2.Objects
    {
        /// <summary>
        /// A static class to handle all URL-related configurations for the application.
        /// </summary>
        public static class LinkHandler
        {
            private static bool testing = true;
            private static int devMode = Preferences.Get("devMode", 0);

            /// <summary>
            /// Gets or sets the development mode. 
            /// Used to determine which API endpoints to use.
            /// </summary>
            public static int DevMode
            {
                get { return devMode; }
                set { devMode = value; }
            }

            /// <summary>
            /// Gets the FAQ URL.
            /// </summary>
            public static string FAQ
            {
                get
                {
                    return "https://desive2.org/index.php/faq/";
                }
            }

            /// <summary>
            /// Gets the Contact URL.
            /// </summary>
            public static string Contact
            {
                get
                {
                    return "https://desive2.org/index.php/kontakt/";
                }
            }

            /// <summary>
            /// Gets the API endpoint URL based on the current development mode and testing status.
            /// </summary>
            public static string API
            {
                get
                {
                    if (testing)
                    {
                        if (DevMode == 2)
                            return "https://api-dev2.desive2.org/DBConnect.php";
                        else if (DevMode == 1)
                            return "https://api-dev1.desive2.org/DBConnect.php";
                        else
                            return "https://api-testing.desive2.org/DBConnect.php";
                    }
                    else
                    {
                        return "https://api.desive2.org/DBConnect.php";
                    }
                }
            }

            /// <summary>
            /// Gets the URL for resetting passwords based on the current development mode and testing status.
            /// </summary>
            public static string ResetPassword
            {
                get
                {
                    if (testing)
                    {
                        if (DevMode == 2)
                            return "https://api-dev2.desive2.org/resetPassword.php/";
                        else if (DevMode == 1)
                            return "https://api-dev1.desive2.org/resetPassword.php/";
                        else
                            return "https://api-testing.desive2.org/resetPassword.php/";
                    }
                    else
                    {
                        return "https://api.desive2.org/resetPassword.php/";
                    }
                }
            }

            /// <summary>
            /// Gets the registration URL based on the current development mode and testing status.
            /// </summary>
            public static string Register
            {
                get
                {
                    if (testing)
                    {
                        if (DevMode == 2)
                            return "https://api-dev2.desive2.org/register.php";
                        else if (DevMode == 1)
                            return "https://api-dev1.desive2.org/register.php";
                        else
                            return "https://api-testing.desive2.org/register.php";
                    }
                    else
                    {
                        return "https://api.desive2.org/register.php";
                    }
                }
            }

            /// <summary>
            /// Gets the homepage URL for Desive2.
            /// </summary>
            public static string HomePage
            {
                get
                {
                    return "https://desive2.org";
                }
            }

            /// <summary>
            /// Gets the Terms of Service URL.
            /// </summary>
            public static string ToS
            {
                get
                {
                    return "https://desive2.org/index.php/datenschutz/";
                }
            }
        }
    }

}
