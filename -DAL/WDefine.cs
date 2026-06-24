using System.Data;
using System.Data.SqlClient;
using DAL;

namespace WDAL;

public class WDefine
{
    public void LoadDefList(DataSet ds, string fromText)
    {
        if (ds.Tables["defList"] != null) ds.Tables["defList"].Clear();

        var strSelect = "SELECT code, Name FROM " + fromText;

        SqlHelper.FillDataset(cSet.cnString, CommandType.Text,
            strSelect, ds, new[] { "DefList" });
    }

    //==============================================================================================================
    public void DelRecord
        (int code, string tableName)
    {
        var parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@code", code);

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            "Delete From " + tableName + " Where code=@code",
            parm);
    }

    //====================================================================================================================
    public void UpdRecord
        (int code, string tableName, string Name)
    {
        var parm = new SqlParameter[2];
        parm[0] = new SqlParameter("@code", code);
        parm[1] = new SqlParameter("@Name", Name);

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            "Update " + tableName + " Set Name=@Name Where code=@code",
            parm);
    }

    //====================================================================================================================
    public int GetMaxCode
        (string tableName)
    {
        var strSelect = "SELECT IsNull(Max(code)+1, 0) FROM " + tableName;

        var maxCode = int.Parse(SqlHelper.ExecuteScalar(cSet.cnString,
            CommandType.Text,
            strSelect).ToString());
        return maxCode;
    }

    //====================================================================================================================
    public void InsRecord
        (int code, string tableName, string Name)
    {
        var parm = new SqlParameter[2];
        parm[0] = new SqlParameter("@code", code);
        parm[1] = new SqlParameter("@Name", Name);

        var strIns = "Insert Into " + tableName +
                     "(code, Name) " +
                     "Select @code, @Name";

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            strIns, parm);
    }
}