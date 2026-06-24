using System;
using System.Windows.Forms;
using App;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using FarsiLibrary.Win.Enums;
using Control = App.Control;

namespace Saman.UC.UI;

public partial class UcDateTime : XtraUserControl
{
    private bool _Line;

    public UcDateTime()
    {
        InitializeComponent();
    }

    public bool ShowNow
    {
        set => ShowNowDateTime(value);
    }

    public bool Show1DayOfMonth
    {
        set => ShowFirstDayOfMonthDateTime(value);
    }

    public bool ShowTodayStart
    {
        set => ShowTodayStartDateTime(value);
    }

    public bool ShowTodayEnd
    {
        set => ShowTodayEndDateTime(value);
    }

    public string Text
    {
        get => Date.Text + " " + tim.Text;
        set => SetDateTime(value);
    }


    public bool Enabled
    {
        set
        {
            Date.Enabled = value;
            tim.Enabled = value;
        }
    }

    public bool ReadOnly
    {
        set
        {
            Date.ReadOnly = value;
            tim.ReadOnly = value;
        }
    }

    private void UcDateTime_Load(object sender, EventArgs e)
    {
        Date.Theme = ThemeTypes.Office2000;
    }

    private void SetDateTime(string val)
    {
        if (string.IsNullOrEmpty(val)) return;

        Date.Text = val.Substring(0, 10);
        tim.EditValue = DateTime.Parse(val);
    }

    private void ShowNowDateTime(bool val)
    {
        if (!val) return;

        var appSDate = new SDate();

        var curDateTime = DateTime.Parse(appSDate.ShamsiDate());
        Date.Text = appSDate.ShamsiDate().Substring(0, 10);
        tim.EditValue = curDateTime;
    }

    private void ShowFirstDayOfMonthDateTime(bool val)
    {
        if (!val) return;

        var appSDate = new SDate();
        var strCurDateTime = appSDate.ShamsiDate();

        strCurDateTime = strCurDateTime.Substring(0, 8) + "01 00:00";
        var curDateTime = DateTime.Parse(strCurDateTime);

        Date.Text = strCurDateTime.Substring(0, 10);
        tim.EditValue = curDateTime;
    }

    private void ShowTodayStartDateTime(bool val)
    {
        if (!val) return;

        var appSDate = new SDate();
        var curDateTime = DateTime.Parse(appSDate.ShamsiDate());

        var ts = new TimeSpan(0, 0, 0);
        curDateTime = curDateTime.Date + ts;

        Date.Text = appSDate.ShamsiDate().Substring(0, 10);
        tim.EditValue = curDateTime;
    }

    private void ShowTodayEndDateTime(bool val)
    {
        if (!val) return;

        var appSDate = new SDate();
        var curDateTime = DateTime.Parse(appSDate.ShamsiDate());

        var ts = new TimeSpan(23, 59, 59);
        curDateTime = curDateTime.Date + ts;

        Date.Text = appSDate.ShamsiDate().Substring(0, 10);
        tim.EditValue = curDateTime;
    }

    private void ShowLinevalue(bool val)
    {
        if (val)
            _Line = true;
        else
            _Line = false;
    }

    private void UcDateTime_Paint(object sender, PaintEventArgs e)
    {
        if (_Line)
        {
            Control.TextEditLine(Date, e);
            Control.TextEditLine(tim, e);
        }
    }

    private void Date_EditValueChanging(object sender, ChangingEventArgs e)
    {
        if (e.NewValue == null) Date.Text = "";
    }

    private void tim_EditValueChanging(object sender, ChangingEventArgs e)
    {
        if (e.NewValue == null) tim.Text = "00:00";
    }
}