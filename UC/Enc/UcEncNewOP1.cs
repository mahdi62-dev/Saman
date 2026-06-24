using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.Dep;
using Saman.BLL.Enc;
using Saman.BLL.SAC;
using Saman.Form.Enc;

namespace Saman.UC.Enc;

public partial class UcEncNewOP1 : XtraUserControl
{
    private readonly Dep _bllDep = new();

    private readonly EncNew _bllEncNew = new();

    private readonly Permission _bllSacPermission = new();

    private readonly BLL.ServiceReg.ServiceReg _bllServiceReg = new();

    private readonly DataTable _dtDep = new();


    private readonly DataTable _dtEncCur;
    private readonly DataTable _dtEncPage = new();
    private readonly DataTable _dtServiceReg = new();
    private readonly int _EncID;


    //----ENC------
    private readonly bool _IsEncNew;

    //--END --ENC------


    //----Page----
    private readonly int _PageID;
    private readonly DataRowView _rowCurServie;
    private readonly DataRow _rowEncSearch;

    private readonly int _UCID = 1;
    public DataTable _dtEncSearch;
    private DataTable _dtUCSetting = new();

    private int _PatientID;
    //--END--Page----


    public UcEncNewOP1()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public int _StorageID { get; set; }
    public int _StoragePeriodID { get; set; }
    public bool _EncEditMode { get; set; }
    public byte _EncType { get; set; }
    private int _PageListRecNo { get; set; }

    private void UcEncNewOP1_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        SetMask();
        FillLookup();

        //  UCSetting(floMain);


        ActiveControl = floMain.Controls[1];

        _EncType = 0;


        ////----Page----
        //var bllServceReg = new BLL.ServiceReg.DT();
        //bllServceReg.CreateTableServiceReg(_dtServiceReg);


        ////EncPageLoad();
        //if (_EncID == 0)
        //{
        //    lagPageService.Enabled = false;
        //    lagEncCash.Enabled = false;
        //}


        ////CheckReadOnly();

