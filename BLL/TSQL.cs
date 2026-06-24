namespace Saman.BLL;

public static class TSQL
{
    public static string EncSearchByEncID => @"SELECT
[EncID]
,e.[PatientID]
,[TypeAD]
,[StatusAD]
,[StartDate]
,[EndDate]
,[DepID]
,[DrID]
,[ChiefComplaint]
,[IsEmerg]
,[Description]
,[Age]
,p.*
FROM Enc e
INNER JOIN Patient p WITH(NOLOCK) on p.PatientID=e.PatientID
WHERE e.EncId= @EncID";
}