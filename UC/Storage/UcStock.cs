using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DAL;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using LIB;
using Saman.BLL.Dep;
using Saman.BLL.SAC;

namespace Saman.UC;

public partial class UcStock : XtraUserControl
{
    private readonly Dep _bllDep = new();

    private readonly Permission _bllSACPermission = new();

    private readonly BLL.Storage.Storage _bllStorage = new();

    private readonly DataRowView _rowCurServie;

    private readonly DataRow[] _rowEncCur;

    private DataTable _dtDep = new();

    private DataTable _dtStorageStock = new();

    public UcStock()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public int _StorageID { get; set; }
    public int _StoragePeriodID { get; set; }

    public byte _EncType { get; set; }
    public int _PageID { get; set; }

    private void UcStock_Load(object sender, EventArgs e)
    {
        _dtDep = _bllDep.DepDataLoad(_DepID);
        _StorageID = Convert.ToInt32(_dtDep.Rows[0]["StorageID"]);
        _StoragePeriodID = Convert.ToInt32(_dtDep.Rows[0]["StoragePeriodID"]);

        switch (_ModID)
        {
            case 4:
                UL.FillLookup(lueStorage, "0", cSet.cnStringERP, false, "StorageID,Name", "InvStorage",
                    "InvStorage", null, null);
                break;
            default:
                UL.FillLookup(lueStorage, _StorageID.ToString(), cSet.cnStringERP, false, "StorageID,Name",
                    "InvStorage", "InvStorage", "StorageID=" + _StorageID, null);
                break;
        }

        StorageStockLoad(_StorageID, _StoragePeriodID);
    }

    private void lueStorage_EditValueChanged(object sender, EventArgs e)
    {
        _StorageID = Convert.ToInt32(lueStorage.EditValue);
        _StoragePeriodID = _bllStorage.StoragePeriodIDLoad(_StorageID);
        FormNull();
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (lueStorage.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueStorage.Text))
        {
            lueStorage.ErrorText = "لطفا انبار را انتخاب نمایید";

            isValid = false;
        }

        return isValid;
    }


    private void btnCancel_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucEncCur = new UcEncCur
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        ucEncCur._ModID = _ModID;
        panUC.Controls.Add(ucEncCur);
    }

    private void StorageStockLoad(object StorageID, object StoragePeriodID)
    {
        var bllStorageStockLoad = new BLL.Storage.Storage();
        _dtStorageStock = bllStorageStockLoad.StorageStockLoad(_StorageID, _StoragePeriodID);

        if (_dtStorageStock.Rows.Count > 0)
        {
            var col = grvStockLoad.Columns.ColumnByFieldName("RecNo");
            var col1 = grvStockLoad.Columns.ColumnByFieldName("Sum1");
            var col2 = grvStockLoad.Columns.ColumnByFieldName("Sum2");

            // col.FieldName = "RecNo";
            col.Visible = true;
            col1.Visible = true;
            col2.Visible = true;

            col.UnboundType = UnboundColumnType.Integer;
            col1.UnboundType = UnboundColumnType.Integer;
            col2.UnboundType = UnboundColumnType.Integer;

            grvStockLoad.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

            void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
            {
                if (e.Column.FieldName == "RecNo")
                    if (e.IsGetData)
                        e.Value = e.ListSourceRowIndex + 1;

                if (e.Column.FieldName == "Sum1")
                    if (e.IsGetData)
                        e.Value = getTotalValue1(grvStockLoad, e.ListSourceRowIndex);

                if (e.Column.FieldName == "Sum2")
                    if (e.IsGetData)
                        e.Value = getTotalValue2(grvStockLoad, e.ListSourceRowIndex);
            }

            decimal getTotalValue1(GridView view, int listSourceRowIndex)
            {
                var Num = Convert.ToDecimal(view.GetListSourceRowCellValue(listSourceRowIndex, "Num"));
                var BuyAmountLast =
                    Convert.ToDecimal(view.GetListSourceRowCellValue(listSourceRowIndex, "BuyAmountLast"));
                return Num * BuyAmountLast;
            }

            decimal getTotalValue2(GridView view, int listSourceRowIndex)
            {
                var Num = Convert.ToDecimal(view.GetListSourceRowCellValue(listSourceRowIndex, "Num"));
                var BuyAmountLast = Convert.ToDecimal(view.GetListSourceRowCellValue(listSourceRowIndex, "Price"));
                return Num * BuyAmountLast;
            }

            grdStockLoad.DataSource = _dtStorageStock;
        }
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        CheckValidation();
        StorageStockLoad(_StorageID, _StoragePeriodID);
    }


    private void FormNull()
    {
        grdStockLoad.DataSource = null;

        ActiveControl = lueStorage;
    }

    private void grvStockLoad_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        var view = sender as GridView;

        var StockNum = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "StockNum"));
        if (StockNum <= 0) e.Appearance.BackColor = Color.PaleVioletRed;
    }

    private void lblPrint_Click(object sender, EventArgs e)
    {
        grvStockLoad.BestFitColumns();
        grdStockLoad.ShowRibbonPrintPreview();
    }

    private void grvStockLoad_PrintInitialize(object sender, PrintInitializeEventArgs e)
    {
        var pb = e.PrintingSystem as PrintingSystemBase;
        var printableComponentLink = new PrintableComponentLink();
        pb.PageSettings.Landscape = true;
        pb.PageSettings.PaperKind = PaperKind.A4;
        pb.Document.AutoFitToPagesWidth = 1;
        pb.PageSettings.TopMargin = 100;
        pb.PageSettings.BottomMargin = 100;
        pb.PageSettings.LeftMargin = 40;
        pb.PageSettings.RightMargin = 40;

        grvStockLoad.OptionsPrint.PrintHeader = true;
        grvStockLoad.OptionsPrint.PrintFooter = true;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.LineAlignment = BrickAlignment.Center;
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.LineAlignment = BrickAlignment.Center;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Font =
            new Font("IRANSansFaNum", 10, FontStyle.Regular);
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Font = new Font("IRANSansFaNum", 8);

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.AddRange(new[]
            { grdStockLoad.Tag.ToString() });
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.AddRange(new[]
        {
            "[Date Printed]-[Time Printed] : تاریخ گزارش گیری  ", " [Pages #] از [Page #] : صفحه  ",
            "گزارش گیرنده : " + GL.UserPersName
        });
    }
}