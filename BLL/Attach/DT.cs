using System;
using System.Data;

namespace Saman.BLL.Attach;

public class DT
{
    public void CreateTableFileList(DataTable dt)
    {
        if (dt == null) return;

        dt.Columns.Add("CRUD", typeof(int));
        dt.Columns.Add("FileName");

        dt.Columns.Add("AttFile");
        dt.Columns["AttFile"].DataType = Type.GetType("System.Byte[]");
        dt.Columns.Add("AttFileType");
        dt.Columns.Add("AttTag");
        //dt.Columns.Add("AttSelect", typeof(CheckBox));
    }
}