using System;
using System.Drawing;
using System.Windows.Forms;
using App;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using FarsiLibrary.Win.Enums;
using Control = App.Control;

namespace Saman.UC.UI;

public partial class UcDate : XtraUserControl
{
    private bool _Line;

    private string _Text;


    public bool YearOnlyInput = false;

    public UcDate()
    {
        InitializeComponent();
    }

    public bool ShowNow
    {
        set => ShowNowDateTime(value);
    }

    public bool ShowFirstDayOfMonth
    {
        set => ShowFirstDayOfMonthDateTime(value);
    }

    //public bool ShowTodayStart { set => ShowTodayStartDateTime(value); }
    //public bool ShowTodayEnd { set => ShowTodayEndDateTime(value); }
    public bool ShowLine
    {
        set => ShowLinevalue(value);
    }

    public string Text
    {
        get => GetDateTime();

        set => SetDateTime(value);
    }

    //public Size Sizedat
    //{
    //    get => dat.Size;
    //    set => dat.Size = value;
    //}

    public Color BackColorFocused
    {
        set => dat.Properties.AppearanceFocused.BackColor = value;
    }

    //public bool Enabled
    //{
    //    set => dat.Enabled = value;
    //}
    public bool ReadOnly
    {
        set => dat.ReadOnly = value;
    }

    private void UcDateTime_Load(object sender, EventArgs e)
    {
        dat.Theme = ThemeTypes.Office2000;
    }

    private string GetDateTime()
    {
        return dat.Text;
    }


    private void SetDateTime(string val)
    {
        if (string.IsNullOrEmpty(val))
            dat.Text = val;
        else if (val.Length >= 10) dat.Text = val.Substring(0, 10);
    }

    private void ShowNowDateTime(bool val)
    {
        if (!val) return;

        var appSDate = new SDate();
        dat.Text = appSDate.ShamsiDate().Substring(0, 10);
    }

    private void ShowFirstDayOfMonthDateTime(bool val)
    {
        if (!val) return;

        var appSDate = new SDate();
        var strCurDateTime = appSDate.ShamsiDate();

        strCurDateTime = strCurDateTime.Substring(0, 8) + "01 00:00";
        dat.Text = strCurDateTime.Substring(0, 10);
    }


    private void ShowTodayStartDateTime(bool val)
    {
        if (!val) return;

        var appSDate = new SDate();
        var curDateTime = DateTime.Parse(appSDate.ShamsiDate());

        var ts = new TimeSpan(0, 0, 0);
        curDateTime = curDateTime.Date + ts;

        dat.Text = curDateTime.ToShortDateString();
    }

    private void ShowTodayEndDateTime(bool val)
    {
        if (!val) return;

        var appSDate = new SDate();
        var curDateTime = DateTime.Parse(appSDate.ShamsiDate());

        var ts = new TimeSpan(23, 59, 59);
        curDateTime = curDateTime.Date + ts;

        dat.Text = curDateTime.ToShortDateString();
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
        if (_Line) Control.TextEditLine(dat, e);
    }

    private void dat_EditValueChanging(object sender, ChangingEventArgs e)
    {
        if (e.NewValue == null)
        {
            dat.Text = "";
            return;
        }

        if (e.NewValue != null)
            if (string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                dat.Text = null;
                return;
            }

        if (YearOnlyInput && e.NewValue.ToString().Length == 4)
        {
            e.NewValue = e.NewValue + "/01/01";
            return;
        }

        if (e.OldValue != null)
            if (e.NewValue.ToString().Length < e.OldValue.ToString().Length)
                return;

        if (!YearOnlyInput && e.NewValue.ToString().Length == 4)
        {
            e.NewValue = e.NewValue + "/";
            return;
        }

        if (!YearOnlyInput && e.NewValue.ToString().Length == 7)
        {
            e.NewValue = e.NewValue + "/";
            return;
        }

        _Text = dat.Text;
    }
}