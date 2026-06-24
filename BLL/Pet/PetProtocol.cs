using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Pet;

public class PetProtocol
{
    public DataTable PetPageLoad(int DepID, string Date1, string Date2)
    {
        var tsql = @"
                    SELECT pf.*
,(SELECT s.Description  FROM dbo.Sch s WHERE s.EncID = pf.EncID) AS SchDescription
  FROM [SamanHIS].[dbo].[vPetProtocolFull] pf
  WHERE 1=1
  AND pf.DepID =  @DepID
  AND pf.PageDate BETWEEN @Date1 and @Date2
                        ORDER BY pf.PageDate Desc

                        ";
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@DepID", DepID);
        com.Parameters.AddWithValue("@Date1", Date1);
        com.Parameters.AddWithValue("@Date2", Date2);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, tsql);
    }

    public DataTable PetProtocolData(int CRUD, bool HasProtocolData, bool HasCyclotronData, bool HasScanData,
        int PPID, int PageID, int InjectorID, object Weight, object Height, object DrugDose, object DrugNameID,
        string LastDrugTakeTime, string NpoTime, int InjectionLocation, object BsValue, int BsStatus,
        int InjectionProcedure,
        string InjectionTime, string PersAccuDose, string UptakeTime, bool IsCancel, string ReasonForCancellation,
        string DeviationOfProcedure,
        int DataRecorderID, object PreinjectionAssay, object TracerVolume, string PreinjectionAssayTime,
        object PostInjectionAssay, string PostInjectionAssayTime,
        int ScanPerformerID, string ScanDeviationOfProcedure,
        string PatientDisease, string TubeVoltage, int mATube, string mATubeDesc, string NoiseIndex, string CTDI,
        string DLP, string NoOfBedPosition,
        string BedOverlap, int Reconstruction, string TimePerBed, string ScanPersAccuDose,
        int UserPersID)

    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@HasProtocolData", HasProtocolData);
        com.Parameters.AddWithValue("@HasCyclotronData", HasCyclotronData);
        com.Parameters.AddWithValue("@HasScanData", HasScanData);

        com.Parameters.AddWithValue("@PPID", PPID);
        com.Parameters.AddWithValue("@PageID", PageID);
        com.Parameters.AddWithValue("@InjectorID", InjectorID);
        com.Parameters.AddWithValue("@Weight", Weight);
        com.Parameters.AddWithValue("@Height", Height);
        com.Parameters.AddWithValue("@DrugDose", DrugDose);
        com.Parameters.AddWithValue("@DrugNameID", DrugNameID);
        com.Parameters.AddWithValue("@LastDrugTakeTime", LastDrugTakeTime);
        com.Parameters.AddWithValue("@NpoTime", NpoTime);
        com.Parameters.AddWithValue("@InjectionLocation", InjectionLocation);
        com.Parameters.AddWithValue("@BsValue", BsValue);
        com.Parameters.AddWithValue("@BsStatus", BsStatus);
        com.Parameters.AddWithValue("@InjectionProcedure", InjectionProcedure);
        com.Parameters.AddWithValue("@InjectionTime", InjectionTime);
        com.Parameters.AddWithValue("@PersAccuDose", PersAccuDose);
        com.Parameters.AddWithValue("@UptakeTime", UptakeTime);
        com.Parameters.AddWithValue("@IsCancel", IsCancel);
        com.Parameters.AddWithValue("@ReasonForCancellation", ReasonForCancellation);
        com.Parameters.AddWithValue("@DeviationOfProcedure", DeviationOfProcedure);
        com.Parameters.AddWithValue("@DataRecorderID", DataRecorderID);
        com.Parameters.AddWithValue("@PreinjectionAssay", PreinjectionAssay);
        com.Parameters.AddWithValue("@TracerVolume", TracerVolume);
        com.Parameters.AddWithValue("@PreinjectionAssayTime", PreinjectionAssayTime);
        com.Parameters.AddWithValue("@PostInjectionAssay", PostInjectionAssay);
        com.Parameters.AddWithValue("@PostInjectionAssayTime", PostInjectionAssayTime);
        com.Parameters.AddWithValue("@ScanPerformerID", ScanPerformerID);
        com.Parameters.AddWithValue("@ScanDeviationOfProcedure", ScanDeviationOfProcedure);
        com.Parameters.AddWithValue("@PatientDisease", PatientDisease);
        com.Parameters.AddWithValue("@TubeVoltage", TubeVoltage);
        com.Parameters.AddWithValue("@mATube", mATube);
        com.Parameters.AddWithValue("@mATubeDesc", mATubeDesc);
        com.Parameters.AddWithValue("@NoiseIndex", NoiseIndex);
        com.Parameters.AddWithValue("@CTDI", CTDI);
        com.Parameters.AddWithValue("@DLP", DLP);
        com.Parameters.AddWithValue("@NoOfBedPosition", NoOfBedPosition);
        com.Parameters.AddWithValue("@BedOverlap", BedOverlap);
        com.Parameters.AddWithValue("@Reconstruction", Reconstruction);
        com.Parameters.AddWithValue("@TimePerBed", TimePerBed);
        com.Parameters.AddWithValue("@ScanPersAccuDose", ScanPersAccuDose);


        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spPetProtocol");
    }

    public DataTable PetProtocolDataLoad(int CRUD, int PageID)

    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@PageID", PageID);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spPetProtocol");
    }
}