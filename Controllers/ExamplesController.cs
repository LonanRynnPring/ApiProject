using ApprenticeWebAPI.ApplicationLayer.Interfaces;
using ApprenticeWebAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        /// <summary>
        /// Endpoint for getting examples.
        /// </summary>
        /// <returns>The example retrieval response.</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ExampleResponseDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(IList<ExampleResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(IList<ExampleResponseDto>))]
        public ActionResult GetExamples()
        {
            var response = _exampleLogic.GetExamples();
            return StatusCode(response.Count == 0 ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Endpoint for getting an example by Id.
        /// </summary>
        /// <returns>The example retrieval response.</returns>
        [HttpGet("{exampleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExampleResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExampleResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExampleResponseDto))]
        public ActionResult GetExampleById(int exampleId)
        {
            var response = _exampleLogic.GetExampleById(exampleId);
            return StatusCode(response == default(ExampleResponseDto) ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Endpoint for updating an example.
        /// </summary>
        /// <param name="exampleId">The Id of the example to retrieve.</param>
        /// <param name="patchRequest">The patch request body.</param>
        /// <returns>The updated example.</returns>
        [HttpPatch("{exampleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExampleResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExampleResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExampleResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExampleResponseDto))]
        public ActionResult UpdateAccount(int exampleId, [FromBody]JsonPatchDocument<ExampleRequestDto> patchRequest)
        {
            var response = _exampleLogic.UpdateExample(exampleId, patchRequest);
            return StatusCode(response == default(ExampleResponseDto) ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Endpoint for deleting an example.
        /// </summary>
        /// <param name="exampleId">The Id of the example to retrieve.</param>
        /// <returns>The status of the example deletion.</returns>
        [HttpDelete("{exampleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(void))]
        public ActionResult DeleteExample(int exampleId)
        {
            return StatusCode((int)_exampleLogic.DeleteExample(exampleId));
        }
    }
}