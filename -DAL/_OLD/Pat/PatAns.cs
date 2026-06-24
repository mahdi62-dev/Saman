using System.Data;
using System.Data.SqlClient;

namespace DAL;

public class PatAns
{
    //====================================================================================================================

    public static void SavePatAns
    (int paperIntCode, string tissue, string clinical, string macroscopic, string microscopic,
        string diagnosis, string comment)
    {
        var persCode = 0;
        var parm = new SqlParameter[10];
        parm[0] = new SqlParameter("@PaperIntCode", paperIntCode);
        parm[1] = new SqlParameter("@PersCode", persCode);
        parm[2] = new SqlParameter("@Tissue", tissue);
        parm[3] = new SqlParameter("@Clinical", clinical);
        parm[4] = new SqlParameter("@Macroscopic", macroscopic);
        parm[5] = new SqlParameter("@Microscopic", microscopic);
        parm[6] = new SqlParameter("@Diagnosis", diagnosis);
        parm[7] = new SqlParameter("@Comment", comment);


        var strUpd = "Update Paper Set isAns=1 Where [Paper Int Code]=" + paperIntCode;
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strUpd, parm);

        Lib.Delete("PatAns", "PaperIntCode=" + paperIntCode);

        var strIns = "Insert Into PatAns " +
                     "(PaperIntCode, PersCode, Tissue, Clinical, Macroscopic, Microscopic, Diagnosis, Comment) " +
                     "Select @PaperIntCode, @PersCode, @Tissue, @Clinical, @Macroscopic, @Microscopic, @Diagnosis, @Comment";
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strIns, parm);
    }

    //====================================================================================================================
    public static SqlDataReader ReadPatAns(int paperIntCode)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@PaperIntCode", paperIntCode);

        var strSel = "Select * From PatAns Where PaperIntCode = @PaperIntCode";
        var readPatAns = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text, strSel, parm);
        return readPatAns;
    }

    //====================================================================================================================
}