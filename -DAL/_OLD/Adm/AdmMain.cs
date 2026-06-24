using System.Data;
using System.Data.SqlClient;

namespace DAL;

public class AdmMain
{
    public static SqlDataReader DrAdmCode(int admCode)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@AdmCode", admCode);

        var strSel = "Select * From Adm Where [Adm Code]= @admCode";

        var DrAdmCode = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text, strSel, parm);
        return DrAdmCode;
    }

    //====================================================================================================================
}