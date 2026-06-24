using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using LIB;
using Saman.BLL.Enc;
using Saman.BLL.Opr;
using Saman.Properties;
using Saman.UC.Dis;
using Saman.UC.Drug;
using Saman.UC.ServiceReg;
using Control = App.Control;

namespace Saman.UC.Opr;

public partial class UcPageOpr : XtraUserControl
{
    private readonly OprCalc _bllOprCalc = new();

    private readonly DataTable _dtDep = new();
    private readonly DataTable _dtModifier = new();

    private readonly bool _MuliFieldOpr = false;

    private readonly double _OprK, _AnesK, _OprRoomK;
    private readonly float _recoveryK;
    private readonly string _str;
    private readonly int RecNoAdd;
    private int _AnesTime, _RecoveryTime, _OprTime;
    private DataTable _dtEncPage = new();
    private DataTable _dtPage = new();
    private DataTable _dtPageOpr = new();
    private DataTable _dtPageTeam = new();
    private DataTable _dtServiceReg = new();

    private int _EncStatus;

    private int _OprServiceID;
    private int _PageListRecNo;
    public bool _ReadOnly;
    private DataRowView _rowCurServie;
    private DataRow[] _rowEncCur;

    private int _ServiceModifierCode;
    private double _TotalOprK, _TotalAnesK, _TotalOprRoomK;


    public UcPageOpr()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public int _StorageID { get; set; }
    public int _StoragePeriodID { get; set; }
    public int _EncID { get; set; }
    public int _PageID { get; set; }
    public byte _EncType { get; set; }

    private void FormNull()
    {
        _PageID = 0;
        lblPageID.Text = null;
        lblTotalOprK.Text = "0";
        lblTotalAnesK.Text = "0";
        lblTotalOprRoomK.Text = "0";
        txtAndoTime.Text = "0";

        lblTitle.Text = null;
        lblTitle2.Text = null;
        lblTitleEncDate.Text = null;
    }

    private void UcPageOpr_Load(object sender, EventArgs e)
    {
        btnServiceRegSave.Enabled = !_ReadOnly;
        UL.LangChange(2);

        txtAndoTime.Properties.Mask.EditMask = App.UI.MaskNum;


        if (_PageID == 0)
        {
            var appSDate = new SDate();

            var curDateTime = DateTime.Parse(appSDate.ShamsiDate());
            datOprEndDate.DateTime = datOprStartDate.DateTime = curDateTime;
            // timAnesStartTime.EditValue = timAnesEndTime.EditValue = curDateTime;
            //timRecoveryStartTime.EditValue = timRecoveryEndTime.EditValue = curDateTime;
        }

        _rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);
        if (_rowEncCur.Length > 0)
        {
            _EncID = Convert.ToInt32(_rowEncCur[0]["EncId"]);

            _EncStatus = Convert.ToInt32(_rowEncCur[0]["EncStatus"]);
            //_EncType = Convert.ToInt32(_rowEncCur[0]["EncType"]);
            var EncStatusName = _rowEncCur[0]["EncStatusName"].ToString();


            lblTitle.Text = _rowEncCur[0]["FirstName"] + " " + _rowEncCur[0]["LastName"] + "      " + EncStatusName;
            lblEncID.Text = _rowEncCur[0]["EncID"].ToString();
            lblTitle2.Text = _rowEncCur[0]["Age"] + @" سال - فرزند " + _rowEncCur[0]["FatherName"];
            lblTitleEncDate.Text = _rowEncCur[0]["StartDate"].ToString();
            picGender.Image = Convert.ToByte(_rowEncCur[0]["Gender"]) == 1 ? Resources.male : Resources.female;
            lblDrName.Text = _rowEncCur[0]["DrName"].ToString();
            lblDepName.Text = _rowEncCur[0]["DepName"].ToString();

            EncSearch(_EncID);
        }

        FillLookup();

        var bllOprDataTable = new DT();
        bllOprDataTable.CreateTableServiceReg(_dtServiceReg);
        bllOprDataTable.CreateTablePageTeam(_dtPageTeam);
        bllOprDataTable.CreateTableModifier(_dtModifier);


        EncPageLoad();
        //AddNewPage();

