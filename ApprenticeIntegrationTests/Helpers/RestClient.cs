using log4net;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ApprenticeIntegrationTests.Helpers
{
    public class RestClient
    {
        private readonly ILog log = LogManager.GetLogger(typeof(RestClient));
        public string EndPoint { get; set; }
        public string Resource { get; set; }
        public Verb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Params { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public WebHeaderCollection ResponseHeaders { get; set; }
        public string Response { get; set; }
        public string Error { get; set; }

        public RestClient(string endpoint, string resource, Verb method, string contentType, string postData, Dictionary<string, string> headers, Dictionary<string, string> parameters)
        {
            EndPoint = endpoint;
            Resource = resource;
            Method = method;
            ContentType = contentType;
            PostData = postData;
            Headers = headers;
            Params = parameters;
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

        public string Execute(bool allowAutoRedirect = true)
        {
            string stringResponse = null;
            try
            {
                if (!Headers.ContainsKey("X-Forwarded-For"))
                {
                    Headers.Add("X-Forwarded-For", "127.0.0.1");
                }
                var webRequest = RestHelper.CreateRequest(EndPoint, Resource, Params, Method, Headers, ContentType, PostData, allowAutoRedirect);

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

        public T Execute<T>(object postObject, JsonConverter[] converters, bool allowAutoRedirect = true)
        {
            try
            {
                PostData = postObject == null ? null : JsonConvert.SerializeObject(postObject, converters);

                Response = Execute(allowAutoRedirect);

                var objectResponse = JsonConvert.DeserializeObject<T>(Response, converters);

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
