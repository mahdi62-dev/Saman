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
using Saman.Form.Insurance;
using Saman.Properties;
using Control = App.Control;
using DataTable = System.Data.DataTable;

namespace Saman.UC.ServiceReg;

public partial class UcServiceReg : XtraUserControl
{
    private readonly Dep _bllDep = new();
    private readonly ServicePrice _bllServicePrice = new();
    private readonly BLL.ServiceReg.ServiceReg _bllServiceReg = new();
    private readonly bool _IsOpr = false;
    private readonly long _ManualPrice = 0;
    private readonly double _ServiceNum;
    private readonly string _str;
    private DataTable _dtDep = new();
    private DataTable _dtEncPage = new();
    private DataTable _dtServicePackageItem = new();


    private DataTable _dtServicePrice = new();
    private DataTable _dtServiceReg = new();

    private int _EncStatus;


    private bool _IsEditRow;
    private bool _IsManualPrice, _IsOIC;

    private object _PageDate;
    public bool _ReadOnly;
    private int _RecNoServiceReg;

    private DataRowView _rowCurServie;
    private DataRow[] _rowEncCur;

    private string _ServiceCode, _ServiceName;
    private int _ServiceID, _DrIDFunctor, _ServiceCategoryID;
    private double _ServiceTotalPrice;

