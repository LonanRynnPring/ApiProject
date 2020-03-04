using ApprenticeWebAPI.ApplicationLayer.Interfaces;
using ApprenticeWebAPI.Models.Dto;

namespace ApprenticeWebAPI.ApplicationLayer
{
    /// <summary>
    /// The example logic implementation.
    /// </summary>
    public class ExampleLogic : IExampleLogic
    {
        /// <inheritdoc />
        public ExampleResponseDto CreateExample(ExampleRequestDto exampleRequestDto)
        {
            ExampleResponseDto exampleResponseDto = new ExampleResponseDto { ExampleId = 1, Value = exampleRequestDto.Value };
            return exampleResponseDto;
        }
    }
}
