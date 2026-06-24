using Saman.BLL.Enc;
using Saman.BLL.SAC;
using Saman.Form.Enc;
using System;
using System.Data;
using System.Windows.Forms;

namespace Saman.UC.Clinic
{
    public partial class UcEncNewOpFull : DevExpress.XtraEditors.XtraUserControl
    {


        public int _ModID { get; set; }
        public int _DepID { get; set; }
        public int _StorageID { get; set; }
        public int _StoragePeriodID { get; set; }

        private BLL.SAC.Permission _bllSACPermission = new Permission();
        BLL.Dep.Dep _bllDep = new BLL.Dep.Dep();


        //----ENC------
        private bool _IsEncNew;
        private int _PatientID, _EncID;
        private DataRow _rowEncSearch;
        public bool _EncEditMode { get; set; }
        public byte _EncType { get; set; }


        private DataTable _dtEncCur;
        public DataTable _dtEncSearch;

        private BLL.Enc.EncNew _bllEncNew = new EncNew();

        //--END --ENC------














        //----Page----
        private int _PageID;
        private int _PageListRecNo { get; set; }



        private DataRowView _rowCurServie;
        private DataTable _dtDep = new DataTable();
        private DataTable _dtEncPage = new DataTable();
        private DataTable _dtServiceReg = new DataTable();

        BLL.ServiceReg.ServiceReg _bllServiceReg = new BLL.ServiceReg.ServiceReg();
        //--END--Page----

        public UcEncNewOpFull()
        {
            InitializeComponent();
        }
        private void UcEncNewOpFull_Load(object sender, EventArgs e)
        {
            LIB.UL.LangChange(2);
            ActiveControl = txtNationalCode;

            _EncType = 0;

            FillLookup();
            SetMask();


            //----Page----
            var bllServceReg = new BLL.ServiceReg.DT();
            bllServceReg.CreateTableServiceReg(_dtServiceReg);




            //EncPageLoad();
            if (_EncID == 0)
            {
                lagPageService.Enabled = false;
                lagEncCash.Enabled = false;
            }
           

            //CheckReadOnly();
        }

        private void FillLookup()
        {
            LIB.UL.FillLookup(lueGender, "1", DAL.cSet.cnStringA, false, "ID,Name", "vADR", "Gender", "ADName='Gender'", null);
            LIB.UL.FillLookup(lueMaritalStatus, "1", DAL.cSet.cnStringA, false, "ID,Name", "vADR", "MaritalStatus", "ADName='MaritalStatus'", null);
            LIB.UL.FillLookup(lueCountry, "1", DAL.cSet.cnStringA, false, "ID,Name", "vADR", "Country", "ADName='Country'", null);

            LIB.UL.FillLookup(lueEncDep, "1", DAL.cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);
            LIB.UL.FillLookup(lueEncDr, "0", DAL.cSet.cnStringA, false, "*", "vDr", "Dr", null, null);
            LIB.UL.FillLookup(lueEncChiefComplaint, "0", DAL.cSet.cnStringA, false, "ID,Name", "vADR", "ChiefComplaint", "ADName='ChiefComplaint'", null);

            LIB.UL.FillLookup(lueInsurance, "1", DAL.cSet.cnStringA, false, "InsuranceID,Name", "Insurance", "Insurance", null, null);
            LIB.UL.FillLookup(lueCity, "19", DAL.cSet.cnStringA, false, "ID,Name", "vADRCity", "City", null, null);
            LIB.UL.FillLookup(lueDistrict, "0", DAL.cSet.cnStringA, false, "ID,Name", "vADR", "District", "ADName='District'", null);


            //----Page----
            LIB.UL.FillLookup(luePageDep, "0", DAL.cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);
            LIB.UL.FillLookup(luePageDr, "0", DAL.cSet.cnStringA, false, "*", "vDr", "Dr", null, null);
            LIB.UL.FillLookup(luePageDr2, "0", DAL.cSet.cnStringA, false, "*", "vDr", "Dr", null, null);
            LIB.UL.FillLookup(luePagePersonnel, "0", DAL.cSet.cnStringA, false, "*", "vPersonnel", "PagePersonnel", "IsDisable=0 AND PersType BETWEEN 2 AND 999", "Name");




            string strServiceWhere = null; //"DepID=" + _DepID;
            LIB.UL.FillLookup(lueService, null, DAL.cSet.cnStringA, false, "*", "vServiceAccessDep", "ServiceAccessDep", strServiceWhere, "Name");
            //--END--Page----
        }

