using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using DAL;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Saman.APP.Stimul;
using Stimulsoft.Report.Dictionary;

namespace Saman.UC.Payroll;

public partial class UcSalaryCalc : XtraUserControl
{
    private readonly Stimul _appStimul = new();

    private readonly BLL.Payroll.Payroll _bllPayrollSummarySave = new();


    private readonly BLL.Report.Report _bllReport = new();

    private readonly BLL.Payroll.Payroll _bllSalaryRegSign = new();


    private readonly SUI sui = new();

    private DataRow _rowPersSelect;

    public UcSalaryCalc()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }

    public int _IsSign { get; set; }

    public int _PersID { get; set; }

    public int _Year { get; set; }
    public int _Mon { get; set; }

    private void UcSalaryCalc_Load(object sender, EventArgs e)
    {
        LoadData();

        if (_IsSign == 1)
        {
            btnPayrollCalc.Enabled = false;
            btnSignPayroll.Enabled = false;
        }


        ShortKey();
    }

    private void ShortKey()
    {
        msiSave.Click += btnPayrollCalc_Click;
        msiClose.Click += btnClose_Click;
        msiSign.Click += btnSignPayroll_Click;

        panMain.Focus();
    }

    private void ShowPersNameinTitle()
    {
        if (grvSalaryTotal.FocusedRowHandle < 0 || grvPayrollBenefit.FocusedRowHandle < 0) return;

        _rowPersSelect = grvSalaryTotal.GetDataRow(grvSalaryTotal.FocusedRowHandle);
        _PersID = Convert.ToInt32(_rowPersSelect["PersID"]);
        lblTitle2.Text = _rowPersSelect["FullName"] + " ";
    }

    private void grdSalaryTotal_Click(object sender, EventArgs e)
    {
        ShowPersNameinTitle();
    }

    private void grdSalaryTotal_KeyDown(object sender, KeyEventArgs e)
    {
        ShowPersNameinTitle();
    }

    private void grdSalaryTotal_KeyUp(object sender, KeyEventArgs e)
    {
        ShowPersNameinTitle();
    }

    private void LoadData()
    {
        CreateGridRecNo();
        grdPayrollBenefit.DataSource = _bllPayrollSummarySave.PayrollItemSummaryLoad(_Year, _Mon);
        grdSalaryTotal.DataSource = _bllPayrollSummarySave.SalaryTotalLoad(_Year, _Mon);
    }

    private void btnPayrollCalc_Click(object sender, EventArgs e)
    {
        _bllPayrollSummarySave.CreateSalary(_Year, _Mon, GL.UserPersID);

        LoadData();
    }


    private void CreateGridRecNo()
    {
        var col = grvPayrollBenefit.Columns.ColumnByFieldName("RecNo");
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvPayrollBenefit.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        var col1 = grvSalaryTotal.Columns.ColumnByFieldName("RecNo");
        col1.Visible = true;
        col1.UnboundType = UnboundColumnType.Integer;
        grvSalaryTotal.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
    }

    private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }

    private void barBtnPayrollItemReport_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvPayrollSummaryTotal =
            @"select * 
                    from vPayrollSummaryTotal
                    where Year=@Year AND Mon=@Mon Order By SortID";

        tsqlvPayrollSummaryTotal = tsqlvPayrollSummaryTotal.Replace("@Year", _Year.ToString());
        tsqlvPayrollSummaryTotal = tsqlvPayrollSummaryTotal.Replace("@Mon", _Mon.ToString());

        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");


        var report = _bllReport.RepRptFileLoad(100006);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;


        ((StiSqlSource)report.Dictionary.DataSources["vPayrollSummaryTotal"]).SqlCommand = tsqlvPayrollSummaryTotal;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;


        report.Show();
    }

    private void barBtnSalaryReport_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvSalaryTotal =
            @"select * 
                    from vSalaryTotal
                    where Year=@Year AND Mon=@Mon ORDER BY SortID";

        tsqlvSalaryTotal = tsqlvSalaryTotal.Replace("@Year", _Year.ToString());
        tsqlvSalaryTotal = tsqlvSalaryTotal.Replace("@Mon", _Mon.ToString());

        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");


        var report = _bllReport.RepRptFileLoad(100007);


        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;


        ((StiSqlSource)report.Dictionary.DataSources["vSalaryTotal"]).SqlCommand = tsqlvSalaryTotal;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;


        report.Show();
    }

    private void barBtnPersReceipt_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (_PersID == 0)
        {
            var strMessage = "لطفا پرسنل راانتخاب نمائید";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("پیغام خطا : " + _PersID, strMessage, true);
            return;
        }

        var tsqlvPersonnelFull =
            @"select * from vPersonnelFull
                  where issalary = 1 AND PersID=@PersID";

        tsqlvPersonnelFull = tsqlvPersonnelFull.Replace("@PersID", _PersID.ToString());

        var tsqlvPayrollReceipt =
            @"select* from vPayrollReceipt
                 WHERE Year = @Year AND Mon = @Mon AND itemval> 0";

        tsqlvPayrollReceipt = tsqlvPayrollReceipt.Replace("@Year", _Year.ToString());
        tsqlvPayrollReceipt = tsqlvPayrollReceipt.Replace("@Mon", _Mon.ToString());

        var tsqlvSalaryTotal =
            @" SELECT s.*	, 
		ISNULL(PersonPercase.PercasePercaseAmount,0) AS PercasePercaseAmount, PersonPercaseMonths
                FROM vSalaryTotal s
                LEFT OUTER JOIN 
