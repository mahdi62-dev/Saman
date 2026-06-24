using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DAL;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using LIB;
using Control = App.Control;

namespace Saman.UC;

public partial class UcCashReceiptSearch : XtraUserControl
{
    public UcCashReceiptSearch()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }

    private void UcCashOtherSearch_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        ActiveControl = txtEncID;

        UL.FillLookup(luePayType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "PayType", "ADName='PayType'",
            null);
        UL.FillLookup(lueBank, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Bank",
            "ADName='Bank' and IsDisable=0", null);
        UL.FillLookup(lueTransType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "TransType",
            "ADName='TransType'", null);
        UL.FillLookup(luePayGroup, "0", cSet.cnStringA, false, "ID,Name", "vADR", "PayGroup", "ADName='PayGroup'",
            null);
        UL.FillLookup(lueAbout, "0", cSet.cnStringA, false, "ID,Name", "vADR", "CashAboutType",
            "ADName='CashAboutType'", null);
        UL.FillLookup(lueDr, "0", cSet.cnStringA, false, "*", "vDr", "Dr", null, null);

        ShortKey();
    }

    private void ShortKey()
    {
        msiSearch.Click += btnCashSearch_Click;
        msiClose.Click += btnCancel_Click;

        panMain.Focus();
    }

    private void btnCashSearch_Click(object sender, EventArgs e)
    {
        CashSearch();
    }

    private void CashSearch()
    {
        var com = new SqlCommand();
        com.Parameters.AddRange(new[]
        {
            new SqlParameter("@EncID", txtEncID.Text),
            new SqlParameter("@CashID", txtCashID.Text),
            new SqlParameter("@CashDate", txtCashDate.Text + ' ' + txtCashTime.Text),
            new SqlParameter("@PayGroup", luePayGroup.EditValue),
            new SqlParameter("@DrID", lueDr.EditValue),
            new SqlParameter("@PayType", luePayType.EditValue),
            new SqlParameter("@TransType", lueTransType.EditValue),
            new SqlParameter("@About", lueAbout.EditValue),
            new SqlParameter("@Bank", lueBank.EditValue)
        });

        var dalData = new Data();
        grdEncCashData.DataSource = dalData.GetDataTableBySP(cSet.cnStringA, com, "spReceiptCashSearch");

        var col = grvEncCashData.Columns.ColumnByFieldName("RecNo");
        col.UnboundType = UnboundColumnType.Integer;
        grvEncCashData.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
        }
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.LookupEditLine(luePayType, e);
        Control.LookupEditLine(lueBank, e);
        Control.LookupEditLine(lueTransType, e);
        Control.LookupEditLine(luePayGroup, e);
        Control.LookupEditLine(lueDr, e);
        Control.LookupEditLine(lueAbout, e);
        Control.TextEditLine(txtCashID, e);
        Control.TextEditLine(txtCashDate, e);
        Control.TextEditLine(txtCashTime, e);
    }

    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtEncID, e);
    }


    private void btnCancel_Click(object sender, EventArgs e)
    {
    }

    private void lblPrint_Click(object sender, EventArgs e)
    {
        grvEncCashData.BestFitColumns();
        grdEncCashData.ShowRibbonPrintPreview();
    }

    private void grvEncCashData_PrintInitialize(object sender, PrintInitializeEventArgs e)
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

        grvEncCashData.OptionsPrint.PrintHeader = true;
        grvEncCashData.OptionsPrint.PrintFooter = true;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.LineAlignment = BrickAlignment.Center;
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.LineAlignment = BrickAlignment.Center;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Font =
            new Font("IRANSansFaNum", 10, FontStyle.Regular);
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Font = new Font("IRANSansFaNum", 8);

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.AddRange(new[]
            { grdEncCashData.Tag.ToString() });
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.AddRange(new[]
        {
            "[Date Printed]-[Time Printed] : تاریخ گزارش گیری  ", " [Pages #] از [Page #] : صفحه  ",
            "گزارش گیرنده : " + GL.UserPersName
        });
    }
}