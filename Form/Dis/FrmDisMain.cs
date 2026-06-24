using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.UC;
using Saman.UC.Dis;
using Saman.UC.Enc;
using Saman.UC.Opr;
using Saman.UC.ServiceReg;

namespace Saman.Form.Dis;

public partial class FrmDisMain : XtraForm
{
    private readonly string _ModIDName = "ترخیص";

    private readonly SACPermission sacPermission = new();
    public byte _DepID = 6;
    public byte _ModID = 6;


    public FrmDisMain()
    {
        InitializeComponent();
    }

    private void FrmDisMain_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();

        if (barBtnDisEnc.Enabled)
        {
            var ucControl = new UcEncDis { _ModID = _ModID, _DepID = _DepID };
            ShowUC(ucControl);
        }
        else
        {
            var ucControl = new UcDefult();
            ShowUC(ucControl);
        }
    }

    private void CheckPermission()
    {
        barBtnDisEnc.Enabled = sacPermission.CheckPermission(_ModID, 60001);
        barBtnEncSearch.Enabled = sacPermission.CheckPermission(_ModID, 60002);
        barBtnEncStatus.Enabled = sacPermission.CheckPermission(_ModID, 60003);
        barRegService.Enabled = sacPermission.CheckPermission(_ModID, 60004);
        barBtnServiceRegOpr.Enabled = sacPermission.CheckPermission(_ModID, 60005);
        barbtnCashList.Enabled = sacPermission.CheckPermission(_ModID, 60006);
    }

    private void barBtnStatus()
    {
        barBtnDisEnc.Enabled = false;
        barBtnEncSearch.Enabled = false;
        barBtnEncStatus.Enabled = false;
        barRegService.Enabled = false;
        barBtnServiceRegOpr.Enabled = false;
        barbtnCashList.Enabled = false;
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

    private int CheckCurEnc()
    {
        var CurEncID = 0;
        var rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);
        if (rowEncCur.Length > 0) CurEncID = Convert.ToInt32(rowEncCur[0]["EncID"]);

        return CurEncID;
    }


    private void barRegService_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (CheckCurEnc() == 0) return;

        var ucControl = new UcServiceReg { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnEncSearch_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncSearch { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barBtnServiceRegOpr_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (CheckCurEnc() == 0) return;

        var ucControl = new UcPageOpr
        {
            _ModID = _ModID,
            _DepID = _DepID,
            _ReadOnly = true
        };
        ShowUC(ucControl);
    }

    private void barBtnDisEnc_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncDis { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnEncStatus_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncInfo { _ModID = _ModID, _DepID = 2, _EncType = 1 };
        ShowUC(ucControl);
    }
}