        private void SetMask()
        {

            txtFirstName.Properties.Mask.EditMask = App.UI.MaskPersian;
            txtLastName.Properties.Mask.EditMask = App.UI.MaskPersian;
            txtFatherName.Properties.Mask.EditMask = App.UI.MaskPersian;
            //txtCompanionFullName.Properties.Mask.EditMask = App.UI.MaskPersian;
        }
        private void lacMain_Paint(object sender, PaintEventArgs e)
        {
            App.Control.TextEditLine(txtNationalCode, e);
            App.Control.TextEditLine(txtFirstName, e);
            App.Control.TextEditLine(txtLastName, e);
            App.Control.TextEditLine(txtFatherName, e);

            App.Control.LookupEditLine(lueGender, e);
            App.Control.LookupEditLine(lueMaritalStatus, e);
            App.Control.LookupEditLine(lueGender, e);
            App.Control.TextEditLine(txtBirthYear, e);

            App.Control.TextEditLine(txtMobile, e);
            App.Control.LookupEditLine(lueCity, e);
            App.Control.LookupEditLine(lueDistrict, e);
            App.Control.TextEditLine(txtAddress, e);

            App.Control.LookupEditLine(lueEncDep, e);
            App.Control.LookupEditLine(lueEncDr, e);
            App.Control.LookupEditLine(lueEncChiefComplaint, e);
            App.Control.TextEditLine(txtEncDescription, e);

            App.Control.TextEditLine(lueInsurance, e);
            App.Control.TextEditLine(txtInsuranceID, e);
            App.Control.TextEditLine(txtInsurancePageNumber, e);

            //----Page----
            App.Control.LookupEditLine(luePageDep, e);
            App.Control.LookupEditLine(luePageDr, e);
            App.Control.LookupEditLine(luePagePersonnel, e);
            App.Control.LookupEditLine(lueService, e);

            App.Control.TextEditLine(txtServiceNum, e);
            //--END--Page----
        }

        private void EncNull()
        {
            _PatientID = 0;
            _EncID = 0;
            txtEncID.Text = null;
            txtNationalCode.Text = null;
            txtFirstName.Text = null;
            txtLastName.Text = null;
            txtFatherName.Text = null;
            lueCountry.EditValue = null;
            lueGender.EditValue = null;
            txtBirthYear.Text = null;
            lueMaritalStatus.EditValue = null;
            txtMobile.Text = null;
            lueCity.EditValue = null;
            lueDistrict.EditValue = null;
            txtAddress.Text = null;


            EncFormEdit(true);

            if (_EncID == 0)
            {
                lagPageService.Enabled = false;
                lagEncCash.Enabled = false;
            }


        }
        private void EncFormEdit(bool enable)
        {
            laiNationalCode.Enabled = enable;
            laiFirstName.Enabled = enable;
            laiLastName.Enabled = enable;
            laiFatherName.Enabled = enable;
            laiCountry.Enabled = enable;
            laiGender.Enabled = enable;
            laiBirthYear.Enabled = enable;
            laiMaritalStatus.Enabled = enable;
            laiDistrict.Enabled = enable;
            laiAddress.Enabled = enable;
            laiCity.Enabled = enable;
            laiMobile.Enabled = enable;


            lagEnc.Enabled = enable;


        }
        private void PatientSearch(string NationalCode)
        {
            _dtEncSearch = _bllEncNew.PatientSearch
                (txtFirstName.Text, txtLastName.Text, txtFatherName.Text, txtNationalCode.Text);


            if (_dtEncSearch.Rows.Count > 0)
            {
                if (NationalCode.Length < 10)
                {
                    Form.Enc.FrmEncSearch frmEncSearch = new FrmEncSearch();
                    frmEncSearch._dtEncSearch = _dtEncSearch;

                    frmEncSearch.ShowDialog();

                    _PatientID = frmEncSearch._PatientID;
                    PatientSetFromSearch(_PatientID, null);
                }

                else
                {
                    PatientSetFromSearch(0, NationalCode);
                }

            }

        }
        private void PatientSetFromSearch(int PatientID, string NationalCode)
        {
            DataRow row = null;

            if (_PatientID > 0)
                row = _dtEncSearch.Select("PatientID=" + _PatientID)[0];

            if (_PatientID == 0)
                row = _dtEncSearch.Select("NationalCode=" + NationalCode)[0];

            txtNationalCode.ReadOnly = true;
            txtFirstName.ReadOnly = true;
            txtLastName.ReadOnly = true;
            txtFatherName.ReadOnly = true;
            lueGender.ReadOnly = true;
            lueMaritalStatus.ReadOnly = true;
            lueCountry.ReadOnly = true;
            txtBirthYear.ReadOnly = true;
            txtBirthYear.ReadOnly = true;
            //txtMobile.ReadOnly = true;
            //txtStreet.ReadOnly = true;

            _PatientID = Convert.ToInt32(row["PatientID"]);
            txtNationalCode.Text = row["NationalCode"].ToString();
            txtFirstName.Text = row["FirstName"].ToString();
            txtLastName.Text = row["LastName"].ToString();
            txtFatherName.Text = row["FatherName"].ToString();
            lueGender.EditValue = Convert.ToInt32(row["Gender"]);

            txtBirthYear.Text = row["BirthDate"].ToString().Substring(0, 4);
            //picGender.Image = Convert.ToInt32(row["Gender"]) == 1 ? Properties.Resources.male : Properties.Resources.female;
            //lueMaritalStatus.EditValue = Convert.ToInt32(row["MaritalStatus"]);
            //lueCountry.EditValue = Convert.ToInt32(row["Country"]);
            //txtBirthYear.Text = row["BirthDate"].ToString();
            //txtMobile.Text = row["Mobile"].ToString();
            //txtStreet.Text = row["Street"].ToString();
        }

