using ITMS.Business.Helpers;
using ITMS.Business.Logger;
using ITMS.Database.Domain.Common;
using NLog;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ITMS.Database.Domain
{
    public partial class DBHelper
    {
        public NpgHelper npghelper { get; set; }

        public static string connectionString = "";
        //Convert.ToString(ConfigurationManager.ConnectionStrings["RTLSRedisString"]);
        //Since this class provides only static methods, make the default constructor private to prevent 
        //instances from being created with "new DBHelper()".
        public DBHelper(string _connectionString)
        {
            connectionString = _connectionString;
            npghelper = new NpgHelper();
        }
        public sealed class NpgHelper
        {
            #region Execute DataReader without Result format
            public object ExecuteScalar(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
            {
                object returnValue = null;
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameterList != null && parameterList.Any())
                            {
                                foreach (NpgsqlParameter param in parameterList)
                                {
                                    command.Parameters.Add(param);
                                }
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            returnValue = command.ExecuteScalar();
                            tran.Commit();
                        }
                    }
                    return returnValue;
                }
                catch (Exception ex)
                {
                    clsLogger.WriteLog("NpgHelpoer found error while executing method ExecuteScalar", ex, LogLevel.Error, 0);
                    throw ex;
                }
            }
            public object ExecuteScalar(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
            {
                object returnValue = null;
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameter != null)
                            {
                                command.Parameters.Add(parameter);
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            returnValue = command.ExecuteScalar();
                            tran.Commit();
                        }
                    }
                    return returnValue;
                }
                catch (Exception ex)
                {
                    clsLogger.WriteLog("NpgHelpoer found error while executing method ExecuteScalar", ex, LogLevel.Error, 0);
                    throw ex;
                }
            }
            public DataSet ExecuteDataSet(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
            {
                DataSet resultList = new DataSet();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameterList != null && parameterList.Any())
                            {
                                foreach (NpgsqlParameter param in parameterList)
                                {
                                    command.Parameters.Add(param);
                                }
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            string cursorName = (string)command.ExecuteScalar();

                            resultList = FetchResultFromCursorDataset(cursorName, conn);
                            tran.Commit();
                        }
                    }
                    return resultList;
                }
                catch (Exception ex)
                {
                    clsLogger.WriteLog("NpgHelpoer found error while executing method ExecuteDataSet", ex, LogLevel.Error, 0);
                    throw ex;
                }
            }
            public DataTable ExecuteDataTable(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
            {
                DataTable resultList = new DataTable();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameterList != null && parameterList.Any())
                            {
                                foreach (NpgsqlParameter param in parameterList)
                                {
                                    command.Parameters.Add(param);
                                }
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;


                            string cursorName = (string)command.ExecuteScalar();

                            resultList = FetchResultFromCursorDataTable(cursorName, conn);
                            tran.Commit();
                        }
                    }
                    return resultList;
                }
                catch (Exception ex)
                {
                    clsLogger.WriteLog("NpgHelpoer found error while executing method ExecuteDataTable", ex, LogLevel.Error, 0);
                    throw ex;
                }
            }
            public DataSet ExecuteDataSet(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
            {
                DataSet resultList = new DataSet();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameter != null)
                            {
                                command.Parameters.Add(parameter);
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            string cursorName = (string)command.ExecuteScalar();

                            resultList = FetchResultFromCursorDataset(cursorName, conn);
                            tran.Commit();
                        }
                    }
                    return resultList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public DataTable ExecuteDataTable(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
            {
                DataTable resultList = new DataTable();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameter != null)
                            {
                                command.Parameters.Add(parameter);
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            string cursorName = (string)command.ExecuteScalar();

                            resultList = FetchResultFromCursorDataTable(cursorName, conn);
                            tran.Commit();
                        }
                    }
                    return resultList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public DataTable FetchResultFromCursorDataTable(string cursorName, NpgsqlConnection connection)
            {
                DataTable dtResult = new DataTable();
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "fetch all in \"" + cursorName + "\"";
                        command.CommandType = CommandType.Text;

                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                        adapter.Fill(dtResult);
                    }
                }
                catch
                {

                }
                return dtResult;

            }

            #region CloseConnection
            public static void closeConnection(NpgsqlConnection cn, NpgsqlCommand cmd)
            {
                if (cn != null)
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
                cmd.Dispose();
            }
            #endregion
            public DataSet FetchResultFromCursorDataset(string cursorName, NpgsqlConnection connection)
            {
                DataSet dsResult = new DataSet();

                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "fetch all in \"" + cursorName + "\"";
                    command.CommandType = CommandType.Text;

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        adapter.Fill(dsResult);
                    }
                }
                return dsResult;
            }
            public NpgsqlParameter PrepareCommand(string parameterName, NpgsqlTypes.NpgsqlDbType parameterType, ParameterDirection direction, object value)
            {
                NpgsqlParameter parameter = new NpgsqlParameter(parameterName, parameterType);
                parameter.Direction = direction;
                parameter.Value = value;
                return parameter;
            }
            #endregion

            #region Execute DataReader with Result format

            public Result<object> ExecuteScalarResult(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
            {
                Result<object> resultList = new Result<object>()
                {
                    data = new object()
                };
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameterList != null && parameterList.Any())
                            {
                                foreach (NpgsqlParameter param in parameterList)
                                {
                                    command.Parameters.Add(param);
                                }
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            var returnValue = command.ExecuteScalar();
                            tran.Commit();

                            resultList.data = returnValue;
                        }
                    }
                    resultList.issuccess = true;
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                }
                return resultList;
            }
            public Result<object> ExecuteScalarResult(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
            {
                Result<object> resultList = new Result<object>()
                {
                    data = new object()
                };
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameter != null)
                            {
                                command.Parameters.Add(parameter);
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            var returnValue = command.ExecuteScalar();
                            tran.Commit();
                            resultList.data = returnValue;
                        }
                    }
                    resultList.issuccess = true;
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                }
                return resultList;
            }
            public Result<DataSet> ExecuteDataSetResult(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
            {
                Result<DataSet> resultList = new Result<DataSet>();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameterList != null && parameterList.Any())
                            {
                                foreach (NpgsqlParameter param in parameterList)
                                {
                                    command.Parameters.Add(param);
                                }
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            var cursorName = command.ExecuteScalar();

                            resultList = FetchResultFromCursorDatasetResult(cursorName.ToString(), conn);
                            tran.Commit();
                        }
                    }
                    resultList.issuccess = true;
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                }
                return resultList;
            }
            public Result<DataTable> ExecuteDataTableResult(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
            {
                Result<DataTable> resultList = new Result<DataTable>();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {   
                            try
                            {
                                NpgsqlTransaction tran = conn.BeginTransaction();

                                command.CommandText = queryText;
                                command.CommandTimeout = timeOut;
                                command.Connection = conn;
                                if (parameterList != null && parameterList.Any())
                                {
                                    foreach (NpgsqlParameter param in parameterList)
                                    {
                                        command.Parameters.Add(param);
                                    }
                                }
                                command.CommandType = CommandType.StoredProcedure;
                                command.Transaction = tran;

                                var cursorName = command.ExecuteScalar();

                                resultList = FetchResultFromCursorDataTableResult(cursorName.ToString(), conn);
                                tran.Commit();

                                resultList.issuccess = true;
                            }
                            catch (Exception ex)
                            {
                                resultList.exception = ex;
                                resultList.issuccess = false;
                                resultList.message = ex.Message;
                                clsLogger.WriteLog("NpgHelpoer found error while executing method ExecuteDataTableResult", ex, LogLevel.Error, 10);
                            }
                            finally
                            {
                                closeConnection(conn, command);
                            }
                        }
                    }
                  
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                    clsLogger.WriteLog("NpgHelpoer found error while executing method ExecuteDataTableResult", ex, LogLevel.Error, 10);

                }
                return resultList;
            }
            public Result<DataTable> ExecuteDataTableResultCustom(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
            {
                Result<DataTable> resultList = new Result<DataTable>();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameterList != null && parameterList.Any())
                            {
                                foreach (NpgsqlParameter param in parameterList)
                                {
                                    command.Parameters.Add(param);
                                }
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            resultList = FetchResultFromCursorDataTableResultCustom(command);
                            tran.Commit();
                            resultList.issuccess = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                    clsLogger.WriteLog("NpgHelpoer found error while executing method ExecuteDataTableResult", ex, LogLevel.Error, 10);

                }
                return resultList;
            }
            public Result<DataSet> ExecuteDataSetResult(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
            {
                Result<DataSet> resultList = new Result<DataSet>();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameter != null)
                            {
                                command.Parameters.Add(parameter);
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            var cursorName = command.ExecuteScalar();

                            resultList = FetchResultFromCursorDatasetResult(cursorName.ToString(), conn);
                            tran.Commit();
                        }
                    }
                    resultList.issuccess = true;
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                }
                return resultList;
            }
            public Result<DataTable> ExecuteDataTableResult(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
            {
                Result<DataTable> resultList = new Result<DataTable>();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameter != null)
                            {
                                command.Parameters.Add(parameter);
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            var cursorName = command.ExecuteScalar();

                            resultList = FetchResultFromCursorDataTableResult(cursorName.ToString(), conn);
                            tran.Commit();
                        }
                    }
                    resultList.issuccess = true;
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                }
                return resultList;
            }

            public Result<DataTable> FetchResultFromCursorDataTableResult(string cursorName, NpgsqlConnection connection)
            {
                Result<DataTable> dtResult = new Result<DataTable>
                {
                    data = new DataTable()
                };

                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "fetch all in \"" + cursorName + "\"";
                    command.CommandType = CommandType.Text;

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    adapter.Fill(dtResult.data);

                }
                if (dtResult.data != null && dtResult.data.Rows.Count > 0)
                {
                    dtResult.issuccess = true;
                    dtResult.rowcount = dtResult.data.Rows.Count;
                }
                return dtResult;
            }
            public Result<DataTable> FetchResultFromCursorDataTableResultCustom(NpgsqlCommand command)
            {
                Result<DataTable> dtResult = new Result<DataTable>
                {
                    data = new DataTable()
                };

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                adapter.Fill(dtResult.data);

                return dtResult;
            }
            public Result<DataSet> FetchResultFromCursorDatasetResult(string cursorName, NpgsqlConnection connection)
            {
                Result<DataSet> dsResult = new Result<DataSet>
                {
                    data = new DataSet()
                };

                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "fetch all in \"" + cursorName + "\"";
                    command.CommandType = CommandType.Text;

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        adapter.Fill(dsResult.data);
                    }
                }
                if (dsResult.data != null && dsResult.data.Tables[0].Rows.Count > 0)
                {
                    dsResult.issuccess = true;
                    dsResult.rowcount = dsResult.data.Tables[0].Rows.Count;
                }
                return dsResult;
            }
            public Result<NpgsqlParameter> PrepareCommandResult(string parameterName, NpgsqlTypes.NpgsqlDbType parameterType, ParameterDirection direction, object value)
            {
                Result<NpgsqlParameter> parameter = new Result<NpgsqlParameter>
                {
                    data = new NpgsqlParameter()
                };
                parameter.data = new NpgsqlParameter(parameterName, parameterType);
                parameter.data.Direction = direction;
                parameter.data.Value = value;
                return parameter;

            }
            #endregion


            #region Execute DataReader Async & Result

            public async Task<Result<object>> ExecuteScalarAsync(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
            {
                Result<object> resultList = new Result<object>()
                {
                    data = new object()
                };
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            await conn.OpenAsync();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameterList != null && parameterList.Any())
                            {
                                foreach (NpgsqlParameter param in parameterList)
                                {
                                    command.Parameters.Add(param);
                                }
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            var returnValue = await command.ExecuteScalarAsync();
                            tran.Commit();

                            resultList.data = returnValue;
                        }
                    }
                    resultList.issuccess = true;
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                }
                return resultList;
            }
            public async Task<Result<object>> ExecuteScalarAsync(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
            {
                Result<object> resultList = new Result<object>()
                {
                    data = new object()
                };
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            await conn.OpenAsync();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameter != null)
                            {
                                command.Parameters.Add(parameter);
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            var returnValue = await command.ExecuteScalarAsync();
                            tran.Commit();
                            resultList.data = returnValue;
                        }
                    }
                    resultList.issuccess = true;
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                }
                return resultList;
            }
            public async Task<Result<DataSet>> ExecuteDataSetAsync(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
            {
                Result<DataSet> resultList = new Result<DataSet>();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            await conn.OpenAsync();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameterList != null && parameterList.Any())
                            {
                                foreach (NpgsqlParameter param in parameterList)
                                {
                                    command.Parameters.Add(param);
                                }
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            var cursorName = await command.ExecuteScalarAsync();

                            resultList = await FetchResultFromCursorDatasetAsync(cursorName.ToString(), conn);
                            tran.Commit();
                        }
                    }
                    resultList.issuccess = true;
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                }
                return resultList;
            }
            public async Task<Result<DataTable>> ExecuteDataTableAsync(string queryText, List<NpgsqlParameter> parameterList, int timeOut = 3000)
            {
                Result<DataTable> resultList = new Result<DataTable>();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            await conn.OpenAsync();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameterList != null && parameterList.Any())
                            {
                                foreach (NpgsqlParameter param in parameterList)
                                {
                                    command.Parameters.Add(param);
                                }
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            var cursorName = await command.ExecuteScalarAsync();

                            resultList = await FetchResultFromCursorDataTableAsync(cursorName.ToString(), conn);
                            tran.Commit();
                        }
                    }
                    resultList.issuccess = true;
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                }
                return resultList;
            }
            public async Task<Result<DataSet>> ExecuteDataSetAsync(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
            {
                Result<DataSet> resultList = new Result<DataSet>();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            await conn.OpenAsync();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameter != null)
                            {
                                command.Parameters.Add(parameter);
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            var cursorName = await command.ExecuteScalarAsync();

                            resultList = await FetchResultFromCursorDatasetAsync(cursorName.ToString(), conn);
                            tran.Commit();
                        }
                    }
                    resultList.issuccess = true;
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                }
                return resultList;
            }
            public async Task<Result<DataTable>> ExecuteDataTableAsync(string queryText, NpgsqlParameter parameter, int timeOut = 3000)
            {
                Result<DataTable> resultList = new Result<DataTable>();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            await conn.OpenAsync();
                        }
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            NpgsqlTransaction tran = conn.BeginTransaction();

                            command.CommandText = queryText;
                            command.CommandTimeout = timeOut;
                            command.Connection = conn;
                            if (parameter != null)
                            {
                                command.Parameters.Add(parameter);
                            }
                            command.CommandType = CommandType.StoredProcedure;
                            command.Transaction = tran;

                            var cursorName = await command.ExecuteScalarAsync();

                            resultList = await FetchResultFromCursorDataTableAsync(cursorName.ToString(), conn);
                            tran.Commit();
                        }
                    }
                    resultList.issuccess = true;
                }
                catch (Exception ex)
                {
                    resultList.exception = ex;
                    resultList.issuccess = false;
                    resultList.message = ex.Message;
                }
                return resultList;
            }

            public async Task<Result<DataTable>> FetchResultFromCursorDataTableAsync(string cursorName, NpgsqlConnection connection)
            {
                Result<DataTable> dtResult = new Result<DataTable>
                {
                    data = new DataTable()
                };

                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "fetch all in \"" + cursorName + "\"";
                    command.CommandType = CommandType.Text;

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    await Task.Run(() => adapter.Fill(dtResult.data));

                }
                if (dtResult.data != null && dtResult.data.Rows.Count > 0)
                {
                    dtResult.issuccess = true;
                    dtResult.rowcount = dtResult.data.Rows.Count;
                }
                return dtResult;
            }
            public async Task<Result<DataSet>> FetchResultFromCursorDatasetAsync(string cursorName, NpgsqlConnection connection)
            {
                Result<DataSet> dsResult = new Result<DataSet>
                {
                    data = new DataSet()
                };

                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "fetch all in \"" + cursorName + "\"";
                    command.CommandType = CommandType.Text;

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        await Task.Run(() => adapter.Fill(dsResult.data));
                    }
                }
                if (dsResult.data != null && dsResult.data.Tables[0].Rows.Count > 0)
                {
                    dsResult.issuccess = true;
                    dsResult.rowcount = dsResult.data.Tables[0].Rows.Count;
                }
                return dsResult;
            }
            public Result<NpgsqlParameter> PrepareCommandAsync(string parameterName, NpgsqlTypes.NpgsqlDbType parameterType, ParameterDirection direction, object value)
            {
                Result<NpgsqlParameter> parameter = new Result<NpgsqlParameter>
                {
                    data = new NpgsqlParameter()
                };
                parameter.data = new NpgsqlParameter(parameterName, parameterType);
                parameter.data.Direction = direction;
                parameter.data.Value = value;
                return parameter;

            }
            #endregion

            [Obsolete]
            public DataTable BulkInsert<T>(string queryText, List<T> dataList, string CompositeTypeName, string parametername, List<NpgsqlParameter> parameter) where T : new()
            {
                DataTable dtCursor = new DataTable();
                DataTable dtResult = new DataTable();
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            try
                            {
                                if (conn.State == ConnectionState.Closed)
                                {
                                    conn.Open();
                                    conn.MapComposite<T>(CompositeTypeName);
                                }
                                NpgsqlTransaction tran = conn.BeginTransaction();
                                command.CommandText = queryText;
                                command.Connection = conn;
                                command.CommandType = CommandType.StoredProcedure;
                                command.Transaction = tran;
                                command.Parameters.AddWithValue(parametername, dataList);

                                //command.ExecuteNonQuery();
                                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                                adapter.Fill(dtCursor);

                                dtResult = FetchResultFromCursorDataTable(dtCursor.Rows[0][0].ToString(), conn);
                                tran.Commit();
                            }
                            catch
                            {

                            }
                            finally
                            {
                                closeConnection(conn, command);
                            }

                        }

                    }
                    return dtResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
    }
}
