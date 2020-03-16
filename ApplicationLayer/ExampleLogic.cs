using ApprenticeWebAPI.ApplicationLayer.Interfaces;
using ApprenticeWebAPI.DataAccessLayer.Interfaces;
using ApprenticeWebAPI.Models.Dto;
using ApprenticeWebAPI.Models.Entity;
using ApprenticeWebAPI.Utility;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using System;
using System.Collections.Generic;
using System.Net;

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

        /// <inheritdoc />
        public IList<ExampleResponseDto> GetExamples()
        {
            return _examplesRepository.GetExamples();
        }

        /// <inheritdoc />
        public ExampleResponseDto GetExampleById(int exampleId)
        {
            return _examplesRepository.GetExampleById(exampleId);
        }

        /// <inheritdoc />
        public ExampleResponseDto UpdateExample(int exampleId, JsonPatchDocument<ExampleRequestDto> patchRequest)
        {
            var patchModel = new ExampleRequestDto();
            patchRequest.ApplyTo(patchModel, new ObjectAdapter(patchRequest.ContractResolver, logErrorAction: null));
            var example = _examplesRepository.GetExampleById(exampleId);

            if (example != default(ExampleResponseDto))
            {
                var patchOperations = PatchRequestConverter<ExampleRequestDto>.GeneratePatchRequestList(patchRequest);

                foreach (PatchRequest patchOperation in patchOperations)
                {
                    if (patchOperation.Action == PatchOperations.Replace)
                    {
                        if (patchOperation.ParameterName.Equals($"/{nameof(ExampleRequestDto.Value)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            example.Value = patchOperation.Value;
                        }
                    }
                }
            }

            _examplesRepository.UpdateExample(exampleId, example.Value);
            return example;
        }

        /// <inheritdoc />
        public HttpStatusCode DeleteExample(int exampleId)
        {
            var example = _examplesRepository.GetExampleById(exampleId);

            if (example == default(ExampleResponseDto))
            {
                return HttpStatusCode.NotFound;
            }
            
            _examplesRepository.DeleteExample(exampleId);
            return HttpStatusCode.OK;
        }
    }
}
