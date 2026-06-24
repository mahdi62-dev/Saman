using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Percase;

public class ADrPercase
{
    public DataTable SPADrPercase(object CRUD, object Year, object Mon, object RegID, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);
        com.Parameters.AddWithValue("@RegID", RegID);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spADrPercase");
    }

    public DataTable ADrPercaseItemSave
        (object RegID, object DrID, object TotalAnesK, object DayCount, object ADrPercaseAmount, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@RegID", RegID);
        com.Parameters.AddWithValue("@DrID", DrID);
        com.Parameters.AddWithValue("@TotalAnesK", TotalAnesK);
        com.Parameters.AddWithValue("@DayCount", DayCount);
        com.Parameters.AddWithValue("@ADrPercaseAmount", ADrPercaseAmount);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spADrPercaseItemSave");
    }


    //public DataTable SPADrPercase(object CRUD, object Year, object Mon, object RegID, object DrID,
    //    object DrName, object ShareCount, object AnesCount, object DayCount, object TotalAnesK
    //    , object ShareAmount, object ADrPercase, object TotalAmount, object ManagerAmount, object TaxAmount
    //    , object PersonPercaseAmount, object KosurAmount, object PosAmount, object PureAmount, object UserPersID)
    //{
    //    var com = new SqlCommand();
    //    com.Parameters.AddWithValue("@CRUD", CRUD);
    //    com.Parameters.AddWithValue("@Year", Year);
    //    com.Parameters.AddWithValue("@Mon", Mon);
    //    com.Parameters.AddWithValue("@RegID", RegID);
    //    com.Parameters.AddWithValue("@DrID", DrID);
    //    com.Parameters.AddWithValue("@DrName", DrName);
    //    com.Parameters.AddWithValue("@ShareCount", ShareCount);
    //    com.Parameters.AddWithValue("@AnesCount", AnesCount);
    //    com.Parameters.AddWithValue("@DayCount", DayCount);
    //    com.Parameters.AddWithValue("@TotalAnesK", TotalAnesK);
    //    com.Parameters.AddWithValue("@ShareAmount", ShareAmount);
    //    com.Parameters.AddWithValue("@ADrPercase", ADrPercase);
    //    com.Parameters.AddWithValue("@TotalAmount", TotalAmount);
    //    com.Parameters.AddWithValue("@ManagerAmount", ManagerAmount);
    //    com.Parameters.AddWithValue("@TaxAmount", TaxAmount);
    //    com.Parameters.AddWithValue("@PersonPercaseAmount", PersonPercaseAmount);
    //    com.Parameters.AddWithValue("@KosurAmount", KosurAmount);
    //    com.Parameters.AddWithValue("@PosAmount", PosAmount);
    //    com.Parameters.AddWithValue("@PureAmount", PureAmount);
    //    com.Parameters.AddWithValue("@UserPersID", UserPersID);


    //    var dalData = new DAL.Data();
    //    return dalData.GetDataTableBySP(DAL.cSet.cnStringERP, com, "spADrPercase");
    //}

    public DataTable ADrPercaseReg(object RegID, object IsSign, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@RegID", RegID);
        com.Parameters.AddWithValue("@IsSign", IsSign);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spPersonPercaseReg");
    }

    public DataTable ADrPercaseAmountFromDrPercase(object Year, object Mon)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spPersonPercaseAmountFromDrPercase");
    }

    public void ADrPercaseItemSave
        (object RegID, object PersID, object ItemID, object ItemVal, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@RegID", RegID);
        com.Parameters.AddWithValue("@PersID", PersID);
        com.Parameters.AddWithValue("@ItemID", ItemID);
        com.Parameters.AddWithValue("@ItemVal", ItemVal);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringERP, com, "spPersonPercaseItemSave");
    }
}