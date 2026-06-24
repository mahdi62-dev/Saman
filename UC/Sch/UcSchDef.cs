using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using LIB;
using Saman.BLL.Sch;
using Control = App.Control;

namespace Saman.UC.sch;

public partial class UcSchDef : XtraUserControl
{
    private readonly Sch _bllSchItem = new();
    private readonly int _CRUD;


    private readonly int _PersID;

    private DataTable _dtSchItem = new();

    public int _PresenceNum, _InternetNum, _TelNum;

    private string _WeekDay;

    public UcSchDef()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }

    public bool _ReadOnly { get; set; }

    private void UcSch_Load(object sender, EventArgs e)
    {
        UL.FillLookup(lueDep, "0", cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);
        UL.FillLookup(lueDr, "0", cSet.cnStringA, false, "*", "vDr", "Dr", null, null);
        MaskSet();
    }

    private void MaskSet()
    {
        txtPresenceNum.Properties.Mask.EditMask = App.UI.MaskNum;
        txtExtraNum.Properties.Mask.EditMask = App.UI.MaskNum;
        txtInternetNum.Properties.Mask.EditMask = App.UI.MaskNum;
        txtCapacity.Properties.Mask.EditMask = App.UI.MaskNum;
        txtTelNum.Properties.Mask.EditMask = App.UI.MaskNum;
        txtSchTime.Properties.Mask.EditMask = App.UI.MaskNum;
    }

    private void FormNull()
    {
        datStartDate.Text = null;
        datEndDate.Text = null;
        lueDep.Text = null;
        lueDr.Text = null;
        timStartTime.Text = "00:00";
        timEndTime.Text = "00:00";
        txtCapacity.Text = null;
        txtExtraNum.Text = null;
        txtNote.Text = null;
        txtSchTime.Text = null;

        chkListDay.UnCheckAll();
    }

    private void CheckReadOnly()
    {
        //_ReadOnly = false;
        //if (_PersLoanRecNo > 0) _ReadOnly = true;

        //datLoanAssignDate.ReadOnly = _ReadOnly;
        //datLoanAssignStartDate.ReadOnly = _ReadOnly;
        //lueDep.ReadOnly = _ReadOnly;
        //txtLoanBaseAmount.ReadOnly = _ReadOnly;
        //txtLoanFeePer.ReadOnly = _ReadOnly;
        //txtInstallCount.ReadOnly = _ReadOnly;
        //txtDelayFineAmount.ReadOnly = _ReadOnly;
        //txtNote.ReadOnly = _ReadOnly;
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(lueDep, e);
        Control.TextEditLine(datStartDate, e);
        Control.TextEditLine(datEndDate, e);
        Control.TextEditLine(lueDr, e);
        Control.TextEditLine(timStartTime, e);
        Control.TextEditLine(timEndTime, e);
        Control.TextEditLine(txtExtraNum, e);
        Control.TextEditLine(txtNote, e);
        Control.TextEditLine(txtCapacity, e);
        Control.TextEditLine(txtSchTime, e);
        Control.TextEditLine(txtPresenceNum, e);
        Control.TextEditLine(txtInternetNum, e);
        Control.TextEditLine(txtTelNum, e);
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (string.IsNullOrEmpty(datStartDate.Text))
        {
            datStartDate.ErrorText = "لطفا تاریخ را وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(datEndDate.Text))
        {
            datEndDate.ErrorText = "لطفا تاریخ را وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(timStartTime.Text) || timStartTime.Text == "00:00")
        {
            timStartTime.ErrorText = "لطفا ساعت را وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(timEndTime.Text) || timEndTime.Text == "00:00")
        {
            timEndTime.ErrorText = "لطفا ساعت را وارد نمایید";
            isValid = false;
        }

        if (lueDr.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueDr.Text))
        {
            lueDr.ErrorText = "لطفا پزشک را انتخاب نمایید";
            isValid = false;
        }

        if (lueDep.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueDep.Text))
        {
            lueDep.ErrorText = "لطفا بخش را انتخاب نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(txtPresenceNum.Text) || txtPresenceNum.EditValue.ToString() == "0")
        {
            txtPresenceNum.ErrorText = "لطفا تعداد نوبت را وارد نمایید";
            isValid = false;
        }


        //if (string.IsNullOrEmpty(_WeekDay))
        //{
        //    txtListDay.ErrorText = "لطفا روز را انتخاب نمایید";
        //    isValid = false;
        //}

        return isValid;
    }

    private void btnEsc_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
    }

    private string WeekDay()
    {
        foreach (CheckedListBoxItem item in chkListDay.CheckedItems)
            if (item.CheckState == CheckState.Checked)
                _WeekDay += item.Value + ",";

        _WeekDay = _WeekDay.Substring(0, _WeekDay.Length - 1);
        return _WeekDay;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        _dtSchItem = _bllSchItem.SchDef(0, lueDep.EditValue, lueDr.EditValue, datStartDate.Text, datEndDate.Text
            , timStartTime.Text, timEndTime.Text, WeekDay(), chkHoliday.Checked, txtPresenceNum.Text,
            txtInternetNum.Text, txtTelNum.Text, txtCapacity.Text, txtExtraNum.Text, txtSchTime.Text, null,
            GL.UserPersID);

        ucShowTextSave.ShowText(" وقت دهی با موفقیت ذخیره گردید", 9000);

        var col = grvSch.Columns.ColumnByFieldName("RecNo");
        col.UnboundType = UnboundColumnType.Integer;
        grvSch.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        grdSch.DataSource = _dtSchItem;

        FormNull();

        ActiveControl = datStartDate;
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
        //Int32[] rows = grvServiceReg.GetSelectedRows();
        //foreach (Int32 i in rows)
        //{
        //    grvServiceReg.GetDataRow(i);

        //    int NO = Convert.ToInt32(grvServiceReg.GetDataRow(i)["RecNo"].ToString());

        //    DataRow row = _dtServiceReg.Select("RecNo=" + NO)[0];

        //    if (Convert.ToInt32(row["SRID"]) == 0)
        //        row.Delete();
        //    else
        //        row["CRUD"] = 4;
        //}
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        //_IsEditRow = true;
        //DataRow row = grvServiceReg.GetDataRow(grvServiceReg.FocusedRowHandle);

        //_RecNoServiceReg = Convert.ToInt32(row["RecNo"]);
        //lueService.EditValue = row["ServiceID"];
        //txtServiceNum.Text = row["Num"].ToString();
        //lueDrFunctor.Text = row["DrName"].ToString();
    }

    private void btnRefreshSch_Click(object sender, EventArgs e)
    {
        SchDefLoad();
    }

    private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }

    private void datStartDate_EditValueChanged(object sender, EventArgs e)
    {
        SchDefLoad();
    }

    private void datEndDate_EditValueChanged(object sender, EventArgs e)
    {
        SchDefLoad();
    }

    private void lueDep_EditValueChanged(object sender, EventArgs e)
    {
        SchDefLoad();
    }

    private void lueDr_EditValueChanged(object sender, EventArgs e)
    {
        SchDefLoad();
    }

    private void SchDefLoad()
    {
        _dtSchItem = _bllSchItem.SchDef(1, lueDep.EditValue, lueDr.EditValue, datStartDate.Text, datEndDate.Text
            , null, null, null, null, null, null, null, null, null, null, null, null);


        var col = grvSch.Columns.ColumnByFieldName("RecNo");
        col.UnboundType = UnboundColumnType.Integer;
        grvSch.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        grdSch.DataSource = _dtSchItem;
        lblTitle2.Text = lueDr.Text;
    }

    private void txtPresenceNum_EditValueChanged(object sender, EventArgs e)
    {
        _PresenceNum = Convert.ToInt32(txtPresenceNum.Text);
        CalcCapacity();
    }

    private void txtInternetNum_EditValueChanged(object sender, EventArgs e)
    {
        _InternetNum = Convert.ToInt32(txtInternetNum.Text);
        CalcCapacity();
    }

    private void txtTelNum_EditValueChanged(object sender, EventArgs e)
    {
        _TelNum = Convert.ToInt32(txtTelNum.Text);
        CalcCapacity();
    }

    private void txtPresenceNum_Leave(object sender, EventArgs e)
    {
        CalcCapacity();
    }

    private void txtInternetNum_Leave(object sender, EventArgs e)
    {
        CalcCapacity();
    }

    private void txtTelNum_Leave(object sender, EventArgs e)
    {
        CalcCapacity();
    }

    private void CalcCapacity()
    {
        txtCapacity.EditValue = _PresenceNum + _InternetNum + _TelNum;
    }
}