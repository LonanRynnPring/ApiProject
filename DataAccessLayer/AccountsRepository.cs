using ApprenticeWebAPI.DataAccessLayer.Interfaces;
using ApprenticeWebAPI.Models.Dto;
using ApprenticeWebAPI.Models.Entity;
using ApprenticeWebAPI.Utility.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace ApprenticeWebAPI.DataAccessLayer
{
    /// <summary>
    /// Class for the account repository implementation.
    /// </summary>
    public class AccountsRepository : IAccountsRepository
    {        
        private readonly IDataHelper _dataHelper;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="dataHelper">The data helper.</param>
        /// <param name="configuration">The configuration.</param>
        public AccountsRepository(IDataHelper dataHelper, IConfiguration configuration)
        {
            _dataHelper = dataHelper;
            _configuration = configuration;
        }
        
        /// <inheritdoc />
        public AccountsEntity CreateAccount(AccountsEntity entity)
        {
            DataTable dtAccounts = _dataHelper.Execute<DataTable>(
                _dataHelper.BindDb(_configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.CREATE_ACCOUNT),
                _dataHelper.BindParameters(new Dictionary<string, object>
                {
                    { "@FirstName", entity.FirstName },
                    { "@Surname", entity.Surname },
                    { "@Title", entity.Title },
                    { "@Email", entity.Email }
                }));

            AccountsEntity account = default(AccountsEntity);

            if (dtAccounts.Rows.Count == 1)
            {
                DataRow drAccount = dtAccounts.Rows[0];
                account = rowToEntity(drAccount);
            }

            return account;
        }

        /// <inheritdoc />
        public List<AccountsEntity> GetAccounts()
        {
            DataTable dtAccounts = _dataHelper.Execute<DataTable>(
                _dataHelper.BindDb(_configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.GET_ACCOUNTS));

            List<AccountsEntity> accounts = new List<AccountsEntity>();

            foreach (DataRow drAccount in dtAccounts.Rows)
            {
                accounts.Add(rowToEntity(drAccount));
            }

            return accounts;
        }

        /// <inheritdoc />
        public AccountsEntity GetAccountById(int accountId)
        {
            DataTable dtAccounts = _dataHelper.Execute<DataTable>(
                _dataHelper.BindDb(_configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.GET_ACCOUNT_BY_ID),
                _dataHelper.BindParameters(new Dictionary<string, object>
                {
                    { "@AccountId", accountId }
                }));

            AccountsEntity account = default(AccountsEntity);

            if (dtAccounts.Rows.Count == 1)
            {
                DataRow drAccount = dtAccounts.Rows[0];
                account = rowToEntity(drAccount);
            }

            return account;
        }

        /// <inheritdoc />
        public void UpdateAccount(int accountId, string firstName = null, string surname = null, string title = null, string email = null)
        {
            _dataHelper.Execute(
                _dataHelper.BindDb(_configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.UPDATE_ACCOUNT),
                _dataHelper.BindParameters(new Dictionary<string, object>
                {
                    { "@AccountId", accountId },
                    { "@FirstName", firstName },
                    { "@Surname", surname },
                    { "@Title", title },
                    { "@Email", email }
                }));
        }

        /// <inheritdoc />
        public void DeleteAccount(int accountId)
        {
            _dataHelper.Execute(
                _dataHelper.BindDb(_configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.DELETE_ACCOUNT),
                _dataHelper.BindParameters(new Dictionary<string, object>
                {
                    { "@AccountId", accountId }
                }));
        }

        #region Auxiliaries

        private class StoredProcedures
        {
            public const string CREATE_ACCOUNT = "[dbo].[CreateAccount]";
            public const string GET_ACCOUNTS = "[dbo].[GetAccounts]";
            public const string GET_ACCOUNT_BY_ID = "[dbo].[GetAccountById]";
            public const string UPDATE_ACCOUNT = "[dbo].[UpdateAccount]";
            public const string DELETE_ACCOUNT = "[dbo].[DeleteAccount]";
        }

        #endregion Auxiliaries

        #region Private Methods

        private class Columns
        {
            public const string ACCOUNT_ID = "AccountId";
            public const string FIRST_NAME = "FirstName";
            public const string SURNAME = "Surname";
            public const string TITLE = "Title";
            public const string EMAIL = "Email";
            public const string DATE_CREATED = "DateCreated";
            public const string DATE_LAST_UPDATED = "DateLastUpdated";
        }

        private AccountsEntity rowToEntity(DataRow drAccount)
        {

            return new AccountsEntity
            {
                AccountId = drAccount.Field<int>(Columns.ACCOUNT_ID),
                FirstName = drAccount.Field<string>(Columns.FIRST_NAME),
                Surname = drAccount.Field<string>(Columns.SURNAME),
                Title = drAccount.Field<string>(Columns.TITLE),
                Email = drAccount.Field<string>(Columns.EMAIL),
                DateCreated = drAccount.Field<DateTime>(Columns.DATE_CREATED),
                DateLastUpdated = drAccount.Field<DateTime>(Columns.DATE_LAST_UPDATED)
            };
        }

        #endregion Private Methods
    }
}