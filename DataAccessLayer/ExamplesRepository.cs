using ApprenticeWebAPI.DataAccessLayer.Interfaces;
using ApprenticeWebAPI.Models.Dto;
using ApprenticeWebAPI.Utility.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

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
                    { "@Value", value }
                }));

            return exampleId;
        }

        /// <inheritdoc />
        public IList<ExampleResponseDto> GetExamples()
        {
            DataTable dtExamples = _dataHelper.Execute<DataTable>(
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.GET_EXAMPLES));

            List<ExampleResponseDto> examples = new List<ExampleResponseDto>();

            foreach (DataRow drExample in dtExamples.Rows)
            {
                examples.Add(new ExampleResponseDto 
                {
                    ExampleId = drExample.Field<int>("ExampleId"),
                    Value = drExample.Field<string>("Value")
                });
            }

            return examples;
        }

        /// <inheritdoc />
        public ExampleResponseDto GetExampleById(int exampleId)
        {
            DataTable dtExamples = _dataHelper.Execute<DataTable>(
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.GET_EXAMPLE_BY_ID),
                _dataHelper.BindParameters(new Dictionary<string, object>
                {
                    { "@ExampleId", exampleId }
                }));

            ExampleResponseDto example = default(ExampleResponseDto);

            if (dtExamples.Rows.Count == 1)
            {
                DataRow drExample = dtExamples.Rows[0];
                example = new ExampleResponseDto
                {
                    ExampleId = drExample.Field<int>("ExampleId"),
                    Value = drExample.Field<string>("Value")
                };
            }

            return example;
        }

        /// <inheritdoc />
        public void UpdateExample(int exampleId, string value)
        {
            _dataHelper.Execute(
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.UPDATE_EXAMPLE),
                _dataHelper.BindParameters(new Dictionary<string, object>
                {
                    { "@ExampleId", exampleId },
                    { "@Value", value }
                }));
        }

        /// <inheritdoc />
        public void DeleteExample(int exampleId)
        {
            _dataHelper.Execute(
                _dataHelper.BindDb(Configuration["ConnectionStrings:DefaultConnection"]),
                _dataHelper.StoredProc(StoredProcedures.DELETE_EXAMPLE),
                _dataHelper.BindParameters(new Dictionary<string, object>
                {
                    { "@ExampleId", exampleId }
                }));
        }

        #region Auxiliaries

        private class StoredProcedures
        {
            public const string CREATE_EXAMPLE = "[dbo].[CreateExample]";
            public const string GET_EXAMPLES = "[dbo].[GetExamples]";
            public const string GET_EXAMPLE_BY_ID = "[dbo].[GetExampleById]";
            public const string UPDATE_EXAMPLE = "[dbo].[UpdateExample]";
            public const string DELETE_EXAMPLE = "[dbo].[DeleteExample]";
        }

        #endregion Auxiliaries
    }
}
