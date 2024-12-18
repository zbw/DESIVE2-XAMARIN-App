using Desive2.Objects;
using Desive2.Services;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.Models
{
    public class Database
    {
        //API link provided by the Linkhandler
        static string API = LinkHandler.API;

        // Checks the validity of a user by email and token.
        // Returns 200 if the user is valid, -2 if unauthorized, and -1 for connection or other errors.
        public static async Task<int> CheckUser(string mail, string token)
        {
            // Ensure there is a network connection before proceeding.
            if (!await CheckConnection())
            {
                return -1; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "mail", mail },
        { "token", token }
    };

            // Make an asynchronous API call to validate the user.
            Response responseString = await AsyncApiCall("validUser", postData);

            // Handle response statuses.
            if (responseString.Status == 200)
            {
                return responseString.Status; // Valid user.
            }
            else if (responseString.Status == 401)
            {
                ValidationError(); // Handle unauthorized access.
                return -2; // Unauthorized.
            }
            else
            {
                return -1; // Other errors.
            }
        }

        /// <summary>
        /// Authenticates a user by email and password via an API call.
        /// </summary>
        /// <param name="mail">The email address of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>Returns a Response object if successful, or null if there is no connection.</returns>
        public static async Task<Response> Login(string mail, string password)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return null; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
            {
                { "mail", mail },
                { "password", password }
            };

            // Perform the login API call and return the response.
            Response responseString = await AsyncApiCall("login", postData);
            return responseString;
        }


        /// <summary>
        /// Sets a new password for the user using the provided password and token.
        /// </summary>
        /// <param name="password">The new password to set for the user.</param>
        /// <param name="token">The authentication token for the user.</param>
        /// <returns>
        /// Returns 1 if the password was successfully set (status 200), 
        /// -2 if the user is unauthorized (status 401), 
        /// or -1 for other errors, including connection issues.
        /// </returns>
        public static async Task<int> SetPassword(string password, string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return -1; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
            {
                { "password", password },
                { "token", token }
            };

            // Perform the set password API call and handle the response.
            Response responseString = await AsyncApiCall("setPassword", postData);

            // Handle different status codes from the response.
            if (responseString.Status == 200)
            {
                return 1; // Password set successfully.
            }
            else if (responseString.Status == 401)
            {
                ValidationError(); // Handle unauthorized access.
                return -2; // Unauthorized.
            }
            else
            {
                return -1; // Other errors.
            }
        }

        /// <summary>
        /// Checks if the user is able to receive push notifications based on the provided token.
        /// </summary>
        /// <param name="token">The authentication token for the user.</param>
        /// <returns>
        /// Returns true if the user is able to receive push notifications (status 200), 
        /// false if there is no connection or for other errors (including status 401).
        /// </returns>
        public static async Task<bool> ReceivesPush(string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return false; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "token", token }
    };

            // Perform the API call to check push notification status.
            Response responseString = await AsyncApiCall("getPushNotificationStatus", postData);

            // Handle response based on status codes.
            if (responseString.Status == 200)
            {
                return responseString.Body.ReceivesPush; // Return the push notification status.
            }
            else if (responseString.Status == 401)
            {
                ValidationError(); // Handle unauthorized access.
            }

            return false; // Default to false for other errors.
        }

        /// <summary>
        /// Requests a password reset for the user with the provided email address.
        /// </summary>
        /// <param name="mail">The email address of the user requesting a password reset.</param>
        /// <returns>
        /// Returns true if the request for a password reset was successful (status 200), 
        /// or false if there is no connection or for other errors.
        /// </returns>
        public static async Task<bool> RequestPassword(string mail)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return false; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "mail", mail }
    };

            // Perform the API call to request a password reset.
            Response responseString = await AsyncApiCall("requestPassword", postData);

            // Return the result based on the response status.
            if (responseString.Status == 200)
            {
                return true; // Password reset request was successful.
            }

            return false; // Return false for other errors.
        }

        /// <summary>
        /// Checks if a survey is open based on the provided token.
        /// </summary>
        /// <param name="token">The authentication token for the user.</param>
        /// <returns>
        /// Returns a SurveyState object if the survey is open (status 200), 
        /// or null if there is no connection, the survey is closed, or for other errors (status 401 or 400).
        /// </returns>
        public static async Task<SurveyState> IsSuveyOpen(string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return null; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
            {
                { "token", token }
            };

            // Perform the API call to check if the survey is open.
            Response responseString = await AsyncApiCall("getOpenSurveys", postData);

            // Return the survey state if the request was successful.
            if (responseString.Status == 200)
                return responseString.Body.Survey;

            // Handle unauthorized or bad request errors.
            else if (responseString.Status == 401 || responseString.Status == 400)
                ValidationError();

            return null; // Return null for other errors.
        }

        /// <summary>
        /// Uploads a picture using the provided picture data and authentication token.
        /// </summary>
        /// <param name="picture">The picture data to be uploaded.</param>
        /// <param name="token">The authentication token for the user.</param>
        /// <returns>
        /// Returns the ID of the uploaded picture if the upload was successful (status 200), 
        /// -2 if the user is unauthorized (status 401), 
        /// or -1 if there is no connection or other errors.
        /// </returns>
        public static async Task<int> UploadPicture(string picture, string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return -1; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "picture", picture },
        { "token", token }
    };

            // Perform the API call to upload the picture.
            Response response = await AsyncApiCall("uploadPicture", postData);

            // Handle unauthorized access.
            if (response.Status == 401)
            {
                ValidationError(); // Handle the validation error.
                return -2; // Unauthorized.
            }

            // Return the ID of the uploaded picture.
            return response.Body.IdUpload;
        }


        /// <summary>
        /// Retrieves the CleverPush channel ID associated with the provided token.
        /// </summary>
        /// <param name="token">The authentication token for the user.</param>
        /// <returns>
        /// Returns the CleverPush channel ID as a string if the request is successful (status 200), 
        /// "-2" if the user is unauthorized or the request is invalid (status 401 or 400), 
        /// or "-1" if there is no connection or other errors.
        /// </returns>
        public static async Task<string> GetCleverpushChannelId(string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return "-1"; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "token", token }
    };

            // Perform the API call to get the CleverPush channel ID.
            Response response = await AsyncApiCall("getChannelId", postData);

            // Handle unauthorized or bad request errors.
            if (response.Status == 401 || response.Status == 400)
            {
                ValidationError(); // Handle validation error.
                return "-2"; // Unauthorized or invalid request.
            }

            // Return the CleverPush channel ID as a string.
            return response.Body.CleverPushChannelId.ToString();
        }


        /// <summary>
        /// Uploads a PDF file to the server for the specified user.
        /// </summary>
        /// <param name="user">The user ID for whom the PDF is being uploaded.</param>
        /// <param name="fileName">The path to the PDF file to be uploaded.</param>
        /// <param name="token">The authentication token for the user.</param>
        /// <returns>
        /// Returns the ID of the uploaded file if the upload is successful (status 200), 
        /// -2 if the upload fails or the user is unauthorized (status 401), 
        /// or -1 if there is no connection or other errors.
        /// </returns>
        public static async Task<int> UploadPDF(string user, string fileName, string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return -1; // No connection.
            }

            // Prepare the request URL and parameters.
            string requestURL = API + "?action=uploadPDF";
            WebClient wc = new WebClient();
            byte[] bytes = File.ReadAllBytes(fileName); // Read the file as bytes.

            Dictionary<string, object> postParameters = new Dictionary<string, object>
        {
            { "idAppUser", user },
            { "token", token }
        };

            // Add the file parameter to the POST data.
            var fileParameter = new FormUpload.FileParameter(bytes, Path.GetFileName(fileName), "application/pdf");
            postParameters.Add("pdf", fileParameter);

            // Perform the multipart form upload request.
            HttpWebResponse webResponse = await FormUpload.MultipartFormPost(requestURL, postParameters);

            // Process the response from the server.
            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            var returnResponseText = responseReader.ReadToEnd();
            webResponse.Close();

            // Deserialize the response into a Response object.
            Response res = JsonConvert.DeserializeObject<Response>(returnResponseText);

            // Return the result based on the response status.
            if (res.Status == 200)
            {
                return res.Body.IdUpload; // Return the ID of the uploaded file.
            }
            else if (res.Status == 401)
            {
                ValidationError(); // Handle unauthorized access.
            }

            return -2; // Return -2 for failed upload or other errors.
        }


        /// <summary>
        /// Uploads a new diary entry for the specified user.
        /// </summary>
        /// <param name="entry">The diary entry text to be uploaded.</param>
        /// <param name="idUpload">The ID of the previous uploaded entry (if any).</param>
        /// <param name="token">The authentication token for the user.</param>
        /// <returns>
        /// Returns true if the diary entry was uploaded successfully (status 200), 
        /// or false if there is no connection, the user is unauthorized (status 401), 
        /// or for other errors.
        /// </returns>
        public static async Task<bool> UploadMoreDiary(string entry, string idUpload, string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return false; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "entry", entry },
        { "idUpload", idUpload },
        { "token", token }
    };

            // Perform the API call to upload the diary entry.
            Response responseString = await AsyncApiCall("uploadDiaryEntry", postData);

            // Return true if the upload was successful (status 200).
            if (responseString.Status == 200)
            {
                return true;
            }
            // Handle unauthorized access.
            else if (responseString.Status == 401)
            {
                ValidationError(); // Handle validation error.
                return false; // Unauthorized.
            }
            // Return false for other errors.
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Uploads a registration ID for push notifications.
        /// </summary>
        /// <param name="registration">The registration ID to be uploaded.</param>
        /// <param name="token">The authentication token for the user.</param>
        /// <returns>
        /// Returns true if the registration ID was uploaded successfully (status 200), 
        /// or false if there is no connection, the user is unauthorized (status 401), 
        /// or for other errors.
        /// </returns>
        public static async Task<bool> UploadRegristrationId(string registration, string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return false; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "registrationID", registration },
        { "token", token }
    };

            // Perform the API call to upload the registration ID.
            Response responseString = await AsyncApiCall("uploadRegistrationID", postData);

            // Return true if the upload was successful (status 200).
            if (responseString.Status == 200)
            {
                return true;
            }
            // Handle unauthorized access.
            else if (responseString.Status == 401)
            {
                ValidationError(); // Handle validation error.
                return false; // Unauthorized.
            }
            // Return false for other errors.
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Sets the push notification preference for the user.
        /// </summary>
        /// <param name="push">The push notification preference (e.g., 1 for enabled, 0 for disabled).</param>
        /// <param name="token">The authentication token for the user.</param>
        /// <returns>
        /// Returns true if the push notification preference was successfully updated (status 200), 
        /// or false if there is no connection, the user is unauthorized (status 401), 
        /// or for other errors.
        /// </returns>
        public static async Task<bool> SetPushNotif(int push, string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return false; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "push", push },
        { "token", token }
    };

            // Perform the API call to set the push notification preference.
            Response res = await AsyncApiCall("setPushNotification", postData);

            // Return true if the operation was successful (status 200).
            if (res.Status == 200)
            {
                return true;
            }
            // Handle unauthorized access.
            else if (res.Status == 401)
            {
                ValidationError(); // Handle validation error.
                return false; // Unauthorized.
            }
            // Return false for other errors.
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Retrieves the IBAN (International Bank Account Number) associated with the user's account.
        /// </summary>
        /// <param name="token">The authentication token for the user.</param>
        /// <returns>
        /// Returns the IBAN if the request is successful (status 200), 
        /// "-1" if there is no connection, 
        /// or "-2" if the user is unauthorized (status 401) or for other errors.
        /// </returns>
        internal static async Task<string> GetIban(string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return "-1"; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "token", token }
    };

            // Perform the API call to retrieve the IBAN.
            Response response = await AsyncApiCall("GetIBAN", postData);

            // Return the IBAN if the operation was successful (status 200).
            if (response.Status == 200)
            {
                return response.Body.Iban;
            }
            // Handle unauthorized access.
            else if (response.Status == 401)
            {
                ValidationError(); // Handle validation error.
            }

            return "-2"; // Return "-2" for other errors.
        }


        /// <summary>
        /// Uploads the user's answers to a survey.
        /// </summary>
        /// <param name="user">The user ID of the person submitting the answers.</param>
        /// <param name="buttons">The answers selected by the user (could be a button or other input data).</param>
        /// <param name="surveyID">The ID of the survey the user is responding to.</param>
        /// <param name="token">The authentication token for the user.</param>
        /// <returns>
        /// Returns true if the survey answers were successfully uploaded (status 200), 
        /// or false if there is no connection, the user is unauthorized (status 401), 
        /// or for other errors such as server issues (status 500) or access issues (status 400).
        /// </returns>
        public static async Task<bool> UploadSurveyAnswers(string user, string buttons, string surveyID, string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return false; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "idAppUser", user },
        { "surveyID", surveyID },
        { "answers", buttons },
        { "token", token }
    };

            // Perform the API call to upload the survey answers.
            Response responseString = await AsyncApiCall("uploadSurveyAnswer", postData);

            // Return true if the operation was successful (status 200).
            if (responseString.Status == 200)
            {
                return true;
            }
            // Handle unauthorized access.
            else if (responseString.Status == 401)
            {
                ValidationError(); // Handle validation error.
                return false; // Unauthorized.
            }
            // Handle server error (status 500).
            else if (responseString.Status == 500)
            {
                await App.Current.MainPage.DisplayAlert("Fehler", "Auf dem Server ist ein Fehler aufgetreten. Bitte versuchen Sie es erneut. Falls es weiterhin nicht funktioniert, beenden sie bitte die App.", "Okay");
                return false;
            }
            // Handle survey not available (status 400).
            else if (responseString.Status == 400)
            {
                await App.Current.MainPage.DisplayAlert("Fehler", "Die Umfrage ist für Sie zum jetzigen Zeitpunkt nicht freigeschaltet.", "Okay");
                return false;
            }

            return false; // Return false for other errors.
        }


        /// <summary>
        /// Modifies an existing diary entry by updating the entry content and related additional information.
        /// </summary>
        /// <param name="idDiaryEntry">The ID of the diary entry to modify.</param>
        /// <param name="idAdditionalInformation">The ID of additional information related to the diary entry (can be 0 if no additional information exists).</param>
        /// <param name="token">The authentication token for the user making the modification.</param>
        /// <param name="entry">The new content for the diary entry.</param>
        /// <returns>
        /// Returns the response containing the status of the operation if successful (status 200),
        /// or null if there is no connection, the user is unauthorized (status 401), or if an error occurs.
        /// </returns>
        public static async Task<Response> ModifyDiaryEntry(int idDiaryEntry, int idAdditionalInformation, string token, string entry)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return null; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "idDiaryEntry", idDiaryEntry },
        { "token", token },
        { "entry", entry }
    };

            // Add additional information ID if it's provided (non-zero).
            if (idAdditionalInformation == 0)
            {
                postData.Add("idAdditionalInformation", "");
            }
            else
            {
                postData.Add("idAdditionalInformation", idAdditionalInformation);
            }

            // Perform the API call to modify the diary entry.
            Response response = await AsyncApiCall("modifyDiaryEntry", postData);

            // Return the response if the operation was successful (status 200).
            if (response.Status == 200)
            {
                return response;
            }
            // Handle unauthorized access.
            else if (response.Status == 401)
            {
                ValidationError(); // Handle validation error.
            }

            return null; // Return null for other errors or failure.
        }

        /// <summary>
        /// Uploads a new diary entry for a specific user.
        /// </summary>
        /// <param name="entry">The content of the diary entry to be uploaded.</param>
        /// <param name="token">The authentication token of the user submitting the diary entry.</param>
        /// <param name="idUpload">The ID of the upload associated with the entry (for tracking purposes).</param>
        /// <returns>
        /// Returns the ID of the uploaded entry if successful (status 200), 
        /// or -1 if there is no connection, the user is unauthorized (status 401), 
        /// or if any other error occurs.
        /// </returns>
        public static async Task<int> UploadDiaryEntry(string entry, string token, int idUpload)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return -1; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "entry", entry },
        { "token", token },
        { "idUpload", idUpload }
    };

            // Perform the API call to upload the diary entry.
            Response responseString = await AsyncApiCall("uploadDiaryEntry", postData);

            // Return the ID of the uploaded entry if the operation was successful (status 200).
            if (responseString.Status == 200)
            {
                return responseString.Body.IdUpload;
            }
            // Handle unauthorized access.
            else if (responseString.Status == 401)
            {
                ValidationError(); // Handle validation error.
                return -1; // Unauthorized.
            }

            return -1; // Return -1 for other errors or failure.
        }


        /// <summary>
        /// Uploads a user's IBAN and associated name for processing.
        /// </summary>
        /// <param name="name">The name associated with the IBAN to be uploaded.</param>
        /// <param name="iban">The IBAN to be uploaded.</param>
        /// <param name="token">The authentication token of the user submitting the IBAN.</param>
        /// <returns>
        /// Returns true if the upload is successful (status 200), 
        /// false if there is no connection, the user is unauthorized (status 401), 
        /// or if any other error occurs.
        /// </returns>
        public static async Task<bool> UploadIban(string name, string iban, string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return false; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "name", name },
        { "iban", iban },
        { "token", token }
    };

            // Perform the API call to upload the IBAN and name.
            Response responseString = await AsyncApiCall("uploadIBANandName", postData);

            // Return true if the operation was successful (status 200).
            if (responseString.Status == 200)
            {
                return true;
            }
            // Handle unauthorized access.
            else if (responseString.Status == 401)
            {
                ValidationError(); // Handle validation error.
                return false; // Unauthorized.
            }

            return false; // Return false for other errors or failure.
        }

        /// <summary>
        /// Retrieves the number of entries associated with the provided token.
        /// </summary>
        /// <param name="token">The authentication token of the user whose number of entries is to be fetched.</param>
        /// <returns>
        /// Returns the number of entries if the operation is successful (status 200), 
        /// or -1 if there is no connection, the user is unauthorized (status 401), 
        /// or if any other error occurs.
        /// </returns>
        public static async Task<int> GetNumOfEntries(string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return -1; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "token", token }
    };

            // Perform the API call to get the number of entries.
            Response responseString = await AsyncApiCall("getNumOfEntries", postData);

            // Return the number of entries if the operation was successful (status 200).
            if (responseString.Status == 200)
            {
                return responseString.Body.NumOfEntries;
            }
            // Handle unauthorized access.
            else if (responseString.Status == 401)
            {
                ValidationError(); // Handle validation error.
            }

            return -1; // Return -1 for other errors or failure.
        }

        /// <summary>
        /// Logs the user out by invalidating their authentication token.
        /// </summary>
        /// <param name="token">The authentication token of the user to be logged out.</param>
        /// <returns>
        /// Returns true if the logout operation was successful (status 200),
        /// false if there is no connection, the user is unauthorized (status 401),
        /// or if any other error occurs.
        /// </returns>
        public static async Task<bool> Logout(string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return false; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "token", token }
    };

            // Perform the API call to log out the user.
            Response responseString = await AsyncApiCall("Logout", postData);

            // Return true if the logout operation was successful (status 200).
            if (responseString.Status == 200)
            {
                return true;
            }
            // Handle unauthorized access.
            else if (responseString.Status == 401)
            {
                ValidationError(); // Handle validation error.
            }

            return false; // Return false for other errors or failure.
        }

        /// <summary>
        /// Retrieves the name associated with the provided authentication token.
        /// </summary>
        /// <param name="token">The authentication token used to retrieve the name.</param>
        /// <returns>
        /// Returns the name of the user if the operation is successful (status 200),
        /// or "-1" if there is no connection, the user is unauthorized (status 401),
        /// or if any other error occurs.
        /// </returns>
        public static async Task<string> GetName(string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return "-1"; // No connection.
            }

            // Prepare the POST data for the API call.
            Dictionary<string, object> postData = new Dictionary<string, object>
    {
        { "token", token }
    };

            // Perform the API call to retrieve the user's name.
            Response responseString = await AsyncApiCall("getName", postData);

            // Return the name if the operation was successful (status 200).
            if (responseString.Status == 200)
                return responseString.Body.Name;

            // Handle unauthorized access.
            else if (responseString.Status == 401)
                ValidationError(); // Handle validation error.

            // Return "-1" for other errors or failure.
            return "-1";
        }

        /// <summary>
        /// Uploads an audio file to the server using the provided file name and token.
        /// </summary>
        /// <param name="fileName">The name of the file to be uploaded.</param>
        /// <param name="token">The authentication token required for the API call.</param>
        /// <returns>
        /// Returns the upload ID if the operation is successful (status 200),
        /// or -1 if the operation fails or the user is unauthorized (status 401).
        /// </returns>
        public static async Task<int> UploadFileEx(string fileName, string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return -1; // No connection.
            }

            // Prepare the request URL for the file upload action.
            string requestURL = API + "?action=uploadAudio";

            // Initialize WebClient for file upload.
            WebClient wc = new WebClient();

            // Read the file bytes from the given file name.
            byte[] bytes = File.ReadAllBytes(fileName);

            // Prepare POST parameters for the request.
            Dictionary<string, object> postParameters = new Dictionary<string, object>
    {
        { "token", token }
    };

            // Create the file parameter for the audio file.
            var fileParameter = new FormUpload.FileParameter(bytes, Path.GetFileName(fileName), "audio/wav");
            postParameters.Add("uploadfile", fileParameter);

            // Perform the multipart form POST request to upload the file.
            HttpWebResponse webResponse = await FormUpload.MultipartFormPost(requestURL, postParameters);

            // Read and process the response from the server.
            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            var returnResponseText = responseReader.ReadToEnd();
            webResponse.Close();

            // Deserialize the response into a Response object.
            Response res = JsonConvert.DeserializeObject<Response>(returnResponseText);

            // Return the upload ID if successful.
            if (res.Status == 200)
            {
                return res.Body.IdUpload;
            }
            // Handle unauthorized access.
            else if (res.Status == 401)
            {
                ValidationError(); // Handle validation error.
            }

            // Return -1 for failure or other errors.
            return -1;
        }

        /// <summary>
        /// Retrieves the Android version from the server.
        /// </summary>
        /// <returns>
        /// Returns the Android version as a string if the operation is successful (status 200),
        /// or "-1" if the operation fails or the version cannot be retrieved.
        /// </returns>
        public static async Task<string> GetAndroidVersion()
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return "-1"; // No connection.
            }

            // Prepare the request data.
            Dictionary<string, object> postData = new Dictionary<string, object>();

            // Perform the API call to get the Android version.
            Response response = await AsyncApiCall("androidVersion", postData);

            // Return the Android version if the response status is 200 (successful).
            if (response.Status == 200)
            {
                return response.Body.Version;
            }

            // Return "-1" if the operation fails.
            return "-1";
        }

        /// <summary>
        /// Retrieves the Apple version from the server.
        /// </summary>
        /// <returns>
        /// Returns the Apple version as a string if the operation is successful (status 200),
        /// or "-1" if the operation fails or the version cannot be retrieved.
        /// </returns>
        public static async Task<string> GetAppleVersion()
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return "-1"; // No connection.
            }

            // Prepare the request data.
            Dictionary<string, object> postData = new Dictionary<string, object>();

            // Perform the API call to get the Apple version.
            Response response = await AsyncApiCall("appleVersion", postData);

            // Return the Apple version if the response status is 200 (successful).
            if (response.Status == 200)
            {
                return response.Body.Version;
            }

            // Return "-1" if the operation fails.
            return "-1";
        }


        /// <summary>
        /// Deletes a user account based on the provided token.
        /// </summary>
        /// <param name="token">The token of the user whose account is to be deleted.</param>
        /// <returns>
        /// Returns true if the account is successfully deleted (status 200),
        /// otherwise returns false.
        /// </returns>
        public static async Task<bool> DeleteAccount(string token)
        {
            // Check for network connectivity.
            if (!await CheckConnection())
            {
                return false; // No connection.
            }

            // Prepare the request data with the user's token.
            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("token", token);

            // Perform the API call to delete the user account.
            Response response = await AsyncApiCall("deleteUser", postData);

            // Return true if the account is deleted successfully.
            if (response.Status == 200)
            {
                return true;
            }

            // Return false if the operation fails.
            return false;
        }

        /// <summary>
        /// Makes an asynchronous API call to the specified action with the provided data.
        /// </summary>
        /// <param name="action">The action to be performed in the API call.</param>
        /// <param name="postData">A dictionary containing the data to be sent with the request.</param>
        /// <returns>
        /// A <see cref="Response"/> object containing the response data from the API call.
        /// </returns>
        public static async Task<Response> AsyncApiCall(string action, Dictionary<string, object> postData)
        {
            // Build the request URL with the action parameter.
            string requestString = API + "?action=" + action;

            // Send the POST request and get the response.
            HttpWebResponse webResponse = await FormUpload.MultipartFormPost(requestString, postData);

            // Read and process the response from the API.
            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            var returnResponseText = responseReader.ReadToEnd();
            webResponse.Close();

            // Deserialize the response into a Response object.
            Response res = JsonConvert.DeserializeObject<Response>(returnResponseText);

            // Return the Response object.
            return res;
        }


        /// <summary>
        /// Handles validation errors by clearing stored user data, resetting relevant properties, and navigating to the login page.
        /// </summary>
        private static void ValidationError()
        {
            // Clear stored user data.
            Preferences.Set("loginToken", null);
            Preferences.Set("idUser", null);

            // Reset survey content and file-related data.
            SurveyContent.Reset();
            FilePathHandler.IdDiaryEntry = 0;
            FilePathHandler.IdUpload = 0;

            // Clear the previous page navigation stack.
            Navigator.PreviousPage = new Stack<Previous>();

            // Display an alert to the user informing them of the session expiration.
            App.Current.MainPage.DisplayAlert("Ihre Session ist abgelaufen!",
                "Eventuell haben Sie sich mit einem anderen Gerät angemeldet. Sie wurden zu Ihrer Sicherheit abgemeldet.",
                "Ok");

            // Navigate to the login page.
            Shell.Current.GoToAsync("//LoginPage");
        }

        /// <summary>
        /// Changes the API endpoint based on the specified mode (testing or production).
        /// Updates the API connection and stores the mode in preferences.
        /// </summary>
        /// <param name="testing">A boolean indicating whether to use the testing environment (true) or production (false).</param>
        public static void ChangeAPI(bool testing)
        {
            if (testing)
            {
                // Set the development mode to testing (1) and update the API URL.
                Preferences.Set("devMode", 1);
                LinkHandler.DevMode = 1;
                API = LinkHandler.API;
            }
            else
            {
                // Set the development mode to production (2) and update the API URL.
                Preferences.Set("devMode", 2);
                LinkHandler.DevMode = 2;
                API = LinkHandler.API;
            }

            // Display an alert showing the new API connection.
            App.Current.MainPage.DisplayAlert("New Connection", API, "OK");

            // Handle validation error (reset session, navigate to login page).
            ValidationError();
        }

        /// <summary>
        /// Checks if the device is currently connected to the internet.
        /// Displays an alert if there is no internet connection.
        /// </summary>
        /// <returns>A boolean indicating whether the device is connected to the internet (true) or not (false).</returns>
        private static async Task<bool> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                // Display an alert if there is no internet connection.
                await App.Current.MainPage.DisplayAlert("Fehler", "Bitte überpüfen Sie Ihre Internetverbindung.", "Okay");
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
