using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Report;

public class DocFile
{
    public DataTable DocFileSave
        (object CRUD, object Name, object ContentType, object Data, object DataText, object DataBinery)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@Name", Name);
        com.Parameters.AddWithValue("@ContentType", ContentType);
        com.Parameters.AddWithValue("@Data", Data);
        com.Parameters.AddWithValue("@DataText", DataText);
        com.Parameters.AddWithValue("@DataBinery", DataBinery);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringReport, com, "spDocSave");
    }
}