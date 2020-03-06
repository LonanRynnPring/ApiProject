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
    /// Address API Controller
    /// </summary>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        /// <summary>
        /// Address logic interface.
        /// </summary>
        private readonly IAddressesLogic _addressLogic;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="addressLogic">The address logic.</param>
        public AddressesController(IAddressesLogic addressLogic)
        {
            _addressLogic = addressLogic;
        }

        /// <summary>
        /// Endpoint for creating an address for an account.
        /// </summary>
        /// <param name="accountId">The account Id.</param>
        /// <param name="addressRequestDto">The address creation request body.</param>
        /// <returns>The address creation response.</returns>
        [HttpPost("api/accounts/{accountId}/addresses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AddressResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AddressResponseDto))]
        public ActionResult CreateAddress(int accountId, [FromBody]AddressRequestDto addressRequestDto)
        {
            var response = _addressLogic.CreateAddress(accountId, addressRequestDto);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// Endpoint for retrieving an account's addresses.
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <returns>The account's addresses.</returns>
        [HttpGet("api/accounts/{accountId}/addresses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<AddressResponseDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(IList<AddressResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IList<AddressResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(IList<AddressResponseDto>))]
        public ActionResult RetrieveAccountAddresses(int accountId)
        {
            var response = _addressLogic.RetrieveAccountAddresses(accountId);
            return StatusCode(response.Count == 0 ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Endpoint for retrieving a address belonging to an account.
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <param name="addressId">The Id of the address.</param>
        /// <returns>The account's address.</returns>
        [HttpGet("api/accounts/{accountId}/addresses/{addressId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressResponseDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(AddressResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AddressResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AddressResponseDto))]
        public ActionResult RetrieveAccountAddressById(int accountId, int addressId)
        {
            var response = _addressLogic.RetrieveAddressById(accountId, addressId);
            return StatusCode(response == default(AddressResponseDto) ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Endpoint for updating an address.
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <param name="addressId">The Id of the address to update.</param>
        /// <param name="patchRequest">The patch request body.</param>
        /// <returns>The updated address.</returns>
        [HttpPatch("api/accounts/{accountId}/addresses/{addressId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(AddressResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AddressResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AddressResponseDto))]
        public ActionResult UpdateAccount(int accountId, int addressId, [FromBody]JsonPatchDocument<AddressRequestDto> patchRequest)
        {
            var response = _addressLogic.UpdateAddress(accountId, addressId, patchRequest);
            return StatusCode(response == default(AddressResponseDto) ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Endpoint for deleting an address.
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <param name="addressId">The Id of the address to delete.</param>
        /// <returns>The status of the address deletion.</returns>
        [HttpDelete("api/accounts/{accountId}/addresses/{addressId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(void))]
        public ActionResult DeleteAddress(int accountId, int addressId)
        {
            return StatusCode((int)_addressLogic.DeleteAddress(accountId, addressId));
        }
    }
}