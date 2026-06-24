using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Loan;

public class LoanAssign
{
    public DataTable PersLoanLoad(int PersID, int LoanAssignID)
    {
        var tsql = @"
            SELECT ROW_NUMBER() OVER (PARTITION BY PersID ORDER BY LoanAssignID) AS RecNo
				,*
             FROM  vLoanAssign
            ";

        var com = new SqlCommand();
        if (PersID > 0)
        {
            tsql += "WHERE PersID=@PersID";
            com.Parameters.Add("@PersID", PersID);
        }

        if (LoanAssignID > 0)
        {
            tsql += " AND LoanAssignID=@LoanAssignID";
            com.Parameters.Add("@LoanAssignID", LoanAssignID);
        }

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, tsql);
    }

    public DataTable PersLoanInstallLoad(int PersID, int LoanAssignID)
    {
        var tsql = @"
SELECT ROW_NUMBER() OVER (PARTITION BY l.PersID ORDER BY l.LoanAssignID) AS RecNo
,l.LoanAssignID
,l.PersID
,l.AssignDate
,l.LoanID
,l.LoanName
,l.LoanBaseAmount
,l.LoanFeePer
,l.InstallCount
,l.LoanAmount
--,l.InstallAmount
,li.InstallAmount
,l.InstallLastAmount
,l.DelayFineAmount
,l.InstallStartDate
,l.InstallPayCount
,l.InstallPayAmount
,l.InstallRemainCount
,l.InstallRemainAmount
,l.Description
,p.FullName
,li.InstallRecNO
,li.InstallDate
--,li.InstallAmount
,li.PayDate
,li.PayStatus
,li.DelayDay
,li.DelayFineAmount
,li.InstallResultAmount
				
FROM  vLoanAssign l
left outer join LoanInstall li on li.LoanAssignID=l.LoanAssignID
inner join vPersonnelFull p on p.PersID=l.PersID
where l.LoanAssignID = @LoanAssignID And l.PersID=@PersID
            ";

        var com = new SqlCommand();
        com.Parameters.Add("@LoanAssignID", LoanAssignID);
        com.Parameters.Add("@PersID", PersID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, tsql);
    }

    public DataTable LoanSave
    (object CRUD, object PersID, object AssignDate, object LoanID, object LoanName, object LoanBaseAmount,
        object LoanFeePer, object InstallCount,
        object LoanAmount, object InstallAmount, object InstallLastAmount, object DelayFineAmount,
        object InstallStartDate, object Description, object UserPersID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@PersID", PersID);
        com.Parameters.AddWithValue("@AssignDate", AssignDate);
        com.Parameters.AddWithValue("@LoanID", LoanID);
        com.Parameters.AddWithValue("@LoanName ", LoanName);
        com.Parameters.AddWithValue("@LoanBaseAmount", LoanBaseAmount);
        com.Parameters.AddWithValue("@LoanFeePer", LoanFeePer);
        com.Parameters.AddWithValue("@InstallCount", InstallCount);
        com.Parameters.AddWithValue("@LoanAmount", LoanAmount);
        com.Parameters.AddWithValue("@InstallAmount", InstallAmount);
        com.Parameters.AddWithValue("@InstallLastAmount", InstallLastAmount);
        com.Parameters.AddWithValue("@DelayFineAmount", DelayFineAmount);
        com.Parameters.AddWithValue("@InstallStartDate", InstallStartDate);
        com.Parameters.AddWithValue("@Description", Description);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spLoanAssignSave");
    }
}