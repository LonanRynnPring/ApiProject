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
        public AccountResponseDto CreateAccount(AccountRequestDto accountRequestDto)
        {
            var entity = MapToAccountsEntity(accountRequestDto);
            var account = _accountsRepository.CreateAccount(entity);
            var accountResponseDto = MapToAccountResponseDto(account);
            return accountResponseDto;
        }

        /// <inheritdoc />
        public List<AccountResponseDto> RetrieveAccounts()
        {
            List<AccountResponseDto> AccountsEntities = MapToAccountResponseDtoList(_accountsRepository.GetAccounts());
            return AccountsEntities;
        }

        /// <inheritdoc />
        public AccountResponseDto RetrieveAccount(int accountId)
        {
            AccountResponseDto accountResponseDto = MapToAccountResponseDto(_accountsRepository.GetAccountById(accountId));
            return accountResponseDto;
        }

        /// <inheritdoc />
        public AccountResponseDto UpdateAccount(int accountId, JsonPatchDocument<AccountRequestDto> patchRequest)
        {
            var patchModel = new AccountRequestDto();
            patchRequest.ApplyTo(patchModel, new ObjectAdapter(patchRequest.ContractResolver, logErrorAction: null));

            AccountResponseDto accountResponseDto = MapToAccountResponseDto(_accountsRepository.GetAccountById(accountId));

            if (accountResponseDto != default(AccountResponseDto))
            {
                var patchOperations = PatchRequestConverter<AccountRequestDto>.GeneratePatchRequestList(patchRequest);
                string firstName = null, surname = null, title = null, email = null;
                foreach (PatchRequest patchOperation in patchOperations)
                {
                    if (patchOperation.Action == PatchOperations.Replace)
                    {
                        if (patchOperation.ParameterName.Equals($"/{nameof(AccountRequestDto.FirstName)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            firstName = patchOperation.Value;
                        }
                        else if (patchOperation.ParameterName.Equals($"/{nameof(AccountRequestDto.Surname)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            surname = patchOperation.Value;
                        }
                        else if (patchOperation.ParameterName.Equals($"/{nameof(AccountRequestDto.Title)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            title = patchOperation.Value;
                        }
                        else if (patchOperation.ParameterName.Equals($"/{nameof(AccountRequestDto.Email)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            email = patchOperation.Value;
                        }
                    }
                }
                _accountsRepository.UpdateAccount(accountId, firstName, surname, title, email);
            }

            accountResponseDto = MapToAccountResponseDto(_accountsRepository.GetAccountById(accountId));
            return accountResponseDto;
        }

        /// <inheritdoc />
        public bool DeleteAccount(int accountId)
        {
            AccountsEntity account = _accountsRepository.GetAccountById(accountId);

            if (account != default(AccountsEntity))
            {
                _accountsRepository.DeleteAccount(accountId);
                return true;
            }

            return false;
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
            return new AccountResponseDto
            {
                AccountId = dataIn.AccountId,
                FirstName = dataIn.FirstName,
                Surname = dataIn.Surname,
                Title = dataIn.Title,
                Email = dataIn.Email,
                DateCreated = dataIn.DateCreated,
                DateLastUpdated = dataIn.DateLastUpdated
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataIn"></param>
        /// <returns></returns>
        private AccountsEntity MapToAccountsEntity(AccountRequestDto dataIn)
        {
            return new AccountsEntity
            {
                FirstName = dataIn.FirstName,
                Surname = dataIn.Surname,
                Title = dataIn.Title,
                Email = dataIn.Email
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataIn"></param>
        /// <returns></returns>
        private List<AccountResponseDto> MapToAccountResponseDtoList(List<AccountsEntity> dataIn)
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
