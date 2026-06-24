using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using LIB;
using Saman.BLL.Payroll;
using Control = App.Control;
using Timex = Saman.BLL.Timex.Timex;

namespace Saman.UC;

public partial class UcTimexDayOff : XtraUserControl
{
    private Timex _bllDayOffLoad = new();

    private int _CRUD;
    private DataTable _dtDayOff = new();

    private DataTable _dtDayOffLoad;

    public UcTimexDayOff()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }
    public int _Year { get; set; }
    public int _Mon { get; set; }
    public int _IsSign { get; set; }
    public int _PersID { get; set; }

    private void UcTimexDayOff_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        ActiveControl = luePers;

        UL.FillLookup(luePers, "0", cSet.cnStringERP, false, "*", "vPersonnelFull", "PersonnelFull", null, null);
        UL.FillLookup(lueDayOffType, "1", cSet.cnStringERP, false, "ID,Name", "vADR", "DayOffType",
            "ADName='DayOffType'", null);

        var bllOprDataTable = new DT();
        bllOprDataTable.CreateTableDayOff(_dtDayOff);

        var bllTimexDAyOff = new BLL.Payroll.Timex();
        _dtDayOff = bllTimexDAyOff.GetTimexDayOff(_Year, _Mon);
        grdTimexDayOff.DataSource = _dtDayOff;
        var col = grvTimexDayOff.Columns.ColumnByFieldName("RecNo");
        // col.FieldName = "RecNo";
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvTimexDayOff.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        lblTitle1.Text = "سال و ماه جاری" + "   " + _Year + "/" + "0" + _Mon;

        if (_IsSign == 1)
        {
            btnDayOffSave.Enabled = false;
            btnDayOffAdd.Enabled = false;
            btnDayOffDel.Enabled = false;
        }

        ShortKey();
    }

    private void ShortKey()
    {
        msiSave.Click += btnDayOffSave_Click;
        msiClose.Click += btnCancel_Click;

        panMain.Focus();
    }

    private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }

    private bool CheckValidation()
    {
        var isValid = true;


        if (luePers.EditValue.ToString() == "0" && string.IsNullOrEmpty(luePers.Text))
        {
            luePers.ErrorText = "پرسنل را انتخاب نمائید";
            isValid = false;
        }

        if (lueDayOffType.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueDayOffType.Text))
        {
            lueDayOffType.ErrorText = "نوع مرخصی را وارد نمائید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(datDayOffStart.Text))
        {
            datDayOffStart.ErrorText = "لطفا تاریخ را به صورت صحیح وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(datDayOffEnd.Text))
        {
            datDayOffEnd.ErrorText = "لطفا تاریخ را به صورت صحیح وارد نمایید";
            isValid = false;
        }

        int startmon, endmon;
        startmon = Convert.ToInt32(datDayOffStart.Text.Substring(5, 2));
        endmon = Convert.ToInt32(datDayOffEnd.Text.Substring(5, 2));

        if (startmon != _Mon)
        {
            datDayOffStart.ErrorText = "ماه جاری را انتخاب نمائید";
            isValid = false;
        }

        if (endmon != _Mon)
        {
            datDayOffEnd.ErrorText = "ماه جاری را انتخاب نمائید";
            isValid = false;
        }


        return isValid;
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(lueDayOffType, e);
        Control.TextEditLine(luePers, e);
        Control.TextEditLine(datDayOffEnd, e);
        Control.TextEditLine(datDayOffStart, e);
    }

    private void FormNull()
    {
        datDayOffStart.Text = null;
        datDayOffEnd.Text = null;
        luePers.EditValue = 0;
        txtTotalDayOff.Text = string.Empty;
        txtUsedDayOff.Text = string.Empty;
        txtRemainDayOff.Text = string.Empty;
    }

    private void btnDayOffAdd_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        int startnew, endnew;

        startnew = Convert.ToInt32(datDayOffStart.Text.Substring(8, 2));
        endnew = Convert.ToInt32(datDayOffEnd.Text.Substring(8, 2));

        if (startnew > endnew)
        {
            datDayOffEnd.ErrorText = "تاریخ از شروع مرخصی کوچکتر است";
            return;
        }


        {
            var rowDayOff = _dtDayOff.NewRow();

            var persID = Convert.ToInt32(luePers.EditValue);
            var DayOffType = Convert.ToInt32(lueDayOffType.EditValue);

            int start, end, Val;

            start = Convert.ToInt32(datDayOffStart.Text.Substring(8, 2));
            end = Convert.ToInt32(datDayOffEnd.Text.Substring(8, 2));
            Val = end - start + 1;

            rowDayOff["CRUD"] = 0;
            rowDayOff["Year"] = _Year;
            rowDayOff["Mon"] = _Mon;
            rowDayOff["PersID"] = persID;
            rowDayOff["PersName"] = luePers.Properties.GetDisplayText(luePers.EditValue);

            rowDayOff["DayOffType"] = DayOffType;
            rowDayOff["DayOffTypeName"] = lueDayOffType.Properties.GetDisplayText(lueDayOffType.EditValue);

            rowDayOff["StartDate"] = datDayOffStart.Text.Substring(0, 10);
            rowDayOff["EndDate"] = datDayOffEnd.Text.Substring(0, 10);
            rowDayOff["Val"] = Val;


            _dtDayOff.Rows.Add(rowDayOff);
            grdTimexDayOff.DataSource = _dtDayOff;
        }

        FormNull();

        ActiveControl = luePers;
    }

    private void btnDayOffDel_Click(object sender, EventArgs e)
    {
        var row = grvTimexDayOff.GetFocusedDataRow();
        if (row != null)
        {
            var startdate = Convert.ToDateTime(row["StartDate"].ToString());
            var filter = @"PersID=" + row["PersID"] + " AND DayOffType=" + row["DayOffType"];

            var crud = Convert.ToByte(_dtDayOff.Select(filter)[0]["CRUD"]);

            if (crud == 0) _dtDayOff.Select(filter)[0].Delete();

            if (crud == 2) _dtDayOff.Select(filter)[0]["CRUD"] = 4;
        }

        if (_dtDayOff.Select("CRUD<4").Length > 0)
            grdTimexDayOff.DataSource = _dtDayOff.Select("CRUD<4").CopyToDataTable();
    }

    private void btnDayOffSave_Click(object sender, EventArgs e)
    {
        var bllDayOff = new BLL.Payroll.Timex();

        if (_dtDayOff.Rows.Count > 0)
            foreach (DataRow row in _dtDayOff.Rows)
            {
                int CRUD = Convert.ToByte(row["CRUD"]);


                bllDayOff.DayOffSave(CRUD, _Year, _Mon, row["PersID"], row["DayOffType"],
                    row["StartDate"], row["EndDate"], row["Val"], GL.UserPersID);

                if (CRUD == 0) row["CRUD"] = 2;
            }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
    }

    private void luePers_EditValueChanged(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(luePers.EditValue) == 0)
        //{
        //    return;
        //}

        //_PersID = Convert.ToInt32(luePers.EditValue);
        //FormNull();
        //PersDayOffLoad(_PersID);
    }

    // private void PersDayOffLoad(int PersID)
    //{
    //    _CRUD = 1;
    //    _dtDayOffLoad = _bllDayOffLoad.DayOffLoad(_CRUD, _PersID);

    //    txtTotalDayOff.Text = _dtDayOffLoad.Rows[0]["TotalDayOff"].ToString();
    //    txtUsedDayOff.Text = _dtDayOffLoad.Rows[0]["UsedDayOff"].ToString();
    //    txtRemainDayOff.Text = _dtDayOffLoad.Rows[0]["RemainDayOff"].ToString();

    //}
}