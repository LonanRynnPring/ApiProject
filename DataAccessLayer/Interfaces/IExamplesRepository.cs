namespace ApprenticeWebAPI.DataAccessLayer.Interfaces
{
    /// <summary>
    /// Interface for the examples repository.
    /// </summary>
    public interface IExamplesRepository
    {
        /// <summary>
        /// Method for creating an example.
        /// </summary>
        /// <param name="value">The example value.</param>
        /// <returns>The Id of the new example.</returns>
        int CreateExample(string value);
    }
}
