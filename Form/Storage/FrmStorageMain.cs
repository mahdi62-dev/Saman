using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using LIB;
using Saman.APP.SAC;
using Saman.UC;
using Saman.UC.Storage;

namespace Saman.Form.Storage;

public partial class frmStorageMain : XtraForm
{
    private readonly BLL.Storage.Storage _bllStorage = new();
    private readonly string _FormName = "انبار";
    private readonly SACPermission sacPermission = new();
    public int _DepID = 4;

    public byte _ModID = 4;
    private DataRowView _rowCurServie;

    private int _StorageID, _StoragePeriodID, _StorageType;


    public frmStorageMain()
    {
        InitializeComponent();
    }

    private void frmStorageMain_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();

        UL.FillLookupBar(barLueStorage, lueStorage, "1", cSet.cnStringERP, true, "StorageID,Name", "InvStorage",
            "InvStorage", null, null);

        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
    }

    private void CheckPermission()
    {
        lueStorage.Enabled = sacPermission.CheckPermission(_ModID, 40001);
        barBtnInvInput.Enabled = sacPermission.CheckPermission(_ModID, 40002);
        barBtnInvOutput.Enabled = sacPermission.CheckPermission(_ModID, 40003);
        barBtnInvStock.Enabled = sacPermission.CheckPermission(_ModID, 40004);
        barBtnInvFromServiceReg.Enabled = sacPermission.CheckPermission(_ModID, 40005);
        barBtnWarehouseCardex.Enabled = sacPermission.CheckPermission(_ModID, 40006);
        batBtnWarehouseHandling.Enabled = sacPermission.CheckPermission(_ModID, 40007);
        barBtnpurchaseRequest.Enabled = sacPermission.CheckPermission(_ModID, 40008);
    }

    private void barBtnStatus()
    {
        lueStorage.Enabled = false;
        barBtnInvInput.Enabled = false;
        barBtnInvOutput.Enabled = false;
        barBtnInvStock.Enabled = false;
        barBtnInvFromServiceReg.Enabled = false;
        barBtnWarehouseCardex.Enabled = false;
        batBtnWarehouseHandling.Enabled = false;
        barBtnpurchaseRequest.Enabled = false;
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (lueStorage.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueStorage.EditValue.ToString()))
            // barBtnEncNew.Hint = "لطفا انبار را انتخاب نمایید";
            isValid = false;

        return isValid;
    }


    private void lueStorage_EditValueChanged(object sender, EventArgs e)
    {
        _StorageID = Convert.ToInt32(lueStorage.EditValue);
        //lueStorage.Edit
        //_rowCurServie = lueStorage.Properties.GetDataSourceRowByKeyValue(lueStorage.EditValue) as DataRowView;
        //_rowCurServie = lueStorage.Properties.GetDataSourceRowByKeyValue(lueStorage.EditValue) as DataRowView;
        //_StorageType = Convert.ToInt32(_rowCurServie["ServiceCode"].ToString());

        _StoragePeriodID = _bllStorage.StoragePeriodIDLoad(_StorageID);

        panUC.Controls.Clear();
        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
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


    private void barBtnInvInput_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (!CheckValidation()) return;

        var ucControl = new UcInvInput
            { _ModID = _ModID, _DepID = _DepID, _StorageID = _StorageID, _StoragePeriodID = _StoragePeriodID };
        ShowUC(ucControl);

        ActiveControl = ucControl;
    }

    private void barBtnInvStock_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (!CheckValidation()) return;

        var ucStock = new UcStock
            { _ModID = _ModID, _DepID = _DepID }; //, _StorageID = _StorageID , _StoragePeriodID = _StoragePeriodID
        ShowUC(ucStock);

        ActiveControl = ucStock;
    }

    private void barBtnWarehouseCardex_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (!CheckValidation()) return;

        var ucCardex = new UcCardex
            { _ModID = _ModID, _DepID = _DepID }; //, _StorageID = _StorageID , _StoragePeriodID = _StoragePeriodID
        ShowUC(ucCardex);

        ActiveControl = ucCardex;
    }

    private void barBtnpurchaseRequest_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (!CheckValidation()) return;

        var ucControl = new UcBuyReq
            { _ModID = _ModID, _DepID = _DepID, _StorageID = _StorageID, _StoragePeriodID = _StoragePeriodID };
        ShowUC(ucControl);

        ActiveControl = ucControl;
    }

    private void barBtnInvOutput_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (!CheckValidation()) return;

        var ucControl = new UcInvOutput
            { _ModID = _ModID, _DepID = _DepID, _StorageID = _StorageID, _StoragePeriodID = _StoragePeriodID };
        ShowUC(ucControl);

        ActiveControl = ucControl;
    }

    private void barBtnInvFromServiceReg_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (!CheckValidation()) return;

        var ucControl = new UcInvFromServiceReg
            { _ModID = _ModID, _DepID = _DepID, _StorageID = _StorageID, _StoragePeriodID = _StoragePeriodID };
        ShowUC(ucControl);

        ActiveControl = ucControl;
    }
}