using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Maticsoft.Common
{
    public class Sequences
    {
        public static string GetUiqueIDs(
            /*
                入口：已打开的数据库连接，公司代码，项目代码，表名
             */
           SqlConnection  sqlConn,
           string tableName)
        {             
            SqlCommand sCmd = new SqlCommand();
            sCmd.CommandType = CommandType.StoredProcedure;
            sCmd.CommandText = "GetUiqueIDs";
            sCmd.Connection = sqlConn;
            sCmd.CommandTimeout = 5;
            sCmd.Parameters.AddWithValue("@companyCoce","" );
            sCmd.Parameters.AddWithValue("@projectCode", "");
            sCmd.Parameters.AddWithValue("@tableName", tableName);
            sCmd.Parameters.AddWithValue("@result", "");
            sCmd.Parameters["@result"].Size = 50;
            sCmd.Parameters["@result"].Direction = ParameterDirection.Output;
            try
            {
                sCmd.ExecuteNonQuery();
                return sCmd.Parameters["@result"].Value.ToString();
            }
            catch
            {
                return "";
            }             
        }
        public static string GetUiqueIDs(
            /*
                入口：数据库连接字符串，公司代码，项目代码，表名
             */
          string connString,
          string tableName)
        {
            using(SqlConnection sConn=new SqlConnection(connString) )
            {
                sConn.Open();
                return GetUiqueIDs(sConn,tableName);
                 
            }
        }


        public static string GetUniqueSequences(
           SqlConnection sqlConn)
        {
            SqlCommand sCmd = new SqlCommand();
            sCmd.CommandType = CommandType.StoredProcedure;
            sCmd.CommandText = "GetUniqueSequences";
            sCmd.Connection = sqlConn;
            sCmd.CommandTimeout = 5;       
            sCmd.Parameters.AddWithValue("@result", "");
            sCmd.Parameters["@result"].Direction = ParameterDirection.Output;
            sCmd.Parameters["@result"].Size = 50;
            try
            {
                sCmd.ExecuteNonQuery();
                return sCmd.Parameters["@result"].Value.ToString();
            }
            catch
            {
                return "";
            }
        }
        public static string GetUniqueSequences(
          string connString )
        {
            using (SqlConnection sConn = new SqlConnection(connString))
            {
                sConn.Open();
                return GetUniqueSequences(sConn);
            }
        } 
    }
}
