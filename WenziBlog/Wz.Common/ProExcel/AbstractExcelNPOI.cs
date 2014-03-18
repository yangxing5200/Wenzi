using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using Common.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Wz.Common.ProExcel.Interfaces;

namespace Wz.Common.ProExcel
{
    public abstract class AbstractExcelNPOI : IExcelNPOI
    {
        #region<<用户参数>>

        /// <summary>
        /// 服务日志接口
        /// </summary>
        ILog log = LogManager.GetLogger("bll");

        #endregion

        #region<<接口实现>>

        public DataTable ConvertToTable(string path)
        {
            return ConvertToTable(path, 0, -1, -1, 0);
        }

        public DataTable ConvertToTable(string path, int sheetIndex)
        {
            return ConvertToTable(path, sheetIndex, -1, -1, 0);
        }

        public DataTable ConvertToTable(string path, int sheetIndex, int titleRow, int captionRow, int dataFrom)
        {
            try
            {
                // 获取excel指定表单
                var workBook = GetWorkbook(path);
                var sheet = workBook.GetSheetAt(sheetIndex);
                // 获取表单中所有行
                var rows = sheet.GetRowEnumerator();
                // 最大列数
                var maxCellNum = 0;
                while (rows.MoveNext())
                {
                    if (((IRow)rows.Current).LastCellNum > maxCellNum)
                        maxCellNum = ((IRow)rows.Current).LastCellNum;
                }
                rows.Reset();

                #region<<构建返回表结构>>

                var table = new DataTable();
                // 列名标题行
                IRow rowTitle = null;
                if (titleRow >= 0) rowTitle = sheet.GetRow(titleRow);
                // caption
                IRow rowCaption = null;
                if (captionRow >= 0) rowCaption = sheet.GetRow(captionRow);
                for (var i = 0; i < maxCellNum; i++)
                {
                    // 列名
                    var dc = new DataColumn();
                    string colName;
                    if (rowTitle != null)
                    {
                        if (i >= rowTitle.LastCellNum) colName = Convert.ToChar(((int)'A') + i).ToString();
                        else
                        {
                            var cell = rowTitle.GetCell(i);
                            colName = cell == null ? Convert.ToChar(((int)'A') + i).ToString() : GetCellValue(cell).ToString();
                        }
                    }
                    else colName = Convert.ToChar(((int)'A') + i).ToString();
                    if (table.Columns.Contains(colName)) colName = colName + i;
                    dc.ColumnName = colName;

                    // 标题
                    if (rowCaption != null)
                    {
                        if (i >= rowCaption.LastCellNum) dc.Caption = Convert.ToChar(((int)'A') + i).ToString();
                        else
                        {
                            var cell = rowCaption.GetCell(i);
                            dc.Caption = cell == null ? Convert.ToChar(((int)'A') + i).ToString() : GetCellValue(cell).ToString();
                        }
                    }
                    else dc.Caption = Convert.ToChar(((int)'A') + i).ToString();
                    table.Columns.Add(dc);
                }

                #endregion

                #region<<填充返回数据>>
                while (rows.MoveNext())
                {
                    var row = rows.Current as IRow;

              
                    if (row.RowNum < dataFrom) continue;
                    if (row.RowNum == titleRow || row.RowNum == captionRow) continue;
                    // 填充行数据
                    var dr = table.NewRow();
                    for (var i = 0; i < maxCellNum; i++)
                    {
                        if (i >= row.LastCellNum) continue;
                        var cell = row.GetCell(i);
                        if (cell == null) dr[i] = null;
                        else dr[i] = GetCellValue(cell);
                    }
                    table.Rows.Add(dr);
                }
                #endregion

                return table;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public DataTable ConvertToTable(string path, string sheetName)
        {
            return ConvertToTable(path, sheetName, -1, -1, 0);
        }

        public DataTable ConvertToTable(string path, string sheetName, int titleRow, int captionRow, int dataFrom)
        {
            try
            {
                var workbook = GetWorkbook(path);
                var sheetIndex = workbook.GetSheetIndex(sheetName);

                return ConvertToTable(path, sheetIndex, titleRow, captionRow, dataFrom);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public List<T> ConvertToClassObject<T>(string path) where T : class
        {
            try
            {
                return ConvertToClassObject<T>(path, 0, 0, -1, 1);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public List<T> ConvertToClassObject<T>(string path, int sheetIndex) where T : class
        {
            return ConvertToClassObject<T>(path, sheetIndex, 0, -1, 1);
        }

        public List<T> ConvertToClassObject<T>(string path, int sheetIndex, int titleRow, int captionRow, int dataFrom) where T : class
        {
            try
            {
                var table = ConvertToTable(path, sheetIndex, titleRow, captionRow, dataFrom);
                if (table == null) throw new Exception("数据转换异常！");

                var listT = new List<T>();
                var t = typeof(T);
                // 获取所有属性对象
                var pros = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var row in table.Rows.Cast<DataRow>())
                {
                    // 实例化T
                    var instanceT = Activator.CreateInstance<T>();
                    // 循环属性赋值
                    foreach (var pro in pros.Where(pro => table.Columns.Contains(pro.Name)))
                    {
                        pro.SetValue(instanceT, Convert.ChangeType(row[pro.Name], pro.GetType()), null);
                    }
                    listT.Add(instanceT);
                }

                return listT;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public List<T> ConvertToClassObject<T>(string path, string sheetName) where T : class
        {
            try
            {
                return ConvertToClassObject<T>(path, sheetName, 0, -1, 1);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public List<T> ConvertToClassObject<T>(string path, string sheetName, int titleRow, int captionRow, int dataFrom) where T : class
        {
            try
            {
                var workbook = GetWorkbook(path);
                var sheetIndex = workbook.GetSheetIndex(sheetName);

                return ConvertToClassObject<T>(path, sheetIndex, titleRow, captionRow, dataFrom);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public DataTable ConvertToTable(Stream fs, bool isXls, int sheetIndex, int titleRow, int captionRow, int dataFrom)
        {
            try
            {
                // 获取excel指定表单
                var workBook = GetWorkbook(fs, isXls);
                var sheet = workBook.GetSheetAt(sheetIndex);
                // 获取表单中所有行
                var rows = sheet.GetRowEnumerator();
                // 最大列数
                var maxCellNum = 0;
                while (rows.MoveNext())
                {
                    if (((IRow)rows.Current).LastCellNum > maxCellNum)
                        maxCellNum = ((IRow)rows.Current).LastCellNum;
                }
                rows.Reset();

                #region<<构建返回表结构>>

                var table = new DataTable();
                // 列名标题行
                IRow rowTitle = null;
                if (titleRow >= 0) rowTitle = sheet.GetRow(titleRow);
                // caption
                IRow rowCaption = null;
                if (captionRow >= 0) rowCaption = sheet.GetRow(captionRow);
                for (var i = 0; i < maxCellNum; i++)
                {
                    // 列名
                    var dc = new DataColumn();
                    string colName;
                    if (rowTitle != null)
                    {
                        if (i >= rowTitle.LastCellNum) colName = Convert.ToChar(((int)'A') + i).ToString();
                        else
                        {
                            var cell = rowTitle.GetCell(i);
                            colName = cell == null ? Convert.ToChar(((int)'A') + i).ToString() : GetCellValue(cell).ToString();
                        }
                    }
                    else colName = Convert.ToChar(((int)'A') + i).ToString();
                    if (table.Columns.Contains(colName)) colName = colName + i;
                    dc.ColumnName = colName;

                    // 标题
                    if (rowCaption != null)
                    {
                        if (i >= rowCaption.LastCellNum) dc.Caption = Convert.ToChar(((int)'A') + i).ToString();
                        else
                        {
                            var cell = rowCaption.GetCell(i);
                            dc.Caption = cell == null ? Convert.ToChar(((int)'A') + i).ToString() : GetCellValue(cell).ToString();
                        }
                    }
                    else dc.Caption = Convert.ToChar(((int)'A') + i).ToString();
                    table.Columns.Add(dc);
                }

                #endregion

                #region<<填充返回数据>>
                while (rows.MoveNext())
                {
                    var row = rows.Current as IRow;

                  
                    if (row.RowNum < dataFrom) continue;
                    if (row.RowNum == titleRow || row.RowNum == captionRow) continue;
                    // 填充行数据
                    var dr = table.NewRow();
                    for (var i = 0; i < maxCellNum; i++)
                    {
                        if (i >= row.LastCellNum) continue;
                        var cell = row.GetCell(i);
                        if (cell == null) dr[i] = null;
                        else dr[i] = GetCellValue(cell);
                    }
                    table.Rows.Add(dr);
                }
                #endregion

                return table;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public List<T> ConvertToClassObject<T>(Stream fs, bool isXls, int sheetIndex, int titleRow, int captionRow, int dataFrom) where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        #region<<抽象方法>>

        public abstract bool ProcessTable(DataTable table, params object[] s);

        public abstract bool ProcessRow(DataRow row, params object[] s);

        public abstract bool ProcessRows(List<DataRow> rows, params object[] s);

        public abstract bool ProcessObjectClass<T>(T t, params object[] s) where T : class;

        public abstract bool ProcessObjectClass<T>(List<T> listT, params object[] s) where T : class;

        #endregion

        #region<<用户函数>>

        /// <summary>
        /// 获取WorkBook
        /// </summary>
        /// <param name="path">文件位置</param>
        /// <returns>IWorkbook</returns>
        private IWorkbook GetWorkbook(string path)
        {
            try
            {
                using (Stream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    if (path.ToLower().EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                    {
                        return new HSSFWorkbook(fs) as IWorkbook;
                    }
                    if (path.ToLower().EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        return new XSSFWorkbook(fs) as IWorkbook;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 获取WorkBook
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <param name="isXls">是否xls类型，TRUE：xls文件，FALSE：xlsx文件</param>
        /// <returns>IWorkbook</returns>
        private IWorkbook GetWorkbook(Stream fs, bool isXls)
        {
            try
            {
                if (isXls) return new HSSFWorkbook(fs) as IWorkbook;
                return new XSSFWorkbook(fs) as IWorkbook;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 获取单元格数据
        /// </summary>
        /// <param name="cell">单元格对象</param>
        /// <returns>数据object</returns>
        protected object GetCellValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.BLANK:
                    return "[null]";
                case CellType.BOOLEAN:
                    return cell.BooleanCellValue;
                case CellType.ERROR:
                    return cell.ErrorCellValue;
                case CellType.NUMERIC:
                    if (DateUtil.IsCellDateFormatted(cell)) return cell.DateCellValue;
                    return cell.NumericCellValue;
                case CellType.STRING:
                    return cell.StringCellValue;
                case CellType.Unknown:
                    return cell.RichStringCellValue.String;
                default:
                    return "=" + cell.CellFormula;
            }
        }


        /// <summary>
        /// 将dt里的数据导出为Excel到指定位置
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="TitleType">标题的格式：(中文标题：Chinese,英文标题：English,中英文双标题：Both)</param>
        /// <param name="path">导出的数据文件要放目录</param>
        /// <param name="filename">文件名称。文件名称无效时自动生成一个文件名</param>
        /// <returns></returns>
        public string DataSetExportToExcel(DataSet ds, string TitleType,string path, params object[] s)
        {
            HSSFWorkbook book = new HSSFWorkbook();
            foreach (DataTable dt in ds.Tables)
            {
                ISheet sheet = book.CreateSheet(dt.TableName);
                #region  sheet赋值
                //初始化标题
                IRow Title1 = sheet.CreateRow(0);
                IRow Title2 = new HSSFRow();
                if (TitleType == "Both")
                {
                    Title2 = sheet.CreateRow(1);
                }
                foreach (DataColumn column in dt.Columns)
                {
                    switch (TitleType)
                    {
                        case "Chinese":
                            Title1.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                            break;
                        case "English":
                            Title1.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            break;
                        case "Both":
                            Title1.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                            Title2.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            break;
                    }
                }

                //导入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow dataRow = new HSSFRow();
                    if (TitleType == "Both")
                    {
                        dataRow = sheet.CreateRow(i + 2);
                    }
                    else
                    {
                        dataRow = sheet.CreateRow(i + 1);
                    }
                    foreach (DataColumn column in dt.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(dt.Rows[column.Ordinal].ToString());
                    }
                }

                #endregion
            }

            //处理文件路径
            path = path.Trim();
            if (path.Substring(path.Length - 1) != "/" && path.Substring(path.Length - 1) != @"\")
            {
                path = path + "/";
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = "";
            //如果文件名无效，则自动生成一个文件名
            if (s == null || s.Length < 1)
            {
                filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            }
            else
            {
                filename = s[0] as string;
            }

            //保存文件
            FileStream file = new FileStream(path + filename, FileMode.Create);
            book.Write(file);
            file.Close();
            return path + filename;
        }

        /// <summary>
        /// 将dt里的数据导出为Excel到指定位置
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="TitleType">标题的格式：(中文标题：Chinese,英文标题：English,中英文双标题：Both)</param>
        /// <param name="path">导出的数据文件要放目录</param>
        /// <param name="filename">文件名称。文件名称无效时自动生成一个文件名</param>
        /// <returns></returns>
        public string DataTabelExportToExcel(DataTable dt, string TitleType, string path, params object[] s) 
        {
            HSSFWorkbook book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet();
            //sheet = this.DataTabelToSheet(dt, TitleType);

            #region  sheet赋值
            //初始化标题
            IRow Title1 = sheet.CreateRow(0);
            IRow Title2 = new HSSFRow();
            if (TitleType == "Both")
            {
                Title2 = sheet.CreateRow(1);
            }
            foreach (DataColumn column in dt.Columns)
            {
                switch (TitleType)
                {
                    case "Chinese":
                        Title1.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                        break;
                    case "English":
                        Title1.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        break;
                    case "Both":
                        Title1.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        Title2.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                        break;
                }
            }

            //导入数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow dataRow = new HSSFRow();
                if (TitleType == "Both")
                {
                    dataRow = sheet.CreateRow(i + 2);
                }
                else
                {
                    dataRow = sheet.CreateRow(i + 1);
                }
                foreach (DataColumn column in dt.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(dt.Rows[column.Ordinal].ToString());
                }
            }

            #endregion

            //处理文件路径
            path = path.Trim();
            if (path.Substring(path.Length - 1) != "/" && path.Substring(path.Length - 1) != @"\") 
            {
                path = path + "/";
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = "";
            //如果文件名无效，则自动生成一个文件名
            if (s == null || s.Length < 1)
            {
                filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            }
            else 
            {
                filename = s[0] as string;
            }
            
            //保存文件
            FileStream file = new FileStream(path+filename,FileMode.Create);
            book.Write(file);
            file.Close();
            return path + filename;
        }

        /// <summary>
        /// 将DataTable的数据导入Sheet
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="TitleType">标题的格式：(中文标题：Chinese,英文标题：English,中英文双标题：Both)</param>
        /// <returns></returns>
        private ISheet DataTabelToSheet(DataTable dt,string TitleType) 
        {
            //book操作类
            HSSFWorkbook book = new HSSFWorkbook();
            //sheet操作类
            ISheet sheet = book.CreateSheet();
            //初始化标题
            IRow Title1 = sheet.CreateRow(0);
            IRow Title2 = new HSSFRow();
            if (TitleType == "Both")
            {
                Title2 = sheet.CreateRow(1);
            }
            foreach (DataColumn column in dt.Columns) 
            {
                switch (TitleType) 
                { 
                    case "Chinese":
                        Title1.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                        break;
                    case "English":
                        Title1.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        break;
                    case "Both":
                        Title1.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        Title2.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                        break;
                }
            }

            //导入数据
            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                IRow dataRow = new HSSFRow();
                if (TitleType == "Both")
                {
                    dataRow = sheet.CreateRow(i + 2);
                }
                else 
                {
                    dataRow = sheet.CreateRow(i+1);
                }
                foreach (DataColumn column in dt.Columns) 
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(dt.Rows[column.Ordinal].ToString());
                }
            }
            return sheet;
        } 

        #endregion
    }
}
