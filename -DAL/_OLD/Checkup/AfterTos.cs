using System.Data;
using System.Data.SqlClient;

namespace DAL.Checkup;

public class AfterTos
{
    public static void SaveAfterTos
    (bool hasRecord, int admCode, double r1, double r2, double r3, double r4, double r5, double r6, double r7,
        double r8, double r9, double r10, double r11, double r12, double r13, double r14, double r15, double r16,
        double l1, double l2, double l3, double l4, double l5, double l6, double l7, double l8, double l9,
        double l10, double l11, double l12, double l13, double l14, double l15, double l16)
    {
        var parm = new SqlParameter[33];

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
        parm[12] = new SqlParameter("@R12", r12);
        parm[13] = new SqlParameter("@R13", r13);
        parm[14] = new SqlParameter("@R14", r14);
        parm[15] = new SqlParameter("@R15", r15);
        parm[16] = new SqlParameter("@R16", r16);
        parm[17] = new SqlParameter("@L1", l1);
        parm[18] = new SqlParameter("@L2", l2);
        parm[19] = new SqlParameter("@L3", l3);
        parm[20] = new SqlParameter("@L4", l4);
        parm[21] = new SqlParameter("@L5", l5);
        parm[22] = new SqlParameter("@L6", l6);
        parm[23] = new SqlParameter("@L7", l7);
        parm[24] = new SqlParameter("@L8", l8);
        parm[25] = new SqlParameter("@L9", l9);
        parm[26] = new SqlParameter("@L10", l10);
        parm[27] = new SqlParameter("@L11", l11);
        parm[28] = new SqlParameter("@L12", l12);
        parm[29] = new SqlParameter("@L13", l13);
        parm[30] = new SqlParameter("@L14", l14);
        parm[31] = new SqlParameter("@L15", l15);
        parm[32] = new SqlParameter("@L16", l16);

        if (!hasRecord)
        {
            var strIns =
                "Insert Into AfterTos (AdmCode, R1,R2,R3,R4,R5,R6,R7,R8,R9,R10,R11,R12,R13,R14,R15,R16,L1,L2,L3,L4,L5,L6,L7,L8,L9,L10,L11,L12,L13,L14,L15,L16) " +
                "Select @AdmCode, @R1, @R2, @R3, @R4, @R5, @R6,@R7,@R8,@R9,@R10,@R11,@R12,@R13,@R14,@R15,@R16, @L1, @L2, @L3, @L4, @L5,@L6,@L7,@L8,@L9,@L10,@L11,@L12,@L13,@L14,@L15,@L16";

            SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strIns, parm);
        }

        else
        {
            var strUpd = "Update DrChkAfterTos " +
                         "Set R1=@R1, R2=@R2, R3=@R3, R4=@R4, R5=@R5, R6=@R6,R7=@R7,R8=@R8,R9=@R9,R10=@R10,R11=@R11,R12=@R12,R13=@R13,R14=@R14,R15=@R15,R16=@R16, L1=@L1, L2=@L2, L3=@L3, L4=@L4, L5=@L5,L6=@L6,L7=@L7,L8=@L8,L9=@L9,L10=@L10,L11=@L11,L12=@L12,L13=@L13,L14=@L14,L15=@L15,L16=@L16) " +
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

        var strDel = "Delete From DrChkAfterTos " +
                     "Where AdmCode=@AdmCode";
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strDel, parm);
    }

    //===============================================================================================================================

    public static SqlDataReader ReadData(int admCode)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@AdmCode", admCode);

        var strSel = "Select * From DrChkAfterTos Where AdmCode= @Admcode";

        var readScan = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text,
            strSel, parm);
        return readScan;
    }

    //====================================================================================================================
}