using ApprenticeWebAPI.ApplicationLayer.Interfaces;
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
    /// The card logic implementation.
    /// </summary>
    public class CardsLogic : ICardsLogic
    {
        #region CRUD Methods

        /// <inheritdoc />
        public CardResponseDto CreateCardForAccount(int accountId, CardRequestDto cardRequestDto)
        {
            /*
            * TODO: Replace the CardResponseDto instance with a call to the repository layer to add
            * the card to the database.
            * Assign the newly created card's details to the response and return back out to the controller
            * layer.
           */

            var response = new CardResponseDto
            {
                CardId = 1,
                AccountId = accountId,
                CardNumber = $"************{cardRequestDto.CardNumber.Substring(cardRequestDto.CardNumber.Length - 4)}",
                Bank = cardRequestDto.Bank
            };

            return response;
        }

        /// <inheritdoc />
        public IList<CardResponseDto> RetrieveAccountCards(int accountId)
        {
            /*
             * TODO: Replace GetCards() with a call to the repository layer to retrieve
             * the cards that matches the provided Id parameter from the database.
             * Assign the existing returned cards to the response and return back out to the controller layer.
            */

            return GetCards().Where(a => a.AccountId == accountId).ToList();
        }

        /// <inheritdoc />
        public CardResponseDto RetrieveAccountCardById(int accountId, int cardId)
        {
            /*
             * TODO: Replace GetCards() with a call to the repository layer to retrieve
             * the card that matches the provided Id parameters from the database.
             * Assign the existing returned card to the response and return back out to the controller layer.
            */

            return GetCards().FirstOrDefault(a => a.AccountId == accountId && a.CardId == cardId);
        }

        /// <inheritdoc />
        public CardResponseDto UpdateCard(int accountId, int cardId, JsonPatchDocument<CardRequestDto> patchRequest)
        {
            var patchModel = new CardRequestDto();
            patchRequest.ApplyTo(patchModel, new ObjectAdapter(patchRequest.ContractResolver, logErrorAction: null));

            /*
             * TODO: Replace GetCards() with a call to the repository layer to retrieve
             * the card that matches the provided Id parameters from the database to check it exists.
             * Update the card details with the request parameters and then go back to the repository layer to update the 
             * row in the table.
             * Assign the updated card to the response and return back out to the controller layer.
            */

            var card = GetCards().FirstOrDefault(a => a.AccountId == accountId && a.CardId == cardId);

            if (card != default(CardResponseDto))
            {
                var patchOperations = PatchRequestConverter<CardRequestDto>.GeneratePatchRequestList(patchRequest);

                foreach (PatchRequest patchOperation in patchOperations)
                {
                    if (patchOperation.Action == PatchOperations.Replace)
                    {
                        if (patchOperation.ParameterName.Equals($"/{nameof(CardRequestDto.CardNumber)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            card.CardNumber = $"************{patchOperation.Value.Substring(patchOperation.Value.Length - 4)}";
                        }
                        else if (patchOperation.ParameterName.Equals($"/{nameof(CardRequestDto.Bank)}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Banks newBank;
                            Enum.TryParse(patchOperation.Value, out newBank);

                            if (!Enum.TryParse(patchOperation.Value, out newBank))
                            {
                                return default(CardResponseDto);
                            }

                            card.Bank = newBank;
                        }
                    }
                }
            }

            return card;
        }

        /// <inheritdoc />
        public HttpStatusCode DeleteCard(int accountId, int cardId)
        {
            /*
             * TODO: Replace GetCards() with a call to the repository layer to retrieve
             * the card that matches the provided Id parameters from the database to check it exists.
             * If the card exists, remove the row from the table by going back to the repository layer.
            */

            var cards = GetCards();
            var card = cards.FirstOrDefault(a => a.AccountId == accountId && a.CardId == cardId);

            if (card != default(CardResponseDto))
            {
                cards.Remove(card);
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.NotFound;
        }

        #endregion CRUD Methods

        #region Private Methods

        /// <summary>
        /// Temporary method for returning dummy data.
        /// </summary>
        /// <returns>A list of dummy data cards.</returns>
        private List<CardResponseDto> GetCards()
        {
            return new List<CardResponseDto>
            {
                new CardResponseDto
                {
                    CardId = 1,
                    AccountId = 1,
                    CardNumber = "************1111",
                    Bank = Banks.Barclays
                }
            };
        }

        #endregion Private Methods
    }
}
