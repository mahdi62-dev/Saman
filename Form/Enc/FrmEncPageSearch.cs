using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using LIB;
using Saman.BLL.Enc;

namespace Saman.Form.Enc;

public partial class FrmEncPageSearch : XtraForm
{
    private readonly EncPage _bllPage = new();

    private DataTable _dtPage;
    public byte _SearchType = 0;


    public FrmEncPageSearch()
    {
        InitializeComponent();
    }

    public int _PageID { get; set; }
    public int _PageCode { get; set; }
    public int _PageDepID { get; set; }

    private void FrmEncPageSearch_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        ActiveControl = txtPageFirstName;

        SetMask();

        if (_SearchType == 0) tabEnc.PageVisible = false;

        if (_SearchType == 2) tabPage.PageVisible = false;

        ShortKey();
    }

    private void ShortKey()
    {
        msiRefresh.Click += lblRefreshPage_Click;
    }

    private void SetMask()
    {
        txtPageFirstName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtPageLastName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtPageFatherName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtPageMotherName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtPageMotherFamily.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtEncPageID.Properties.Mask.EditMask = App.UI.MaskNum;
        txtPageNationalCode.Properties.Mask.EditMask = App.UI.MaskNum;
        txtPatientPageID.Properties.Mask.EditMask = App.UI.MaskNum;
        txtPageCode.Properties.Mask.EditMask = App.UI.MaskNum;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
        Close();
    }

    private void grvPage_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }

    private void lblRefreshPage_Click(object sender, EventArgs e)
    {
        _dtPage = _bllPage.PageSearch(txtPageFirstName.Text, txtPageLastName.Text, txtPageFatherName.Text,
            txtPageMotherName.Text, txtPageMotherFamily.Text,
            txtEncPageID.Text, txtPageNationalCode.Text, txtPatientPageID.Text, txtPageCode.Text,
            luePageInsurance.EditValue, lueEncPageType.EditValue,
            lueEncPageStatus.EditValue, lueDepIDPage.EditValue, lueDrIDPage.EditValue);

        var col = grvPage.Columns.ColumnByFieldName("RecNo");
        col.UnboundType = UnboundColumnType.Integer;
        grvPage.CustomUnboundColumnData += grvPage_CustomUnboundColumnData;

        grdPage.DataSource = _dtPage;
        var rowSelectgrv = grvPage.GetFocusedDataSourceRowIndex();
        grvPage.SelectRow(rowSelectgrv);
        grvPage.FocusedRowHandle = rowSelectgrv;
    }

    private void grvPage_DoubleClick(object sender, EventArgs e)
    {
        PageSelect();

        DialogResult = DialogResult.Yes;
        Close();
    }

    private void PageSelect()
    {
        if (grvPage.FocusedRowHandle < 0) return;

        DataRow rowSelect;

        rowSelect = grvPage.GetDataRow(grvPage.FocusedRowHandle);

        _PageID = Convert.ToInt32(rowSelect["PageID"]);
        _PageCode = Convert.ToInt32(rowSelect["PageCode"]);
        _PageDepID = Convert.ToInt32(rowSelect["DepID"]);
    }


    //private void btnPatientSelect_Click(object sender, EventArgs e)
    //{
    //    var row = grvSearch.GetFocusedDataRow();
    //    _PatientID = Convert.ToInt32(row["PatientID"]);
    //    this.DialogResult = DialogResult.Yes;
    //    this.Close();

    //}
}