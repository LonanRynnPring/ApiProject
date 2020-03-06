using ApprenticeWebAPI.ApplicationLayer.Interfaces;
using ApprenticeWebAPI.Models.Dto;
using ApprenticeWebAPI.Models.Entity;
using ApprenticeWebAPI.Utility;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ApprenticeWebAPI.ApplicationLayer
{
    /// <summary>
    /// The address logic implementation.
    /// </summary>
    public class AddressesLogic : IAddressesLogic
    {
        #region CRUD Methods

        /// <inheritdoc />
        public AddressResponseDto CreateAddress(int accountId, AddressRequestDto addressRequestDto)
        {
            /*
             * TODO: Replace the AddressResponseDto instance with a call to the repository layer to add
             * the address to the database. Then assign the new address row to the account via a linking table.
             * Assign the newly created address's details to the response and return back out to the controller
             * layer.
            */

            var response = new AddressResponseDto
            {
                AddressId = 1,
                AccountId = accountId,
                HouseNumber = addressRequestDto.HouseNumber,
                Street = addressRequestDto.Street,
                TownCity = addressRequestDto.TownCity,
                County = addressRequestDto.County,
                PostCode = addressRequestDto.PostCode
            };

            return response;
        }

        /// <inheritdoc />
        public IList<AddressResponseDto> RetrieveAccountAddresses(int accountId)
        {
            /*
             * TODO: Replace GetAddresses() with a call to the repository layer to retrieve
             * the addresses that matches the provided Id parameter from the database.
             * Assign the existing returned addresses to the response and return back out to the controller layer.
            */

            return GetAddresses().Where(a => a.AccountId == accountId).ToList();
        }

        /// <inheritdoc />
        public AddressResponseDto RetrieveAddressById(int accountId, int addressId)
        {
            /*
             * TODO: Replace GetAddresses() with a call to the repository layer to retrieve
             * the address that matches the provided Id parameters from the database.
             * Assign the existing returned address to the response and return back out to the controller layer.
            */

            return GetAddresses().FirstOrDefault(a => a.AccountId == accountId && a.AddressId == addressId);
        }

        /// <inheritdoc />
        public AddressResponseDto UpdateAddress(int accountId, int addressId, JsonPatchDocument<AddressRequestDto> patchRequest)
        {
            var patchModel = new AddressRequestDto();
            patchRequest.ApplyTo(patchModel, new ObjectAdapter(patchRequest.ContractResolver, logErrorAction: null));

            /*
             * TODO: Replace GetAddresses() with a call to the repository layer to retrieve
             * the address that matches the provided Id parameter from the database to check it exists.
             * Update the address details with the request parameters and then go back to the repository layer to update the 
             * row in the table.
             * Assign the updated address to the response and return back out to the controller layer.
            */

            var address = GetAddresses().FirstOrDefault(a => a.AccountId == accountId && a.AddressId == addressId);

            if (address != default(AddressResponseDto))
            {
                var patchOperations = PatchRequestConverter<AddressRequestDto>.GeneratePatchRequestList(patchRequest);

                foreach (PatchRequest patchOperation in patchOperations)
                {
                    if (patchOperation.Action == PatchOperations.Replace)
                    {
                        if (patchOperation.ParameterName.Equals($"/{nameof(AddressRequestDto.HouseNumber)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            address.HouseNumber = patchOperation.Value;
                        }
                        else if (patchOperation.ParameterName.Equals($"/{nameof(AddressRequestDto.Street)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            address.Street = patchOperation.Value;
                        }
                        else if (patchOperation.ParameterName.Equals($"/{nameof(AddressRequestDto.TownCity)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            address.TownCity = patchOperation.Value;
                        }
                        else if (patchOperation.ParameterName.Equals($"/{nameof(AddressRequestDto.County)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            address.County = patchOperation.Value;
                        }
                        else if (patchOperation.ParameterName.Equals($"/{nameof(AddressRequestDto.PostCode)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            address.PostCode = patchOperation.Value;
                        }
                    }
                }
            }

            return address;
        }

        /// <inheritdoc />
        public HttpStatusCode DeleteAddress(int accountId, int addressId)
        {
            /*
             * TODO: Replace GetAddresses() with a call to the repository layer to retrieve
             * the address that matches the provided Id parameters from the database to check it exists.
             * If the address exists, remove the row from the table by going back to the repository layer.
            */

            var addresses = GetAddresses();
            var address = addresses.FirstOrDefault(a => a.AccountId == accountId && a.AddressId == addressId);

            if (address != default(AddressResponseDto))
            {
                addresses.Remove(address);
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.NotFound;
        }

        #endregion CRUD Methods

        #region Private Methods

        /// <summary>
        /// Temporary method for returning dummy data.
        /// </summary>
        /// <returns>A list of dummy data addresses.</returns>
        private List<AddressResponseDto> GetAddresses()
        {
            return new List<AddressResponseDto>
            {
                new AddressResponseDto
                {
                    AddressId = 1,
                    AccountId = 1,
                    HouseNumber = "City Tower",
                    Street = "New York Street",
                    TownCity = "Manchester",
                    County = "Greater Manchester",
                    PostCode = "M1 4BT"
                }
            };
        }

        #endregion Private Methods
    }
}
