using ApprenticeIntegrationTests.Helpers;
using ApprenticeIntegrationTests.Models.Example;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ApprenticeIntegrationTests.Tests.Example
{
    [TestClass]
    public class Create
    {
        private const string RESOURCE = "/examples";
        private readonly string VALUE = "Example Value";

        [TestMethod]
        [Description("Successfully creata an example item")]
        public void ExampleCreated()
        {
            var request = new Request { Value = VALUE };
            var req = JsonConvert.SerializeObject(request);
            var restClient = new RestClient(RESOURCE, HttpVerbs.Post, postData: req);
            var result = restClient.Execute<Response>();

            Assert.AreEqual(HttpStatusCode.Created, restClient.ResponseStatusCode);
            Assert.IsNotNull(result);
            Assert.AreEqual(VALUE, result.Value);
            Assert.IsNotNull(result.ExampleId);

            var examples = ExampleHelper.GetExamples();
            Assert.IsTrue(examples.Any());
            Assert.IsNotNull(examples.SingleOrDefault(e => e.ExampleId == result.ExampleId && e.Value == result.Value));
        }

        [TestMethod]
        [Description("Checks that a bad request is returned and a new example is not added")]
        public void ExampleBadRequest()
        {
            var beforeExamples = ExampleHelper.GetExamples();

            var restClient = new RestClient(RESOURCE, HttpVerbs.Post);
            var result = restClient.Execute<Response>();

            Assert.AreEqual(HttpStatusCode.BadRequest, restClient.ResponseStatusCode);
            Assert.IsNotNull(result);
            Assert.AreEqual("A non-empty request body is required.", result.Errors.First().Value.First());

            var afterExamples = ExampleHelper.GetExamples();
            Assert.IsTrue(beforeExamples.Count == afterExamples.Count);
        }
    }
}
