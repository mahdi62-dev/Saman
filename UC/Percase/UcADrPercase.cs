using System;
using System.Data;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using LIB;
using Saman.APP.Stimul;
using Saman.BLL.Percase;
using Stimulsoft.Report.Dictionary;
using Control = App.Control;

namespace Saman.UC;

public partial class UcADrPercase : XtraUserControl
{
    private readonly Stimul _appStimul = new();


    private readonly ADrPercase _bllADrPercase = new();

    private readonly BLL.Report.Report _bllReport = new();

    private readonly long _ManagerAmount, _SDrAmount, _ADrAmount;
    private readonly string _SignDate;
    private string _Date1, _Date2;
    private DataTable _dtADrPercase;

    private DataTable _dtADrPercaseReg;
    private bool _IsSign;
    private int _Mon;
    private string _Monstr;

    private int _RegID;
    private string _SignUserPersName;
    private int _Year;


    public UcADrPercase()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }

    private void UcADrPercase_Load(object sender, EventArgs e)
    {
        var appSDate = new SDate();

        var curDateTime = appSDate.ShamsiDate();
        string year, mon;

        year = curDateTime.Substring(0, 4);
        mon = curDateTime.Substring(5, 2);

        UL.FillLookup(lueYear, year, cSet.cnStringA, false, "ID,Name", "vADR", "Year",
            "ADName='Year'", null);
        UL.FillLookup(lueMon, mon, cSet.cnStringA, false, "ID,Name", "vADR", "mon",
            "ADName='Month'", null);

        ShortKey();

        ConvertDate();
    }

    private void ConvertDate()
    {
        if (_Mon.ToString().Length != 2)
            _Monstr = "0" + _Mon;
        else
            _Monstr = _Mon.ToString();

        _Date1 = "'" + _Year + "/" + _Monstr + "/" + "01 00:00'";
        _Date2 = "'" + _Year + "/" + _Monstr + "/" + "31 23:59'";
    }

    private void ShortKey()
    {
        msiCalcPercase.Click += btnCalcPercase_Click;
        msiClose.Click += btnCancel_Click;
        msiSign.Click += btnSignPercase_Click;


        panMain.Focus();
    }

    private void FormNull()
    {
        _RegID = 0;

        ActiveControl = lueMon;

        if (_dtADrPercase == null) return;

        _dtADrPercase.Clear();
        grdADrPercase.DataSource = _dtADrPercase;
    }

    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(lueMon, e);
        Control.TextEditLine(lueYear, e);
    }

    private void grvADrPercase_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }

    private void lueYear_EditValueChanged(object sender, EventArgs e)
    {
        _Year = Convert.ToInt32(lueYear.EditValue);
        ConvertDate();
        ADrPercaseLoadData();
    }

    private void lueMon_EditValueChanged(object sender, EventArgs e)
    {
        _Mon = Convert.ToInt32(lueMon.EditValue);
        ConvertDate();
        ADrPercaseLoadData();
    }

    private void ADrPercaseLoadData()
    {
        FormNull();

        var col = grvADrPercase.Columns.ColumnByFieldName("RecNo");
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvADrPercase.CustomUnboundColumnData += grvADrPercase_CustomUnboundColumnData;

        _dtADrPercase = _bllADrPercase.SPADrPercase(1, _Year, _Mon, 0, GL.UserPersID);

        grdADrPercase.DataSource = _dtADrPercase;

        if (_dtADrPercase.Rows.Count > 0) _RegID = Convert.ToInt32(_dtADrPercase.Rows[0]["RegID"]);

        if (_RegID == 0)
        {
            ADrPercaseAmountFromDrPercase();
            btnCalcPercase.Text = "تشکیل کارانه";
        }
        else
        {
            RegData();
            btnCalcPercase.Text = "محاسبه";
        }
    }

    private void ADrPercaseAmountFromDrPercase()
    {
        var dtADrPercaseAmountFromDrPercase = _bllADrPercase.ADrPercaseAmountFromDrPercase(_Year, _Mon);
    }

    private void RegData()
    {
        _dtADrPercaseReg = _bllADrPercase.ADrPercaseReg(_RegID, null, null);

        _IsSign = Convert.ToBoolean(_dtADrPercaseReg.Rows[0]["IsSign"]);
        _SignUserPersName = _dtADrPercaseReg.Rows[0]["SignUserPersName"].ToString();
    }

    private void btnCalcPercase_Click(object sender, EventArgs e)
    {
        if (_IsSign)
        {
            var sui = new SUI();

            var strMessage = "";

            strMessage = "در تاریخ انتخاب شده کارانه محاسبه و امضاء شده است";
            sui.ShowWarningMessageForm("هشدار", strMessage, false);
        }


        _dtADrPercase = _bllADrPercase.SPADrPercase(0, _Year, _Mon, _RegID, GL.UserPersID);

        _RegID = Convert.ToInt32(_dtADrPercase.Rows[0]["RegID"]);
        RegData();

        PercaseItemSave();

        //_dtADrPercase = _bllADrPercase.SPADrPercase(0, _Year, _Mon, _RegID, _ManagerAmount, _SDrAmount,
        //    _ADrAmount, GL.UserPersID);
        grdADrPercase.DataSource = _dtADrPercase;

        ADrPercaseLoadData();

        btnCalcPercase.Text = "محاسبه";
    }


    private void PercaseItemSave()
    {
        var DrID = 0;
        double TotalAnesK = 0;
        var DayCount = 0;
        var ADrPercase = 0;


        for (var i = 0; i < grvADrPercase.RowCount; i++)
        {
            DrID = Convert.ToInt32(grvADrPercase.GetRowCellValue(i, "DrID"));
            TotalAnesK = Convert.ToDouble(grvADrPercase.GetRowCellValue(i, "TotalAnesK"));
            DayCount = Convert.ToInt32(grvADrPercase.GetRowCellValue(i, "DayCount"));
            ADrPercase = Convert.ToInt32(grvADrPercase.GetRowCellValue(i, "ADrPercaseAmount"));

            _bllADrPercase.ADrPercaseItemSave(_RegID, DrID, TotalAnesK, DayCount, ADrPercase, GL.UserPersID);
        }
    }

    private void CheckADrPercaseSign()
    {
        if (_IsSign)
        {
            btnSignPercase.Text = "بازگشت از امضاء";
            btnSignPercase.Enabled = false;
        }
        else
        {
            btnSignPercase.Text = "امضای سند";
            btnSignPercase.Enabled = true;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
    }


    private void btnSignPercase_Click(object sender, EventArgs e)
    {
        _IsSign = !_IsSign;
        _bllADrPercase.ADrPercaseReg(_RegID, _IsSign, GL.UserPersID);

        CheckADrPercaseSign();
    }

    private void grvADrPercase_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
    {
        if (e.Column.FieldName == "TotalAnesK")
        {
            RepTxtItemVal.Mask.EditMask = App.UI.MaskNumWithDecimal;
            e.RepositoryItem = RepTxtItemVal;
        }

        if (e.Column.FieldName == "ADrPercase" || e.Column.FieldName == "DayCount")
        {
            RepTxtItemVal.Mask.EditMask = App.UI.MaskNum;
            e.RepositoryItem = RepTxtItemVal;
        }
    }

    private void barBtnReportSelectADrPercaseSummary_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvADrDetial =
            @"

DECLARE @RT	TABLE (DrID INT , Name NVARCHAR(1000), AnesCount INT, TotalAnesK FLOAT, ResultAmount BIGINT DEFAULT(0))

INSERT INTO @RT
	(DrID , Name , AnesCount, TotalAnesK )
SELECT 
	d.DrID,
	MAX(d.Name) AS Name,
	COUNT(po.PageID) AS AnesCount,
	SUM(po.TotalAnesK) AS TotalAnesK

	FROM [SamanHIS].[dbo].[PageOpr] po
INNER JOIN SamanHIS.dbo.Page p ON p.PageID = po.PageID
INNER JOIN samanhis.dbo.vEncFix e ON e.EncID = p.EncID
INNER JOIN dbo.vDr d ON d.DrID=po.ADrID

WHERE e.EndDate BETWEEN @Date1 AND @Date2 AND e.EncStatus=4
AND TotalAnesK>0

GROUP BY d.DrID



UPDATE @RT
SET ResultAmount=t.ResultAmount
FROM @rt rt
INNER JOIN 
(
SELECT 
	d.DrID,
	SUM(eb.ResultAmount) AS ResultAmount

	FROM
	dbo.EncBill eb 
INNER JOIN dbo.Enc e ON e.EncID=eb.EncID
INNER JOIN vdr d ON d.DrID=eb.DrID

WHERE eb.ServiceCategory=32 AND e.EncStatus>=3 AND e.EndDate BETWEEN @Date1 AND @Date2
GROUP BY d.DrID
) AS T ON t.DrID=rt.DrID



SELECT 
	--DrID AS,
	Name AS 'نام پزشک',
	AnesCount 'تعداد بیمار',
	TotalAnesK AS 'کای بیهوشی',
	ResultAmount 'جمع مبلغ'
FROM @RT";

        tsqlvADrDetial = tsqlvADrDetial.Replace("@Date1", _Date1);
        tsqlvADrDetial = tsqlvADrDetial.Replace("@Date2", _Date2);

        var tsqlvADrSum =
            @"	
	SELECT 
	SUM(PayAmount) AS N'دریافتی صندوق پزشکان بیهوشی'
	, 	SUBSTRING(MAX(CashDate),0,8) AS N'تاریخ'

  FROM [SamanHIS].[dbo].[Cash]
  WHERE PayGroup=3 AND  CashDate BETWEEN @Date1 AND @Date2";

        tsqlvADrSum = tsqlvADrSum.Replace("@Date1", _Date1);
        tsqlvADrSum = tsqlvADrSum.Replace("@Date2", _Date2);

        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100024);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringA);
        report.Dictionary.Databases[0] = sti;


        ((StiSqlSource)report.Dictionary.DataSources["vADrDetial"]).SqlCommand = tsqlvADrDetial;
        ((StiSqlSource)report.Dictionary.DataSources["vADrSum"]).SqlCommand = tsqlvADrSum;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;


        report.Show();
    }
}