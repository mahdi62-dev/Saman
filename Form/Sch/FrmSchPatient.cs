using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.Form.Def;

namespace Saman.Form.Sch;

public partial class FrmSchPatient : XtraForm
{
    private readonly BLL.Sch.Sch _bllSch = new();


    private readonly int _UCID = 2;
    private DataTable _dtUCSetting = new();
    public int _SchDefID, _RecNo;

    public int _SchID;

    public FrmSchPatient()
    {
        InitializeComponent();
    }

    private void FrmSchPatient_Load(object sender, EventArgs e)
    {
        //grdSearch.DataSource = _dtEncSearch;
        UL.LangChange(2);
        ActiveControl = txtNationalCode;

        //UCSetting(floMain);
        FillLookup();
        SetMask();

        if (_SchID > 0) SchLoad();
    }

    private void SchLoad()
    {
        var dt = _bllSch.SchRecLoad(1, _SchID);
        txtNationalCode.Text = dt.Rows[0]["NationalCode"].ToString();
        txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
        txtLastName.Text = dt.Rows[0]["LastName"].ToString();
        txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();

        txtAge.Text = dt.Rows[0]["Age"].ToString();
        txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
        txtTel.Text = dt.Rows[0]["Tel"].ToString();
        txtDescription.Text = dt.Rows[0]["Description"].ToString();
        txtHeight.Text = dt.Rows[0]["Height"].ToString();
        txtWeight.Text = dt.Rows[0]["Weight"].ToString();
        txtF1.Text = dt.Rows[0]["F1"].ToString();

        lueCountry.EditValue = Convert.ToInt32(dt.Rows[0]["Country"]);
        lueCity.EditValue = Convert.ToInt32(dt.Rows[0]["City"]);
        lueGender.EditValue = Convert.ToInt32(dt.Rows[0]["Gender"]);
        lueChiefComplaint.EditValue = Convert.ToInt32(dt.Rows[0]["ChiefComplaint"]);
        lueEncService.EditValue = Convert.ToInt32(dt.Rows[0]["EncService"]);
        lueInsurance.EditValue = Convert.ToInt32(dt.Rows[0]["Insurance"]);
        lueRefDr.EditValue = Convert.ToInt32(dt.Rows[0]["RefDrID"]);

        chkEmr.EditValue = (bool)dt.Rows[0]["Emergency"];
        chkAnes.EditValue = (bool)dt.Rows[0]["Anes"];
        chkSpecialDisease.EditValue = (bool)dt.Rows[0]["SpecialDisease"];
        chkPriority.EditValue = (bool)dt.Rows[0]["Priority"];

        if (!string.IsNullOrEmpty(dt.Rows[0]["LastChemotherapyDate"].ToString()))
            datLastChemotherapyDate.EditValue = Convert.ToDateTime(dt.Rows[0]["LastChemotherapyDate"]);
        if (!string.IsNullOrEmpty(dt.Rows[0]["LastRadiotherapyDate"].ToString()))
            datLastRadiotherapyDate.EditValue = Convert.ToDateTime(dt.Rows[0]["LastRadiotherapyDate"]);
        if (!string.IsNullOrEmpty(dt.Rows[0]["LastSurgeryDate"].ToString()))
            datLastSurgeryDate.EditValue = Convert.ToDateTime(dt.Rows[0]["LastSurgeryDate"]);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var CRUD = 0;
        if (_SchID > 0) CRUD = 2;

        _bllSch.SchSave(CRUD, _SchDefID, _SchID, _RecNo, txtNationalCode.Text.Trim(), txtFirstName.Text.Trim(),
            txtLastName.Text.Trim(), txtFatherName.Text.Trim(), Convert.ToInt32(lueCountry.EditValue),
            Convert.ToInt32(lueGender.EditValue), Convert.ToInt32(txtAge.Text),
            txtMobile.Text, txtTel.Text, Convert.ToInt32(lueCity.EditValue), txtDescription.Text,
            Convert.ToInt32(lueChiefComplaint.EditValue), Convert.ToInt32(lueEncService.EditValue),
            Convert.ToInt32(lueRefDr.EditValue),
            Convert.ToInt32(lueInsurance.EditValue), Convert.ToInt32(txtHeight.Text),
            Convert.ToInt32(txtWeight.Text), chkEmr.Checked, chkSpecialDisease.Checked, chkAnes.Checked, txtF1.Text,
            datLastChemotherapyDate.Text.Trim(),
            datLastRadiotherapyDate.Text.Trim(), datLastSurgeryDate.Text.Trim(), chkPriority.Checked, GL.UserPersID);

        DialogResult = DialogResult.Yes;
        Close();
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

    //        ctrl[0].TabIndex = recNo;

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

    private void btnClose_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
        Close();
    }

    private void SetMask()
    {
        txtFirstName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtLastName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtFatherName.Properties.Mask.EditMask = App.UI.MaskPersian;
    }

    private void lblRefDr_Click(object sender, EventArgs e)
    {
        var frm = new FrmDefADR
        {
            _ADID = 53
        };
        var result = frm.ShowDialog();

        lueRefDr.EditValue = frm._ID;
    }

    private void FillLookup()
    {
        UL.FillLookup(lueCountry, "1", cSet.cnStringA, false, "ID,Name", "vADR", "Country", "ADName='Country'",
            null);
        UL.FillLookup(lueGender, "1", cSet.cnStringA, false, "ID,Name", "vADR", "Gender", "ADName='Gender'", null);

        UL.FillLookup(lueCity, "19", cSet.cnStringA, false, "ID,Name", "vADRCity", "City", null, null);
        UL.FillLookup(lueInsurance, "1", cSet.cnStringA, false, "InsuranceID,Name", "Insurance", "Insurance", null,
            null);
        UL.FillLookup(lueChiefComplaint, "0", cSet.cnStringA, false, "ID,Name", "vADR", "ChiefComplaint",
            "ADName='ChiefComplaint'", null);
        UL.FillLookup(lueRefDr, "0", cSet.cnStringA, false, "ID,Name", "vADR", "RefDR", "ADName='RefDR'", null);
        UL.FillLookup(lueEncService, "0", cSet.cnStringA, false, "ID,Name", "vADR", "EncService",
            "ADName='EncService'", null);
    }

    private void txtWeight_EditValueChanged(object sender, EventArgs e)
    {
        //if (txtWeight.Text.Length == 0) return;
        //if (Convert.ToInt32(lueEncService.EditValue) == 2 || Convert.ToInt32(lueEncService.EditValue) == 3)
        //{
        //    txtF1.Text = ((Convert.ToInt32(txtWeight.Text) + 5) * 2).ToString();
        //}
        //else
        //{
        //    if (Convert.ToInt32(txtAge.Text) > 18)
        //    {
        //        txtF1.Text = "0";

        //    }
        //    else
        //    {
        //        txtF1.Text = "0";

        //    }
        //}
    }

    private void lueEncService_EditValueChanged(object sender, EventArgs e)
    {
        //txtWeight.Text = "0";
        //txtF1.Text = "0";
    }
}