using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Enc;

public class EncPage
{
    public DataTable PageSearch(object FirstName, object LastName, object FatherName, object MotherName,
        object MotherFamily,
        object EncID, object NationalCode, object PatientID, object PageCode, object Insurance, object EncType,
        object EncStatus, object DepID, object DrID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@FirstName", FirstName);
        com.Parameters.AddWithValue("@LastName", LastName);
        com.Parameters.AddWithValue("@FatherName", FatherName);
        com.Parameters.AddWithValue("@MotherName", MotherName);
        com.Parameters.AddWithValue("@MotherFamily", MotherFamily);
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@NationalCode", NationalCode);
        com.Parameters.AddWithValue("@PatientID", PatientID);
        com.Parameters.AddWithValue("@PageCode", PageCode);
        com.Parameters.AddWithValue("@Insurance ", Insurance);
        com.Parameters.AddWithValue("@EncType ", EncType);
        com.Parameters.AddWithValue("@EncStatus", EncStatus);
        com.Parameters.AddWithValue("@DepID", DepID);
        com.Parameters.AddWithValue("@DrID", DrID);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spPageSearch");
    }
}