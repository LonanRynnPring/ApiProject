using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ApprenticeIntegrationTests.Helpers
{
    public enum Verb
    {
        POST,
        GET,
        PUT,
        PATCH,
        DELETE
    }

    public class RestHelper
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RestHelper));

        public static HttpStatusCode ResponseStatusCode { get; set; }
        public static WebHeaderCollection ResponseHeaders { get; set; }

        #region publicMethods
        public static WebRequest CreateRequest(string endPoint, String resource, Dictionary<string, string> parameters, Verb method, Dictionary<string, string> headers, string contentType, string data, bool allowAutoRedirect = true)
        {
            WebRequest request = null;
            try
            {
                var requestUriString = String.Concat(endPoint, resource, GetParametersString(parameters));
                log.Debug("Request URI string: " + requestUriString);
                request = WebRequest.Create(requestUriString);

                ((HttpWebRequest)request).AllowAutoRedirect = allowAutoRedirect;

                request.Method = method.ToString();
                log.Debug("REST verb: " + request.Method);

                request.ContentType = contentType;
                log.Debug("REST content type: " + request.ContentType);

                //handle headers
                if (headers != null)
                {
                    log.Debug("REST headers: ");
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                        log.Debug(String.Format("Added header key [{0}] with value [{1}]", header.Key, header.Value));
                    }
                }

                if (data != null)
                {
                    var encoding = new UTF8Encoding();
                    var bytes = encoding.GetBytes(data);

                    request.ContentLength = bytes.Length;
                    log.Debug("REST content length: " + request.ContentLength);
                    log.Debug("REST post data: " + data);
                    using (var requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(bytes, 0, bytes.Length);
                    }
                }
                else
                {
                    request.ContentLength = 0;
                    log.Debug("REST content length: 0");
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
            }

            return request;
        }

        private static string GetParametersString(Dictionary<string, string> parameters)
        {
            //handle parameters
            if (parameters != null && parameters.Count > 0)
            {
                log.Debug("REST parameters: ");
                var query = new StringBuilder();
                foreach (var param in parameters)
                {
                    log.Debug(String.Format("Added parameter key [{0}] with value [{1}]", param.Key, param.Value));
                    query.Append("&");
                    query.Append(param.Key);
                    query.Append("=");
                    query.Append(param.Value);
                }
                //remove the first added &
                query.Remove(0, 1);
                //insert ? at the start of the string
                query.Insert(0, "?", 1);

                log.Debug("Computed parameters string :" + query);
                return query.ToString();
            }
            else
                return String.Empty;
        }

        public static string GetResponse(WebRequest request)
        {
            string responseString = null;
            try
            {
                WebResponse webResponse = null;
                try
                {
                    webResponse = request.GetResponse();

                }
                catch (WebException e)
                {
                    //return the response string as it is
                    webResponse = e.Response;
                    log.Error(e.Message, e);

                    switch (e.Status)
                    {
                        case WebExceptionStatus.Timeout:
                            ResponseStatusCode = HttpStatusCode.RequestTimeout;
                            break;
                    }
                }

                using (var reponseStream = webResponse.GetResponseStream())
                {
                    if (reponseStream != null)
                    {
                        using (var reponseStreamReader = new StreamReader(reponseStream))
                        {
                            responseString = reponseStreamReader.ReadToEnd();
                        }
                    }
                }
                var response = ((HttpWebResponse)webResponse);
                ResponseStatusCode = response.StatusCode;
                ResponseHeaders = response.Headers;

            }
            catch (Exception e)
            {
                //avoid failures inside of the helper classes
                //this should be handled by Assert methods inside of the testcase method
                log.Error(e.Message, e);
            }

            return responseString;
        }

        #endregion

        #region privateMethods


        #endregion
    }
}
