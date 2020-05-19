using ApprenticeWebAPI.Models.Entity;
using System.Collections.Generic;

namespace ApprenticeWebAPI.DataAccessLayer.Interfaces
{
    /// <summary>
    /// Interface for the accounts repository.
    /// </summary>
    public interface IAccountsRepository
    {
        /// <summary>
        /// Method for creating an account.
        /// </summary>
        /// <param name="entity">The accounts first name.</param>
        /// <returns>The Id of the new account.</returns>
        AccountsEntity CreateAccount(AccountsEntity entity);

        /// <summary>
        /// Method for getting all accounts.
        /// </summary>
        /// <returns>The collection of accounts.</returns>
        List<AccountsEntity> GetAccounts();

        /// <summary>
        /// Method for getting an account by Id.
        /// </summary>
        /// <param name="accountId">The accounts Id.</param>
        /// <returns>The account.</returns>
        AccountsEntity GetAccountById(int accountId);

        /// <summary>
        /// Method for updating an account.
        /// </summary>
        /// <param name="accountId">The accounts Id.</param>
        /// <param name="firstName">The accounts new first name.</param>
        /// <param name="surname">The accounts new surname.</param>
        /// <param name="title">The accounts new title.</param>
        /// <param name="email">The accounts new email.</param>
        void UpdateAccount(int accountId, string firstName = null, string surname = null, string title = null, string email = null);

        /// <summary>
        /// Method for deleting an account.
        /// </summary>
        /// <param name="accountId">The account Id.</param>
        void DeleteAccount(int accountId);
    }
}
