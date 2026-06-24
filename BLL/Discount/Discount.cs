using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Discount;

internal class Discount
{
    public DataTable DiscountSave
    (int CRUD, int ModID, int EncID, int PageID, bool IsDisable, int DiscountEncType,
        int DiscountReferType, int DiscountActionTaken, int DiscountType, int DepID, long DiscountAmount,
        string Note, int UserPersID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@ModID", ModID);
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@PageID", PageID);
        com.Parameters.AddWithValue("@IsDisable", IsDisable);
        com.Parameters.AddWithValue("@DiscountEncType", DiscountEncType);
        com.Parameters.AddWithValue("@DiscountReferType", DiscountReferType);
        com.Parameters.AddWithValue("@DiscountActionTaken", DiscountActionTaken);
        com.Parameters.AddWithValue("@DiscountType", DiscountType);
        com.Parameters.AddWithValue("@DepID", DepID);
        com.Parameters.AddWithValue("@DiscountAmount", DiscountAmount);
        com.Parameters.AddWithValue("@Note", Note);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spDiscount");
    }

    public DataTable DiscountLoad(object CRUD, object PageID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@PageID", PageID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spDiscount");
    }
}