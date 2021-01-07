using System.Collections.Generic;
using System.Net;
using System;
using System.IO;
using System.Text;
//Discord Webhook Script for Unity
//Morgan Skillicorn @MorganSkilly
//https://morgan.games/

public class DiscordWebhook
{
    //CHANGE THESE!
    private static string defaultWebhook = ""; // ссылка на дискорд вебхук
    private static string defaultUserAgent = "example"; // ник дискорд бота (ТОЛЬКО АНГЛИЙСКИЙ ЯЗЫК)
    private static string defaultUserName = "example"; // ник дискорд бота (ТОЛЬКО АНГЛИЙСКИЙ ЯЗЫК)
    private static string defaultAvatar = ""; // ссылка на аватарку

    public static void SendTestWebhook()
    {
        Send("Test message recieved successfully! :raised_hands:");
    }

    #region simple message
    //Send a simple message

    public static string Send(string mssgBody)
    {
        // Generate post objects
        Dictionary<string, object> postParameters = new Dictionary<string, object>();
        postParameters.Add("username", defaultUserName);
        postParameters.Add("content", mssgBody);
        postParameters.Add("avatar_url", defaultAvatar);

        // Create request and receive response
        HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(defaultWebhook, defaultUserAgent, postParameters);

        // Process response
        StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
        string fullResponse = responseReader.ReadToEnd();
        webResponse.Close();

        //return string with response
        return fullResponse;
    }

    public static string Send(string mssgBody, string userName)
    {
        // Generate post objects
        Dictionary<string, object> postParameters = new Dictionary<string, object>();
        postParameters.Add("username", userName);
        postParameters.Add("content", mssgBody);
        postParameters.Add("avatar_url", defaultAvatar);

        // Create request and receive response
        HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(defaultWebhook, defaultUserAgent, postParameters);

        // Process response
        StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
        string fullResponse = responseReader.ReadToEnd();
        webResponse.Close();

        //return string with response
        return fullResponse;
    }

    public static string Send(string mssgBody, string userName, string webhook)
    {
        // Generate post objects
        Dictionary<string, object> postParameters = new Dictionary<string, object>();
        postParameters.Add("username", userName);
        postParameters.Add("content", mssgBody);
        postParameters.Add("avatar_url", defaultAvatar);

        // Create request and receive response
        HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(webhook, defaultUserAgent, postParameters);

        // Process response
        StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
        string fullResponse = responseReader.ReadToEnd();
        webResponse.Close();

        //return string with response
        return fullResponse;
    }

    #endregion

    #region send file
    //Send a simple message with an embedded file

    public static string SendFile(
        string mssgBody,
        string filename,
        string fileformat,
        string filepath,
        string application)
    {
        // Read file data
        FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();

        // Generate post objects
        Dictionary<string, object> postParameters = new Dictionary<string, object>();
        postParameters.Add("filename", filename);
        postParameters.Add("fileformat", fileformat);
        postParameters.Add("file", new FormUpload.FileParameter(data, filename, application/*"application/msexcel"*/));

        postParameters.Add("username", defaultUserName);
        postParameters.Add("content", mssgBody);

        // Create request and receive response
        HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(defaultWebhook, defaultUserAgent, postParameters);

        // Process response
        StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
        string fullResponse = responseReader.ReadToEnd();
        webResponse.Close();

        //return string with response
        return fullResponse;
    }

    public static string SendFile(
        string mssgBody,
        string filename,
        string fileformat,
        string filepath,
        string application,
        string userName)
    {
        // Read file data
        FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();

        // Generate post objects
        Dictionary<string, object> postParameters = new Dictionary<string, object>();
        postParameters.Add("filename", filename);
        postParameters.Add("fileformat", fileformat);
        postParameters.Add("file", new FormUpload.FileParameter(data, filename, application/*"application/msexcel"*/));

        postParameters.Add("username", userName);
        postParameters.Add("content", mssgBody);

        // Create request and receive response
        HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(defaultWebhook, defaultUserAgent, postParameters);

        // Process response
        StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
        string fullResponse = responseReader.ReadToEnd();
        webResponse.Close();

        //return string with response
        return fullResponse;
    }

    public static string SendFile(
        string mssgBody,
        string filename,
        string fileformat,
        string filepath,
        string application,
        string userName,
        string webhook)
    {
        // Read file data
        FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();

        // Generate post objects
        Dictionary<string, object> postParameters = new Dictionary<string, object>();
        postParameters.Add("filename", filename);
        postParameters.Add("fileformat", fileformat);
        postParameters.Add("file", new FormUpload.FileParameter(data, filename, application/*"application/msexcel"*/));

        postParameters.Add("username", userName);
        postParameters.Add("content", mssgBody);

        // Create request and receive response
        HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(webhook, defaultUserAgent, postParameters);

        // Process response
        StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
        string fullResponse = responseReader.ReadToEnd();
        webResponse.Close();

        //return string with response
        return fullResponse;
    }

    #endregion

    public static class FormUpload //formats data as a multi part form to allow for file sharing
    {
        private static readonly Encoding encoding = Encoding.UTF8;
        public static HttpWebResponse MultipartFormDataPost(string postUrl, string userAgent, Dictionary<string, object> postParameters)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());

            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            return PostForm(postUrl, userAgent, contentType, formData);
        }

        private static HttpWebResponse PostForm(string postUrl, string userAgent, string contentType, byte[] formData)
        {
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            // Set up the request properties.
            request.Method = "POST";
            request.ContentType = contentType;
            request.UserAgent = userAgent;
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;

            // Send the form data to the request.
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            return request.GetResponse() as HttpWebResponse;
        }

        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                if (needsCLRF)
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is FileParameter)
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

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