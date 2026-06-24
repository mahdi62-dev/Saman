using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Saman.BLL.Enc;
using Saman.BLL.Ward;

namespace Saman.Form.Dis;

public partial class FrmDisDiscount : XtraForm
{
    private readonly EncInfo _bllEncInfo = new();

    private readonly WardRecord _bllWardRecord = new();

    private readonly bool _IsInfoStatusChanged;

    public int _EncID;
    public int _EncStatus = 0;


    public string _FullName;
    public int _InfoStatus;

    public FrmDisDiscount()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }

    public bool _ReadOnly { get; set; }

    private void FrmEncInfo_Load(object sender, EventArgs e)
    {
        lblTitle.Text = _FullName;
        //if(_InfoStatus ==2)
        //udatEnterDateTime.ShowNow = true;
        //udatEnterDateTime.Enabled = true;


        CheckReadOnly();
    }

    private void FrmEncInfo_Paint(object sender, PaintEventArgs e)
    {
    }


    private void CheckReadOnly()
    {
        //_ReadOnly = false;
        //if (_EncStatus > 2) _ReadOnly = true;

        //btnSave.Enabled = !_ReadOnly;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        //string nima= udatEnterDateTime.Text;

        //if(_InfoStatus ==2) 
        //    _bllEncInfo.EncInfoSave(_EncID, 3);

        var bllDisDiscount = new BLL.Dis.Discount();
        bllDisDiscount.CenterDiscountASave(_EncID, txtCenterDiscountAmountA.EditValue, GL.UserPersID);
        //_bllWardRecord.EncTeamSave(0,_EncID, lueNurseDay.EditValue, 18, GL.UserPersID);
        //_bllWardRecord.EncTeamSave(0, _EncID, lueNurseNight.EditValue, 19, GL.UserPersID);

        DialogResult = DialogResult.Yes;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}