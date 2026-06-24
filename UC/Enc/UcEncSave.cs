using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.APP.SettingApp;
using Saman.BLL.Enc;
using Saman.BLL.FormSetting;
using Saman.BLL.SAC;
using Saman.BLL.Sch;
using Saman.Form.Def;
using Saman.Properties;
using Control = System.Windows.Forms.Control;

namespace Saman.UC.Enc;

public partial class UcEncSave : XtraUserControl
{
    private readonly SettingApp _appSettingApp = new();
    private readonly EncNew _bllEncNew = new();
    private readonly EncSave _bllEncSave = new();
    private readonly bool _IsflwOprData;

    private readonly DataRow _rowEncSearch;

    private readonly Permission bllSACPermission = new();
    private int _Age;
    private string _curDateTime;
    private int _DrID;
    public DataTable _dtEncCur;

    private DataTable _dtEncSearch;
    private int _EncID;
    private int _EncStatus;

    public byte _EncType = 0;

    private string _FirstName,
        _LastName,
        _FatherName,
        _DrName,
        _InsuranceName,
        _InsuranceName2,
        _EncStartDate,
        _EncEndDate,
        _EncChiefComplaint,
        _EncDescription,
        _CountryName;

    public int _GenderId,
        _MaritalStatusId,
        _PatientCountryId,
        _EncDepId,
        _EncServiceId,
        _EncInsuranceId,
        _EncInsurance2Id,
        _PatientCityId;

    public int _Height;
    private int _InsuranceID;

    private bool _IsFatherNameMandatory;

    //--Sch----------------------
    public bool _IsFromSch = false;
    private int _PatientID;

    public int _SchID = 0;
    //--END--Sch----------------------

    private string _Title1Prefix;
    public int _Weight;


    public UcEncSave()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public bool _EditModePatient { get; set; }
    public bool _EditModeEnc { get; set; }
    public bool _Enabled { get; set; }
    public string _NationalCode { get; set; }

    private void UcEncNew_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        ActiveControl = flpMain;
        ActiveControl = txtPatientNationalCode;

        var appSDate = new SDate();
        _curDateTime = appSDate.ShamsiDate();
        datEncStartDate.EditValue = Convert.ToDateTime(_curDateTime);
        timEncStartTime.EditValue = DateTime.Parse(_curDateTime.Substring(11, 5));


