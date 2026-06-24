using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.DocReport;

public class AutoCorrect
{
    public DataTable AutoCorrectAction(object CRUD, object ID, object ModID, object CurrentLanguage, object Replace,
        object With)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@ID", ID);
        com.Parameters.AddWithValue("@ModID", ModID);
        com.Parameters.AddWithValue("@CurrentLanguage", CurrentLanguage);
        com.Parameters.AddWithValue("@Replace", Replace);
        com.Parameters.AddWithValue("@With", With);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringReport, com, "spAutoCorrect");
    }
}