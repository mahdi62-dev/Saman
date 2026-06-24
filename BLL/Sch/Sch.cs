using System;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Sch;

public class Sch
{
    public DataTable SchData(object CRUD, object DepID, object DrID, object StartDate, object EndDate,
        object StartTime
        , object EndTime, object WeekDay, object Capacity, object ExtraNum, object SchTime, object UserPersID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@DepID", DepID);
        com.Parameters.AddWithValue("@DrID", DrID);
        com.Parameters.AddWithValue("@StartDate", StartDate);
        com.Parameters.AddWithValue("@EndDate", EndDate);
        com.Parameters.AddWithValue("@StartTime", StartTime);
        com.Parameters.AddWithValue("@EndTime", EndTime);
        com.Parameters.AddWithValue("@WeekDay", WeekDay);
        com.Parameters.AddWithValue("@Capacity", Capacity);
        com.Parameters.AddWithValue("@ExtraNum ", ExtraNum);
        com.Parameters.AddWithValue("@SchTime ", SchTime);

        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spSchDef");
    }

    public DataTable SchDef
    (object CRUD, object DepID, object DrID, object StartDate, object EndDate, object StartTime
        , object EndTime, object WeekDay, object IsHoliday, object PresenceNum, object InternetNum, object TelNum
        , object Capacity, object ExtraNum, object SchTime, object SchDefID, object UserPersID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@DepID", DepID);
        com.Parameters.AddWithValue("@DrID", DrID);
        com.Parameters.AddWithValue("@StartDate", StartDate);
        com.Parameters.AddWithValue("@EndDate", EndDate);
        com.Parameters.AddWithValue("@StartTime", StartTime);
        com.Parameters.AddWithValue("@EndTime", EndTime);
        com.Parameters.AddWithValue("@WeekDay", WeekDay);
        com.Parameters.AddWithValue("@IsHoliday", IsHoliday);
        com.Parameters.AddWithValue("@Capacity", Capacity);
        com.Parameters.AddWithValue("@PresenceNum", PresenceNum);
        com.Parameters.AddWithValue("@InternetNum", InternetNum);
        com.Parameters.AddWithValue("@TelNum", TelNum);
        com.Parameters.AddWithValue("@ExtraNum ", ExtraNum);
        com.Parameters.AddWithValue("@SchTime ", SchTime);
        com.Parameters.AddWithValue("@SchDefID", SchDefID);

        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spSchDef");
    }


    public DataTable SchLoad(object CRUD, object SchDefID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@SchDefID", SchDefID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spSch");
    }


    public DataTable SchSave
    (int CRUD, int SchDefID, int SchID, int RecNo, string NationalCode, string FirstName, string LastName,
        string FatherName,
        int Country, int Gender, int Age, string Mobile, string Tel, int City, string Description,
        int ChiefComplaint, int EncService,
        int RefDr, int Insurance, int Height, int Weight, bool Emergency, bool SpecialDisease, bool Anes, string F1,
        string LastChemotherapyDate,
        string LastRadiotherapyDate, string LastSurgeryDate, bool Priority, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@SchDefID", SchDefID);
        com.Parameters.AddWithValue("@SchID", SchID);
        com.Parameters.AddWithValue("@RecNo", RecNo);
        com.Parameters.AddWithValue("@NationalCode", NationalCode);
        com.Parameters.AddWithValue("@FirstName", FirstName);
        com.Parameters.AddWithValue("@LastName", LastName);
        com.Parameters.AddWithValue("@FatherName", FatherName);
        com.Parameters.AddWithValue("@Country", Country);
        com.Parameters.AddWithValue("@Gender", Gender);
        com.Parameters.AddWithValue("@Age", Age);
        com.Parameters.AddWithValue("@Mobile", Mobile);
        com.Parameters.AddWithValue("@Tel", Tel);


        com.Parameters.AddWithValue("@City", City);
        com.Parameters.AddWithValue("@Description", Description);
        com.Parameters.AddWithValue("@ChiefComplaint", ChiefComplaint);
        com.Parameters.AddWithValue("@EncService", EncService);
        com.Parameters.AddWithValue("@RefDrID", RefDr);
        com.Parameters.AddWithValue("@Insurance", Insurance);
        com.Parameters.AddWithValue("@Height", Height);
        com.Parameters.AddWithValue("@Weight", Weight);
        com.Parameters.AddWithValue("@Emergency", Emergency);
        com.Parameters.AddWithValue("@SpecialDisease", SpecialDisease);
        com.Parameters.AddWithValue("@Priority", Priority);
        com.Parameters.AddWithValue("@Anes", Anes);
        com.Parameters.AddWithValue("@F1", F1);

        com.Parameters.AddWithValue("@LastChemotherapyDate",
            LastChemotherapyDate == string.Empty ? DBNull.Value : LastChemotherapyDate);

        com.Parameters.AddWithValue("@LastRadiotherapyDate",
            LastRadiotherapyDate == string.Empty ? DBNull.Value : LastRadiotherapyDate);

        com.Parameters.AddWithValue("@LastSurgeryDate",
            LastSurgeryDate == string.Empty ? DBNull.Value : LastSurgeryDate);

        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spSch");
    }

    public DataTable SchRecLoad(object CRUD, object SchID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@SchID", SchID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spSch");
    }

    public void SchRecDel(object CRUD, object SchID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@SchID", SchID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spSch");
    }

    public void SchRecPaste(object CRUD, object SchDefID, object SchID, object RecNo, bool IsCut, bool IsCopy)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);

        com.Parameters.AddWithValue("@IsPaste", 1);
        com.Parameters.AddWithValue("@IsCut", IsCut);
        com.Parameters.AddWithValue("@IsCopy", IsCopy);

        com.Parameters.AddWithValue("@SchDefID", SchDefID);
        com.Parameters.AddWithValue("@SchID", SchID);
        com.Parameters.AddWithValue("@RecNo", RecNo);


        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spSch");
    }


    public void SchConfirm(int CRUD, int SchDefID, int SchID, bool IsConfirm)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@SchDefID", SchDefID);
        com.Parameters.AddWithValue("@SchID", SchID);

        if (IsConfirm)
            com.Parameters.AddWithValue("@IsConfirm", IsConfirm);
        else
            com.Parameters.AddWithValue("@IsUnConfirm", 1);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spSch");
    }

    public void SchEncNew(int CRUD, int EncID, int SchID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@SchID", SchID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spSch");
    }


    public void SchGridSave(int SchID, int RecNo, string SchTime)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", -1);
        com.Parameters.AddWithValue("@IsSchGridSave", 1);

        com.Parameters.AddWithValue("@SchID", SchID);
        com.Parameters.AddWithValue("@RecNo", RecNo);
        com.Parameters.AddWithValue("@SchTime", SchTime);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spSch");
    }


    public void SchCancel(int SchID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", -1);
        com.Parameters.AddWithValue("@IsCancel", 1);
        com.Parameters.AddWithValue("@SchID", SchID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spSch");
    }
}