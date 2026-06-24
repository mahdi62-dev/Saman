using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LIB;
using Saman.APP.SettingApp;
using Saman.BLL.FormSetting;
using Saman.BLL.SAC;
using Control = System.Windows.Forms.Control;

namespace Saman.UC.Acc.Bond;

public partial class UcAccBranch : XtraUserControl
{
    private readonly SettingApp _appSettingApp = new();
    private readonly bool _IsflwOprData;
    private readonly Permission bllSACPermission = new();

    public UcAccBranch()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }


    private void UcAccBranch_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        ActiveControl = flpMain;
        //  ActiveControl = txtPatientNationalCode;

        FormSetting();
        MaskSet();
        FillLookup();
    }


    private void MaskSet()
    {
        //txtPatientFirstName.Properties.Mask.EditMask = App.UI.MaskPersian;
        //txtPatientLastName.Properties.Mask.EditMask = App.UI.MaskPersian;
        //txtPatientFatherName.Properties.Mask.EditMask = App.UI.MaskPersian;
        //txtPatientIDCardNumber.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtBondSerial.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtPatientMobile.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtPatientTel.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtCompanionTel.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtCompanionFullName.Properties.Mask.EditMask = App.UI.MaskPersian;
        //txtPatientEmail.Properties.Mask.EditMask = App.UI.MaskEnglishWithNum;
        //txtEncInsuranceNumber.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtEncInsurancePageNumber.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtEncInsuranceNumber2.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtPatientEmail.Properties.Mask.EditMask = App.UI.MaskEmail;
    }

    private void FillLookup()
    {
        //lueBranch
        //    lueBondType
        //lueAccSide
        //    lueBondReason


        //UL.FillLookup(lueBondCategory, "0", cSet.cnStringA, false, "ID,Name", "vADR", "EducationLevel",
        //    "ADName='EducationLevel'", null);
        //UL.FillLookup(luePatientReligion, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Religion",
        //    "ADName='Religion'", null);
        //UL.FillLookup(luePatientJob, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Job", "ADName='Job'", null);
    }

    private void FormSetting()
    {
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

    #region FormSetting

    private readonly int _FormID = 1;
    private readonly FormSetting _bllFormSetting = new();
    private readonly DataTable _dtFormSetting = new();

    #endregion
}