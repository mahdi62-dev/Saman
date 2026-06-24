using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Acc.Bond;

public class Bond
{
    public DataTable BondSave
    (int CRUD, int BranchId, int BondType, int AccSide, int BondReason, int BondCategory, string BondSerial,
        object RegDate, object DeliveryDate, object StartDate, object EndDate, long BondAmount, int CurrencyType,
        string ContractId, string Tender, string Subject, string Description, int Bank, string BankBranch,
        int BondStatus, object AccountSubId, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@BranchId", BranchId);
        com.Parameters.AddWithValue("@BondType", BondType);
        com.Parameters.AddWithValue("@AccSide", AccSide);
        com.Parameters.AddWithValue("@BondReason", BondReason);
        com.Parameters.AddWithValue("@BondCategory", BondCategory);
        com.Parameters.AddWithValue("@BondSerial", BondSerial);
        com.Parameters.AddWithValue("@RegDate", RegDate);
        com.Parameters.AddWithValue("@DeliveryDate", DeliveryDate);
        com.Parameters.AddWithValue("@StartDate", StartDate);
        com.Parameters.AddWithValue("@EndDate", EndDate);
        com.Parameters.AddWithValue("@BondAmount", BondAmount);
        com.Parameters.AddWithValue("@CurrencyType", CurrencyType);
        com.Parameters.AddWithValue("@ContractId", ContractId);
        com.Parameters.AddWithValue("@Tender", Tender);
        com.Parameters.AddWithValue("@Subject", Subject);
        com.Parameters.AddWithValue("@Description", Description);
        com.Parameters.AddWithValue("@Bank", Bank);
        com.Parameters.AddWithValue("@BankBranch", BankBranch);
        com.Parameters.AddWithValue("@BondStatus", BondStatus);
        com.Parameters.AddWithValue("@AccountSubId", AccountSubId);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spAccBond");
    }

    public DataTable BondSearch
    (int CRUD, object BondId, int BranchId, int BondType, int AccSide, int BondReason, int BondCategory,
        string BondSerial,
        string StartDate, string StartDate2, string EndDate, string EndDate2, string ContractId, string Tender,
        string Subject, string Description, int Bank, string BankBranch, int BondStatus, object AccountSubId)


    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@BondId", BondId);
        com.Parameters.AddWithValue("@BranchId", BranchId);
        com.Parameters.AddWithValue("@BondType", BondType);
        com.Parameters.AddWithValue("@AccSide", AccSide);
        com.Parameters.AddWithValue("@BondReason", BondReason);
        com.Parameters.AddWithValue("@BondCategory", BondCategory);
        com.Parameters.AddWithValue("@BondSerial", BondSerial);
        com.Parameters.AddWithValue("@StartDate", StartDate);
        com.Parameters.AddWithValue("@StartDate2", StartDate2);
        com.Parameters.AddWithValue("@EndDate", EndDate);
        com.Parameters.AddWithValue("@EndDate2", EndDate2);
        com.Parameters.AddWithValue("@ContractId", ContractId);
        com.Parameters.AddWithValue("@Tender", Tender);
        com.Parameters.AddWithValue("@Subject", Subject);
        com.Parameters.AddWithValue("@Description", Description);
        com.Parameters.AddWithValue("@Bank", Bank);
        com.Parameters.AddWithValue("@BankBranch", BankBranch);
        com.Parameters.AddWithValue("@BondStatus", BondStatus);
        com.Parameters.AddWithValue("@AccountSubId", AccountSubId);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spAccBond");
    }

    public DataTable BondChangesList
        (int CRUD, object BondId)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@BondId", BondId);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spAccBondChanges");
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
        dalData.ExecuteNonQueryBySP(cSet.cnStringERP, com, "spAccBondChanges");
    }
}