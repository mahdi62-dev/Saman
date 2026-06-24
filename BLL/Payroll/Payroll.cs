using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Payroll;

public class Payroll
{
    public DataTable GetPayrollItem(object Year, object Mon, object ItemID)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@Year", Year);
        com.Parameters.Add("@Mon", Mon);
        com.Parameters.Add("@ItemID", ItemID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spPayrollItemGet");
    }

    public DataTable PayrollItemSave
        (object Year, object Mon, object PersID, object ItemID, object ItemVal, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);
        com.Parameters.AddWithValue("@PersID", PersID);
        com.Parameters.AddWithValue("@ItemID", ItemID);
        com.Parameters.AddWithValue("@ItemVal", ItemVal);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spPayrollItemSave");
    }


    public void CreateSalary
        (object Year, object Mon, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        dalData.GetDataTableBySP(cSet.cnStringERP, com, "spCreateSalary");
    }


    public DataTable PayrollItemSummaryLoad
        (object Year, object Mon)
    {
        var TSQL = "SELECT * FROM vPayrollSummaryTotal WHERE Year=@Year AND Mon=@Mon 	ORDER BY SortID";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, TSQL);
    }

    public DataTable SalaryTotalLoad
        (object Year, object Mon)
    {
        var TSQL = "SELECT * FROM vSalaryTotal WHERE Year=@Year AND Mon=@Mon ORDER BY SortID";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, TSQL);
    }

    public DataTable SalaryRegLoad
        (object Year, object Mon)
    {
        var TSQL = "SELECT * FROM vSalaryReg WHERE Year=@Year AND Mon=@Mon";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, TSQL);
    }


    public DataTable SalaryRegSign(object Year, object mon, object IsSign, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@mon", mon);
        com.Parameters.AddWithValue("@IsSign", IsSign);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spSalaryRegSign");
    }
}