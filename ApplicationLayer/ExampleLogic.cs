using ApprenticeWebAPI.ApplicationLayer.Interfaces;
using ApprenticeWebAPI.DataAccessLayer.Interfaces;
using ApprenticeWebAPI.Models.Dto;

namespace ApprenticeWebAPI.ApplicationLayer
{
    /// <summary>
    /// The example logic implementation.
    /// </summary>
    public class ExampleLogic : IExampleLogic
    {
        /// <summary>
        /// Example repository interface.
        /// </summary>
        private readonly IExamplesRepository _examplesRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="examplesRepository">The example repository.</param>
        public ExampleLogic(IExamplesRepository examplesRepository)
        {
            _examplesRepository = examplesRepository;
        }

        /// <inheritdoc />
        public ExampleResponseDto CreateExample(ExampleRequestDto exampleRequestDto)
        {
            var exampleId = _examplesRepository.CreateExample(exampleRequestDto.Value);
            ExampleResponseDto exampleResponseDto = new ExampleResponseDto { ExampleId = exampleId, Value = exampleRequestDto.Value };
            return exampleResponseDto;
        }
    }
}
