using System;
using System.Collections.Generic;
using System.Text;

namespace ApprenticeIntegrationTests.Models.Example
{
    public class Response : ErrorResponse
    {
        public string Value { get; set; }
        public int? ExampleId { get; set; }
    }
}
