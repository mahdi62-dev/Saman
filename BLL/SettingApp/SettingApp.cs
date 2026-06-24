using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.SettingApp
{
    public class SettingApp
    {
        public DataTable SettingAppLoad(int UserID)
        {
            string TSQL = @"
                            SELECT *
                            FROM dbo.vSettingApp
                            WHERE UserID=@UserID
                           ";

            SqlCommand com = new SqlCommand();
            com.Parameters.Add("@UserID", UserID);

            Data dalData = new Data();
            return dalData.GetDataTableByText(cSet.cnStringERP, com, TSQL);
        }
    }
}