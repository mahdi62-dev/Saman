using System.Data;
using System.Data.SqlClient;

namespace DAL.Checkup;

public class MusclePower
{
    public static void SaveMusclePower
    (bool hasRecord, int admCode, double r1, double r2, double r3, double r4, double r5, double l1, double l2,
        double l3, double l4, double l5)
    {
        var parm = new SqlParameter[11];

        parm[0] = new SqlParameter("@AdmCode", admCode);
        parm[1] = new SqlParameter("@R1", r1);
        parm[2] = new SqlParameter("@R2", r2);
        parm[3] = new SqlParameter("@R3", r3);
        parm[4] = new SqlParameter("@R4", r4);
        parm[5] = new SqlParameter("@R5", r5);
        parm[6] = new SqlParameter("@L1", l1);
        parm[7] = new SqlParameter("@L2", l2);
        parm[8] = new SqlParameter("@L3", l3);
        parm[9] = new SqlParameter("@L4", l4);
        parm[10] = new SqlParameter("@L5", l5);

        if (!hasRecord)
        {
            var strIns = "Insert Into DrChkMusclePower (AdmCode, R1,R2,R3,R4,R5,L1,L2,L3,L4,L5) " +
                         "Select @AdmCode, @R1, @R2, @R3, @R4, @R5, @L1, @L2, @L3, @L4, @L5";

            SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strIns, parm);
        }

        else
        {
            var strUpd = "Update DrChkMusclePower " +
                         "Set R1=@R1, R2=@R2, R3=@R3, R4=@R4, R5=@R5, L1=@L1, L2=@L2, L3=@L3, L4=@L4, L5=@L5 " +
                         "Where AdmCode=@AdmCode";

            SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strUpd, parm);
        }
    }

    //===============================================================================================================================

    public static void DelRecord
        (int admCode)
    {
        var parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@AdmCode", admCode);

        var strDel = "Delete From DrChkMusclePower " +
                     "Where AdmCode=@AdmCode";
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strDel, parm);
    }

    //===============================================================================================================================

    public static SqlDataReader ReadData(int admCode)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@AdmCode", admCode);

        var strSel = "Select * From DrChkMusclePower Where AdmCode= @Admcode";

        var readScan = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text,
            strSel, parm);
        return readScan;
    }

    //====================================================================================================================
}