namespace ApprenticeWebAPI.Models.Dto
{
    /// <summary>
    /// The address response Dto.
    /// </summary>
    public class AddressResponseDto
    {
        /// <summary>
        /// The address Id
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// The account Id
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// The house number.
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// The street.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// The town/city.
        /// </summary>
        public string TownCity { get; set; }

        /// <summary>
        /// The county.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// The postcode.
        /// </summary>
        public string PostCode { get; set; }
    }
}
