using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using LIB;

namespace Saman.UC;

public partial class UcEncSearch : XtraUserControl
{
    private DataRow _rowEncSearch;

    public UcEncSearch()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }

    public bool _EditMode { get; set; }
    public byte _EncType { get; set; }

    private void UcEncSearch_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        ActiveControl = panEncID;
        ActiveControl = txtEncID;

        txtFirstName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtLastName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtFatherName.Properties.Mask.EditMask = App.UI.MaskPersian;

        var appSDate = new SDate();
        datStartDate2.DateTime = datStartDate1.DateTime = DateTime.Parse(appSDate.ShamsiDate());
        timStartTime2.EditValue = "23:59";
        btnEncSearch_Click(null, null);
        ShortKey();
    }

    private void ShortKey()
    {
        msiSearch.Click += btnEncSearch_Click;
        msiEncSelect.Click += btnEncSelect_Click;

        panMain.Focus();
    }

    private void UcEncSearch_Paint(object sender, PaintEventArgs e)
    {
        UL.LangChange(2);
        UL.FillLookup(lueDep, "0", cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);
        UL.FillLookup(lueDr, "0", cSet.cnStringA, false, "*", "vDr", "Dr", null, null);
        UL.FillLookup(lueInsurance, "0", cSet.cnStringA, false, "InsuranceID,Name", "Insurance", "Insurance", null,
            null);
        UL.FillLookup(lueEncType, "0", cSet.cnStringA, false, "*", "vADREncType", "TypeAD", null, null);
        UL.FillLookup(lueEncStatus, "0", cSet.cnStringA, false, "ID,Name", "vADR", "EncStatus",
            "ADName='EncStatus'", null);

        ////LIB.UL.FillLookup(lueReportStatus, "0", true, "*", "vADRRegServiceStatus", "RegServiceStatus", null, null);
        ////LIB.UL.FillLookup(lueBillStatus, "0", true, "*", "vADRBillType", "BillStatus", null, null);
        //LIB.UL.FillLookup(lueDisStatus, "0", true, "ID,Name", "vADR", "DisStatus", "ADName='DisStatus'", null);
    }

    private void btnEncSearch_Click(object sender, EventArgs e)
    {
        PatientSearch();
    }

    private void PatientSearch()
    {
        var com = new SqlCommand();
        com.Parameters.AddRange(new[]
        {
            new SqlParameter("@EncID", txtEncID.Text),
            new SqlParameter("@PatientID", txtPatientID.Text),
            new SqlParameter("@NationalCode", txtNationalCode.Text),
            new SqlParameter("@FirstName", txtFirstName.Text),
            new SqlParameter("@LastName", txtLastName.Text),
            new SqlParameter("@FatherName", txtFatherName.Text),
            new SqlParameter("@DepID", lueDep.EditValue),
            new SqlParameter("@DrID", lueDr.EditValue),
            new SqlParameter("@Insurance", lueInsurance.EditValue),
            new SqlParameter("@EncType", lueEncType.EditValue),
            new SqlParameter("@EncStatus", lueEncStatus.EditValue),
            new SqlParameter("@InsuranceNumber", txtInsuranceNumber.Text),
            new SqlParameter("@StartDate1", datStartDate1.Text + " " + timStartTime1.Text),
            new SqlParameter("@StartDate2", datStartDate2.Text + " " + timStartTime2.Text)
        });

        var dalData = new Data();

        var col = grvSearch.Columns.ColumnByFieldName("RecNo");
        col.UnboundType = UnboundColumnType.Integer;
        grvSearch.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
        }

        gridSearch.DataSource = dalData.GetDataTableBySP(cSet.cnStringA, com, "spEncSearch");
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (_rowEncSearch == null)
            //gridSearch.Error = "لطفا نام را وارد نمایید";
            isValid = false;

        return isValid;
    }

    private void btnEncSelect_Click(object sender, EventArgs e)
    {
        _rowEncSearch = grvSearch.GetDataRow(grvSearch.FocusedRowHandle);

        if (!CheckValidation()) return;

        var panUc = Parent as PanelControl;

        var ucEncCur = new UcEncCur
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Dock = DockStyle.Fill,
            _EncID = Convert.ToInt32(_rowEncSearch["EncID"]),
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