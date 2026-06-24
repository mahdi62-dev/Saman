using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using LIB;
using Saman.BLL.FormSetting;

namespace Saman.UC.Acc.Bond;

public partial class UcBond : XtraUserControl
{
    private readonly App.UI _appUI = new();
    private readonly BLL.Acc.Bond.Bond _bllBondChandesSave = new();
    private readonly BLL.Acc.Bond.Bond _bllBondChangesList = new();


    private readonly BLL.Acc.Bond.Bond _bllBondList = new();
    private readonly BLL.Acc.Bond.Bond _bllBondSave = new();


    private int[,] _arrRowCrud;
    public int _BondID;
    public DataTable _dtBond;
    private DataTable _dtBondChangesList;
    private DataTable _dtBondList;
    private DataRow _rowbondSearch;


    public UcBond()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }


    private void UcBond_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        ActiveControl = flpMain;
        ActiveControl = lueBranch;

        MaskSet();
        FillLookup();
        FormSetting();
        BondSearch(_BondID);

        if (_BondID == 0)
        {
            panel16.Enabled = false;
            panel15.Enabled = false;
        }
    }


    private void MaskSet()
    {
        txtBondSerial.Properties.Mask.EditMask = App.UI.MaskNum;
        txtBondAmount.Properties.Mask.EditMask = App.UI.MaskNum;
        txtContractId.Properties.Mask.EditMask = App.UI.MaskPersianWithNum;
        txtTenderId.Properties.Mask.EditMask = App.UI.MaskPersianWithNum;
        txtBankBranch.Properties.Mask.EditMask = App.UI.MaskPersianWithNum;
    }

    private void FillLookup()
    {
        UL.FillLookup(lueBranch, "0", cSet.cnStringERP, false, "*", "AccBranch", "AccBranch",
            null, null);
        UL.FillLookup(lueBondType, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "BondType",
            "ADName='BondType'", null);
        UL.FillLookup(lueAccSide, "0", cSet.cnStringERP, false, "SideId,SideName,NationalId", "AccSide", "AccSide",
            null, null);
        UL.FillLookup(lueBondReason, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "BondReason",
            "ADName='BondReason'", null);
        UL.FillLookup(lueBondCategory, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "BondCategory",
            "ADName='BondCategory'", null);
        UL.FillLookup(lueCurrencyType, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "CurrencyType",
            "ADName='CurrencyType'", null);
        UL.FillLookup(lueBank, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "Bank",
            "ADName='Bank'", null);
        UL.FillLookup(lueBondStatus, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "BondStatus",
            "ADName='BondStatus'", null);
    }

    private void lblSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        BondSave();
    }

    private void BondSave()
    {
        var CRUD = 0;

        if (_BondID > 0)
            CRUD = 2;

        _dtBond = _bllBondSave.BondSave(CRUD, Convert.ToInt32(lueBranch.EditValue),
            Convert.ToInt32(lueBondType.EditValue),
            Convert.ToInt32(lueAccSide.EditValue), Convert.ToInt32(lueBondReason.EditValue),
            Convert.ToInt32(lueBondCategory.EditValue), txtBondSerial.Text,
            _appUI.SDateCheck(datRegDate.Text),
            _appUI.SDateCheck(datDeliveryDate.Text),
            _appUI.SDateCheck(datStartDate.Text),
            _appUI.SDateCheck(datEndDate.Text),
            Convert.ToInt64(txtBondAmount.EditValue),
            Convert.ToInt32(lueCurrencyType.EditValue), txtContractId.Text, txtTenderId.Text, txtSubject.Text,
            txtDescription.Text, Convert.ToInt32(lueBank.EditValue), txtBankBranch.Text,
            Convert.ToInt32(lueBondStatus.EditValue),
            txtSubId.Text, GL.UserPersID);
    }

    private void BondSearch(int bondID)
    {
        if (bondID == 0) return;
        _dtBondList = _bllBondList.BondSearch(5, bondID, 0, 0, 0, 0, 0, null, null, null, null, null, null, null, null,
            null, 0, null, 0, null);
        BondLoad();

        _dtBondChangesList = _bllBondChangesList.BondChangesList(1, bondID);
        gridBondChanges.DataSource = _dtBondChangesList;
    }

    private void BondLoad()
    {
        txtBondId.Text = _dtBondList.Rows[0]["BondId"].ToString();
        lueBranch.EditValue = Convert.ToInt32(_dtBondList.Rows[0]["BranchId"]);
        lueBondType.EditValue = Convert.ToInt32(_dtBondList.Rows[0]["BondType"]);
        lueAccSide.EditValue = Convert.ToInt32(_dtBondList.Rows[0]["AccSide"]);
        lueBondReason.EditValue = Convert.ToInt32(_dtBondList.Rows[0]["BondReason"]);
        lueBondCategory.EditValue = Convert.ToInt32(_dtBondList.Rows[0]["BondCategory"]);
        txtBondSerial.Text = _dtBondList.Rows[0]["BondSerial"].ToString();
        if (!string.IsNullOrEmpty(_dtBondList.Rows[0]["RegDate"].ToString()))
            datRegDate.DateTime = DateTime.Parse(_dtBondList.Rows[0]["RegDate"].ToString());
        if (!string.IsNullOrEmpty(_dtBondList.Rows[0]["DeliveryDate"].ToString()))
            datDeliveryDate.DateTime = DateTime.Parse(_dtBondList.Rows[0]["DeliveryDate"].ToString());
        if (!string.IsNullOrEmpty(_dtBondList.Rows[0]["StartDate"].ToString()))
            datStartDate.DateTime = DateTime.Parse(_dtBondList.Rows[0]["StartDate"].ToString());
        if (!string.IsNullOrEmpty(_dtBondList.Rows[0]["EndDate"].ToString()))
            datEndDate.DateTime = DateTime.Parse(_dtBondList.Rows[0]["EndDate"].ToString());
        txtBondAmount.Text = _dtBondList.Rows[0]["BondAmount"].ToString();
        lueCurrencyType.EditValue = Convert.ToInt32(_dtBondList.Rows[0]["CurrencyType"]);
        txtContractId.Text = _dtBondList.Rows[0]["ContractId"].ToString();
        txtTenderId.Text = _dtBondList.Rows[0]["Tender"].ToString();
        txtSubject.Text = _dtBondList.Rows[0]["Subject"].ToString();
        txtDescription.Text = _dtBondList.Rows[0]["Description"].ToString();
        lueBank.EditValue = Convert.ToInt32(_dtBondList.Rows[0]["Bank"]);
        txtBankBranch.Text = _dtBondList.Rows[0]["BankBranch"].ToString();
        lueBondStatus.EditValue = Convert.ToInt32(_dtBondList.Rows[0]["BondStatus"]);
        txtSubId.Text = _dtBondList.Rows[0]["AccountSubId"].ToString();
    }

    private void lblEncNext_Click(object sender, EventArgs e)
    {
        var oldEncID = _BondID;

        _BondID += 1;
        BondSearch(_BondID);

        if (_BondID == 0) BondSearch(oldEncID);
    }

    private void lblPagePreRec_Click(object sender, EventArgs e)
    {
        var oldEncID = _BondID;

        _BondID -= 1;
        BondSearch(_BondID);

        if (_BondID == 0) BondSearch(oldEncID);
    }

    private void lblNew_Click(object sender, EventArgs e)
    {
        grvBondChanges.AddNewRow();
        //int newRowHandle = grvBondChanges.FocusedRowHandle;
        //grvBondChanges.SetRowCellValue(newRowHandle, grvBondChanges.Columns["RowNum"], 10);
        //grvBondChanges.UpdateCurrentRow();
        //grvBondChanges.RefreshData();
    }

    private void lblSaveBondChanges_Click(object sender, EventArgs e)
    {
        BondChangesSave();
    }

    private void grvBondChanges_RowUpdated(object sender, RowObjectEventArgs e)
    {
        var rowhandle = e.RowHandle;
        var lastRowHandle = grvBondChanges.RowCount - 1;

        int recHandle;

        if (rowhandle >= 0)
            recHandle = rowhandle;
        else
            recHandle = lastRowHandle;


        var strId = grvBondChanges.GetRowCellValue(recHandle, "Id").ToString();
        long id = 0;

        if (!string.IsNullOrEmpty(strId))
            id = Convert.ToInt32(grvBondChanges.GetRowCellValue(recHandle, "Id"));


        if (id == 0)
        {
            var recCount = _dtBondChangesList.Rows.Count;
            _dtBondChangesList.Rows[recCount - 1]["CRUD"] = 0;
            _dtBondChangesList.Rows[recCount - 1]["Id"] = 0;
        }

        if (id > 0)
        {
            var row = _dtBondChangesList.Select("Id=" + id)[0];
            row["CRUD"] = 2;
        }
    }

    private void BondChangesSave()
    {
        if (_dtBondChangesList.Rows.Count == 0) return;

        int crud;

        var result = _dtBondChangesList.Select("CRUD <> 1");

        DataTable dt = null;
        if (result.Length > 0) dt = result.CopyToDataTable();

        if (dt == null) return;

        foreach (DataRow row in dt.Rows)
        {
            crud = Convert.ToInt32(row["CRUD"]);
            var Id = Convert.ToInt32(row["Id"]);

            var LetterNumber = row["LetterNumber"].ToString();
            var RegDate = row["RegDate"].ToString();
            var DeliveryDate = row["DeliveryDate"].ToString();
            var StartDate = row["StartDate"].ToString();
            var EndDate = row["EndDate"].ToString();
            var BondAmount = Convert.ToInt64(row["BondAmount"]);

            _bllBondChandesSave.BondChandesSave(crud, _BondID, Id, LetterNumber, RegDate, DeliveryDate,
                StartDate, EndDate, BondAmount, GL.UserPersID);

            _dtBondChangesList = _bllBondChangesList.BondChangesList(1, _BondID);
            gridBondChanges.DataSource = _dtBondChangesList;

            //if (!string.IsNullOrEmpty(_dtBondChangesList.Rows[0]["RegDate"].ToString()))
            //{
            //    repdatRegDate. = DateTime.Parse(_dtBondChangesList.Rows[0]["RegDate"].ToString());

            //}
            //if (!string.IsNullOrEmpty(_dtBondChangesList.Rows[0]["DeliveryDate"].ToString()))
            //{
            //    datDeliveryDate.DateTime = DateTime.Parse(_dtBondChangesList.Rows[0]["DeliveryDate"].ToString());
            //}
            //if (!string.IsNullOrEmpty(_dtBondChangesList.Rows[0]["StartDate"].ToString()))
            //{
            //    datStartDate.DateTime = DateTime.Parse(_dtBondChangesList.Rows[0]["StartDate"].ToString());
            //}
            //if (!string.IsNullOrEmpty(_dtBondChangesList.Rows[0]["EndDate"].ToString()))
            //{
            //    datEndDate.DateTime = DateTime.Parse(_dtBondChangesList.Rows[0]["EndDate"].ToString());
            //}
        }
    }

    private void FormSetting()
    {
        _dtFormSetting = _bllFormSetting.FormSettingLoad(_FormID);
        FormSettingUI();
    }

    private void FormSettingUI()
    {
        FlowLayoutPanel flp = null;

        //int b = flo.Controls.Count;
        string UiName, UiCaption, UiTypePrefix;
        var UiType = 0;


        Control[] ctrl;

        //bool ctrlTabStop = false;
        int FormSubID = 0, UiRecNo = 0;


        var dtFormSetting = _dtFormSetting.Select("IsUI=1").CopyToDataTable();

        foreach (DataRow row in dtFormSetting.Rows)
        {
            FormSubID = Convert.ToInt32(row["FormSubID"]);
            UiName = row["UiName"].ToString();
            UiType = Convert.ToInt32(row["Uitype"]);
            UiTypePrefix = row["UiTypePrefix"].ToString();

            UiCaption = row["UiCaption"].ToString();
            UiRecNo = Convert.ToInt32(row["RecNo"]);

            flp = FormSubID == 1 ? flpMain : flpSide;

            ctrl = flp.Controls.Find("pan" + UiName, false);
            if (ctrl.Length == 0) continue;

            ctrl[0].Enabled = Convert.ToBoolean(row["Enabled"]);
            ctrl[0].Visible = Convert.ToBoolean(row["Visible"]);
            //ctrl[0].ReadOnly = Convert.ToBoolean(row["ReadOnly"]);


            var lblCaption = ctrl[0].Controls.Find("lbl" + UiName, false);
            if (ctrl.Length > 0) lblCaption[0].Text = UiCaption;
        }
    }

    private bool CheckValidation()
    {
        var isValid = true;

        var BranchIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "Branch");
        if (BranchIsRequired && lueBranch.EditValue.ToString() == "0" &&
            string.IsNullOrEmpty(lueBranch.Text))
        {
            lueBranch.ErrorText = "مرکز را وارد نمایید";
            isValid = false;
        }

        var BondTypeIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "BondType");
        if (BondTypeIsRequired && lueBondType.EditValue.ToString() == "0" &&
            string.IsNullOrEmpty(lueBondType.Text))
        {
            lueBondType.ErrorText = "نوع ضمانتنامه را وارد نمایید";
            isValid = false;
        }

        var AccSideIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "AccSide");
        if (AccSideIsRequired && lueAccSide.EditValue.ToString() == "0" &&
            string.IsNullOrEmpty(lueAccSide.Text))
        {
            lueAccSide.ErrorText = "کد تفصیلی را وارد نمایید";
            isValid = false;
        }

        var BondReasonIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "BondReason");
        if (BondReasonIsRequired && lueBondReason.EditValue.ToString() == "0" &&
            string.IsNullOrEmpty(lueBondReason.Text))
        {
            lueBondReason.ErrorText = "بابت را وارد نمایید";
            isValid = false;
        }

        var BondCategoryIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "BondCategory");
        if (BranchIsRequired && lueBondCategory.EditValue.ToString() == "0" &&
            string.IsNullOrEmpty(lueBondCategory.Text))
        {
            lueBondCategory.ErrorText = "نوع ضمانتنامه را وارد نمایید";
            isValid = false;
        }


        var BondSerialIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "BondSerial");
        if (BondSerialIsRequired && txtBondSerial.Visible && string.IsNullOrEmpty(txtBondSerial.Text))
        {
            txtBondSerial.ErrorText = "شماره ضمانتنامه را وارد نمایید";
            isValid = false;
        }

        var RegDateIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "RegDate");
        if (RegDateIsRequired && datRegDate.Visible && string.IsNullOrEmpty(datRegDate.Text))
        {
            datRegDate.ErrorText = "تاریخ صدور را وارد نمایید";
            isValid = false;
        }

        var DeliveryDateIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "DeliveryDate");
        if (DeliveryDateIsRequired && datDeliveryDate.Visible && string.IsNullOrEmpty(datDeliveryDate.Text))
        {
            datDeliveryDate.ErrorText = "تاریخ تحویل را وارد نمایید";
            isValid = false;
        }

        var StartDateIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "StartDate");
        if (StartDateIsRequired && datStartDate.Visible && string.IsNullOrEmpty(datStartDate.Text))
        {
            datStartDate.ErrorText = "تاریخ شروع را وارد نمایید";
            isValid = false;
        }

        var EndDateIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "EndDate");
        if (EndDateIsRequired && datEndDate.Visible && string.IsNullOrEmpty(datEndDate.Text))
        {
            datEndDate.ErrorText = "تاریخ سررسید را وارد نمایید";
            isValid = false;
        }

        var BondAmountIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "BondAmount");
        if (BondAmountIsRequired && txtBondAmount.Visible && string.IsNullOrEmpty(txtBondAmount.Text))
        {
            txtBondAmount.ErrorText = "مبلغ را وارد نمایید";
            isValid = false;
        }

        return isValid;
    }

    #region FormSetting

    private readonly int _FormID = 106001;
    private readonly FormSetting _bllFormSetting = new();
    private DataTable _dtFormSetting = new();

    #endregion
}