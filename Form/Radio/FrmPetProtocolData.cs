using System;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.APP.SAC;
using Saman.APP.Stimul;
using Saman.BLL.Pet;
using Saman.BLL.Report;
using Stimulsoft.Report.Dictionary;

namespace Saman.Form.Radio;

public partial class frmPetProtocolData : XtraForm
{
    private readonly Stimul _appStimul = new();

    private readonly PetProtocol _bllPetProtocol = new();
    private readonly Report _bllReport = new();
    private readonly int _FormPetID;
    private readonly SACPermission sacPermission = new();
    public string _Age, _PageDate, _NationalCode, _FirstName, _LastName;


    public int _EncID, _PageID, _ChiefComplaint, _AnsStatus, _Height, _Weight, _PPID;

    private bool _HasProtocolData, _HasCyclotronData, _HasScanData;

    private bool _PermissionProtocolData, _PermissionCyclotronData, _PermissionScanData;

    public frmPetProtocolData()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }


    public string _Date1 { get; set; }
    public string _Date2 { get; set; }

    public bool _CheckPermission { get; set; }

    private void chkCancellation_EditValueChanged(object sender, EventArgs e)
    {
        if (chkCancellation.Checked)
            txtReasonForCancellation.Enabled = true;
        else
            txtReasonForCancellation.Enabled = false;
    }

    private void FrmPetProtocolData_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        FillLookup();
        SetMask();


        CheckPermission();

        ProtocolDataLoad();
    }

    private void CheckPermission()
    {
        _PermissionProtocolData = sacPermission.CheckPermission(_ModID, 3000111);
        _PermissionCyclotronData = sacPermission.CheckPermission(_ModID, 3000112);
        _PermissionScanData = sacPermission.CheckPermission(_ModID, 3000113);

        btnSave.Enabled = sacPermission.CheckPermission(_ModID, 3000114);
    }

    private void ProtocolDataLoad()
    {
        Text += "    " + _FirstName + " " + _LastName + " - " + _Age + " " + "سال";

        txtHeight.Text = _Height.ToString();
        txtWeight.Text = _Weight.ToString();

        var dtPetProtocolData = _bllPetProtocol.PetProtocolDataLoad(1, _PageID);

        if (dtPetProtocolData.Rows.Count == 0) return;

        _PPID = Convert.ToInt32(dtPetProtocolData.Rows[0]["PPID"]);
        lueInjector.EditValue = Convert.ToInt32(dtPetProtocolData.Rows[0]["InjectorID"]);
        txtWeight.EditValue = dtPetProtocolData.Rows[0]["PPWeight"].ToString();
        txtHeight.EditValue = dtPetProtocolData.Rows[0]["PPHeight"].ToString();
        txtDrugDose.EditValue = dtPetProtocolData.Rows[0]["DrugDose"].ToString().Replace("/", ".");
        lueDrugName.EditValue = Convert.ToInt32(dtPetProtocolData.Rows[0]["DrugNameID"]);
        txtLastDrugTakeTime.Text = dtPetProtocolData.Rows[0]["LastDrugTakeTime"].ToString();
        txtNPOTime.Text = dtPetProtocolData.Rows[0]["NpoTime"].ToString();
        lueInjectionLocation.EditValue = Convert.ToInt32(dtPetProtocolData.Rows[0]["InjectionLocation"]);
        txtBSValue.EditValue = dtPetProtocolData.Rows[0]["BsValue"].ToString().Replace("/", ".");
        lueBSStatus.EditValue = Convert.ToInt32(dtPetProtocolData.Rows[0]["BsStatus"]);
        lueInjectionProcedure.EditValue = Convert.ToInt32(dtPetProtocolData.Rows[0]["InjectionProcedure"]);
        timInjectionTime.EditValue = dtPetProtocolData.Rows[0]["InjectionTime"].ToString();
        txtPersAccumulativeDose.Text = dtPetProtocolData.Rows[0]["PersAccuDose"].ToString();
        timUptakeTime.EditValue = dtPetProtocolData.Rows[0]["UptakeTime"].ToString();
        //txtUptakeTime.Text = dtPetProtocolData.Rows[0]["UptakeTime"].ToString();
        chkCancellation.EditValue = (bool)dtPetProtocolData.Rows[0]["IsCancel"];
        txtReasonForCancellation.Text = dtPetProtocolData.Rows[0]["ReasonForCancellation"].ToString();
        txtDeviationOfProcedure.Text = dtPetProtocolData.Rows[0]["DeviationOfProcedure"].ToString();
        lueDataRecorder.EditValue = Convert.ToInt32(dtPetProtocolData.Rows[0]["DataRecorderID"]);
        txtPreinjectionAssay.EditValue = dtPetProtocolData.Rows[0]["PreinjectionAssay"].ToString().Replace("/", ".");
        txtTracerVolume.EditValue = dtPetProtocolData.Rows[0]["TracerVolume"].ToString().Replace("/", ".");
        timPreinjectionAssayTime.EditValue = dtPetProtocolData.Rows[0]["PreinjectionAssayTime"].ToString();
        txtPostInjectionAssay.EditValue = dtPetProtocolData.Rows[0]["PostInjectionAssay"].ToString().Replace("/", ".");
        timPostInjectionAssayTime.EditValue = dtPetProtocolData.Rows[0]["PostInjectionAssayTime"].ToString();
        lueScanPerformer.EditValue = Convert.ToInt32(dtPetProtocolData.Rows[0]["ScanPerformerID"]);
        txtScanDeviationOfProcedure.Text = dtPetProtocolData.Rows[0]["ScanDeviationOfProcedure"].ToString();

        txtPatientDisease.Text = dtPetProtocolData.Rows[0]["PatientDisease"].ToString();
        txtTubeVoltage.Text = dtPetProtocolData.Rows[0]["TubeVoltage"].ToString();
        luemATube.EditValue = Convert.ToInt32(dtPetProtocolData.Rows[0]["mATube"]);
        txtmATubeDescription.Text = dtPetProtocolData.Rows[0]["mATubeDesc"].ToString();
        txtNoiseIndex.Text = dtPetProtocolData.Rows[0]["NoiseIndex"].ToString();
        txtCTDI.Text = dtPetProtocolData.Rows[0]["CTDI"].ToString().Replace("/", ".");
        txtDLP.Text = dtPetProtocolData.Rows[0]["DLP"].ToString().Replace("/", ".");
        txtNoOfBedPosition.Text = dtPetProtocolData.Rows[0]["NoOfBedPosition"].ToString();
        txtBedOverlap.Text = dtPetProtocolData.Rows[0]["BedOverlap"].ToString();
        lueReconstruction.EditValue = Convert.ToInt32(dtPetProtocolData.Rows[0]["Reconstruction"]);
        timTimePerBed.EditValue = dtPetProtocolData.Rows[0]["TimePerBed"].ToString();
        txtScanPersAccumulativeDose.Text = dtPetProtocolData.Rows[0]["ScanPersAccuDose"].ToString();


        _HasProtocolData = Convert.ToBoolean(dtPetProtocolData.Rows[0]["HasProtocolData"]);
        _HasCyclotronData = Convert.ToBoolean(dtPetProtocolData.Rows[0]["HasCyclotronData"]);
        _HasScanData = Convert.ToBoolean(dtPetProtocolData.Rows[0]["HasScanData"]);


        if (Convert.ToInt32(lueInjector.EditValue) == 0) lueInjector.EditValue = GL.UserPersID;

        if (Convert.ToInt32(lueDataRecorder.EditValue) == 0) lueDataRecorder.EditValue = GL.UserPersID;

        if (Convert.ToInt32(lueScanPerformer.EditValue) == 0) lueScanPerformer.EditValue = GL.UserPersID;

        CheckDataEntry();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
    }

    private void PetProtocolSave(bool HasProtocolData, bool HasCyclrotlon, bool HasScanData)
    {
        var CRUD = 0;
        if (_PPID > 0) CRUD = 2;

        _bllPetProtocol.PetProtocolData(CRUD, HasProtocolData, HasCyclrotlon, HasScanData, _PPID, _PageID,
            Convert.ToInt32(lueInjector.EditValue), txtWeight.EditValue,
            txtHeight.EditValue, txtDrugDose.EditValue, Convert.ToInt32(lueDrugName.EditValue),
            txtLastDrugTakeTime.Text, txtNPOTime.Text,
            Convert.ToInt32(lueInjectionLocation.EditValue), txtBSValue.EditValue,
            Convert.ToInt32(lueBSStatus.EditValue), Convert.ToInt32(lueInjectionProcedure.EditValue),
            timInjectionTime.Text, txtPersAccumulativeDose.Text, timUptakeTime.Text,
            chkCancellation.Checked, txtReasonForCancellation.Text, txtDeviationOfProcedure.Text,
            Convert.ToInt32(lueDataRecorder.EditValue), txtPreinjectionAssay.EditValue,
            txtTracerVolume.EditValue, timPreinjectionAssayTime.Text,
            txtPostInjectionAssay.EditValue, timPostInjectionAssayTime.Text,
            Convert.ToInt32(lueScanPerformer.EditValue), txtScanDeviationOfProcedure.Text,
            txtPatientDisease.Text, txtTubeVoltage.Text,
            Convert.ToInt32(luemATube.EditValue), txtmATubeDescription.Text,
            txtNoiseIndex.Text, txtCTDI.Text,
            txtDLP.Text, txtNoOfBedPosition.Text,
            txtBedOverlap.Text, Convert.ToInt32(lueReconstruction.EditValue),
            timTimePerBed.Text, txtScanPersAccumulativeDose.Text,
            GL.UserPersID);

        CheckDataEntry();

        DialogResult = DialogResult.Yes;
        Close();
    }

    private void CheckDataEntry()
    {
        if (chkCancellation.Checked) _HasProtocolData = _HasCyclotronData = false;

        lblCheck1.Visible = _HasProtocolData;
        lblCheck2.Visible = _HasCyclotronData;
        lblCheck3.Visible = _HasScanData;

        floProtocolData.Enabled = false;
        floCyclotronData.Enabled = false;
        floScan.Enabled = false;

        if (_PermissionProtocolData)
        {
            floProtocolData.Enabled = true;
            floCyclotronData.Enabled = false;
            floScan.Enabled = false;
        }

        if (_HasProtocolData)
        {
            floProtocolData.Enabled = true;
            floCyclotronData.Enabled = false;
            floScan.Enabled = false;
        }

        if (_PermissionCyclotronData)
        {
            floProtocolData.Enabled = false;
            floCyclotronData.Enabled = true;
            floScan.Enabled = false;
        }

        if (_HasCyclotronData)
        {
            floProtocolData.Enabled = true;
            floCyclotronData.Enabled = false;
            floScan.Enabled = false;
        }

        if (_PermissionScanData)
        {
            floProtocolData.Enabled = false;
            floCyclotronData.Enabled = false;
            floScan.Enabled = true;
        }

        if (_HasScanData)
        {
            floProtocolData.Enabled = false;
            floCyclotronData.Enabled = false;

            floScan.Enabled = false;

            lblTitleCyclotronData.Enabled = false;
            lblTitleImagingData.Enabled = false;
            lblTitleProtocolData.Enabled = false;
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
        Close();
    }

    private void SetMask()
    {
        txtWeight.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
        txtHeight.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
        txtDrugDose.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
        txtNPOTime.Properties.Mask.EditMask = App.UI.MaskNum;
        txtLastDrugTakeTime.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtUptakeTime.Properties.Mask.EditMask = App.UI.MaskNum;
        txtBSValue.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
        txtPersAccumulativeDose.Properties.Mask.EditMask = App.UI.MaskEnglishWithNumChar;
        txtPreinjectionAssay.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
        txtTracerVolume.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
        txtPostInjectionAssay.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;

        txtPatientDisease.Properties.Mask.EditMask = App.UI.MaskEnglishWithNumChar;
        txtTubeVoltage.Properties.Mask.EditMask = App.UI.MaskNum;
        txtNoiseIndex.Properties.Mask.EditMask = App.UI.MaskNum;
        txtCTDI.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
        txtDLP.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
        txtNoOfBedPosition.Properties.Mask.EditMask = App.UI.MaskNum;
        txtBedOverlap.Properties.Mask.EditMask = App.UI.MaskNum;
        txtScanPersAccumulativeDose.Properties.Mask.EditMask = App.UI.MaskEnglishWithNumChar;
        txtmATubeDescription.Properties.Mask.EditMask = App.UI.MaskEnglishWithNumChar;

        timUptakeTime.Properties.EditMask = "HH:mm";
        timInjectionTime.Properties.EditMask = "HH:mm";
        timPostInjectionAssayTime.Properties.EditMask = "HH:mm";
        timPreinjectionAssayTime.Properties.EditMask = "HH:mm";
        timTimePerBed.Properties.EditMask = "HH:mm";
    }

    private void FillLookup()
    {
        UL.FillLookup(lueInjector, null, cSet.cnStringERP, false, "PersID, LastName + '_' + FirstName AS Name",
            "Personnel", "vPersonnel", "PersType=18 ", null);

        UL.FillLookup(lueDataRecorder, null, cSet.cnStringERP, false, "PersID, LastName + '_' + FirstName AS Name",
            "Personnel", "vPersonnel", "PersType=19 ", null);

        UL.FillLookup(lueScanPerformer, null, cSet.cnStringERP, false, "PersID, LastName + '_' + FirstName AS Name",
            "Personnel", "vPersonnel", "PersType=20 ", null);

        UL.FillLookup(lueDrugName, "0", cSet.cnStringA, false, "ID,Name", "vADR", "DrugName", "ADName='DrugName'",
            null);
        UL.FillLookup(lueBSStatus, "0", cSet.cnStringA, false, "ID,Name", "vADR", "BSStatus", "ADName='BSStatus'",
            null);
        UL.FillLookup(lueInjectionLocation, "0", cSet.cnStringA, false, "ID,Name", "vADR", "InjectionLocation",
            "ADName='InjectionLocation'", null);
        UL.FillLookup(lueInjectionProcedure, "0", cSet.cnStringA, false, "ID,Name", "vADR", "InjectionProcedure",
            "ADName='InjectionProcedure'", null);
        UL.FillLookup(luemATube, "0", cSet.cnStringA, false, "ID,Name", "vADR", "mATube", "ADName='mATube'", null);
        UL.FillLookup(lueReconstruction, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Reconstruction",
            "ADName='Reconstruction'", null);
    }

    private void lblTitleProtocolData_Click(object sender, EventArgs e)
    {
        PetProtocolSave(true, false, false);
    }

    private void lblTitleCyclotronData_Click(object sender, EventArgs e)
    {
        PetProtocolSave(false, true, false);
    }

    private void lblTitleScanData_Click(object sender, EventArgs e)
    {
        PetProtocolSave(false, false, true);
    }

    private void lblPrint_Click(object sender, EventArgs e)
    {
        var tsqlvPetProtocolFull =
            @"select * from SamanHIS.dbo.vPetProtocolFull
                    where EncID=@EncID";

        tsqlvPetProtocolFull = tsqlvPetProtocolFull.Replace("@EncID", _EncID.ToString());


        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100021);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;


        ((StiSqlSource)report.Dictionary.DataSources["vPetProtocolFull"]).SqlCommand = tsqlvPetProtocolFull;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;


        report.Show();
    }
}