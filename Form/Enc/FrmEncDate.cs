using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.Enc;

namespace Saman.Form.Enc;

public partial class FrmEncDate : XtraForm
{
    private readonly EncDate _bllChangeDate = new();
    public int _EncID;
    public string _FirstName, _LastName, _StatusName, _EncTypeName, _EncStartDate, _EncEndDate;


    public FrmEncDate()
    {
        InitializeComponent();
    }

    private void FrmEncDate_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        lblFirstName1.Text = _FirstName;
        lblLastName1.Text = _LastName;
        lblStatusName.Text = _StatusName;
        lblEncTypeName.Text = _EncTypeName;
        lblEncStartDate.Text = _EncStartDate;
        lblEncEndDate.Text = _EncEndDate;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var encStartNew = datEncStartDate.Text + " " + timEncStartTime.Text;
        var encEndNew = datEncEndDate.Text + " " + timEncEndDate.Text;
        var encPageNew = datPageDate.Text + " " + timPageTime.Text;
        var encCashNew = datCashDate.Text + " " + timCashTime.Text;


        _bllChangeDate.EncChangeDate(_EncID, encStartNew, encEndNew, encPageNew,
            encCashNew, (bool)chkIsStartDate.EditValue, (bool)chkIsEncEndDate.EditValue,
            (bool)chkIsPageDate.EditValue, (bool)chkIsCashDate.EditValue, GL.UserPersID);

        DialogResult = DialogResult.Yes;

        Close();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
        Close();
    }
}