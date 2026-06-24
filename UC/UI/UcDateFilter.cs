using System;
using System.Data;
using System.Globalization;
using App;
using DAL;
using DevExpress.XtraEditors;
using LIB;

namespace Saman.UC.UI;

public partial class UcDateFilter : XtraUserControl
{
    private DateTime _curDateTime;

    private int _DateFilterType;
    public string _Filter;
    public string _filterColumn;

    private int _rangeType;

    public string _StartDate, _EndDate;
    private string _strCurDate;
    private string _strCurDateTime;


    public UcDateFilter()
    {
        InitializeComponent();
    }

    public int DateFilterType
    {
        set
        {
            _DateFilterType = value;

            if (_DateFilterType > 0) lueDateFilterType.EditValue = _DateFilterType;
        }
    }

    private void UcDateTime_Load(object sender, EventArgs e)
    {
        lueDateFilterType.Enabled = true;


        UL.FillLookup(lueDateFilterType, "1", cSet.cnStringA, false, "ID,Name,Name2", "vADR", "DateFilterType",
            "ADName='DateFilterType'", null);

        UL.FillLookup(lueDateFilterRange, "1", cSet.cnStringA, false, "ID,Name", "vADR", "DateFilterRange",
            "ADName='DateFilterRange'", null);

        SetInitialDate();


        if (_DateFilterType > 0)
        {
            lueDateFilterType.EditValue = _DateFilterType.ToString();
            lueDateFilterType.Enabled = false;
        }


        DateFilterRange();
    }

    private void SetInitialDate()
    {
        var appSDate = new SDate();

        _strCurDateTime = appSDate.ShamsiDate();
        _strCurDate = _strCurDateTime.Substring(0, 10);

        _curDateTime = DateTime.Parse(_strCurDateTime);

        var year = _strCurDateTime.Substring(0, 4);
        var mon = Convert.ToInt32(_strCurDateTime.Substring(5, 2));

        UL.FillLookup(lueYear, year, cSet.cnStringA, false, "ID,Name", "vADR", "Year",
            "ADName='Year'", null);

        UL.FillLookup(lueMon, mon.ToString(), cSet.cnStringA, false, "ID,Name", "vADR", "mon",
            "ADName='Month'", null);

        datDate.DateTime = _curDateTime;
    }

    private void lueDateFilterRange_EditValueChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lueDateFilterRange.EditValue) == 0) return;

        DateFilterRange();
    }

    private void DateFilterRange()
    {
        if (_strCurDateTime == null || Convert.ToInt32(lueDateFilterRange.EditValue) == 0) return;

        panDay.Visible = false;
        panMon.Visible = false;
        panDateRange.Visible = false;

        _rangeType = Convert.ToInt32(lueDateFilterRange.EditValue);

        switch (_rangeType)
        {
            case 1:
                panDay.Visible = true;
                datDate.DateTime = _curDateTime;

                dat1.DateTime = _curDateTime;
                tim1.EditValue = " 00:00";

                dat2.DateTime = _curDateTime;
                tim2.EditValue = DateTime.Parse(_strCurDate + " 23:59:59");
                break;

            case 2:
                panMon.Visible = true;
                MonthSet();
                break;

            case 3:
                panDateRange.Visible = true;
                dat1.DateTime = _curDateTime;
                tim1.EditValue = " 00:00";

                dat2.DateTime = _curDateTime;
                tim2.EditValue = DateTime.Parse(_strCurDate + " 23:59:59");
                break;
        }

        FilterSet();
    }

    private void FilterSet()
    {
        string date1, date2;
        date1 = "'" + dat1.Text + " " + tim1.Text + "'";
        date2 = "'" + dat2.Text + " " + tim2.Text + "'";

        _StartDate = dat1.Text + " " + tim1.Text;
        _EndDate = dat2.Text + " " + tim2.Text;

        _Filter = _filterColumn;
        _Filter = _Filter.Replace("@Date1", date1);
        _Filter = _Filter.Replace("@Date2", date2);
    }

    private void lueDateFilterType_EditValueChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lueDateFilterType.EditValue) == 0) return;

        var row =
            lueDateFilterType.Properties.GetDataSourceRowByKeyValue(lueDateFilterType.EditValue) as DataRowView;
        _filterColumn = row["Name2"] + " BETWEEN @Date1 AND @Date2";

        SetInitialDate();
        DateFilterRange();
    }

    private void datDate_EditValueChanged(object sender, EventArgs e)
    {
        if (_rangeType != 1) return;

        dat1.DateTime = datDate.DateTime;
        tim1.EditValue = " 00:00";

        dat2.DateTime = datDate.DateTime;
        tim2.EditValue = " 23:59";

        FilterSet();
    }

    private string MonthStartDateTime(int Year, int Month)
    {
        string month;
        if (Month.ToString().Length == 1)
            month = "0" + Month;
        else
            month = Month.ToString();

        return Year + "/" + month + "/01 00:00";
    }

    private string MonthEndDateTime(int Year, int Month)
    {
        string month, day;
        if (Month.ToString().Length == 1)
            month = "0" + Month;
        else
            month = Month.ToString();

        var pc = new PersianCalendar();
        day = pc.GetDaysInMonth(Year, Month).ToString();

        return Year + "/" + month + "/" + day + " 23:59";
    }

    private void lueMon_EditValueChanged(object sender, EventArgs e)
    {
        MonthSet();
        FilterSet();
    }

    private void MonthSet()
    {
        if (_rangeType != 2) return;

        var mon = Convert.ToInt32(lueMon.EditValue);
        var year = Convert.ToInt32(lueYear.EditValue);

        var date1 = MonthStartDateTime(year, mon);
        dat1.DateTime = DateTime.Parse(date1);

        tim1.EditValue = " 00:00";

        var date2 = MonthEndDateTime(year, mon);
        dat2.DateTime = DateTime.Parse(date2);

        tim2.EditValue = " 23:59";
    }

    private void lueYear_EditValueChanged(object sender, EventArgs e)
    {
        var mon = 1;

        var year = Convert.ToInt32(lueYear.EditValue);
        if (lueMon.EditValue != null) mon = Convert.ToInt32(lueMon.EditValue);

        if (_rangeType != 2) return;

        dat1.DateTime = DateTime.Parse(MonthStartDateTime(year, mon));
        tim1.EditValue = " 00:00";

        dat2.DateTime = DateTime.Parse(MonthEndDateTime(year, mon));
        tim2.EditValue = " 23:59";

        FilterSet();
    }

    private void dat1_EditValueChanged(object sender, EventArgs e)
    {
        FilterSet();
    }

    private void tim1_EditValueChanged(object sender, EventArgs e)
    {
        FilterSet();
    }

    private void dat2_EditValueChanged(object sender, EventArgs e)
    {
        FilterSet();
    }

    private void tim2_EditValueChanged(object sender, EventArgs e)
    {
        FilterSet();
    }
}