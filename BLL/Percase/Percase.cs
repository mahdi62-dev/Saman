using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Percase;

public class Percase
{
    public DataTable CreateDrPercase(string StartDate, string EndDate, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@StartDate", StartDate);
        com.Parameters.AddWithValue("@EndDate", EndDate);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spDrPercaseRegSave");
    }

    public DataTable DrPercaseSummaryLoad(object RegID)
    {
        var query = @"
SELECT *
FROM dbo.vDrPercaseSummary
WHERE RegID = @RegID
";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@RegID", RegID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, query);
    }

    public DataTable DrPercaseSummaryCheckExistance(object StartDate, object EndDate)
    {
        var query = @"

SELECT TOP(1) *
  FROM dbo.DrPercaseReg
  WHERE 
  @StartDate BETWEEN StartDate AND EndDate OR @EndDate BETWEEN StartDate AND EndDate 
  ORDER BY RegID DESC
";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@StartDate", StartDate);
        com.Parameters.AddWithValue("@EndDate", EndDate);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, query);
    }


    public DataTable DrPercaseSummaryCheckIsSignAllByReg(int RegID)
    {
        var query = @"
                            SELECT  *
                              FROM dbo.DrPercaseReg
                              WHERE  IsSign = 0 AND RegID <> @RegID
                              ORDER BY RegID DESC
                            ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@RegID", RegID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, query);
    }

    public DataTable DrPercaseSummaryCheckIsSignAll()
    {
        var query = @"
                            SELECT  *
                              FROM dbo.DrPercaseReg
                              WHERE  IsSign = 0
                              ORDER BY RegID DESC
                            ";

        var com = new SqlCommand();

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, query);
    }


    public DataTable DrPercaseDetailLoad(object RegID, object DrID)
    {
        var query = @"
SELECT *
FROM dbo.vIncomeDetail
WHERE RegID = @RegID AND DrID = @DrID
";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@RegID", RegID);
        com.Parameters.AddWithValue("@DrID", DrID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, query);
    }


    public DataTable DrPercaseRegSign(object RegID, object IsSign, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@RegID", RegID);
        com.Parameters.AddWithValue("@IsSign", IsSign);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spDrPercaseRegSign");
    }
}