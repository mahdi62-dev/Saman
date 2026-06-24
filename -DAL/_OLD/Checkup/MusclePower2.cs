using System.Data;
using System.Data.SqlClient;

namespace DAL.Checkup;

public class MusclePower2
{
    public static void SaveMusclePower2
    (bool hasRecord, int admCode, double r1, double r2, double r3, double r4, double r5, double r6, double l1,
        double l2, double l3, double l4, double l5, double l6)
    {
        var parm = new SqlParameter[13];

        parm[0] = new SqlParameter("@AdmCode", admCode);
        parm[1] = new SqlParameter("@R1", r1);
        parm[2] = new SqlParameter("@R2", r2);
        parm[3] = new SqlParameter("@R3", r3);
        parm[4] = new SqlParameter("@R4", r4);
        parm[5] = new SqlParameter("@R5", r5);
        parm[6] = new SqlParameter("@R6", r6);
        parm[7] = new SqlParameter("@L1", l1);
        parm[8] = new SqlParameter("@L2", l2);
        parm[9] = new SqlParameter("@L3", l3);
        parm[10] = new SqlParameter("@L4", l4);
        parm[11] = new SqlParameter("@L5", l5);
        parm[12] = new SqlParameter("@L6", l6);

        if (!hasRecord)
        {
            var strIns = "Insert Into DrChkMusclePower2 (AdmCode, R1,R2,R3,R4,R5,R6,L1,L2,L3,L4,L5,L6) " +
                         "Select @AdmCode, @R1, @R2, @R3, @R4, @R5,@R6, @L1, @L2, @L3, @L4, @L5,@L6";

            SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strIns, parm);
        }

        else
        {
            var strUpd = "Update DrChkMusclePower2 " +
                         "Set R1=@R1, R2=@R2, R3=@R3, R4=@R4, R5=@R5,R6=@R6, L1=@L1, L2=@L2, L3=@L3, L4=@L4, L5=@L5,L6=@L6) " +
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

        var strDel = "Delete From DrChkMusclePower2 " +
                     "Where AdmCode=@AdmCode";
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strDel, parm);
    }

    //===============================================================================================================================

    public static SqlDataReader ReadData(int admCode)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@AdmCode", admCode);

        const string strSel = "Select * From DrChkMusclePower2 Where AdmCode= @Admcode";

        var readScan = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text,
            strSel, parm);
        return readScan;
    }

    //====================================================================================================================
}