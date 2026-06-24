using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Pet;

public class FormPet
{
    public void FormPetSave
    (int CRUD, int EncID, int PageID, int ReportDrID1, int ReportDrID2, int Question, int ChiefComplaint,
        int UserPersID)


    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@PageID", PageID);
        com.Parameters.AddWithValue("@ReportDrID1", ReportDrID1);
        com.Parameters.AddWithValue("@ReportDrID2", ReportDrID2);
        com.Parameters.AddWithValue("@Question", Question);
        com.Parameters.AddWithValue("@ChiefComplaint", ChiefComplaint);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spFormPet");
    }


    public DataTable FormPetLoad(int CRUD, int PageID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@PageID", PageID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spFormPet");
    }
}