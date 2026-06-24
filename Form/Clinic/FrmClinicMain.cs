using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.BLL.Dep;
using Saman.UC;
using Saman.UC.Enc;
using Saman.UC.ServiceReg;

namespace Saman.Form.Clinic;

public partial class FrmClinicMain : XtraForm
{
    private readonly Dep _bllDep = new();
    private readonly string _FormName = "پذیرش کلینیک";

    private readonly SACPermission sacPermission = new();
    public int _DepID = 1;

    private DataTable _dtDep = new();
    public int _ModID = 9;

    public int _StorageID, _StoragePeriodID;

    public FrmClinicMain()
    {
        InitializeComponent();
    }


    private void FrmClinicMain_Load(object sender, EventArgs e)
    {
        _dtDep = _bllDep.DepDataLoad(_DepID);
        _StorageID = Convert.ToInt32(_dtDep.Rows[0]["StorageID"]);
        _StoragePeriodID = Convert.ToInt32(_dtDep.Rows[0]["StoragePeriodID"]);

        // BarBtnCheckPermission();


        //var ucControl = new UcEncCur() { _ModID = _ModID, _DepID = _DepID };
        //ShowUC(ucControl);

        //this.ActiveControl = ucControl;


        barBtnEncNewOP_ItemClick(null, null);
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

    private void BarBtnCheckPermission()
    {
        var per = sacPermission.CheckPermission(_ModID, 1000);

        if (sacPermission.CheckPermission(_ModID, 1001)) barBtnEncNewDayCare.Visibility = BarItemVisibility.Always;

        if (sacPermission.CheckPermission(_ModID, 1002)) barBtnEncNewIP.Visibility = BarItemVisibility.Always;

        if (sacPermission.CheckPermission(_ModID, 1003)) barBtnEncSearch.Visibility = BarItemVisibility.Always;

        if (sacPermission.CheckPermission(_ModID, 1004)) barBtnEncInfo.Visibility = BarItemVisibility.Always;

        if (sacPermission.CheckPermission(_ModID, 1005)) barRegService.Visibility = BarItemVisibility.Always;

        if (sacPermission.CheckPermission(_ModID, 1006)) barRegService.Visibility = BarItemVisibility.Always;
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

    private void barBtnEncStatus_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncInfo { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnEncSearch_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncSearch { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnEncNewOP_ItemClick(object sender, ItemClickEventArgs e)
    {
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