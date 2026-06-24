using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Insurance;

public class Insurance2
{
    public DataTable EncInsuranceInfo
    (int CRUD, int EncID, int PageID, int Insurance2Id, string InsuranceNumber2, long InsuranceAcceptAmount2,
        int Insurance2Percent,
        string InsuranceIntroSerial2, object Insurance2Type, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@PageID", PageID);
        com.Parameters.AddWithValue("@Insurance2", Insurance2Id);
        com.Parameters.AddWithValue("@InsuranceNumber2", InsuranceNumber2);
        com.Parameters.AddWithValue("@InsuranceAcceptAmount2", InsuranceAcceptAmount2);
        com.Parameters.AddWithValue("@Insurance2Percent", Insurance2Percent);
        com.Parameters.AddWithValue("@InsuranceIntroSerial2", InsuranceIntroSerial2);
        com.Parameters.AddWithValue("@Insurance2Type", Insurance2Type);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spEncInsuranceInfo");
    }
}