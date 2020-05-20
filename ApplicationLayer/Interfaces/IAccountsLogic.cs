using ApprenticeWebAPI.Models.Dto;
using ApprenticeWebAPI.Models.Entity;
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
        /// <param name="accountsEntity">The account entity.</param>
        /// <returns>An account entity.</returns>
        AccountsEntity CreateAccount(AccountsEntity accountsEntity);

        /// <summary>
        /// Method for retrieving all accounts.
        /// </summary>
        /// <returns>A collection of accounts.</returns>
        List<AccountsEntity> RetrieveAccounts();

        /// <summary>
        /// Method for retrieving a single account.
        /// </summary>
        /// <param name="accountId">The Id of the account we want to retrieve.</param>
        /// <returns>The account.</returns>
        AccountsEntity RetrieveAccount(int accountId);

        /// <summary>
        /// Method for updating an account.
        /// </summary>
        /// <param name="accountId">The Id of the account we want to retrieve.</param>
        /// <param name="patchRequest">The account patch request.</param>
        /// <returns>The updated account.</returns>
        AccountsEntity UpdateAccount(int accountId, JsonPatchDocument<AccountRequestDto> patchRequest);

        /// <summary>
        /// Method for deleting an account.
        /// </summary>
        /// <param name="accountId">The Id of the account we want to retrieve.</param>
        /// <returns>The status of the deletion of the account.</returns>
        bool DeleteAccount(int accountId);
    }
}
