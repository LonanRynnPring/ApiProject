using System;

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

        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The date created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The date last updated.
        /// </summary>
        public DateTime DateLastUpdated { get; set; }
    }
}
