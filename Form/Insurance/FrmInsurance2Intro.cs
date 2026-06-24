using System;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.Insurance;
using Saman.BLL.Page;
using List = Saman.BLL.Common.List;

namespace Saman.Form.Insurance;

public partial class FrmInsurance2Intro : XtraForm
{
    private readonly Insurance2 _bllInsurance2 = new();

    private readonly List _bllList = new();

    public string _Age,
        _PageDate,
        _NationalCode,
        _FirstName,
        _LastName,
        _PersSignTextEn,
        _PersSignTextFa,
        _QuestionName,
        _ChiefComplaintName;

    public int _CRUD,
        _ModID,
        _DepID,
        _InsuranceID2,
        _EncID,
        _PageID = 0,
        _EncIDInsInfo,
        _PageIDInsInfo,
        _PageCode;

    public FrmInsurance2Intro()
    {
        InitializeComponent();
    }

    private void FrmInsurance2Intro_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        FillLookup();
        SetMask();

        LoadEncInsuranceData();
    }

    private void LoadEncInsuranceData()
    {
        //Load Insurance2 From Enc Data
        var dt = _bllList.LoadTable("*", "SamanHIS.dbo.EncInsurance", "EncID=" + _EncID);
        lueInsurance2.EditValue = Convert.ToInt32(dt.Rows[0]["Insurance2"]);
        txtInsuranceNumber2.Text = dt.Rows[0]["InsuranceNumber2"].ToString();


        var dtInsurance2 = _bllInsurance2.EncInsuranceInfo(1, _EncID, _PageID,
            0, null, 0, 0, null,
            null, 0);

        if (dtInsurance2.Rows.Count == 0) return;

        _EncIDInsInfo = Convert.ToInt32(dtInsurance2.Rows[0]["EncID"]);
        _PageIDInsInfo = Convert.ToInt32(dtInsurance2.Rows[0]["PageID"]);
        txtInsurance2Limit.EditValue = Convert.ToInt64(dtInsurance2.Rows[0]["InsuranceAcceptAmount2"]);
        txtInsurance2Perc.EditValue = Convert.ToInt32(dtInsurance2.Rows[0]["Insurance2Percent"]);
        txtInsuranceIntroSerial2.Text = dtInsurance2.Rows[0]["InsuranceIntroSerial2"].ToString();
        radgInsurance2Type.SelectedIndex = Convert.ToInt32(dtInsurance2.Rows[0]["Insurance2Type"]);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (_EncIDInsInfo != 0 && _PageIDInsInfo != 0) _CRUD = 2;

        _bllInsurance2.EncInsuranceInfo(_CRUD, _EncID, _PageID, Convert.ToInt32(lueInsurance2.EditValue),
            txtInsuranceNumber2.Text, Convert.ToInt64(txtInsurance2Limit.EditValue),
            Convert.ToInt32(txtInsurance2Perc.Text), txtInsuranceIntroSerial2.Text, radgInsurance2Type.EditValue,
            GL.UserPersID);

        if (_PageID > 0)
        {
            var bllPage = new Page();
            bllPage.PageAmountSave(_PageID);
        }

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
        // txtInsurance2Limit.Properties.Mask.EditMask = App.UI.MaskNum;
        txtInsurance2Perc.Properties.Mask.EditMask = App.UI.MaskNum;
        txtInsuranceIntroSerial2.Properties.Mask.EditMask = App.UI.MaskPersianWithNum;
    }

    private void FillLookup()
    {
        UL.FillLookup(lueInsurance2, "1", cSet.cnStringA, false, "*", "Insurance", "Insurance",
            "IsDisable = 0 AND InsuranceType = 1 OR InsuranceID = 0", null);
    }

    private void radgInsurance2Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        panInsurance2Limit.Enabled = panInsurance2Perc.Enabled = false;

        if (radgInsurance2Type.SelectedIndex == 0)
        {
            panInsurance2Limit.Enabled = false;
            txtInsurance2Limit.Text = "0";
            panInsurance2Perc.Enabled = false;
            txtInsurance2Perc.Text = "0";
        }

        if (radgInsurance2Type.SelectedIndex == 1)
        {
            panInsurance2Limit.Enabled = true;
            panInsurance2Perc.Enabled = false;
            txtInsurance2Perc.Text = "0";
        }

        if (radgInsurance2Type.SelectedIndex == 2)
        {
            panInsurance2Limit.Enabled = false;
            txtInsurance2Limit.Text = "0";
            panInsurance2Perc.Enabled = true;
        }

        if (radgInsurance2Type.SelectedIndex == 3)
        {
            panInsurance2Limit.Enabled = panInsurance2Perc.Enabled = true;
            txtInsurance2Limit.Text = "0";
            txtInsurance2Perc.Text = "0";
        }
    }
}