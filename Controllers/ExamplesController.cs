using ApprenticeWebAPI.ApplicationLayer.Interfaces;
using ApprenticeWebAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApprenticeWebAPI.Controllers
{
    /// <summary>
    /// Example API Controller
    /// </summary>
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/examples")]
    [ApiController]
    public class ExamplesController : ControllerBase
    {
        /// <summary>
        /// Example logic interface.
        /// </summary>
        private readonly IExampleLogic _exampleLogic;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exampleLogic">The example logic.</param>
        public ExamplesController(IExampleLogic exampleLogic)
        {
            _exampleLogic = exampleLogic;
        }

        /// <summary>
        /// Endpoint for creating an example.
        /// </summary>
        /// <param name="exampleRequestDto">The example creation request body.</param>
        /// <returns>The example creation response.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExampleResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExampleResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExampleResponseDto))]
        public ActionResult CreateExample([FromBody]ExampleRequestDto exampleRequestDto)
        {
            var response = _exampleLogic.CreateExample(exampleRequestDto);
            return StatusCode((int)HttpStatusCode.Created, response);
        }
    }
}