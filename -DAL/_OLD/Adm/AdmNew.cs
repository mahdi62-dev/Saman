using System.Data;
using System.Data.SqlClient;

namespace DAL;

public class AdmNew
{
    public static int AddReAdmPatient
    (int idCode, byte pType, int persCode, byte oprType, int wardCode, int bimehCode, string bimehNum, int partCode,
        bool isEmerg, byte age, byte ageMon, byte ageDay, bool isBimehEndDate, bool isVip, int drCode,
        string mBimehNum)
    {
        var admCode = 0;
        var parm = new SqlParameter[18];
        parm[0] = new SqlParameter("@pIDCode", idCode);
        parm[1] = new SqlParameter("@pPType", pType);
        parm[2] = new SqlParameter("@pPersCode", persCode);
        parm[3] = new SqlParameter("@pOprType", oprType);
        parm[4] = new SqlParameter("@pAdmWardCode", wardCode);
        parm[5] = new SqlParameter("@pBimehCode", bimehCode);
        parm[6] = new SqlParameter("@pBimehNum", bimehNum);
        parm[7] = new SqlParameter("@pPartCode", partCode);
        parm[8] = new SqlParameter("@isEmerg", isEmerg);
        parm[9] = new SqlParameter("@Age", age);
        parm[10] = new SqlParameter("@AgeMon", ageMon);
        parm[11] = new SqlParameter("@AgeDay", ageDay);
        parm[12] = new SqlParameter("@pisBimehEndDate", isBimehEndDate);
        parm[13] = new SqlParameter("@isVIP", isVip);
        parm[14] = new SqlParameter("@DrCode", drCode);
        parm[15] = new SqlParameter("@MBimehNum", mBimehNum);
        parm[16] = new SqlParameter("@pCurDate", "0000/00/00 00:00");
        //parm[16] = new SqlParameter("@MaxAdmCode", SqlDbType.Int) {Direction = ParameterDirection.ReturnValue};


        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.StoredProcedure,
            "AddReAdmPatient", parm);

        admCode = int.Parse(Lib.GetScaler("Max([Adm Code])", "Adm"));
        return admCode;
    }

    //====================================================================================================================
    public static int AddNewPatient
    (string lastName, string firstName, string father, byte sex, byte pType, int persCode, byte oprType,
        int admWardCode,
        bool isBefore, byte beforeNum, byte beforeBastNum, int bimehCode, string bimehNum, byte fromHospCode,
        int mBimehCode, bool isBimehEndDate, string bimehEndDate, string birthDate, string address, string shNum,
        int partCode, byte age, byte ageMon, byte ageDay, bool isEmerg, bool isVIP, int drCode,
        string mBimehNum)
    {
        var parm = new SqlParameter[30];
        parm[0] = new SqlParameter("@pFamily", lastName);
        parm[1] = new SqlParameter("@pName", firstName);
        parm[2] = new SqlParameter("@pFather", father);
        parm[3] = new SqlParameter("@pSex", sex);
        parm[4] = new SqlParameter("@pPType", pType);
        parm[5] = new SqlParameter("@pPersCode", persCode);
        parm[6] = new SqlParameter("@pOprType", oprType);
        parm[7] = new SqlParameter("@pAdmWardCode", admWardCode);
        parm[8] = new SqlParameter("@pIsBefore", isBefore);
        parm[9] = new SqlParameter("@pBeforeNum", beforeNum);
        parm[10] = new SqlParameter("@pBeforeBastNum", beforeBastNum);
        parm[11] = new SqlParameter("@pBimehCode", bimehCode);
        parm[12] = new SqlParameter("@pBimehNum", bimehNum);
        parm[13] = new SqlParameter("@pFromHospCode", fromHospCode);
        parm[14] = new SqlParameter("@pMBimehCode", mBimehCode);
        parm[15] = new SqlParameter("@pIsBimehEndDate", isBimehEndDate);
        parm[16] = new SqlParameter("@pShNum", shNum);
        parm[17] = new SqlParameter("@pPartCode", partCode);
        parm[18] = new SqlParameter("@pAge", age);
        parm[19] = new SqlParameter("@pAgeMon", ageMon);
        parm[20] = new SqlParameter("@pAgeDay", ageDay);
        parm[21] = new SqlParameter("@isEmerg", isEmerg);
        parm[22] = new SqlParameter("@isVIP", isVIP);
        parm[23] = new SqlParameter("@DrCode", drCode);
        parm[24] = new SqlParameter("@pBirthDate", birthDate);

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.StoredProcedure,
            "AddNewPatient", parm);

        var admCode = 0;
        admCode = int.Parse(Lib.GetScaler("Max([Adm Code])", "Adm"));
        return admCode;
    }

    //====================================================================================================================


    public static int GetMaxCode
        (string tableName)
    {
        var strSelect = "SELECT IsNull(Max(code)+ 1, 0) FROM " + tableName;
        var maxCode = int.Parse(SqlHelper.ExecuteScalar(cSet.cnString,
            CommandType.Text,
            strSelect).ToString());
        return maxCode;
    }
    //====================================================================================================================

    public static void DelRecord
        (int id)
    {
        var parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@Id", id);

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            "Delete From FilmList Where Id=@Id",
            parm);
    }

    //====================================================================================================================

    public static void UpdRecord
        (int id, string code, string fName, string filmLen, string fDesc, string fPath)
    {
        var parm = new SqlParameter[8];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@code", code);
        parm[2] = new SqlParameter("@fName", fName);
        parm[3] = new SqlParameter("@filmLen", filmLen);
        parm[4] = new SqlParameter("@fDesc", fDesc);
        parm[5] = new SqlParameter("@fPath", fPath);

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.StoredProcedure,
            "sp_UpdFilm", parm);
    }
}