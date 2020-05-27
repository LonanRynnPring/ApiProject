using ApprenticeWebAPI.ApplicationLayer.Interfaces;
using ApprenticeWebAPI.Controllers;
using ApprenticeWebAPI.DataAccessLayer.Interfaces;
using ApprenticeWebAPI.Models.Dto;
using ApprenticeWebAPI.Models.Entity;
using ApprenticeWebAPI.Models.Mapper;
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
        public AccountsEntity CreateAccount(AccountsEntity accountsEntity)
        {
            return _accountsRepository.CreateAccount(accountsEntity);
        }

        /// <inheritdoc />
        public List<AccountsEntity> RetrieveAccounts()
        {
            return _accountsRepository.GetAccounts();
        }

        /// <inheritdoc />
        public AccountsEntity RetrieveAccount(int accountId)
        {
            return _accountsRepository.GetAccountById(accountId);
        }

        /// <inheritdoc />
        public AccountsEntity UpdateAccount(int accountId, JsonPatchDocument<AccountRequestDto> patchRequest)
        {
            var patchModel = new AccountRequestDto();
            patchRequest.ApplyTo(patchModel, new ObjectAdapter(patchRequest.ContractResolver, logErrorAction: null));

            AccountsEntity accountsEntity = _accountsRepository.GetAccountById(accountId);

            if (accountsEntity != default(AccountsEntity))
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

            accountsEntity = _accountsRepository.GetAccountById(accountId);
            return accountsEntity;
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
    }
}
