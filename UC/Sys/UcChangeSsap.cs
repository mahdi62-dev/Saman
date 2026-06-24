using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Saman.BLL.Def;

namespace Saman.UC.Sys;

public partial class UcChangeSsap : XtraUserControl
{
    private readonly PersDef _bllResuInfo = new();

    private readonly string _HaDrowssap;

    private int _CRUD;

    private string _Drowssap;

    private DataTable _dtResuInfo;

    private string _EncryptDrowssap;

    private string _EncryptDrowssapOld;

    private int _PersID, _ResuID;


    public UcChangeSsap()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }

    private void UcChangeSsap_Load(object sender, EventArgs e)
    {
        PersLoad();
        ActiveControl = txtDrowssapOld;
    }

    private void PersLoad()
    {
        txtResuName.Text = GL.ResuName;
        _PersID = GL.UserPersID;
        _ResuID = GL.UserID;
        _Drowssap = GL.Drowssap;
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (_EncryptDrowssapOld != _Drowssap)
        {
            txtDrowssapOld.ErrorText = "پسورد اشتباه می باشد";
            isValid = false;
        }

        if (string.IsNullOrEmpty(txtDrowssapNew.Text) && string.IsNullOrEmpty(txtDrowssapConfirm.Text))
        {
            txtDrowssapNew.ErrorText = "لطفا رمز عبور را وارد نمایید";
            txtDrowssapConfirm.ErrorText = "لطفا تایید رمز عبور را وارد نمایید";
            isValid = false;
        }

        if (txtDrowssapNew.Text.Length < 3)
        {
            txtDrowssapNew.ErrorText = "حداقل رمز عبور 3 کاراکتر می باشد";
            isValid = false;
        }

        if (txtDrowssapNew.Text != txtDrowssapConfirm.Text)
        {
            txtDrowssapConfirm.ErrorText = "تایید رمز عبور با رمز عبور یکسان نمی باشد";
            isValid = false;
        }

        return isValid;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        _CRUD = 5;
        _dtResuInfo = _bllResuInfo.ResuInfo(_CRUD, _ResuID, null, _EncryptDrowssap, _PersID, GL.UserPersID);

        var myUC = this;
        myUC.Parent.Controls.Remove(myUC);
    }

    private void lblShowPassOld_MouseDown(object sender, MouseEventArgs e)
    {
        txtDrowssapOld.Properties.UseSystemPasswordChar = false;
    }

    private void lblShowPassOld_MouseUp(object sender, MouseEventArgs e)
    {
        txtDrowssapOld.Properties.UseSystemPasswordChar = true;
    }

    private void lblShowPassNew_MouseDown(object sender, MouseEventArgs e)
    {
        txtDrowssapNew.Properties.UseSystemPasswordChar = false;
    }

    private void lblShowPassNew_MouseUp(object sender, MouseEventArgs e)
    {
        txtDrowssapNew.Properties.UseSystemPasswordChar = true;
    }

    private void lblShowPassConfirm_MouseDown(object sender, MouseEventArgs e)
    {
        txtDrowssapConfirm.Properties.UseSystemPasswordChar = false;
    }

    private void lblShowPassConfirm_MouseUp(object sender, MouseEventArgs e)
    {
        txtDrowssapConfirm.Properties.UseSystemPasswordChar = true;
    }

    private static string Encrypt(string value)
    {
        //Using MD5 to encrypt a string
        using (var md5 = new MD5CryptoServiceProvider())
        {
            var utf8 = new UTF8Encoding();
            //Hash data
            var data = md5.ComputeHash(utf8.GetBytes(value));
            return Convert.ToBase64String(data);
        }
    }

    private void txtDrowssapOld_EditValueChanged(object sender, EventArgs e)
    {
        _EncryptDrowssapOld = Encrypt(txtDrowssapOld.Text);
    }

    private void txtDrowssapNew_EditValueChanged(object sender, EventArgs e)
    {
        _EncryptDrowssap = Encrypt(txtDrowssapNew.Text);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        var myUC = this;
        myUC.Parent.Controls.Remove(myUC);
    }
}