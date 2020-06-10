using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace ApprenticeIntegrationTests.Helpers
{
    public class RestClient
    {
        private readonly ILog log = LogManager.GetLogger(typeof(RestClient));
        public string EndPoint { get; set; }
        public string Resource { get; set; }
        public HttpVerbs Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Params { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public WebHeaderCollection ResponseHeaders { get; set; }
        public string Response { get; set; }
        public string Error { get; set; }
        public const string REST_URI = "https://localhost:44322/api";
        public const string CONTEST_TYPE_JSON = "application/json";

        public RestClient(string resource, 
            HttpVerbs method, 
            string endpoint = REST_URI, 
            string contentType = CONTEST_TYPE_JSON, 
            string postData = null, 
            Dictionary<string, string> headers = null, 
            Dictionary<string, string> parameters = null)
        {
            EndPoint = endpoint;
            Resource = resource;
            Method = method;
            ContentType = contentType;
            PostData = postData;
            Headers = headers;
            Params = parameters;

            if (Headers == null)
            {
                Headers = new Dictionary<string, string>();
            }
        }

        public void AddHeader(string key, string value)
        {
            if (Headers == null)
                Headers = new Dictionary<string, string>();
            Headers.Add(key, value);
        }

        public void AddParameter(string key, string value)
        {
            if (Params == null)
                Params = new Dictionary<string, string>();
            Params.Add(key, value);
        }

        private string Execute()
        {
            string stringResponse = null;
            try
            {
                if (!Headers.ContainsKey("X-Forwarded-For"))
                {
                    Headers.Add("X-Forwarded-For", "127.0.0.1");
                }
                var webRequest = RestHelper.CreateRequest(EndPoint, Resource, Params, Method, Headers, ContentType, PostData);

                Assert.IsNotNull(webRequest);

                stringResponse = RestHelper.GetResponse(webRequest);

                ResponseStatusCode = RestHelper.ResponseStatusCode;
                ResponseHeaders = RestHelper.ResponseHeaders;
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
            }
            return stringResponse;
        }

        public T Execute<T>()
        {
            try
            {
                Response = Execute();

                var objectResponse = JsonConvert.DeserializeObject<T>(Response);

                return objectResponse;
            }
            catch (Exception e)
            {
                Error = e.Message;
                log.Error(e.Message, e);
            }

            return default(T);
        }

        public string GetResponseHeaderValue(string key)
        {
            return ResponseHeaders.HasKeys() ? ResponseHeaders[key] : null;
        }

        public string ResponseLocation()
        {
            var locationFull = ResponseHeaders?[HttpResponseHeader.Location];
            return locationFull?.Substring(locationFull.LastIndexOf('/') + 1);
        }
    }
}