        FormSetting();
        MaskSet();
        FillLookup();
        PatientAndEncLoad();
        CheckReadOnly();
        ShortKey();
    }

    private void PatientAndEncLoad()
    {
        if (_EditModePatient) _Title1Prefix = "ویرایش پروفایل:";

        if (_EditModeEnc) _Title1Prefix = "ویرایش مراجعه:";

        if (_IsFromSch && _EncID == 0)
        {
            SchLoad();
        }
        else
        {
            if (_EditModePatient || (_EditModeEnc && _dtEncCur.Rows.Count > 0))
            {
                PatientPropertiesSet(_dtEncCur);
                picGender.Image = Convert.ToByte(_dtEncCur.Rows[0]["Gender"]) == 1 ? Resources.male : Resources.female;
                _EncStatus = Convert.ToInt32(_dtEncCur.Rows[0]["EncStatus"]);
                _PatientID = Convert.ToInt32(_dtEncCur.Rows[0]["PatientID"]);
                _EncID = Convert.ToInt32(_dtEncCur.Rows[0]["EncID"]);
                _EncStatus = Convert.ToInt32(_dtEncCur.Rows[0]["EncStatus"]);
                _InsuranceID = Convert.ToInt32(_dtEncCur.Rows[0]["Insurance"]);
                _DrName = _dtEncCur.Rows[0]["DrName"].ToString();
                _InsuranceName = _dtEncCur.Rows[0]["InsuranceName"].ToString();
                _InsuranceName2 = _dtEncCur.Rows[0]["InsuranceName2"].ToString();
                _EncStartDate = _dtEncCur.Rows[0]["StartDate"].ToString();
                _EncEndDate = _dtEncCur.Rows[0]["EndDate"].ToString();
                _EncChiefComplaint = _dtEncCur.Rows[0]["ChiefComplaintName"].ToString();
                _EncDescription = _dtEncCur.Rows[0]["Description"].ToString();


                #region EncLoad

                txtEncID.Text = _EncID.ToString();
                txtPaitentID.Text = _PatientID.ToString();
                txtCompanionAddress.Text = _dtEncCur.Rows[0]["CompanionAddress"].ToString();
                txtCompanionFullName.Text = _dtEncCur.Rows[0]["CompanionFullName"].ToString();
                txtCompanionTel.Text = _dtEncCur.Rows[0]["CompanionTel"].ToString();
                lueEncDep.EditValue = Convert.ToInt32(_dtEncCur.Rows[0]["DepID"]);
                datEncStartDate.EditValue = Convert.ToDateTime(_EncStartDate);
                timEncStartTime.EditValue = Convert.ToDateTime(_EncStartDate);
                lueEncDr.EditValue = Convert.ToInt32(_dtEncCur.Rows[0]["DrID"]);
                txtEncRefDrID.Text = _dtEncCur.Rows[0]["RefDrID"].ToString();
                if (!string.IsNullOrEmpty(_dtEncCur.Rows[0]["PresDate"].ToString()))
                    datEncPresDate.EditValue = Convert.ToDateTime(_dtEncCur.Rows[0]["PresDate"]);
                lueEncInsurance.EditValue = _InsuranceID;
                txtEncInsuranceNumber.Text = _dtEncCur.Rows[0]["InsuranceNumber"].ToString();
                txtEncInsurancePageNumber.Text = _dtEncCur.Rows[0]["InsurancePageNumber"].ToString();
                //txtEncInsuranceNumber.Text = _dtEncCur.Rows[0]["InsuranceSerial"].ToString();
                if (!string.IsNullOrEmpty(_dtEncCur.Rows[0]["InsuranceExpireDate"].ToString()))
                    datInsuranceExpireDate.EditValue = Convert.ToDateTime(_dtEncCur.Rows[0]["InsuranceExpireDate"]);

                lueEncChiefComplaint.EditValue = Convert.ToInt32(_dtEncCur.Rows[0]["ChiefComplaint"]);
                lueEncService.EditValue = Convert.ToInt32(_dtEncCur.Rows[0]["EncService"]);
                chkEncIsEmerg.EditValue = Convert.ToBoolean(_dtEncCur.Rows[0]["IsEmerg"]);
                txtEncDescription.Text = _dtEncCur.Rows[0]["Description"].ToString();
                lueEncInsurance2.EditValue = Convert.ToInt32(_dtEncCur.Rows[0]["Insurance2"]);
                txtEncInsuranceNumber2.Text = _dtEncCur.Rows[0]["InsuranceNumber2"].ToString();
                txtSDrFixAmount.Text = _dtEncCur.Rows[0]["SDrFixAmount"].ToString();
                chkIsSDrAcceptAmount.EditValue = (bool)_dtEncCur.Rows[0]["IsSDrAcceptAmount"];
                if (datOprReserveDate.Visible &&
                    !string.IsNullOrEmpty(_dtEncCur.Rows[0]["OprReserveDate"].ToString()))
                    datOprReserveDate.EditValue = Convert.ToDateTime(_dtEncCur.Rows[0]["OprReserveDate"]);

                #endregion
            }
        }

        EncInfoTitleSet();
    }

    private void PatientPropertiesSet(DataTable dt)
    {
        _FirstName = txtPatientFirstName.Text = dt.Rows[0]["FirstName"].ToString();
        _LastName = txtPatientLastName.Text = dt.Rows[0]["LastName"].ToString();
        _FatherName = txtPatientFatherName.Text = dt.Rows[0]["FatherName"].ToString();
        _NationalCode = txtPatientNationalCode.Text = dt.Rows[0]["NationalCode"].ToString();
        _Age = Convert.ToInt32(dt.Rows[0]["Age"]);
        _CountryName = dt.Rows[0]["CountryName"].ToString();


        txtPatientIDCardNumber.Text = dt.Rows[0]["IDCardNumber"].ToString();
        luePatientGender.EditValue = Convert.ToInt32(dt.Rows[0]["Gender"]);
        luePatientBirthArea.EditValue = dt.Rows[0]["BirthArea"];
        datPatientBirthDate.DateTime = DateTime.Parse(dt.Rows[0]["BirthDate"].ToString());
        luePatientEducationLevel.EditValue = Convert.ToInt32(dt.Rows[0]["EducationLevel"]);
        lueMaritalStatus.EditValue = Convert.ToInt32(dt.Rows[0]["MaritalStatus"]);
        luePatientCountry.EditValue = Convert.ToInt32(dt.Rows[0]["Country"]);
        txtPatientPassportID.Text = dt.Rows[0]["PassportID"].ToString();
        luePatientReligion.EditValue = Convert.ToInt32(dt.Rows[0]["Religion"]);
        luePatientJob.EditValue = Convert.ToInt32(dt.Rows[0]["Job"]);
        luePatientCity.EditValue = Convert.ToInt32(dt.Rows[0]["CCity"]);
        txtPatientMobile.Text = dt.Rows[0]["Mobile"].ToString();
        txtPatientTel.Text = dt.Rows[0]["Tel"].ToString();
        txtPatientStreet.Text = dt.Rows[0]["Street"].ToString();
        txtPatientEmail.Text = dt.Rows[0]["Email"].ToString();
    }

    private void EncInfoTitleSet()
    {
        ucEncInfoTitle.TitlePrefix = _Title1Prefix;
        ucEncInfoTitle.FullName = _FirstName + " " + _LastName;
        ucEncInfoTitle.FatherName = _FatherName;
        ucEncInfoTitle.Age = _Age.ToString();
        ucEncInfoTitle.DrName = _DrName;
        ucEncInfoTitle.InsuranceName = _InsuranceName;
        ucEncInfoTitle.InsuranceName2 = _InsuranceName2;
        ucEncInfoTitle.EncStartDate = _EncStartDate;
        ucEncInfoTitle.EncEndDate = _EncEndDate;
        ucEncInfoTitle.ChiefComplaintName = _EncChiefComplaint;
        ucEncInfoTitle.Description = _EncDescription;
        ucEncInfoTitle.Country = _CountryName;
    }

    private void CheckReadOnly()
    {
        _Enabled = true;
        if (_EncStatus > 3) _Enabled = false;

        btnSave.Enabled = _Enabled;
    }

    private void MaskSet()
    {
        txtPatientFirstName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtPatientLastName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtPatientFatherName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtPatientIDCardNumber.Properties.Mask.EditMask = App.UI.MaskNum;
        txtPatientPassportID.Properties.Mask.EditMask = App.UI.MaskNum;
        txtPatientMobile.Properties.Mask.EditMask = App.UI.MaskNum;
        txtPatientTel.Properties.Mask.EditMask = App.UI.MaskNum;
        txtCompanionTel.Properties.Mask.EditMask = App.UI.MaskNum;
        txtCompanionFullName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtPatientEmail.Properties.Mask.EditMask = App.UI.MaskEnglishWithNum;
        txtEncInsuranceNumber.Properties.Mask.EditMask = App.UI.MaskNum;
        txtEncInsurancePageNumber.Properties.Mask.EditMask = App.UI.MaskNum;
        txtEncInsuranceNumber2.Properties.Mask.EditMask = App.UI.MaskNum;
        txtPatientEmail.Properties.Mask.EditMask = App.UI.MaskEmail;
    }

    private void ShortKey()
    {
        if (!_Enabled) return;
        msiSave.Click += btnSave_Click;
        msiClose.Click += btnClose_Click;
        panMain.Focus();
    }

    private void SchLoad()
    {
        var bllSch = new Sch();

        var dt = bllSch.SchRecLoad(1, _SchID);

        txtPatientNationalCode.Text = dt.Rows[0]["NationalCode"].ToString();

        if (_PatientID == 0)
        {
            _FirstName = txtPatientFirstName.Text = dt.Rows[0]["FirstName"].ToString();
            _LastName = txtPatientLastName.Text = dt.Rows[0]["LastName"].ToString();
            _FatherName = txtPatientFatherName.Text = dt.Rows[0]["FatherName"].ToString();
        }

        var year = Convert.ToInt32(_curDateTime.Substring(0, 4));
        var age = Convert.ToInt32(dt.Rows[0]["Age"]);
        datPatientBirthDate.DateTime = DateTime.Parse((year - age) + "/01/01");
        _Age = Convert.ToInt32(dt.Rows[0]["Age"]);

        txtPatientMobile.Text = dt.Rows[0]["Mobile"].ToString();
        txtEncDescription.Text = dt.Rows[0]["Description"].ToString();
        _Height = Convert.ToInt32(dt.Rows[0]["Height"]);
        _Weight = Convert.ToInt32(dt.Rows[0]["Weight"]);

        luePatientCountry.EditValue = Convert.ToInt32(dt.Rows[0]["Country"]);
        _CountryName = dt.Rows[0]["CountryName"].ToString();

        luePatientCity.EditValue = Convert.ToInt32(dt.Rows[0]["City"]);
        luePatientGender.EditValue = Convert.ToInt32(dt.Rows[0]["Gender"]);
        lueEncDr.EditValue = Convert.ToInt32(dt.Rows[0]["DrID"]);

        txtEncRefDrID.Text = dt.Rows[0]["RefDrID"].ToString();
        lueEncDep.EditValue = Convert.ToInt32(dt.Rows[0]["DepID"]);
        lueEncChiefComplaint.EditValue = Convert.ToInt32(dt.Rows[0]["ChiefComplaint"]);
        _EncChiefComplaint = dt.Rows[0]["ChiefComplaintName"].ToString();

        lueEncService.EditValue = Convert.ToInt32(dt.Rows[0]["EncService"]);
        lueEncInsurance.EditValue = Convert.ToInt32(dt.Rows[0]["Insurance"]);

        Search(txtPatientNationalCode.Text);
    }

    private void FillLookup()
    {
        UL.FillLookup(luePatientGender, _GenderId.ToString(), cSet.cnStringA, false, "ID,Name", "vADR", "Gender",
            "ADName='Gender'",
            null);
        UL.FillLookup(lueMaritalStatus, _MaritalStatusId.ToString(), cSet.cnStringA, false, "ID,Name", "vADR",
            "MaritalStatus",
            "ADName='MaritalStatus'", null);
        UL.FillLookup(luePatientCountry, _PatientCountryId.ToString(), cSet.cnStringA, false, "ID,Name", "vADR",
            "Country",
            "ADName='Country'", null);
        UL.FillLookup(lueEncDep, _EncDepId.ToString(), cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);
        UL.FillLookup(lueEncDr, _DrID.ToString(), cSet.cnStringA, false, "*", "vDr", "Dr", "IsDisable=0", null);
        UL.FillLookup(lueEncChiefComplaint, "0", cSet.cnStringA, false, "ID,Name", "vADR", "ChiefComplaint",
            "ADName='ChiefComplaint' AND IsDisable = 0", null);
        UL.FillLookup(lueEncService, _EncServiceId.ToString(), cSet.cnStringA, false, "ID,Name", "vADR", "EncService",
            "ADName='EncService'", null);
        UL.FillLookup(lueEncInsurance, _EncInsuranceId.ToString(), cSet.cnStringA, false, "*", "Insurance", "Insurance",
            "IsDisable = 0 AND InsuranceType <> 2", null);
        UL.FillLookup(lueEncInsurance2, _EncInsurance2Id.ToString(), cSet.cnStringA, false, "*", "Insurance",
            "Insurance",
            "IsDisable = 0 AND InsuranceType = 1 OR InsuranceID = 0", null);
        UL.FillLookup(luePatientCity, _PatientCityId.ToString(), cSet.cnStringA, false, "ID,Name", "vADRCity", "City",
            null, null);
        UL.FillLookup(luePatientBirthArea, "0", cSet.cnStringA, false, "ID,Name", "vADR", "City", "ADName='City'",
            null);
        UL.FillLookup(luePatientEducationLevel, "0", cSet.cnStringA, false, "ID,Name", "vADR", "EducationLevel",
            "ADName='EducationLevel'", null);
        UL.FillLookup(luePatientReligion, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Religion",
            "ADName='Religion'", null);
        UL.FillLookup(luePatientJob, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Job", "ADName='Job'", null);
    }

    private void FormSetting()
    {
        _dtFormSetting = _bllFormSetting.FormSettingLoad(_FormID);
        FormSettingUI();

        var IsBirthYearMandatory = _bllFormSetting.UiIsRequied(_dtFormSetting, "BirthDate");
        if (!IsBirthYearMandatory)
        {
            var appSDate = new SDate();
            var curDateTime = appSDate.ShamsiDate();
            datPatientBirthDate.Text = curDateTime.Substring(0, 4);
        }

        _IsFatherNameMandatory = _bllFormSetting.UiIsRequied(_dtFormSetting, "PatientFatherName");
        _DrID = _bllFormSetting.UiDefaultValueInt(_dtFormSetting, "EncDr");
        _GenderId = _bllFormSetting.UiDefaultValueInt(_dtFormSetting, "PatientGender");
        _MaritalStatusId = _bllFormSetting.UiDefaultValueInt(_dtFormSetting, "PatientMaritalStatus");
        _PatientCountryId = _bllFormSetting.UiDefaultValueInt(_dtFormSetting, "PatientCountry");
        _EncDepId = _bllFormSetting.UiDefaultValueInt(_dtFormSetting, "EncDep");
        _EncServiceId = _bllFormSetting.UiDefaultValueInt(_dtFormSetting, "EncService");
        _EncInsuranceId = _bllFormSetting.UiDefaultValueInt(_dtFormSetting, "EncInsurance");
        _EncInsurance2Id = _bllFormSetting.UiDefaultValueInt(_dtFormSetting, "EncInsurance2");
        _PatientCityId = _bllFormSetting.UiDefaultValueInt(_dtFormSetting, "PatientCity");


        //int UiBirtDateVal1 = _bllFormSetting.GetVal1(_dtFormSetting, "PatientBirthDate");
        //if (UiBirtDateVal1 == 1)
        //{
        //    datPatientBirthDate.YearOnlyInput = true;
        //}

        //bool IsBirthYearMandatory = _bllFormSetting.UiIsRequied(_dtFormSetting, "BirthDate");
        //if (IsBirthYearMandatory == false)
        //{
        //    App.SDate appSDate = new SDate();
        //    string curDateTime = appSDate.ShamsiDate();
        //    datPatientBirthDate.Text = curDateTime.Substring(0, 4);
        //}

        //if (UiType == 1)
        //{
        //    var ctrlHeaderTitle = ctrl[0].Controls.Find(UiTypePrefix + UiName, true);
        //    ctrlHeaderTitle[0].Text = UiCaption;
        //}

        //if (UiType == 2)
        //{
        //    var ctrlHeaderTitle = ctrl[0].Controls.Find(UiTypePrefix + UiName, true);
        //    ctrlHeaderTitle[0].Text = UiCaption;
        //}
        //    #endregion
        //}
    }

    private void FormSettingUI()
    {
        FlowLayoutPanel flp = null;

        //int b = flo.Controls.Count;
        string UiName, UiCaption, UiTypePrefix;
        var UiType = 0;


        Control[] ctrl;

        //bool ctrlTabStop = false;
        int FormSubID = 0, UiRecNo = 0;


        var dtFormSetting = _dtFormSetting.Select("IsUI=1").CopyToDataTable();

        foreach (DataRow row in dtFormSetting.Rows)
        {
            FormSubID = Convert.ToInt32(row["FormSubID"]);
            UiName = row["UiName"].ToString();
            UiType = Convert.ToInt32(row["Uitype"]);
            UiTypePrefix = row["UiTypePrefix"].ToString();

            UiCaption = row["UiCaption"].ToString();
            UiRecNo = Convert.ToInt32(row["RecNo"]);

            flp = FormSubID == 1 ? flpMain : flpSide;

            ctrl = flp.Controls.Find("pan" + UiName, false);
            if (ctrl.Length == 0) continue;

            ctrl[0].Enabled = Convert.ToBoolean(row["Enabled"]);
            ctrl[0].Visible = Convert.ToBoolean(row["Visible"]);
            //ctrl[0].ReadOnly = Convert.ToBoolean(row["ReadOnly"]);


            var lblCaption = ctrl[0].Controls.Find("lbl" + UiName, false);
            if (ctrl.Length > 0) lblCaption[0].Text = UiCaption;
        }
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (luePatientCountry.EditValue.ToString() == "1" && string.IsNullOrEmpty(txtPatientNationalCode.Text))
        {
            txtPatientNationalCode.ErrorText = "کد ملی را وارد نمایید";
            isValid = false;
        }

        var PatientCityIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "PatientCity");
        if (PatientCityIsRequired && luePatientCity.EditValue.ToString() == "0" &&
            string.IsNullOrEmpty(luePatientCity.Text))
        {
            luePatientCity.ErrorText = "شهرستان را وارد نمایید";
            isValid = false;
        }

        var PatientMobileIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "PatientMobile");
        if (PatientMobileIsRequired && string.IsNullOrEmpty(txtPatientMobile.Text))
        {
            txtPatientMobile.ErrorText = "موبایل را وارد نمایید";
            isValid = false;
        }

        var PatientTelIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "PatientTel");
        if (PatientTelIsRequired && string.IsNullOrEmpty(txtPatientTel.Text))
        {
            txtPatientTel.ErrorText = "تلفن را وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(txtPatientFirstName.Text) && string.IsNullOrEmpty(txtPatientLastName.Text))
        {
            txtPatientFirstName.ErrorText = "نام را وارد نمایید";
            txtPatientLastName.ErrorText = "نام خانوادگی را وارد نمایید";
            isValid = false;
        }

        if (_IsFatherNameMandatory && string.IsNullOrEmpty(txtPatientFatherName.Text))
        {
            txtPatientFatherName.ErrorText = "نام پدر را وارد نمایید";
            isValid = false;
        }

        var PatientGenderIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "PatientGender");
        if (PatientGenderIsRequired && luePatientGender.EditValue.ToString() == "0" &&
            string.IsNullOrEmpty(luePatientGender.Text))
        {
            luePatientGender.ErrorText = "جنسیت را وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(datPatientBirthDate.Text))
        {
            datPatientBirthDate.ErrorText = "تاریخ تولد را وارد نمایید";
            isValid = false;
        }

        var NationalCodeIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "NationalCode");
        if (NationalCodeIsRequired || (luePatientCountry.EditValue.ToString() == "1" &&
                                       string.IsNullOrEmpty(txtPatientNationalCode.Text)))
        {
            txtPatientNationalCode.ErrorText = "کد ملی را وارد نمایید";
            isValid = false;
        }

        var EncDepIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "EncDep");
        if (EncDepIsRequired && (lueEncDep.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueEncDep.Text)))
        {
            lueEncDep.ErrorText = "نام بخش را انتخاب نمایید";
            isValid = false;
        }

        var EncDrIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "EncDr");
        if (EncDrIsRequired && (lueEncDr.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueEncDr.Text)))
        {
            lueEncDr.ErrorText = "نام پزشک را انتخاب نمایید";
            isValid = false;
        }

        var EncChiefComplaintIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "EncChiefComplaint");
        if (EncChiefComplaintIsRequired && (lueEncChiefComplaint.EditValue.ToString() == "0" ||
                                            string.IsNullOrEmpty(lueEncChiefComplaint.Text)))
        {
            lueEncChiefComplaint.ErrorText = "دلیل مراجعه را انتخاب نمایید";
            isValid = false;
        }

        var EncServiceIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "EncService");
        if (EncServiceIsRequired &&
            (lueEncService.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueEncService.Text)))
        {
            lueEncService.ErrorText = "سرویس مراجعه را انتخاب نمایید";
            isValid = false;
        }

        var OprReserveDateIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "OprReserveDate");
        if (OprReserveDateIsRequired && panOprReserveDate.Visible && string.IsNullOrEmpty(datOprReserveDate.Text))
        {
            datOprReserveDate.ErrorText = "تاریخ جراحی را وارد نمایید";
            isValid = false;
        }

        var EncStartDateIsRequired = _bllFormSetting.UiIsRequied(_dtFormSetting, "EncStartDate");
        if (EncStartDateIsRequired && datEncStartDate.Enabled && string.IsNullOrEmpty(datEncStartDate.Text))
        {
            datEncStartDate.ErrorText = "تاریخ پذیرش را وارد نمایید";
            isValid = false;
        }

        if (Convert.ToInt32(lueEncInsurance.GetColumnValue("IsMandatorySerialNumber")) == 1 &&
            string.IsNullOrEmpty(txtEncInsuranceNumber.Text))
        {
            txtEncInsuranceNumber.ErrorText = "شماره بیمه را وارد نمایید";
            isValid = false;
        }

        if (Convert.ToInt32(lueEncInsurance.GetColumnValue("IsMandatoryPageNumber")) == 1 &&
            string.IsNullOrEmpty(txtEncInsurancePageNumber.Text))
        {
            txtEncInsurancePageNumber.ErrorText = "شماره صفحه بیمه را وارد نمایید";
            isValid = false;
        }


        if (Convert.ToInt32(lueEncInsurance.GetColumnValue("IsMandatoryEndDate")) == 1 &&
            string.IsNullOrEmpty(datInsuranceExpireDate.Text))
        {
            datInsuranceExpireDate.ErrorText = "اعتبار بیمه را وارد نمایید";
            isValid = false;
        }

        if (lueEncDep.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueEncDep.Text))
        {
            lueEncDep.ErrorText = "نام بخش را وارد نمایید";
            isValid = false;
        }

        if (lueEncDr.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueEncDr.Text))
        {
            lueEncDr.ErrorText = "پزشک را وارد نمایید";
            isValid = false;
        }

        if (lueEncInsurance.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueEncInsurance.Text))
        {
            lueEncInsurance.ErrorText = "نوع بیمه را وارد نمایید";
            isValid = false;
        }


        if (txtPatientEmail.Text.Length > 0)
        {
            if (EmailValidationCheck(txtPatientEmail.Text)) return isValid;

            txtPatientEmail.ErrorText = "ایمیل را وارد نمایید";
            isValid = false;
        }

        return isValid;
    }

    private bool EmailValidationCheck(string email)
    {
        try
        {
            var mail = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void PatientSearch()
    {
        _dtEncSearch = _bllEncNew.PatientSearch(txtPatientFirstName.Text, txtPatientLastName.Text,
            txtPatientFatherName.Text,
            txtPatientNationalCode.Text);
    }

    private void btnPatientSelect_Click(object sender, EventArgs e)
    {
        panEncSearch.Visible = false;
        var row = grvSearch.GetFocusedDataRow();
        _PatientID = Convert.ToInt32(row["PatientID"]);
        SetFromSearch(_PatientID, null);
    }

    private void gridSearch_DoubleClick(object sender, EventArgs e)
    {
        btnPatientSelect_Click(sender, e);
    }

    private void btnPatientNew_Click(object sender, EventArgs e)
    {
        panEncSearch.Visible = false;
    }

    private void txtFatherName_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13) Search(txtPatientNationalCode.Text);
    }

    private void Search(string NationalCode)
    {
        PatientSearch();

        if (_dtEncSearch.Rows.Count > 0)
        {
            if (NationalCode.Length < 10)
            {
                gridSearch.DataSource = _dtEncSearch;
                panEncSearch.Height = 680;
                panEncSearch.Visible = true;
            }

            else
            {
                SetFromSearch(0, NationalCode);
            }
        }
    }

    private void SetFromSearch(int PatientID, string NationalCode)
    {
        DataTable dt = null;

        if (_PatientID > 0) dt = _dtEncSearch.Select("PatientID=" + _PatientID).CopyToDataTable();

        if (_PatientID == 0) dt = _dtEncSearch.Select("NationalCode=" + NationalCode).CopyToDataTable();

        if (!_EditModePatient)
        {
            panEncSearch.Visible = false;
            txtPatientNationalCode.ReadOnly = true;
            txtPatientFirstName.ReadOnly = true;
            txtPatientLastName.ReadOnly = true;
            txtPatientFatherName.ReadOnly = true;
            luePatientGender.ReadOnly = true;
            lueMaritalStatus.ReadOnly = true;
            luePatientCountry.ReadOnly = true;
            datPatientBirthDate.ReadOnly = true;
            txtPatientMobile.ReadOnly = true;
            txtPatientStreet.ReadOnly = true;
        }


        PatientPropertiesSet(dt);
    }

    private void PatientSave()
    {
        //if (_dtEncSearch != null) 
        //    if (_dtEncSearch.Rows.Count > 0) 
        //        return;

        var com = new SqlCommand();
        var CRUD = 0;
        CRUD = !_EditModePatient ? 0 : 3;

        var dtPatient = _bllEncNew.PatientSave(CRUD, _PatientID, txtPatientFirstName.Text.Trim(),
            txtPatientLastName.Text.Trim(), txtPatientFatherName.Text.Trim(), txtPatientNationalCode.Text.Trim(),
            Convert.ToInt32(luePatientGender.EditValue), datPatientBirthDate.Text.Trim(),
            Convert.ToInt32(lueMaritalStatus.EditValue), Convert.ToInt32(luePatientCountry.EditValue),
            txtPatientMobile.Text.Trim(), Convert.ToInt32(luePatientCity.EditValue), txtPatientStreet.Text.Trim(),
            txtPatientIDCardNumber.Text.Trim(),
            Convert.ToInt32(luePatientBirthArea.EditValue), Convert.ToInt32(luePatientReligion.EditValue),
            Convert.ToInt32(luePatientJob.EditValue), txtPatientPassportID.Text.Trim(),
            Convert.ToInt32(luePatientEducationLevel.EditValue),
            txtPatientEmail.Text.Trim(), txtPatientTel.Text.Trim(), GL.UserPersID);

        _PatientID = Convert.ToInt32(dtPatient.Rows[0]["PatientID"]);
    }

    private void EncSave()
    {
        if (!CheckValidation()) return;

        #region ChangeInsurance

        if (Convert.ToInt32(lueEncInsurance.EditValue) != _InsuranceID && _InsuranceID > 0)
        {
            var EncTotalPayAmount = _bllEncSave.EncTotalPayAmount(_EncID);
            if (EncTotalPayAmount > 0)
            {
                var strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                                 + "\n"
                                 + "\n بیمار دارای پراختی می باشد آیا از تغییر بیمه اطمینان دارید ؟";
                var sui = new SUI();
                var result = sui.FormShowMessage("کد پذیرش : " + _EncID, strMessage);

                if (result != DialogResult.Yes)
                {
                    lueEncInsurance.EditValue = _InsuranceID;
                    return;
                }
            }

            _bllEncSave.EncInsuranceChange(_EncID, Convert.ToInt32(lueEncInsurance.EditValue), GL.UserPersID);
        }

        #endregion

        string OprReserveDate = null;
        if (datOprReserveDate.Visible) OprReserveDate = datOprReserveDate.Text + " 00:00";

        string EncStartDate = null;
        if (datEncStartDate.Enabled) EncStartDate = datEncStartDate.Text + " " + timEncStartTime.Text;

        var CRUD = 0;
        CRUD = !_EditModeEnc ? 0 : 3;
        _dtEncCur = _bllEncSave.EncSaveFull(CRUD, _EncID, _PatientID, EncStartDate, _EncType, chkEncIsEmerg.Checked
            , Convert.ToInt32(lueEncDep.EditValue), Convert.ToInt32(lueEncDr.EditValue),
            Convert.ToInt32(txtEncRefDrID.Text),
            datEncPresDate.Text.Trim(), Convert.ToInt32(lueEncService.EditValue),
            Convert.ToInt32(lueEncChiefComplaint.EditValue), txtEncDescription.Text,
            Convert.ToInt32(lueEncInsurance.EditValue), txtEncInsuranceNumber.Text,
            "0", txtEncInsurancePageNumber.Text, datInsuranceExpireDate.Text.Trim(),
            false, Convert.ToInt64(txtSDrFixAmount.EditValue), chkIsSDrAcceptAmount.Checked,
            OprReserveDate, _Height, _Weight, txtCompanionFullName.Text.Trim(), txtCompanionTel.Text.Trim(),
            txtCompanionAddress.Text.Trim(), Convert.ToInt32(lueEncInsurance2.EditValue),
            txtEncInsuranceNumber2.Text, GL.UserPersID);

        _EncID = Convert.ToInt32(_dtEncCur.Rows[0]["EncID"]);

        GL.SetEncCurVal(_DepID, _dtEncCur);
    }

    private void txtNationalCode_Validating(object sender, CancelEventArgs e)
    {
        if (txtPatientNationalCode.Text.Length > 0 &&
            !App.UI.NationalCodeCValidate(txtPatientNationalCode.Text, out var errorText))
        {
            e.Cancel = true;
            txtPatientNationalCode.ErrorText = errorText;
        }
    }

    private void txtNationalCode_Leave(object sender, EventArgs e)
    {
        if (txtPatientNationalCode.Text.Length == 10) Search(txtPatientNationalCode.Text);
    }

    private void lueCountry_EditValueChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(luePatientCountry.EditValue) > 1)
        {
            txtPatientFirstName.Properties.Mask.EditMask = App.UI.MaskEnglish;
            txtPatientLastName.Properties.Mask.EditMask = App.UI.MaskEnglish;
            txtPatientFatherName.Properties.Mask.EditMask = App.UI.MaskEnglish;
        }
        else
        {
            txtPatientFirstName.Properties.Mask.EditMask = App.UI.MaskPersian;
            txtPatientLastName.Properties.Mask.EditMask = App.UI.MaskPersian;
            txtPatientFatherName.Properties.Mask.EditMask = App.UI.MaskPersian;
        }
    }

    private void txtFatherName_Leave(object sender, EventArgs e)
    {
        Search(txtPatientNationalCode.Text);
    }

    private void txtLastName_Leave(object sender, EventArgs e)
    {
        if (!_IsFatherNameMandatory) Search(txtPatientNationalCode.Text);
    }

    private void lblRefDr_Click(object sender, EventArgs e)
    {
        var frm = new FrmDefADR
        {
            _ADID = 53
        };
        var result = frm.ShowDialog();

        txtEncRefDrID.Text = frm._ID.ToString();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucEncCur = new UcEncCur
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucEncCur);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        if (_EditModePatient) PatientSave();

        if (_EditModeEnc) EncSave();

        if (!_EditModePatient && !_EditModeEnc)
        {
            PatientSave();
            EncSave();
        }

        if (_IsFromSch && _SchID > 0)
        {
            var bllSch = new Sch();
            bllSch.SchEncNew(-1, _EncID, _SchID);
        }

        var panUc = Parent as PanelControl;
        panUc.Controls.Clear();

        var ucEncCur = new UcEncCur
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Dock = DockStyle.Fill,
            _ModID = _ModID,
            _DepID = _DepID
        };
        panUc.Controls.Add(ucEncCur);
    }

    private void datPatientBirthDate_Leave(object sender, EventArgs e)
    {
        if (datPatientBirthDate.Text.Length == 4)
        {
            //datPatientBirthDate.DateTime = DateTime.Parse(datPatientBirthDate.Text + "/01/01");
        }
    }

    private void txtRefDrID_EditValueChanged(object sender, EventArgs e)
    {
        var bllEncNew = new EncNew();
        lblEncRefDrName.Text = bllEncNew.RefDrName(txtEncRefDrID.Text);
    }

    private void lueEncInsurance2_EditValueChanged(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(lueEncInsurance2.EditValue) == 0)
        //    return;
        //if (_a == Convert.ToInt32(lueEncInsurance2.EditValue))
        //    return;

        //var frmInsurance2Intro = new Saman.Form.Insurance.FrmInsurance2Intro()
        //{
        //    _ModID = _ModID,
        //    _DepID = _DepID,
        //    _EncID = _EncID,

        //};
        //var result = frmInsurance2Intro.ShowDialog();
    }

    #region FormSetting

    private readonly int _FormID = 1;
    private readonly FormSetting _bllFormSetting = new();
    private DataTable _dtFormSetting = new();

    #endregion
}