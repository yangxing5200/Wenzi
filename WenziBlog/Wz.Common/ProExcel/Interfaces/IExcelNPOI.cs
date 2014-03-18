using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Wz.Common.ProExcel.Interfaces
{
    public interface IExcelNPOI
    {
        /// <summary>
        /// Excel文件内容转换为DataTable
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>DataTable</returns>
        DataTable ConvertToTable(string path);

        /// <summary>
        /// Excel文件内容转换为DataTable
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="sheetIndex">表单索引</param>
        /// <returns>DataTable</returns>
        DataTable ConvertToTable(string path, int sheetIndex);

        /// <summary>
        /// Excel文件内容转换为DataTable
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="sheetIndex">表单索引</param>
        /// <param name="titleRow">列名行号，-1则不指定，由系统指定</param>
        /// <param name="captionRow">标题行号，-1则不指定，由系统指定</param>
        /// <param name="dataFrom">数据起始行号，若为-1则从0行开始</param>
        /// <returns>DataTable</returns>
        DataTable ConvertToTable(string path, int sheetIndex, int titleRow, int captionRow, int dataFrom);

        /// <summary>
        /// Excel文件内容转换为DataTable
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="sheetName">表单名称</param>
        /// <returns>DataTable</returns>
        DataTable ConvertToTable(string path, string sheetName);

        /// <summary>
        /// Excel文件内容转换为DataTable
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="sheetName">表单名称</param>
        /// <param name="titleRow">列名行号，-1则不指定，由系统指定</param>
        /// <param name="captionRow">标题行号，-1则不指定，由系统指定</param>
        /// <param name="dataFrom">数据起始行号，若为-1则从0行开始</param>
        /// <returns>DataTable</returns>
        DataTable ConvertToTable(string path, string sheetName, int titleRow, int captionRow, int dataFrom);

        /// <summary>
        /// Excel文件内容转换为指定实体类
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <returns>数据列表List</returns>
        List<T> ConvertToClassObject<T>(string path) where T : class;

        /// <summary>
        /// Excel文件内容转换为指定实体类
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="sheetIndex">表单索引</param>
        /// <returns>数据列表List</returns>
        List<T> ConvertToClassObject<T>(string path, int sheetIndex) where T : class;

        /// <summary>
        /// Excel文件内容转换为指定实体类
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="sheetIndex">表单索引</param>
        /// <param name="titleRow">列名行号，-1则不指定，由系统指定</param>
        /// <param name="captionRow">标题行号，-1则不指定，由系统指定</param>
        /// <param name="dataFrom">数据起始行号，若为-1则从0行开始</param>
        /// <returns>数据列表List</returns>
        List<T> ConvertToClassObject<T>(string path, int sheetIndex, int titleRow, int captionRow, int dataFrom) where T : class;

        /// <summary>
        /// Excel文件内容转换为指定实体类
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="sheetName">表单名称</param>
        /// <returns>数据列表List</returns>
        List<T> ConvertToClassObject<T>(string path, string sheetName) where T : class;

        /// <summary>
        /// Excel文件内容转换为指定实体类
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="sheetName">表单名称</param>
        /// <param name="titleRow">列名行号，-1则不指定，由系统指定</param>
        /// <param name="captionRow">标题行号，-1则不指定，由系统指定</param>
        /// <param name="dataFrom">数据起始行号，若为-1则从0行开始</param>
        /// <returns>数据列表List</returns>
        List<T> ConvertToClassObject<T>(string path, string sheetName, int titleRow, int captionRow, int dataFrom) where T : class;

        /// <summary>
        /// Excel文件流转换为DataTable
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <param name="isXls">是否xls类型，TRUE：xls文件，FALSE：xlsx文件</param>
        /// <param name="sheetIndex">表单索引</param>
        /// <param name="titleRow">列名行号，-1则不指定，由系统指定</param>
        /// <param name="captionRow">标题行号，-1则不指定，由系统指定</param>
        /// <param name="dataFrom">数据起始行号，若为-1则从0行开始</param>
        /// <returns>DataTable</returns>
        DataTable ConvertToTable(Stream fs, bool isXls, int sheetIndex, int titleRow, int captionRow, int dataFrom);

        /// <summary>
        /// Excel文件流转换为指定实体类
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="fs">文件流</param>
        /// <param name="isXls">是否xls类型，TRUE：xls文件，FALSE：xlsx文件</param>
        /// <param name="sheetIndex">表单索引</param>
        /// <param name="titleRow">列名行号，-1则不指定，由系统指定</param>
        /// <param name="captionRow">标题行号，-1则不指定，由系统指定</param>
        /// <param name="dataFrom">数据起始行号，若为-1则从0行开始</param>
        /// <returns></returns>
        List<T> ConvertToClassObject<T>(Stream fs, bool isXls, int sheetIndex, int titleRow, int captionRow, int dataFrom) where T : class;

        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="table">转换后的数据</param>
        /// <param name="s">可选参数</param>
        /// <returns>TRUE：处理成功，FALSE：处理失败</returns>
        bool ProcessTable(DataTable table, params object[] s);

        /// <summary>
        /// 行数据处理
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="s">可选参数</param>
        /// <returns>TRUE：处理成功，FALSE：处理失败</returns>
        bool ProcessRow(DataRow row, params object[] s);

        /// <summary>
        /// 多行数据处理
        /// </summary>
        /// <param name="rows">数据行</param>
        /// <param name="s">可选参数</param>
        /// <returns>TRUE：处理成功，FALSE：处理失败</returns>
        bool ProcessRows(List<DataRow> rows, params object[] s);

        /// <summary>
        /// 单条数据处理
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="t">数据记录</param>
        /// <param name="s">可选参数</param>
        /// <returns>TRUE：处理成功，FALSE：处理失败</returns>
        bool ProcessObjectClass<T>(T t, params object[] s) where T : class;

        /// <summary>
        /// 单条数据处理
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="listT">数据记录</param>
        /// <param name="s">可选参数</param>
        /// <returns>TRUE：处理成功，FALSE：处理失败</returns>
        bool ProcessObjectClass<T>(List<T> listT, params object[] s) where T : class;
    }
}