        private void EncSearch(int encID)
        {
            txtEncID.ErrorText = null;

            var bllEnc = new BLL.Enc.EncList();
            _dtEncCur = bllEnc.EncSearch(encID, true);

            if (_dtEncCur.Rows.Count > 0)

            {
                EncFormEdit(false);

                lagPageService.Enabled = true;
                lagEncCash.Enabled = true;

                _EncID = Convert.ToInt32(_dtEncCur.Rows[0]["EncID"].ToString());
                txtEncID.Text = _EncID.ToString();

                txtFirstName.Text = _dtEncCur.Rows[0]["FirstName"].ToString();
                txtLastName.Text = _dtEncCur.Rows[0]["LastName"].ToString();

            }
            else
            {
                EncNull();
                txtEncID.ErrorText = "بیماری یافت نشد";
            }

            GL.SetEncCurVal(_DepID, _dtEncCur);
        }

        private void PatientSave()
        {
            if (_dtEncSearch.Rows.Count > 0)
                return;

            int crud = 0;
            crud = _EncEditMode == false ? 0 : 3;

            //_PatientID = _bllEncNew.PatientSave(GL.UserPersID, crud, _PatientID, txtFirstName.Text, txtLastName.Text,
            //    txtFatherName.Text, txtNationalCode.Text, (int)lueGender.EditValue, txtBirthYear.Text,
            //    (int)lueMaritalStatus.EditValue, (int)lueCountry.EditValue, txtMobile.Text,
            //    (int)lueCity.EditValue, txtAddress.Text);
        }
        #region Enc
        //----Enc----
        private void txtNationalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (txtNationalCode.Text.Length == 10)
                PatientSearch(txtNationalCode.Text);
        }

        private void EncSave()
        {
            //if (!CheckValidation())
            //    return;

            int crud = 0;
            //crud = _EditMode == false ? 0 : 3;

            _dtEncCur = _bllEncNew.EncSave(GL.UserPersID, crud, _EncID, _PatientID, _EncType, _DepID, (int)lueEncDr.EditValue,
                (int)lueInsurance.EditValue, txtInsuranceID.Text.Trim(),
                txtInsurancePageNumber.Text.Trim(), (int)lueEncChiefComplaint.EditValue, txtEncDescription.Text.Trim());

            EncSearch((int)_dtEncCur.Rows[0]["EncID"]);
            //GL.SetEncCurVal(_DepID, _dtEncCur);
        }
        private void txtNationalCode_Leave(object sender, EventArgs e)
        {
            if (txtNationalCode.Text.Length == 10)
                PatientSearch(txtNationalCode.Text);
        }

