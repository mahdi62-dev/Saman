using System.Data;
using System.Data.SqlClient;

namespace DAL.Define;

public class Define
{
    public static void LoadDefList(DataSet ds, string fromText)
    {
        if (ds.Tables["defList"] != null) ds.Tables["defList"].Clear();

        var strSelect = "SELECT Code, Name, Explain FROM " + fromText + " WHERE Code>0";

        SqlHelper.FillDataset(cSet.cnString, CommandType.Text,
            strSelect, ds, new[] { "DefList" });
    }

    //==============================================================================================================
    public static void DelRecord
        (int code, string tableName)
    {
        var parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@code", code);

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            "Delete From " + tableName + " Where Code=@Code",
            parm);
    }

    //====================================================================================================================
    public static void UpdRecord
        (int code, string tableName, string Name, string Explain)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@Code", code);
        parm[1] = new SqlParameter("@Name", Name);
        parm[2] = new SqlParameter("@Explain", Explain);

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            "Update " + tableName + " Set Name=@Name, Explain=@Explain Where Code=@Code",
            parm);
    }

    //====================================================================================================================
    public static int GetMaxCode
        (string tableName)
    {
        var strSelect = "SELECT IsNull(Max(Code)+1, 0) FROM " + tableName;

        var maxCode = int.Parse(SqlHelper.ExecuteScalar(cSet.cnString,
            CommandType.Text,
            strSelect).ToString());
        return maxCode;
    }

    //====================================================================================================================
    public static void InsRecord
        (int code, string tableName, string Name, string Explain)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@Code", code);
        parm[1] = new SqlParameter("@Name", Name);
        parm[2] = new SqlParameter("@Explain", Explain);

        var strIns = "Insert Into " + tableName +
                     "(Code, Name, Explain) " +
                     "Select @Code, @Name, @Explain";

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            strIns, parm);
    }
}