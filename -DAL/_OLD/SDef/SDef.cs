using System.Data;
using System.Data.SqlClient;

namespace DAL.SDef;

public class SDef
{
    public static void LoadServiceTree(DataSet ds) //, string fName, string lName, string father)
    {
        //if (ds.Tables["AdmSearch"] != null)
        //  ds.Tables["AdmSearch"].Clear();

        var strSeelect = "Select sId, fName, parentId From ServiceList " +
                         "Where isFolder=1 And parentId>0";

        SqlHelper.FillDataset(cSet.cnString, CommandType.Text, strSeelect,
            ds, new[] { "ServiceTree" });
    }

    //====================================================================================================================
    public static void LoadServiceListByParentId(DataSet ds, int parentId)
    {
        if (ds.Tables["vServiceList"] != null) ds.Tables["vServiceList"].Clear();

        var parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@parentId", parentId);

        var strSeelect = "SELECT * FROM vServiceList " +
                         "WHERE parentId=@parentId and isActive=1";

        SqlHelper.FillDataset(cSet.cnString, CommandType.Text, strSeelect,
            ds, new[] { "vServiceList" }, parm);
    }
    //====================================================================================================================
}