using App;
using DevExpress.Data;
using DevExpress.Text.Interop;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DocumentFormat.OpenXml.Drawing.Charts;
using Saman.APP.SAC;
using Saman.BLL.Enc;
using Saman.BLL.Pet;
using Saman.Form.DocReport;
using Saman.Form.Radio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Control = App.Control;

namespace Saman.UC.Page;

public partial class UcPageAnsPet : XtraUserControl
{
    private readonly EncInfo _bllEncInfo = new();
    private readonly SACPermission sacPermission = new();

    private System.Data.DataTable _dtEncList;


    public int _EncID, _PageID, _Country, _EncService, _AnsStatus, _Gender, _IsSign;

    public int _EncStatus = 0;

    public string _FullName,
        _Age,
        _PageDate,
        _NationalCode,
        _FirstName,
        _LastName,
        _GenderName,
        _ChiefComplaint,
        _QuestionName,
        _ChiefComplaintName,
        _RefDrName,
        _Height,
        _Weight;

    private DataRow _rowEncSelect;


    public UcPageAnsPet()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public bool _ReadOnly { get; set; }
    public bool _CheckPermission { get; set; }


    public byte _EncType { get; set; }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtFullName, e);
        Control.TextEditLine(timStartTime, e);
        Control.TextEditLine(timEndTime, e);
        Control.TextEditLine(datDate1, e);
        Control.TextEditLine(datDate2, e);
    }

    private void grvPageAns_RowCellClick(object sender, RowCellClickEventArgs e)
    {
        if (e.Column.Name == "gcReportStatus" || e.Column.Name == "gcReportDate")
            if (_AnsStatus == 20)
            {
                if (_CheckPermission)
                {
                    var strMessage = "به این قسمت دسترسی ندارید";
                    var sui = new SUI();
                    var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
                    return;
                }

                var appSDate = new SDate();
                var frmRicReport = new FrmRichReport
                {
                    _curDateTime = appSDate.ShamsiDate(),
                    _EncID = _EncID,
                    _GenderFa = _GenderName,
                    _Age = _Age,
                    _PageDate = _PageDate,
                    _ChiefComplaint = _ChiefComplaint,
                    _RefDrName = _RefDrName,
                    _PageID = _PageID,
                    _PageDateEn = appSDate.S2MDateMDYSlash(_PageDate).Substring(0, 10),
                    _FullName = _FullName,
                    _IsSign = _IsSign,
                    _Height = _Height,
                    _Weight =_Weight
                };
                frmRicReport.Tag = _PageID;
                ShowForm(frmRicReport);
            }
    }

    private void CheckPermission()
    {
        _CheckPermission = sacPermission.CheckPermission(_ModID, 3000081);
    }

    private void UcPageAnsPet_Load(object sender, EventArgs e)
    {
        CheckPermission();

        var appSDate = new SDate();

        datDate2.DateTime = datDate1.DateTime = DateTime.Parse(appSDate.ShamsiDate());
        timEndTime.Time = DateTime.Now;
        txtFullName.Properties.Mask.EditMask = App.UI.MaskPersian;

        ShortKey();
    }

    private void ShortKey()
    {
        //var menu1 = new MenuStrip();
        //var item1 = new ToolStripMenuItem();
        //item1.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W;
        //item1.Click += btnCancel_Click;
        //menu1.Items.Add(item1);
        //menu1.Visible = false;
        //this.Controls.Add(menu1);

        msiSearch.Click += btnSearch_Click;
        msiClose.Click += btnCancel_Click;

        panMain.Focus();
    }


    public void PageAnsSeach()
    {
        var bllPageAns = new PageAnsPet();

        var col = grvPageAns.Columns.ColumnByFieldName("RecNo");
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvPageAns.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        _dtEncList = bllPageAns.PageAnsSearch(datDate1.Text + " " + timStartTime.Text,
            datDate2.Text + " " + timEndTime.Text, txtFullName.Text);
        grdPageAns.DataSource = _dtEncList;
    }

    private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }


    private void btnCancel_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucEncCur = new UcEncCur
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucEncCur);
    }


    private void grvPageAns_Click(object sender, EventArgs e)
    {
        if (grvPageAns.FocusedRowHandle < 0) return;

        _rowEncSelect = grvPageAns.GetDataRow(grvPageAns.FocusedRowHandle);
        lblTitle1.Text = _rowEncSelect["FullName"].ToString();
        lblTitle3.Text = "تاریخ پذیرش " + _rowEncSelect["StartDate"].ToString().Replace(' ', ' ');

        PageSelect();
    }

    private void PageSelect()
    {
        if (grvPageAns.FocusedRowHandle < 0) return;

        DataRow rowSelect;

        rowSelect = grvPageAns.GetDataRow(grvPageAns.FocusedRowHandle);

        _EncID = Convert.ToInt32(rowSelect["EncID"]);
        _PageID = Convert.ToInt32(rowSelect["PageID"]);
        _FullName = rowSelect["Fullname"].ToString();
        _NationalCode = rowSelect["NationalCode"].ToString();
        _FirstName = rowSelect["FirstName"].ToString();
        _LastName = rowSelect["LastName"].ToString();
        _GenderName = rowSelect["GenderName"].ToString();
        _Gender = Convert.ToInt32(rowSelect["Gender"].ToString());
        _Country = Convert.ToInt32(rowSelect["Country"].ToString());
        _Age = rowSelect["Age"].ToString();
        _ChiefComplaint = rowSelect["ChiefComplaint"].ToString();
        _IsSign = Convert.ToInt32(rowSelect["IsSign"]);
        _RefDrName = rowSelect["RefDrName"].ToString();
        _EncService = Convert.ToInt32(rowSelect["EncService"].ToString());
        _PageDate = rowSelect["PageDate"].ToString();
        _AnsStatus = Convert.ToInt32(rowSelect["AnsStatus"].ToString());
        _Height = (rowSelect["Height"].ToString());
        _Weight = (rowSelect["Weight"].ToString());

    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        if (_dtEncList != null) _dtEncList.Clear();

        PageAnsSeach();
    }

    public void grvPageAns_DoubleClick(object sender, EventArgs e)
    {
        if (_CheckPermission)
        {
            var strMessage = "به این قسمت دسترسی ندارید";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
            return;
        }

        PageSelect();

        if (grvPageAns.FocusedRowHandle < 0) return;

        if (_ModID == 30)
        {
            var frmPetData = new FrmPetData
            {
                _EncID = _EncID,
                _PageID = _PageID,
                _FirstName = _FirstName,
                _LastName = _LastName,
                _AnsStatus = _AnsStatus,
                _ChiefComplaint = Convert.ToInt32(_ChiefComplaint)
            };


            var result = frmPetData.ShowDialog();

            if (result == DialogResult.Yes)
            {
                string GenderEn = null;
                if (_Gender == 1) GenderEn = "Male";

                if (_Gender == 2) GenderEn = "Female";

                var templateID = 0;

                if (_AnsStatus == 0) templateID = _EncService;

                var appSDate = new SDate();
                var frmRicReport = new FrmRichReport
                {
                    _curDateTime = appSDate.ShamsiDate(),
                    _TemplateID = templateID,
                    _EncID = _EncID,
                    _PageID = _PageID,
                    _FullName = _FullName,
                    _GenderEn = GenderEn,
                    _GenderFa = _GenderName,
                    _Age = _Age,
                    _PageDate = _PageDate,
                    _PageDateEn = appSDate.S2MDateMDYSlash(_PageDate).Substring(0, 10),
                    _ChiefComplaint = _ChiefComplaint,
                    _ChiefComplaintName = frmPetData._ChiefComplaintName,
                    _QuestionName = frmPetData._QuestionName,
                    _RefDrName = _RefDrName,
                    _DrReportSignTextFa1 = frmPetData._DrReportSignTextFa1,
                    _DrReportSignTextFa2 = frmPetData._DrReportSignTextFa2,
                    _DrReportSignTextEn1 = frmPetData._DrReportSignTextEn1,
                    _DrReportSignTextEn2 = frmPetData._DrReportSignTextEn2,
                    _PersSignTextFa = frmPetData._PersSignTextFa,
                    _PersSignTextEn = frmPetData._PersSignTextEn,
                    _IsSign = _IsSign,
                    _Height = _Height,
                    _Weight =_Weight
                };

                frmRicReport.Tag = _PageID;

                ShowForm(frmRicReport);
            }
        }
    }

    private void ShowForm(System.Windows.Forms.Form frm)
    {
        var isOpenFrom = IsFormOpenByTag(_PageID.ToString());
        var sui = new SUI();

        if (isOpenFrom)
        {
            sui.ShowWarningMessageForm("پیام", "ریپورت بیمار فعال می باشد", true);
            return;
        }


        frm.MdiParent = System.Windows.Forms.Form.ActiveForm;
        frm.Show();
    }

    private void grvPageAns_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        var view = sender as GridView;

        var AnsStatus = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "AnsStatus"));
        if (AnsStatus == 20) e.Appearance.BackColor = Color.LightGreen;
    }

    public bool IsFormOpenByTag(string tag)
    {
        var isFormOpen = false;
        foreach (System.Windows.Forms.Form frm in Application.OpenForms)
            if (frm.Tag == null)
            {
                isFormOpen = false;
            }

            else if (frm.Tag.ToString() == tag)
            {
                isFormOpen = true;
                break;
            }

        return isFormOpen;
    }
}