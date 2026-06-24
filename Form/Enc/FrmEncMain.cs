using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.BLL.Dep;
using Saman.UC;
using Saman.UC.Enc;
using Saman.UC.sch;
using Saman.UC.ServiceReg;

namespace Saman.Form.Enc;

public partial class FrmEncMain : XtraForm
{
    private readonly Dep _bllDep = new();
    private readonly string _FormName = "پذیرش";

    private readonly SACPermission sacPermission = new();
    public int _DepID = 1;

    private DataTable _dtDep = new();
    public int _ModID = 1;

    public int _StorageID, _StoragePeriodID;


    public FrmEncMain()
    {
        InitializeComponent();
    }


    private void FrmEncMain_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();

        _dtDep = _bllDep.DepDataLoad(_DepID);
        _StorageID = Convert.ToInt32(_dtDep.Rows[0]["StorageID"]);
        _StoragePeriodID = Convert.ToInt32(_dtDep.Rows[0]["StoragePeriodID"]);

        if (barBtnHome.Enabled)
        {
            var ucControl = new UcEncCur { _ModID = _ModID, _DepID = _DepID };
            ShowUC(ucControl);

            ActiveControl = ucControl;
        }
        else
        {
            var ucControl = new UcDefult();
            ShowUC(ucControl);

            ActiveControl = ucControl;
        }


        //barBtnEncNewDayCare_ItemClick(null,null);
    }

    private void CheckPermission()
    {
        barBtnHome.Enabled = sacPermission.CheckPermission(_ModID, 10001);
        barBtnEncNewOP.Enabled = sacPermission.CheckPermission(_ModID, 10002);
        barBtnEncNewDayCare.Enabled = sacPermission.CheckPermission(_ModID, 10003);
        barBtnEncNewIP.Enabled = sacPermission.CheckPermission(_ModID, 10004);
        barBtnEncSearch.Enabled = sacPermission.CheckPermission(_ModID, 10005);
        barBtnEncInfo.Enabled = sacPermission.CheckPermission(_ModID, 10006);
        barRegService.Enabled = sacPermission.CheckPermission(_ModID, 10007);
        barBtnSch.Enabled = sacPermission.CheckPermission(_ModID, 10008);
        barBtnScan.Enabled = sacPermission.CheckPermission(_ModID, 10009);
    }

    private void barBtnStatus()
    {
        barBtnHome.Enabled = false;
        barBtnEncNewOP.Enabled = false;
        barBtnEncNewDayCare.Enabled = false;
        barBtnEncNewIP.Enabled = false;
        barBtnEncSearch.Enabled = false;
        barBtnEncInfo.Enabled = false;
        barRegService.Enabled = false;
        barBtnSch.Enabled = false;
        barBtnScan.Enabled = false;
    }

    private void ShowUC(object ucControl)
    {
        panUC.Controls.Clear();
        var myUC = (UserControl)ucControl;
        myUC.Anchor = AnchorStyles.Top;
        myUC.Anchor = AnchorStyles.Right;
        myUC.Dock = DockStyle.Fill;

        Text = _FormName + " - " + myUC.Tag;
        panUC.Controls.Add(myUC);
    }

    private int CheckCurEnc()
    {
        var CurEncID = 0;
        var rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);
        if (rowEncCur.Length > 0) CurEncID = Convert.ToInt32(rowEncCur[0]["EncID"]);

        return CurEncID;
    }

    private void barEnc_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncCur { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnEncNewOP_ItemClick(object sender, ItemClickEventArgs e)
    {
    }


    private void barBtnEncNewDayCare_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncSave { _ModID = _ModID, _DepID = _DepID, _EncType = 1 };
        ShowUC(ucControl);
    }

    private void barBtnEncNewIP_ItemClick(object sender, ItemClickEventArgs e)
    {
    }

    private void barBtnEncStatus_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncInfo { _ModID = _ModID, _DepID = _DepID, _EncType = 2 };
        ShowUC(ucControl);
    }

    private void barBtnEncSearch_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncSearch { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnSch_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcSch { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barRegService_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (CheckCurEnc() == 0) return;

        var ucControl = new UcServiceReg
        {
            _ModID = _ModID,
            _DepID = _DepID,
            _StorageID = _StorageID,
            _StoragePeriodID = _StoragePeriodID
        };

        ShowUC(ucControl);
    }
}