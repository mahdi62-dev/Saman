using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.Calendar;
using Saman.BLL.Timex;
using Control = App.Control;

namespace Saman.UC;

public partial class UcTimexShiftPers : XtraUserControl
{
    private readonly Timex _bllTimexPersShift = new();


    private DataTable _dtTimexPersShift;


    public UcTimexShiftPers()
    {
        InitializeComponent();
    }

    public int _Year { get; set; }
    public int _Mon { get; set; }
    public int _IsSign { get; set; }
    public byte _ModID { get; set; }
    public int _PersID { get; set; }
    public int _DepID { get; set; }

    private void UcTimexShiftPers_Load(object sender, EventArgs e)
    {
        ShortKey();
        CheckPermission();
        UL.FillLookup(lueDep, "0", cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);

        UL.FillLookupRep(repLueDep, "0", cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);
        UL.FillLookupRep(repLueTimexShift, "0", cSet.cnStringA, false, "ID,Name,Name2", "vADR", "TimexShift",
            "ADName='TimexShift'", null);

        GridHeaderLoad();
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.LookupEditLine(lueDep, e);
    }

    private void GridHeaderLoad()
    {
        var bllCalendar = new Calendar();
        var dtCalendar = bllCalendar.CalendarData(1, _Year, _Mon);

        int c = 0, WeekDay, IsHoliday;
        string weekDayName, day, header;


        for (var i = 0; i < dtCalendar.Rows.Count; i++)
        {
            c = i + 3;

            weekDayName = dtCalendar.Rows[i]["WeekDayName"].ToString();
            WeekDay = Convert.ToInt32(dtCalendar.Rows[i]["WeekDay"]);
            IsHoliday = Convert.ToInt32(dtCalendar.Rows[i]["IsHoliday"]);

            day = dtCalendar.Rows[i]["day"].ToString();
            header = weekDayName + "\r\n" + day;
            grvTimexShiftPers.Columns[c].Caption = header;
            grvTimexShiftPers.Columns[c].Visible = true;

            if (WeekDay == 6 || IsHoliday == 1)
            {
                grvTimexShiftPers.Columns[c].AppearanceHeader.BackColor = Color.PaleVioletRed;
                grvTimexShiftPers.Columns[c].AppearanceCell.BackColor = Color.PaleVioletRed;
                grvTimexShiftPers.Columns[c].AppearanceCell.ForeColor = Color.White;
            }
        }
    }

    private void ShortKey()
    {
        //msiEditEmpAction.Click += btnEditEmpAction_Click;
        //msiNewEmpAction.Click += btnNewEmpAction_Click;

        //panMain.Focus();
    }

    private void CheckPermission()
    {
        //msiEditEmpAction.Click += btnEditEmpAction_Click;
        //msiNewEmpAction.Click += btnNewEmpAction_Click;

        //panMain.Focus();
    }

    private void lueDep_EditValueChanged(object sender, EventArgs e)
    {
        _DepID = Convert.ToInt32(lueDep.EditValue);
        PersShiftLoad(_DepID);
    }

    private void PersShiftLoad(int DepID)
    {
        var crud = 1;
        _dtTimexPersShift = _bllTimexPersShift.TimexPersShiftLoad(crud, _Year, _Mon, _DepID, null, GL.UserPersID);
        grdTimexShiftPers.DataSource = _dtTimexPersShift;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var crud = 0;
        int depId, persId;


        for (var i = 0; i < grvTimexShiftPers.RowCount; i++)
        {
            depId = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "DepId"));
            persId = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "PersId"));

            var D1 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D1"));
            var D2 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D2"));
            var D3 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D3"));
            var D4 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D4"));
            var D5 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D5"));
            var D6 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D6"));
            var D7 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D7"));
            var D8 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D8"));
            var D9 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D9"));
            var D10 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D10"));
            var D11 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D11"));
            var D12 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D12"));
            var D13 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D13"));
            var D14 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D14"));
            var D15 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D15"));
            var D16 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D16"));
            var D17 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D17"));
            var D18 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D18"));
            var D19 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D19"));
            var D20 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D20"));
            var D21 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D21"));
            var D22 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D22"));
            var D23 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D23"));
            var D24 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D24"));
            var D25 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D25"));
            var D26 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D26"));
            var D27 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D27"));
            var D28 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D28"));
            var D29 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D29"));
            var D30 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D30"));
            var D31 = Convert.ToInt32(grvTimexShiftPers.GetRowCellValue(i, "D31"));


            _dtTimexPersShift = _bllTimexPersShift.TimexPersShiftSave(crud, _Year, _Mon, depId, persId,
                D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11,
                D12, D13, D14, D15, D16, D17, D18, D19, D20, D21,
                D22, D23, D24, D25, D26, D27, D28, D29, D30, D31,
                GL.UserPersID);
        }

        PersShiftLoad(_DepID);
    }

    private void repLueTimexShift_EditValueChanged(object sender, EventArgs e)
    {
        grvTimexShiftPers.PostEditor();
    }

    private void repLueDep_EditValueChanged(object sender, EventArgs e)
    {
        grvTimexShiftPers.PostEditor();
    }
}