using System.Data;
using System.Data.SqlClient;

namespace DAL;

public class LabMain
{
    public static void InsRecord
        (byte[] BinFile)
    {
        var parm = new SqlParameter[12];
        parm[0] = new SqlParameter("@AdmCode", 21);
        parm[1] = new SqlParameter("@Date", "1390/07/06 00:00");
        parm[2] = new SqlParameter("@ScanType", 2);
        parm[3] = new SqlParameter("@BinFile", BinFile);

        const string strIns = "Insert Into AdmScan (AdmCode, Date, ScanType, BinFile) " +
                              "Select @AdmCode, @Date, @ScanType, @BinFile";

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strIns, parm);
    }

    //====================================================================================================================

    public static SqlDataReader Paper(int PaperIntCode)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@PaperIntCode", PaperIntCode);
        var strSel = "Select Top 1 * From [Paper View] Where [Paper Int Code]=@PaperIntCode";

        var readScan = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text, strSel, parm);
        return readScan;
    }

    //====================================================================================================================

    public static SqlDataReader AdmData(int admCode)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@AdmCode", admCode);
        const string strSel = "Select Top 1 * From [Adm] Where [Adm Code]=@AdmCode";

        var admData = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text, strSel, parm);
        return admData;
    }

    //====================================================================================================================
    public static void AddServiceToDsTable
        (DataSet ds, int seriveIntCode, string serviceName, int num, int servicePrice)
    {
        const string tableName = "GiveService";
        var newRow = ds.Tables[tableName].NewRow();

        newRow["fType"] = 1;
        newRow["GS-ID"] = 0;
        newRow["Code"] = 0;
        newRow["Service Int Code"] = seriveIntCode;
        newRow["Name"] = serviceName;
        newRow["num"] = num;
        newRow["SumPrice"] = servicePrice;
        newRow["Note"] = "";
        newRow["Price"] = 0;
        newRow["Parent GS-ID"] = 0;
        newRow["isDrug"] = 0;
        newRow["DPersonCode"] = 0;

        ds.Tables[tableName].Rows.Add(newRow);

        //string tmpTableName = "tmpGiveService";
        //DataRow tmpNewRow = ds.Tables[tmpTableName].NewRow();
        //tmpNewRow["IntCode"] = seriveIntCode;
        //tmpNewRow["Num"] = num;

        //ds.Tables[tmpTableName].Rows.Add(tmpNewRow);
    }

    //===================================================================================================================
    public static void CreateGiveServiceTable
        (DataSet ds)
    {
        const string tableName = "GiveService";
        const string tmpTableName = "tmpDelGiveService";

        //if (ds.Tables[tableName] != null)
        //ds.Tables[tableName].Clear();

        if (ds.Tables[tableName] == null)
        {
            ds.Tables.Add(tableName);

            ds.Tables[tableName].Columns.Add("fType");
            ds.Tables[tableName].Columns.Add("GS-ID");
            ds.Tables[tableName].Columns.Add("Code");
            ds.Tables[tableName].Columns.Add("Service Int Code");
            ds.Tables[tableName].Columns.Add("Name");
            ds.Tables[tableName].Columns.Add("Num");
            ds.Tables[tableName].Columns.Add("SumPrice");
            ds.Tables[tableName].Columns.Add("Note");
            ds.Tables[tableName].Columns.Add("Price");
            ds.Tables[tableName].Columns.Add("Parent GS-ID");
            ds.Tables[tableName].Columns.Add("isDrug");
            ds.Tables[tableName].Columns.Add("DPersonCode");
        }


        //if (ds.Tables[tmpTableName] != null)
        //ds.Tables[tmpTableName].Clear();

        if (ds.Tables[tmpTableName] == null)
        {
            ds.Tables.Add(tmpTableName);
            ds.Tables[tmpTableName].Columns.Add("GSId");
            ds.Tables[tmpTableName].Columns.Add("fType");
        }
    }

    //===================================================================================================================

    public static void AddLabToGiveService
        (DataSet ds, int paperIntCode)
    {
        const string tableName = "GiveService";
        var count = ds.Tables[tableName].Rows.Count;

        if (count > 0)
        {
            var dPersonCode = 0;


            for (var i = 0; i < count; i++)
                if (ds.Tables[tableName].Rows[i].RowState != DataRowState.Deleted)
                {
                    var fType = byte.Parse(ds.Tables[tableName].Rows[i]["fType"].ToString());
                    if (fType == 1)
                    {
                        var IntCode = int.Parse(ds.Tables[tableName].Rows[i]["Service Int Code"].ToString());
                        var parm = new SqlParameter[5];
                        parm[0] = new SqlParameter("@PaperIntCode", paperIntCode);
                        parm[1] = new SqlParameter("@IntCode", IntCode);
                        parm[2] = new SqlParameter("@IsPrice2", null);
                        parm[3] = new SqlParameter("@DPersonCode", dPersonCode);

                        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.StoredProcedure,
                            "AddLabToGiveService", parm);
                    }
                }
        }

        count = ds.Tables["tmpDelGiveService"].Rows.Count;

        if (count > 0)
        {
            for (var i = 0; i < count; i++)
            {
                var gsId = int.Parse(ds.Tables["tmpDelGiveService"].Rows[i]["GsId"].ToString());
                var parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@GSID", gsId);

                SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.StoredProcedure,
                    "RemoveLabService", parm);
            }

            ds.Tables["tmpDelGiveService"].Clear();
        }
    }

    //===================================================================================================================

    public static void SavePaper
    (int AdmCode, string Date, int DrCode, int PaperIntCode, byte PType, int MBimehCode,
        int AdmPrice, int TakhfifPrice, int WardCode, string Date2, string Date3, int DrNCode,
        bool isOrder, bool isAns, int AnbarCode, byte PrgCode, bool isHospital, int PersCode,
        bool isDrugAdmPrice, bool isOTC, bool isCalAdmPrice)
    {
        var parm = new SqlParameter[22];
        parm[0] = new SqlParameter("@AdmCode", AdmCode);
        parm[1] = new SqlParameter("@Date", Date);
        parm[2] = new SqlParameter("@DrCode", DrCode);
        parm[3] = new SqlParameter("@PaperIntCode", PaperIntCode);
        parm[4] = new SqlParameter("@PType", PType);
        parm[5] = new SqlParameter("@MBimehCode", MBimehCode);
        parm[6] = new SqlParameter("@AdmPrice", AdmPrice);
        parm[7] = new SqlParameter("@TakhfifPrice", TakhfifPrice);
        parm[8] = new SqlParameter("@WardCode", WardCode);
        parm[9] = new SqlParameter("@Date2", Date2);
        parm[10] = new SqlParameter("@Date3", Date3);
        parm[11] = new SqlParameter("@DrNCode", DrNCode);
        parm[12] = new SqlParameter("@IsOrder", isOrder);
        parm[13] = new SqlParameter("@IsAns", isAns);
        parm[14] = new SqlParameter("@AnbarCode", AnbarCode);
        parm[15] = new SqlParameter("@PrgCode", PrgCode);
        parm[16] = new SqlParameter("@IsHospital", isHospital);
        parm[17] = new SqlParameter("@PersCode", PersCode);
        parm[19] = new SqlParameter("@isDrugAdmPrice", isDrugAdmPrice);
        parm[20] = new SqlParameter("@IsOTC", isOTC);
        parm[21] = new SqlParameter("@IsCalAdmPrice", isCalAdmPrice);

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.StoredProcedure,
            "SavePaper", parm);
    }

    //===================================================================================================================

    public static void SaveFishBank
        (int paperIntCode, string fishNum, int fishPrice)
    {
        var parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@PaperIntCode", paperIntCode);
        parm[1] = new SqlParameter("@FishNum", fishNum);
        parm[2] = new SqlParameter("@PayPrice", fishPrice);
        parm[3] = new SqlParameter("@FishPrice", fishPrice);

        const string strUpd =
            "Update Paper Set FishNum=@FishNum, FishPrice=@FishPrice, [Pay Price]=@PayPrice Where [Paper Int Code]=@PaperIntCode";
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strUpd, parm);
    }

    //===================================================================================================================

    public static int AddNewPaper
    (int admCode, int partCode, byte prgCode, int anbarCode, int drCode, int persCode,
        char paperCode3Prefix, int anbarPeriodCode, int wardCode, int bimehPageNum)
    {
        var curDate = "0000/00/00 00:00";
        var parm = new SqlParameter[12];
        parm[0] = new SqlParameter("@AdmCode", admCode);
        parm[1] = new SqlParameter("@PartCode", partCode);
        parm[2] = new SqlParameter("@PrgCode", prgCode);
        parm[3] = new SqlParameter("@AnbarCode", anbarCode);
        parm[4] = new SqlParameter("@DrCode", drCode);
        parm[5] = new SqlParameter("@PersonCode", persCode);
        parm[6] = new SqlParameter("@CurDate", curDate);
        parm[7] = new SqlParameter("@PaperCode3Prefix", paperCode3Prefix);
        parm[8] = new SqlParameter("@AnbarPeriodCode", anbarPeriodCode);
        parm[9] = new SqlParameter("@WardCode", wardCode);
        parm[10] = new SqlParameter("@BimehPageNum", bimehPageNum);
        parm[11] = new SqlParameter("@PaperIntCode", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };


        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.StoredProcedure,
            "AddNewPaper", parm);

        return int.Parse(parm[11].Value.ToString());
    }

    //===================================================================================================================
}