(
SELECT pp.PersID, SUM(pp.ResultAmount) AS PercasePercaseAmount, 
STRING_AGG(TRIM(STR(ppr.Mon)), ',') WITHIN GROUP (ORDER BY TRIM(STR(ppr.Mon)) ASC) AS PersonPercaseMonths
	FROM SamanERP.dbo.PersonPercaseReg ppr
                INNER JOIN  SamanERP.dbo.PersonPercase pp ON pp.RegID=ppr.RegID
                WHERE 1=1
				AND LEFT(ppr.ActionDate,7)=TRIM(STR(@Year))+'/'+ RIGHT('0'+TRIM(STR(@Mon)),2)
				AND ppr.IsSign = 1   
                GROUP BY pp.PersID               		   
			   ) PersonPercase ON PersonPercase.PersID=s.PersID
                WHERE s.Year = @Year AND s.Mon = @Mon";

        tsqlvSalaryTotal = tsqlvSalaryTotal.Replace("@Year", _Year.ToString());
        tsqlvSalaryTotal = tsqlvSalaryTotal.Replace("@Mon", _Mon.ToString());

        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");


        var report = _bllReport.RepRptFileLoad(100008);


        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;


        ((StiSqlSource)report.Dictionary.DataSources["vPersonnelFull"]).SqlCommand = tsqlvPersonnelFull;
        ((StiSqlSource)report.Dictionary.DataSources["vPayrollReceipt"]).SqlCommand = tsqlvPayrollReceipt;
        ((StiSqlSource)report.Dictionary.DataSources["vSalaryTotal"]).SqlCommand = tsqlvSalaryTotal;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;


        report.Show();
    }

    private void barBtnPayrollReceipt_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvPersonnelFull =
            @"select * from vPersonnelFull
                  where issalary = 1";


        var tsqlvPayrollReceipt =
            @"select* from vPayrollReceipt
                 WHERE Year = @Year AND Mon = @Mon AND itemval> 0";

        tsqlvPayrollReceipt = tsqlvPayrollReceipt.Replace("@Year", _Year.ToString());
        tsqlvPayrollReceipt = tsqlvPayrollReceipt.Replace("@Mon", _Mon.ToString());

        var tsqlvSalaryTotal =
            @" SELECT s.*	, 
		ISNULL(PersonPercase.PercasePercaseAmount,0) AS PercasePercaseAmount, PersonPercaseMonths
                FROM vSalaryTotal s
                LEFT OUTER JOIN 
