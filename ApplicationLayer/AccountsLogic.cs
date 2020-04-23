using ApprenticeWebAPI.ApplicationLayer.Interfaces;
using ApprenticeWebAPI.DataAccessLayer.Interfaces;
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

        /// <summary>
        /// Account repository interface.
        /// </summary>
        private readonly IAccountsRepository _accountsRepository;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="accountsRepository">The accounts repository.</param>
        public AccountsLogic(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        #region CRUD Operation Methods

        /// <inheritdoc />
        //public AccountResponseDto CreateAccount(AccountRequestDto accountRequestDto)
        //{
        //    /*
        //     * TODO: Replace the AccountResponseDto instance with a call to the repository layer to add
        //     * the account to the database. 
        //     * Assign the newly created account's details to the response and return back out to the controller
        //     * layer.
        //    */

        //    var response = new AccountResponseDto
        //    {
        //        AccountId = 1,
        //        FirstName = accountRequestDto.FirstName,
        //        Surname = accountRequestDto.Surname
        //    };

        //    return response;
        //}
        public AccountResponseDto CreateAccount(AccountRequestDto accountRequestDto)
        {
            var accountId = _accountsRepository.CreateAccount(accountRequestDto.FirstName, accountRequestDto.Surname, accountRequestDto.Title, accountRequestDto.Email);
            var accountResponseDto = MapToAccountResponseDto(_accountsRepository.GetAccountById(accountId));
            return accountResponseDto;
        }

        /// <inheritdoc />
        public IList<AccountResponseDto> RetrieveAccounts()
        {
            /*
             * TODO: Replace GetAccounts() with a call to the repository layer to retrieve
             * any accounts from the database.
             * Assign the returned accounts to the response and return back out to the controller layer.
            */

            IList<AccountResponseDto> AccountsEntity = MapToAccountResponseDtoList(_accountsRepository.GetAccount());
            //AccountsEntity = MapToAccountResponseDto(AccountsEntity);
            return AccountsEntity;
        }

        /// <inheritdoc />
        public AccountResponseDto RetrieveAccount(int accountId)
        {
            /*
             * TODO: Replace GetAccounts() with a call to the repository layer to retrieve
             * the account that matches the provided Id parameter from the database.
             * Assign the existing returned account to the response and return back out to the controller layer.
            */

            //return GetAccounts().FirstOrDefault(a => a.AccountId == accountId);

            AccountResponseDto accountResponseDto = MapToAccountResponseDto(_accountsRepository.GetAccount().FirstOrDefault(a => a.AccountId == accountId));
            return accountResponseDto;
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

            AccountResponseDto accountResponseDto = MapToAccountResponseDto(_accountsRepository.GetAccount().FirstOrDefault(a => a.AccountId == accountId));

            if (accountResponseDto != default(AccountResponseDto))
            {
                var patchOperations = PatchRequestConverter<AccountRequestDto>.GeneratePatchRequestList(patchRequest);

                foreach (PatchRequest patchOperation in patchOperations)
                {
                    if (patchOperation.Action == PatchOperations.Replace)
                    {
                        if (patchOperation.ParameterName.Equals($"/{nameof(AccountRequestDto.FirstName)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            accountResponseDto.FirstName = patchOperation.Value;
                        }
                        else if (patchOperation.ParameterName.Equals($"/{nameof(AccountRequestDto.Surname)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            accountResponseDto.Surname = patchOperation.Value;
                        }
                        else if (patchOperation.ParameterName.Equals($"/{nameof(AccountRequestDto.Title)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            accountResponseDto.Title = patchOperation.Value;
                        }
                        else if (patchOperation.ParameterName.Equals($"/{nameof(AccountRequestDto.Email)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            accountResponseDto.Email = patchOperation.Value;
                        }
                    }
                }
            }

            return accountResponseDto;
        }

        /// <inheritdoc />
        public HttpStatusCode DeleteAccount(int accountId)
        {
            /*
             * TODO: Replace GetAccounts() with a call to the repository layer to retrieve
             * the account that matches the provided Id parameter from the database to check it exists.
             * If the account exists, remove the row from the table by going back to the repository layer.
            */

            IList<AccountsEntity> accountsEntity = _accountsRepository.GetAccount();
            var account = accountsEntity.FirstOrDefault(a => a.AccountId == accountId);

            if (account != default(AccountsEntity))
            {
                accountsEntity.Remove(account);
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.NotFound;
        }

        #endregion CRUD Operation Methods

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataIn"></param>
        /// <returns></returns>
        private AccountResponseDto MapToAccountResponseDto(AccountsEntity dataIn)
        {
            AccountResponseDto accountResponseDto = new AccountResponseDto
            {
                AccountId = dataIn.AccountId,
                FirstName = dataIn.FirstName,
                Surname = dataIn.Surname,
                Title = dataIn.Title,
                Email = dataIn.Email,
                DateCreated = dataIn.DateCreated,
                DateLastUpdated = dataIn.DateLastUpdated
            };
            return accountResponseDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataIn"></param>
        /// <returns></returns>
        private IList<AccountResponseDto> MapToAccountResponseDtoList(List<AccountsEntity> dataIn)
        {
            var account = new List<AccountResponseDto>();
            foreach(AccountsEntity accountsEntity in dataIn)
            {
                AccountResponseDto accountResponseDto = new AccountResponseDto
                {
                    AccountId = accountsEntity.AccountId,
                    FirstName = accountsEntity.FirstName,
                    Surname = accountsEntity.Surname,
                    Title = accountsEntity.Title,
                    Email = accountsEntity.Email,
                    DateCreated = accountsEntity.DateCreated,
                    DateLastUpdated = accountsEntity.DateLastUpdated
                };
                account.Add(accountResponseDto);
            }
            return account;
        }

        #endregion Private Methods
    }
}
