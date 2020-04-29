using ApprenticeWebAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Net;

namespace ApprenticeWebAPI.ApplicationLayer.Interfaces
{
    /// <summary>
    /// The accounts logic interface.
    /// </summary>
    public interface IAccountsLogic
    {
        /// <summary>
        /// Method for creating an account.
        /// </summary>
        /// <param name="accountRequestDto">The account request Dto.</param>
        /// <returns>An account response Dto.</returns>
        AccountResponseDto CreateAccount(AccountRequestDto accountRequestDto);

        /// <summary>
        /// Method for retrieving all accounts.
        /// </summary>
        /// <returns>A collection of accounts.</returns>
        List<AccountResponseDto> RetrieveAccounts();

        /// <summary>
        /// Method for retrieving a single account.
        /// </summary>
        /// <param name="accountId">The Id of the account we want to retrieve.</param>
        /// <returns>The account.</returns>
        AccountResponseDto RetrieveAccount(int accountId);

        /// <summary>
        /// Method for updating an account.
        /// </summary>
        /// <param name="accountId">The Id of the account we want to retrieve.</param>
        /// <param name="patchRequest">The account patch request.</param>
        /// <returns>The updated account.</returns>
        AccountResponseDto UpdateAccount(int accountId, JsonPatchDocument<AccountRequestDto> patchRequest);

        /// <summary>
        /// Method for deleting an account.
        /// </summary>
        /// <param name="accountId">The Id of the account we want to retrieve.</param>
        /// <returns>The status of the deletion of the account.</returns>
        HttpStatusCode DeleteAccount(int accountId);
    }
}
