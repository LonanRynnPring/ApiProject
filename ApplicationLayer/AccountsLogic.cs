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
    /// The accounts logic implementation.
    /// </summary>
    public class AccountsLogic : IAccountsLogic
    {
        #region CRUD Operation Methods

        /// <inheritdoc />
        public AccountResponseDto CreateAccount(AccountRequestDto accountRequestDto)
        {
            /*
             * TODO: Replace the AccountResponseDto instance with a call to the repository layer to add
             * the account to the database. 
             * Assign the newly created account's details to the response and return back out to the controller
             * layer.
            */

            var response = new AccountResponseDto
            {
                AccountId = 1,
                FirstName = accountRequestDto.FirstName,
                Surname = accountRequestDto.Surname
            };

            return response;
        }

        /// <inheritdoc />
        public IList<AccountResponseDto> RetrieveAccounts()
        {
            /*
             * TODO: Replace GetAccounts() with a call to the repository layer to retrieve
             * any accounts from the database.
             * Assign the returned accounts to the response and return back out to the controller layer.
            */

            return GetAccounts();
        }

        /// <inheritdoc />
        public AccountResponseDto RetrieveAccount(int accountId)
        {
            /*
             * TODO: Replace GetAccounts() with a call to the repository layer to retrieve
             * the account that matches the provided Id parameter from the database.
             * Assign the existing returned account to the response and return back out to the controller layer.
            */

            return GetAccounts().FirstOrDefault(a => a.AccountId == accountId);
        }

        /// <inheritdoc />
        public AccountResponseDto UpdateAccount(int accountId, JsonPatchDocument<AccountRequestDto> patchRequest)
        {
            var patchModel = new AccountRequestDto();
            patchRequest.ApplyTo(patchModel, new ObjectAdapter(patchRequest.ContractResolver, logErrorAction: null));           

            /*
             * TODO: Replace GetAccounts() with a call to the repository layer to retrieve
             * the account that matches the provided Id parameter from the database to check it exists.
             * Update the account details with the request parameters and then go back to the repository layer to update the 
             * row in the table.
             * Assign the updated account to the response and return back out to the controller layer.
            */

            var account = GetAccounts().FirstOrDefault(a => a.AccountId == accountId);

            if (account != default(AccountResponseDto))
            {
                var patchOperations = PatchRequestConverter<AccountRequestDto>.GeneratePatchRequestList(patchRequest);

                foreach (PatchRequest patchOperation in patchOperations)
                {
                    if (patchOperation.Action == PatchOperations.Replace)
                    {
                        if (patchOperation.ParameterName.Equals($"/{nameof(AccountRequestDto.FirstName)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            account.FirstName = patchOperation.Value;
                        }
                        else if (patchOperation.ParameterName.Equals($"/{nameof(AccountRequestDto.Surname)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            account.Surname = patchOperation.Value;
                        }
                    }
                }
            }

            return account;
        }

        /// <inheritdoc />
        public HttpStatusCode DeleteAccount(int accountId)
        {
            /*
             * TODO: Replace GetAccounts() with a call to the repository layer to retrieve
             * the account that matches the provided Id parameter from the database to check it exists.
             * If the account exists, remove the row from the table by going back to the repository layer.
            */

            var accounts = GetAccounts();
            var account = accounts.FirstOrDefault(a => a.AccountId == accountId);

            if (account != default(AccountResponseDto))
            {
                accounts.Remove(account);
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.NotFound;
        }

        #endregion CRUD Operation Methods

        #region Private Methods

        /// <summary>
        /// Temporary method for returning dummy data.
        /// </summary>
        /// <returns>A list of dummy data accounts.</returns>
        private List<AccountResponseDto> GetAccounts()
        {            
            return new List<AccountResponseDto>()
            {
                new AccountResponseDto
                {
                    AccountId = 1,
                    FirstName = "Demo",
                    Surname = "Account 1"
                }
            };
        }

        #endregion Private Methods
    }
}
