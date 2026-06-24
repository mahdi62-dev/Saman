using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.PComment;

public class Comment
{
    public DataTable EncSearch(string NationalCode)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@CRUD", 1);
        com.Parameters.Add("@FuncType", 1);
        com.Parameters.Add("@NationalCode", NationalCode);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spCommentPatient");
    }

    public DataTable PageSearch(string NationalCode)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@CRUD", 1);
        com.Parameters.Add("@FuncType", 2);
        com.Parameters.Add("@NationalCode", NationalCode);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spCommentPatient");
    }

    public DataTable PersSearch(string NationalCode, int DepID)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@CRUD", 1);
        com.Parameters.Add("@FuncType", 3);
        com.Parameters.Add("@NationalCode", NationalCode);
        com.Parameters.Add("@DepID", DepID);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spCommentPatient");
    }


    public DataTable PCommentSave(int CRUD, int CommentPatientID, int FuncType, string NationalCode, int DepID,
        int PersID, int Point, int Reason)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@CommentPatientID", CommentPatientID);
        com.Parameters.AddWithValue("@FuncType", FuncType);
        com.Parameters.AddWithValue("@NationalCode", NationalCode);
        com.Parameters.AddWithValue("@DepID", DepID);
        com.Parameters.AddWithValue("@PersID", PersID);
        com.Parameters.AddWithValue("@Point", Point);
        //com.Parameters.AddWithValue("@VoiceFile", VoiceData);
        com.Parameters.AddWithValue("@Reason", Reason);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spCommentPatient");
    }


    public DataTable PCommentSaveVoice(int CRUD, int FuncType, string NationalCode, int DepID, byte[] VoiceData)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@FuncType", FuncType);
        com.Parameters.AddWithValue("@NationalCode", NationalCode);
        com.Parameters.AddWithValue("@DepID", DepID);
        com.Parameters.AddWithValue("@VoiceFile", VoiceData);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spCommentPatient");
    }
}