using System;
using DevExpress.XtraEditors;
using Saman.BLL.Payroll;

namespace Saman.UC;

public partial class UcTimexCalc : XtraUserControl
{
    private readonly Timex _bllTimex = new();


    public UcTimexCalc()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }
    public int _Year { get; set; }
    public int _Mon { get; set; }

    public int _IsSign { get; set; }
    public int _PersID { get; set; }

    private void UcTimexCalc_Load(object sender, EventArgs e)
    {
        grdTimex.DataSource = _bllTimex.TimexGetData(1, _Year, _Mon, GL.UserPersID);
        ShortKey();
    }

    private void ShortKey()
    {
        msiTimexGetData.Click += btnTimexGetData_Click;
        msiClose.Click += btnCancel_Click;

        panMain.Focus();
    }

    private void FormNull()
    {
        //datDayOffStart.Text = null;
        //datDayOffEnd.Text = null;
        //luePers.EditValue = 0;
    }

    private void btnTimexGetData_Click(object sender, EventArgs e)
    {
        prgLoading.Visible = true;

        grdTimex.DataSource = _bllTimex.TimexGetData(0, _Year, _Mon, GL.UserPersID);

        prgLoading.Visible = false;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
    }

    private void lblPrint_Click(object sender, EventArgs e)
    {
        grvTimex.BestFitColumns();
        grdTimex.ShowRibbonPrintPreview();
    }
}