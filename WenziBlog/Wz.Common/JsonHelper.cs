using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Collections.Generic;

/// <summary>
/// JSON序列化和反序列化辅助类
/// </summary>
public class JsonHelper
{
    public JsonHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// JSON序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <returns></returns>
    public static string JsonSerializer<T>(T t)
    {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        MemoryStream ms = new MemoryStream();
        ser.WriteObject(ms, t);
        string jsonString = System.Text.Encoding.UTF8.GetString(ms.ToArray());
        ms.Close();
        return jsonString;
    }

    /// <summary>
    /// JSON反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static T JsonDeserializer<T>(string jsonString)
    {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString));
        T obj = (T)ser.ReadObject(ms);
        return obj;
    }

    /// <summary>
    /// JSON序列化(包含日期格式转换)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <returns></returns>
    public static string JsonSerializer<T>(T t, bool bdate)
    {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        MemoryStream ms = new MemoryStream();
        ser.WriteObject(ms, t);
        string jsonString = System.Text.Encoding.UTF8.GetString(ms.ToArray());
        ms.Close();

        //替换Json的Date字符串
        string p = @"\\/Date\((\d+)\+\d+\)\\/";
        MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
        Regex reg = new Regex(p);
        jsonString = reg.Replace(jsonString, matchEvaluator);
        return jsonString;
    }

    /// <summary>
    /// JSON反序列化(包含日期格式转换)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static T JsonDeserializer<T>(string jsonString, bool bdate)
    {
        //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式
        string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
        MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
        Regex reg = new Regex(p);
        jsonString = reg.Replace(jsonString, matchEvaluator);

        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString));
        T obj = (T)ser.ReadObject(ms);
        return obj;
    }

    /// <summary>
    /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    private static string ConvertJsonDateToDateString(Match m)
    {
        string result = string.Empty;
        DateTime dt = new DateTime(1970, 1, 1);
        dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
        dt = dt.ToLocalTime();
        result = dt.ToString("yyyy-MM-dd HH:mm:ss");
        return result;
    }

    /// <summary>
    /// 将时间字符串转为Json时间
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    private static string ConvertDateStringToJsonDate(Match m)
    {
        string result = string.Empty;
        DateTime dt = DateTime.Parse(m.Groups[0].Value);
        dt = dt.ToUniversalTime();
        TimeSpan ts = dt - DateTime.Parse("1970-01-01");
        result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
        return result;
    }

    /// <summary>
    /// 将对象转换为Json
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns>Json格式字符串</returns>
    public static string ObjectToJson(object obj)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        try
        {
            return jss.Serialize(obj);
        }
        catch (Exception ex)
        {
            throw new Exception("JSonHelp.objectToJson();" + ex.Message);
        }
    }
    /// <summary>
    /// 数据表转键值对集合
    /// 将DataTable转成List集合，存每一行
    /// </summary>
    /// <param name="dt">数据表</param>
    /// <returns>哈希表数组</returns>
    public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        foreach (DataRow dr in dt.Rows)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (DataColumn dc in dt.Columns)
            {
                dic.Add(dc.ColumnName, dr[dc.ColumnName]);
            }
            list.Add(dic);
        }
        return list;
    }

    /// <summary>
    /// 数据集转键值对数组字典
    /// </summary>
    /// <param name="ds">数据集</param>
    /// <returns>键值对数组字典</returns>
    public static Dictionary<string, List<Dictionary<string, object>>> DataSetToDic(DataSet ds)
    {
        Dictionary<string, List<Dictionary<string, object>>> result = new Dictionary<string, List<Dictionary<string, object>>>();
        foreach (DataTable dt in ds.Tables)
        {
            result.Add(dt.TableName, DataTableToList(dt));

        }
        return result;
    }
    /// <summary>
    /// 数据表转JSON
    /// </summary>
    /// <param name="dt">数据表</param>
    /// <returns>JSON字符串</returns>
    public static string DataTableToJson(DataTable dt)
    {
        return ObjectToJson(DataTableToList(dt));
    }
    /// <summary>
    /// 将Json字符串转换为对象泛型方法
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="JsonText">Json字符串</param>
    /// <returns>制定类型对象</returns>
    public static T JsonToObject<T>(string JsonText)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        try
        {
            return jss.Deserialize<T>(JsonText);
        }
        catch (Exception ex)
        {
            throw new Exception("JsonHelp.JsonToObject():" + ex.Message);
        }
    }
    /// <summary>
    /// 将Json字符串转换为数据表数据
    /// </summary>
    /// <param name="JsonText">Json文本</param>
    /// <returns>数据表字典</returns>
    public static Dictionary<string, List<Dictionary<string, object>>> TablesDataFormJson(string JsonText)
    {
        return JsonToObject<Dictionary<string, List<Dictionary<string, object>>>>(JsonText);
    }
    /// <summary>
    /// 将json文本转换成数据行
    /// </summary>
    /// <param name="JsonText">Json文本</param>
    /// <returns>数据行的字典</returns>
    public static Dictionary<string, object> DataRowFormJson(string JsonText)
    {
        return JsonToObject<Dictionary<string, object>>(JsonText);
    }


    public static DataTable JsonToDataTable(string strJson, string a)
    {
        //取出表名  
        var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
        string strName = rg.Match(strJson).Value;
        DataTable tb = null;
        //去除表名  
        strJson = strJson.Substring(strJson.IndexOf("[") + 1);
        strJson = strJson.Substring(0, strJson.IndexOf("]"));

        //获取数据  
        rg = new Regex(@"(?<={)[^}]+(?=})");
        MatchCollection mc = rg.Matches(strJson);
        for (int i = 0; i < mc.Count; i++)
        {
            string strRow = mc[i].Value;
            string[] strRows = strRow.Split(',');

            //创建表  
            if (tb == null)
            {
                tb = new DataTable();
                tb.TableName = strName;
                foreach (string str in strRows)
                {
                    DataColumn dc = new DataColumn();
                    string[] strCell = str.Split(':');
                    dc.ColumnName = strCell[0].ToString();
                    tb.Columns.Add(dc);
                }
                tb.AcceptChanges();
            }

            //增加内容  
            DataRow dr = tb.NewRow();
            for (int r = 0; r < strRows.Length; r++)
            {
                dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");

            }
            tb.Rows.Add(dr);
            tb.AcceptChanges();
        }

        return tb;
    }
    public static DataTable JsonToDataTableNoFix(string strJson)
    {
        //取出表名               
        var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
        string strName = rg.Match(strJson).Value; DataTable tb = null;
        //去除表名              
        strJson = strJson.Substring(strJson.IndexOf("[") + 1);
        strJson = strJson.Substring(0, strJson.IndexOf("]"));
        //获取数据              
        rg = new Regex(@"(?<={)[^}]+(?=})");
        MatchCollection mc = rg.Matches(strJson);
        for (int i = 0; i < mc.Count; i++)
        {
            string strRow = mc[i].Value;
            string[] strRows = strRow.Split(',');
            //创建表                 
            if (tb == null)
            {
                tb = new DataTable();
                tb.TableName = strName;
                foreach (string str in strRows)
                {
                    var dc = new DataColumn();
                    string[] strCell = str.Split(':');
                    dc.ColumnName = strCell[0].Replace("\"", "");
                    tb.Columns.Add(dc);
                }
                tb.AcceptChanges();
            }
            //增加内容          
            DataRow dr = tb.NewRow();
            for (int r = 0; r < strRows.Length; r++)
            {
                dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
            }
            tb.Rows.Add(dr);
            tb.AcceptChanges();
        }
        return tb;
    }

    public static DataTable JsonToDataTable(string strJson)
    {
        //取出表名               
        var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
        string strName = rg.Match(strJson).Value; DataTable tb = null;
        //去除表名              
        strJson = strJson.Substring(strJson.IndexOf("[") + 1);
        strJson = strJson.Substring(0, strJson.IndexOf("]"));
        //获取数据              
        rg = new Regex(@"(?<={)[^}]+(?=})");
        MatchCollection mc = rg.Matches(strJson);
        for (int i = 0; i < mc.Count; i++)
        {
            string strRow = mc[i].Value;
            string[] strRows = strRow.Split(',');
            //创建表                 
            if (tb == null)
            {
                tb = new DataTable();
                tb.TableName = strName;
                foreach (string str in strRows)
                {
                    var dc = new DataColumn();
                    string[] strCell = str.Split(':');
                    dc.ColumnName = strCell[0];
                    tb.Columns.Add(dc);
                }
                tb.AcceptChanges();
            }
            //增加内容          
            DataRow dr = tb.NewRow();
            for (int r = 0; r < strRows.Length; r++)
            {
                dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
            }
            tb.Rows.Add(dr);
            tb.AcceptChanges();
        }
        return tb;
    }


    /// <summary> 
    /// DataSet转换为Json 
    /// </summary> 
    /// <param name="dataSet">DataSet对象</param> 
    /// <returns>Json字符串</returns> 
    public static string DataSetToJson(DataSet dataSet)
    {
        string jsonString = "{";
        foreach (DataTable table in dataSet.Tables)
        {
            jsonString += "\"" + table.TableName + "\":" + DataTableToJson(table) + ",";
        }
        jsonString = jsonString.TrimEnd(',');
        return jsonString + "}";
    }

    
}
