namespace ApprenticeWebAPI.Models.Dto
{
    /// <summary>
    /// The account response Dto.
    /// </summary>
    public class AccountResponseDto
    {
        /// <summary>
        /// The account Id.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// The first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The surname.
        /// </summary>
        public string Surname { get; set; }
    }
}
