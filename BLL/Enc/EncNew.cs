using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Enc;

public class EncNew
{
    public DataTable PatientSearch
        (string FirstName, string LastName, string FatherName, string NationalCode)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@FirstName", FirstName);
        com.Parameters.Add("@LastName", LastName);
        com.Parameters.Add("@FatherName", FatherName);
        com.Parameters.Add("@NationalCode", NationalCode);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spPatientSearch");
    }


    public DataTable PatientSave(int CRUD, int PatientID, string FirstName, string LastName,
        string FatherName, string NationalCode, int Gender, string BirthDate, int MaritalStatus,
        int Country, string Mobile, int City, string Street, string IDCardNumber, int BirthArea,
        int Religion, int Job, string PassportID, int EducationLevel, string Email, string Tel, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@CRUD", CRUD);
        com.Parameters.Add("@PatientID", PatientID);
        com.Parameters.Add("@FirstName", FirstName);
        com.Parameters.Add("@LastName", LastName);
        com.Parameters.Add("@FatherName", FatherName);
        com.Parameters.Add("@NationalCode", NationalCode);
        com.Parameters.Add("@Gender", Gender);
        com.Parameters.Add("@BirthDate", BirthDate);
        com.Parameters.Add("@MaritalStatus", MaritalStatus);
        com.Parameters.Add("@Country", Country);
        com.Parameters.Add("@Mobile", Mobile);
        com.Parameters.Add("@City", City);
        com.Parameters.Add("@Street", Street);
        com.Parameters.Add("@IDCardNumber", IDCardNumber);
        com.Parameters.Add("@BirthArea", BirthArea);
        com.Parameters.Add("@Religion", Religion);
        com.Parameters.Add("@Job", Job);
        com.Parameters.Add("@PassportID", PassportID);
        com.Parameters.Add("@EducationLevel", EducationLevel);
        com.Parameters.Add("@Email", Email);
        com.Parameters.Add("@Tel", Tel);
        com.Parameters.Add("@PersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spPatientSave");
    }

    public DataTable EncSave(int PersID, int CRUD, int EncID, int PatientID, int EncType, int DepID, int DrID,
        int InsuranceID, string InsuranceNumber, string InsurancePageNumber,
        int ChiefComplaint, string Description)

    {
        var com = new SqlCommand();
        com.Parameters.Add("@PersID", PersID);
        com.Parameters.Add("@CRUD", CRUD);

        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@PatientID", PatientID);
        com.Parameters.AddWithValue("@EncType", EncType);
        //com.Parameters.AddWithValue("@IsEmerg", chkIsEmerg.EditValue);
        //com.Parameters.AddWithValue("@StartDate", txtEncStartDate.Text);
        com.Parameters.AddWithValue("@DepID", DepID);
        com.Parameters.AddWithValue("@DrID", DrID);
        //com.Parameters.AddWithValue("@PresDate", txtPresDate.Text);
        com.Parameters.AddWithValue("@Insurance", InsuranceID);
        com.Parameters.AddWithValue("@InsuranceNumber", InsuranceNumber);
        //com.Parameters.AddWithValue("@InsuranceSerial", txtInsuranceSerial.Text);
        com.Parameters.AddWithValue("@InsurancePagenumber", InsurancePageNumber);
        //com.Parameters.AddWithValue("@InsuranceExpireValid", chkInsuranceExpireValid.EditValue);
        com.Parameters.AddWithValue("@ChiefComplaint", ChiefComplaint);
        com.Parameters.AddWithValue("@Description", Description);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spEncSave");
    }

    public string RefDrName(object ID)
    {
        var strTSQL = @"SELECT CASE WHEN COUNT(*)=0 THEN N'' ELSE MAX(Name) END AS Name
                            FROM[SamanHIS].[dbo].[vADR]
                            WHERE ADName = 'RefDR'
                            AND ID = @ID";

        var com = new SqlCommand();
        com.Parameters.Add("@ID", ID);

        var dalData = new Data();
        var dt = dalData.GetDataTableByText(cSet.cnStringA, com, strTSQL);

        return dt.Rows[0]["Name"].ToString();
    }
}