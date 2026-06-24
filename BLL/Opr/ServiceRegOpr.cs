using System;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Opr;

public class ServiceRegOpr
{
    public DataTable ServiceRegOprSave
    (byte CRUD, int PageID, int ServiceID, int ParentSRID, int SMID, int RecNo, int Num, int OprItem, int DrID,
        int HDrID, double KBase, byte KPer, bool IsCosmetic)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@PageID", PageID);
        com.Parameters.AddWithValue("@ServiceID", ServiceID);
        com.Parameters.AddWithValue("@ParentSRID", ParentSRID);
        com.Parameters.AddWithValue("@SMID", SMID);
        com.Parameters.AddWithValue("@RecNo", RecNo);
        com.Parameters.AddWithValue("@Num", Num);
        com.Parameters.AddWithValue("@OprItem", OprItem);

        com.Parameters.AddWithValue("@DrID", DrID);
        com.Parameters.AddWithValue("@HDrID", HDrID);

        com.Parameters.AddWithValue("@KBase", KBase);

        com.Parameters.AddWithValue("@KPer", KPer);
        com.Parameters.AddWithValue("@IsCosmetic", IsCosmetic);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spServiceRegOprSave");
    }

    public void ServiceRegOprModifierSave
    (byte CRUD, int PageID, int ServiceID, int ParentSRID, int SMID, int RecNo, int Num, int OprItem, int DrID,
        double KBase, byte KPer, bool IsCosmetic)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@PageID", PageID);
        com.Parameters.AddWithValue("@ServiceID", ServiceID);
        com.Parameters.AddWithValue("@ParentSRID", ParentSRID);
        com.Parameters.AddWithValue("@SMID", SMID);
        com.Parameters.AddWithValue("@RecNo", RecNo);
        com.Parameters.AddWithValue("@Num", Num);
        com.Parameters.AddWithValue("@OprItem", OprItem);

        com.Parameters.AddWithValue("@DrID", DrID);
        com.Parameters.AddWithValue("@KBase", KBase);

        com.Parameters.AddWithValue("@KPer", KPer);
        com.Parameters.AddWithValue("@IsCosmetic", IsCosmetic);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spServiceRegOprSave");
    }

    public DataTable ServiceRegLoad(int pageID)
    {
        var query = @"
SELECT 
sr.[EncID]
,sr.[PageID]
,2 AS CRUD
,sr.[SRID]
,sr.[ServiceID]
,sr.[ServiceCode]
,[RecNo]
,[ParentSRID]
,sro.[DrID]
,dr.Name AS DrName
,[OprItem]
,CASE WHEN SMID=0 THEN oi.Name ELSE N'تعدیلی ' + [ServiceModifierName] END AS OprItemName
,[KBase]
,[KPer]
,sro.[Num]
,[SMID]
,[KFinal]
,[ServiceName] AS Name
,s.AnesK AS BookAnesK
,IsCosMetic
FROM SamanHIS.[dbo].[vServiceRegOpr] sro
INNER JOIN dbo.vServiceReg sr on sr.SRID=sro.SRID
INNER JOIN dbo.vService s ON s.ServiceID = sr.ServiceID
INNER JOIN dbo.vDr dr on dr.DrID=sro.DrID
INNER JOIN dbo.vADR oi on oi.ID=sro.OprItem

where 1=1
AND oi.ADID=40
AND sr.PageID=@PageID

ORDER BY OprItem,SMID
";
        var com = new SqlCommand();
        com.Parameters.AddRange(new[]
        {
            new SqlParameter("@PageID", pageID)
        });
        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, query);
    }

    public int CalAnesKByTime(int AnesTime)
    {
        var k = 0;
        var mod = 0;

        //هر 15 دقیقه تا سال 1401
        //if (AnesTime <= 240)
        //{
        //    k = AnesTime / 15;
        //    mod = AnesTime % 15;
        //}


        //if (AnesTime > 240)
        //{
        //    k = 240 / 15 + (AnesTime - 240) / 10;
        //    mod = (AnesTime - 240) % 10;
        //}

        //هر 10 دقیقه از سال 1402
        //if (AnesTime <= 240)
        //{
        //    k = AnesTime / 10;
        //    mod = AnesTime % 10;
        //}


        //if (AnesTime > 240)
        //{
        //    k = 240 / 10 + (AnesTime - 240) / 10;
        //    mod = (AnesTime - 240) % 10;
        //}

        //هر 5 دقیقه از سال 1405
        //if (AnesTime <= 240)
        if (AnesTime >0)
        {
            k = AnesTime / 5;
            mod = AnesTime % 5;
        }

        //تا کتاب سه از  4 برداشته شد
        //if (AnesTime > 240)
        //{
        //    k = 240 / 5 + (AnesTime - 240) / 10;
        //    mod = (AnesTime - 240) % 5;
        //}


        if (mod >= 5) k += 1;

        return k;
    }

    public int CalRecoveryKByTime(int RecoveryTime)
    {
        var k = 0;

        k = RecoveryTime / 30;

        if (k > 4) k = 4;

        return k;
    }

    public int CalRecoveryKByOprTime(int OprTime, int AnesType)
    {
        var k = 0;
        if (AnesType == 2) return 0;

        var hour = Convert.ToInt32(OprTime / 60);

        var min = OprTime - hour * 60;


        k = hour;
        if (min > 30) k += 1;

        if (k > 4) k = 4;

        return k;
    }
}