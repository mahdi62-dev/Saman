using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using LIB;
using Saman.APP.Stimul;
using Saman.BLL.Enc;
using Saman.Form.Enc;
using Saman.Properties;
using Saman.UC.Enc;
using Stimulsoft.Report.Dictionary;

namespace Saman.UC;

public partial class UcEncCur : XtraUserControl
{
    private readonly Stimul _appStimul = new();
    private readonly EncCancel _bllEncFinancialData = new();


    private readonly BLL.Report.Report _bllReport = new();

    private readonly DataTable _dtEncCancel;
    private readonly string _str;

    private DataTable _dtEncCur, _dtEncFinancial;

    private DataRow[] _rowEncCur;


    public UcEncCur()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public int _EncID { get; set; }
    public int _EncStatus { get; set; }

    private void UcEncCur_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        panMain.Location = new Point(
            ClientSize.Width / 2 - panMain.Size.Width / 2,
            ClientSize.Height / 2 - panMain.Size.Height / 2);

        ActiveControl = txtEncID;

        _rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);

        if (_rowEncCur.Length > 0) _EncID = Convert.ToInt32(_rowEncCur[0]["EncID"]);

        if (_EncID > 0)
        {
            EncSearch(_EncID);
        }
        else
        {
            var bllEncList = new EncList();
            EncSearch(bllEncList.EncIDLast());
        }
    }

    private void ShowUCParent(object ucControl)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var myUC = (UserControl)ucControl;
        myUC.Anchor = AnchorStyles.Top;
        myUC.Anchor = AnchorStyles.Right;
        myUC.Dock = DockStyle.Fill;

        //panUC.Parent.Text += myUC.Tag.ToString();
        panUC.Controls.Add(myUC);
    }

    private void EncNull()
    {
        _EncID = 0;
        lblTitle.Text = null;
        lblTitle2.Text = null;
        lblTitleEncDate.Text = null;
        lblMaritalStatus.Text = null;
        lblCountry.Text = null;
        lblEducationLevel.Text = null;
        lblJob.Text = null;
        lblBirthArea.Text = null;
        lblMobile.Text = null;
        lblAddressTel.Text = null;
        lblInPatientEncCount.Text = null;
        lblEncType.Text = null;
        lblEncService.Text = null;
        lblDepName.Text = null;
        lblDrName.Text = null;
        lblRefDrName.Text = null;
        lblInsuranceName.Text = null;
        lblInsuranceNumber.Text = null;
        lblInsurancePageNumber.Text = null;
        lblDescription.Text = null;
    }

    private void EncSearch(int encID)
    {
        txtEncID.ErrorText = null;

        var bllEnc = new EncList();
        _dtEncCur = bllEnc.EncSearch(encID, true);

        if (_dtEncCur.Rows.Count > 0)
        {
            _EncID = Convert.ToInt32(_dtEncCur.Rows[0]["EncID"]);
            _EncStatus = Convert.ToInt32(_dtEncCur.Rows[0]["EncStatus"]);
            var EncStatusName = _dtEncCur.Rows[0]["EncStatusName"].ToString();

            txtEncID.Text = _EncID.ToString();

            lblTitle.Text = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"] + "      " +
                            EncStatusName;
            lblTitle2.Text = _dtEncCur.Rows[0]["Age"] + " سال - فرزند " + _dtEncCur.Rows[0]["FatherName"];

            string OprReserveDate = null;
            OprReserveDate = _dtEncCur.Rows[0]["OprReserveDate"].ToString();
            if (OprReserveDate.Length > 0) lblTitle2.Text += " - تاریخ عمل " + OprReserveDate;

            lblTitleEncDate.Text = "تاریخ پذیرش " + _dtEncCur.Rows[0]["StartDate"].ToString().Replace(' ', ' ');

            picGender.Image = Convert.ToByte(_dtEncCur.Rows[0]["Gender"]) == 1 ? Resources.male : Resources.female;

            lblMaritalStatus.Text = _dtEncCur.Rows[0]["MaritalStatusName"].ToString();

            lblCountry.Text = _dtEncCur.Rows[0]["CountryName"].ToString();
            lblEducationLevel.Text = _dtEncCur.Rows[0]["EducationLevelName"].ToString();
            lblJob.Text = _dtEncCur.Rows[0]["JobName"].ToString();
            lblBirthArea.Text = _dtEncCur.Rows[0]["BirthAreaName"].ToString();
            lblMobile.Text = _dtEncCur.Rows[0]["Mobile"].ToString();
            lblAddressTel.Text = (_dtEncCur.Rows[0]["Tel"] + "  " + _dtEncCur.Rows[0]["AddressFull"]).TrimStart();

            lblInPatientEncCount.Text = Convert.ToInt32(_dtEncCur.Rows[0]["InPatientEncCount"]) == 0
                ? "ندارد"
                : Convert.ToInt32(_dtEncCur.Rows[0]["InPatientEncCount"]) + " بار";

            lblEncType.Text = Convert.ToInt32(_dtEncCur.Rows[0]["EncType"]) == 0 ? "سرپایی" : "بستری";

            lblDepName.Text = _dtEncCur.Rows[0]["DepName"].ToString();
            lblEncDisType.Text = _dtEncCur.Rows[0]["EncStatusName"].ToString();
            lblDrName.Text = _dtEncCur.Rows[0]["DrName"].ToString();
            lblRefDrName.Text = _dtEncCur.Rows[0]["RefDrName"].ToString();

            lblDescription.Text = _dtEncCur.Rows[0]["ChiefComplaintName"] + " " + _dtEncCur.Rows[0]["Description"];
            lblChiefComplaint.Text = _dtEncCur.Rows[0]["ChiefComplaintName"].ToString();
            lblEncService.Text = _dtEncCur.Rows[0]["EncServiceName"].ToString();

            lblInsuranceName.Text = _dtEncCur.Rows[0]["InsuranceName"].ToString();
            lblInsuranceNumber.Text = _dtEncCur.Rows[0]["InsuranceNumber"].ToString();
            lblInsurancePageNumber.Text = _dtEncCur.Rows[0]["InsurancePageNumber"].ToString();

            lblCompanionFullName.Text = _dtEncCur.Rows[0]["CompanionFullName"].ToString();
            lblCompanionTel.Text = _dtEncCur.Rows[0]["CompanionTel"].ToString();
            lblCompanionAddress.Text = _dtEncCur.Rows[0]["CompanionAddress"].ToString();

            if (lblCompanionFullName.Text.Length > 0) panComapnion.Visible = true;
        }
        else
        {
            EncNull();
            txtEncID.ErrorText = "بیماری یافت نشد";
        }

        GL.SetEncCurVal(_DepID, _dtEncCur);
    }

    private void lblEditPatient_Click(object sender, EventArgs e)
    {
        if (_dtEncCur == null) return;

        var ucControl = new UcEncSave
        {
            _ModID = _ModID,
            _DepID = _DepID,
            _EditModePatient = true,
            _dtEncCur = _dtEncCur
        };
        ShowUCParent(ucControl);
    }

    private void lblEditEnc_Click(object sender, EventArgs e)
    {
        if (_dtEncCur == null) return;

        var ucControl = new UcEncSave
        {
            _ModID = _ModID,
            _DepID = _DepID,
            _EditModeEnc = true,
            _dtEncCur = _dtEncCur
        };
        ShowUCParent(ucControl);
    }

    private void txtEncID_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtEncID.Text)) EncSearch(Convert.ToInt32(txtEncID.Text));
    }

    private void UcEncCur_Resize(object sender, EventArgs e)
    {
        picBG.Refresh();
    }

    private void lblEditInsurance_Click(object sender, EventArgs e)
    {
        lblEditEnc_Click(sender, e);
    }

    private void lblEncNext_Click(object sender, EventArgs e)
    {
        var oldEncID = _EncID;

        _EncID += 1;
        EncSearch(_EncID);

        if (_EncID == 0) EncSearch(oldEncID);
    }

    private void lblEncPre_Click(object sender, EventArgs e)
    {
        var oldEncID = _EncID;

        _EncID -= 1;
        EncSearch(_EncID);

        if (_EncID == 0) EncSearch(oldEncID);
    }

    private void barBtnEncTag_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvEncFullName =
            @"select * from SamanHIS.dbo.vEncFullName
                    where EncID=@EncID";

        tsqlvEncFullName = tsqlvEncFullName.Replace("@EncID", _EncID.ToString());


        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100017);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;

        ////var t = report.Dictionary.DataSources.Items;

        ((StiSqlSource)report.Dictionary.DataSources["vEncFullName"]).SqlCommand = tsqlvEncFullName;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;


        report.Show();
    }

    private void barBtnEncInfo_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvEncFullName =
            @"select * from SamanHIS.dbo.vEncFullName
                    where EncID=@EncID";

        tsqlvEncFullName = tsqlvEncFullName.Replace("@EncID", _EncID.ToString());

        var report = _bllReport.RepRptFileLoad(100025);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;

        ((StiSqlSource)report.Dictionary.DataSources["vEncFullName"]).SqlCommand = tsqlvEncFullName;

        report.Show();
    }

    private void barBtnEncBracelet_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvEncFullName =
            @"select * from SamanHIS.dbo.vEncFullName
                    where EncID=@EncID";

        tsqlvEncFullName = tsqlvEncFullName.Replace("@EncID", _EncID.ToString());


        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100020);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;

        ////var t = report.Dictionary.DataSources.Items;

        ((StiSqlSource)report.Dictionary.DataSources["vEncFullName"]).SqlCommand = tsqlvEncFullName;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;


        report.Print();
    }

    private void barBtnEncCancel_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (_EncStatus == 3 || _EncStatus == 4)
        {
            var strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                             + "\n"
                             + "\n بیمار ترخیص گردیده است";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
            return;
        }

        if (_EncStatus == 5)
        {
            var strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                             + "\n"
                             + "\n بیمار کنسل گردیده است";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
            return;
        }

        _dtEncFinancial = _bllEncFinancialData.GetEncFinancialData(_EncID);
        var TotalPayAmount = Convert.ToInt64(_dtEncFinancial.Rows[0]["TotalPayAmount"]);
        var TotalDiscountAmount = Convert.ToInt64(_dtEncFinancial.Rows[0]["TotalDiscountAmount"]);
        var PageDrugCount = Convert.ToInt64(_dtEncFinancial.Rows[0]["PageDrugCount"]);


        if (TotalPayAmount > 0)
        {
            var strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                             + "\n"
                             + "\n بیمار دارای پرداختی است لطفا تسویه فرمائید";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
        }

        if (TotalDiscountAmount > 0)
        {
            var strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                             + "\n"
                             + "\n بیمار دارای تخفیف است لطفا تسویه فرمائید";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
        }

        if (PageDrugCount > 0)
        {
            var strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                             + "\n"
                             + "\n بیمار دارای دارو و لوازم است لطفا حذف نمائید";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
        }

        if (TotalDiscountAmount + TotalPayAmount + PageDrugCount > 0) return;

        if (_EncStatus < 3)
        {
            var strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                             + "\n"
                             + "\nکنسل گردد؟";


            var sui = new SUI();
            var result = sui.FormShowMessage("کد پذیرش : " + _EncID, strMessage);

            if (result == DialogResult.No) return;

            var bllDis = new EncCancel();
            bllDis.EncCancelSave(true, _EncID, GL.UserPersID);

            EncSearch(_EncID);
        }
    }


    private void barBtnChangeDate_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (_EncStatus > 4)
        {
            var strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                             + "\n"
                             + "\n امکان تغییر تاریخ امکان پذیر نیست";
            var sui = new SUI();
            var result1 = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
            return;
        }

        var frmEncData = new FrmEncDate
        {
            _EncID = _EncID,
            _FirstName = _dtEncCur.Rows[0]["FirstName"].ToString(),
            _LastName = _dtEncCur.Rows[0]["LastName"].ToString(),
            _StatusName = _dtEncCur.Rows[0]["EncStatusName"].ToString(),
            _EncTypeName = Convert.ToInt32(_dtEncCur.Rows[0]["EncType"]) == 0 ? "سرپایی" : "بستری",
            _EncStartDate = _dtEncCur.Rows[0]["StartDate"].ToString().Replace(' ', ' '),
            _EncEndDate = _dtEncCur.Rows[0]["EndDate"].ToString().Replace(' ', ' ')
        };


        var result = frmEncData.ShowDialog();
        if (result == DialogResult.Yes) EncSearch(_EncID);
    }
}