using ApprenticeIntegrationTests.Models.Example;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace ApprenticeIntegrationTests.Helpers
{
    public static class ExampleHelper
    {
        private const string RESOURCE = "/examples";
        public static IList<Response> GetExamples()
        {
            var restClient = new RestClient(RESOURCE, HttpVerbs.Get);
            var result = restClient.Execute<List<Response>>();

            Assert.IsTrue(restClient.ResponseStatusCode == HttpStatusCode.OK || 
                restClient.ResponseStatusCode == HttpStatusCode.NoContent, 
                $"Expected {HttpStatusCode.OK} or {HttpStatusCode.NoContent} but got {restClient.ResponseStatusCode}");

            return result;
        }
    }
}
