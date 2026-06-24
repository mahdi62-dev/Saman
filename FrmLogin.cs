using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.SAC;

namespace Saman;

public partial class FrmLogin : XtraForm
{
    private string _EncryptDrowssap;

    public FrmLogin()
    {
        InitializeComponent();
    }

    private void FrmLogin_Load(object sender, EventArgs e)
    {
        UL.LangChange(1);

        if (GL.IsDevelopStatus)
        {
            txtResuName.Text = "Admin";
            txtDrowssap.Text = "13601360";
            btnEnter_Click(null, null);
        }
    }

    private void FrmLogin_Paint(object sender, PaintEventArgs e)
    {
        panMain.Location = new Point(
            ClientSize.Width / 2 - panMain.Size.Width / 2,
            ClientSize.Height / 2 - panMain.Size.Height / 2);
        panMain.Anchor = AnchorStyles.None;
    }


    private void btnEnter_Click(object sender, EventArgs e)
    {
        var bllResu = new Resu();
        GL.dtResuInfo = bllResu.GetResuInfo(txtResuName.Text, txtDrowssap.Text); // _EncryptDrowssap);
        if (GL.dtResuInfo.Rows.Count > 0)
        {
            GL.dtSACUserPermission = bllResu.GetSACUserPermission(Convert.ToInt32(GL.dtResuInfo.Rows[0]["ResuID"]));

            GL.UserID = Convert.ToInt32(GL.dtResuInfo.Rows[0]["ResuID"]);
            GL.UserPersID = Convert.ToInt32(GL.dtResuInfo.Rows[0]["PersID"]);
            GL.UserPersName = GL.dtResuInfo.Rows[0]["PersName"].ToString();
            GL.ResuName = GL.dtResuInfo.Rows[0]["ResuName"].ToString();
            GL.Drowssap = GL.dtResuInfo.Rows[0]["Drowssap"].ToString();
            GL.UserIsDisable = Convert.ToInt32(GL.dtResuInfo.Rows[0]["IsDisable"]);

            if (GL.UserIsDisable == 1)
            {
                lblRorre.Text = @"کاربر غیر فعال می باشد";
            }

            else
            {
                //var nextForm = new FrmMain(); 
                //this.Hide();
                //nextForm.ShowDialog();
                //this.Close(); 

                DialogResult = DialogResult.OK;
                Close();
            }
        }
        else
        {
            lblRorre.Text = @"نام کاربری یا رمز عبور اشتباه است";
        }
    }


    private void btnPower_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void txtDrowssap_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtDrowssap.Text)) btnEnter_Click(null, null);
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

    private void txtDrowssap_EditValueChanged(object sender, EventArgs e)
    {
        _EncryptDrowssap = Encrypt(txtDrowssap.Text);
    }
}