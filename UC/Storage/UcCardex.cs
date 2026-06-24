using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using LIB;
using Saman.BLL.Dep;
using Saman.BLL.SAC;

namespace Saman.UC;

public partial class UcCardex : XtraUserControl
{
    private readonly Dep _bllDep = new();

    private readonly Permission _bllSACPermission = new();

    private readonly BLL.Storage.Storage _bllStorage = new();

    private readonly DataRowView _rowCurServie;

    private readonly DataRow[] _rowEncCur;

    private DataTable _dtCardex = new();

    private DataTable _dtDep = new();

    public UcCardex()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public int _StorageID { get; set; }
    public int _ServiceCode { get; set; }

    public byte _EncType { get; set; }
    public int _PageID { get; set; }

    public string _Date1 { get; set; }
    public string _Date2 { get; set; }

    private void UcCardex_Load(object sender, EventArgs e)
    {
        _dtDep = _bllDep.DepDataLoad(_DepID);
        _StorageID = Convert.ToInt32(_dtDep.Rows[0]["StorageID"]);

        UL.FillLookup(lueService, null, cSet.cnStringA, false, "*", "vServiceDrug", "vServiceDrug",
            "EndDate IS NULL", "CAST(ServiceCode as INT)");

        UL.FillLookup(lueStorage, "0", cSet.cnStringERP, false, "StorageID,Name", "InvStorage",
            "InvStorage", null, null);


        var appSDate = new SDate();
        datDate2.DateTime = datDate1.DateTime = DateTime.Parse(appSDate.ShamsiDate());

        FormNull();
    }

    private void lueStorage_EditValueChanged(object sender, EventArgs e)
    {
        _StorageID = Convert.ToInt32(lueStorage.EditValue);
    }

    private void lueService_EditValueChanged(object sender, EventArgs e)
    {
        _ServiceCode = Convert.ToInt32(lueService.EditValue);
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

        if (string.IsNullOrEmpty(lueService.Text))
        {
            lueService.ErrorText = "لطفا کالا را انتخاب نمایید";

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

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        CheckValidation();

        _Date1 = datDate1.Text + " 00:00";
        _Date2 = datDate2.Text + " 23:59";


        var bllCardexLoad = new BLL.Storage.Storage();
        _dtCardex = bllCardexLoad.CardexLoad(_StorageID, _ServiceCode, _Date1, _Date2);

        grdStockLoad.DataSource = _dtCardex;
    }


    private void FormNull()
    {
        grdStockLoad.DataSource = null;
        ActiveControl = lueStorage;
    }

    private void grvStockLoad_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        var view = sender as GridView;

        //int StockNum = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "StockNum"));
        //if (StockNum <= 0)
        //{
        //    e.Appearance.BackColor = Color.PaleVioletRed;
        //}
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