(
SELECT pp.PersID, SUM(pp.ResultAmount) AS PercasePercaseAmount, 
STRING_AGG(TRIM(STR(ppr.Mon)), ',') WITHIN GROUP (ORDER BY TRIM(STR(ppr.Mon)) ASC) AS PersonPercaseMonths
	FROM SamanERP.dbo.PersonPercaseReg ppr
                INNER JOIN  SamanERP.dbo.PersonPercase pp ON pp.RegID=ppr.RegID
                WHERE 1=1
				AND LEFT(ppr.ActionDate,7)=TRIM(STR(@Year))+'/'+ RIGHT('0'+TRIM(STR('@Mon')),2)
				AND ppr.IsSign = 1   
                GROUP BY pp.PersID               		   
			   ) PersonPercase ON PersonPercase.PersID=s.PersID
                WHERE s.Year = @Year AND s.Mon = @Mon
                ";

        tsqlvSalaryTotal = tsqlvSalaryTotal.Replace("@Year", _Year.ToString());
        tsqlvSalaryTotal = tsqlvSalaryTotal.Replace("@Mon", _Mon.ToString());

        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");


        var report = _bllReport.RepRptFileLoad(100008);


        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;


        ((StiSqlSource)report.Dictionary.DataSources["vPersonnelFull"]).SqlCommand = tsqlvPersonnelFull;
        ((StiSqlSource)report.Dictionary.DataSources["vPayrollReceipt"]).SqlCommand = tsqlvPayrollReceipt;
        ((StiSqlSource)report.Dictionary.DataSources["vSalaryTotal"]).SqlCommand = tsqlvSalaryTotal;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;


        report.Show();
    }


    private void btnSignPayroll_Click(object sender, EventArgs e)
    {
        //_IsSign = !_IsSign;
        _bllSalaryRegSign.SalaryRegSign(_Year, _Mon, 1, GL.UserPersID);
        System.Windows.Forms.Form.ActiveForm.Update();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
    }

    private void grvSalaryTotal_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        var view = sender as GridView;

        var TotalPayAmount = Convert.ToInt64(view.GetRowCellValue(e.RowHandle, "TotalPayAmount"));
        if (TotalPayAmount <= 0) e.Appearance.BackColor = Color.PaleVioletRed;
    }

    private void barBtnWH_ItemClick(object sender, ItemClickEventArgs e)
    {
        using (var saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.Title = "ذخیره فایل خروجی";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.FileName = "WH" + _Year + _Mon;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = saveFileDialog.FileName;
                SaveDataToFileWH(filePath);
                MessageBox.Show("داده‌ها با موفقیت ذخیره شدند!", "موفقیت", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }

    private void SaveDataToFileWH(string filePath)
    {
        var connectionString = cSet.cnStringERP;
        var query =
            "SELECT CONVERT(NVARCHAR(MAX), pe.NationalCode) + ',' + --کد ملی\n\tCONVERT(NVARCHAR(MAX), 1) + ',' + --نوع پرداخ تی\n\tCONVERT(NVARCHAR(MAX), 1) + ',' + --وضع یت محل خدمت\n\tCONVERT(NVARCHAR(MAX), 1) + ',' + --استثنائات موضوع قانون بودجه 1\n\tCONVERT(NVARCHAR(MAX), 84) + ',' + --نوع ارز\n\tCONVERT(NVARCHAR(MAX), 1) + ',' + --نرخ تسع یر ارز\n    CONVERT(NVARCHAR(MAX), p.BenefitTempImpure + s.TotalSalaryAmount) + ',' + --مبلغ جمع ناخالص حقوق و مزایا ی مستمر نقدی ماه جاری - ریالی\n    CONVERT(NVARCHAR(MAX), 0) + ',' + --مبلغ حقوق و مزایای مستمر نقدی معوق که مال یات ی برای آنها محاسبه نشده است - ریال ی\n\tCONVERT(NVARCHAR(MAX), 1) + ',' + --مسک ن )نحوه استفاده از مسکن(\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --مبلغ کسر شده از حقوق کارمند بابت مسکن ماه جاری- ریالی\n\tCONVERT(NVARCHAR(MAX), 1) + ',' + --اتومب یل اختصاصی )نحوه استفاده از اتومبیل(\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --مبلغ کسر شده از حقوق کارمند بابت اتومبیل اختصاصی ماه جار ی- ریالی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --مبلغ ق یمت تمام شده سایر مزایا ی مستمر غ یرنقدی- ریالی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --مبلغ حقوق و مزایای مستمر غ یر نقدی معوق که مال یات ی برای آنها محاسبه نشده است- ریال ی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --مبلغ حق الزحمه/حق مشاوره/حق حضور/حق نظارت/حق التالیف/ حق فنی/ پاداش شورای\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --مبلغ قراردادهای پژوهشی - ریال ی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --اضافه کاری- ریالی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --هزینه سفر- ریالی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --فوق العاده مسافرت )ماموریت( - ریالی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --کارانه - ریال ی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --پاداش )به استثنای پاداش آخر سال و پاداش پایان خدمت و پاداش بهره وری( - ریالی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --پاداش آخر سال- ریالی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --ع یدی سالانه- ریالی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --پاداش پایان خدمت- ریال ی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --خسارت اخراج- ریالی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --بازخرید خدمت- ریال ی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --حق سنوات- ریال ی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --حقوق ایام مرخص ی استفاده نشده- ریالی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --سایر حقوق و مزایای غ یر مستمر نقدی ماه جاری - ریال ی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --مبلغ حقوق و مزایای غ یر مستمر نقدی معوق که مال یات ی برای آنها محاسبه نشده است-\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --مبلغ ق یمت تمام شده مزایای غ یر مستمر غ یرنقدی ماه جاری - ریال ی\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + --مبلغ مزایای غ یر مستمر غ یر نقدی معوق که مال یات ی برای آنها محاسبه نشده است- ریالی\n    CONVERT(NVARCHAR(MAX), 0) + ',' + -- حق ب یمه های درمان موضوع ماده 137 ق.م. م .\n\tCONVERT(NVARCHAR(MAX), 0) + ',' +\t\t\t\t\t-- حق ب یمه های عمر و زندگی موضوع ماده 137 ق.م. م\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + -- خالص پرداختی به حقوق بگیر\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + -- حق التدریس/حق التحقیق/ حق پژوهش\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + -- حق کشیک\n\tCONVERT(NVARCHAR(MAX), 0) + ',' + -- رفاهی و انگیزشی و بهره وری\n    CONVERT(NVARCHAR(MAX), 0) AS result_string --حق السعی ) به استثنای مزد، حقوق، پاداش(\nFROM [SamanERP].[dbo].[PayrollSummaryTotal] p\nINNER JOIN dbo.SalaryTotal s ON s.Year = p.Year AND s.Mon = p.Mon AND s.PersID = p.PersID\r\nINNER JOIN dbo.Personnel pe ON p.PersID = pe.PersID\r\n\r\n\t\t\tWHERE p.Year = @Year\t AND p.Mon = @Mon";

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Year", _Year);
                    command.Parameters.AddWithValue("@Mon", _Mon);

                    using (var reader = command.ExecuteReader())
                    using (var writer = new StreamWriter(filePath))
                    {
                        while (reader.Read())
                        {
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                writer.Write(reader[i].ToString());
                                if (i < reader.FieldCount - 1)
                                    writer.Write("\t");
                            }

                            writer.WriteLine();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("خطا: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void barBtnWP_ItemClick(object sender, ItemClickEventArgs e)
    {
        using (var saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.Title = "ذخیره فایل خروجی";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.FileName = "WP" + _Year + _Mon;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = saveFileDialog.FileName;
                SaveDataToFileWP(filePath);
                MessageBox.Show("داده‌ها با موفقیت ذخیره شدند!", "موفقیت", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }

    private void SaveDataToFileWP(string filePath)
    {
        var connectionString = cSet.cnStringERP;
        var query =
            "SELECT\r\n\r\nCONVERT(NVARCHAR(MAX),\t1) + ',' + --\tملیت+\r\nCONVERT(NVARCHAR(MAX),\tISNULL(p.NationalCode, '0')) + ',' + --\tکد ملی\r\nCONVERT(NVARCHAR(MAX),\tISNULL(p.FirstName, '0')) + ',' + --\tنام+\r\nCONVERT(NVARCHAR(MAX),\tISNULL(p.LastName, '0')) + ',' + --\tنام خانوادگی+\r\nCONVERT(NVARCHAR(MAX),\tISNULL(p.FatherName, '0')) + ',' + --\tنام پد ر\r\nCONVERT(NVARCHAR(MAX),\tISNULL(p.BirthDate, '0')) + ',' + --\tتاریخ تولد+\r\nCONVERT(NVARCHAR(MAX),\tISNULL(p.IDCardNumber, '0')) + ',' + --\tشماره شناسنامه\r\nCONVERT(NVARCHAR(MAX),  ISNULL(p.BirthArea, '0')) + ',' + --\tمحل تولد\r\nCONVERT(NVARCHAR(MAX),\tISNULL(p.EducationLevel, '0')) + ',' + --\tمدرک تحصیلی+\r\nCONVERT(NVARCHAR(MAX),\tISNULL(p.Insurance, '0')) + ',' + --\tنوع بیمه*\r\nCONVERT(NVARCHAR(MAX),\t'0') + ',' + --\tشماره بیمه*\r\nCONVERT(NVARCHAR(MAX),\tISNULL(p.InsuranceName, '0')) + ',' + --\tنام بیمه*\r\nCONVERT(NVARCHAR(MAX),\tISNULL(p.TaxExemption, '0')) + ',' + --\t*نوع معافیت+\r\nCONVERT(NVARCHAR(MAX),\t103) + ',' + --\tکشور محل تابعیت\r\nCONVERT(NVARCHAR(MAX),\t103) + ',' + --\tکشور محل زندگی\r\nCONVERT(NVARCHAR(MAX),\t0) + ',' + --\tکدپستی\r\nCONVERT(NVARCHAR(MAX),\tISNULL(p.AddressDetail, '0')) + ',' + --\tآدرس\r\nCONVERT(NVARCHAR(MAX),\t7) + ',' + --\tرسته+\r\nCONVERT(NVARCHAR(MAX),\t'پزشک') + ',' + --\tسمت*+\r\nCONVERT(NVARCHAR(MAX),\tISNULL(p.ContractType, '0')) + ',' + --\tنوع استخدام+\r\nCONVERT(NVARCHAR(MAX),\tISNULL(e.StartDate, '0')) + ',' + --\tتاریخ شروع به کار+\r\nCONVERT(NVARCHAR(MAX),\t0) + ',' + --\tتاریخ پایان کار\r\nCONVERT(NVARCHAR(MAX),\t0) AS result_string--\tتاریخ بازنشستگی\r\n\r\nFROM SamanERP.dbo.SalaryTotal s\r\n\r\nINNER JOIN SamanERP.dbo.vPersonnelFull p ON p.PersID = s.PersID\r\nINNER JOIN SamanERP.dbo.EmpAction e ON e.PersID = p.PersID\r\n\r\nWHERE s.Year = @Year\t AND s.Mon = @Mon AND e.IsDisable = 0";

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Year", _Year);
                    command.Parameters.AddWithValue("@Mon", _Mon);

                    using (var reader = command.ExecuteReader())
                    using (var writer = new StreamWriter(filePath))
                    {
                        while (reader.Read())
                        {
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                writer.Write(reader[i].ToString());
                                if (i < reader.FieldCount - 1)
                                    writer.Write("\t");
                            }

                            writer.WriteLine();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("خطا: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    //private void barBtnImportExcelTax_ItemClick(object sender, ItemClickEventArgs e)
    //{
    //    // نمایش دیالوگ انتخاب فایل اکسل
    //    OpenFileDialog openFileDialog = new OpenFileDialog
    //    {
    //        Filter = "Excel Files|*.xlsx;*.xls",
    //        Title = "Select an Excel File"
    //    };

    //    if (openFileDialog.ShowDialog() == DialogResult.OK)
    //    {
    //        try
    //        {
    //            using (var workbook = new XLWorkbook(openFileDialog.FileName))
    //            {
    //                var worksheet = workbook.Worksheet(1); // اولین شیت
    //                var rows = worksheet.RangeUsed().RowsUsed(); // خواندن محدوده استفاده‌شده

    //                using (SqlConnection conn = new SqlConnection(cSet.cnStringERP))
    //                {
    //                    conn.Open();

    //                    foreach (var row in rows)
    //                    {
    //                        string column1 = row.Cell(1).GetValue<string>(); // مقدار ستون اول
    //                        string column2 = row.Cell(2).GetValue<string>(); // مقدار ستون دوم

    //                        string query = "INSERT INTO MyTable (Column1, Column2) VALUES (@column1, @column2)";
    //                        using (SqlCommand cmd = new SqlCommand(query, conn))
    //                        {
    //                            cmd.Parameters.AddWithValue("@column1", column1);
    //                            cmd.Parameters.AddWithValue("@column2", column2);
    //                            cmd.ExecuteNonQuery();
    //                        }
    //                    }

    //                    MessageBox.Show("اطلاعات با موفقیت وارد و در SQL Server ذخیره شدند!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show($"خطا در پردازش فایل: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //        }
    //    }
    //}


    private void barBtnImportExcelTax_ItemClick(object sender, ItemClickEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Excel Files|*.xlsx;*.xls",
            Title = "Select an Excel File"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
            try
            {
                var fileName =
                    Path.GetFileNameWithoutExtension(openFileDialog.FileName); // نام فایل اکسل به عنوان نام جدول
                using (var workbook = new XLWorkbook(openFileDialog.FileName))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RangeUsed().RowsUsed();

                    using (var conn = new SqlConnection(cSet.cnStringERP))
                    {
                        conn.Open();

                        // خواندن نام ستون‌ها از اولین ردیف
                        var headers = rows.First().CellsUsed().Select(c => c.GetValue<string>()).ToList();

                        // ساخت دستور ایجاد جدول دینامیک
                        var createTableQuery = $"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '{fileName}') " +
                                               $"CREATE TABLE {fileName} ({string.Join(", ", headers.Select(h => $"[{h}] NVARCHAR(MAX)"))})";

                        using (var createCmd = new SqlCommand(createTableQuery, conn))
                        {
                            createCmd.ExecuteNonQuery();
                        }

                        // درج داده‌ها در جدول SQL
                        foreach (var row in rows.Skip(1)) // حذف ردیف اول (نام ستون‌ها)
                        {
                            var insertQuery =
                                $"INSERT INTO {fileName} ({string.Join(", ", headers.Select(h => $"[{h}]"))}) " +
                                $"VALUES ({string.Join(", ", headers.Select((h, i) => $"@param{i}"))})";

                            using (var insertCmd = new SqlCommand(insertQuery, conn))
                            {
                                for (var i = 0; i < headers.Count; i++)
                                    insertCmd.Parameters.AddWithValue($"@param{i}", row.Cell(i + 1).GetValue<string>());
                                insertCmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("اطلاعات با موفقیت در پایگاه داده ذخیره شدند!", "Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در پردازش فایل: {ex.Message}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
    }
}