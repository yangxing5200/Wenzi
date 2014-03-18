using System;
using System.Collections.Generic;

namespace Wz.Common.ProExcel
{
    public class ExcelNPOI : AbstractExcelNPOI
    {

        public override bool ProcessTable(System.Data.DataTable table, params object[] s)
        {
            throw new NotImplementedException();
        }

        public override bool ProcessRow(System.Data.DataRow row, params object[] s)
        {
            throw new NotImplementedException();
        }

        public override bool ProcessRows(List<System.Data.DataRow> rows, params object[] s)
        {
            throw new NotImplementedException();
        }

        public override bool ProcessObjectClass<T>(T t, params object[] s)
        {
            throw new NotImplementedException();
        }

        public override bool ProcessObjectClass<T>(List<T> listT, params object[] s)
        {
            throw new NotImplementedException();
        }
    }
}
