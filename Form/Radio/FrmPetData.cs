using System;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.Pet;
using Saman.Form.Def;

namespace Saman.Form.Radio;

public partial class FrmPetData : XtraForm
{
    private readonly FormPet _bllFormPet = new();

    public string _Age,
        _PageDate,
        _NationalCode,
        _FirstName,
        _LastName,
        _PersSignTextEn,
        _PersSignTextFa,
        _QuestionName,
        _ChiefComplaintName;

    public string _DrReportSignTextFa1, _DrReportSignTextEn1, _DrReportSignTextFa2, _DrReportSignTextEn2;

    public int _EncID,
        _PageID,
        _Country,
        _ChiefComplaint,
        _EncService,
        _DrID1,
        _DrID2,
        _Question,
        _AnsStatus;

    private int _FormPetID;

    public int _TemplateID = 0;


    public FrmPetData()
    {
        InitializeComponent();
    }

    private void FrmPetData_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        FillLookup();
        SetMask();

        FormPetLoad();
    }

    private void FormPetLoad()
    {
        txtFirstName.Text = _FirstName;
        txtLastName.Text = _LastName;
        lueChiefComplaint.EditValue = _ChiefComplaint;


        var dtFormPet = _bllFormPet.FormPetLoad(1, _PageID);

        if (dtFormPet.Rows.Count == 0) return;

        _FormPetID = Convert.ToInt32(dtFormPet.Rows[0]["FormPetID"]);
        lueReportDr1.EditValue = Convert.ToInt32(dtFormPet.Rows[0]["ReportDrID1"]);
        lueReportDr2.EditValue = Convert.ToInt32(dtFormPet.Rows[0]["ReportDrID2"]);
        lueQuestion.EditValue = Convert.ToInt32(dtFormPet.Rows[0]["Question"]);

        _PersSignTextEn = dtFormPet.Rows[0]["PersSignTextEn"].ToString();
        _PersSignTextFa = dtFormPet.Rows[0]["PersSignTextFa"].ToString();
        _QuestionName = dtFormPet.Rows[0]["QuestionName"].ToString();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        {
            _DrReportSignTextFa1 = lueReportDr1.GetColumnValue("SignTextFa").ToString();
            _DrReportSignTextEn1 = lueReportDr1.GetColumnValue("SignTextEn").ToString();
            _DrReportSignTextFa2 = lueReportDr2.GetColumnValue("SignTextFa").ToString();
            _DrReportSignTextEn2 = lueReportDr2.GetColumnValue("SignTextEn").ToString();
        }
        _DrID1 = Convert.ToInt32(lueReportDr1.EditValue);
        _DrID2 = Convert.ToInt32(lueReportDr2.EditValue);
        _QuestionName = lueQuestion.Text;
        _ChiefComplaintName = lueChiefComplaint.Text;


        if (_FormPetID == 0)
            _bllFormPet.FormPetSave(0, _EncID, _PageID, _DrID1, _DrID2, Convert.ToInt32(lueQuestion.EditValue),
                Convert.ToInt32(lueChiefComplaint.EditValue), GL.UserPersID);
        else
            _bllFormPet.FormPetSave(2, _EncID, _PageID, _DrID1, _DrID2, Convert.ToInt32(lueQuestion.EditValue),
                Convert.ToInt32(lueChiefComplaint.EditValue), GL.UserPersID);

        var dtFormPet = _bllFormPet.FormPetLoad(1, _PageID);
        _PersSignTextEn = dtFormPet.Rows[0]["PersSignTextEn"].ToString();
        _PersSignTextFa = dtFormPet.Rows[0]["PersSignTextFa"].ToString();

        if (dtFormPet.Rows.Count == 0) return;

        DialogResult = DialogResult.Yes;
        Close();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
        Close();
    }

    private void SetMask()
    {
        txtFirstName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtLastName.Properties.Mask.EditMask = App.UI.MaskPersian;
    }

    private void FillLookup()
    {
        UL.FillLookup(lueReportDr1, "0", cSet.cnStringERP, false,
            "PersID AS DrID, LastName + '_' + FirstName AS Name, SignTextFa, SignTextEn", "Personnel", "vDr",
            "PersType=1 OR PersID=0", null);
        UL.FillLookup(lueReportDr2, "0", cSet.cnStringERP, false,
            "PersID AS DrID, LastName + '_' + FirstName AS Name, SignTextFa, SignTextEn", "Personnel", "vDr",
            "PersType=1 OR PersID=0", null);
        UL.FillLookup(lueQuestion, "0", cSet.cnStringA, false, "ID,Name", "vADR", "PetQuestion",
            "ADName='PetQuestion'", null);
        UL.FillLookup(lueChiefComplaint, "0", cSet.cnStringA, false, "ID,Name", "vADR", "ChiefComplaint",
            "ADName='ChiefComplaint'", null);
    }

    private void lblQuestion_Click(object sender, EventArgs e)
    {
        var frm = new FrmDefADR
        {
            _ADID = 55
        };
        var result = frm.ShowDialog();

        lueQuestion.EditValue = frm._ID;
    }

    private void lblChiefComplaint_Click(object sender, EventArgs e)
    {
        var frm = new FrmDefADR
        {
            _ADID = 12
        };
        var result = frm.ShowDialog();

        lueChiefComplaint.EditValue = frm._ID;
    }
}