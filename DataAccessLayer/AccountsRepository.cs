using ApprenticeWebAPI.DataAccessLayer.Interfaces;
using ApprenticeWebAPI.Models.Entity;
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
        public IList<AccountsEntity> GetAccount()
        {
            DataTable dtAccounts = _dataHelper.Execute<DataTable>(
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.GET_ACCOUNTS));

            List<AccountsEntity> accounts = new List<AccountsEntity>();

            foreach (DataRow drAccount in dtAccounts.Rows)
            {
                accounts.Add(new AccountsEntity
                {
                    AccountId = drAccount.Field<int>("AccountId"),
                    FirstName = drAccount.Field<string>("FirstName"),
                    Surname = drAccount.Field<string>("Surname"),
                    Title = drAccount.Field<string>("Title"),
                    Email = drAccount.Field<string>("Email"),
                    DateCreated = drAccount.Field<string>("DateCreated"),
                    DateLastUpdated = drAccount.Field<string>("DateLastUpdated")
                });
            }

            return accounts;
        }

        /// <inheritdoc />
        public AccountsEntity GetAccountById(int accountId)
        {
            DataTable dtAccounts = _dataHelper.Execute<DataTable>(
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.GET_ACCOUNT_BY_ID),
                _dataHelper.BindParameters(new Dictionary<string, object>
                {
                    { "@AccountId", accountId }
                }));

            AccountsEntity account = default(AccountsEntity);

            if (dtAccounts.Rows.Count == 1)
            {
                DataRow drAccount = dtAccounts.Rows[0];
                account = new AccountsEntity
                {
                    AccountId = drAccount.Field<int>("AccountId"),
                    FirstName = drAccount.Field<string>("FirstName"),
                    Surname = drAccount.Field<string>("Surname"),
                    Title = drAccount.Field<string>("Title"),
                    Email = drAccount.Field<string>("Email"),
                    DateCreated = drAccount.Field<string>("DateCreated"),
                    DateLastUpdated = drAccount.Field<string>("DateLastUpdated")
                };
            }

            return account;
        }

        /// <inheritdoc />
        public void UpdateAccount(int accountId, string firstName, string surname, string title, string email)
        {
            _dataHelper.Execute(
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
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