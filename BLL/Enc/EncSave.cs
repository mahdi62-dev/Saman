using System;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Enc;

public class EncSave
{
    public DataTable EncSaveFull
    (int CRUD, int EncID, int PatientID, string StartDate, int EncType, bool IsEmerg, int DepID, int DrID,
        int RefDrID, string PresDate, int EncService, int ChiefComplaint, string Description, int Insurance,
        string InsuranceNumber, string InsuranceSerial, string InsurancePageNumber, string InsuranceExpireDate,
        bool InsuranceExpireValid, long SDrFixAmount, bool IsSDrAcceptAmmount, string OprReserveDate, int Height,
        int Weight, string CompanionFullName, string CompanionTel, string CompanionAddress, int Insurance2,
        string InsuranceNumber2, int UserPersID
    )
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@PatientID", PatientID);
        com.Parameters.AddWithValue("@StartDate", StartDate);
        com.Parameters.AddWithValue("@EncType", EncType);
        com.Parameters.AddWithValue("@IsEmerg", IsEmerg);
        com.Parameters.AddWithValue("@DepID", DepID);
        com.Parameters.AddWithValue("@DrID", DrID);
        com.Parameters.AddWithValue("@RefDrID", RefDrID);
        com.Parameters.AddWithValue("@PresDate", PresDate);
        // PresDate == null ? (object)DBNull.Value : PresDate);
        com.Parameters.AddWithValue("@EncService", EncService);
        com.Parameters.AddWithValue("@ChiefComplaint", ChiefComplaint);
        com.Parameters.AddWithValue("@Description", Description);
        com.Parameters.AddWithValue("@Insurance", Insurance);
        com.Parameters.AddWithValue("@InsuranceNumber", InsuranceNumber);
        com.Parameters.AddWithValue("@InsuranceSerial", InsuranceSerial);
        com.Parameters.AddWithValue("@InsurancePagenumber", InsurancePageNumber);
        com.Parameters.AddWithValue("@InsuranceExpireDate", InsuranceExpireDate);
        com.Parameters.AddWithValue("@InsuranceExpireValid", InsuranceExpireValid);
        com.Parameters.AddWithValue("@SDrFixAmount", SDrFixAmount);
        com.Parameters.AddWithValue("@IsSDrAcceptAmount", IsSDrAcceptAmmount);
        com.Parameters.AddWithValue("@OprReserveDate",
            OprReserveDate == null ? DBNull.Value : OprReserveDate);
        com.Parameters.AddWithValue("@Height", Height);
        com.Parameters.AddWithValue("@Weight", Weight);
        com.Parameters.AddWithValue("@CompanionFullName", CompanionFullName);
        com.Parameters.AddWithValue("@CompanionTel", CompanionTel);
        com.Parameters.AddWithValue("@CompanionAddress", CompanionAddress);
        com.Parameters.AddWithValue("@Insurance2", Insurance2);
        com.Parameters.AddWithValue("@InsuranceNumber2", InsuranceNumber2);

        com.Parameters.AddWithValue("@PersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spEncSave");
    }

    public DataTable EncInsuranceChange
        (int EncID, int InsuranceID, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@InsuranceIDNew", InsuranceID);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spEncInsuranceChange");
    }

    public int EncTotalPayAmount(int EncID)
    {
        var tsql = @" SELECT ISNULL(SUM(PayAmount),0) AS TotalPayAmount
                            FROM dbo.Page p
                            WHERE P.EncID=@EncID ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);

        var dalData = new Data();
        var dt = dalData.GetDataTableByText(cSet.cnStringA, com, tsql);
        return Convert.ToInt32(dt.Rows[0]["TotalPayAmount"]);
    }
}