using System.Data;
using System.Data.SqlClient;

namespace DAL.Cash;

public class CashPaper
{
    public static SqlDataReader DrPaperData(int partCode, int paperCode)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@PaperCode", paperCode);
        parm[1] = new SqlParameter("@PartCode", partCode);

        const string fields =
            "[Paper Int Code],[Sum Price],[Bimeh Price],[MBimehPrice],[Result Price],[Pay Price], [Adm Code],Sex,[Family],[Name],[PType],[BimehName],[Bimeh Code]" +
            ",isDis,[Date],[Bimeh Code],UnivTakhfifPrice,[Doctor Code],[DrName],[PartName]";

        const string strSel = "Select " + fields +
                              " From [Paper View] Where [Paper Code]=@PaperCode AND [Part Code]=@PartCode";

        var DrPaperData = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text, strSel, parm);
        return DrPaperData;
    }
    //===========================================================================================================================

    public static SqlDataReader DrPaperData(int paperIntCode)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@PaperIntCode", paperIntCode);

        const string fields =
            "[Paper Int Code],[Sum Price],[Bimeh Price],[MBimehPrice],[Result Price],[Pay Price], [Adm Code],Sex,[Family],[Name],[PType],[BimehName],[Bimeh Code]" +
            ",isDis,[Date],[Bimeh Code],UnivTakhfifPrice,[Doctor Code],[DrName],[PartName]";

        const string strSel = "Select " + fields +
                              " From [Paper View] Where [Paper Int Code]=@PaperIntCode";

        var DrPaperData = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text, strSel, parm);
        return DrPaperData;
    }
}