using ApprenticeWebAPI.Models.Dto;
using ApprenticeWebAPI.Models.Entity;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApprenticeWebAPI.Utility
{
    /// <summary>
    /// Class for converting PATCH requests.
    /// </summary>
    public static class PatchRequestConverter
    {
        /// <summary>
        /// Method for generating a list of patch requests from a PATCH document.
        /// </summary>
        /// <param name="patchRequest">The original PATCH document request.</param>
        /// <returns>A list of PATCH requests.</returns>
        public static List<PatchRequest> GeneratePatchRequestList(JsonPatchDocument<AccountRequestDto> patchRequest)
        {
            var list = new List<PatchRequest>();

            foreach (var operation in patchRequest.Operations)
            {
                var request = new PatchRequest
                {
                    ParameterName = operation.path.Substring(operation.path.LastIndexOf("/")),
                    Value = operation.value?.ToString() ?? string.Empty
                };

                switch (operation.op.ToLower())
                {
                    case "add":
                        {
                            request.Action = PatchOperations.Add;
                            break;
                        }
                    case "remove":
                        {
                            request.Action = PatchOperations.Remove;
                            break;
                        }
                    case "replace":
                        {
                            request.Action = PatchOperations.Replace;
                            break;
                        }
                }

                list.Add(request);
            }

            return list;
        }
    }
}
