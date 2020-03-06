using ApprenticeWebAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Net;

namespace ApprenticeWebAPI.ApplicationLayer.Interfaces
{
    /// <summary>
    /// The address logic interface.
    /// </summary>
    public interface IAddressesLogic
    {
        /// <summary>
        /// Method for creating an address for an account.
        /// </summary>
        /// <param name="accountId">The account Id.</param>
        /// <param name="addressRequestDto">The address request Dto.</param>
        /// <returns>An address response Dto.</returns>
        AddressResponseDto CreateAddress(int accountId, AddressRequestDto addressRequestDto);

        /// <summary>
        /// Method for retrieving account addresses.
        /// </summary>
        /// <param name="accountId">The Id of the account we want to retrieve addresses for.</param>
        /// <returns>The account's addresses.</returns>
        IList<AddressResponseDto> RetrieveAccountAddresses(int accountId);

        /// <summary>
        /// Method for retrieving an address.
        /// </summary>
        /// <param name="accountId">The Id of the account we want to retrieve address for.</param>
        /// <param name="addressId">The Id of the address to retrieve.</param>
        /// <returns>The address.</returns>
        AddressResponseDto RetrieveAddressById(int accountId, int addressId);

        /// <summary>
        /// Method for updating an address.
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <param name="addressId">The Id of the address to update.</param>
        /// <param name="patchRequest">The address patch request.</param>
        /// <returns>The updated address.</returns>
        AddressResponseDto UpdateAddress(int accountId, int addressId, JsonPatchDocument<AddressRequestDto> patchRequest);

        /// <summary>
        /// Method for deleting an address.
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <param name="addressId">The Id of the address to delete.</param>
        /// <returns>The status of the deletion of the address.</returns>
        HttpStatusCode DeleteAddress(int accountId, int addressId);
    }
}
