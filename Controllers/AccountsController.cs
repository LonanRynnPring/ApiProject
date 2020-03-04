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
    /// Account API Controller
    /// </summary>
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        /// <summary>
        /// Accounts logic interface.
        /// </summary>
        private readonly IAccountsLogic _accountsLogic;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="accountsLogic">The account logic.</param>
        public AccountsController(IAccountsLogic accountsLogic)
        {
            _accountsLogic = accountsLogic;
        }

        /// <summary>
        /// Endpoint for creating an account.
        /// </summary>
        /// <param name="accountRequestDto">The account creation request body.</param>
        /// <returns>The account creation response.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AccountResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AccountResponseDto))]
        public ActionResult CreateAccount([FromBody]AccountRequestDto accountRequestDto)
        {
            var response = _accountsLogic.CreateAccount(accountRequestDto);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// Endpoint for retrieving accounts.
        /// </summary>
        /// <returns>A collection of accounts.</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<AccountResponseDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(IList<AccountResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IList<AccountResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(IList<AccountResponseDto>))]
        public ActionResult RetrieveAccounts()
        {
            var response = _accountsLogic.RetrieveAccounts();
            return StatusCode(response.Count == 0 ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK, response);
        }        

        /// <summary>
        /// Endpoint for retrieving an account.
        /// </summary>
        /// <param name="accountId">The Id of the account to retrieve.</param>
        /// <returns>The account.</returns>
        [HttpGet("{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(AccountResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AccountResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AccountResponseDto))]
        public ActionResult RetrieveAccount(int accountId)
        {
            var response = _accountsLogic.RetrieveAccount(accountId);
            return StatusCode(response == default(AccountResponseDto) ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Endpoint for updating an account.
        /// </summary>
        /// <param name="accountId">The Id of the account to retrieve.</param>
        /// <param name="patchRequest">The patch request body.</param>
        /// <returns>The updated account.</returns>
        [HttpPatch("{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(AccountResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AccountResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AccountResponseDto))]
        public ActionResult UpdateAccount(int accountId, [FromBody]JsonPatchDocument<AccountRequestDto> patchRequest)
        {
            var response = _accountsLogic.UpdateAccount(accountId, patchRequest);
            return StatusCode(response == default(AccountResponseDto) ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Endpoint for deleting an account.
        /// </summary>
        /// <param name="accountId">The Id of the account to retrieve.</param>
        /// <returns>The status of the account deletion.</returns>
        [HttpDelete("{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(void))]
        public ActionResult DeleteAccount(int accountId)
        {
            return StatusCode((int)_accountsLogic.DeleteAccount(accountId));
        }
    }
}