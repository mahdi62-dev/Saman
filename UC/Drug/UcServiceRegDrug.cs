using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using LIB;
using Saman.BLL.Dep;
using Saman.BLL.Enc;
using Saman.BLL.ServiceReg;
using Saman.Properties;
using Control = App.Control;

namespace Saman.UC.Drug;

public partial class UcServiceRegDrug : XtraUserControl
{
    private readonly Dep _bllDep = new();
    private readonly BLL.Storage.Storage _bllStorage = new();
    private readonly DataTable _dtDep = new();
    private readonly string _str;
    private DataTable _dtEncPage = new();
    private DataTable _dtServiceReg = new();

    private int _EncStatus;
    private bool _IsCheckStockNum;

    private bool _IsEditRow;

    private DataTable _ItemData = new();
    private int _RecNoServiceReg;


    private DataRowView _rowCurServie;

    private DataRow[] _rowEncCur;
    private int _ServiceCategory;

    private string _ServiceCode;
    private double _StockNum;

    public UcServiceRegDrug()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public int _StorageID { get; set; }
    public int _StoragePeriodID { get; set; }
    public bool _ReadOnly { get; set; }
    public byte _EncType { get; set; }
    public int _EncID { get; set; }


    public int _PageID { get; set; }
    private int _PageListRecNo { get; set; }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.LookupEditLine(lueService, e);
        Control.TextEditLine(txtServiceNum, e);
        Control.TextEditLine(txtServicePrice, e);
    }

    private void UcServiceRegDrug_Load(object sender, EventArgs e)
    {
        IsCheckStockNum();


        btnServiceRegSave.Enabled = !_ReadOnly;

        UL.LangChange(2);

        txtServiceNum.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;

        ActiveControl = lueService;

        var bllServceReg = new DT();
        bllServceReg.CreateTableServiceReg(_dtServiceReg);


        UL.FillLookup(lueService, null, cSet.cnStringA, false, "*", "vServiceDrug", "vServiceDrug",
            "EndDate IS Null", "Name");

        var appSDate = new SDate();
        datPageDate.Text = appSDate.ShamsiDateTime();

        UL.FillLookup(lueDrApplicant, "0", cSet.cnStringA, false, "*", "vDr", "Dr", null, null);
        UL.FillLookup(lueDrFunctor, "0", cSet.cnStringA, false, "*", "vDr", "Dr", null, null);

        _rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);
        if (_rowEncCur.Length > 0)
        {
            _EncID = Convert.ToInt32(_rowEncCur[0]["EncId"]);
            _EncStatus = Convert.ToInt32(_rowEncCur[0]["EncStatus"]);
            var EncStatusName = _rowEncCur[0]["EncStatusName"].ToString();

            lblTitle.Text = _rowEncCur[0]["FirstName"] + " " + _rowEncCur[0]["LastName"] + "      " + EncStatusName;
            txtEncID.Text = _rowEncCur[0]["EncId"].ToString();
            lblTitle2.Text = _rowEncCur[0]["Age"] + @" سال - فرزند " + _rowEncCur[0]["FatherName"];
            lblTitleEncDate.Text = _rowEncCur[0]["StartDate"].ToString().Replace(' ', ' ');
            picGender.Image = Convert.ToByte(_rowEncCur[0]["Gender"]) == 1 ? Resources.male : Resources.female;
            lblDrName.Text = _rowEncCur[0]["DrName"].ToString();
            lblDepName.Text = _rowEncCur[0]["DepName"].ToString();
            UL.SetLookupValueByText(lueDrApplicant, _rowEncCur[0]["DrName"].ToString());

            lblDescription.Text = _rowEncCur[0]["ChiefComplaintName"] + " " + _rowEncCur[0]["Description"];
        }

        //if (_PageID > 0)
        //    panPageNavigator.Visible = false;   //از برنامه دیگری دارو و لوازم ثبت می کنیم
        EncPageLoad();

        CheckReadOnly();
        ShortKey();
    }

    private void ShortKey()
    {
        msiSave.Click += btnServiceRegSave_Click;
        msiClose.Click += btnCancel_Click;
        msiNew.Click += lblPageNew_Click;


        panMain.Focus();
    }

    private void CheckReadOnly()
    {
        //_ReadOnly = false;
        if (_EncStatus > 2) _ReadOnly = true;

        btnServiceRegSave.Enabled = !_ReadOnly;
    }

    private void FormNull()
    {
        lueService.EditValue = null;
        lblServiceCode.Text = null;
        txtServicePrice.Text = null;
        txtServiceNum.Text = "1";
        ActiveControl = lueService;
    }

    private void btnServiceAdd_Click(object sender, EventArgs e)
    {
        if (_IsEditRow && Convert.ToInt32(lueService.EditValue) > 0 && _rowCurServie != null)
        {
            EditService();
            return;
        }


        if (Convert.ToInt32(lueService.EditValue) > 0 && _rowCurServie != null)
        {
            double stokNumAfter = 0;
            stokNumAfter = _StockNum - double.Parse(txtServiceNum.Text.Replace('.', '/'));

            if (stokNumAfter < 0)
            {
                MessageBox.Show("تعداد ثبتی " + Math.Abs(stokNumAfter) + " " + lblStockServiceUnit.Text +
                                " بیشتر از موجودی است!!!");
                return;
            }

            var rowService = _dtServiceReg.NewRow();
            rowService["RecNo"] = _dtServiceReg.Rows.Count + 1;

            rowService["CRUD"] = 0;
            rowService["SRID"] = 0;

            rowService["DrID"] = lueDrFunctor.EditValue;
            rowService["DrName"] = lueDrFunctor.Properties.GetDisplayText(lueDrFunctor.EditValue);

            rowService["ServiceID"] = _rowCurServie["ServiceID"];


            var ServiceCategory = Convert.ToInt32(_rowCurServie["ServiceCategory"]);
            if (_ModID == 28) ServiceCategory += 1; //تبدیل به دارو و لوازم اتاق عمل

            rowService["ServiceCategory"] = ServiceCategory;

            rowService["ServiceCode"] = _rowCurServie["ServiceCode"];
            rowService["ServiceName"] = _rowCurServie["Name"];
            rowService["Price"] = _rowCurServie["Price"];

            var num = double.Parse(txtServiceNum.Text.Replace('.', '/'));
            rowService["Num"] = num;
            rowService["IsManualPrice"] = false;

            var price = Convert.ToDouble(_rowCurServie["Price"]);

            rowService["TotalAmount"] = price * num;

            var IsManualPrice = Convert.ToBoolean(_rowCurServie["IsManualPrice"]);
            if (IsManualPrice)
            {
                rowService["Price"] = 0;
                rowService["IsManualPrice"] = IsManualPrice;
                rowService["TotalAmount"] = Convert.ToDouble(txtServicePrice.EditValue);
            }

            _dtServiceReg.Rows.Add(rowService);
        }

        grdServiceReg.DataSource = _dtServiceReg;
        ActiveControl = lueService;
        FormNull();

        ActiveControl = lueService;

        AllServiceSave();
    }

    private void IsCheckStockNum()
    {
        var _bllStorage = new BLL.Storage.Storage();
        _IsCheckStockNum = _bllStorage.IsCheckStockNum(_StorageID);
    }

    private void EditService()
    {
        var rowService = _dtServiceReg.Select("RecNO=" + _RecNoServiceReg)[0];

        rowService["CRUD"] = 3;
        rowService["ServiceID"] = _rowCurServie["ServiceId"];
        rowService["ServiceCode"] = _rowCurServie["ServiceCode"];
        rowService["ServiceName"] = _rowCurServie["Name"];
        rowService["Price"] = _rowCurServie["Price"];
        rowService["Num"] = txtServiceNum.Text;
        rowService["IsManualPrice"] = false;

        var price = Convert.ToDouble(_rowCurServie["Price"]);
        var ManualPrice = Convert.ToDouble(txtServicePrice.EditValue);
        var IsNamualPrice = Convert.ToBoolean(_rowCurServie["IsManualPrice"]);

        if (IsNamualPrice)
        {
            rowService["Num"] = Convert.ToInt32(1);
            rowService["Price"] = 0;
            rowService["IsManualPrice"] = IsNamualPrice;
            rowService["TotalAmount"] = txtServicePrice.EditValue;
        }

        _IsEditRow = false;

        FormNull();

        ActiveControl = lueService;
    }

    private void lueService_EditValueChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lueService.EditValue) > 0)
        {
            btnServiceAdd.Enabled = true;
            txtServiceNum.Enabled = true;
            _rowCurServie = lueService.Properties.GetDataSourceRowByKeyValue(lueService.EditValue) as DataRowView;

            _ServiceCode = _rowCurServie["ServiceCode"].ToString();
            _ServiceCategory = Convert.ToInt32(_rowCurServie["Servicecategory"]);


            lblServiceCode.Text = _ServiceCode;
            lblServiceUnit.Text = lblStockServiceUnit.Text = _rowCurServie["ServiceUnitName"].ToString();

            _ItemData = _bllStorage.GetItemData(_StorageID, _StoragePeriodID,
                _ServiceCategory, _ServiceCode);

            if (_ItemData.Rows.Count > 0)
            {
                //_BuyAmountLast = Convert.ToInt64(_ItemData.Rows[0]["BuyAmountLast"]);
                //lblBuyAmountLast.Text = _BuyAmountLast.ToString("n0");
                _StockNum = Convert.ToDouble(_ItemData.Rows[0]["StockNum"]);

                if (_IsCheckStockNum && _StockNum <= 0)
                {
                    btnServiceAdd.Enabled = false;
                    txtServiceNum.Enabled = false;
                }

                lblStockNum.Text = _StockNum.ToString();

                lblStockNum.ForeColor = _StockNum <= 0 ? Color.Red : Color.FromArgb(1, 90, 158);
            }


            var price = Convert.ToInt32(_rowCurServie["Price"]);
            txtServicePrice.Text = price.ToString();

            var IsManualPrice = Convert.ToBoolean(_rowCurServie["IsManualPrice"]);
            if (IsManualPrice)
            {
                txtServicePrice.ReadOnly = false;
                lblManualPrice.Visible = true;
                txtServicePrice.Text = "0";
            }

            ActiveControl = txtServiceNum;
        }
    }


    private bool CheckValidation()
    {
        var isValid = true;

        if (string.IsNullOrEmpty(txtEncID.Text))
        {
            txtEncID.ErrorText = "لطفا کد را وارد نمایید";

            isValid = false;
        }

        return isValid;
    }

    private void btnServiceRegSave_Click(object sender, EventArgs e)
    {
        AllServiceSave();
    }

    private void AllServiceSave()
    {
        if (!CheckValidation()) return;

        var bllPage = new BLL.Page.Page();
        var dtPage = bllPage.PageSave
        (
            _ModID, _DepID, _StorageID, _StoragePeriodID, _EncID, GL.UserPersID, _PageID,
            datPageDate.Text + ' ' + timPageTime.Text,
            datPageDate.Text + ' ' + timPageTime.Text,
            Convert.ToInt32(lueDrApplicant.EditValue), 0
        );

        _PageID = (int)dtPage.Rows[0]["PageID"];

        // lblPageID.Text = dtPage.Rows[0]["PageID"].ToString();
        lblPageID.Text = dtPage.Rows[0]["PageCode"].ToString();

        var bllEncServiceControl = new EncServiceControl();

        bllEncServiceControl.EncServiceControlSave(_EncID, _ModID == 28 ? 4 : 1,
            _dtServiceReg.Select("CRUD<>4").Length > 0);


        ServiceRegSave();
        EncPageLoad();
        PageDataLoad();

        ucShowTextSave.ShowText("اطلاعات ذخیره گردید", 5000);
    }

    private void ServiceRegSave()
    {
        if (_dtServiceReg.Rows.Count == 0) return;

        int crud;
        var TotalAmountManual = 0;
        bool IsManualPrice;


        DataTable dtService = null;
        var result = _dtServiceReg.Select("CRUD<>2");

        if (result.Length > 0) dtService = result.CopyToDataTable();

        if (dtService == null) return;

        foreach (DataRow row in dtService.Rows)
        {
            crud = Convert.ToInt32(row["CRUD"]);
            //قیمت دستی
            IsManualPrice = Convert.ToBoolean(row["IsManualPrice"]);
            if (IsManualPrice) TotalAmountManual = Convert.ToInt32(row["TotalAmount"]);

            var bllServiceReg = new BLL.ServiceReg.ServiceReg();
            bllServiceReg.ServiceRegSave
            (crud, _EncID, _PageID, _StorageID, _StoragePeriodID, row["DrID"], row["SRID"], row["ServiceID"],
                row["Num"], row["ServiceCategory"], TotalAmountManual, GL.UserPersID);

            if (crud == 0) row["CRUD"] = 2;

            if (crud == 4) row.Delete();
        }

        ServiceRegLoad(_PageID);
    }

    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        //App.Control.TextEditLine(txtEncID, e);
        //App.Control.TextEditLine(txtPageID, e);
        Control.TextEditLine(datPageDate, e);
        Control.TextEditLine(datPageAnsDate, e);
        Control.TextEditLine(lueDrApplicant, e);
        //App.Control.TextEditLine(lueDrFunctor, e);
        //App.Control.TextEditLine(lblDepName, e);
        Control.TextEditLine(timPageTime, e);
        Control.TextEditLine(timPageAnsTime, e);
    }


    private void btnServiceDel_Click(object sender, EventArgs e)
    {
        var rows = grvServiceReg.GetSelectedRows();
        foreach (var i in rows)
        {
            grvServiceReg.GetDataRow(i);

            var NO = Convert.ToInt32(grvServiceReg.GetDataRow(i)["RecNo"].ToString());

            var row = _dtServiceReg.Select("RecNo=" + NO)[0];

            if (Convert.ToInt32(row["SRID"]) == 0)
                row.Delete();
            else
                row["CRUD"] = 4;

            grdServiceReg.DataSource = _dtServiceReg;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucEncCur = new UcEncCur
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        ucEncCur._ModID = _ModID;
        panUC.Controls.Add(ucEncCur);
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        timer1.Interval = 9000;
        timer1.Enabled = true;
        ucMessageAddReg1.Visible = false;
    }

    private void EncPageLoad()
    {
        var bllPage = new BLL.Page.Page();
        _dtEncPage = bllPage.EncPageLoad(_EncID, _ModID, _DepID, _PageID);


        _PageListRecNo = _dtEncPage.Rows.Count;

        if (_PageListRecNo > 0) // && _PageID==0)
        {
            _PageID = Convert.ToInt32(_dtEncPage.Rows[_PageListRecNo - 1]["PageID"].ToString());
            PageDataLoad();
            lblPageList.Text = "صفحه " + _PageListRecNo + " از " + _dtEncPage.Rows.Count;
        }
    }


    private void PageDataLoad()
    {
        if (_dtEncPage.Rows.Count == 0) return;

        var rowPage = _dtEncPage.Select("PageID=" + _PageID)[0];
        //lblPageID.Text = _PageID.ToString();
        lblPageID.Text = rowPage["PageCode"].ToString();
        datPageDate.DateTime = DateTime.Parse(rowPage["Date"].ToString());
        timPageTime.Time = DateTime.Parse(rowPage["Date"].ToString());

        var isReadOnly = false;
        isReadOnly = Convert.ToBoolean(rowPage["IsReadOnly"]);

        if (isReadOnly)
        {
            _ReadOnly = true;
            CheckReadOnly();
        }

        ServiceRegLoad(_PageID);
    }

    private void ServiceRegLoad(int pageID)
    {
        var bllServiceLerg = new BLL.ServiceReg.ServiceReg();
        _dtServiceReg = bllServiceLerg.ServiceRegDrugLoad(pageID);

        //if (_dtServiceReg.Rows.Count > 0)
        //{
        grdServiceReg.DataSource = _dtServiceReg;

        //}
    }

    private void grvServiceReg_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        var view = sender as GridView;

        var crud = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "CRUD"));
        if (crud == 4) e.Appearance.BackColor = Color.PaleVioletRed;
    }

    private void lueService_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 43 && _rowCurServie != null)
        {
            btnServiceAdd_Click(sender, e);
            //EncSearch(int.Parse(txtEncID.Text));
            lueService.EditValue = null;
            e.KeyChar = (char)0;
        }
    }

    private void lblPagePreRec_Click(object sender, EventArgs e)
    {
        PageNavigate(-1);
    }


    private void lblPageNextRec_Click(object sender, EventArgs e)
    {
        PageNavigate(1);
    }

    private void PageNavigate(int navigate)
    {
        if (_dtEncPage.Rows.Count == 0) return;

        _PageListRecNo = Convert.ToInt32(_dtEncPage.Select("PageID=" + _PageID)[0]["RecNo"]);
        _PageListRecNo = _PageListRecNo + navigate;
        if ((_PageListRecNo >= 1 && _PageListRecNo <= _dtEncPage.Rows.Count && _PageListRecNo > 0)
            || (_PageListRecNo <= _dtEncPage.Rows.Count && _PageListRecNo > 0 && _PageListRecNo > 0))
        {
            _PageID = Convert.ToInt32(_dtEncPage.Rows[_PageListRecNo - 1]["PageID"].ToString());

            if (_PageID > 0) PageDataLoad();

            lblPageList.Text = "صفحه " + _PageListRecNo + " از " + _dtEncPage.Rows.Count;
        }
    }

    private void lblPageNew_Click(object sender, EventArgs e)
    {
        _PageID = 0;
        if (_PageID == 0)
        {
            _ReadOnly = false;
            CheckReadOnly();
        }

        _dtServiceReg.Clear();
        grdServiceReg.DataSource = _dtServiceReg;

        lblPageID.Text = null;


        datPageDate.Text = "";
        timPageTime.Time = Convert.ToDateTime("00:00");

        datPageAnsDate.Text = "";
        timPageAnsTime.Time = Convert.ToDateTime("00:00");
    }

    private void btnServiceEdit_Click(object sender, EventArgs e)
    {
        _IsEditRow = true;
        var row = grvServiceReg.GetDataRow(grvServiceReg.FocusedRowHandle);

        _RecNoServiceReg = Convert.ToInt32(row["RecNo"]);
        lueService.EditValue = row["ServiceID"];
        txtServiceNum.Text = row["Num"].ToString();
    }


    private void txtServiceNum_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13) btnServiceAdd_Click(null, null);
    }
}