using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.UC;
using Saman.UC.Discount;
using Saman.UC.Enc;

namespace Saman.Form.Discount;

public partial class FrmDiscountMain : XtraForm
{
    private readonly string _ModIDName = "تخفیف و مددکاری";
    private readonly SACPermission sacPermission = new();
    public byte _ModID = 17;


    public FrmDiscountMain()
    {
        InitializeComponent();
    }

    private void FrmDisMain_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();

        //if (barBtnDisEnc.Enabled != false)
        //{
        //    var ucControl = new UcEncDiscount() { _ModID = _ModID };
        //    ShowUC(ucControl);

        //}
        //else
        //{
        var ucControl = new UcDefult();
        ShowUC(ucControl);
        //}
    }

    private void CheckPermission()
    {
        barBtnDisPage.Enabled = sacPermission.CheckPermission(_ModID, 170001);
        barBtnEncSearch.Enabled = sacPermission.CheckPermission(_ModID, 170002);
        barBtnEncStatus.Enabled = sacPermission.CheckPermission(_ModID, 170003);
    }

    private void barBtnStatus()
    {
        barBtnDisPage.Enabled = false;
        barBtnEncSearch.Enabled = false;
        barBtnEncStatus.Enabled = false;
        // barBtnDisEnc.Enabled = false;
    }

    private void ShowUC(object ucControl)
    {
        panUC.Controls.Clear();
        var myUC = (UserControl)ucControl;
        myUC.Anchor = AnchorStyles.Top;
        myUC.Anchor = AnchorStyles.Right;
        myUC.Dock = DockStyle.Fill;

        Text = _ModIDName + "-" + myUC.Tag;
        panUC.Controls.Add(myUC);
    }


    private void barBtnEncSearch_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncSearch { _ModID = _ModID };
        ShowUC(ucControl);
    }


    private void barBtnDisEnc_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncDiscount { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barBtnDisPage_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcDiscount { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barBtnEncStatus_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncInfo { _ModID = _ModID, _DepID = 2, _EncType = 1 };
        ShowUC(ucControl);
    }
}