using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using LIB;
using Saman.APP.Stimul;
using Saman.BLL.Storage;
using Stimulsoft.Report.Dictionary;
using Control = App.Control;

namespace Saman.UC.Storage;

public partial class UcInvOutput : XtraUserControl
{
    private readonly Stimul _appStimul = new();

    private readonly InvOutput _bllInvOutput = new();


    private readonly BLL.Report.Report _bllReport = new();
    private readonly BLL.Storage.Storage _bllStorage = new();


    private readonly DataTable _dtPage = new();

    private readonly DataTable _dtPageDataLoad;

    private readonly bool _IsEditRow;
    private readonly int _RecNoServiceReg;

    private long _BuyAmountLast, _SellAmountLast;
    private long _BuyFeeAmount, _BuyFeeAmount2, _BuyAmount, _BuyAmount2;

    private DataTable _dtInvPage = new();
    private DataTable _dtServiceReg = new();


    private int _InvPageID, _InvPageCode;
    private bool _IsSigned;
    private DataTable _ItemData = new();
    private double _Num, _Num2;
    private DataRowView _rowCurServie;

    private long _SellFeeAmount, _SellFeeAmount2, _SellAmount, _SellAmount2;

    private int _ServiceUnit, _ServiceUnit2;
    private double _StockNum, _StockNumAll;

    private long _TotalBuyAmount,
        _TotalSellAmount,
        _DiscountAmount,
        _DiscountAmount2,
        _TotalDiscountAmount,
        _TaxAmount,
        _TaxAmount2,
        _TotalTaxAmount,
        _OtherAmount,
        _TotalBonusAmount,
        _ResultAmount;


    private int _UnitKNum;

