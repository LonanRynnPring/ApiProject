using ApprenticeWebAPI.ApplicationLayer.Interfaces;
using ApprenticeWebAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace ApprenticeWebAPI.Controllers
{
    /// <summary>
    /// Cards API Controller
    /// </summary>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        /// <summary>
        /// Card logic interface.
        /// </summary>
        private readonly ICardsLogic _cardLogic;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cardLogic">The card logic.</param>
        public CardsController(ICardsLogic cardLogic)
        {
            _cardLogic = cardLogic;
        }

        /// <summary>
        /// Endpoint for creating a card for an account.
        /// </summary>
        /// <param name="accountId">The account Id.</param>
        /// <param name="cardRequestDto">The card creation request body.</param>
        /// <returns>The card creation response.</returns>
        [HttpPost("api/accounts/{accountId}/cards")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CardResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CardResponseDto))]
        public ActionResult CreateAddress(int accountId, [FromBody]CardRequestDto cardRequestDto)
        {
            var response = _cardLogic.CreateCardForAccount(accountId, cardRequestDto);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// Endpoint for retrieving an account's cards.
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <returns>The account's cards.</returns>
        [HttpGet("api/accounts/{accountId}/cards")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<CardResponseDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(IList<CardResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IList<CardResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(IList<CardResponseDto>))]
        public ActionResult RetrieveAccountCards(int accountId)
        {
            var response = _cardLogic.RetrieveAccountCards(accountId);
            return StatusCode(response.Count == 0 ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Endpoint for retrieving a card belonging to an account.
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <param name="cardId">The Id of the card.</param>
        /// <returns>The account's card.</returns>
        [HttpGet("api/accounts/{accountId}/cards/{cardId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardResponseDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CardResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CardResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CardResponseDto))]
        public ActionResult RetrieveAccountAddressById(int accountId, int cardId)
        {
            var response = _cardLogic.RetrieveAccountCardById(accountId, cardId);
            return StatusCode(response == default(CardResponseDto) ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Endpoint for updating a card.
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <param name="cardId">The Id of the card to update.</param>
        /// <param name="patchRequest">The patch request body.</param>
        /// <returns>The updated card.</returns>
        [HttpPatch("api/accounts/{accountId}/cards/{cardId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CardResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CardResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CardResponseDto))]
        public ActionResult UpdateAccount(int accountId, int cardId, [FromBody]JsonPatchDocument<CardRequestDto> patchRequest)
        {
            var response = _cardLogic.UpdateCard(accountId, cardId, patchRequest);
            return StatusCode(response == default(CardResponseDto) ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Endpoint for deleting a card.
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <param name="cardId">The Id of the card to delete.</param>
        /// <returns>The status of the card deletion.</returns>
        [HttpDelete("api/accounts/{accountId}/cards/{cardId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(void))]
        public ActionResult DeleteAddress(int accountId, int cardId)
        {
            return StatusCode((int)_cardLogic.DeleteCard(accountId, cardId));
        }
    }
}