    public UcServiceReg()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public int _StorageID { get; set; }
    public int _StoragePeriodID { get; set; }
    public bool _WithDrug { get; set; }
    public byte _EncType { get; set; }
    public int _EncID { get; set; }
    public int _PageID { get; set; }
    private int _PageListRecNo { get; set; }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.LookupEditLine(lueService, e);
        Control.TextEditLine(txtServiceNum, e);
        Control.TextEditLine(txtServicePrice, e);
        Control.LookupEditLine(lueDrFunctor, e);
        Control.LookupEditLine(lueTech, e);
    }

    private void UcRegService_Load(object sender, EventArgs e)
    {
        _dtDep = _bllDep.DepDataLoad(_DepID);
        _StorageID = Convert.ToInt32(_dtDep.Rows[0]["StorageID"]);

        btnServiceRegSave.Enabled = !_ReadOnly;

        UL.LangChange(2);
        ActiveControl = lueService;

        var appSDate = new SDate();
        datPageDate.Text = appSDate.ShamsiDateTime();

        var bllServceReg = new DT();
        bllServceReg.CreateTableServiceReg(_dtServiceReg);

       

        UL.FillLookup(lueDrApplicant, "0", cSet.cnStringA, false, "*", "vDr", "Dr", "IsDisable=0", null);
        UL.FillLookup(lueDrFunctor, "0", cSet.cnStringA, false, "*", "vDr", "Dr", "IsDisable=0", null);
        UL.FillLookup(lueDrTechAssist, "0", cSet.cnStringA, false, "*", "vDr", "Dr", "IsDisable=0", null);
        UL.FillLookup(lueTech, "0", cSet.cnStringA, false, "PersID, Name", "vPersonnel", "Personnel", "IsDisable=0",
            null);


        _rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);
        if (_rowEncCur.Length > 0)
        {
            _EncID = Convert.ToInt32(_rowEncCur[0]["EncID"]);
            _EncType = Convert.ToByte(_rowEncCur[0]["EncType"]);

            _EncStatus = Convert.ToInt32(_rowEncCur[0]["EncStatus"]);
            var EncStatusName = _rowEncCur[0]["EncStatusName"].ToString();

            lblTitle.Text = _rowEncCur[0]["FirstName"] + " " + _rowEncCur[0]["LastName"] + "      " + EncStatusName;
            txtEncID.Text = _rowEncCur[0]["EncId"].ToString();
            lblTitle2.Text = _rowEncCur[0]["Age"] + @" سال - فرزند " + _rowEncCur[0]["FatherName"];
            lblTitleEncDate.Text = "تاریخ پذیرش " + _rowEncCur[0]["StartDate"].ToString().Replace(' ', ' ');
            picGender.Image = Convert.ToByte(_rowEncCur[0]["Gender"]) == 1 ? Resources.male : Resources.female;
            lblDrName.Text = _rowEncCur[0]["DrName"].ToString();
            lblDepName.Text = _rowEncCur[0]["DepName"].ToString();
            UL.SetLookupValueByText(lueDrApplicant, _rowEncCur[0]["DrName"].ToString());

            lueDrFunctor.EditValue = Convert.ToInt32(_rowEncCur[0]["DrID"]);
        }

        //if (_PageID > 0)
        //    panPageNavigator.Visible = false;   //از برنامه دیگری دارو و لوازم ثبت می کنیم
        EncPageLoad();


        var strServiceWhere = "DepID=" + _DepID;

        if (_ModID == 27) strServiceWhere += " AND ServiceType<>4";


        if (_PageID == 0)
        {
            strServiceWhere += " AND EndDate IS Null";
        }
        else
        {
            strServiceWhere += " AND (('" + _PageDate + "' >= StartDate AND '" + _PageDate + "' <= EndDate) OR ('" + _PageDate + "' >= StartDate AND EndDate IS NULL))";
        }

        UL.FillLookup(lueService, null, cSet.cnStringA, false, "*", "vServiceAccessDep", "ServiceAccessDep",
            strServiceWhere, "ServiceCode Desc");


        CheckReadOnly();

        //بیمه تکمیلی فقط در بستری
        if (_EncType > 0) lblInsurance2.Visible = false;
    }

    private void CheckReadOnly()
    {
        _ReadOnly = false;
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
        // lblPageID.Text = _PageID.ToString();

        _PageDate = rowPage["Date"].ToString();
        lblPageID.Text = rowPage["PageCode"].ToString();
        datPageDate.DateTime = DateTime.Parse(rowPage["Date"].ToString());
        timPageTime.Time = DateTime.Parse(rowPage["Date"].ToString());
        lueDrTechAssist.EditValue = Convert.ToInt32(rowPage["DrTechAssist"]);
        ServiceRegLoad(_PageID);
    }

    private void ServiceRegLoad(int pageID)
    {
        var bllServiceLerg = new BLL.ServiceReg.ServiceReg();
        if (_WithDrug) _dtServiceReg = bllServiceLerg.ServiceRegLoad(pageID);

        if (!_WithDrug) _dtServiceReg = bllServiceLerg.ServiceRegWithoutDrugLoad(pageID);

        grdServiceReg.DataSource = _dtServiceReg;
    }

    private void ServicePriceLoad()
    {
        if (_PageDate == null) _PageDate = DBNull.Value;

        _dtServicePrice = _bllServicePrice.ServicePriceLoad(_PageDate, _ServiceID, _EncID, _DrIDFunctor, _IsOIC,
            _ManualPrice, _IsOpr);

        if (_dtServicePrice.Rows.Count > 0 && !_IsManualPrice)
        {
            _ServiceTotalPrice = Convert.ToInt32(_dtServicePrice.Rows[0]["TotalPrice"]);
            txtServicePrice.Text = _ServiceTotalPrice.ToString();
        }
        else
        {
            txtServicePrice.Text = "0";
        }
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
            var _ServiceNum = Convert.ToDouble(txtServiceNum.Text);

            var rowService = _dtServiceReg.NewRow();
            rowService["RecNo"] = _dtServiceReg.Rows.Count + 1;

            rowService["CRUD"] = 0;
            rowService["SRID"] = 0;
            rowService["DrID"] = _DrIDFunctor;
            rowService["DrName"] = lueDrFunctor.Properties.GetDisplayText(_DrIDFunctor);
            rowService["ServiceID"] = _ServiceID;
            rowService["ServiceCategory"] = _ServiceCategoryID;
            rowService["ServiceCode"] = _ServiceCode;
            rowService["ServiceName"] = _ServiceName;

            rowService["TotalPrice"] = _ServiceTotalPrice;
            rowService["Num"] = _ServiceNum;
            rowService["IsManualPrice"] = _IsManualPrice;

            rowService["TotalAmount"] = _ServiceTotalPrice * _ServiceNum;


            if (_IsManualPrice)
            {
                // rowService["Price"] = 0;
                rowService["IsManualPrice"] = _IsManualPrice;
                rowService["TotalAmount"] = Convert.ToDouble(txtServicePrice.EditValue);
            }

            _dtServiceReg.Rows.Add(rowService);
        }
        //else
        //{
        //    ucMessageAddReg1.Visible = true;
        //}

        grdServiceReg.DataSource = _dtServiceReg;

        FormNull();

        ActiveControl = lueService;
    }

    private void EditService()
    {
        var rowService = _dtServiceReg.Select("RecNO=" + _RecNoServiceReg)[0];

        rowService["CRUD"] = 3;

        rowService["ServiceID"] = _rowCurServie["ServiceId"];
        rowService["ServiceCode"] = _rowCurServie["ServiceCode"];
        rowService["ServiceName"] = _rowCurServie["Name"];
        //rowService["Price"] = _rowCurServie["Price"];
        rowService["Num"] = txtServiceNum.Text;
        rowService["IsManualPrice"] = false;

        //double price = Convert.ToDouble(_rowCurServie["Price"]);
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
        var row = lueService.Properties.GetDataSourceRowByKeyValue(lueService.EditValue) as DataRowView;

        if (row == null) return;

        var serviceType = Convert.ToInt32(row["ServiceType"]);


        if (Convert.ToInt32(lueService.EditValue) == 0)
        {
        }

        else if (Convert.ToInt32(lueService.EditValue) > 0 && serviceType > 0)
        {
            _rowCurServie = lueService.Properties.GetDataSourceRowByKeyValue(lueService.EditValue) as DataRowView;
            lblServiceCode.Text = _rowCurServie["ServiceCode"].ToString();

            _ServiceID = Convert.ToInt32(_rowCurServie["ServiceID"]);
            _ServiceCode = _rowCurServie["ServiceCode"].ToString();
            _ServiceName = _rowCurServie["Name"].ToString();

            _ServiceCategoryID = Convert.ToInt32(_rowCurServie["ServiceCategory"]);
            _DrIDFunctor = Convert.ToInt32(lueDrFunctor.EditValue);
            _IsOIC = chkIsOIC.Checked;
            _IsManualPrice = Convert.ToBoolean(_rowCurServie["IsManualPrice"]);


            ServicePriceLoad();

            txtServicePrice.Enabled = _IsManualPrice;
        }

        //for ServicePackage
        else if (Convert.ToInt32(lueService.EditValue) > 0 && serviceType < 0)
        {
            ServicePackageItemLoad(Convert.ToInt32(row["ServiceID"]));
        }
    }

    private void ServicePackageItemLoad(int servicePackageID)
    {
        //nima

        _dtServicePackageItem = _bllServiceReg.ServicePakageItemLoad(servicePackageID);

        if (_dtServicePackageItem.Rows.Count > 0)
        {
            var bindingSource = (BindingSource)lueService.Properties.DataSource;
            var dtService = (DataTable)bindingSource.DataSource;

            foreach (DataRow rowPItem in _dtServicePackageItem.Rows)
            {
                // Find the row that matches the value.
                var rowServiceSel = dtService.Select("ServiceCode = " + rowPItem["ServiceCode"])[0];


                var _ServiceNum = Convert.ToDouble(rowPItem["ServiceNum"]);


                _ServiceID = Convert.ToInt32(rowServiceSel["ServiceID"]);
                _ServiceCode = rowServiceSel["ServiceCode"].ToString();
                _ServiceName = rowServiceSel["Name"].ToString();

                _ServiceCategoryID = Convert.ToInt32(rowServiceSel["ServiceCategory"]);
                _DrIDFunctor = Convert.ToInt32(lueDrFunctor.EditValue);
                _IsOIC = chkIsOIC.Checked;
                _IsManualPrice = false;

                ServicePriceLoad();


                var rowService = _dtServiceReg.NewRow();
                rowService["RecNo"] = _dtServiceReg.Rows.Count + 1;

                rowService["CRUD"] = 0;
                rowService["SRID"] = 0;
                rowService["DrID"] = _DrIDFunctor;
                rowService["DrName"] = lueDrFunctor.Properties.GetDisplayText(_DrIDFunctor);

                rowService["ServiceID"] = _ServiceID;
                rowService["ServiceCategory"] = _ServiceCategoryID;
                rowService["ServiceCode"] = _ServiceCode;
                rowService["ServiceName"] = _ServiceName;

                rowService["TotalPrice"] = _ServiceTotalPrice;

                rowService["Num"] = _ServiceNum;
                rowService["IsManualPrice"] = _IsManualPrice;

                rowService["TotalAmount"] = _ServiceTotalPrice * _ServiceNum;


                _dtServiceReg.Rows.Add(rowService);
            }

            grdServiceReg.DataSource = _dtServiceReg;
            btnServiceRegSave_Click(null, null);

            FormNull();
            ActiveControl = lueService;
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
        if (!CheckValidation()) return;

        var date2 = datPageDate2.Text + " " + timPageTime2.Text;
        var date3 = datPageAnsDate.Text + " " + timPageAnsTime.Text;


        var bllPage = new BLL.Page.Page();
        var dtPage = bllPage.PageSave(_ModID, _DepID, _StorageID, _StoragePeriodID, _EncID, GL.UserPersID, _PageID,
            date2,
            date3,
            Convert.ToInt32(lueDrApplicant.EditValue),
            Convert.ToInt32(lueDrTechAssist.EditValue));

        _PageID = (int)dtPage.Rows[0]["PageID"];

        // lblPageID.Text = dtPage.Rows[0]["PageID"].ToString();
        lblPageID.Text = dtPage.Rows[0]["PageCode"].ToString();


        var bllEncServiceControl = new EncServiceControl();
        if (_ModID == 7 || _ModID == 8)
            bllEncServiceControl.EncServiceControlSave(_EncID, 2, _dtServiceReg.Select("CRUD<>4").Length > 0);

        ServiceRegSave();
        bllPage.PageAmountSave(_PageID);

        EncPageLoad();
        PageDataLoad();

        ucShowTextRegSave.ShowText("خدمات ذخیره گردید", 5000);
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

            _bllServiceReg.ServiceRegSave
            (crud, _EncID, _PageID, _StorageID, _StoragePeriodID, row["DrID"], row["SRID"], row["ServiceID"],
                row["Num"], row["ServiceCategory"], TotalAmountManual, GL.UserPersID
            );

            if (crud == 0) row["CRUD"] = 2;

            if (crud == 4) row.Delete();
        }
    }

    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(datPageDate, e);
        Control.TextEditLine(datPageAnsDate, e);
        Control.TextEditLine(lueDrApplicant, e);
        Control.TextEditLine(lueDrFunctor, e);
        Control.TextEditLine(timPageTime, e);
        Control.TextEditLine(timPageAnsTime, e);
        Control.TextEditLine(lueDrTechAssist, e);
        Control.TextEditLine(datPageDate2, e);
        Control.TextEditLine(timPageTime2, e);
    }

    private void btnServiceDel_Click(object sender, EventArgs e)
    {
        var rows = grvServiceReg.GetSelectedRows();
        foreach (var i in rows)
        {
            var NO = Convert.ToInt32(grvServiceReg.GetDataRow(i)["RecNo"].ToString());

            var row = _dtServiceReg.Select("RecNo=" + NO)[0];

            if (Convert.ToInt32(row["SRID"]) == 0)
                row.Delete();
            else
                row["CRUD"] = 4;
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

    private void lblInsurance2_Click(object sender, EventArgs e)
    {
        var frmInsurance2Intro = new FrmInsurance2Intro
        {
            _ModID = _ModID,
            _DepID = _DepID,
            _EncID = _EncID,
            _PageID = _PageID
        };
        var result = frmInsurance2Intro.ShowDialog();
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
        lueDrFunctor.Text = row["DrName"].ToString();
    }
}