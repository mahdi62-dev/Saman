using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Saman.APP;
using Saman.BLL.Enc;
using Saman.BLL.Ward;
using Saman.SmsWebService;

namespace Saman.Form;

public partial class FrmTestSms : XtraForm
{
    private readonly EncInfo _bllEncInfo = new();

    private readonly WardRecord _bllWardRecord = new();

    private readonly bool _IsInfoStatusChanged;
    public int _EncID;
    public string _FullName;
    public int _InfoStatus;

    public FrmTestSms()
    {
        InitializeComponent();
    }

    private void FrmEncInfo_Paint(object sender, PaintEventArgs e)
    {
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
    }

    private void FrmTestSms_Load(object sender, EventArgs e)
    {
    }

    private void btnSmsLine_Click(object sender, EventArgs e)
    {
        var ws = new SendReceive();
        var message = string.Empty;

        var smsLine = ws.GetSMSLines("09125141297", "Alfa@2020#", ref message);

        txtNumber.Text = smsLine[1].LineNumber.ToString();
        txtLineID.Text = smsLine[1].ID.ToString();
    }

    private void btnSendSms_Click(object sender, EventArgs e)
    {
        try
        {
            var smslinedID = 225714;
            var sendDetaiLs = new List<WebServiceSmsSend>();
            {
                var messagebody = string.Empty;
                long mobileno = 0;
                var isFlash = false;


                messagebody = txtSmsText.Text.Trim();
                mobileno = Convert.ToInt64(txtMobileNo.Text.Trim());
                isFlash = false;

                sendDetaiLs.Add(new WebServiceSmsSend
                    {
                        MessageBody = messagebody,
                        MobileNo = mobileno,
                        IsFlash = isFlash
                    }
                );
            }
            /////////////Chek Line ID


            if (!int.TryParse(smslinedID.ToString(), out smslinedID)) throw new Exception("شماره آی دی معتبر نیست");

            /////////////Send Info To WebService

            var ws = new SendReceive();
            var message = string.Empty;
            var result = ws.SendMessage(
                "09125141297",
                "Alfa@2020#",
                sendDetaiLs.ToArray(),
                smslinedID,
                null,
                ref message);
        }
        catch
        {
            MessageBox.Show("مشکل در ارسال اس ام اس");
        }
    }

    private void simpleButton1_Click(object sender, EventArgs e)
    {
        var appSms = new SMS();

        appSms.SendSms("09125141297", "Alfa@2020#", txtSmsText.Text, Convert.ToInt64(txtMobileNo.Text));
    }
}