    public UcInvOutput()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }
    public int _DepID { get; set; }
    public int _StorageID { get; set; }
    public int _StoragePeriodID { get; set; }

    private int _PageDataLoadRecNo { get; set; }


    private void UcRegService_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        ActiveControl = lueService;

        UL.FillLookup(lueService, null, cSet.cnStringA, false, "*", "vServiceDrug", "vServiceDrug",
            "EndDate IS NULL", "CAST(ServiceCode as INT)");
        UL.FillLookup(lueDestDep, "1", cSet.cnStringERP, false, "StorageID,DepID,Name", "vInvDestinationDep",
            "vInvDestinationDep", null, null);
        UL.FillLookup(lueInvCategoryType, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "InvOutputType",
            "ADName='InvOutputType'", null);
        UL.FillLookup(lueDeliverPers, "0", cSet.cnStringERP, false, "PersID,Name", "vPersonnel", "vPersonnel", null,
            null);
        UL.FillLookup(lueTransferee, "0", cSet.cnStringERP, false, "PersID,Name", "vPersonnel", "vPersonnel", null,
            null);

        UL.FillLookup(lueUnit2, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "Unit", "ADName='Unit'", null);

        var bllStorageTable = new DT();
        bllStorageTable.CreateTableServiceReg(_dtServiceReg);

        // ServiceRegLoad(_InvPageID);
        PageDataLoad(_bllInvOutput.InvPageCodeLast(_StorageID));

        MaskSet();
    }

    private void MaskSet()
    {
        txtNum2.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
        txtUnitKNum.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtBuyFeeAmount2.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtSellFeeAmount.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtDiscountAmount.Properties.Mask.EditMask = App.UI.MaskNum;
        ////txtDiscountAmount2.Properties.Mask.EditMask = App.UI.MaskNum;
        //txtOtherAmount.Properties.Mask.EditMask = App.UI.MaskNum;
    }


    private void FormNull()
    {
        lblBuyAmountLast.Text = "";
        lblSellAmountLast.Text = "";
        lblStockNum.Text = "";
        lblTitleName.Text = "";

        lueService.EditValue = null;
        lblServiceCode.Text = null;
        txtNum2.Text = "0";

        _InvPageID = 0;
        _InvPageCode = 0;

        txtInvPageCode.Text = "";
        lblInvDate.Text = "";
        lueDestDep.EditValue = 0;
        lueInvCategoryType.EditValue = 0;


        lueDeliverPers.EditValue = 0;
        datDeliverDate.Text = null;
        txtDescription.Text = "";

        txtDiscountAmount2.Text = "0";
        txtOtherAmount.Text = "0";
        txtTaxAmount2.Text = "0";
        lblTotalBuyAmount.Text = "0";
        lblTotalDiscountAmount.Text = "0";
        lblTotalBonusAmount.Text = "0";
        lblTotalTaxAmount.Text = "0";
        lblTotalResultAmount.Text = "0";
        lblTotalSellAmount.Text = "0";
        txtOrderCode.Text = "";

        lblPageSign.Enabled = true;
        btnServiceAdd.Enabled = true;
        btnServiceDel.Enabled = true;
        btnRegEdit.Enabled = true;
        btnServiceRegSave.Enabled = true;


        _dtServiceReg.Clear();
        grdServiceReg.DataSource = _dtServiceReg;


        ActiveControl = lueService;
    }


    private void PageDataLoad(int InvPageCode)
    {
        _InvPageID = 0;


        _dtInvPage = _bllInvOutput.PageDataLoad(_StorageID, InvPageCode);

        if (_dtInvPage.Rows.Count == 0) return;

        _InvPageCode = InvPageCode;
        txtInvPageCode.Text = _InvPageCode.ToString();

        _InvPageID = Convert.ToInt32(_dtInvPage.Rows[0]["InvPageID"]);

        lblInvDate.Text = _dtInvPage.Rows[0]["Date"].ToString().Replace(' ', ' ');
        lueDestDep.EditValue = Convert.ToInt32(_dtInvPage.Rows[0]["DestDepID"]);
        lueInvCategoryType.EditValue = Convert.ToInt32(_dtInvPage.Rows[0]["InvCategoryType"]);

        if (!(_dtInvPage.Rows[0]["Date2"] is DBNull))
            datDeliverDate.EditValue = Convert.ToDateTime(_dtInvPage.Rows[0]["Date2"]);

        txtOrderCode.EditValue = Convert.ToInt32(_dtInvPage.Rows[0]["OrderCode"]);
        txtDescription.Text = _dtInvPage.Rows[0]["Description"].ToString();
        lueDeliverPers.EditValue = Convert.ToInt32(_dtInvPage.Rows[0]["DeliverPersID"]);
        lueTransferee.EditValue = Convert.ToInt32(_dtInvPage.Rows[0]["TransfereeID"]);


        lblTotalBuyAmount.Text = Convert.ToInt64(_dtInvPage.Rows[0]["BuyAmount"]).ToString("n0");
        lblTotalSellAmount.Text = Convert.ToInt64(_dtInvPage.Rows[0]["SellAmount"]).ToString("n0");
        lblTotalDiscountAmount.Text = Convert.ToInt32(_dtInvPage.Rows[0]["DiscountAmount"]).ToString("n0");
        lblTotalBonusAmount.Text = _dtInvPage.Rows[0]["BonusAmount"].ToString();
        txtOtherAmount.Text = _dtInvPage.Rows[0]["OtherAmount"].ToString();
        txtDiscountAmount2.Text = _dtInvPage.Rows[0]["DiscountAmount2"].ToString();
        lblTotalTaxAmount.Text = _dtInvPage.Rows[0]["TotalTaxAmount"].ToString();
        txtTaxAmount2.Text = _dtInvPage.Rows[0]["TaxAmount2"].ToString();

        ServiceRegLoad();

        _IsSigned = Convert.ToBoolean(_dtInvPage.Rows[0]["IsSign"]);
        if (_IsSigned)
        {
            lblPageSign.Enabled = false;
            btnServiceAdd.Enabled = false;
            btnServiceDel.Enabled = false;
            btnRegEdit.Enabled = false;
            btnServiceRegSave.Enabled = false;
        }
        else
        {
            lblPageSign.Enabled = true;
            btnServiceAdd.Enabled = true;
            btnServiceDel.Enabled = true;
            btnRegEdit.Enabled = true;
            btnServiceRegSave.Enabled = true;
        }
    }

    private void ServiceRegLoad()
    {
        var bllServiceReg = new InvOutput();
        _dtServiceReg = bllServiceReg.ServiceRegLoad(_InvPageID);

        if (_dtServiceReg.Rows.Count == 0) return;

        grdServiceReg.DataSource = _dtServiceReg;
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.LookupEditLine(lueService, e);
        Control.TextEditLine(txtNum2, e);
        //App.Control.TextEditLine(txtSellPer, e);
        Control.TextEditLine(lueUnit2, e);
        Control.TextEditLine(lueUnit2, e);
        Control.TextEditLine(txtUnitKNum, e);
        //App.Control.TextEditLine(txtSellFeeAmount, e);
        //App.Control.TextEditLine(txtTaxAmount, e);
        //App.Control.TextEditLine(txtDiscountAmount, e);
        //App.Control.TextEditLine(txtOtherAmount, e);
        //App.Control.TextEditLine(txtDiscountAmount2, e);
        // App.Control.TextEditLine(txtBuyFeeAmount2, e);
        //App.Control.TextEditLine(txtTaxAmount2, e);
    }


    private bool CheckValidationServiceAdd()
    {
        var isValid = true;

        if (string.IsNullOrEmpty(txtNum2.Text))
        {
            txtNum2.ErrorText = "لطفا تعداد را وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(lueUnit2.Text))
        {
            lueUnit2.ErrorText = "لطفا واحد را انتخاب نمایید";
            isValid = false;
        }

        if (_ServiceUnit != _ServiceUnit2 && _UnitKNum == 1)
        {
            txtUnitKNum.ErrorText = "لطفا ضریب را به درستی وارد نمایید";
            isValid = false;
        }

        return isValid;
    }

    private void CalcTotalAmount()
    {
        _TotalBuyAmount = (long)Convert.ToUInt64(_dtServiceReg.Compute("Sum(BuyAmount)", "1=1"));
        _TotalSellAmount = (long)Convert.ToUInt64(_dtServiceReg.Compute("Sum(SellAmount)", "1=1"));
        _DiscountAmount = (long)Convert.ToUInt64(_dtServiceReg.Compute("Sum(DiscountAmount)", "1=1"));
        _TaxAmount = (long)Convert.ToUInt64(_dtServiceReg.Compute("Sum(TaxAmount)", "1=1"));

        if (!DBNull.Value.Equals(_dtServiceReg.Compute("Sum(BuyAmount)", "IsBonus=true")))
            _TotalBonusAmount = (long)Convert.ToUInt64(_dtServiceReg.Compute("Sum(BuyAmount)", "IsBonus=true"));

        lblTotalBuyAmount.Text = _TotalBuyAmount.ToString("n0");
        lblTotalSellAmount.Text = _TotalSellAmount.ToString("n0");
        lblTotalBonusAmount.Text = _TotalBonusAmount.ToString("n0");

        CalcTotalAmount2();
    }

    private void CalcTotalAmount2()
    {
        _OtherAmount = Convert.ToInt64(txtOtherAmount.Text.Replace(",", ""));

        _DiscountAmount2 = (long)Convert.ToUInt64(txtDiscountAmount2.Text.Replace(",", ""));
        _TotalDiscountAmount = _DiscountAmount + _DiscountAmount2;
        lblTotalDiscountAmount.Text = _TotalDiscountAmount.ToString("n0");

        _TaxAmount2 = (long)Convert.ToUInt64(txtTaxAmount2.EditValue);
        _TotalTaxAmount = _TaxAmount + _TaxAmount2;
        lblTotalTaxAmount.Text = _TotalTaxAmount.ToString("n0");

        _ResultAmount = _TotalBuyAmount - _TotalDiscountAmount - _TotalBonusAmount + _OtherAmount + _TotalTaxAmount;

        lblTotalResultAmount.Text = _ResultAmount.ToString("n0");
    }


    private void btnServiceAdd_Click(object sender, EventArgs e)
    {
        //if (!CheckValidationServiceAdd()) 
        //    return;

        if (lueService.EditValue != null && _rowCurServie != null)
        {
            CalcTotalNum();
            CalcSellAmount();

            var rowService = _dtServiceReg.NewRow();
            rowService["RecNO"] = _dtServiceReg.Rows.Count + 1;

            rowService["CRUD"] = 0;
            rowService["InvDID"] = 0;

            rowService["ServiceID"] = _rowCurServie["ServiceID"];
            rowService["ServiceCode"] = _rowCurServie["ServiceCode"];
            rowService["ServiceName"] = _rowCurServie["Name"];

            rowService["Num2"] = _Num2;
            rowService["Unit2"] = _ServiceUnit2;
            rowService["Unit2Name"] = lueUnit2.Properties.GetDisplayText(lueUnit2.EditValue);
            rowService["UnitKNum"] = _UnitKNum;
            rowService["Unit"] = _ServiceUnit;
            rowService["UnitName"] = _rowCurServie["ServiceUnitName"];


            _BuyAmount = (long)(_BuyFeeAmount * _Num);
            _BuyAmount2 = (long)(_BuyFeeAmount2 * _Num2);


            rowService["Num"] = _Num;
            rowService["IsBonus"] = (bool)chkIsBonus.EditValue;
            rowService["BuyFeeAmount2"] = _BuyFeeAmount2;

            rowService["BuyAmount2"] = _BuyAmount2;


            rowService["SellPer"] = txtSellPer.EditValue;
            rowService["SellFeeAmount"] = _SellFeeAmount;
            rowService["SellFeeAmount2"] = _SellFeeAmount2;

            rowService["BuyFeeAmount"] = _BuyAmountLast; //_BuyFeeAmount;
            rowService["BuyAmount"] = _BuyAmount;

            rowService["TaxAmount"] = txtTaxAmount.EditValue;
            rowService["DiscountAmount"] = txtDiscountAmount.EditValue;

            rowService["BuyAmountLast"] = _BuyAmountLast;
            rowService["StockNum"] = _StockNum;
            rowService["StockNumAll"] = _StockNumAll;
            rowService["SellAmountLast"] = _SellAmountLast;

            rowService["SellAmount"] = _SellAmount;
            rowService["SellAmount2"] = _SellAmount2;

            _ResultAmount = _SellAmount;
            rowService["ResultAmount"] = _ResultAmount;


            _dtServiceReg.Rows.Add(rowService);
            grdServiceReg.DataSource = _dtServiceReg;

            lueService.EditValue = null;
            lblServiceCode.Text = null;
            txtNum2.Text = "0";
            lueUnit2.EditValue = 0;
            txtUnitKNum.Text = "1";
            txtBuyFeeAmount2.Text = "0";
            txtSellFeeAmount.Text = "0";
            txtTaxAmount.Text = "0";
            txtDiscountAmount.Text = "0";
            lblStockNum.Text = null;
            lblBuyAmountLast.Text = null;
            lblSellAmountLast.Text = null;
            lblTitleName.Text = null;

            _ServiceUnit = 0;
            _ServiceUnit2 = 0;
            _BuyFeeAmount = 0;
            _BuyFeeAmount2 = 0;
            _BuyAmount = 0;
            _BuyAmount2 = 0;
            _UnitKNum = 0;
            _Num = 0;
            _Num2 = 0;
            _SellFeeAmount = 0;
            _SellFeeAmount2 = 0;
            _SellAmount = 0;
            _SellAmount2 = 0;
            _BuyAmountLast = 0;
            _SellAmountLast = 0;
            _StockNum = 0;
            _StockNumAll = 0;


            CalcTotalAmount();
        }

        ActiveControl = lueService;
    }


    private void lueService_EditValueChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lueService.EditValue) == 0)
        {
            panServiceProp.Visible = true;
            return;
        }

        panServiceProp.Visible = true;
        _rowCurServie = lueService.Properties.GetDataSourceRowByKeyValue(lueService.EditValue) as DataRowView;

        lblServiceCode.Text = _rowCurServie["ServiceCode"].ToString();
        lblServiceUnit.Text = _rowCurServie["ServiceUnitName"].ToString();

        _ServiceUnit = Convert.ToInt32(_rowCurServie["ServiceUnit"]);
        _ServiceUnit2 = Convert.ToInt32(_rowCurServie["ServiceUnit2"]);
        lueUnit2.EditValue = _ServiceUnit2;


        _SellAmountLast = Convert.ToInt64(_rowCurServie["Price"]);
        _SellFeeAmount = _SellAmountLast;
        lblSellAmountLast.Text = _SellAmountLast.ToString("n0");

        lblTitleName.Text = lueService.Text;
        lblUnit.Text = _rowCurServie["ServiceUnitName"].ToString();

        _ItemData = _bllStorage.GetItemData(0, _StoragePeriodID,
            _rowCurServie["ServiceCategory"], _rowCurServie["ServiceCode"]);


        if (_ItemData.Rows.Count > 0) _StockNumAll = Convert.ToDouble(_ItemData.Rows[0]["StockNum"]);

        _ItemData = _bllStorage.GetItemData(_StorageID, _StoragePeriodID,
            _rowCurServie["ServiceCategory"], _rowCurServie["ServiceCode"]);

        lblStockNum.Text = "0";


        if (_ItemData.Rows.Count > 0)
        {
            _BuyAmountLast = Convert.ToInt64(_ItemData.Rows[0]["BuyAmountLast"]);
            _BuyFeeAmount = _BuyAmountLast;
            lblBuyAmountLast.Text = _BuyAmountLast.ToString("n0");

            _StockNum = Convert.ToDouble(_ItemData.Rows[0]["StockNum"]);
            lblStockNum.Text = _StockNum.ToString();

            CheckStockNumForInvOutput();
        }

        //ServiceRegLoad(_InvPageID);

        //txtBuyFeeAmount2.EditValue = (_dtServiceReg.Rows[0]["BuyFeeAmount2"]);
        //txtSellPer.EditValue = (_dtServiceReg.Rows[0]["SellPer"]); 
        //txtSellFeeAmount.EditValue = (_dtServiceReg.Rows[0]["SellFeeAmount"]); 
        //txtTaxAmount.EditValue = (_dtServiceReg.Rows[0]["TaxAmount"]); 
        //txtBuyFeeAmount.EditValue = (_dtServiceReg.Rows[0]["BuyFeeAmount"]); 
        //txtSellFeeAmount2.EditValue = (_dtServiceReg.Rows[0]["BuyFeeAmount2"]); 
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (string.IsNullOrEmpty(lueDestDep.Text))
        {
            lueDestDep.ErrorText = "لطفا بخش مقصد را انتخاب نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(lueInvCategoryType.Text))
        {
            lueInvCategoryType.ErrorText = "لطفا نوع حواله خروج را انتخاب نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(datDeliverDate.Text))
        {
            datDeliverDate.ErrorText = "لطفا تاریخ تحویل را وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(lueDeliverPers.Text))
        {
            lueDeliverPers.ErrorText = "لطفا تحویل دهنده را انتخاب نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(lueTransferee.Text))
        {
            lueTransferee.ErrorText = "لطفا تحویل گیرنده را انتخاب نمایید";
            isValid = false;
        }


        return isValid;
    }

    private void btnServiceRegSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        _DiscountAmount2 = (long)Convert.ToUInt64(txtDiscountAmount2.Text.Replace(",", ""));
        _OtherAmount = Convert.ToInt64(txtOtherAmount.Text.Replace(",", ""));

        _dtInvPage = _bllInvOutput.InvPageSave
        (_InvPageID,
            lueInvCategoryType.EditValue,
            _StorageID,
            _StoragePeriodID,
            txtOrderCode.Text,
            datDeliverDate.Text + " 00:00",
            lueTransferee.EditValue,
            _DiscountAmount2,
            _OtherAmount,
            lueDeliverPers.EditValue,
            lueDestDep.EditValue,
            txtDescription.Text,
            GL.UserPersID,
            0);


        if (_dtInvPage.Rows.Count == 0) return;

        _InvPageID = Convert.ToInt32(_dtInvPage.Rows[0]["InvPageID"]);
        _InvPageCode = Convert.ToInt32(_dtInvPage.Rows[0]["InvPageCode"]);

        txtInvPageCode.Text = _InvPageCode.ToString();
        //ucMessage1.Visible = true;

        InvPageItemSave();
        //_bllInvOutput.InvPageAmountSave(_InvPageID, GL.UserPersID);

        _bllInvOutput.InvPageAmountSave(_InvPageID, GL.UserPersID);

        ////EncPageLoad();

        PageDataLoad(_InvPageCode);

        ucShowTextSave.ShowText("سند خروجی ذخیره گردید", 5000);
    }

    private void InvPageItemSave()
    {
        var crud = 0;
        var result = _dtServiceReg.Select("CRUD<>2");

        foreach (var row in result)
        {
            crud = Convert.ToInt32(row["CRUD"]);
            _bllInvOutput.InvPageItemSave
            (crud, _InvPageID, row["InvDID"], lueInvCategoryType.EditValue, _StorageID, _StoragePeriodID,
                row["ServiceID"],
                row["Num2"], row["Unit2"], row["UnitKNum"], row["Unit"], row["Num"],
                row["StockNum"], row["StockNumAll"], row["IsBonus"], row["BuyFeeAmount2"], row["BuyAmount2"],
                row["SellPer"], row["SellFeeAmount"], row["SellFeeAmount2"],
                row["BuyFeeAmount"], row["BuyAmount"], row["DiscountAmount"],
                row["TaxAmount"], row["BuyAmountLast"], row["SellAmount"],
                row["SellAmount2"], row["SellAmountLast"], row["ResultAmount"]
            );

            if (crud == 0) row["CRUD"] = 2;

            if (crud == 4) row.Delete();
        }
    }

    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtInvPageCode, e);
        Control.TextEditLine(lueDestDep, e);
        Control.TextEditLine(lueInvCategoryType, e);
        Control.TextEditLine(lueDeliverPers, e);
        Control.TextEditLine(lueTransferee, e);
        Control.TextEditLine(datDeliverDate, e);
        Control.TextEditLine(txtDescription, e);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucEncCur = new UcEncCur
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        ucEncCur._ModID = _ModID;
        panUC.Controls.Add(ucEncCur);
    }

    private void ServiceRegLoad(int InvPageID)
    {
        var bllstorage = new InvOutput();
        _dtServiceReg = bllstorage.ServiceRegLoad(_InvPageID);

        if (_dtServiceReg.Rows.Count > 0) grdServiceReg.DataSource = _dtServiceReg;
    }

    private void grvServiceReg_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        var view = sender as GridView;

        var crud = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "CRUD"));
        if (crud == 4) e.Appearance.BackColor = Color.PaleVioletRed;
    }

    private void lueService_KeyPress(object sender, KeyPressEventArgs e)
    {
        //if (e.KeyChar == 43 && _rowCurServie != null)  //+
        //{
        //    btnServiceAdd_Click(sender, e);
        //    //EncSearch(int.Parse(txtEncID.Text));
        //   lueService.EditValue = null;
        //   e.KeyChar = (char)0;

        //}
    }

    private void txtDiscountAmount2_EditValueChanged(object sender, EventArgs e)
    {
        CalcTotalAmount2();
    }

    private void txtOtherAmount_EditValueChanged(object sender, EventArgs e)
    {
        CalcTotalAmount2();
    }

    private void txtDiscountAmount2_TextChanged(object sender, EventArgs e)
    {
        if (txtDiscountAmount2.Text != string.Empty)
        {
            txtDiscountAmount2.Text =
                string.Format("{0:N0}", double.Parse(txtDiscountAmount2.Text.Replace(",", "")));
            txtDiscountAmount2.Select(txtDiscountAmount2.Text.Length, 0);
        }
    }

    private void txtDiscountAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtDiscountAmount.Text != string.Empty)
        {
            txtDiscountAmount.Text = string.Format("{0:N0}", double.Parse(txtDiscountAmount.Text.Replace(",", "")));
            txtDiscountAmount.Select(txtDiscountAmount.Text.Length, 0);
        }
    }

    private void txtOtherAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtOtherAmount.Text != string.Empty)
        {
            txtOtherAmount.Text = string.Format("{0:N0}", double.Parse(txtOtherAmount.Text.Replace(",", "")));
            txtOtherAmount.Select(txtOtherAmount.Text.Length, 0);
        }
    }


    private void txtTaxAmount2_EditValueChanged(object sender, EventArgs e)
    {
        CalcTotalAmount2();
    }

    private void lblPageSign_Click(object sender, EventArgs e)
    {
        var strMessage = "رسید ورود به شماره " + _InvPageCode
                                               + "\n"
                                               + "\nامضا و بسته شود؟";

        var sui = new SUI();
        var result = sui.FormShowMessage("امضا رسید ورود", strMessage);

        if (result == DialogResult.No) return;

        _bllStorage.InvPageSign(_InvPageID);
        PageDataLoad(_InvPageCode);
    }

    private void txtInvPageCode_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtInvPageCode.Text))
        {
            PageDataLoad(Convert.ToInt32(txtInvPageCode.EditValue));

            if (_InvPageID == 0)
            {
                FormNull();
                txtInvPageCode.ErrorText = "سندی یافت نشد";
            }
        }
    }

    private void lblPageNew_Click(object sender, EventArgs e)
    {
        FormNull();
    }

    private void lblPagePreRec_Click(object sender, EventArgs e)
    {
        var oldInvPageCode = _InvPageCode;

        _InvPageCode -= 1;
        PageDataLoad(_InvPageCode);

        if (_InvPageCode == 0) PageDataLoad(oldInvPageCode);
    }

    private void lblPageNextRec_Click(object sender, EventArgs e)
    {
        var oldInvPageCode = _InvPageCode;

        _InvPageCode += 1;
        PageDataLoad(_InvPageCode);

        if (_InvPageCode == 0) PageDataLoad(oldInvPageCode);
    }

    private void btnServiceDel_Click(object sender, EventArgs e)
    {
        var rows = grvServiceReg.GetSelectedRows();
        foreach (var i in rows)
        {
            grvServiceReg.GetDataRow(i);

            var NO = Convert.ToInt32(grvServiceReg.GetDataRow(i)["RecNo"].ToString());

            var row = _dtServiceReg.Select("RecNo=" + NO)[0];
            if (Convert.ToInt32(row["InvDID"]) == 0)
                row.Delete();
            else
                row["CRUD"] = 4;
        }

        grdServiceReg.DataSource = null;
        grdServiceReg.DataSource = _dtServiceReg;
    }

    private void txtBuyPrice_Leave(object sender, EventArgs e)
    {
        CalcSellAmount();
    }

    private void txtSellPer_EditValueChanged(object sender, EventArgs e)
    {
        CalcSellAmount();
    }

    private void CalcSellAmount()
    {
        txtSellFeeAmount.EditValue = _SellFeeAmount;
        txtSellFeeAmount2.EditValue = _SellFeeAmount2;

        _BuyAmount = (long)(_BuyFeeAmount * _Num);
        _BuyAmount2 = (long)(_BuyFeeAmount2 * _Num2);

        _SellAmount = (long)(_SellFeeAmount * _Num);
        _SellAmount2 = (long)(_SellFeeAmount2 * _Num2);
    }

    private void txtSellFeeAmount2_Leave(object sender, EventArgs e)
    {
        _SellFeeAmount2 = (long)txtSellFeeAmount2.EditValue;
        _SellFeeAmount = _SellFeeAmount2 / _UnitKNum;

        txtSellFeeAmount.EditValue = _SellFeeAmount;
        txtSellFeeAmount2.EditValue = _SellFeeAmount2;

        _SellAmount = (long)(_SellFeeAmount * _Num);
        _SellAmount2 = (long)(_SellFeeAmount2 * _Num2);


        double sellPer = 0;
        sellPer = _SellFeeAmount2 / Convert.ToDouble(_BuyFeeAmount2);

        txtSellPer.EditValue = sellPer;


        //String.Format("{0:0.00}", someValue);
    }

    private void txtSellFeeAmount2_EditValueChanged(object sender, EventArgs e)
    {
    }

    private void txtSellFeeAmount2_KeyUp(object sender, KeyEventArgs e)
    {
    }

    private void CalcTotalNum()
    {
        if (string.IsNullOrEmpty(txtNum2.Text) || string.IsNullOrEmpty(txtUnitKNum.Text)) return;

        _Num2 = double.Parse(txtNum2.Text.Replace('.', '/'));
        _UnitKNum = Convert.ToInt32(txtUnitKNum.EditValue);
        _Num = _Num2 * _UnitKNum;
        lblNum.Text = _Num.ToString();
    }

    private void txtServiceNum_EditValueChanged(object sender, EventArgs e)
    {
        CalcTotalNum();
        CheckStockNumForInvOutput();
    }

    private void CheckStockNumForInvOutput()
    {
        if (_StockNum <= 0 || _Num > _StockNum)
        {
            txtNum2.ErrorText = "موجودی کافی نیست";
            btnServiceAdd.Enabled = false;
        }
        else
        {
            btnServiceAdd.Enabled = true;
            txtNum2.ErrorText = null;
        }
    }

    private void btnRegEdit_Click(object sender, EventArgs e)
    {
        //_IsEditRow = true;
        //DataRow row = grvServiceReg.GetDataRow(grvServiceReg.FocusedRowHandle);

        //_RecNoServiceReg = Convert.ToInt32(row["RecNo"]);
        //lueService.EditValue = row["ServiceID"];
        //txtNum2.Text = row["Num"].ToString();
        //lueUnit2.EditValue = row["Unit"].ToString();
        //txtUnitKNum.Text = row["UnitKNum"].ToString();
        //lblNum.Text = row["TotalServiceNum"].ToString();
        //lblUnit.Text = row["UnitName"].ToString();
        //txtBuyFeeAmount2.Text = row["BuyPrice"].ToString();
        //txtSellFeeAmount.Text = row["SellPrice"].ToString();
        //txtTaxAmount.Text = row["TaxAmount"].ToString();
        //txtDiscountAmount.Text = row["DiscountAmount"].ToString();
    }

    private void panFactorUnit2_Paint(object sender, PaintEventArgs e)
    {
        //App.Control.TextEditLine(txtBuyFeeAmount, e);
        //App.Control.TextEditLine(txtSellFeeAmount2, e);
    }

    private void txtBuyFactorFeePrice_EditValueChanged(object sender, EventArgs e)
    {
        _BuyFeeAmount2 = Convert.ToInt64(txtBuyFeeAmount2.EditValue);
        _UnitKNum = Convert.ToInt32(txtUnitKNum.Text);

        _BuyFeeAmount = _BuyFeeAmount2 / _UnitKNum;


        if (_ServiceUnit == _ServiceUnit2)
            txtBuyFeeAmount.Text = txtBuyFeeAmount2.Text;
        else
            txtBuyFeeAmount.Text = _BuyFeeAmount.ToString();

        CalcSellAmount();
    }

    private void lueUnit2_EditValueChanged(object sender, EventArgs e)
    {
        //_ServiceUnit2 = Convert.ToInt32(lueUnit2.EditValue);

        //if (_ServiceUnit == _ServiceUnit2)
        //    panFactorUnit2.Visible = false;
        //else
        //{
        //    panFactorUnit2.Visible = true;
        //}
    }

    private void txtUnitKNum_EditValueChanged(object sender, EventArgs e)
    {
        CalcTotalNum();
    }


    private void barBtnInvOutputReport_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqvInvOutputPageFullName =
            @"select * 
                from vInvOutputPageFullName
                where InvPageID = @InvPageID";

        tsqvInvOutputPageFullName = tsqvInvOutputPageFullName.Replace("@InvPageID", _InvPageID.ToString());


        var tsqvInvOutputPageItemFullName =
            @"select * 
                from vInvOutputPageItemFullName
                where InvPageID = @InvPageID";

        tsqvInvOutputPageItemFullName = tsqvInvOutputPageItemFullName.Replace("@InvPageID", _InvPageID.ToString());


        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100016);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;

        ((StiSqlSource)report.Dictionary.DataSources["vInvOutputPageFullName"]).SqlCommand =
            tsqvInvOutputPageFullName;
        ((StiSqlSource)report.Dictionary.DataSources["vInvOutputPageItemFullName"]).SqlCommand =
            tsqvInvOutputPageItemFullName;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;

        report.Show();
    }
}