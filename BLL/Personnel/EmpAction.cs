using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Personnel;

public class EmpAction
{
    public DataTable GetEmpActionItem(int EmpActionID)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@EmpActionID", EmpActionID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spEmpActionItemGet");
    }

    public DataTable EmpActionSave
    (object EmpActionID, object PersID, object ContractStartDate, object ContractEndDate,
        object StartDate, object EndDate, object EmpActionTitle, object Description,
        object ContractType, object PersType)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EmpActionID", EmpActionID);
        com.Parameters.AddWithValue("@PersID", PersID);
        com.Parameters.AddWithValue("@ContractStartDate", ContractStartDate);
        com.Parameters.AddWithValue("@ContractEndDate", ContractEndDate);
        com.Parameters.AddWithValue("@StartDate", StartDate);
        com.Parameters.AddWithValue("@EndDate", EndDate);
        com.Parameters.AddWithValue("@EmpActionTitle", EmpActionTitle);
        com.Parameters.AddWithValue("@ContractType", ContractType);
        com.Parameters.AddWithValue("@PersType", PersType);
        com.Parameters.AddWithValue("@Description", Description);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spEmpActionSave");
    }

    public DataTable EmpActionLoad(int EmpActionID)
    {
        var query = @"
                        SELECT *
                        FROM SamanERP.dbo.vEmpAction
                        WHERE EmpActionID=@EmpActionID
        ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EmpActionID", EmpActionID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, query);
    }

    public DataTable PersEmpActionLoad(int PersID)
    {
        var query = @"
                           SELECT *
                           FROM SamanERP.dbo.vEmpAction
                           WHERE PersID=@PersID
                            ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@PersID", PersID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, query);
    }


    public DataTable EmpActionItemSave
        (object EmpActionID, object ItemID, object ItemVal, object ItemK)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EmpActionID", EmpActionID);
        com.Parameters.AddWithValue("@ItemID", ItemID);
        com.Parameters.AddWithValue("@ItemVal", ItemVal);
        com.Parameters.AddWithValue("@ItemK", ItemK);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spEmpActionItemSave");
    }

    public void EmpActionDel(object EmpActionID, object ItemID)
    {
        var tsql = "DELETE FROM dbo.EmpActionItem WHERE EmpActionID=@EmpActionID AND ItemID=@ItemID";
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EmpActionID", EmpActionID);
        com.Parameters.AddWithValue("@ItemID", ItemID);


        var dalData = new Data();
        dalData.ExecuteNonQueryByText(cSet.cnStringERP, com, tsql);
    }


    public DataTable PerssonelLoad()
    {
        var query = @"
            SELECT 
            p.*,
            ISNULL(ea.EmpActionID,0) AS EmpActionID
            FROM dbo.vPersonnelFull p
            LEFT OUTER JOIN EmpAction ea on ea.PersId = p.PersID
            ";

        var com = new SqlCommand();

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, query);
    }
}