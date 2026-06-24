using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Percase;

public class PersonPercase
{
    public DataTable SPPersonPercase(object CRUD, object Year, object Mon, object RegID, object ManagerAmount,
        object SDrAmount,
        object ADrAmount, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);
        com.Parameters.AddWithValue("@RegID", RegID);
        com.Parameters.AddWithValue("@ManagerAmount", ManagerAmount);
        com.Parameters.AddWithValue("@SDrAmount", SDrAmount);
        com.Parameters.AddWithValue("@ADrAmount", ADrAmount);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spPersonPercase");
    }


    public DataTable SPPersonPercase2(object Year, object Mon, object RegID, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);
        com.Parameters.AddWithValue("@RegID", RegID);

        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spPersonPercase2");
    }

    public DataTable PersonPercaseReg(object CRUD, object RegID, object IsSign, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@RegID", RegID);
        com.Parameters.AddWithValue("@IsSign", IsSign);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spPersonPercaseReg");
    }

    public DataTable PersonPercaseAmountFromDrPercase(object Year, object Mon)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spPersonPercaseAmountFromDrPercase");
    }

    public void PersonPercaseItemSave
    (object CRUD, object RegID, object PersID, object ItemID, object ItemVal, object ModifierAmount,
        object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@RegID", RegID);
        com.Parameters.AddWithValue("@PersID", PersID);
        com.Parameters.AddWithValue("@ItemID", ItemID);
        com.Parameters.AddWithValue("@ItemVal", ItemVal);
        com.Parameters.AddWithValue("@ModifierAmount", ModifierAmount);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringERP, com, "spPersonPercaseItemSave");
    }
}