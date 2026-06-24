using System;
using System.Data;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.ServiceReg;
using Saman.Properties;
using Control = App.Control;

namespace Saman.UC.Enc;

public partial class UcEncDepRec : XtraUserControl
{
    private readonly DataTable _dtServiceReg = new();

    private readonly bool _IsEditRow;
    private readonly int _RecNoServiceReg;

    private readonly DataRowView _rowCurServie;
    private DataTable _dtEncPage = new();
    public bool _ReadOnly;

    private DataRow[] _rowEncCur;

    public UcEncDepRec()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }

    public int _EncID { get; set; }
    public int _PageID { get; set; }
    private int _PageListRecNo { get; set; }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(timEncFinalAcceptanceTime, e);
        Control.TextEditLine(datEncFinalAcceptanceDate, e);
        Control.LookupEditLine(lueNurseDay, e);
        Control.LookupEditLine(lueNurseNight, e);
    }

    private void UcEncDepRec_Load(object sender, EventArgs e)
    {
        btnServiceRegSave.Enabled = !_ReadOnly;

        UL.LangChange(2);
        var appSDate = new SDate();
        var curDateTime = DateTime.Parse(appSDate.ShamsiDate());


        datEncFinalAcceptanceDate.DateTime = curDateTime;
        timEncFinalAcceptanceTime.Time = curDateTime;

        var bllServceReg = new DT();
        bllServceReg.CreateTableServiceReg(_dtServiceReg);


        UL.FillLookup(lueNurseDay, "0", cSet.cnStringA, false, "PersID, Name", "vPersonnel", "Personnel",
            "PersType=1", null);
        UL.FillLookup(lueNurseNight, "0", cSet.cnStringA, false, "PersID, Name", "vPersonnel", "Personnel",
            "PersType=1", null);

        _rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);
        if (_rowEncCur.Length > 0)
        {
            _EncID = Convert.ToInt32(_rowEncCur[0]["EncId"]);
            lblTitle.Text = _rowEncCur[0]["FirstName"] + " " + _rowEncCur[0]["LastName"];
            txtEncID.Text = _rowEncCur[0]["EncId"].ToString();
            lblTitle2.Text = _rowEncCur[0]["Age"] + @" سال - فرزند " + _rowEncCur[0]["FatherName"];
            lblTitleEncDate.Text = _rowEncCur[0]["StartDate"].ToString().Replace(' ', ' ');
            lblEncDate.Text = _rowEncCur[0]["StartDate"].ToString().Replace(' ', ' ');
            picGender.Image = Convert.ToByte(_rowEncCur[0]["Gender"]) == 1 ? Resources.male : Resources.female;
            lblDrName.Text = _rowEncCur[0]["DrName"].ToString();
            lblDepName.Text = _rowEncCur[0]["DepName"].ToString();
        }

        EncPageLoad();
    }

    private void FormNull()
    {
    }


    private void EncPageLoad()
    {
        var bllPage = new BLL.Page.Page();
        _dtEncPage = bllPage.EncPageLoad(_EncID, _ModID, _DepID, _PageID);

        _PageListRecNo = _dtEncPage.Rows.Count;
    }


    private bool CheckValidation()
    {
        var isValid = true;

        if (string.IsNullOrEmpty(txtEncID.Text))
        {
            txtEncID.ErrorText = "لطفا کد را وارد نمایید";

            isValid = false;
        }

        return isValid;
    }


    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
    }


    private void btnCancel_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucEncCur = new UcEncCur
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        ucEncCur._ModID = _ModID;
        panUC.Controls.Add(ucEncCur);
    }
}