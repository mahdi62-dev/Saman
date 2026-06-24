using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using LIB;
using Saman.BLL.Acc.AccSide;
using Saman.BLL.FormSetting;
using Control = System.Windows.Forms.Control;

namespace Saman.UC.Acc.Bond;

public partial class UcAccSide : XtraUserControl
{
    private readonly AccSide _bllAccSide = new();
    private readonly AccSide _bllAccSideList = new();
    public int _AccSideID;
    public DataTable _dtAccSide;
    private DataTable _dtAccSideList;
    private DataRow _rowAccSide;

    public UcAccSide()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }

    private void UcAccSide_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        ActiveControl = flpMain;
        ActiveControl = lueSideType;
        MaskSet();
        FillLookup();
        FormSetting();
        AccSideSearch();
    }


    private void MaskSet()
    {
        txtSideName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtNationalId.Properties.Mask.EditMask = App.UI.MaskNum;
        txtEconomicCode.Properties.Mask.EditMask = App.UI.MaskNum;
        txtCompanyRegId.Properties.Mask.EditMask = App.UI.MaskNum;
        txtTel.Properties.Mask.EditMask = App.UI.MaskNum;
        txtMobile.Properties.Mask.EditMask = App.UI.MaskNum;
        txtPostalCode.Properties.Mask.EditMask = App.UI.MaskNum;
    }

    private void FillLookup()
    {
        UL.FillLookup(lueSideType, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "SideType",
            "ADName='SideType'", null);
        UL.FillLookup(lueCurrencyType, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "CurrencyType",
            "ADName='CurrencyType'", null);
    }

    private void AccSideSearch()
    {
        var col = grvAccSide.Columns.ColumnByFieldName("RecNo");
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvAccSide.CustomUnboundColumnData += grvAccSide_CustomUnboundColumnData;

        _dtAccSideList = _bllAccSideList.AccSideSearch(1);

        gridAccSide.DataSource = _dtAccSideList;
    }

    private void grvAccSide_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }

    private void grvAccSide_RowClick(object sender, RowClickEventArgs e)
    {
    }

    private void gridAccSide_Click(object sender, EventArgs e)
    {
        if (grvAccSide.FocusedRowHandle < 0) return;

        _rowAccSide = grvAccSide.GetDataRow(grvAccSide.FocusedRowHandle);

        _AccSideID = Convert.ToInt32(_rowAccSide["SideId"].ToString());
        txtAccSideID.Text = _AccSideID.ToString();
        lueSideType.EditValue = Convert.ToInt32(_rowAccSide["SideType"].ToString());
        txtSideName.Text = _rowAccSide["SideName"].ToString();
        txtNationalId.Text = _rowAccSide["NationalId"].ToString();
        txtEconomicCode.Text = _rowAccSide["EconomicCode"].ToString();
        txtCompanyRegId.Text = _rowAccSide["CompanyRegId"].ToString();
        lueCurrencyType.EditValue = Convert.ToInt32(_rowAccSide["CurrencyType"].ToString());
        txtTel.Text = _rowAccSide["Tel"].ToString();
        txtMobile.Text = _rowAccSide["Mobile"].ToString();
        txtAddress.Text = _rowAccSide["Address"].ToString();
        txtPostalCode.Text = _rowAccSide["PostalCode"].ToString();
        txtEmail.Text = _rowAccSide["Email"].ToString();
        txtWebSite.Text = _rowAccSide["WebSite"].ToString();
    }

    private void lblSaveAccSide_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        AccSideSave();
        FormNull();
    }

    private void AccSideSave()
    {
        var CRUD = 0;

        if (_AccSideID > 0)
            CRUD = 2;

        _dtAccSide = _bllAccSide.AccSideSave(CRUD, Convert.ToInt32(lueSideType.EditValue),
            txtSideName.Text, txtNationalId.Text, txtEconomicCode.Text, txtCompanyRegId.Text,
            txtTel.Text, txtMobile.Text, txtAddress.Text, txtPostalCode.Text, txtEmail.Text,
            txtWebSite.Text, Convert.ToInt32(lueCurrencyType.EditValue), GL.UserPersID);

        _dtAccSideList = _bllAccSideList.AccSideSearch(1);
        gridAccSide.DataSource = _dtAccSideList;
    }

    private void FormNull()
    {
        lueSideType.EditValue = 0;
        _AccSideID = 0;
        txtAccSideID.Text = string.Empty;
        txtSideName.Text = string.Empty;
        txtNationalId.Text = string.Empty;
        txtEconomicCode.Text = string.Empty;
        txtCompanyRegId.Text = string.Empty;
        txtTel.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtPostalCode.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtWebSite.Text = string.Empty;
        lueCurrencyType.EditValue = 0;
    }

    private void lblNewAccSide_Click(object sender, EventArgs e)
    {
        FormNull();
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

        var SideTypeIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "SideType");
        if (SideTypeIsRequired && lueSideType.EditValue.ToString() == "0" &&
            string.IsNullOrEmpty(lueSideType.Text))
        {
            lueSideType.ErrorText = "نوع شخص را وارد نمایید";
            isValid = false;
        }

        var SideNameIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "SideName");
        if (SideNameIsRequired && txtSideName.Visible && string.IsNullOrEmpty(txtSideName.Text))
        {
            txtSideName.ErrorText = "نام را وارد نمایید";
            isValid = false;
        }

        var NationalIdIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "NationalId");
        if (NationalIdIsRequired && txtNationalId.Visible && string.IsNullOrEmpty(txtNationalId.Text))
        {
            txtNationalId.ErrorText = "کد ملی را وارد نمایید";
            isValid = false;
        }


        var EconomicCodeIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "EconomicCode");
        if (EconomicCodeIsRequired && txtEconomicCode.Visible && string.IsNullOrEmpty(txtEconomicCode.Text))
        {
            txtEconomicCode.ErrorText = "کد اقتصادی را وارد نمایید";
            isValid = false;
        }

        var CompanyRegIdIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "CompanyRegId");
        if (CompanyRegIdIsRequired && txtCompanyRegId.Visible && string.IsNullOrEmpty(txtCompanyRegId.Text))
        {
            txtCompanyRegId.ErrorText = "شماره ثبت را وارد نمایید";
            isValid = false;
        }

        var CurrencyTypeIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "CurrencyType");
        if (CurrencyTypeIsRequired && lueCurrencyType.EditValue.ToString() == "0" &&
            string.IsNullOrEmpty(lueCurrencyType.Text))
        {
            lueCurrencyType.ErrorText = "نوع ارز را وارد نمایید";
            isValid = false;
        }


        var TelIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "Tel");
        if (TelIsRequired && txtTel.Visible && string.IsNullOrEmpty(txtTel.Text))
        {
            txtTel.ErrorText = "تلفن را وارد نمایید";
            isValid = false;
        }

        return isValid;
    }

    #region FormSetting

    private readonly int _FormID = 106002;
    private readonly FormSetting _bllFormSetting = new();
    private DataTable _dtFormSetting = new();

    #endregion
}