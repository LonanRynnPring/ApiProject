using ApprenticeWebAPI.DataAccessLayer.Interfaces;
using ApprenticeWebAPI.Models.Dto;
using ApprenticeWebAPI.Utility.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace ApprenticeWebAPI.DataAccessLayer
{
    /// <summary>
    /// Class for the account repository implementation.
    /// </summary>
    public class AccountsRepository : IAccountsRepository
    {
        /// <summary>
        /// IDataHelper interface.
        /// </summary>
        private readonly IDataHelper _dataHelper;

        /// <summary>
        /// The configuration
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="dataHelper">The data helper.</param>
        /// <param name="configuration">The configuration.</param>
        public AccountsRepository(IDataHelper dataHelper, IConfiguration configuration)
        {
            _dataHelper = dataHelper;
            Configuration = configuration;
        }

        /// <inheritdoc />
        public int CreateAccount(string firstName, string surname, string title, string email)
        {
            var accountId = 0;
            _dataHelper.Execute(
                (c) => accountId = (int)c,
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.CREATE_ACCOUNT),
                _dataHelper.BindParameters(new Dictionary<string, object>
                {
                    { "@FirstName", firstName },
                    { "@Surname", surname },
                    { "@Title", title },
                    { "@Email", email }
                }));

            return accountId;
        }

        /// <inheritdoc />
        public IList<AccountResponseDto> Getaccounts()
        {
            DataTable dtAccounts = _dataHelper.Execute<DataTable>(
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.GET_ACCOUNTS));

            List<AccountResponseDto> accounts = new List<AccountResponseDto>();

            foreach (DataRow drAccount in dtAccounts.Rows)
            {
                accounts.Add(new AccountResponseDto
                {
                    AccountId = drAccount.Field<int>("AccountId"),
                    FirstName = drAccount.Field<string>("FirstName"),
                    Surname = drAccount.Field<string>("Surname"),
                    Title = drAccount.Field<string>("Title"),
                    Email = drAccount.Field<string>("Email")
                });
            }

            return accounts;
        }

        /// <inheritdoc />
        public AccountResponseDto GetAccountById(int accountId)
        {
            DataTable dtAccounts = _dataHelper.Execute<DataTable>(
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.GET_ACCOUNT_BY_ID),
                _dataHelper.BindParameters(new Dictionary<string, object>
                {
                    { "@AccountId", accountId }
                }));

            AccountResponseDto account = default(AccountResponseDto);

            if (dtAccounts.Rows.Count == 1)
            {
                DataRow draccount = dtAccounts.Rows[0];
                account = new AccountResponseDto
                {
                    AccountId = draccount.Field<int>("AccountId"),
                    Value = draccount.Field<string>("Value")
                };
            }

            return account;
        }

        /// <inheritdoc />
        public void UpdateAccount(int accountId, string value)
        {
            _dataHelper.Execute(
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.UPDATE_ACCOUNT),
                _dataHelper.BindParameters(new Dictionary<string, object>
                {
                    { "@AccountId", accountId },
                    { "@Value", value }
                }));
        }

        /// <inheritdoc />
        public void DeleteAccount(int accountId)
        {
            _dataHelper.Execute(
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
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
    }
}