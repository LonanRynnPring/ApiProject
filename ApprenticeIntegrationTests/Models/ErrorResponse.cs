using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ApprenticeIntegrationTests.Models
{
    public class ErrorResponse
    {
        public Dictionary<string, List<string>> Errors { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public HttpStatusCode Status { get; set; }
        public string TraceId { get; set; }
    }
}