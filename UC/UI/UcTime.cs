using System;
using System.Windows.Forms;
using App;
using DevExpress.XtraEditors;
using Control = App.Control;

namespace Saman.UC.UI;

public partial class UcTime : XtraUserControl
{
    public UcTime()
    {
        InitializeComponent();
    }

    public bool ShowNow
    {
        set => ShowNowDateTime(value);
    }

    public bool Show1DayOfMonth
    {
        set => Show1DayOfMonthDateTime(value);
    }

    public bool ShowTodayStart
    {
        set => ShowTodayStartDateTime(value);
    }

    public bool ShowTodayEnd
    {
        set => ShowTodayEndDateTime(value);
    }

    //public string Text
    //{
    //    //date.Hour;
    //    //date.Minute;

    //    set => SetDateTime(value);
    //}

    //public bool Enabled
    //{
    //    set => tim.Enabled = value;
    //}

    public bool ReadOnly
    {
        set => tim.ReadOnly = value;
    }

    private void UcDateTime_Load(object sender, EventArgs e)
    {
        //var curDateTime = DateTime.Parse(App.SDate.ShamsiDate());
        //datOprEndDate.DateTime = datOprStartDate.DateTime = curDateTime;
    }

    private void SetDateTime(string val)
    {
        if (string.IsNullOrEmpty(val)) return;

        tim.EditValue = DateTime.Parse(val);
    }

    private void ShowNowDateTime(bool val)
    {
        if (!val) return;

        var appSDate = new SDate();
        var curDateTime = DateTime.Parse(appSDate.ShamsiDate());
        tim.EditValue = curDateTime;
    }

    private void Show1DayOfMonthDateTime(bool val)
    {
        if (!val) return;

        var appSDate = new SDate();
        var strCurDateTime = appSDate.ShamsiDate();

        strCurDateTime = strCurDateTime.Substring(0, 8) + "01 00:00";
        var curDateTime = DateTime.Parse(strCurDateTime);
        tim.EditValue = curDateTime;
    }


    private void ShowTodayStartDateTime(bool val)
    {
        if (!val) return;

        var appSDate = new SDate();
        var curDateTime = DateTime.Parse(appSDate.ShamsiDate());

        var ts = new TimeSpan(0, 0, 0);
        curDateTime = curDateTime.Date + ts;

        tim.EditValue = curDateTime;
    }

    private void ShowTodayEndDateTime(bool val)
    {
        if (!val) return;

        var appSDate = new SDate();
        var curDateTime = DateTime.Parse(appSDate.ShamsiDate());

        var ts = new TimeSpan(23, 59, 59);
        curDateTime = curDateTime.Date + ts;

        tim.EditValue = curDateTime;
    }

    private void UcDateTime_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(tim, e);
    }
}