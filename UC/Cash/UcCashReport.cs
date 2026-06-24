using System;
using System.Data;
using DAL;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using LIB;
using Saman.APP.Stimul;
using Saman.BLL.Cash;
using Stimulsoft.Report.Dictionary;

namespace Saman.UC;

public partial class UcCashReport : XtraUserControl
{
    private readonly Stimul _appStimul = new();

    private readonly BLL.Report.Report _bllReport = new();
    private readonly Cash bllCash = new();
    private DataTable _EncCashDataList;

    public UcCashReport()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }

    private void UcCashReport_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        ActiveControl = datStartCashDate;

        datStartCashDate.ShowTodayStart = true;
        datEndCashDate.ShowTodayEnd = true;
        ShortKey();
    }

    private void ShortKey()
    {
        msiSearch.Click += btnDateLoad_Click;
        msiClose.Click += btnCancel_Click;

        panMain.Focus();
    }

    private void btnDateLoad_Click(object sender, EventArgs e)
    {
        CashSearch();
    }

    private void CashSearch()
    {
        _EncCashDataList = bllCash.GetCashDataReport(datStartCashDate.Text, datEndCashDate.Text);
        grdEncCashData.DataSource = _EncCashDataList;

        var col = grvEncCashData.Columns.ColumnByFieldName("RecNo");
        col.UnboundType = UnboundColumnType.Integer;
        grvEncCashData.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
        }
    }

    private void barBtnPrintCashReport_ItemClick_1(object sender, ItemClickEventArgs e)
    {
        var Date1 = datStartCashDate.Text;
        var Date2 = datEndCashDate.Text;


        var FirstDayOfMonth = Date1.Substring(0, 8) + "01 00:00";

        var tsqlVCashBook =
            @"
SELECT 
ISNULL(SUM(c.PayAmount),0) AS TotalCashPayAmount,
COUNT(*) AS CashCount
FROM dbo.Cash c
WHERE CashDate BETWEEN '1402/04/01 00:00' AND dbo.SDateAdd('DAY', -1, @Date2)
AND c.PayType=2 --AND c.PayGroup IN (1,4)

";

        tsqlVCashBook = tsqlVCashBook.Replace("@Date1", "'" + FirstDayOfMonth + "'");
        tsqlVCashBook = tsqlVCashBook.Replace("@Date2", "'" + Date2 + "'");

        var tsqlVCashReport =
            @"
 SELECT *
	,CASE WHEN PayAmount<0 THEN N'برگشتی' ELSE N'' END AS PayAmountStatus
FROM [dbo].[vCashReport]
Where 
CashDate between @Date1 AND @Date2
--AND PayGroup =1

 ORDER BY CashID

";

        tsqlVCashReport = tsqlVCashReport.Replace("@Date1", "'" + Date1 + "'");
        tsqlVCashReport = tsqlVCashReport.Replace("@Date2", "'" + Date2 + "'");


        var tsqlVCashBook2 =
            @"
 SELECT
TotalPayAmount,
TotalPayAmountClinic,
TotalGetAmount,
TotalRetAmount,
TotalClinicNaghdiGetAmount,
TotalClinicNaghdiRetAmount,
TotalCashPayAmountNaghdiGet,
TotalCashPayAmountNaghdiRet,
TotalBankPayAmountGheyrNaghdiGet,
TotalBankPayAmountGheyrNaghdiRet,
BankPasargadPos,
BankTejaratPos,
BankPasargadFish,
BankTejaratFish
FROM
(
 SELECT  
 ISNULL(SUM(CAST(c.PayAmount AS BIGINT)),0) AS TotalPayAmount
,ISNULL(SUM(CASE WHEN c.PayGroup=1 THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS TotalPayAmountClinic

,ISNULL(SUM(CASE WHEN c.PayAmount>0 THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS TotalGetAmount
,ISNULL(SUM(CASE WHEN c.PayAmount<0 THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS TotalRetAmount

,ISNULL(SUM(CASE WHEN c.PayAmount>0 AND c.PayType=2 AND c.PayGroup=1 THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS TotalClinicNaghdiGetAmount
,ISNULL(SUM(CASE WHEN c.PayAmount<0 AND c.PayType=2 AND c.PayGroup=1 THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS TotalClinicNaghdiRetAmount

,ISNULL(SUM(CASE WHEN c.PayAmount>0 AND c.PayType=2  THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS TotalCashPayAmountNaghdiGet
,ISNULL(SUM(CASE WHEN c.PayAmount<0 AND c.PayType=2  THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS TotalCashPayAmountNaghdiRet

,ISNULL(SUM(CASE WHEN c.PayAmount>0 AND c.PayType<>2 AND c.PayGroup=1 THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS TotalBankPayAmountGheyrNaghdiGet
,ISNULL(SUM(CASE WHEN c.PayAmount<0 AND c.PayType<>2 AND c.PayGroup=1 THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS TotalBankPayAmountGheyrNaghdiRet


,ISNULL(SUM(CASE WHEN c.PayType IN (1) AND c.PayGroup=1 AND c.Bank=7 THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS BankPasargadPos
,ISNULL(SUM(CASE WHEN c.PayType IN (1) AND c.PayGroup=1 AND c.Bank=16 THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS BankTejaratPos


,ISNULL(SUM(CASE WHEN c.PayType IN (3) AND c.PayGroup=1 AND c.Bank=7 THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS BankPasargadFish
,ISNULL(SUM(CASE WHEN c.PayType IN (3) AND c.PayGroup=1 AND c.Bank=16 THEN CAST(c.PayAmount AS BIGINT) ELSE 0 END),0) AS BankTejaratFish

FROM dbo.Cash c
WHERE CashDate BETWEEN @Date1 AND @Date2
--AND c.PayGroup =1
 ) AS T
";
        tsqlVCashBook2 = tsqlVCashBook2.Replace("@Date1", "'" + Date1 + "'");
        tsqlVCashBook2 = tsqlVCashBook2.Replace("@Date2", "'" + Date2 + "'");
        tsqlVCashBook2 = tsqlVCashBook2.Replace("@FirstDayOfMonth", "'" + FirstDayOfMonth + "'");

        var RptProperty =
            @"  SELECT
            dbo.SCurDateTime() AS PrintDate
            ,'@DateRange' as DateRange
            ,'@ReportUserName' AS ReportUserName ";

        RptProperty = RptProperty.Replace("@DateRange", Date1.Substring(0, 10) + " تا " + Date2.Substring(0, 10));
        RptProperty = RptProperty.Replace("@ReportUserName", GL.UserPersName);


        var report = _bllReport.RepRptFileLoad(100005);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringA);
        report.Dictionary.Databases[0] = sti;

        ////var t = report.Dictionary.DataSources.Items;


        ((StiSqlSource)report.Dictionary.DataSources["vCashBook"]).SqlCommand = tsqlVCashBook;
        ((StiSqlSource)report.Dictionary.DataSources["vCashBook2"]).SqlCommand = tsqlVCashBook2;
        ((StiSqlSource)report.Dictionary.DataSources["vCashReport"]).SqlCommand = tsqlVCashReport;

        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = RptProperty;


        report.Show();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
    }
}