using System.Data;
using System.Data.SqlClient;

namespace Saman.BLL.Def
{
   public class ServiceDef
    {
        public DataTable ServiceLoad(int ServiceID)
        {
            var strTSql = @"
                       SELECT 
	                    s.[ServiceID]
	                    ,s.[ServiceCode]
                        ,s.[ServiceCode2]
	                    ,s.[ServiceType]
	                    ,s.[ServiceCategory]
	                    ,scat.Name as ServiceCategoryName
	                    ,s.[Name]
	                    ,s.[TotalK]
	                    ,s.[ProK]
	                    ,s.[TechK]
	                    ,s.[AnesK]
	                    ,s.[Price]
	                    ,s.[PricePro]
	                    ,s.[PriceTech]
	                    ,s.[MapIDA]
	                    ,s.[StartDate]
	                    ,s.[EndDate]
	                    ,s.[IsTech]
	                    ,s.[IsManualPrice]
	                    ,s.[ServicePriceType]
                    FROM [SamanHIS].[dbo].[vService] s
                    inner join dbo.vServiceCategory scat ON scat.ID= s.ServiceCategory
                    WHERE 1=1

                        ";
            string where = " AND s.ServiceType=2";

            if (ServiceID > 0)
                where = where + " AND ServiceID=" + ServiceID;

            strTSql = strTSql + where;

            var com = new SqlCommand();
           
            var dalData = new DAL.Data();
            return dalData.GetDataTableByText(DAL.cSet.cnStringA, com, strTSql);
        }

        public DataTable ServiceLoad(int ServiceType, int ServiceID)
        {
            var strTSql = @"
                       SELECT *
                    FROM ServiceTable s
                    inner join dbo.vServiceCategory scat ON scat.ID= s.ServiceCategory
                    WHERE s.ServiceType=@ServiceType AND s.ServiceID=@ServiceID

                        ";
            string fromTable = null;
            switch (ServiceType)
            {
                case 1:
                    fromTable = "vService";
                    break;
                case 2:
                    fromTable = "vServiceDrug";
                    break;
            }

            strTSql = strTSql.Replace("ServiceTable", fromTable);

            var com = new SqlCommand();
            com.Parameters.AddWithValue("@ServiceType", ServiceType);
            com.Parameters.AddWithValue("@ServiceID", ServiceID);

            var dalData = new DAL.Data();
            return dalData.GetDataTableByText(DAL.cSet.cnStringA, com, strTSql);
        }

        public DataTable ServiceSave
            (int CRUD, int ServiceID, int ServiceType, int ServiceCategory, string ServiceCode, string ServiceCode2, string Name,
            string NameLong, int Price, int ServiceUnit, int ServiceStatus, bool IsDisable
            )
        {
            var com = new SqlCommand();
            com.Parameters.AddWithValue("@CRUD", CRUD);
            com.Parameters.AddWithValue("@ServiceID", ServiceID);
            com.Parameters.AddWithValue("@ServiceType", ServiceType);
            com.Parameters.AddWithValue("@ServiceCategory", ServiceCategory);
            com.Parameters.AddWithValue("@ServiceCode", ServiceCode);
            com.Parameters.AddWithValue("@ServiceCode2", ServiceCode2);
            com.Parameters.AddWithValue("@Name", Name);
            com.Parameters.AddWithValue("@NameLong", NameLong);
            com.Parameters.AddWithValue("@Price", Price);
            com.Parameters.AddWithValue("@ServiceUnit", ServiceUnit);
            com.Parameters.AddWithValue("@ServiceStatus", ServiceStatus);
            com.Parameters.AddWithValue("@IsDisable", IsDisable);

            var dalData = new DAL.Data();
            return dalData.GetDataTableByText(DAL.cSet.cnStringA, com, "spServiceSave");
        }

        public DataTable GetEncBillFull(int EncID)
        {
            var query = @"
                        SELECT *
                        FROM vEncBillFull
                        WHERE EncID=@EncID
                        ";
            var com = new SqlCommand();
            com.Parameters.AddWithValue("@EncID", EncID);

            var dalData = new DAL.Data();
            return dalData.GetDataTableByText(DAL.cSet.cnStringA, com, query);
        }

    }
}
