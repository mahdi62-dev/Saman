using System;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.Enc;
using Saman.BLL.Ward;
using Control = App.Control;

namespace Saman.Form.Ward;

public partial class FrmEncInfo : XtraForm
{
    private readonly EncInfo _bllEncInfo = new();

    private readonly WardRecord _bllWardRecord = new();

    private readonly bool _IsInfoStatusChanged;

    public int _EncID;
    public int _EncStatus = 0;


    public string _FullName;
    public int _InfoStatus;

    public FrmEncInfo()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }

    public bool _ReadOnly { get; set; }

    private void FrmEncInfo_Load(object sender, EventArgs e)
    {
        lblTitle.Text = _FullName;

        UL.FillLookup(lueNurseDay, "0", cSet.cnStringA, false, "PersID,Name", "vPersonnel", "NursePersonnel",
            "PersType=4 AND IsDisable=0", null);
        UL.FillLookup(lueNurseNight, "0", cSet.cnStringA, false, "PersID,Name", "vPersonnel", "NursePersonnel",
            "PersType=4 AND IsDisable=0", null);
        UL.FillLookup(lueNurseEvening, "0", cSet.cnStringA, false, "PersID,Name", "vPersonnel", "NursePersonnel",
            "PersType=4 AND IsDisable=0", null);


        //LIB.UL.FillLookup(lueInfoStatus, "0", DAL.cSet.cnStringA, false, "ID,Name", "vADR", "EncInfoStatus", "ADName='EncInfoStatus' AND IsDisable=0 AND ID in (0,3,4,5)", null);
        //if(_InfoStatus ==2)
        //udatEnterDateTime.ShowNow = true;
        //udatEnterDateTime.Enabled = true;

        var dtEncTeam = _bllWardRecord.EncTeamLoad(_EncID);
        if (dtEncTeam.Rows.Count > 0)
        {
            lueNurseDay.EditValue = Convert.ToInt32(dtEncTeam.Rows[0]["NurseDay"]);
            lueNurseEvening.EditValue = Convert.ToInt32(dtEncTeam.Rows[0]["NurseEvening"]);
            lueNurseNight.EditValue = Convert.ToInt32(dtEncTeam.Rows[0]["NurseNight"]);
        }

        CheckReadOnly();
    }

    private void FrmEncInfo_Paint(object sender, PaintEventArgs e)
    {
        Control.LookupEditLine(lueNurseDay, e);
        Control.LookupEditLine(lueNurseEvening, e);
        Control.LookupEditLine(lueNurseNight, e);
    }


    private void CheckReadOnly()
    {
        _ReadOnly = false;
        if (_EncStatus > 2) _ReadOnly = true;

        btnSave.Enabled = !_ReadOnly;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var nima = udatEnterDateTime.Text;

        if (_InfoStatus == 2) _bllEncInfo.EncInfoSave(_EncID, 3);

        _bllWardRecord.EncTeamSave(0, _EncID, lueNurseDay.EditValue, 18, GL.UserPersID);
        _bllWardRecord.EncTeamSave(0, _EncID, lueNurseEvening.EditValue, 28, GL.UserPersID);
        _bllWardRecord.EncTeamSave(0, _EncID, lueNurseNight.EditValue, 19, GL.UserPersID);

        DialogResult = DialogResult.Yes;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}