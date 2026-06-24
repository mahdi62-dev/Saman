using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.BLL.Dep;
using Saman.UC;
using Saman.UC.Drug;
using Saman.UC.Enc;
using Saman.UC.Opr;
using Saman.UC.ServiceReg;

namespace Saman.Form.Opr;

public partial class FrmOprMain : XtraForm
{
    private readonly Dep _bllDep = new();
    private readonly string _FormName = "اتاق عمل";
    private readonly SACPermission sacPermission = new();
    public int _DepID = 2;

    private DataTable _dtDep = new();
    public int _ModID = 3;
    public int _StorageID, _StoragePeriodID;

    public FrmOprMain()
    {
        InitializeComponent();
    }


    private void FrmOprMain_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();

        _dtDep = _bllDep.DepDataLoad(_DepID);
        _StorageID = Convert.ToInt32(_dtDep.Rows[0]["StorageID"]);
        _StoragePeriodID = Convert.ToInt32(_dtDep.Rows[0]["StoragePeriodID"]);

        panUC.Controls.Clear();

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
    }

    private void CheckPermission()
    {
        barBtnHome.Enabled = sacPermission.CheckPermission(_ModID, 30001);
        barBtnEncNew.Enabled = sacPermission.CheckPermission(_ModID, 30002);
        barBtnEncSearch.Enabled = sacPermission.CheckPermission(_ModID, 30003);
        barBtnEncStatus.Enabled = sacPermission.CheckPermission(_ModID, 30004);
        barbtnPageOpr.Enabled = sacPermission.CheckPermission(_ModID, 30005);
        barRegService.Enabled = sacPermission.CheckPermission(_ModID, 30006);
        barBtnServerRegDrug.Enabled = sacPermission.CheckPermission(_ModID, 30007);
        barBtnInvStock.Enabled = sacPermission.CheckPermission(_ModID, 30008);
    }

    private void barBtnStatus()
    {
        barBtnHome.Enabled = false;
        barBtnEncNew.Enabled = false;
        barBtnEncSearch.Enabled = false;
        barBtnEncStatus.Enabled = false;
        barbtnPageOpr.Enabled = false;
        barRegService.Enabled = false;
        barBtnServerRegDrug.Enabled = false;
    }

    private int CheckCurEnc()
    {
        var CurEncID = 0;
        var rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);
        if (rowEncCur.Length > 0) CurEncID = Convert.ToInt32(rowEncCur[0]["EncID"]);

        return CurEncID;
    }

    private void barBtnEncNew_ItemClick(object sender, ItemClickEventArgs e)
    {
        // this.Text = "پذیرش سرپایی - سیستم پذیرش";
        panUC.Controls.Clear();

        var ucEncNew = new UcEncSave { Anchor = AnchorStyles.Top | AnchorStyles.Right, Dock = DockStyle.Fill };
        ucEncNew._EncType = 1;
        panUC.Controls.Add(ucEncNew);
    }


    private void barBtnEncSearch_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucEncSearch = new UcEncSearch
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Clear();
        panUC.Controls.Add(ucEncSearch);
    }

    private void barbtnPageOpr_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (CheckCurEnc() == 0) return;

        var ucControl = new UcPageOpr
        {
            _ModID = _ModID,
            _DepID = _DepID,
            _StorageID = _StorageID,
            _StoragePeriodID = _StoragePeriodID
        };
        ShowUC(ucControl);
    }

    private void barBtnHome_ItemClick(object sender, ItemClickEventArgs e)
    {
        FrmOprMain_Load(sender, e);
    }

    private void barBtnRegService_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (CheckCurEnc() == 0) return;

        var ucControl = new UcServiceReg { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnServerRegDrug_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (CheckCurEnc() == 0) return;

        var ucControl = new UcServiceRegDrug { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
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

    private void barBtnInvStock_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucStock = new UcStock
            { _ModID = _ModID, _DepID = _DepID }; //, _StorageID = _StorageID , _StoragePeriodID = _StoragePeriodID
        ShowUC(ucStock);

        ActiveControl = ucStock;
    }


    private void barBtnEncStatus_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncInfo { _ModID = _ModID, _DepID = _DepID, _EncType = 1 };
        ShowUC(ucControl);
    }
}