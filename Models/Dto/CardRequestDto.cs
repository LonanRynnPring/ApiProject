using ApprenticeWebAPI.Utility;

namespace ApprenticeWebAPI.Models.Dto
{
    /// <summary>
    /// The card requrest Dto.
    /// </summary>
    public class CardRequestDto
    {
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
