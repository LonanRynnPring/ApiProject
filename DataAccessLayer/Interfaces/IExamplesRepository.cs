using ApprenticeWebAPI.Models.Dto;
using System.Collections.Generic;

namespace ApprenticeWebAPI.DataAccessLayer.Interfaces
{
    /// <summary>
    /// Interface for the examples repository.
    /// </summary>
    public interface IExamplesRepository
    {
        /// <summary>
        /// Method for creating an example.
        /// </summary>
        /// <param name="value">The example value.</param>
        /// <returns>The Id of the new example.</returns>
        int CreateExample(string value);

        /// <summary>
        /// Method for getting all examples.
        /// </summary>
        /// <returns>The collection of examples.</returns>
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
        /// <param name="value">The new example value.</param>
        void UpdateExample(int exampleId, string value);

        /// <summary>
        /// Method for deleting an example.
        /// </summary>
        /// <param name="exampleId">The example Id.</param>
        void DeleteExample(int exampleId);
    }
}
