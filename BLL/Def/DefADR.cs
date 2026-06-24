using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Def;

public class DefADR
{
    public DataTable ADRLoad(int CRUD, int ADID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@ADID", ADID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spADR");
    }

    public void ADRSave(int CRUD, int ADID, object ID, object Name, object IsDisable)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@ADID", ADID);
        com.Parameters.AddWithValue("@ID", ID);
        com.Parameters.AddWithValue("@Name", Name);
        com.Parameters.AddWithValue("@IsDisable", IsDisable);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringERP, com, "spADR");
    }
}