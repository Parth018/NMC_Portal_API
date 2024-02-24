using ITMS.Database.Domain.Common;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ITMS.Business.Interfaces.Repository
{
    public interface IBaseRepository
    {
        #region Execute DataReader without Result format
        DataSet ExecuteDataSet(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000);
        DataTable ExecuteDataTable(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000);
        DataSet ExecuteDataSet(string queryText, NpgsqlParameter parameter, int timeOut = 3000);
        DataTable ExecuteDataTable(string queryText, NpgsqlParameter parameter, int timeOut = 3000);
        object ExecuteScalar(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000);
        object ExecuteScalar(string queryText, NpgsqlParameter parameter, int timeOut = 3000);

        NpgsqlParameter PrepareCommand(string parameterName, NpgsqlTypes.NpgsqlDbType parameterType, ParameterDirection direction, object value);

        #endregion

        #region Execute DataReader with Result format
        Result<object> ExecuteScalarResult(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000);
        Result<object> ExecuteScalarResult(string queryText, NpgsqlParameter parameter, int timeOut = 3000);
        Result<DataSet> ExecuteDataSetResult(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000);
        Result<DataTable> ExecuteDataTableResult(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000);
        Result<DataTable> ExecuteDataTableResultCustom(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000);
        Result<DataSet> ExecuteDataSetResult(string queryText, NpgsqlParameter parameter, int timeOut = 3000);
        Result<DataTable> ExecuteDataTableResult(string queryText, NpgsqlParameter parameter, int timeOut = 3000);

        Result<NpgsqlParameter> PrepareCommandResult(string parameterName, NpgsqlTypes.NpgsqlDbType parameterType, ParameterDirection direction, object value);
        #endregion


        #region Execute DataReader Async & Result
        Task<Result<object>> ExecuteScalarAsync(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000);
        Task<Result<object>> ExecuteScalarAsync(string queryText, NpgsqlParameter parameter, int timeOut = 3000);
        Task<Result<DataSet>> ExecuteDataSetAsync(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000);
        Task<Result<DataTable>> ExecuteDataTableAsync(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000);
        Task<Result<DataSet>> ExecuteDataSetAsync(string queryText, NpgsqlParameter parameter, int timeOut = 3000);
        Task<Result<DataTable>> ExecuteDataTableAsync(string queryText, NpgsqlParameter parameter, int timeOut = 3000);
        Result<NpgsqlParameter> PrepareCommandAsync(string parameterName, NpgsqlTypes.NpgsqlDbType parameterType, ParameterDirection direction, object value);
        #endregion

        //DataTable BulkInsert<T>(string queryText, List<T> dataList, string CompositeTypeName, string parametername, List<NpgsqlParameter> parameter);
    }
}
