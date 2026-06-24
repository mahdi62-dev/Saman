using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Pet;

public class PageAnsPet
{
    public DataTable PageAnsLoad(string Date1, string Date2)
    {
        var tsql = @"
                        SELECT *
                        FROM dbo.vPageAnsPet
                        where  PageDate BETWEEN @Date1 and @Date2
                        ORDER BY PageDate Desc

                        ";
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Date1", Date1);
        com.Parameters.AddWithValue("@Date2", Date2);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, tsql);
    }

    public DataTable PageAnsSearch(object PageDate1, object PageDate2, object FullName)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@PageDate1", PageDate1);
        com.Parameters.AddWithValue("@PageDate2", PageDate2);
        com.Parameters.AddWithValue("@FullName", FullName);

        //com.Parameters.AddWithValue("@StartDate", StartDate);
        //com.Parameters.AddWithValue("@EndDate", EndDate);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spPageAnsSearch");
    }
}