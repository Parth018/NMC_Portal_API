using ITMS.Business.Common;
using ITMS.Business.Interfaces.Repository;
using ITMS.Database.Domain;
using ITMS.Database.Domain.Common;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace ITMS.Business.Services.Repository
{
    public partial class BaseRepository : IBaseRepository
    {
        //DBHelper dBHelper = new DBHelper(Convert.ToString(ConfigurationManager.ConnectionStrings["ConnectionMaster"]));
        //public DBHelper dBHelper = new DBHelper(Constant.ConnectionMaster);
        public DBHelper dBHelper = new DBHelper(Constant.siteConfiguration.ConnectionMaster); 

        #region Execute DataReader without Result format
        public DataSet ExecuteDataSet(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteDataSet(queryText, parameterList, timeOut);
        }
        public DataSet ExecuteDataSet(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteDataSet(queryText, parameter, timeOut);
        }
        public DataTable ExecuteDataTable(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteDataTable(queryText, parameterList, timeOut);
        }
        public DataTable ExecuteDataTable(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteDataTable(queryText, parameter, timeOut);
        }
        public object ExecuteScalar(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteScalar(queryText, parameter, timeOut);
        }
        public object ExecuteScalar(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteScalar(queryText, parameterList, timeOut);
        }

        public NpgsqlParameter PrepareCommand(string parameterName, NpgsqlDbType parameterType, ParameterDirection direction, object value)
        {
            return dBHelper.npghelper.PrepareCommand(parameterName, parameterType, direction, value);
        }
        #endregion
         
        #region Execute DataReader with Result format
        public Result<object> ExecuteScalarResult(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteScalarResult(queryText, parameterList, timeOut);
        }
        public Result<object> ExecuteScalarResult(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteScalarResult(queryText, parameter, timeOut);
        }
        public Result<DataSet> ExecuteDataSetResult(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteDataSetResult(queryText, parameterList, timeOut);
        }
        public Result<DataTable> ExecuteDataTableResult(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteDataTableResult(queryText, parameterList, timeOut);
        }
        public Result<DataTable> ExecuteDataTableResultCustom(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteDataTableResultCustom(queryText, parameterList, timeOut);
        }
        public Result<DataSet> ExecuteDataSetResult(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteDataSetResult(queryText, parameter, timeOut);
        }
        public Result<DataTable> ExecuteDataTableResult(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
        {
            return dBHelper.npghelper.ExecuteDataTableResult(queryText, parameter, timeOut);
        }

        public Result<NpgsqlParameter> PrepareCommandResult(string parameterName, NpgsqlDbType parameterType, ParameterDirection direction, object value)
        {
            return dBHelper.npghelper.PrepareCommandResult(parameterName, parameterType, direction, value);
        }

        #region Execute DataReader Async & Result
        public async Task<Result<object>> ExecuteScalarAsync(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
        {
            return await dBHelper.npghelper.ExecuteScalarAsync(queryText, parameter, timeOut);
        }
        public async Task<Result<object>> ExecuteScalarAsync(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
        {
            return await dBHelper.npghelper.ExecuteScalarAsync(queryText, parameterList, timeOut);
        }
        public async Task<Result<DataSet>> ExecuteDataSetAsync(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
        {
            var data = await dBHelper.npghelper.ExecuteDataSetAsync(queryText, parameterList, timeOut);
            return data;

            // throw new NotImplementedException();
        }
        public async Task<Result<DataSet>> ExecuteDataSetAsync(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
        {
            //throw new NotImplementedException();
            var data = await dBHelper.npghelper.ExecuteDataSetAsync(queryText, parameter, timeOut);
            return data;
        }
        public async Task<Result<DataTable>> ExecuteDataTableAsync(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
        {
            // throw new NotImplementedException();

            var data = await dBHelper.npghelper.ExecuteDataTableAsync(queryText, parameterList, timeOut);
            return data;
        }
        public async Task<Result<DataTable>> ExecuteDataTableAsync(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
        {
            //throw new NotImplementedException();
            //return dBHelper.npghelper.ExecuteDataTable(queryText, parameter, timeOut);

            var data = await dBHelper.npghelper.ExecuteDataTableAsync(queryText, parameter, timeOut);
            return data;

        }

        public Result<NpgsqlParameter> PrepareCommandAsync(string parameterName, NpgsqlDbType parameterType, ParameterDirection direction, object value)
        {
            return dBHelper.npghelper.PrepareCommandAsync(parameterName, parameterType, direction, value);
        }

        #endregion
        //public DataTable BulkInsert<T>(string queryText, List<T> dataList, string CompositeTypeName, string parametername, List<NpgsqlParameter> parameter)
        //{
        //    return dBHelper.npghelper.BulkInsert(queryText, dataList, CompositeTypeName, parametername, parameter);
        //}

        
        #endregion


    }
}