        private void txtFatherName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //    PatientSearch(txtNationalCode.Text);
        }



        private void txtFatherName_Leave(object sender, EventArgs e)
        {
            PatientSearch(txtNationalCode.Text);
        }

        private void lagEnc_Click(object sender, EventArgs e)
        {
            if (_dtEncCur == null) return;

            var ucControl = new UcEncSave()
            {
                _ModID = _ModID,
                _DepID = _DepID,
                _EditMode = true,
                _dtEncCur = _dtEncCur
            };
            ShowUCParent(ucControl);
        }

        private void lagPatient_Click(object sender, EventArgs e)
        {
            if (_dtEncCur == null) return;

            var ucControl = new UcPatientSave()
            {
                _ModID = _ModID,
                _DepID = _DepID,
                _EditMode = true,
                _dtEncCur = _dtEncCur
            };
            ShowUCParent(ucControl);
        }
        //--END--Enc----
        #endregion




        private void lueService_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lueService.EditValue) == 0)
                return;

            _rowCurServie = lueService.Properties.GetDataSourceRowByKeyValue(lueService.EditValue) as DataRowView;

            //lblServiceCode.Text = _rowCurServie["ServiceCode"].ToString();
            var price = Convert.ToInt32(_rowCurServie["Price"]);
            txtServicePrice.Text = price.ToString();

            if ((Convert.ToBoolean(_rowCurServie["IsManualPrice"])) == true)
            {
                txtServicePrice.ReadOnly = false;
                //lblManualPrice.Visible = true;
                //panServiceNum.Visible = true;
                txtServicePrice.Text = "0";
            }
        }


        private void ShowUCParent(object ucControl)
        {

            var panUC = (this.Parent) as DevExpress.XtraEditors.PanelControl;
            panUC.Controls.Clear();

            var myUC = (UserControl)ucControl;
            myUC.Anchor = AnchorStyles.Top;
            myUC.Anchor = AnchorStyles.Right;
            myUC.Dock = DockStyle.Fill;

            //panUC.Parent.Text += myUC.Tag.ToString();
            panUC.Controls.Add(myUC);
        }

        private void txtEncID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtEncID.Text))
                EncSearch(Convert.ToInt32(txtEncID.Text));
        }



        private void lagPatient_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            if (!CheckValidationPage())
                return;


            if (Convert.ToInt32(lueService.EditValue) > 0 && _rowCurServie != null)
            {
                DataRow rowService = _dtServiceReg.NewRow();
                rowService["RecNo"] = _dtServiceReg.Rows.Count + 1;

                rowService["CRUD"] = 0;
                rowService["SRID"] = 0;
                rowService["DrID"] = luePageDr.EditValue;
                rowService["DrName"] = luePageDr.Properties.GetDisplayText(luePageDr.EditValue);
                rowService["ServiceID"] = _rowCurServie["ServiceID"];
                rowService["ServiceCategory"] = _rowCurServie["ServiceCategory"];
                rowService["ServiceCode"] = _rowCurServie["ServiceCode"];
                rowService["ServiceName"] = _rowCurServie["Name"];
                rowService["Price"] = _rowCurServie["Price"];
                rowService["Num"] = txtServiceNum.Text;
                rowService["IsManualPrice"] = false;

                double price = Convert.ToDouble(_rowCurServie["Price"]);
                double num = Convert.ToDouble(txtServiceNum.Text);
                rowService["TotalAmount"] = price * num;

                bool IsManualPrice = Convert.ToBoolean(_rowCurServie["IsManualPrice"]);

                if (IsManualPrice)
                {
                    rowService["Price"] = 0;
                    rowService["IsManualPrice"] = IsManualPrice;
                    rowService["TotalAmount"] = Convert.ToDouble(txtServicePrice.EditValue);
                }

                _dtServiceReg.Rows.Add(rowService);
            }

            grdServiceReg.DataSource = _dtServiceReg;
            PageSave();

            ActiveControl = lueService;
        }
        private void PageSave()
        {
            if (!CheckValidationPage())
                return;

            var bllPage = new BLL.Page.Page();
            var dtPage = bllPage.PageSave(_ModID, _DepID, _StorageID, _StoragePeriodID, _EncID, GL.UserPersID, _PageID,
                null,null, 0, 0);

            _PageID = (int)dtPage.Rows[0]["PageID"];

            lagPageService.Text += "       کد برگه: " + dtPage.Rows[0]["PageID"] + " - " + dtPage.Rows[0]["Date"];

            //lblPageID.Text = 

            var bllEncServiceControl = new BLL.Enc.EncServiceControl();
            if (_ModID == 7 || _ModID == 8)
                bllEncServiceControl.EncServiceControlSave(_EncID, 2, _dtServiceReg.Select("CRUD<>4").Length > 0);


            ServiceRegSave();
            bllPage.PageAmountSave(_PageID);

            //EncPageLoad();
            //PageDataLoad();

            //ucShowTextRegSave.ShowText("خدمات ذخیره گردید", 5000);
        }


        private void ServiceRegSave()
        {
            if (_dtServiceReg.Rows.Count == 0)
                return;

            int crud;
            int TotalAmountManual = 0;
            bool IsManualPrice;


            DataTable dtService = null;
            var result = _dtServiceReg.Select("CRUD<>2");

            if (result.Length > 0)
                dtService = result.CopyToDataTable();

            if (dtService == null)
                return;

            foreach (DataRow row in dtService.Rows)
            {
                crud = Convert.ToInt32(row["CRUD"]);
                //قیمت دستی
                IsManualPrice = Convert.ToBoolean(row["IsManualPrice"]);
                if (IsManualPrice)
                    TotalAmountManual = Convert.ToInt32(row["TotalAmount"]);




                _bllServiceReg.ServiceRegSave
                (crud, _PageID, _StorageID, _StoragePeriodID, row["DrID"], row["SRID"], row["ServiceID"],
                    row["Num"], _rowCurServie["ServiceCategory"], TotalAmountManual
                );

                if (crud == 0)
                    row["CRUD"] = 2;

                if (crud == 4)
                    row.Delete();
            }
        }

        private bool CheckValidationPage()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(luePageDep.Text))
            {
                luePageDep.ErrorText = "لطفا بخش را انتخاب نمایید";

                isValid = false;

            }

            if ( (int)luePageDr.EditValue + (int)luePagePersonnel.EditValue == 0 )
            {
                luePageDr.ErrorText = "لطفا پزشک را انتخاب نمایید";
                luePagePersonnel.ErrorText = "لطفا تکنسین را انتخاب نمایید";

                isValid = false;
            }

            return isValid;
        }

        private void luePageDep_EditValueChanged(object sender, EventArgs e)
        {
            _dtDep = _bllDep.DepDataLoad(_DepID);
            _StorageID = Convert.ToInt32(_dtDep.Rows[0]["StorageID"]);
            _StoragePeriodID = Convert.ToInt32(_dtDep.Rows[0]["StoragePeriodID"]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void laiDiscount_Click(object sender, EventArgs e)
        {
            int i;
            i = 55;
        }


        private void EncPageLoad()
        {
            var bllPage = new BLL.Page.Page();
            _dtEncPage = bllPage.EncPageLoad(_EncID, _ModID, _DepID, _PageID);

            _PageListRecNo = _dtEncPage.Rows.Count;

            if (_PageListRecNo > 0)// && _PageID==0)
            {
                _PageID = Convert.ToInt32(_dtEncPage.Rows[_PageListRecNo - 1]["PageID"].ToString());
                PageDataLoad();
                //lblPageList.Text = "صفحه " + _PageListRecNo.ToString() + " از " + _dtEncPage.Rows.Count.ToString();
            }
        }

        private void PageDataLoad()
        {
            //if (_dtEncPage.Rows.Count == 0)
            //    return;

            //DataRow rowPage = _dtEncPage.Select("PageID=" + _PageID)[0];
            //lblPageID.Text = _PageID.ToString();
            //datPageDate.DateTime = DateTime.Parse(rowPage["Date"].ToString());
            //timPageTime.Time = DateTime.Parse(rowPage["Date"].ToString());

            //ServiceRegLoad(_PageID);
        }

        private void lagPageService_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            string btnName = e.Button.Properties.Tag.ToString();

            //switch (btnName)
            //{
            //    case "EncNew":
            //        EncNull();

            //        break;

            //    case "EncSave":
            //        PatientSave();
            //        EncSave();

            //        luePageDep.EditValue = lueEncDep.EditValue;
            //        luePageDr.EditValue = lueEncDep.EditValue;

            //        break;

            //}
            ////if (!CheckValidation())
            ////    return;



            //// ucShowTextSave.ShowText("اطلاعات ذخیره گردید", 5000);
        }

        private void ServiceRegLoad(int pageID)
        {
            //var bllServiceLerg = new BLL.ServiceReg.ServiceReg();
            //if (_WithDrug == true)
            //    _dtServiceReg = bllServiceLerg.ServiceRegLoad(pageID);
            //if (_WithDrug == false)
            //    _dtServiceReg = bllServiceLerg.ServiceRegWithoutDrugLoad(pageID);

            //grdServiceReg.DataSource = _dtServiceReg;
        }



    }
}
