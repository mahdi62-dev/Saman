using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using LIB;
using Saman.APP;
using Saman.BLL.Def;
using Control = App.Control;

namespace Saman.UC.Personnel;

public partial class UcPersonNew : XtraUserControl
{
    private static byte[] _file;
    private readonly PersDef _bllPersonnel = new();
    private readonly ImageLib _ImageLib = new();
    private int _CRUD;

    private DataTable _dtPersonnel;

    private string _EncryptDrowssap;
    private Image _Img;

    private int _PersID, _ResuID;

    private int _PersTypeID;
    private string _PicPersonFilePath;

    public UcPersonNew()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }

    private int _PersRecNo { get; set; }

    public bool _ReadOnly { get; set; }

    private void UcPersonNew_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        ActiveControl = luePers;

        FillLookup();

        SetMask();
    }

    private void FillLookup()
    {
        UL.FillLookup(luePers, "0", cSet.cnStringERP, false, "PersID,Name", "vPersonnel", "vPersonnel", null,
            "PersID");
        UL.FillLookup(lueGender, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Gender", "ADName='Gender'", null);
        UL.FillLookup(lueBirthArea, "0", cSet.cnStringA, false, "ID,Name", "vADR", "City", "ADName='City'", null);
        UL.FillLookup(lueEducationLevel, "0", cSet.cnStringA, false, "ID,Name", "vADR", "EducationLevel",
            "ADName='EducationLevel'", null);
        UL.FillLookup(lueMaritalStatus, "0", cSet.cnStringA, false, "ID,Name", "vADR", "MaritalStatus",
            "ADName='MaritalStatus'", null);
        UL.FillLookup(lueGender, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Gender", "ADName='Gender'", null);
        UL.FillLookup(luePersType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "PersType", "ADName='PersType'",
            null);
        UL.FillLookup(lueCity, "0", cSet.cnStringA, false, "ID,Name", "vADR", "City", "ADName='City'", null);
        UL.FillLookup(lueContractType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "ContractType",
            "ADName='ContractType'", null);
        UL.FillLookup(lueBank, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Bank", "ADName='Bank'", null);

        UL.FillLookup(lueDrRole, "0", cSet.cnStringA, false, "ID,Name", "vADR", "DrRoleType", "ADName='DrRoleType'",
            null);
        UL.FillLookup(lueAcademicLevel, "0", cSet.cnStringA, false, "ID,Name", "vADR", "AcademicLevel",
            "ADName='AcademicLevel'", null);
        UL.FillLookup(lueDrExpert, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Expert", "ADName='Expert'",
            null);
        UL.FillLookup(lueDrExpertise, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Expertise",
            "ADName='Expertise'", null);
        UL.FillLookup(lueDrLevel, "0", cSet.cnStringA, false, "ID,Name", "vADR", "DrLevel", "ADName='DrLevel'",
            null);
        UL.FillLookup(lueDrUiType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "DrUiType", "ADName='DrUiType'",
            null);
        UL.FillLookup(lueDep, "0", cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);


        UL.FillLookup(lueInsurance, "0", cSet.cnStringA, false, "*", "Insurance", "Insurance",
            "InsuranceID in (0,1,200)", null);

        UL.FillLookup(lueTaxExemption, "0", cSet.cnStringA, false, "ID,Name", "vADR", "TaxExemption",
            "ADName='TaxExemption'",
            null);
    }

    private void SetMask()
    {
        txtFirstName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtLastName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtFatherName.Properties.Mask.EditMask = App.UI.MaskPersian;
    }

    private void txtNationalCode_Properties_Validating(object sender, CancelEventArgs e)
    {
        if (txtNationalCode.Text.Length > 0 &&
            !App.UI.NationalCodeCValidate(txtNationalCode.Text, out var errorText))
        {
            e.Cancel = true;
            txtNationalCode.ErrorText = errorText;
        }
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.LookupEditLine(luePersType, e);
        Control.LookupEditLine(lueContractType, e);
        Control.LookupEditLine(lueCity, e);
        Control.LookupEditLine(lueMaritalStatus, e);
        Control.LookupEditLine(lueAcademicLevel, e);
        Control.LookupEditLine(lueBank, e);
        Control.LookupEditLine(lueBirthArea, e);
        Control.LookupEditLine(lueDrExpert, e);
        Control.LookupEditLine(lueDrExpertise, e);
        Control.LookupEditLine(lueDrLevel, e);
        Control.LookupEditLine(lueDrRole, e);
        Control.LookupEditLine(lueDrUiType, e);
        Control.LookupEditLine(lueEducationLevel, e);
        Control.LookupEditLine(lueGender, e);
        Control.TextEditLine(datBirthDate, e);
        Control.TextEditLine(txtFirstName, e);
        Control.TextEditLine(txtLastName, e);
        Control.TextEditLine(txtFatherName, e);
        Control.TextEditLine(txtNationalCode, e);
        Control.TextEditLine(txtIDCardNumber, e);
        Control.TextEditLine(txtBankAccountNumber, e);
        Control.TextEditLine(txtAddressDetail, e);
        Control.TextEditLine(txtTel, e);
        Control.TextEditLine(txtMobile, e);
        Control.TextEditLine(txtEmail, e);
        Control.TextEditLine(txtDescription, e);
        Control.TextEditLine(txtRegNumber, e);
        Control.TextEditLine(txtShareCount, e);
        Control.LookupEditLine(lueDep, e);
        Control.LookupEditLine(lueInsurance, e);
        Control.LookupEditLine(lueTaxExemption, e);
    }

    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.LookupEditLine(luePers, e);
    }

    private void luePerson_EditValueChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(luePers.EditValue) == 0) return;

        _PersID = Convert.ToInt32(luePers.EditValue);
        FormNull();
        PersLoad(_PersID);
        CheckReadOnly(true);
    }

    private void PersLoad(int PersID)
    {
        _CRUD = 1;
        _PersID = PersID;
        _dtPersonnel = _bllPersonnel.Personnel
        (_CRUD, _PersID, chkIsDisable.EditValue, luePersType.EditValue, txtNationalCode.EditValue,
            txtFirstName.EditValue, txtLastName.EditValue, txtFatherName.EditValue, lueGender.EditValue,
            txtIDCardNumber.EditValue, datBirthDate.Text.Trim(), lueMaritalStatus.EditValue, txtMobile.EditValue,
            txtTel.EditValue, null, lueCity.EditValue, null, txtAddressDetail.EditValue,
            lueBank.EditValue, txtBankAccountNumber.EditValue, lueDep.EditValue, null, null,
            lueContractType.EditValue, chkIsSalary.EditValue, chkIsInsurance.EditValue, chkIsPercase.EditValue,
            txtShareCount.EditValue,
            txtRegNumber.EditValue, lueAcademicLevel.EditValue, chkIsFullTime.EditValue, null,
            lueDrExpert.EditValue, lueDrExpertise.EditValue, lueDrLevel.EditValue, lueDrUiType.EditValue,
            lueDrRole.EditValue, GL.UserPersID, _ResuID, txtResuName.EditValue, txtDrowssap.EditValue);
        //lueInsurance.EditValue, lueTaxExemption.EditValue, lueBirthArea.EditValue, lueEducationLevel.EditValue);


        txtPersID.Text = _dtPersonnel.Rows[0]["PersID"].ToString();
        lblTitle.Text = _dtPersonnel.Rows[0]["FullName"].ToString();
        txtFirstName.Text = _dtPersonnel.Rows[0]["FirstName"].ToString();
        txtLastName.Text = _dtPersonnel.Rows[0]["LastName"].ToString();
        txtFatherName.Text = _dtPersonnel.Rows[0]["FatherName"].ToString();
        txtNationalCode.Text = _dtPersonnel.Rows[0]["NationalCode"].ToString();
        txtIDCardNumber.Text = _dtPersonnel.Rows[0]["IDCardNumber"].ToString();
        txtBankAccountNumber.Text = _dtPersonnel.Rows[0]["BankAccountNumber"].ToString();
        lueDep.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["Dep"].ToString());
        txtAddressDetail.Text = _dtPersonnel.Rows[0]["AddressDetail"].ToString();
        txtTel.Text = _dtPersonnel.Rows[0]["Tel"].ToString();
        txtMobile.Text = _dtPersonnel.Rows[0]["Mobile"].ToString();
        //txtEmail.Text = _dtPersonnel.Rows[0]["Email"].ToString();
        //txtDescription.Text = _dtPersonnel.Rows[0]["Description"].ToString();
        txtRegNumber.Text = _dtPersonnel.Rows[0]["RegNumber"].ToString();
        txtShareCount.Text = _dtPersonnel.Rows[0]["ShareCount"].ToString();

        if (!string.IsNullOrEmpty(_dtPersonnel.Rows[0]["BirthDate"].ToString()))
            datBirthDate.EditValue = Convert.ToDateTime(_dtPersonnel.Rows[0]["BirthDate"]);

        luePersType.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["PersType"].ToString());
        lueContractType.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["ContractType"].ToString());
        lueCity.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["City"].ToString());
        lueMaritalStatus.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["MaritalStatus"].ToString());
        // lueAcademicLevel.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["AcademicLevel"].ToString());
        lueBank.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["Bank"].ToString());
        //lueBirthArea.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["BirthArea"].ToString());
        lueDrExpert.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["DrExpert"].ToString());
        lueDrExpertise.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["DrExpertise"].ToString());
        lueDrLevel.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["DrLevel"].ToString());
        lueDrRole.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["DrRoleType"].ToString());
        lueDrUiType.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["DrUiType"].ToString());
        //lueEducationLevel.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["EducationLevel"].ToString());
        lueGender.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["Gender"].ToString());


        _ResuID = Convert.ToInt32(_dtPersonnel.Rows[0]["ResuID"].ToString());
        txtResuName.Text = _dtPersonnel.Rows[0]["ResuName"].ToString();
        txtDrowssap.Text = _dtPersonnel.Rows[0]["Drowssap"].ToString();
        if (!string.IsNullOrEmpty(_dtPersonnel.Rows[0]["Photo"].ToString()))
        {
            picPerson.Properties.SizeMode = PictureSizeMode.Stretch;
            _Img = byteArrayToImage((byte[])_dtPersonnel.Rows[0]["Photo"]);
            picPerson.Image = _Img;
        }

        chkIsDisable.EditValue = _dtPersonnel.Rows[0]["IsDisable"];
        chkIsSalary.EditValue = _dtPersonnel.Rows[0]["IsSalary"];
        chkIsInsurance.EditValue = _dtPersonnel.Rows[0]["IsInsurance"];
        chkIsPercase.EditValue = _dtPersonnel.Rows[0]["IsPercase"];

        //lueInsurance.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["Insurance"].ToString());
        //lueTaxExemption.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["TaxExemption"].ToString());
    }

    public Image byteArrayToImage(byte[] byteArrayIn)
    {
        var ms = new MemoryStream(byteArrayIn);
        var returnImage = Image.FromStream(ms);
        return returnImage;
    }

    private void FormNull()
    {
        luePersType.EditValue = 0;
        lueContractType.EditValue = 0;
        lueCity.EditValue = 0;
        lueMaritalStatus.EditValue = 0;
        lueAcademicLevel.EditValue = 0;
        lueBank.EditValue = 0;
        lueBirthArea.EditValue = 0;
        lueDrExpert.EditValue = 0;
        lueDrExpertise.EditValue = 0;
        lueDrLevel.EditValue = 0;
        lueDrRole.EditValue = 0;
        lueDrUiType.EditValue = 0;
        lueEducationLevel.EditValue = 0;
        lueGender.EditValue = 0;
        lueDep.EditValue = 0;
        lueInsurance.EditValue = 0;
        lueTaxExemption.EditValue = 0;

        txtPersID.Text = null;
        txtFirstName.Text = null;
        datBirthDate.Text = null;
        txtLastName.Text = null;
        txtFatherName.Text = null;
        txtNationalCode.Text = null;
        txtIDCardNumber.Text = null;
        txtBankAccountNumber.Text = null;
        txtAddressDetail.Text = null;
        txtTel.Text = null;
        txtMobile.Text = null;
        txtEmail.Text = null;
        txtDescription.Text = null;
        txtRegNumber.Text = null;
        txtShareCount.Text = null;
        chkIsFullTime.Checked = false;
        chkShare.Checked = false;
        chkIsDisable.Checked = false;
        chkIsSalary.Checked = true;
        chkIsInsurance.Checked = true;
        chkIsPercase.Checked = true;
        txtResuName.Text = null;
        txtDrowssap.Text = null;
        lblTitle.Text = null;
        lblTitle1.Text = null;
        picPerson.Image = null;
        _Img = null;

        datBirthDate.Text = null;
    }

    private void CheckReadOnly(bool Status)
    {
        if (!Status)
        {
            _ReadOnly = false;
            picPerson.Enabled = true;
        }
        else
        {
            _ReadOnly = true;
            picPerson.Enabled = false;
        }

        luePersType.ReadOnly = _ReadOnly;
        lueContractType.ReadOnly = _ReadOnly;
        lueCity.ReadOnly = _ReadOnly;
        lueMaritalStatus.ReadOnly = _ReadOnly;
        lueAcademicLevel.ReadOnly = _ReadOnly;
        lueBank.ReadOnly = _ReadOnly;
        lueBirthArea.ReadOnly = _ReadOnly;
        lueDrExpert.ReadOnly = _ReadOnly;
        lueDrExpertise.ReadOnly = _ReadOnly;
        lueDrLevel.ReadOnly = _ReadOnly;
        lueDrRole.ReadOnly = _ReadOnly;
        lueDrUiType.ReadOnly = _ReadOnly;
        lueEducationLevel.ReadOnly = _ReadOnly;
        lueGender.ReadOnly = _ReadOnly;
        lueDep.ReadOnly = _ReadOnly;
        lueInsurance.ReadOnly = _ReadOnly;
        lueTaxExemption.ReadOnly = _ReadOnly;

        txtFirstName.ReadOnly = _ReadOnly;
        datBirthDate.ReadOnly = _ReadOnly;
        txtLastName.ReadOnly = _ReadOnly;
        txtFatherName.ReadOnly = _ReadOnly;
        txtNationalCode.ReadOnly = _ReadOnly;
        txtIDCardNumber.ReadOnly = _ReadOnly;
        txtBankAccountNumber.ReadOnly = _ReadOnly;
        txtAddressDetail.ReadOnly = _ReadOnly;
        txtTel.ReadOnly = _ReadOnly;
        txtMobile.ReadOnly = _ReadOnly;
        txtEmail.ReadOnly = _ReadOnly;
        txtDescription.ReadOnly = _ReadOnly;
        txtRegNumber.ReadOnly = _ReadOnly;
        txtShareCount.ReadOnly = _ReadOnly;
        chkIsFullTime.ReadOnly = _ReadOnly;
        chkShare.ReadOnly = _ReadOnly;
        chkIsDisable.ReadOnly = _ReadOnly;
        chkIsSalary.ReadOnly = _ReadOnly;
        chkIsInsurance.ReadOnly = _ReadOnly;
        chkIsPercase.ReadOnly = _ReadOnly;

        txtResuName.ReadOnly = _ReadOnly;
        txtDrowssap.ReadOnly = _ReadOnly;
    }

    private void CheckReadOnlyDr(bool Status)
    {
        if (!Status)
            _ReadOnly = false;
        else
            _ReadOnly = true;

        lueAcademicLevel.ReadOnly = _ReadOnly;
        lueDrExpert.ReadOnly = _ReadOnly;
        lueDrExpertise.ReadOnly = _ReadOnly;
        lueDrLevel.ReadOnly = _ReadOnly;
        lueDrRole.ReadOnly = _ReadOnly;
        lueDrUiType.ReadOnly = _ReadOnly;

        txtRegNumber.ReadOnly = _ReadOnly;
        txtShareCount.ReadOnly = _ReadOnly;
        chkIsFullTime.ReadOnly = _ReadOnly;
        chkShare.ReadOnly = _ReadOnly;
    }


    private void lblPersEdit_Click(object sender, EventArgs e)
    {
        CheckReadOnly(false);
        CheckReadOnlyDr(true);
        lblTitle1.Text = "ویرایش";

        _PersTypeID = Convert.ToInt32(luePersType.EditValue);

        if (_PersTypeID == 1) CheckReadOnlyDr(false);
    }

    private void lblPersNew_Click(object sender, EventArgs e)
    {
        FormNull();
        luePers.EditValue = 0;
        _PersID = 0;
        CheckReadOnly(false);
        CheckReadOnlyDr(true);
        lblTitle1.Text = "ایجاد پرسنل جدید";
    }

    private void PageNavigate(int navigate)
    {
        if (_dtPersonnel == null || _dtPersonnel.Rows.Count == 0) return;

        var oldPersID = _PersID;

        _PersID = _PersID + navigate;
        PersLoad(_PersID);


        // lblPageList.Text = "نفر " + _PersRecNo.ToString() + " از " + _dtPersLoad.Rows.Count.ToString();
        luePers.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["PersID"].ToString());
    }

    private void lblPageNextRec_Click(object sender, EventArgs e)
    {
        PageNavigate(1);
    }

    private void lblPagePreRec_Click(object sender, EventArgs e)
    {
        PageNavigate(-1);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        if (_PersID > 0 || _PersID == -1000)
            _CRUD = 2;
        else
            _CRUD = 0;

        ToStream(_Img, ImageFormat.Jpeg);

        _dtPersonnel = _bllPersonnel.Personnel
        (_CRUD, _PersID, chkIsDisable.EditValue, luePersType.EditValue, txtNationalCode.EditValue,
            txtFirstName.EditValue, txtLastName.EditValue, txtFatherName.EditValue, lueGender.EditValue,
            txtIDCardNumber.EditValue, datBirthDate.Text.Trim(), lueMaritalStatus.EditValue, txtMobile.EditValue,
            txtTel.EditValue, null, lueCity.EditValue, null, txtAddressDetail.EditValue,
            lueBank.EditValue, txtBankAccountNumber.EditValue, lueDep.EditValue, _file, null,
            lueContractType.EditValue, chkIsSalary.EditValue, chkIsInsurance.EditValue, chkIsPercase.EditValue,
            txtShareCount.EditValue,
            txtRegNumber.EditValue, lueAcademicLevel.EditValue, chkIsFullTime.EditValue, null,
            lueDrExpert.EditValue, lueDrExpertise.EditValue, lueDrLevel.EditValue, lueDrUiType.EditValue,
            lueDrRole.EditValue, GL.UserPersID, _ResuID, txtResuName.EditValue, _EncryptDrowssap);
        //lueInsurance.EditValue, lueTaxExemption.EditValue, lueBirthArea.EditValue, lueEducationLevel.EditValue);


        FormNull();

        CheckReadOnly(true);

        UL.FillLookup(luePers, "0", cSet.cnStringERP, false, "PersID,Name", "vPersonnel", "vPersonnel", null, null);

        luePers.EditValue = Convert.ToInt32(_dtPersonnel.Rows[0]["PersID"].ToString());

        ucShowTextSave.ShowText("اطلاعات پرسنل ذخیره گردید", 5000);
    }

    public byte[] ToStream(Image image, ImageFormat format)
    {
        var stream = new MemoryStream();
        if (image != null)
        {
            image.Save(stream, format);
            stream.Position = 0;
            using (var reader = new BinaryReader(stream))
            {
                _file = reader.ReadBytes((int)stream.Length);
            }

            return _file;
        }

        return null;
    }

    private bool CheckValidation()
    {
        var isValid = true;


        if (string.IsNullOrEmpty(txtNationalCode.Text) || txtNationalCode.Text.Length != 10)
        {
            txtNationalCode.ErrorText = "کد ملی را صحیح وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(txtFirstName.Text) && string.IsNullOrEmpty(txtLastName.Text))
        {
            txtFirstName.ErrorText = "لطفا نام را وارد نمایید";
            txtLastName.ErrorText = "لطفا نام خانوادگی را وارد نمایید";
            isValid = false;
        }

        //if (string.IsNullOrEmpty(datBirthDate.Text) && string.IsNullOrEmpty(datBirthDate.Text))
        //{
        //    datBirthDate.ErrorText = "لطفا تاریخ تولد را وارد نمایید";
        //    isValid = false;
        //}

        return isValid;
    }


    private void lblShowPass_MouseDown(object sender, MouseEventArgs e)
    {
        txtDrowssap.Properties.UseSystemPasswordChar = false;
    }

    private void lblShowPass_MouseUp(object sender, MouseEventArgs e)
    {
        txtDrowssap.Properties.UseSystemPasswordChar = true;
    }

    private void luePersType_EditValueChanged(object sender, EventArgs e)
    {
        _PersTypeID = Convert.ToInt32(luePersType.EditValue);

        if (_PersTypeID == 1) CheckReadOnlyDr(false);
    }

    private static string Encrypt(string value)
    {
        //Using MD5 to encrypt a string
        using (var md5 = new MD5CryptoServiceProvider())
        {
            var utf8 = new UTF8Encoding();
            //Hash data
            var data = md5.ComputeHash(utf8.GetBytes(value));
            return Convert.ToBase64String(data);
        }
    }

    private void txtDrowssap_EditValueChanged(object sender, EventArgs e)
    {
        // _EncryptDrowssap = Encrypt(txtDrowssap.Text);
        _EncryptDrowssap = txtDrowssap.Text;
    }

    private void picPerson_Click(object sender, EventArgs e)
    {
        var dr = ofdOpen.ShowDialog();
        if (dr == DialogResult.OK)
        {
            _PicPersonFilePath = ofdOpen.FileName;
            _Img = Resize(Image.FromFile(_PicPersonFilePath), 300, 400);

            picPerson.Properties.SizeMode = PictureSizeMode.Stretch;
            picPerson.Image = _Img;
        }
    }

    public Image Resize(Image source, int width, int height)

    {
        if (source.Width == width && source.Height == height) return source;
        var result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        result.SetResolution(source.HorizontalResolution, source.VerticalResolution);
        using (var g = Graphics.FromImage(result))
        {
            g.DrawImage(source, new Rectangle(0, 0, width, height), new Rectangle(0, 0, source.Width, source.Height),
                GraphicsUnit.Pixel);
        }

        return result;
    }
}