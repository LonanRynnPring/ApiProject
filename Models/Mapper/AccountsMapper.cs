using ApprenticeWebAPI.Models.Dto;
using ApprenticeWebAPI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApprenticeWebAPI.Models.Mapper
{
    /// <summary>
    /// A class to map to and from account request dtos, account response dtos and account entities
    /// </summary>
    public static class AccountsMapper
    {

        /// <summary>
        /// To acccount response dto
        /// </summary>
        /// <param name="dataIn">takes an accounts entity</param>
        /// <returns>returns an account response dto</returns>
        public static AccountResponseDto MapToAccountResponseDto(this AccountsEntity dataIn)
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
        /// To account entity
        /// </summary>
        /// <param name="dataIn">takes an account request dto</param>
        /// <returns>returns an account entity</returns>
        public static AccountsEntity MapToAccountsEntity(this AccountRequestDto dataIn)
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
        /// To account response dto list
        /// </summary>
        /// <param name="dataIn">takes in a list of account entities</param>
        /// <returns>returns a list of account response dtos</returns>
        public static List<AccountResponseDto> MapToAccountResponseDtoList(this List<AccountsEntity> dataIn)
        {
            var account = new List<AccountResponseDto>();
            foreach (AccountsEntity accountsEntity in dataIn)
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
    }
}
