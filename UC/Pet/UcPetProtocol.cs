using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using App;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Saman.APP.SAC;
using Saman.BLL.Enc;
using Saman.BLL.Pet;
using Saman.Form.Radio;
using Control = App.Control;

namespace Saman.UC.Pet;

public partial class UcPetProtocol : XtraUserControl
{
    private readonly EncInfo _bllEncInfo = new();
    private readonly SACPermission sacPermission = new();


    private DataTable _dtPetPageLoad;

    public int _EncID, _PageID, _AnsStatus, _Gender, _Height, _Weight;

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
        _RefDrName;

    private bool _HasProtocolData, _HasCyclotronData, _HasScanData, _IsCancel;


    private DataRow _rowEncSelect;

    public UcPetProtocol()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public bool _ReadOnly { get; set; }
    public bool _CheckPermission { get; set; }
    public bool _refresh { get; set; }

    public string _Date1 { get; set; }
    public string _Date2 { get; set; }


    public byte _EncType { get; set; }


    private void UcPetProtocol_Enter(object sender, EventArgs e)
    {
        if (_refresh) btnSearch_Click(null, null);
    }


    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        //Control.TextEditLine(txtFullName, e);
        Control.TextEditLine(timStartTime, e);
        Control.TextEditLine(timEndTime, e);
        Control.TextEditLine(datDate1, e);
        Control.TextEditLine(datDate2, e);
    }

    private void grvPageAns_RowCellClick(object sender, RowCellClickEventArgs e)
    {
    }

    private void UcPetProtocol_Load(object sender, EventArgs e)
    {
        CheckPermission();

        timStartTime.Properties.EditMask = "HH:mm";
        timEndTime.Properties.EditMask = "HH:mm";

        var appSDate = new SDate();
        datDate2.DateTime = datDate1.DateTime = DateTime.Parse(appSDate.ShamsiDate());
        timEndTime.EditValue = "23:59";

        txtFullName.Properties.Mask.EditMask = App.UI.MaskPersian;

        ShortKey();
    }

    private void CheckPermission()
    {
        _CheckPermission = sacPermission.CheckPermission(_ModID, 300011);
    }

    private void ShortKey()
    {
        msiSearch.Click += btnSearch_Click;
        msiClose.Click += btnCancel_Click;

        panMain.Focus();
    }


    public void PetPageLoad()
    {
        var bllEncPetLoad = new PetProtocol();

        var col = grvPetRrotocol.Columns.ColumnByFieldName("RecNo");
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvPetRrotocol.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        _Date1 = datDate1.Text + " " + timStartTime.Text;
        _Date2 = datDate2.Text + " " + timEndTime.Text;

        _dtPetPageLoad = bllEncPetLoad.PetPageLoad(_DepID, _Date1, _Date2);
        grdPetRrotocol.DataSource = _dtPetPageLoad;
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


    private void grvPetRrotocol_Click(object sender, EventArgs e)
    {
        if (grvPetRrotocol.FocusedRowHandle < 0) return;

        _rowEncSelect = grvPetRrotocol.GetDataRow(grvPetRrotocol.FocusedRowHandle);
        lblTitle1.Text = _rowEncSelect["FullName"].ToString();
        lblTitle3.Text = "تاریخ پذیرش " + _rowEncSelect["StartDate"].ToString().Replace(' ', ' ');

        PageSelect();
    }

    private void PageSelect()
    {
        if (grvPetRrotocol.FocusedRowHandle < 0) return;

        DataRow rowSelect;

        rowSelect = grvPetRrotocol.GetDataRow(grvPetRrotocol.FocusedRowHandle);

        _EncID = Convert.ToInt32(rowSelect["EncID"]);
        _PageID = Convert.ToInt32(rowSelect["PageID"]);
        _FullName = rowSelect["Fullname"].ToString();
        //_NationalCode = rowSelect["NationalCode"].ToString();
        _FirstName = rowSelect["FirstName"].ToString();
        _LastName = rowSelect["LastName"].ToString();
        //_GenderName = rowSelect["GenderName"].ToString();
        //_Gender = Convert.ToInt32(rowSelect["Gender"].ToString());
        //_Country = Convert.ToInt32(rowSelect["Country"].ToString());
        _Age = rowSelect["Age"].ToString();
        //_ChiefComplaint = rowSelect["ChiefComplaint"].ToString();
        //_RefDrName = rowSelect["RefDrName"].ToString();
        //_EncService = Convert.ToInt32(rowSelect["EncService"].ToString());
        //_PageDate = rowSelect["PageDate"].ToString();
        _Height = Convert.ToInt32(rowSelect["Height"].ToString());
        _Weight = Convert.ToInt32(rowSelect["Weight"].ToString());
    }

    public void btnSearch_Click(object sender, EventArgs e)
    {
        PetPageLoad();
    }

    public void grvPetRrotocol_DoubleClick(object sender, EventArgs e)
    {
        //if (_CheckPermission == true) 
        //{
        //    string strMessage =  "به این قسمت دسترسی ندارید";
        //    SUI sui = new SUI();
        //    var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage,true);
        //    return;
        //}

        PageSelect();

        if (grvPetRrotocol.FocusedRowHandle < 0) return;

        var rowHandle = grvPetRrotocol.GetFocusedDataSourceRowIndex();


        if (_ModID == 30)
        {
            var frmPetProtocolData = new frmPetProtocolData
            {
                _ModID = _ModID,
                _EncID = _EncID,
                _PageID = _PageID,
                _FirstName = _FirstName,
                _LastName = _LastName,
                _AnsStatus = _AnsStatus,
                _Age = _Age,
                _ChiefComplaint = Convert.ToInt32(_ChiefComplaint),
                _Height = _Height,
                _Weight = _Weight
            };

            var result = frmPetProtocolData.ShowDialog();
        }

        PetPageLoad();

        grvPetRrotocol.SelectRow(rowHandle);
        grvPetRrotocol.FocusedRowHandle = rowHandle;
    }

    private void grvPageAns_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        //if (grvPetRrotocol.FocusedRowHandle == e.RowHandle)
        //    return;

        var view = sender as GridView;

        _HasProtocolData = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "HasProtocolData"));
        _HasCyclotronData = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "HasCyclotronData"));
        _HasScanData = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "HasScanData"));
        _IsCancel = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsCancel"));


        if (_HasProtocolData && !_HasCyclotronData && !_HasScanData) e.Appearance.BackColor = Color.LightYellow;

        if (_HasCyclotronData && !_HasScanData) e.Appearance.BackColor = Color.Yellow;

        if (_HasScanData) e.Appearance.BackColor = Color.LightGreen;

        if (_IsCancel) e.Appearance.BackColor = Color.LightCoral;
    }
}