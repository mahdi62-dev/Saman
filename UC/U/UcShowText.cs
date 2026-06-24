using System;
using DevExpress.XtraEditors;


// ReSharper disable once IdentifierTypo
namespace Saman.UC;

public partial class UcShowText : XtraUserControl
{
    public UcShowText()
    {
        InitializeComponent();
    }


    public void ShowText(string displayText, int duration)
    {
        lblText.Text = displayText;

        lblText.Visible = true;
        lblImage.Visible = true;


        if (duration > 0)
        {
            timMain.Enabled = true;
            timMain.Interval = duration;
        }
    }

    private void timMain_Tick(object sender, EventArgs e)
    {
        lblImage.Visible = false;
        lblText.Visible = false;
    }
}