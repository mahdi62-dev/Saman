using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Page;

public class Page
{
    public DataTable PageSave
    (int ModID, int DepID, int StorageID, int StoragePeriodID, int EncID, int UserPersID, int PageID,
        string Date2, string Date3, int Dr2ID, int DrTechAssist)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@ModID", ModID);
        com.Parameters.AddWithValue("@DepID", DepID);
        com.Parameters.AddWithValue("@StorageID", StorageID);
        com.Parameters.AddWithValue("@StoragePeriodID", StoragePeriodID);
        com.Parameters.AddWithValue("EncID", EncID);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);
        com.Parameters.AddWithValue("@PageID", PageID);
        com.Parameters.AddWithValue("@Date2", Date2);
        com.Parameters.AddWithValue("@Date3", Date3);
        com.Parameters.AddWithValue("@Dr2ID", Dr2ID);
        com.Parameters.AddWithValue("@DrTechAssist", DrTechAssist);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spPageSave");
    }

    public DataTable EncPageLoad(int EncID, int ModID, int DepID, int PageID)
    {
        var tsql = @"
            SELECT 
            ROW_NUMBER() OVER (PARTITION BY EncID ORDER BY PageID) AS RecNo
               ,p.*
              FROM [Page] p
            ";

        var com = new SqlCommand();
        if (PageID == 0)
        {
            tsql += "WHERE EncID=@EncID AND ModID=@ModID AND DepID=@DepID";
            com.Parameters.Add("@EncID", EncID);
            com.Parameters.Add("@ModID", ModID);
            com.Parameters.Add("@DepID", DepID);
        }

        if (PageID > 0)
        {
            tsql += "WHERE PageID=@PageID";
            com.Parameters.Add("@PageID", PageID);
        }

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, tsql);
    }


    public DataTable PageAmountSave
        (int PageID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@PageID", PageID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spPageAmountSave");
    }


    public void PageTeamSave
        (int CRUD, int PageID, int PersID, int RoleID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@PageID", PageID);
        com.Parameters.AddWithValue("@PersID", PersID);
        com.Parameters.AddWithValue("@RoleID", RoleID);
        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spPageTeamSave");
    }

    public DataTable PageTeamLoad
        (int PageID)
    {
        var query = @"
                        SELECT
                        2 AS CRUD
                        ,[PageID]
                        ,pt.[PersID]
                        ,pers.Name AS PersName
                         ,[RoleID]
                        ,tr.Name AS RoleName

                        FROM SamanHIS.[dbo].[PageTeam] pt
                            INNER JOIN vPersonnel pers on pers.PersID = pt.PersID
                        INNER JOIN ADR tr on tr.ID = pt.RoleID

                        WHERE tr.ADID = 39
                        AND pt.PageID = @PageID
                        ";
        var com = new SqlCommand();
        com.Parameters.AddRange(new[]
        {
            new SqlParameter("@PageID", PageID)
        });
        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, query);
    }


    public DataTable PageCashLoad(int PageID, int DepID, int PageCode)
    {
        var TSQL = "SELECT * FROM vPage ";
        var strWhere = "WHERE ";

        var com = new SqlCommand();
        if (PageID > 0)
        {
            strWhere += "PageID = @PageID";
            com.Parameters.Add("@PageID", PageID);
        }
        else
        {
            strWhere += "DepID=@DepID AND PageCode=@PageCode";
            com.Parameters.Add("@DepID", DepID);
            com.Parameters.Add("@PageCode", PageCode);
        }

        TSQL += strWhere;

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, TSQL);
    }

    public DataTable CashLoadByCashID(int CashID)
    {
        var TSQL = "SELECT * FROM Cash WHERE CashID=@CashID";

        var com = new SqlCommand();
        com.Parameters.Add("@CashID", CashID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, TSQL);
    }
}