        //Control[] ctrl;
        //ctrl = floMain.Controls.Find("panPatientFirstName", false);
        //ctrl[0].TabStop = false;
    }


    private void SetMask()
    {
        txtPatientFirstName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtPatientLastName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtPatientFatherName.Properties.Mask.EditMask = App.UI.MaskPersian;
        //txtCompanionFullName.Properties.Mask.EditMask = App.UI.MaskPersian;
    }

    //private void UCSetting(FlowLayoutPanel flo)
    //{
    //    _dtUCSetting = _bllUCSetting.UCSettingAppLoad(_UCID, Convert.ToInt32(flo.Tag));

    //    int b = flo.Controls.Count;
    //    string cname;

    //    Control[] ctrl;
    //    Control[] ctrlTitle;
    //    string CTitle = null;
    //    string ctrlLabelName;

    //    bool ctrlTabStop = false;
    //    int recNo = 0;

    //    foreach (DataRow row in _dtUCSetting.Rows)
    //    {
    //        cname = row["CName"].ToString();

    //        ctrl = flo.Controls.Find("pan" + cname, false);
    //        ctrl[0].Visible = Convert.ToBoolean(row["Visible"]);


    //        ctrlTabStop = Convert.ToBoolean(row["TabStop"]);
    //        recNo = Convert.ToInt32(row["RecNo"]);

    //        ////ctrl[0].TabIndex = recNo;

    //        CTitle = row["CTitle"].ToString();

    //        if (CTitle != null)
    //        {
    //            ctrlLabelName = "lbl" + row["CName"];

    //            ctrlTitle = flo.Controls.Find(ctrlLabelName, true);
    //            ctrlTitle[0].Text = CTitle;
    //        }

    //        flo.Controls.SetChildIndex(ctrl[0], recNo);

    //        #region TabSetting

    //        Control[] cTextEdit = ctrl[0].Controls.Find("txt" + cname, true);
    //        if (cTextEdit.Length > 0)
    //        {
    //            TextEdit cTxt = (TextEdit)cTextEdit[0];
    //            cTxt.TabStop = ctrlTabStop;
    //            cTxt.TabIndex = recNo;
    //        }

    //        Control[] cLookUpEdit = ctrl[0].Controls.Find("lue" + cname, true);
    //        if (cLookUpEdit.Length > 0)
    //        {
    //            TextEdit cLue = (TextEdit)cLookUpEdit[0];
    //            cLue.TabStop = ctrlTabStop;
    //            cLue.TabIndex = recNo;
    //        }

    //        #endregion
    //    }
    //}


    private void FillLookup()
    {
        //----Enc----
        UL.FillLookup(luePatientCountry, "1", cSet.cnStringA, false, "ID,Name", "vADR", "Country",
            "ADName='Country'", null);
        UL.FillLookup(luePatientGender, "2", cSet.cnStringA, false, "ID,Name", "vADR", "Gender", "ADName='Gender'",
            null);
        UL.FillLookup(luePatientMaritalStatus, "1", cSet.cnStringA, false, "ID,Name", "vADR", "MaritalStatus",
            "ADName='MaritalStatus'", null);

        UL.FillLookup(luePatientCity, "19", cSet.cnStringA, false, "ID,Name", "vADRCity", "City", null, null);
        UL.FillLookup(luePatientDistrict, "0", cSet.cnStringA, false, "ID,Name", "vADR", "District",
            "ADName='District'", null);


        //--END--Enc----


        //LIB.UL.FillLookup(lueEncDep, "1", DAL.cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);
        //LIB.UL.FillLookup(lueEncDr, "0", DAL.cSet.cnStringA, false, "*", "vDr", "Dr", null, null);
        //LIB.UL.FillLookup(lueEncChiefComplaint, "0", DAL.cSet.cnStringA, false, "ID,Name", "vADR", "ChiefComplaint", "ADName='ChiefComplaint'", null);

        //LIB.UL.FillLookup(lueInsurance, "1", DAL.cSet.cnStringA, false, "InsuranceID,Name", "Insurance", "Insurance", null, null);


        ////----Page----
        //LIB.UL.FillLookup(luePageDep, "0", DAL.cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);
        //LIB.UL.FillLookup(luePageDr, "0", DAL.cSet.cnStringA, false, "*", "vDr", "Dr", null, null);
        //LIB.UL.FillLookup(luePageDr2, "0", DAL.cSet.cnStringA, false, "*", "vDr", "Dr", null, null);
        //LIB.UL.FillLookup(luePagePersonnel, "0", DAL.cSet.cnStringA, false, "*", "vPersonnel", "PagePersonnel", "IsDisable=0 AND PersType BETWEEN 2 AND 999", "Name");


        //string strServiceWhere = null; //"DepID=" + _DepID;
        //LIB.UL.FillLookup(lueService, null, DAL.cSet.cnStringA, false, "*", "vServiceAccessDep", "ServiceAccessDep", strServiceWhere, "Name");
        ////--END--Page----
    }


    #region ENC

    private void txtPatientNationalCode_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != 13) return;

        if (txtPatientNationalCode.Text.Length == 10) PatientSearch(txtPatientNationalCode.Text);
    }

    private void txtPatientNationalCode_Leave(object sender, EventArgs e)
    {
        if (txtPatientNationalCode.Text.Length == 10) PatientSearch(txtPatientNationalCode.Text);
    }

    private void txtPatientLastName_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != 13) return;

        PatientSearch(txtPatientNationalCode.Text);
    }

    private void txtPatientLastName_Leave(object sender, EventArgs e)
    {
        PatientSearch(txtPatientNationalCode.Text);
    }

    private void txtPatientFatherName_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != 13) return;

        PatientSearch(txtPatientNationalCode.Text);
    }

    private void txtPatientFatherName_Leave(object sender, EventArgs e)
    {
        PatientSearch(txtPatientNationalCode.Text);
    }


    private void PatientSearch(string NationalCode)
    {
        _dtEncSearch = _bllEncNew.PatientSearch
        (txtPatientFirstName.Text, txtPatientLastName.Text, txtPatientFatherName.Text,
            txtPatientNationalCode.Text);


        if (_dtEncSearch.Rows.Count > 0)
        {
            if (NationalCode.Length < 10)
            {
                var frmEncSearch = new FrmEncSearch
                {
                    _dtEncSearch = _dtEncSearch
                };

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

        if (_PatientID > 0) row = _dtEncSearch.Select("PatientID=" + _PatientID)[0];

        if (_PatientID == 0) row = _dtEncSearch.Select("NationalCode=" + NationalCode)[0];

        panPatientNationalCode.Enabled = false;
        //txtFirstName.ReadOnly = true;
        //txtLastName.ReadOnly = true;
        //txtFatherName.ReadOnly = true;
        //lueGender.ReadOnly = true;
        //lueMaritalStatus.ReadOnly = true;
        //lueCountry.ReadOnly = true;
        //txtBirthYear.ReadOnly = true;
        //txtBirthYear.ReadOnly = true;
        ////txtMobile.ReadOnly = true;
        ////txtStreet.ReadOnly = true;

        //_PatientID = Convert.ToInt32(row["PatientID"]);
        //txtNationalCode.Text = row["NationalCode"].ToString();
        //txtFirstName.Text = row["FirstName"].ToString();
        //txtLastName.Text = row["LastName"].ToString();
        //txtFatherName.Text = row["FatherName"].ToString();
        //lueGender.EditValue = Convert.ToInt32(row["Gender"]);

        //txtBirthYear.Text = row["BirthDate"].ToString().Substring(0, 4);


        //picGender.Image = Convert.ToInt32(row["Gender"]) == 1 ? Properties.Resources.male : Properties.Resources.female;
        //lueMaritalStatus.EditValue = Convert.ToInt32(row["MaritalStatus"]);
        //lueCountry.EditValue = Convert.ToInt32(row["Country"]);
        //txtBirthYear.Text = row["BirthDate"].ToString();
        //txtMobile.Text = row["Mobile"].ToString();
        //txtStreet.Text = row["Street"].ToString();
    }


    private void EncSearch(int encID)
    {
        //txtEncID.ErrorText = null;

        //var bllEnc = new BLL.Enc.EncList();
        //_dtEncCur = bllEnc.EncSearch(encID, true);

        //if (_dtEncCur.Rows.Count > 0)

        //{
        //    EncFormEdit(false);

        //    lagPageService.Enabled = true;
        //    lagEncCash.Enabled = true;

        //    _EncID = Convert.ToInt32(_dtEncCur.Rows[0]["EncID"].ToString());
        //    txtEncID.Text = _EncID.ToString();

        //    txtFirstName.Text = _dtEncCur.Rows[0]["FirstName"].ToString();
        //    txtLastName.Text = _dtEncCur.Rows[0]["LastName"].ToString();

        //}
        //else
        //{
        //    EncNull();
        //    txtEncID.ErrorText = "بیماری یافت نشد";
        //}

        GL.SetEncCurVal(_DepID, _dtEncCur);
    }

    #endregion
}