using ApprenticeWebAPI.DataAccessLayer.Interfaces;
using ApprenticeWebAPI.Utility.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ApprenticeWebAPI.DataAccessLayer
{
    /// <summary>
    /// Class for the example repository implementation.
    /// </summary>
    public class ExamplesRepository : IExamplesRepository
    {
        /// <summary>
        /// IDataHelper interface.
        /// </summary>
        private readonly IDataHelper _dataHelper;

        /// <summary>
        /// The configuration
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="dataHelper">The data helper.</param>
        /// <param name="configuration">The configuration.</param>
        public ExamplesRepository(IDataHelper dataHelper, IConfiguration configuration)
        {
            _dataHelper = dataHelper;
            Configuration = configuration;
        }

        /// <inheritdoc />
        public int CreateExample(string value)
        {
            var exampleId = 0;
            _dataHelper.Execute(
                (c) => exampleId = (int)c,
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.CREATE_EXAMPLE),
                _dataHelper.BindParameters(new Dictionary<string, object>
                {
                    {"@Value", value }
                }));

            return exampleId;
        }

        #region Auxiliaries

        private class StoredProcedures
        {
            public const string CREATE_EXAMPLE = "[dbo].[CreateExample]";
        }

        #endregion Auxiliaries
    }
}
