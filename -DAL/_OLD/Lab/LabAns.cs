using System.Data;
using System.Data.SqlClient;

namespace DAL;

public class LabAns
{
    //====================================================================================================================

    public static SqlDataReader LoadLabGroups(int id)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@Id", id);

        var strSel = "Select * From AdmScan Where Id= @Id";

        var readScan = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text,
            strSel, parm);
        return readScan;
    }


    public static void SaveLabAns
        (int gsId, string note, byte ageYear, byte sex, byte operatorCode, bool isRecheked)
    {
        var parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@GSID", gsId);
        parm[1] = new SqlParameter("@Result", note);
        parm[2] = new SqlParameter("@AgeYear", ageYear);
        parm[3] = new SqlParameter("@Sex", sex);
        parm[4] = new SqlParameter("@OperatorCode", operatorCode);
        parm[5] = new SqlParameter("@isRechecked", isRecheked);


        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.StoredProcedure,
            "CheckForLabAns", parm);
    }
}