namespace ApprenticeWebAPI.Models.Dto
{
    /// <summary>
    /// The address request Dto.
    /// </summary>
    public class AddressRequestDto
    {
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
