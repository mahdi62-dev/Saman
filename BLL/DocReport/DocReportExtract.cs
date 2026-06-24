using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.DocReport;

public class DocReportExtract
{
    public DataTable DocReportExtractSearch(object StartDate, object EndDate, int DepID, string SearchText,
        bool IsCaseSensitive,
        string SearchTitle, string EncID, string FirstName, string LastName, string NationalCode, int DrID,
        int EncService,
        int ChiefComplaint, int Question)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@StartDate", StartDate);
        com.Parameters.Add("@EndDate", EndDate);
        com.Parameters.Add("@DepID", DepID);
        com.Parameters.Add("@SearchText", SearchText);
        com.Parameters.Add("@IsCaseSensitive", IsCaseSensitive);
        com.Parameters.Add("@SearchTitle", SearchTitle);
        com.Parameters.Add("@EncID", EncID);
        com.Parameters.Add("@FirstName", FirstName);
        com.Parameters.Add("@LastName", LastName);
        com.Parameters.Add("@NationalCode", NationalCode);
        com.Parameters.Add("@DrID", DrID);
        com.Parameters.Add("@EncService", EncService);
        com.Parameters.Add("@ChiefComplaint", ChiefComplaint);
        com.Parameters.Add("@Question", Question);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringReport, com, "spDocExpertSearch");
    }
}