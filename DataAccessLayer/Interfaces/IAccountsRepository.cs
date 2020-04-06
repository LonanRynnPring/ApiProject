using ApprenticeWebAPI.Models.Dto;
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
        /// <param name="firstName">The accounts first name.</param>
        /// <param name="surname">The accounts surname.</param>
        /// <param name="title">The accounts title.</param>
        /// <param name="email">The accounts email.</param>
        /// <returns>The Id of the new account.</returns>
        int CreateAccount(string firstName, string surname, string title, string email);

        /// <summary>
        /// Method for getting all accounts.
        /// </summary>
        /// <returns>The collection of accounts.</returns>
        IList<AccountResponseDto> GetAccount();

        /// <summary>
        /// Method for getting an account by Id.
        /// </summary>
        /// <param name="accountId">The accounts Id.</param>
        /// <returns>The account.</returns>
        AccountResponseDto GetAccountById(int accountId);

        /// <summary>
        /// Method for updating an account.
        /// </summary>
        /// <param name="accountId">The accounts Id.</param>
        /// <param name="firstName">The accounts new first name.</param>
        /// <param name="surname">The accounts new surname.</param>
        /// <param name="title">The accounts new title.</param>
        /// <param name="email">The accounts new email.</param>
        void UpdateAccount(int accountId, string firstName, string surname, string title, string email);

        /// <summary>
        /// Method for deleting an account.
        /// </summary>
        /// <param name="accountId">The account Id.</param>
        void DeleteAccount(int accountId);
    }
}
