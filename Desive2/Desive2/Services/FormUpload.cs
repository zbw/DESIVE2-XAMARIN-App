using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Desive2.Services
{
    /// <summary>
    /// Static class to handle file uploads via multipart form data.
    /// </summary>
    public static class FormUpload
    {
        private static readonly Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// Posts form data to the specified URL using multipart form-data encoding.
        /// </summary>
        /// <param name="postUrl">The URL to send the POST request to.</param>
        /// <param name="postParameters">A dictionary of form parameters, where the key is the field name and the value is the field value.</param>
        /// <returns>Returns an HttpWebResponse containing the server's response.</returns>
        public static async Task<HttpWebResponse> MultipartFormPost(string postUrl, Dictionary<string, object> postParameters)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            var form = await PostForm(postUrl, contentType, formData);
            return form;
        }

        /// <summary>
        /// Sends a POST request with the form data to the specified URL.
        /// </summary>
        /// <param name="postUrl">The URL to send the POST request to.</param>
        /// <param name="contentType">The content type for the request.</param>
        /// <param name="formData">The form data to send in the request body.</param>
        /// <returns>Returns an HttpWebResponse containing the server's response.</returns>
        private static async Task<HttpWebResponse> PostForm(string postUrl, string contentType, byte[] formData)
        {
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            // Set up the request properties.
            request.Method = "POST";
            request.ContentType = contentType;

            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;

            // Send the form data to the request.
            request.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            using (Stream requestStream = await request.GetRequestStreamAsync())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// Builds the multipart form data byte array from the provided parameters.
        /// </summary>
        /// <param name="postParameters">The form parameters, including file data and regular fields.</param>
        /// <param name="boundary">The boundary string to separate parts of the form data.</param>
        /// <returns>Returns a byte array representing the form data.</returns>
        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                if (needsCLRF)
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is FileParameter) // to check if parameter is of file type
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    // Add the header for this file parameter.
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));
                    // Write the file data directly to the Stream.
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    // Add the regular text form data.
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                }
            }

            // Add the end of the request.
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Convert the stream to a byte array and return it.
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

        /// <summary>
        /// Represents a file parameter for multipart form data, including the file content, filename, and content type.
        /// </summary>
        public class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }

            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }
    }

}
