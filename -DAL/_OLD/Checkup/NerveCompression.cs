using System.Data;
using System.Data.SqlClient;

namespace DAL.Checkup;

public class NerveCompression
{
    public static void SaveNerveCompression
    (bool hasRecord, int admCode, double r1, double r2, double r3, double r4, double r5, double r6, double r7,
        double r8, double r9, double r10, double r11,
        double l1, double l2, double l3, double l4, double l5, double l6, double l7, double l8, double l9,
        double l10, double l11)
    {
        var parm = new SqlParameter[23];

        parm[0] = new SqlParameter("@AdmCode", admCode);
        parm[1] = new SqlParameter("@R1", r1);
        parm[2] = new SqlParameter("@R2", r2);
        parm[3] = new SqlParameter("@R3", r3);
        parm[4] = new SqlParameter("@R4", r4);
        parm[5] = new SqlParameter("@R5", r5);
        parm[6] = new SqlParameter("@R6", r6);
        parm[7] = new SqlParameter("@R7", r7);
        parm[8] = new SqlParameter("@R8", r8);
        parm[9] = new SqlParameter("@R9", r9);
        parm[10] = new SqlParameter("@R10", r10);
        parm[11] = new SqlParameter("@R11", r11);
        parm[12] = new SqlParameter("@L1", l1);
        parm[13] = new SqlParameter("@L2", l2);
        parm[14] = new SqlParameter("@L3", l3);
        parm[15] = new SqlParameter("@L4", l4);
        parm[16] = new SqlParameter("@L5", l5);
        parm[17] = new SqlParameter("@L6", l6);
        parm[18] = new SqlParameter("@L7", l7);
        parm[19] = new SqlParameter("@L8", l8);
        parm[20] = new SqlParameter("@L9", l9);
        parm[21] = new SqlParameter("@L10", l10);
        parm[22] = new SqlParameter("@L11", l11);

        if (!hasRecord)
        {
            var strIns =
                "Insert Into DrChkNerveCompression (AdmCode, R1,R2,R3,R4,R5,R6,R7,R8,R9,R10,R11,L1,L2,L3,L4,L5,L6,L7,L8,L9,L10,L11) " +
                "Select @AdmCode, @R1, @R2, @R3, @R4, @R5,@R6,@R7,@R8,@R9,@R10,@R11, @L1, @L2, @L3, @L4, @L5,@L6,@L7,@L8,@L9,@L10,@L11";

            SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strIns, parm);
        }

        else
        {
            var strUpd = "Update DrChkNerveCompression " +
                         "Set R1=@R1, R2=@R2, R3=@R3, R4=@R4, R5=@R5,R6=@R6,R7=@R7,R8=@R8,R9=@R9,R10=@R10,R11=@R11, L1=@L1, L2=@L2, L3=@L3, L4=@L4, L5=@L5,L6=@L6,L7=@L7,L8=@L8,L9=@L9,L10=@L10,L11=@L11) " +
                         "Where AdmCode=@AdmCode";

            SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strUpd, parm);
        }
    }

    //===============================================================================================================================

    public static void DelRecord
        (int admCode)
    {
        var parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@AdmCode", admCode);

        var strDel = "Delete From DrChkNerveCompression " +
                     "Where AdmCode=@AdmCode";
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strDel, parm);
    }

    //===============================================================================================================================

    public static SqlDataReader ReadData(int admCode)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@AdmCode", admCode);

        var strSel = "Select * From DrChkNerveCompression Where AdmCode= @Admcode";

        var readScan = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text,
            strSel, parm);
        return readScan;
    }

    //====================================================================================================================
}