        CheckReadOnly();
        ShortKey();
    }

    private void ShortKey()
    {
        msiSave.Click += btnServiceRegSave_Click;
        msiNewPage.Click += lblPageNew_Click;
        msiServiceReg.Click += btnServiceReg_Click;
        msiServiceRegDrug.Click += btnServiceRegDrug_Click;
        msiClose.Click += btnCancel_Click;

        panMain.Focus();
    }

    private void FillLookup()
    {
        UL.FillLookup(lueOprTeamRole, "0", cSet.cnStringA, false, "ID,Name", "vADR", "TeamRole",
            "ADName='TeamRole' AND IsDisable=0", null);
        UL.FillLookup(lueOprTeamPers, "0", cSet.cnStringA, false, "PersID, Name", "vPersonnel", "Personnel",
            "PersType in (2,3,21) AND IsDisable=0", null);
        UL.FillLookup(lueCategory, "0", cSet.cnStringA, false, "ID,Name", "vADR", "ChiefComplaint",
            "ADName='ChiefComplaint'", null);
        UL.FillLookup(lueCategory1, "0", cSet.cnStringA, false, "ID,Name", "vADR", "OprCategory1",
            "ADName='OprCategory1'", null);
        UL.FillLookup(lueAnesType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "AnesType", "ADName='AnesType'",
            null);
        UL.FillLookup(lueService, null, cSet.cnStringA, false,
            "ServiceID, ServiceCode, Name, ProK, TechK, AnesK", "vServiceOpr", "ServiceOpr",
            null, null);
        UL.FillLookup(lueKPer, "100", cSet.cnStringA, false, "ID,Name,isDisable", "vADR", "KPer",
            "ADName='KPer' AND isDisable ='False'", "ID DESC");
        UL.FillLookup(lueSDr, "0", cSet.cnStringA, false, "*", "vDr", "SDr", "(DrRoleType=1 OR DrID=0) AND IsDisable=0",
            null);
        UL.SetLookupValueByText(lueSDr, lblDrName.Text);
        UL.FillLookup(lueADr, "0", cSet.cnStringA, false, "*", "vDr", "ADr", "(DrRoleType=2 OR DrID=0) AND IsDisable=0",
            null);
        UL.FillLookup(lueHDr, "0", cSet.cnStringA, false, "*", "vDr", "SDr", "DrRoleType=1 AND IsDisable=0", null);
        UL.FillLookup(lueAnesType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "AnesType", "ADName='AnesType'",
            null);
        UL.FillLookup(lueServiceModifier, "0", cSet.cnStringA, false, "*", "vServiceModifier", "ServiceModifier",
            null, null);
        UL.FillLookup(lueModifierDr, "0", cSet.cnStringA, false, "*", "vDr", "Dr", "IsDisable = 0", null);
    }

    private void CheckReadOnly()
    {
        _ReadOnly = false;
        if (_EncStatus > 2 || _ModID == 6) _ReadOnly = true;

        btnServiceRegSave.Enabled = !_ReadOnly;
        btnOprTeamAdd.Enabled = !_ReadOnly;
        btnOprTeamDel.Enabled = !_ReadOnly;
        btnModifierAdd.Enabled = !_ReadOnly;
        btnModifierDel.Enabled = !_ReadOnly;
        lblPageNew.Enabled = !_ReadOnly;
        btnServiceAdd.Enabled = !_ReadOnly;
        btnServiceDel.Enabled = !_ReadOnly;
        btnServiceDelAll.Enabled = !_ReadOnly;
        btnServiceEdit.Enabled = !_ReadOnly;
    }

    private void EncPageLoad()
    {
        var bllPage = new BLL.Page.Page();
        //_dtEncPage = bllPage.EncPageLoad(_EncID, _ModID, _DepID,_PageID);
        _dtEncPage = bllPage.EncPageLoad(_EncID, _ModID, _DepID, 0);

        _PageListRecNo = _dtEncPage.Rows.Count;


        if (_PageListRecNo > 0 && _PageID == 0)
        {
            _PageID = Convert.ToInt32(_dtEncPage.Rows[_PageListRecNo - 1]["PageID"].ToString());
            PageDataLoad();
            lblPageList.Text = "صفحه " + _PageListRecNo + " از " + _dtEncPage.Rows.Count;
        }

        else
        {
            for (var i = 0; i < _dtEncPage.Rows.Count; i++)
                if (Convert.ToInt32(_dtEncPage.Rows[i]["PageID"]) == _PageID)
                    _PageListRecNo = i + 1;

            lblPageList.Text = "صفحه " + _PageListRecNo + " از " + _dtEncPage.Rows.Count;
        }
    }

    private void PageDataLoad()
    {
        if (_dtEncPage.Rows.Count > 0)
        {
            var rowPage = _dtEncPage.Select("PageID=" + _PageID)[0];
            lblPageDate.Text = rowPage["Date"].ToString().Replace(' ', ' ');
        }


        lblPageID.Text = _PageID.ToString();

        //datOprStartDate.DateTime = DateTime.Parse(rowPage["Date2"].ToString());
        //timOprStartTime.Time = DateTime.Parse(rowPage["Date2"].ToString());
        //datOprEndDate.DateTime = DateTime.Parse(rowPage["Date3"].ToString());
        //timOprEndTime.Time = DateTime.Parse(rowPage["Date3"].ToString());

        PageOprLoad(_PageID);
        PageTeamLoad(_PageID);

        ServiceRegLoad(_PageID);
    }

    private void ServiceRegLoad(int pageID)
    {
        var bllServiceLerg = new ServiceRegOpr();
        _dtServiceReg = bllServiceLerg.ServiceRegLoad(pageID);


        if (!_dtServiceReg.Columns.Contains("HDrID"))
        {
            var newColumn = new DataColumn("HDrID", typeof(int)) { DefaultValue = 0 };
            _dtServiceReg.Columns.Add(newColumn);
        }

        if (!_dtServiceReg.Columns.Contains("SMCode"))
        {
            var newColumn = new DataColumn("SMCode", typeof(int)) { DefaultValue = 0 };
            _dtServiceReg.Columns.Add(newColumn);
        }

        grdServiceReg.DataSource = _dtServiceReg;
        CalcTotalK();

        if (_dtServiceReg.Rows.Count > 0)
        {
            var recNo = Convert.ToInt32(_dtServiceReg.Compute("MAX([RecNo])", string.Empty)) + 1;
            lblRecNo.Text = recNo.ToString();
        }
    }

    private void PageOprLoad(int pageID)
    {
        var bllPageOpr = new BLL.Opr.Page();
        _dtPageOpr = bllPageOpr.PageOprLoad(_PageID);

        _OprServiceID = Convert.ToInt32(_dtPageOpr.Rows[0]["OprServiceID"]);

        lueCategory.EditValue = Convert.ToInt32(_dtPageOpr.Rows[0]["OprCategory"]);
        lueCategory1.EditValue = Convert.ToInt32(_dtPageOpr.Rows[0]["OprCategory2"]);

        datOprStartDate.DateTime = DateTime.Parse(_dtPageOpr.Rows[0]["OprStartTime"].ToString());
        timOprStartTime.Time = DateTime.Parse(_dtPageOpr.Rows[0]["OprStartTime"].ToString());
        datOprEndDate.DateTime = DateTime.Parse(_dtPageOpr.Rows[0]["OprEndTime"].ToString());
        timOprEndTime.Time = DateTime.Parse(_dtPageOpr.Rows[0]["OprEndTime"].ToString());

        timAnesStartTime.EditValue = DateTime.Parse(_dtPageOpr.Rows[0]["AnesStartTime"].ToString());
        timAnesEndTime.EditValue = DateTime.Parse(_dtPageOpr.Rows[0]["AnesEndTime"].ToString());

        var anesTime = TimeSpan.FromMinutes(long.Parse(_dtPageOpr.Rows[0]["AnesTime"].ToString()));
        lblAnesTime.Text = anesTime.ToString(@"hh\:mm");

        lueAnesType.EditValue = Convert.ToInt32(_dtPageOpr.Rows[0]["AnesType"]);
        lueADr.EditValue = Convert.ToInt32(_dtPageOpr.Rows[0]["ADrID"]);
        timRecoveryStartTime.Time = DateTime.Parse(_dtPageOpr.Rows[0]["RecoveryStartTime"].ToString());
        timRecoveryEndTime.Time = DateTime.Parse(_dtPageOpr.Rows[0]["RecoveryEndTime"].ToString());

        var recoveryTime = TimeSpan.FromMinutes(long.Parse(_dtPageOpr.Rows[0]["RecoveryTime"].ToString()));
        lblRecoveryTime.Text = recoveryTime.ToString(@"hh\:mm");

        txtAndoTime.Text = _dtPageOpr.Rows[0]["AndoTime"].ToString();
    }

    private void PageTeamLoad(int pageID)
    {
        var bllPageTeam = new BLL.Page.Page();
        _dtPageTeam = bllPageTeam.PageTeamLoad(pageID);

        grdOprTeam.DataSource = _dtPageTeam;
    }


    private void EncSearch(int encID)
    {
        var bllEnc = new EncList();
        var _dtEncCur = bllEnc.EncSearch(encID, true);

        if (_dtEncCur.Rows.Count > 0)
            lblDescription.Text = _dtEncCur.Rows[0]["ChiefComplaintName"] + "-" + _dtEncCur.Rows[0]["Description"];
    }


    private bool CheckValidation()
    {
        var isValid = true;

        //if (_dtServiceReg.Rows.Count == 0)
        //{
        //    ucShowTextOpr.ShowText("لطفا کد عمل را وارد نمایید", 0);

        //    isValid = false;

        //}
        return isValid;
    }

    private void CalcTotalK()
    {
        _TotalOprK = 0;
        _TotalAnesK = 0;
        _TotalOprRoomK = 0;

        //if (_dtServiceReg.Rows.Count==0)
        //    return;

        for (var i = 0; i < _dtServiceReg.Rows.Count; i++)
        {
            var crud = Convert.ToInt32(_dtServiceReg.Rows[i]["CRUD"]);
            var oprItem = Convert.ToInt32(_dtServiceReg.Rows[i]["OprItem"]);

            if (crud != 4 && (oprItem == 1 || oprItem == 4))
                _TotalOprK += Math.Round(Convert.ToDouble(_dtServiceReg.Rows[i]["KFinal"]), 2);

            if (crud != 4 && oprItem == 2)
                _TotalAnesK += Math.Round(Convert.ToDouble(_dtServiceReg.Rows[i]["KFinal"]), 2);

            if (crud != 4 && oprItem == 3)
                _TotalOprRoomK += Math.Round(Convert.ToDouble(_dtServiceReg.Rows[i]["KFinal"]), 2);
        }

        lblTotalOprK.Text = _TotalOprK.ToString();
        lblTotalAnesK.Text = _TotalAnesK.ToString();
        lblTotalOprRoomK.Text = _TotalOprRoomK.ToString();
    }

    private void btnServiceAdd_Click(object sender, EventArgs e)
    {
        if (lueService.EditValue != null && _rowCurServie != null)
        {
            var recNo = 1;

            if (_dtServiceReg.Rows.Count > 0)
                recNo = Convert.ToInt32(_dtServiceReg.Compute("MAX([RecNo])", string.Empty)) + 1;

            var ProK = Convert.ToDouble(_rowCurServie["ProK"]);
            var AnesK = Convert.ToDouble(_rowCurServie["AnesK"]);
            var TechK = Convert.ToDouble(_rowCurServie["TechK"]);
            var Num = Convert.ToInt32(txtServiceNum.Text);
            var KPer = Convert.ToDouble(lueKPer.EditValue);
            var IsCosmetic = (bool)chkIsCosmetic.EditValue;

            double KFinal = 0;

            if (ProK > 0)
            {
                var rowService = _dtServiceReg.NewRow();

                KFinal = ProK * KPer / 100 * Num;
                if (IsCosmetic) KFinal *= 2;

                rowService["CRUD"] = 0;
                rowService["RecNo"] = recNo;
                rowService["SMID"] = 0;
                rowService["ServiceID"] = _rowCurServie["ServiceID"];
                //rowService["FSub1"] = _rowCurServie["FSub1"];
                rowService["BookAnesK"] = _rowCurServie["AnesK"];
                rowService["ServiceCode"] = _rowCurServie["ServiceCode"];
                rowService["Name"] = _rowCurServie["Name"];
                rowService["Num"] = Num;
                rowService["DrID"] = lueSDr.EditValue;
                rowService["DrName"] = lueSDr.Properties.GetDisplayText(lueSDr.EditValue);

                rowService["HDrID"] = Convert.ToInt32(lueHDr.EditValue);
                rowService["OprItem"] = 1;
                rowService["OprItemName"] = "جراحی";
                rowService["KBase"] = ProK;
                rowService["KPer"] = KPer;
                rowService["KFinal"] = KFinal;
                rowService["IsCosmetic"] = chkIsCosmetic.EditValue;

                _dtServiceReg.Rows.Add(rowService);
                //    (SRID, RecNo, DrID, OprItem, KBase, KPer, KFinal)
            }

            if (recNo != 1) AnesK = 0;

            if (recNo == 1 && Convert.ToInt32(lueADr.EditValue) > 0)
            {
                var rowService = _dtServiceReg.NewRow();

                if (AnesK == 0 && Convert.ToInt32(lueAnesType.EditValue) > 0
                               && Convert.ToInt32(lueAnesType.EditValue) != 3
                               && Convert.ToInt32(lueADr.EditValue) > 0)
                    AnesK = 4; //ارزش تام بیهوشی صفرتا1404 عدد 3 بود 1405 شد 4

                KFinal = AnesK;
                rowService["CRUD"] = 0;
                rowService["RecNo"] = recNo;
                rowService["SMID"] = 0;
                rowService["ServiceID"] = _rowCurServie["ServiceID"];
                rowService["ServiceCode"] = _rowCurServie["ServiceCode"];
                rowService["Name"] = _rowCurServie["Name"];
                rowService["Num"] = Num;
                rowService["DrID"] = lueADr.EditValue;
                rowService["DrName"] = lueADr.Properties.GetDisplayText(lueADr.EditValue);
                rowService["OprItem"] = 2;
                rowService["OprItemName"] = "بیهوشی";
                rowService["KBase"] = AnesK;
                rowService["KPer"] = KPer;
                rowService["KFinal"] = KFinal;
                rowService["IsCosmetic"] = chkIsCosmetic.EditValue;
                _dtServiceReg.Rows.Add(rowService);
            }


            if (_dtModifier.Rows.Count > 0)
                foreach (DataRow row in _dtModifier.Rows)
                {
                    if (Convert.ToInt32(row["KType"]) != 3)
                    {
                        var rowService = _dtServiceReg.NewRow();

                        KFinal = Convert.ToDouble(row["KVal"]) * KPer / 100 * Num;
                        rowService["CRUD"] = 0;
                        rowService["RecNo"] = recNo;
                        rowService["ServiceID"] = _rowCurServie["ServiceID"];
                        rowService["SMID"] = row["SMID"];
                        rowService["SMCode"] = row["Code"];
                        rowService["ServiceCode"] = _rowCurServie["ServiceCode"];
                        rowService["Name"] = _rowCurServie["Name"];
                        rowService["Num"] = Num;
                        rowService["DrID"] = row["DrID"];
                        rowService["DrName"] = row["DrName"];
                        rowService["OprItem"] = 1;
                        rowService["OprItemName"] = "تعدیلی " + row["Name"];
                        rowService["KBase"] = ProK;
                        rowService["KPer"] = KPer;
                        rowService["KFinal"] = KFinal;
                        rowService["IsCosmetic"] = chkIsCosmetic.EditValue;


                        _dtServiceReg.Rows.Add(rowService);
                    }

                    if (Convert.ToInt32(row["KType"]) == 3) // && recNo == 1)
                    {
                        var rowService = _dtServiceReg.NewRow();

                        KFinal = Convert.ToDouble(row["KVal"]);
                        rowService["CRUD"] = 0;
                        rowService["RecNo"] = recNo;
                        rowService["ServiceID"] = _rowCurServie["ServiceID"];
                        rowService["SMID"] = row["SMID"];
                        rowService["SMCode"] = row["Code"];
                        rowService["ServiceCode"] = _rowCurServie["ServiceCode"];
                        rowService["Name"] = _rowCurServie["Name"];
                        rowService["Num"] = 1;
                        rowService["DrID"] = row["DrID"];
                        rowService["DrName"] = row["DrName"];
                        rowService["OprItem"] = 2;
                        rowService["OprItemName"] = "تعدیلی " + row["Name"];
                        rowService["KBase"] = row["KVal"];
                        rowService["KPer"] = KPer;
                        rowService["KFinal"] = KFinal;
                        rowService["IsCosmetic"] = chkIsCosmetic.EditValue;

                        _dtServiceReg.Rows.Add(rowService);
                    }
                }

            grdServiceReg.DataSource = _dtServiceReg;
            //lblRecNo.Text = (_dtServiceReg.Rows.Count + 1).ToString();
            lblRecNo.Text = (recNo + 1).ToString();
        }

        ServiceNull();


        CalcTotalK();
    }

    private void ServiceNull()
    {
        lueHDr.EditValue = 0;
        txtServiceNum.Text = "1";
        lueService.EditValue = null;
        lblServiceData.Text = " ";
        txtServiceModifierManualK.Text = "0";
    }

    private void lueService_EditValueChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lueService.EditValue) == 0) return;

        _rowCurServie = lueService.Properties.GetDataSourceRowByKeyValue(lueService.EditValue) as DataRowView;
        lblServiceData.Text = _rowCurServie["ServiceCode"] + " - " +
                              //_rowCurServie["FSUB1"].ToString() + " - " +
                              //_rowCurServie["FSUB2"].ToString() + " - " +
                              //_rowCurServie["FSUB3"].ToString() + " - " +
                              "جراحی " + _rowCurServie["ProK"] + " کا - " +
                              "بیهوشی " + _rowCurServie["AnesK"] + " کا ";

        _dtModifier.Clear();
        CheckServiceModifierAuto();
    }

    private void btnModifierAdd_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lueServiceModifier.EditValue) > 0 && Convert.ToInt32(lueModifierDr.EditValue) > 0)
        {
            var rowModifier = _dtModifier.NewRow();
            var rowSelModifier =
                lueServiceModifier.Properties.GetDataSourceRowByKeyValue(lueServiceModifier.EditValue) as
                    DataRowView;


            rowModifier["RecNo"] = _dtServiceReg.Rows.Count + 1;
            rowModifier["SMID"] = rowSelModifier["SMID"];
            rowModifier["Code"] = rowSelModifier["Code"];
            rowModifier["Name"] = rowSelModifier["Name"];
            rowModifier["DrID"] = lueModifierDr.EditValue;
            rowModifier["DrName"] = lueModifierDr.Properties.GetDisplayText(lueModifierDr.EditValue);

            var KType = Convert.ToInt32(rowSelModifier["KType"]);
            var CalcType = Convert.ToInt32(rowSelModifier["CalcType"]);

            var KVal = Convert.ToDouble(rowSelModifier["KVal"]);
            if (_ServiceModifierCode == 49) KVal = double.Parse(txtServiceModifierManualK.Text.Replace('.', '/'));

            var ProK = Convert.ToDouble(_rowCurServie["ProK"]);
            var IsCosmetic = (bool)chkIsCosmetic.EditValue;

            if (KType == 1 && CalcType == 2)
            {
                KVal = ProK * KVal / 100;
                if (IsCosmetic) KVal *= 2;
            }


            rowModifier["KVal"] = KVal;
            rowModifier["KType"] = rowSelModifier["KType"];

            _dtModifier.Rows.Add(rowModifier);
            grdModifier.DataSource = _dtModifier;
        }

        lueServiceModifier.EditValue = null;
        lueModifierDr.EditValue = null;
        ActiveControl = lueServiceModifier;
    }

    private void CheckServiceModifierAuto()
    {
        var rowCount = 0;
        rowCount = _dtServiceReg.Rows.Count;
        var ServiceCode = Convert.ToInt32(_rowCurServie["ServiceCode"]);

        var bllServiceRegOpr = new ServiceRegOpr();
        //زمان بیهوشی
        if (rowCount == 0 && _AnesTime > 0 && Convert.ToInt32(lueADr.EditValue) > 0)
        {
            var AnesK = bllServiceRegOpr.CalAnesKByTime(_AnesTime);
            ServiceModifierAuto(41, 1, 2, AnesK);
        }

        //زمان ریکاوری
        if (rowCount == 0) // && _RecoveryTime > 0)
        {
            var RecoveryK1 =
                bllServiceRegOpr.CalRecoveryKByOprTime(_OprTime, Convert.ToInt32(lueAnesType.EditValue));

            var RecoveryK2 = bllServiceRegOpr.CalRecoveryKByTime(_RecoveryTime);
            var RecoveryK = RecoveryK1 + RecoveryK2;
            if (RecoveryK > 4) RecoveryK = 4;

            //bool IsRino = ServiceCode == 300070 || ServiceCode == 300075 || ServiceCode == 300080;

            if (RecoveryK > 0 && Convert.ToInt32(lueADr.EditValue) > 0) ServiceModifierAuto(39, 1, 2, RecoveryK);
        }

        ////سن
        //if (_RecoveryTime > 0)
        //{
        //    int RecoveryK = bllServiceRegOpr.CalRecoveryKByTime(_RecoveryTime);
        //    ServiceModifierAuto(39, 1, 2, RecoveryK);
        //}
        ////اورژانس
        //if (_RecoveryTime > 0)
        //{
        //    int RecoveryK = bllServiceRegOpr.CalRecoveryKByTime(_RecoveryTime);
        //    ServiceModifierAuto(39, 1, 2, RecoveryK);
        //}
    }

    private void ServiceModifierAuto(int Code, int RecNo, int DrType, double Num)
    {
        var row = DAL.GL.ds.Tables["ServiceModifier"].Select("Code=" + Code)[0];

        var rowModifier = _dtModifier.NewRow();
        var rowSelModifier =
            lueServiceModifier.Properties.GetDataSourceRowByKeyValue(lueServiceModifier.EditValue) as DataRowView;

        rowModifier["RecNo"] = RecNo;
        rowModifier["SMID"] = row["SMID"];
        rowModifier["Code"] = row["Code"];
        rowModifier["Name"] = row["Name"];

        var DrID = 0;
        string DrName = null;

        double KVal = 0;
        KVal = Convert.ToDouble(row["KVal"]) * Num;

        if (DrType == 1)
        {
            DrID = Convert.ToInt32(lueSDr.EditValue);
            DrName = lueSDr.Properties.GetDisplayText(lueSDr.EditValue);
        }

        if (DrType == 2)
        {
            DrID = Convert.ToInt32(lueADr.EditValue);
            DrName = lueADr.Properties.GetDisplayText(lueADr.EditValue);
        }

        rowModifier["DrID"] = DrID;

        rowModifier["DrName"] = DrName;
        rowModifier["KVal"] = KVal;
        rowModifier["KType"] = row["KType"];

        _dtModifier.Rows.Add(rowModifier);
        grdModifier.DataSource = _dtModifier;
    }

    private void CheckMultiFieldOpr()
    {
        //var dtOpr = _dtServiceReg.Select("OprItem=1 AND SMID=0").CopyToDataTable();

        //string FSub1 = dtOpr.Rows[0]["FSub1"].ToString();

        //double MaxAnesK = 0;
        //double TotalAnesK = 0;

        //for (int i = 0; i < dtOpr.Rows.Count; i++)
        //{
        //    if (FSub1 != dtOpr.Rows[i]["FSub1"].ToString())
        //    {
        //        _MuliFieldOpr = true;
        //        i = dtOpr.Rows.Count - 1; //پایان حلقه
        //    }
        //}

        //if (_MuliFieldOpr == false)
        //    return;


        //for (int i = 0; i < dtOpr.Rows.Count; i++)
        //{
        //    if (FSub1 != dtOpr.Rows[i]["FSub1"].ToString())
        //        _MuliFieldOpr = true;

        //    double AnesK = Convert.ToDouble(dtOpr.Rows[i]["BookAnesK"]);
        //    if (AnesK > MaxAnesK)
        //        MaxAnesK = AnesK;

        //    TotalAnesK += AnesK;
        //}


        //if (_MuliFieldOpr == true)
        //{
        //    double FinalAnesK = 0;
        //    FinalAnesK = MaxAnesK + (TotalAnesK - MaxAnesK) * 0.25;
        //    _dtServiceReg.Rows[1]["KBase"] = 0;
        //    _dtServiceReg.Rows[1]["KFinal"] = 0;

        //    ServiceModifierAuto(49, 1, 2, FinalAnesK);

        //    DataRow rowService = _dtServiceReg.NewRow();


        //    rowService["CRUD"] = 0;
        //    rowService["RecNo"] = 1;
        //    rowService["ServiceID"] = _OprServiceID;
        //    rowService["SMID"] = 17;
        //    rowService["ServiceCode"] = 49;
        //    rowService["Name"] = "اعمال جراحی متعدد با ارزشهای تام بیهوشی";
        //    rowService["Num"] = 1;
        //    rowService["DrID"] = lueADr.EditValue;
        //    rowService["DrName"] = lueADr.Properties.GetDisplayText(lueADr.EditValue);
        //    rowService["OprItem"] = 2;
        //    rowService["OprItemName"] = "تعدیلی بیهوشی " ;
        //    rowService["KBase"] = FinalAnesK;
        //    rowService["KPer"] = 100;
        //    rowService["KFinal"] = FinalAnesK;
        //    rowService["IsCosmetic"] = chkIsCosmetic.EditValue;

        //    _dtServiceReg.Rows.Add(rowService);
        //    grdServiceReg.DataSource = _dtServiceReg;
        //}
    }

    private void btnServiceRegSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        if (_dtServiceReg.Rows.Count > 0)
        {
            _dtServiceReg = _bllOprCalc.CheckAnesK(_dtServiceReg);
            grdServiceReg.DataSource = _dtServiceReg;
        }

        var bllEncServiceControl = new EncServiceControl();
        bllEncServiceControl.EncServiceControlSave(_EncID, 3, _dtServiceReg.Select("CRUD<>4").Length > 0);


        var bllPage = new BLL.Page.Page();
        _dtPage = bllPage.PageSave
        (_ModID, _DepID, _StorageID, _StoragePeriodID, _EncID, GL.UserPersID, _PageID,
            datOprStartDate.Text + " " + timOprStartTime.Text,
            datOprEndDate.Text + " " + timOprEndTime.Text, 0, 0);


        _PageID = (int)_dtPage.Rows[0]["PageID"];
        lblPageID.Text = _PageID.ToString();

        OprTeamSave();


        var bllOpr = new BLL.Opr.Page();

        if (_dtServiceReg.Rows.Count > 0) _OprServiceID = Convert.ToInt32(_dtServiceReg.Rows[0]["ServiceID"]);
        // CheckMultiFieldOpr();


        if (_dtServiceReg.Rows.Count > 0) // کنترل کای بیهوشی
            _dtServiceReg = _bllOprCalc.CheckAnesK(_dtServiceReg);

        CalcTotalK();


        string strAnesStartTime = ((DateTime)timAnesStartTime.EditValue).ToString("yyyy/MM/dd HH:mm",
            CultureInfo.CreateSpecificCulture("fa-IR"));

        string strAnesEndTime = ((DateTime)timAnesEndTime.EditValue).ToString("yyyy/MM/dd HH:mm",
            CultureInfo.CreateSpecificCulture("fa-IR"));


        string strRecoveryStartTime = ((DateTime)timRecoveryStartTime.EditValue).ToString("yyyy/MM/dd HH:mm",
            CultureInfo.CreateSpecificCulture("fa-IR"));

        string strRecoveryEndTime = ((DateTime)timRecoveryEndTime.EditValue).ToString("yyyy/MM/dd HH:mm",
            CultureInfo.CreateSpecificCulture("fa-IR"));

        strRecoveryStartTime = strAnesStartTime.Substring(0, 11) + strRecoveryStartTime.Substring(11, 5);
        strRecoveryEndTime = strAnesEndTime.Substring(0, 11) + strRecoveryEndTime.Substring(11, 5);


        bllOpr.PageOprSave(0, _PageID, _OprServiceID, lueCategory.EditValue,
            lueCategory1.EditValue,
            lueADr.EditValue,
            lueAnesType.EditValue,
            _OprTime,
            datOprStartDate.Text + " " + timOprStartTime.Text,
            datOprEndDate.Text + " " + timOprEndTime.Text,

            _AnesTime,
            strAnesStartTime,
            strAnesEndTime,

            _RecoveryTime,

            strRecoveryStartTime,
            strRecoveryEndTime,

            Convert.ToInt32(txtAndoTime.EditValue),

            _TotalOprK, _TotalAnesK, 0, GL.UserPersID
        );


        ServiceRegOprSave();
        bllPage.PageAmountSave(_PageID);

        EncPageLoad();
        PageDataLoad();

        _dtModifier.Clear();
        grdServiceReg.Update();


        ucShowTextOprSave.ShowText("اطلاعات عمل با موفقیت ذخیره گردید", 9000);
    }


    private void ServiceRegOprSave()
    {
        if (_dtServiceReg.Rows.Count == 0) return;

        var bllServiceReg = new ServiceRegOpr();
        DataTable dtOpr = null;
        var result = _dtServiceReg.Select("CRUD<>2 AND SMID=0");
        if (result.Count() > 0) dtOpr = result.CopyToDataTable();

        DataTable dtServiceReg;
        var RecNo = 0;

        if (dtOpr != null)
            foreach (DataRow row in dtOpr.Rows)
            {
                RecNo = Convert.ToInt32(row["RecNo"]);

                var CRUD = Convert.ToByte(row["CRUD"]);
                var ServiceID = Convert.ToInt32(row["ServiceID"]);
                var Num = Convert.ToInt32(row["Num"]);
                var OprItem = Convert.ToInt32(row["OprItem"]);
                var DrID = Convert.ToInt32(row["DrID"]);
                var HDrID = Convert.ToInt32(row["HDrID"]);
                var KBase = Convert.ToDouble(row["KBase"]);
                var KPer = Convert.ToByte(row["KPer"]);
                var IsCosmetic = Convert.ToBoolean(row["IsCosmetic"]);

                dtServiceReg = bllServiceReg.ServiceRegOprSave
                    (CRUD, _PageID, ServiceID, 0, 0, RecNo, Num, OprItem, DrID, HDrID, KBase, KPer, IsCosmetic);

                //if (Convert.ToByte(row["CRUD"]) == 4)
                //    return;
                if (dtServiceReg.Rows.Count > 0 && CRUD != 4)
                {
                    var SRID = Convert.ToInt32(dtServiceReg.Rows[0]["SRID"]);
                    OprItem = Convert.ToInt32(dtServiceReg.Rows[0]["OprItem"]);

                    if (OprItem == 1)
                    {
                        var filterM = "CRUD<>2 AND SMID>0 AND RecNo=" + RecNo;

                        if (_dtServiceReg.Select(filterM).Length > 0)
                        {
                            var rowM = _dtServiceReg.Select("CRUD<>2 AND SMID>0 AND RecNo=" + RecNo);

                            for (var i = 0; i < rowM.Length; i++)
                            {
                                bllServiceReg.ServiceRegOprModifierSave
                                (Convert.ToByte(rowM[i]["CRUD"]), _PageID, Convert.ToInt32(rowM[i]["ServiceID"]),
                                    SRID,
                                    Convert.ToInt32(rowM[i]["SMID"]), RecNo, Convert.ToInt32(rowM[i]["Num"])
                                    , Convert.ToInt32(rowM[i]["OprItem"]), Convert.ToInt32(rowM[i]["DrID"]),
                                    Convert.ToDouble(rowM[i]["KBase"])
                                    , Convert.ToByte(rowM[i]["KPer"]), Convert.ToBoolean(rowM[i]["IsCosmetic"])
                                );

                                if (Convert.ToByte(rowM[i]["CRUD"]) == 0) row["CRUD"] = 2;
                            }
                        }
                    }
                }
            }

        ServiceRegLoad(_PageID);
    }

    private void tab1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(timOprStartTime, e);
        Control.TextEditLine(datOprStartDate, e);
        Control.TextEditLine(timOprEndTime, e);
        Control.TextEditLine(datOprEndDate, e);
        Control.TextEditLine(timAnesStartTime, e);
        Control.TextEditLine(timAnesEndTime, e);
        Control.TextEditLine(timRecoveryStartTime, e);
        Control.TextEditLine(lueCategory, e);
        Control.TextEditLine(lueCategory1, e);
        Control.TextEditLine(lueAnesType, e);
        Control.TextEditLine(lueADr, e);
        Control.TextEditLine(timRecoveryEndTime, e);
        Control.TextEditLine(txtAndoTime, e);
    }

    private void panOprTeam_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(lueOprTeamRole, e);
        Control.TextEditLine(lueOprTeamPers, e);
    }

    private void tab2_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(lueService, e);
        Control.TextEditLine(txtServiceNum, e);
        Control.TextEditLine(lueKPer, e);
        Control.TextEditLine(lueSDr, e);
        Control.TextEditLine(lueHDr, e);
        Control.TextEditLine(lueServiceModifier, e);
        Control.TextEditLine(lueModifierDr, e);
    }

    private void btnOprTeamAdd_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lueOprTeamRole.EditValue) > 0 && Convert.ToInt32(lueOprTeamPers.EditValue) > 0)
        {
            var row = _dtPageTeam.NewRow();

            var persID = Convert.ToInt32(lueOprTeamPers.EditValue);
            var roleID = Convert.ToInt32(lueOprTeamRole.EditValue);

            row["CRUD"] = 0;
            row["PersID"] = persID;
            row["PersName"] = lueOprTeamPers.Properties.GetDisplayText(lueOprTeamPers.EditValue);

            row["RoleID"] = roleID;
            row["RoleName"] = lueOprTeamRole.Properties.GetDisplayText(lueOprTeamRole.EditValue);

            //string filter = @"PersID=" + persID + " OR RoleID=" + roleID;
            //if (_dtPageTeam.Select(filter).Length == 0) //Check For Duplicate
            //    _dtPageTeam.Rows.Add(row);

            _dtPageTeam.Rows.Add(row);
            grdOprTeam.DataSource = _dtPageTeam;
        }

        lueOprTeamRole.EditValue = 0;
        lueOprTeamPers.EditValue = 0;

        ActiveControl = lueOprTeamRole;
    }

    private void OprTeamSave()
    {
        var bllPage = new BLL.Page.Page();

        if (_dtPageTeam.Rows.Count > 0)
            foreach (DataRow row in _dtPageTeam.Rows)
            {
                int CRUD = Convert.ToByte(row["CRUD"]);

                bllPage.PageTeamSave(CRUD, _PageID, Convert.ToInt32(row["PersID"].ToString()),
                    Convert.ToInt32(row["RoleID"].ToString()));

                if (CRUD == 0) row["CRUD"] = 2;
            }
    }


    private void datOprStartDate_DateTimeChanged(object sender, EventArgs e)
    {
        datOprEndDate.DateTime = datOprStartDate.DateTime;
    }

    private void btnOprTeamDel_Click(object sender, EventArgs e)
    {
        var row = grvOprTeam.GetFocusedDataRow();
        if (row != null)
        {
            var filter = @"PersID=" + row["PersID"] + " AND RoleID=" + row["RoleID"];

            var crud = Convert.ToByte(_dtPageTeam.Select(filter)[0]["CRUD"]);

            if (crud == 0) _dtPageTeam.Select(filter)[0].Delete();

            if (crud == 2) _dtPageTeam.Select(filter)[0]["CRUD"] = 4;
        }

        if (_dtPageTeam.Select("CRUD<4").Length > 0)
            grdOprTeam.DataSource = _dtPageTeam.Select("CRUD<4").CopyToDataTable();
    }

    private void CalcOprTime(object sender, EventArgs e)
    {
        var appSDate = new SDate();
        var OprStartDateTime = appSDate.SDateConvertFromString(datOprStartDate.Text, timOprStartTime.Text);
        var OprEndDateTime = appSDate.SDateConvertFromString(datOprEndDate.Text, timOprEndTime.Text);
        var oprTime = OprEndDateTime.Subtract(OprStartDateTime);
        _OprTime = Convert.ToInt32(oprTime.TotalMinutes);

        lblOprTime.Text = oprTime.ToString(@"hh\:mm");

        //if (_PageID == 0)
        //{
        timAnesStartTime.Time = OprStartDateTime.AddMinutes(0);
        timAnesEndTime.Time = OprEndDateTime.AddMinutes(0);

        CalcAnesTime(null, null);
        //}
    }

    private void CalcAnesTime(object sender, EventArgs e)
    {
        var anesTime = timAnesEndTime.Time.Subtract(timAnesStartTime.Time);
        _AnesTime = Convert.ToInt32(anesTime.TotalMinutes);
        lblAnesTime.Text = anesTime.ToString(@"hh\:mm");
    }

    private void CalcRecoveryTime(object sender, EventArgs e)
    {
        var recoveryTime = timRecoveryEndTime.Time.Subtract(timRecoveryStartTime.Time);
        _RecoveryTime = Convert.ToInt32(recoveryTime.TotalMinutes);

        lblRecoveryTime.Text = recoveryTime.ToString(@"hh\:mm");
    }

    private void btnModifierDel_Click(object sender, EventArgs e)
    {
        var row = grvModifier.GetFocusedDataRow();

        var filter = @"SMID=" + row["SMID"];
        _dtModifier.Select(filter)[0].Delete();
    }

    private void btnServiceDel_Click(object sender, EventArgs e)
    {
        var groupRecNo = grvServiceReg.GetGroupRowValue(grvServiceReg.FocusedRowHandle, gcolRecNo);
        var focRow = grvServiceReg.GetFocusedDataRow();

        var RecNo = 0;
        if (groupRecNo != null) RecNo = Convert.ToInt32(groupRecNo);

        if (focRow != null) RecNo = Convert.ToInt32(focRow["RecNo"]);

        if (groupRecNo != null)
        {
            var message = "عمل ردیف شماره <" + RecNo + "> حذف گردد؟";
            var title = "حذف عمل";
            var buttons = MessageBoxButtons.YesNo;

            var result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                var filter = @"RecNo=" + groupRecNo;


                var rows = _dtServiceReg.Select(filter);
                foreach (var row in rows)
                {
                    if (Convert.ToInt32(row["CRUD"]) == 2) row["CRUD"] = 4;

                    if (Convert.ToInt32(row["CRUD"]) == 0) row.Delete();
                }

                btnServiceRegSave_Click(null, null);
            }
        }


        //grdServiceReg.DataSource = null;
        //grdServiceReg.DataSource = _dtServiceReg;

        //CalcTotalK();
    }

    private void btnServiceDelAll_Click(object sender, EventArgs e)
    {
        var message = "تمام عمل ها حذف گردد؟";
        var title = "حذف تمام عمل ها";
        var buttons = MessageBoxButtons.YesNo;

        var result = MessageBox.Show(message, title, buttons);
        if (result == DialogResult.No) return;

        for (var i = 0; i < _dtServiceReg.Rows.Count; i++)
            if (Convert.ToInt32(_dtServiceReg.Rows[i]["CRUD"]) == 2)
            {
                _dtServiceReg.Rows[i]["CRUD"] = 4;
            }

            else if (Convert.ToInt32(_dtServiceReg.Rows[i]["CRUD"]) == 0)
            {
                _dtServiceReg.Rows[i].Delete();
                i--;
            }

        btnServiceRegSave_Click(null, null);
    }


    private void AddNewPage()
    {
        FormNull();
        _PageID = 0;
        _dtServiceReg.Clear();
        grdServiceReg.DataSource = _dtServiceReg;

        lblPageID.Text = null;


        // datPageAnsDate.Text = "";
        // timPageAnsTime.Time = Convert.ToDateTime("00:00");
    }

    private void lblPageNew_Click(object sender, EventArgs e)
    {
        AddNewPage();
    }

    private void lblPagePreRec_Click(object sender, EventArgs e)
    {
        PageNavigate(-1);
    }


    private void lblPageNextRec_Click(object sender, EventArgs e)
    {
        PageNavigate(1);
    }

    private void grvServiceReg_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        var view = sender as GridView;

        var crud = (int)view.GetRowCellValue(e.RowHandle, "CRUD");
        if (crud == 4) e.Appearance.BackColor = Color.PaleVioletRed;
    }


    private void lueServiceModifier_EditValueChanged(object sender, EventArgs e)
    {
        panServiceModifierManualK.Visible = false;

        if (Convert.ToInt32(lueServiceModifier.EditValue) == 0) return;

        var rowServiceModifier =
            lueServiceModifier.Properties.GetDataSourceRowByKeyValue(lueServiceModifier.EditValue) as DataRowView;

        var KType = Convert.ToInt32(rowServiceModifier["KType"]);
        lueModifierDr.EditValue = KType == 3 ? lueADr.EditValue : lueSDr.EditValue;

        _ServiceModifierCode = Convert.ToInt32(rowServiceModifier["Code"]);

        if (_ServiceModifierCode == 49) panServiceModifierManualK.Visible = true;

        ActiveControl = btnModifierAdd;
    }

    private void PageNavigate(int navigate)
    {
        if (_dtEncPage.Rows.Count == 0 || _PageID == 0) return;

        _PageListRecNo = Convert.ToInt32(_dtEncPage.Select("PageID=" + _PageID)[0]["RecNo"]);
        _PageListRecNo = _PageListRecNo + navigate;

        if ((_PageListRecNo >= 1 && _PageListRecNo <= _dtEncPage.Rows.Count && _PageListRecNo > 0)
            || (_PageListRecNo <= _dtEncPage.Rows.Count && _PageListRecNo > 0 && _PageListRecNo > 0))
        {
            _PageID = Convert.ToInt32(_dtEncPage.Rows[_PageListRecNo - 1]["PageID"].ToString());

            if (_PageID > 0) PageDataLoad();

            lblPageList.Text = "صفحه " + _PageListRecNo + " از " + _dtEncPage.Rows.Count;
        }
    }

    private void btnServiceRegDrug_Click(object sender, EventArgs e)
    {
        if (_PageID == 0) return;

        var panUc = Parent as PanelControl;
        var ucControl = new UcServiceRegDrug
        {
            _ModID = 28,
            _DepID = _DepID,
            _StorageID = _StorageID,
            _StoragePeriodID = _StoragePeriodID,
            _ReadOnly = _ReadOnly
        };
        ShowUCInParent(panUc, ucControl);
    }

    private void lueAnesType_Enter(object sender, EventArgs e)
    {
        UL.LangChange(1);
    }

    private void btnPrintPreviewOpr_Click(object sender, EventArgs e)
    {
        grdServiceReg.ShowPrintPreview();
    }

    private void lueService_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 43 && _rowCurServie != null) //+
        {
            btnServiceAdd_Click(sender, e);
            //EncSearch(int.Parse(txtEncID.Text));
            lueService.EditValue = null;
            e.KeyChar = (char)0;
        }
    }

    private void grdOprTeam_Click(object sender, EventArgs e)
    {
    }

    private void lueService_Enter(object sender, EventArgs e)
    {
        //if (_dtServiceReg.Rows.Count == 0)
        //    lueKPer.EditValue = 100;
        //else
        //    lueKPer.EditValue = 80;
    }

    private void lueOprTeamPers_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13) btnOprTeamAdd_Click(null, null);
    }

    private void lueOprTeamRole_EditValueChanged(object sender, EventArgs e)
    {
        ActiveControl = lueOprTeamPers;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        if (_ModID == 6)
        {
            var panUC = Parent as PanelControl;
            panUC.Controls.Clear();

            var ucEncDis = new UcEncDis
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                Dock = DockStyle.Fill,
                _ModID = _ModID,
                _DepID = _DepID
            };
            panUC.Controls.Add(ucEncDis);
        }
        else
        {
            var panUC = Parent as PanelControl;
            panUC.Controls.Clear();

            var ucEncCur = new UcEncCur
                { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
            panUC.Controls.Add(ucEncCur);
        }
    }

    private void btnServiceReg_Click(object sender, EventArgs e)
    {
        if (_PageID == 0) return;

        var panUc = Parent as PanelControl;
        var ucControl = new UcServiceReg
        {
            _ModID = 27,
            _DepID = _DepID,
            _StorageID = _StorageID,
            _StoragePeriodID = _StoragePeriodID,
            _ReadOnly = _ReadOnly
        };
        ShowUCInParent(panUc, ucControl);
    }

    private void ShowUCInParent(PanelControl panUC, object ucControl)
    {
        panUC.Controls.Clear();
        var myUC = (UserControl)ucControl;
        myUC.Anchor = AnchorStyles.Top;
        myUC.Anchor = AnchorStyles.Right;
        myUC.Dock = DockStyle.Fill;

        Text += " - " + myUC.Tag;
        panUC.Controls.Add(myUC);
    }
}