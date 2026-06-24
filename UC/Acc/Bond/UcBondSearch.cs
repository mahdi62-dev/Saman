using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DAL;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using LIB;
using Saman.BLL.Acc.Bond;
using Saman.UC.Acc.Bond;

namespace Saman.UC;

public partial class UcBondSearch : XtraUserControl
{
    private readonly App.UI _appUI = new();
    private readonly Bond _bllBondList = new();
    private DataTable _dtBondList;
    private DataRow _rowbondSearch;


    public UcBondSearch()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }

    private void UcBondSearch_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        MaskSet();
        FillLookup();
        btnSearch_Click(null, null);
        ShortKey();
    }

    private void MaskSet()
    {
        txtBondSerial.Properties.Mask.EditMask = App.UI.MaskNum;
        txtBondId.Properties.Mask.EditMask = App.UI.MaskNum;
        txtContractId.Properties.Mask.EditMask = App.UI.MaskPersianWithNum;
        txtTenderId.Properties.Mask.EditMask = App.UI.MaskPersianWithNum;
        txtBankBranch.Properties.Mask.EditMask = App.UI.MaskPersianWithNum;
    }

    private void ShortKey()
    {
        msiSearch.Click += btnSearch_Click;
        msiEncSelect.Click += btnSearch_Click;

        panMain.Focus();
    }

    private void FillLookup()
    {
        UL.FillLookup(lueBranch, "0", cSet.cnStringERP, false, "*", "AccBranch", "AccBranch",
            null, null);
        UL.FillLookup(lueBondType, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "BondType",
            "ADName='BondType'", null);
        UL.FillLookup(lueAccSide, "0", cSet.cnStringERP, false, "SideId,SideName", "AccSide", "AccSide",
            null, null);
        UL.FillLookup(lueBondReason, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "BondReason",
            "ADName='BondReason'", null);
        UL.FillLookup(lueBondCategory, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "BondCategory",
            "ADName='BondCategory'", null);
        UL.FillLookup(lueBank, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "Bank",
            "ADName='Bank'", null);
        UL.FillLookup(lueBondStatus, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "BondStatus",
            "ADName='BondStatus'", null);
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        BondSearch();
    }

    private void BondSearch()
    {
        var col = grvSearch.Columns.ColumnByFieldName("RecNo");
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvSearch.CustomUnboundColumnData += grvSearch_CustomUnboundColumnData;

        _dtBondList = _bllBondList.BondSearch(
            5, txtBondId.Text, Convert.ToInt32(lueBranch.EditValue), Convert.ToInt32(lueBondType.EditValue),
            Convert.ToInt32(lueAccSide.EditValue), Convert.ToInt32(lueBondReason.EditValue),
            Convert.ToInt32(lueBondCategory.EditValue), txtBondSerial.Text,
            _appUI.SDateCheck(datStartDate.Text),
            _appUI.SDateCheck(datStartDate2.Text),
            _appUI.SDateCheck(datEndDate.Text),
            _appUI.SDateCheck(datEndDate2.Text), txtContractId.Text, txtTenderId.Text, txtSubject.Text,
            txtDescription.Text, Convert.ToInt32(lueBank.EditValue), txtBankBranch.Text,
            Convert.ToInt32(lueBondStatus.EditValue),
            txtSubId.Text);

        gridSearch.DataSource = _dtBondList;
    }

    private void grvSearch_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }

    private void gridSearch_DoubleClick(object sender, EventArgs e)
    {
        _rowbondSearch = grvSearch.GetDataRow(grvSearch.FocusedRowHandle);

        var panUc = Parent as PanelControl;

        var ucEncCur = new UcBond
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Dock = DockStyle.Fill,
            _BondID = Convert.ToInt32(_rowbondSearch["BondID"]),
            _ModID = _ModID
        };

        panUc.Controls.Clear();
        panUc.Controls.Add(ucEncCur);
    }

    private void lblPrint_Click(object sender, EventArgs e)
    {
        grvSearch.BestFitColumns();
        gridSearch.ShowRibbonPrintPreview();
    }

    private void grvSearch_PrintInitialize(object sender, PrintInitializeEventArgs e)
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

        grvSearch.OptionsPrint.PrintHeader = true;
        grvSearch.OptionsPrint.PrintFooter = true;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.LineAlignment = BrickAlignment.Center;
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.LineAlignment = BrickAlignment.Center;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Font =
            new Font("IRANSansFaNum", 10, FontStyle.Regular);
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Font = new Font("IRANSansFaNum", 8);

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.AddRange(new[] { gridSearch.Tag.ToString() });
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.AddRange(new[]
        {
            "[Date Printed]-[Time Printed] : تاریخ گزارش گیری  ", " [Pages #] از [Page #] : صفحه  ",
            "گزارش گیرنده : " + GL.UserPersName
        });
    }
}