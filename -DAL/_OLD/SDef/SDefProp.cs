using System.Data;
using System.Data.SqlClient;

namespace DAL.SDef;

public class SDefProp
{
    public static SqlDataReader daServiceProp
        (int sId)
    {
        var parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@sId", sId);

        return SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text,
            "Select * From ServiceList Where sId=@sId",
            parm);
    }
    //====================================================================================================================

    public static void UpdateServiceProp
        (int sId, string fName, string comment)
    {
        var strUpdate = "Update ServiceList " +
                        "Set fName=@fName, comment=@comment " +
                        "Where sId=@sId";

        var parm = new SqlParameter[10];

        parm[0] = new SqlParameter("@sId", sId);
        parm[1] = new SqlParameter("@fName", fName);
        parm[2] = new SqlParameter("@comment", comment);


        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            strUpdate, parm);
    }

    //====================================================================================================================
    public static void AddNewService
    (string code, string fName, int price, int priceAzad, int priceOld, int priceVip,
        string comment)
    {
        var strIns = "Insert Into ServiceList " +
                     "(code, fName, price, priceAzad, priceOld, priceVip, comment) " +
                     "Select @code, @fName, @price, @priceAzad, @priceOld, @priceVip, @comment";

        var parm = new SqlParameter[10];

        parm[0] = new SqlParameter("@code", code);
        parm[1] = new SqlParameter("@fName", fName);
        parm[2] = new SqlParameter("@price", price);
        parm[3] = new SqlParameter("@priceAzad", priceAzad);
        parm[4] = new SqlParameter("@priceOld", priceOld);
        parm[5] = new SqlParameter("@priceVip", priceVip);
        parm[6] = new SqlParameter("@comment", comment);

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            strIns, parm);
    }

    //====================================================================================================================
    public static void DisableService
        (int sId)
    {
        var strUpd = "Update ServiceList " +
                     "Set isActive=0 " +
                     "Where sId=@sId";

        var parm = new SqlParameter[2];

        parm[0] = new SqlParameter("@sId", sId);
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            strUpd, parm);
    }
}