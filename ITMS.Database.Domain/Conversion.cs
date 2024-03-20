using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ITMS.Database.Domain
{
    public static class Conversion
    {
        public static List<T> ConvertDataTabletoList<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            foreach (DataRow dr in dt.Rows)
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                           // prop.SetValue(obj, dr[prop.Name], null);
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
                list.Add(obj);
            }
            return list;
        }
        public static string ConvertDataTabletoJson(DataTable dt)
        {
            return JsonConvert.SerializeObject(dt);
        }
        // public static string ConvertDataTabletoJson(DataTable dt)
        //{
        //    return JsonConvert.SerializeObject(dt);
       // }
        public static T JsonToObject<T>(string  jsonValue)
        {
            //string json = "{\"plans\": [{.... ";

            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(jsonValue));
            ms.Position = 0;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            T p = (T)ser.ReadObject(ms);
            return p;
        }
        public static T ResultToModel<T>(object result)
        {
            return JsonConvert.DeserializeObject<T>(Convert.ToString(result));
        }
        public static string ConvertObjectToJSon<T>(T obj)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        public static T ConvertJSonToObject<T>(string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)serializer.ReadObject(ms);
            return obj;
        }
        private static String JsonResponseCode(string values)
        {
            //return "[{\"responsecode\":\"" + values + "\"}]";
            return "[{\"responsecode\":" + values + "}]";
            //return  JsonConvert.SerializeObject("[{ ""ResponseCode"": '"' + values + ' ""}]");
        }

        //TODO :  for return function single value in dt
        private static String GetSingValue(string values)
        {
            //return "[{\"responsecode\":\"" + values + "\"}]";
            return "[{\"responsecode\":" + values + "}]";
            //return  JsonConvert.SerializeObject("[{ ""ResponseCode"": '"' + values + ' ""}]");
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> collection, string tableName)
        {
            DataTable tbl = ToDataTable(collection);

            tbl.TableName = tableName;

            return tbl;
        }
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            //Create the columns in the DataTable
            foreach (PropertyInfo pi in pia)
            {
                dt.Columns.Add(pi.Name, pi.PropertyType);
            }

            //Populate the table

            foreach (T item in collection)
            {
                DataRow dr = dt.NewRow();

                dr.BeginEdit();

                foreach (PropertyInfo pi in pia)
                {
                    dr[pi.Name] = pi.GetValue(item, null);
                }
                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
