using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Saman.UC;

public partial class UcPayrollItem : XtraUserControl
{
    private readonly BLL.Payroll.Payroll _bllPayrollItemLoad = new();

    private readonly DataTable _dtDayOff = new();
    private readonly BLL.Payroll.Payroll _PayrollItemSave = new();
    private readonly BLL.Payroll.Payroll bllPayroll = new();
    private DataTable _dtPayrollItem;

    private int _ItemID;
    private DataRow _rowPersSelect;

    public UcPayrollItem()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }
    public int _Year { get; set; }
    public int _Mon { get; set; }
    public int _IsSign { get; set; }
    public int _PersID { get; set; }

    private void UcPayrollItem_Load(object sender, EventArgs e)
    {
        PersLoad();

        lblTitle1.Text = "سال و ماه جاری" + "   " + _Year + "/" + "0" + _Mon;
        _ItemID = Convert.ToInt32(tabPayrollItem.SelectedTabPage.Tag);

        if (_IsSign == 1) btnPayrollItemSave.Enabled = false;

        ShortKey();
    }

    private void ShortKey()
    {
        msiSave.Click += btnPayrollItemSave_Click;
        msiClose.Click += btnCancel_Click;

        panMain.Focus();
    }

    private void tabPayrollItem_Click(object sender, EventArgs e)
    {
        _ItemID = Convert.ToInt32(tabPayrollItem.SelectedTabPage.Tag);
        PersLoad();
    }

    private void PersLoad()
    {
        if (_ItemID == 0) _ItemID = 100;

        var col = grvPayrollItem.Columns.ColumnByFieldName("RecNo");
        col.UnboundType = UnboundColumnType.Integer;
        grvPayrollItem.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        void gridView1_CustomUnboundColumnData(object sender,
            CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
        }

        _dtPayrollItem = _bllPayrollItemLoad.GetPayrollItem(_Year, _Mon, _ItemID);

        if (_ItemID == 97)
            grvPayrollItem.Columns[5].Caption = "ساعت";
        else
            grvPayrollItem.Columns[5].Caption = "مبلغ(ریال)";

        grdPayrollItem.DataSource = _dtPayrollItem;
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        //App.Control.TextEditLine(lueDayOffType, e);
        //App.Control.TextEditLine(luePers, e);
        //App.Control.TextEditLine(datDayOffEnd, e);
        //App.Control.TextEditLine(datDayOffStart, e);
    }

    private void FormNull()
    {
        //datDayOffStart.Text = null;
        //datDayOffEnd.Text = null;
        //luePers.EditValue = 0;
    }

    private void btnPayrollItemSave_Click(object sender, EventArgs e)
    {
        PayrollItemSave();
    }

    private void grdPayrollItemLoad_Click(object sender, EventArgs e)
    {
        _rowPersSelect = grvPayrollItem.GetDataRow(grvPayrollItem.FocusedRowHandle);
        if (_rowPersSelect == null) return;

        lblTitle2.Text = _rowPersSelect["FirstName"] + " " + _rowPersSelect["LastName"];
    }

    private void grdPayrollItemLoad_KeyUp(object sender, KeyEventArgs e)
    {
        _rowPersSelect = grvPayrollItem.GetDataRow(grvPayrollItem.FocusedRowHandle);
        if (_rowPersSelect == null) return;

        lblTitle2.Text = _rowPersSelect["FirstName"] + " " + _rowPersSelect["LastName"];
    }

    private void grdPayrollItemLoad_EditorKeyDown(object sender, KeyEventArgs e)
    {
        _rowPersSelect = grvPayrollItem.GetDataRow(grvPayrollItem.FocusedRowHandle);
        if (_rowPersSelect == null) return;

        lblTitle2.Text = _rowPersSelect["FirstName"] + " " + _rowPersSelect["LastName"];
    }

    private void PayrollItemSave()
    {
        var Year = 0;
        var Mon = 0;
        var PersID = 0;
        var ItemVal = 0;
        var ItemID = 0;

        for (var i = 0; i < grvPayrollItem.RowCount; i++)
        {
            Year = _Year;
            Mon = _Mon;
            PersID = Convert.ToInt32(grvPayrollItem.GetRowCellValue(i, "PersID"));
            ItemID = _ItemID;
            ItemVal = Convert.ToInt32(grvPayrollItem.GetRowCellValue(i, "ItemVal"));

            //  _PayrollItemSave.PayrollItemDel(_EmpActionID, ItemID);

            //  if (Convert.ToDouble(ItemVal) + Convert.ToDouble(ItemK) > 0)
            _PayrollItemSave.PayrollItemSave(_Year, _Mon, PersID, ItemID, ItemVal, GL.UserPersID);
        }
    }

    private void grvPayrollItem_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
    {
        if (_ItemID == 500 && e.Column.FieldName == "ItemVal")
            e.RepositoryItem = repChkHas;
        else
            e.RepositoryItem = RepTxtAmount;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
    }
}