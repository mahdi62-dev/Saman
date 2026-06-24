using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Acc.AccSide;

public class AccSide
{
    public DataTable AccSideSave
    (int CRUD, int SideType, string SideName, string NationalId, string EconomicCode, string CompanyRegId, string Tel,
        string Mobile,
        string Address, string PostalCode, string Email, string WebSite, int CurrencyType, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@SideType", SideType);
        com.Parameters.AddWithValue("@SideName", SideName);
        com.Parameters.AddWithValue("@NationalId", NationalId);
        com.Parameters.AddWithValue("@EconomicCode", EconomicCode);
        com.Parameters.AddWithValue("@CompanyRegId", CompanyRegId);
        com.Parameters.AddWithValue("@Tel", Tel);
        com.Parameters.AddWithValue("@Mobile", Mobile);
        com.Parameters.AddWithValue("@Address", Address);
        com.Parameters.AddWithValue("@PostalCode", PostalCode);
        com.Parameters.AddWithValue("@Email", Email);
        com.Parameters.AddWithValue("@WebSite", WebSite);
        com.Parameters.AddWithValue("@CurrencyType", CurrencyType);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spAccSide");
    }

    public DataTable AccSideSearch
        (int CRUD)


    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spAccSide");
    }

    public DataTable BondChangesList
        (int CRUD, object BondId)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@BondId", BondId);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spAccSide");
    }

    public void BondChandesSave
    (int CRUD, int BondId, int Id, string LetterNumber, object RegDate, object DeliveryDate, object StartDate,
        object EndDate, long BondAmount, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@BondId", BondId);
        com.Parameters.AddWithValue("@Id", Id);
        com.Parameters.AddWithValue("@LetterNumber", LetterNumber);
        com.Parameters.AddWithValue("@RegDate", RegDate);
        com.Parameters.AddWithValue("@DeliveryDate", DeliveryDate);
        com.Parameters.AddWithValue("@StartDate", StartDate);
        com.Parameters.AddWithValue("@EndDate", EndDate);
        com.Parameters.AddWithValue("@BondAmount", BondAmount);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringERP, com, "spAccSide");
    }
}