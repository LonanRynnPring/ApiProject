using ApprenticeWebAPI.Models.Dto;

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
    }
}
