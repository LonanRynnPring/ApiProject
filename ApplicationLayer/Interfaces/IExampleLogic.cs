using ApprenticeWebAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Net;

namespace ApprenticeWebAPI.ApplicationLayer.Interfaces
{
    /// <summary>
    /// The example logic interface.
    /// </summary>
    public interface IExampleLogic
    {
        /// <summary>
        /// Method for creating an example
        /// </summary>
        /// <param name="exampleRequestDto">The example request Dto.</param>
        /// <returns>The example response Dto.</returns>
        ExampleResponseDto CreateExample(ExampleRequestDto exampleRequestDto);

        /// <summary>
        /// Method for getting examples.
        /// </summary>
        /// <returns>The example response Dtos.</returns>
        IList<ExampleResponseDto> GetExamples();

        /// <summary>
        /// Method for getting an example by Id.
        /// </summary>
        /// <param name="exampleId">The example Id.</param>
        /// <returns>The example.</returns>
        ExampleResponseDto GetExampleById(int exampleId);

        /// <summary>
        /// Method for updating an example.
        /// </summary>
        /// <param name="exampleId">The example Id.</param>
        /// <param name="patchRequest">The example patch request.</param>
        /// <returns>The example.</returns>
        ExampleResponseDto UpdateExample(int exampleId, JsonPatchDocument<ExampleRequestDto> patchRequest);

        /// <summary>
        /// Method for deleting an example.
        /// </summary>
        /// <param name="exampleId">The example Id.</param>
        /// <returns>The status of the deletion of the example.</returns>
        HttpStatusCode DeleteExample(int exampleId);
    }
}
