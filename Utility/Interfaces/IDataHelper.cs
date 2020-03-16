using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApprenticeWebAPI.Utility.Interfaces
{
    /// <summary>
    /// Copy of the MPP Core data helper.
    /// </summary>
    public interface IDataHelper
    {
        /// <summary>
        /// Executes the specified command sequence.
        /// Call this one when you need a DataSet, DataTable, an instance of T where T implements a private constructor that takes a DataRow to instantiate itself from
        /// or the affected row count. The affected row count is only returned when the query returns the affected row count as a single scalar value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandSequence">The command sequence.</param>
        /// <returns></returns>
        T Execute<T>(params Action<SqlCommand>[] commandSequence);

        /// <summary>
        /// Executes the specified command sequence. 
        /// Call this one when you need the affected row count
        /// </summary>
        /// <param name="commandSequence">The command sequence.</param>
        /// <returns></returns>
        int Execute(params Action<SqlCommand>[] commandSequence);
        
        /// <summary>
        /// Executes the specified command sequence. 
        /// Call this one when you need the affected row count
        /// </summary>
        /// <param name="returnValue">An action that is called with the return value from the executed T-SQL.</param>
        /// <param name="commandSequence">The command sequence.</param>
        /// <returns>An integer which is the result of the execution.</returns>
        int Execute(Action<object> returnValue, params Action<SqlCommand>[] commandSequence);

        /// <summary>
        /// This property defines an action that binds an arbitrary database.
        /// </summary>
        Action<SqlCommand> BindDb(string connectionString);

        /// <summary>
        /// This method defines an action that binds parameters as a dictionary.
        /// </summary>
        /// <param name="parameterDictionary">The dictionary of parameters to bind</param>
        /// <returns>A SqlCommand action.</returns>
        Action<SqlCommand> BindParameters(Dictionary<string, object> parameterDictionary);

        /// <summary>
        /// This method creates a SQL Action that binds a stored procedure to a SQL Command.
        /// </summary>
        /// <param name="storedProcedureName">The stored procedure to bind.</param>
        /// <returns>A SqlCommand action.</returns>
        Action<SqlCommand> StoredProc(string storedProcedureName);

    }
}
