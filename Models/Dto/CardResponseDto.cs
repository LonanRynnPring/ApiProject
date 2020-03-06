using ApprenticeWebAPI.Utility;

namespace ApprenticeWebAPI.Models.Dto
{
    /// <summary>
    /// The card response Dto.
    /// </summary>
    public class CardResponseDto
    {
        /// <summary>
        /// The card Id.
        /// </summary>
        public int CardId { get; set; }

        /// <summary>
        /// The account Id.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// The card number.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// The bank.
        /// </summary>
        public Banks Bank { get; set; }
    }
}
