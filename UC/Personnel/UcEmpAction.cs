using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using LIB;
using Saman.BLL.Personnel;
using Control = App.Control;

namespace Saman.UC;

public partial class UcEmpAction : XtraUserControl
{
    private readonly EmpAction _bllEmpAction = new();


    private readonly DataRow[] _rowPersCur;
    private DataTable _dtEmpAction = new();

    private DataTable _dtEncActionItem;
    private DataTable _dtPerCur;
    private DataTable _dtPersEmpAction = new();

    public UcEmpAction()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }
    public byte _DepID { get; set; }
    public int _PersID { get; set; }
    public int _EmpActionID { get; set; }

    private void UcEmpAction_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        ActiveControl = txtPersID;
        lueContractType.Enabled = true;


        FillLookup();

        memDescription.Properties.Mask.EditMask = App.UI.MaskPersianWithNum;

        //_rowPersCur = GL.dtPersCur.Select("ModID=" + _ModID);

        // if (_rowPersCur.Length > 0)
        // _PersID = Convert.ToInt32(_rowPersCur[0]["PersID"]);
        _PersID = GL.ErpCurPersID;

        //if (_PersID > 0)
        //    PersSearch(_PersID);
        if (GL.ErpCurPersID > 0) PersSearch(GL.ErpCurPersID);

        if (_EmpActionID != 0) PersEmpActionLoad();

        EmpActionItemLoad();

        ShortKey();
    }

    private void ShortKey()
    {
        msiSave.Click += btnSave_Click;
        msiClose.Click += btnCancel_Click;
        msiSign.Click += btnsign_Click;


        panMain.Focus();
    }

    private void PersNull()
    {
        _PersID = 0;
        lblPersFirstName.Text = null;
        lblPersLastName.Text = null;
        lblPersNationalCode.Text = null;
    }

    private void FillLookup()
    {
        UL.FillLookup(lueEmpActionTitle, "0", cSet.cnStringA, false, "ID,Name", "vADR", "EmpActionTitle",
            "ADName='EmpActionTitle'", null);
        UL.FillLookup(lueContractType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "ContractType",
            "ADName='ContractType'", null);
        UL.FillLookup(luePersType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "PersType", "ADName='PersType'",
            null);
    }

    private void PersSearch(int persID)
    {
        txtPersID.ErrorText = null;

        var bllPers = new BLL.Personnel.Personnel();
        _dtPerCur = bllPers.PersSearch(persID);

        if (_dtPerCur.Rows.Count > 0)
        {
            _PersID = Convert.ToInt32(_dtPerCur.Rows[0]["PersID"].ToString());
            txtPersID.Text = _PersID.ToString();
            lblPersFirstName.Text = _dtPerCur.Rows[0]["FirstName"].ToString();
            lblPersLastName.Text = _dtPerCur.Rows[0]["LastName"].ToString();
            lblTitle1.Text = _dtPerCur.Rows[0]["FirstName"] + " " + _dtPerCur.Rows[0]["LastName"];
            lblPersNationalCode.Text = _dtPerCur.Rows[0]["NationalCode"].ToString();
            luePersType.EditValue = Convert.ToInt32(_dtPerCur.Rows[0]["PersType"]);
            lueContractType.EditValue = Convert.ToInt32(_dtPerCur.Rows[0]["ContractType"]);
        }
        else
        {
            PersNull();
            txtPersID.ErrorText = "پرسنلی یافت نشد";
        }

        if (_EmpActionID != 0) PersEmpActionLoad();

        EmpActionItemLoad();
    }


    private void PersEmpActionLoad()
    {
        _dtPersEmpAction = _bllEmpAction.EmpActionLoad(_EmpActionID);

        if (_dtPersEmpAction.Rows.Count > 0)
        {
            _EmpActionID = Convert.ToInt32(_dtPersEmpAction.Rows[_dtPersEmpAction.Rows.Count - 1]["EmpActionID"]);
            EmpActionLoad();
        }
    }

    private void EmpActionLoad()
    {
        _dtEmpAction = _bllEmpAction.EmpActionLoad(_EmpActionID);

        lblEmpActionID.Text = _EmpActionID.ToString();
        lblRegDate.Text = _dtEmpAction.Rows[0]["RegDate"].ToString();
        lueEmpActionTitle.EditValue = Convert.ToInt32(_dtEmpAction.Rows[0]["EmpActionTitle"]);

        if (!string.IsNullOrEmpty(_dtEmpAction.Rows[0]["ContractStartDate"].ToString()))
            datContractStartDate.EditValue = Convert.ToDateTime(_dtEmpAction.Rows[0]["ContractStartDate"]);

        if (!string.IsNullOrEmpty(_dtEmpAction.Rows[0]["ContractEndDate"].ToString()))
            datContractEndDate.EditValue = Convert.ToDateTime(_dtEmpAction.Rows[0]["ContractEndDate"]);

        if (!string.IsNullOrEmpty(_dtEmpAction.Rows[0]["StartDate"].ToString()))
            datStartDate.EditValue = Convert.ToDateTime(_dtEmpAction.Rows[0]["StartDate"]);

        if (!string.IsNullOrEmpty(_dtEmpAction.Rows[0]["EndDate"].ToString()))
            datEndDate.EditValue = Convert.ToDateTime(_dtEmpAction.Rows[0]["EndDate"]);
        //datStartDate.Text = _dtEmpAction.Rows[0]["StartDate"].ToString();
        //datEndDate.Text = _dtEmpAction.Rows[0]["EndDate"].ToString();
        luePersType.EditValue = Convert.ToInt32(_dtEmpAction.Rows[0]["PersType"]);
        lueContractType.Enabled = false;
        if (Convert.ToBoolean(_dtEmpAction.Rows[0]["EmpActionIsDisable"])) btnSave.Visible = false;
        EmpActionItemLoad();
    }


    private void EmpActionItemLoad()
    {
        _dtEncActionItem = _bllEmpAction.GetEmpActionItem(_EmpActionID);
        grdEmpActionItem.DataSource = _dtEncActionItem;
    }


    private void txtPersID_KeyPress(object sender, KeyPressEventArgs e)
    {
        //if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtPersID.Text))
        //    PersSearch(Convert.ToInt32(txtPersID.Text));
    }

    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtPersID, e);
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(lueEmpActionTitle, e);
        Control.TextEditLine(luePersType, e);
        Control.TextEditLine(lueSignPers, e);
        Control.TextEditLine(lueContractType, e);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        _dtEmpAction = _bllEmpAction.EmpActionSave
        (_EmpActionID, _PersID, datContractStartDate.Text, datContractEndDate.Text, datStartDate.Text, datEndDate.Text,
            lueEmpActionTitle.EditValue, memDescription.Text, lueContractType.EditValue, luePersType.EditValue);

        _EmpActionID = Convert.ToInt32(_dtEmpAction.Rows[0]["EmpActionId"]);

        EmpActionItemSave();

        PersEmpActionLoad();
        EmpActionItemLoad();
    }

    private void EmpActionItemSave()
    {
        var ItemID = 0;
        var ItemVal = 0;
        float ItemK = 0;

        for (var i = 0; i < grvEmpActionItem.RowCount; i++)
        {
            ItemID = Convert.ToInt32(grvEmpActionItem.GetRowCellValue(i, "ItemID"));
            ItemVal = Convert.ToInt32(grvEmpActionItem.GetRowCellValue(i, "ItemVal"));
            ItemK = Convert.ToInt32(grvEmpActionItem.GetRowCellValue(i, "ItemK"));

            _bllEmpAction.EmpActionDel(_EmpActionID, ItemID);

            if (Convert.ToDouble(ItemVal) + Convert.ToDouble(ItemK) > 0)
                _bllEmpAction.EmpActionItemSave(_EmpActionID, ItemID, ItemVal, ItemK);
        }
    }

    private void grdEmpActionItem_ProcessGridKey(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            //if (grvEmpActionItem.FocusedColumn.VisibleIndex == grvEmpActionItem.VisibleColumns.Count - 1)
            grvEmpActionItem.FocusedRowHandle++;
        //grvEmpActionItem.FocusedColumn = grvEmpActionItem.GetNearestCanFocusedColumn(grvEmpActionItem.FocusedColumn);
    }

    private void btnsign_Click(object sender, EventArgs e)
    {
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
    }

    private void lblPrint_Click(object sender, EventArgs e)
    {
        grdEmpActionItem.ShowPreview();
    }

    private void grvEmpActionItem_PrintInitialize(object sender, PrintInitializeEventArgs e)
    {
        var pb = e.PrintingSystem as PrintingSystemBase;
        var printableComponentLink = new PrintableComponentLink();
        //pb.PageSettings.Landscape = _IsLandscape;
        pb.PageSettings.PaperKind = PaperKind.A5;
        //pb.Document.AutoFitToPagesWidth = 1;

        pb.PageSettings.TopMargin = 60;
        pb.PageSettings.BottomMargin = 10;
        pb.PageSettings.LeftMargin = 30;
        pb.PageSettings.RightMargin = 30;

        grvEmpActionItem.OptionsPrint.PrintHeader = true;
        grvEmpActionItem.OptionsPrint.PrintFooter = true;


        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.LineAlignment = BrickAlignment.Center;
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.LineAlignment = BrickAlignment.Center;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Font = new Font("IRANSansFaNum", 10, FontStyle.Regular);
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Font = new Font("IRANSansFaNum", 9);

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.AddRange(new[]
        {
            "اقلام حکم :" + lblTitle1.Text, " مرکز جراحی آلفا ",
            "شماره حکم :" + _EmpActionID + " " + lueContractType.Text
        });
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.AddRange(new[]
        {
            "[Date Printed]-[Time Printed] : تاریخ گزارش گیری  ", " [Pages #] از [Page #] : صفحه  ",
            "گزارش گیرنده : " + GL.UserPersName
        });

        grvEmpActionItem.BestFitColumns();
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (lueEmpActionTitle.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueEmpActionTitle.Text))
        {
            lueEmpActionTitle.ErrorText = "عنوان حکم را انتخاب نمایید";
            isValid = false;
        }

        if (lueContractType.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueContractType.Text))
        {
            lueContractType.ErrorText = "نوع استخدام را انتخاب نمایید";
            isValid = false;
        }

        if (luePersType.EditValue.ToString() == "0" || string.IsNullOrEmpty(luePersType.Text))
        {
            luePersType.ErrorText = "رسته را انتخاب نمایید";
            isValid = false;
        }


        if (string.IsNullOrEmpty(datContractStartDate.Text))
        {
            datContractStartDate.ErrorText = "تاریخ را انتخاب نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(datContractEndDate.Text))
        {
            datContractEndDate.ErrorText = "تاریخ را انتخاب نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(datStartDate.Text))
        {
            datStartDate.ErrorText = "تاریخ را انتخاب نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(datEndDate.Text))
        {
            datEndDate.ErrorText = "تاریخ را انتخاب نمایید";
            isValid = false;
        }


        return isValid;
    }
}