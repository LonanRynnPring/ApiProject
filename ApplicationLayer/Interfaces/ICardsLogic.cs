using ApprenticeWebAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Net;

namespace ApprenticeWebAPI.ApplicationLayer.Interfaces
{
    /// <summary>
    /// The card logic interface.
    /// </summary>
    public interface ICardsLogic
    {
        /// <summary>
        /// Method for creating a card for an account.
        /// </summary>
        /// <param name="accountId">The account Id.</param>
        /// <param name="cardRequestDto">The card creation request Dto.</param>
        /// <returns>The card creation response Dto.</returns>
        CardResponseDto CreateCardForAccount(int accountId, CardRequestDto cardRequestDto);

        /// <summary>
        /// Method for retrieving account cards.
        /// </summary>
        /// <param name="accountId">The Id of the account we want to retrieve cards for.</param>
        /// <returns>The account's cards.</returns>
        IList<CardResponseDto> RetrieveAccountCards(int accountId);

        /// <summary>
        /// Method for retrieving a card.
        /// </summary>
        /// <param name="accountId">The Id of the account we want to retrieve card for.</param>
        /// <param name="cardId">The Id of the card to retrieve.</param>
        /// <returns>The card.</returns>
        CardResponseDto RetrieveAccountCardById(int accountId, int cardId);

        /// <summary>
        /// Method for updating a card.
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <param name="cardId">The Id of the card to update.</param>
        /// <param name="patchRequest">The card patch request.</param>
        /// <returns>The updated card.</returns>
        CardResponseDto UpdateCard(int accountId, int cardId, JsonPatchDocument<CardRequestDto> patchRequest);

        /// <summary>
        /// Method for deleting a card.
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <param name="cardId">The Id of the card to delete.</param>
        /// <returns>The status of the deletion of the card.</returns>
        HttpStatusCode DeleteCard(int accountId, int cardId);
    }
}
