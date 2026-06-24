using System.Data;
using System.Data.SqlClient;

namespace DAL.Define;

public class DefineDynParm
{
    public static int GetMaxCode
        (int hdId)
    {
        var strSelect = "SELECT IsNull(Max(code)+1, 0) FROM  HdDynParm Where hdId=" + hdId;

        var maxCode = int.Parse(SqlHelper.ExecuteScalar(cSet.cnString,
            CommandType.Text,
            strSelect).ToString());

        if (maxCode == 0) maxCode = 1;

        return maxCode;
    }

    //====================================================================================================================
    public static void InsRecord
        (int hdId, int code, string Name)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@hdId", hdId);
        parm[1] = new SqlParameter("@code", code);
        parm[2] = new SqlParameter("@Name", Name);

        var strIns = "Insert Into HdDynParm " +
                     "(hdId, code, Name) " +
                     "Select @hdId, @code, @Name";

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            strIns, parm);
    }

    //====================================================================================================================
    public static void UpdRecord
        (int hdId, int code, string Name)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@hdId", hdId);
        parm[1] = new SqlParameter("@code", code);
        parm[2] = new SqlParameter("@Name", Name);

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            "Update HdDynParm Set Name=@Name Where hdId=@hdId AND code=@code",
            parm);
    }
    //====================================================================================================================


    public static void DelRecord
        (int hdId, int code)
    {
        var parm = new SqlParameter[2];
        parm[0] = new SqlParameter("@hdId", hdId);
        parm[1] = new SqlParameter("@code", code);


        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            "Delete From HdDynParm Where hdId=@hdId and code=@code",
            parm);
    }
}