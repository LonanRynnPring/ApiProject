using ApprenticeWebAPI.Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ApprenticeWebAPI.Utility
{
    /// <summary>
    /// Copy of the MPP Core class for the data helper implementation.
    /// </summary>
    public class DataHelper : IDataHelper
    {
        /// <inheritdoc />
        public int Execute(params Action<SqlCommand>[] commandSequence)
        {
            using (var connection = new SqlConnection())
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = connection;

                foreach (var commandItem in commandSequence)
                {
                    commandItem(cmd);
                }

                return cmd.ExecuteNonQuery();
            }
        }

        /// <inheritdoc />
        public int Execute(Action<object> returnValue, params Action<SqlCommand>[] commandSequence)
        {
            using (var connection = new SqlConnection())
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                var returnValueParam = new SqlParameter { Direction = ParameterDirection.ReturnValue };
                cmd.Parameters.Add(returnValueParam);

                foreach (var commandItem in commandSequence)
                {
                    commandItem(cmd);
                }

                int nonQueryReturn = cmd.ExecuteNonQuery();
                returnValue(returnValueParam.Value);
                return nonQueryReturn;
            }
        }

        /// <inheritdoc />
        public Action<SqlCommand> BindDb(string connectionString)
        {
            return cmd =>
            {
                cmd.Connection.ConnectionString = connectionString;
                cmd.Connection.Open();
            };
        }

        /// <inheritdoc />
        public Action<SqlCommand> BindParameters(Dictionary<string, object> parameterDictionary)
        {
            return cmd => ParseParameters(cmd, parameterDictionary);
        }

        /// <inheritdoc />
        public Action<SqlCommand> StoredProc(string storedProcedureName)
        {
            return cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                cmd.CommandTimeout = 30;
            };
        }

        /// <summary>
        /// Method for parsing stored procedure parameters.
        /// </summary>
        /// <param name="cmd">The SQL command.</param>
        /// <param name="parameters">The dictionary parameters.</param>
        private static void ParseParameters(SqlCommand cmd, Dictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                return;
            }

            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
            }
        }
    }
}
