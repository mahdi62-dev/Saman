using System.Data;
using System.Data.SqlClient;

namespace DAL;

public class AdmScan
{
    public static void InsRecord
        (int admCode, byte[] binFile, string date, byte scanType)
    {
        var parm = new SqlParameter[12];
        parm[0] = new SqlParameter("@AdmCode", admCode);
        parm[1] = new SqlParameter("@Date", date);
        parm[2] = new SqlParameter("@ScanType", scanType);
        parm[3] = new SqlParameter("@BinFile", binFile);


        const string strIns = "Insert Into AdmScan (AdmCode, Date, ScanType, BinFile) " +
                              "Select @AdmCode, @Date, @ScanType, @BinFile";

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            strIns, parm);
    }

    //====================================================================================================================

    public static SqlDataReader ReadScan(int id)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@Id", id);

        var strSel = "Select * From AdmScan Where Id= @Id";

        var readScan = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text,
            strSel, parm);
        return readScan;
    }

    //====================================================================================================================

    public static void DelRecord
        (int id)
    {
        var parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@Id", id);

        const string strDel = "Delete From AdmScan " +
                              "Where Id=@Id";
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strDel, parm);
    }

    //====================================================================================================================
}