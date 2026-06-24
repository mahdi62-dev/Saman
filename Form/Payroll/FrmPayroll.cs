using System;
using System.Data;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using LIB;
using Saman.APP.SAC;
using Saman.UC;
using Saman.UC.Payroll;

namespace Saman.Form.Payroll;

public partial class FrmPayroll : XtraForm
{
    private readonly string _FormName = "حقوق و دستمزد";

    private readonly SACPermission sacPermission = new();
    private DataTable _dtRegLoad = new();
    public byte _ModID = 101;

    public FrmPayroll()
    {
        InitializeComponent();
    }

    public int _Year { get; set; }
    public int _Mon { get; set; }
    public int _IsSign { get; set; }

    private void FrmPayroll_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();

        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);

        var _appSDate = new SDate();
        var curDateTime = _appSDate.ShamsiDate();
        string year, mon;

        year = curDateTime.Substring(0, 4);
        mon = curDateTime.Substring(5, 2);
        if (Convert.ToInt32(mon) > 1) mon = (Convert.ToInt32(mon) - 1).ToString();

        UL.FillLookupBar(BarLueYear, lueYear, year, cSet.cnStringA, true, "ID,Name", "vADR", "Year",
            "ADName='Year'", null);
        UL.FillLookupBar(BarLueMonth, lueMonth, mon, cSet.cnStringA, true, "ID,Name", "vADR", "mon",
            "ADName='Month'", null);

        _Year = Convert.ToInt32(lueYear.EditValue);
        _Mon = Convert.ToInt32(lueMonth.EditValue);


        var bllPayroll = new BLL.Payroll.Payroll();
        _dtRegLoad = bllPayroll.SalaryRegLoad(_Year, _Mon);

        if (_dtRegLoad.Rows.Count == 0)
        {
            _IsSign = 0;
            return;
        }

        _IsSign = Convert.ToInt32(_dtRegLoad.Rows[0]["IsSign"]);
    }

    private void CheckPermission()
    {
        lueYear.Enabled = sacPermission.CheckPermission(_ModID, 1010001);
        lueMonth.Enabled = sacPermission.CheckPermission(_ModID, 1010002);
        barbtnTimex.Enabled = sacPermission.CheckPermission(_ModID, 1010003);
        barBtnDayOff.Enabled = sacPermission.CheckPermission(_ModID, 1010004);
        barBtnSalaryItem.Enabled = sacPermission.CheckPermission(_ModID, 1010005);
        barBtnSalaryCalc.Enabled = sacPermission.CheckPermission(_ModID, 1010006);
    }

    private void barBtnStatus()
    {
        lueYear.Enabled = false;
        lueMonth.Enabled = false;
        barbtnTimex.Enabled = false;
        barBtnDayOff.Enabled = false;
        barBtnSalaryItem.Enabled = false;
        barBtnSalaryCalc.Enabled = false;
    }

    private void lueYear_EditValueChanged(object sender, EventArgs e)
    {
        _Year = Convert.ToInt32(lueYear.EditValue);
        _Mon = Convert.ToInt32(lueMonth.EditValue);

        panUC.Controls.Clear();
        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);

        var bllPayroll = new BLL.Payroll.Payroll();
        _dtRegLoad = bllPayroll.SalaryRegLoad(_Year, _Mon);

        if (_dtRegLoad.Rows.Count == 0)
        {
            _IsSign = 0;
            return;
        }

        _IsSign = Convert.ToInt32(_dtRegLoad.Rows[0]["IsSign"]);
    }

    private void lueMonth_EditValueChanged(object sender, EventArgs e)
    {
        _Year = Convert.ToInt32(lueYear.EditValue);
        _Mon = Convert.ToInt32(lueMonth.EditValue);

        panUC.Controls.Clear();
        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);

        var bllPayroll = new BLL.Payroll.Payroll();
        _dtRegLoad = bllPayroll.SalaryRegLoad(_Year, _Mon);

        if (_dtRegLoad.Rows.Count == 0)
        {
            _IsSign = 0;
            return;
        }

        _IsSign = Convert.ToInt32(_dtRegLoad.Rows[0]["IsSign"]);
    }

    private void BarBtnCheckPermission()
    {
        if (sacPermission.CheckPermission(1, 1001)) ;
        //  barBtnEncNewOP.Visibility = BarItemVisibility.Always;
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (lueMonth.EditValue.ToString() == "0" || lueYear.EditValue.ToString() == "0") isValid = false;

        return isValid;
    }


    private void ShowUC(object ucControl)
    {
        panUC.Controls.Clear();
        var myUC = (UserControl)ucControl;
        myUC.Anchor = AnchorStyles.Top;
        myUC.Anchor = AnchorStyles.Right;
        myUC.Dock = DockStyle.Fill;

        Text = _FormName + " - " + myUC.Tag;
        panUC.Controls.Add(myUC);
    }

    private void barBtnSalaryItem_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (!CheckValidation()) return;

        var ucControl = new UcPayrollItem { _ModID = _ModID, _Year = _Year, _Mon = _Mon, _IsSign = _IsSign };
        ShowUC(ucControl);
    }

    private void barBtnDayOff_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (!CheckValidation()) return;

        var ucControl = new UcTimexDayOff { _ModID = _ModID, _Year = _Year, _Mon = _Mon, _IsSign = _IsSign };
        ShowUC(ucControl);
    }

    private void barRegService_ItemClick(object sender, ItemClickEventArgs e)
    {
    }


    private void barBtnSalaryCalc_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (!CheckValidation()) return;

        var ucControl = new UcSalaryCalc { _ModID = _ModID, _Year = _Year, _Mon = _Mon, _IsSign = _IsSign };
        ShowUC(ucControl);
    }

    private void barbtnTimex_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (!CheckValidation()) return;

        var ucControl = new UcTimexCalc { _ModID = _ModID, _Year = _Year, _Mon = _Mon, _IsSign = _IsSign };
        ShowUC(ucControl);
    }
}