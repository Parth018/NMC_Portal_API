using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ITMS.Web.Models;
using Npgsql;
using System.Data;

namespace ITMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //use for user authentication
      //public IActionResult Privacy()
        //{
          //  return View();
        //}


        [HttpGet]
        public IActionResult GetDatabaseList(string machineName)
        {
            var model = new QueryBuilderModel
            {
                DataBaseList = new List<string>()
            };
            if (!string.IsNullOrEmpty(machineName))
            {
                try
                {
                    string ConString = "Server=" + machineName + ";Port=2433;User Id=postgres;Password=AmneX#2$@AM!;pooling=false";
                    using (var con1 = new NpgsqlConnection(ConString))
                    {
                        con1.Open();
                        DataTable databases = con1.GetSchema("Databases");
                        foreach (DataRow database in databases.Rows)
                        {
                            String databaseName = database.Field<String>("database_name");
                            model.DataBaseList.Add(databaseName.ToString());                            
                        }
                        con1.Close();
                    }
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    model.Message = "Problem connecting server. Please Check machine name of data base connection";
                    return BadRequest(model);
                }
            }
            model.Message = "Please provide valid Machine Name";
            return BadRequest(model);            
        }

        [HttpGet]
        public IActionResult GetTableList(string machineName, string databaseName)
        {
            var model = new QueryBuilderModel
            {
                TableList = new List<string>()
            };
            if (!string.IsNullOrEmpty(machineName) && !string.IsNullOrEmpty(databaseName))
            {
                try
                {
                    string ConString = "Server=" + machineName + ";Port=2433;Database=" + databaseName + ";User Id=postgres;Password=AmneX#2$@AM!;pooling=false;";

                    NpgsqlConnection con = new NpgsqlConnection(ConString);
                    NpgsqlCommand command = con.CreateCommand();
                    con.Open();
                    DataTable dtable = con.GetSchema("Tables");
                    foreach (DataRow row in dtable.Rows)
                    {
                        string tablename = (string)row[2];
                        model.TableList.Add(tablename);
                    }
                    con.Close();
                    model.TableList = model.TableList.Distinct().OrderBy(x => x).ToList();
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    model.Message = "Problem connecting server. Please Check machine name of data base connection";
                    return BadRequest(model);
                }
            }
            model.Message = "Please provide valid Machine Name/Database Name";
            return BadRequest(model);
        }

        [HttpGet]
        public IActionResult GetColumnList(string machineName, string databaseName, string tableName)
        {
            var model = new QueryBuilderModel
            {
                ColumnList = new List<string>()
            };
            if (!string.IsNullOrEmpty(machineName) && !string.IsNullOrEmpty(databaseName) && !string.IsNullOrEmpty(tableName))
            {
                try
                {
                    string ConString = "Server=" + machineName + ";Port=2433;Database=" + databaseName + ";User Id=postgres;Password=AmneX#2$@AM!;pooling=false;";

                    string sqlQuery = "Select * from " + tableName;
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery);
                    NpgsqlConnection sqlConn = new NpgsqlConnection(ConString);
                    sqlConn.Open();
                    command.Connection = sqlConn;
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataColumn column in dt.Columns)
                    {
                        model.ColumnList.Add(column.ColumnName);                    
                    }

                    return Ok(model);
                }
                catch (Exception ex)
                {
                    model.Message = "Problem connecting server. Please Check machine name of data base connection";
                    return BadRequest(model);
                }
            }
            model.Message = "Please provide valid Machine Name/Database Name";
            return BadRequest(model);
        }

        [HttpGet]
        public IActionResult GenerateQuery(string tableName, string columns, string groupBy, string orderBy)
        {
            var model = new QueryBuilderModel();
            string query = "";

            if (!string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(columns))
            {
                query = "SELECT " + columns + " FROM " + tableName;

                if (!string.IsNullOrEmpty(groupBy))
                {
                    query = query + " GROUP BY " + groupBy;
                }
                if (!string.IsNullOrEmpty(orderBy))
                {
                    query = query + " ORDER BY " + orderBy;
                }
                model.Query = query;
                return Ok(model);
            }
                
            model.Message = "Please select columns";
            return BadRequest(model);
        }

        [HttpGet]
        public IActionResult Result(string machineName, string databaseName, string query)
        {
            var model = new QueryBuilderModel
            {
                TableList = new List<string>()
            };
            if (!string.IsNullOrEmpty(machineName) && !string.IsNullOrEmpty(databaseName) && !string.IsNullOrEmpty(query))
            {
                NpgsqlConnection sqlConn;
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(query);

                    string ConString = "Server=" + machineName + ";Port=2433;Database=" + databaseName + ";User Id=postgres;Password=AmneX#2$@AM!;pooling=false;";
                    sqlConn = new NpgsqlConnection(ConString);
                    sqlConn.Open();
                    command.Connection = sqlConn;
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);                    
                    sqlConn.Close();

                    return View("Result", dataTable);
                }
                catch (Exception ex)
                {
                    model.Message = "Problem connecting server. Please Check machine name of data base connection";
                    return BadRequest(model);
                }
            }
            model.Message = "Please provide valid Machine Name/Database Name/Query";
            return BadRequest(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
