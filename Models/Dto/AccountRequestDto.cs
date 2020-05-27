namespace ApprenticeWebAPI.Models.Dto
{
    /// <summary>
    /// The account request Dto
    /// </summary>
    public class AccountRequestDto
    {
        /// <summary>
        /// The first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The surname.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The email.
        /// </summary>
        public string Email { get; set; }
    }
}