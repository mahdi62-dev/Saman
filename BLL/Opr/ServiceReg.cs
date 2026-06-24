using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Saman.BLL.Opr
{
   public class ServiceReg
    {

        public void ServiceRegOpr
       (int CRUD, int PageID, int ServiceID, int Num, float ProK, float AnesK, float TechK, byte KPer)
        {
            var con = new SqlConnection { ConnectionString = DAL.cSet.cnString };

            var cmd = new SqlCommand("spServiceRegOprSave")
            {
                Connection = con,
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CRUD", CRUD);
            cmd.Parameters.AddWithValue("@PageID", PageID);
            cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
            cmd.Parameters.AddWithValue("@Num", Num);
            cmd.Parameters.AddWithValue("@ProK", ProK);
            cmd.Parameters.AddWithValue("@AnesK", AnesK);
            cmd.Parameters.AddWithValue("@TechK", TechK);
            cmd.Parameters.AddWithValue("@KPer", KPer);

            var dalData = new DAL.Data();
            dalData.ExecuteNonQueryByCommand(cmd);

        }



    }
}
