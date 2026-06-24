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
using Saman.Form.Def;
using Stimulsoft.Report.Dictionary;
using Control = App.Control;

namespace Saman.UC.Storage;

public partial class UcInvInput : XtraUserControl
{
    private readonly Stimul _appStimul = new();

    private readonly InvInput _bllInvInput = new();

    private readonly BLL.Report.Report _bllReport = new();
    private readonly BLL.Storage.Storage _bllStorage = new();


    private readonly DataTable _dtPage = new();

    private readonly DataTable _dtPageDataLoad;

    private readonly bool _IsEditRow;
    private readonly int _RecNoServiceReg;

    private long _BuyAmountLast, _SellAmountLast;
    private double _BuyFeeAmount;
    private long _BuyFeeAmount2, _BuyAmount, _BuyAmount2;

    private DataTable _dtInvPage = new();
    private DataTable _dtServiceReg = new();


    private int _InvPageID, _InvPageCode;

    private int _InvType;
    private bool _IsBonus;
    private bool _IsSigned;
    private DataTable _ItemData = new();
    private double _Num, _Num2;
    private DataRowView _rowCurServie;

    private long _SellFeeAmount, _SellFeeAmount2, _SellAmount, _SellAmount2, _ResultAmount;

    private int _ServiceUnit, _ServiceUnit2;
    private double _StockNum, _StockNumAll;

    private long _TotalBuyAmount,
        _TotalSellAmount,
        _DiscountAmountItem,
        _DiscountAmount,
        _DiscountAmount2,
        _TotalDiscountAmount,
        _TaxAmountItem,
        _TaxAmount,
        _TaxAmount2,
        _TotalTaxAmount,
        _OtherAmount,
        _TotalBonusAmount,
        _TotalResultAmount;

    private int _UnitKNum;

    public UcInvInput()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }
    public int _DepID { get; set; }
    public int _StorageID { get; set; }
    public int _StoragePeriodID { get; set; }

    public bool _ReadOnly { get; set; }

    private int _PageDataLoadRecNo { get; set; }


    private void UcRegService_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        ActiveControl = lueService;

        txtSellPer.Text = "0";


        UL.FillLookup(lueService, null, cSet.cnStringA, false, "*", "vServiceDrug", "vServiceDrug",
            "EndDate IS NULL", "CAST(ServiceCode as INT)");
        UL.FillLookup(lueInvCategoryType, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "InvInputType",
            "ADName='InvInputType'", null);
        UL.FillLookup(lueSeller, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "Seller",
            "ADName='Seller' AND isDisable = 0",
            null);
        UL.FillLookup(lueTransferee, "0", cSet.cnStringERP, false, "PersID,Name", "vPersonnel", "vPersonnel", null,
            null);
        UL.FillLookup(lueUnit2, "0", cSet.cnStringERP, false, "ID,Name", "vADR", "Unit", "ADName='Unit'", null);

        var bllStorageTable = new DT();
        bllStorageTable.CreateTableServiceReg(_dtServiceReg);

        //ServiceRegLoad(_InvPageID);
        PageDataLoad(_bllInvInput.InvPageCodeLast(_StorageID));

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
        lueInvCategoryType.EditValue = 0;
        lueSeller.EditValue = 0;

        chkIsBonus.EditValue = false;

        lueTransferee.EditValue = 0;
        datInvoiceDate.Text = null;
        txtInvoiceSerial.Text = "";
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

        lblPageSign.Enabled = true;
        btnServiceAdd.Enabled = true;
        btnServiceDel.Enabled = true;
        btnRegEdit.Enabled = true;
        btnServiceRegSave.Enabled = true;


        _dtServiceReg.Clear();
        grdServiceReg.DataSource = _dtServiceReg;

        lueInvCategoryType.Enabled = true;
        _ReadOnly = false;
        CheckReadOnly();

        ActiveControl = lueService;
    }


    private void PageDataLoad(int InvPageCode)
    {
        _InvPageID = 0;

        var bllInvInputPage = new InvInput();
        _dtInvPage = bllInvInputPage.PageDataLoad(_StorageID, InvPageCode);

        if (_dtInvPage.Rows.Count == 0) return;
        //DataRow rowPage = _dtEncPage.Select("PageID=" + _PageID)[0];
        //lblPageID.Text = _PageID.ToString();
        _InvPageCode = InvPageCode;
        txtInvPageCode.Text = _InvPageCode.ToString();

        _InvPageID = Convert.ToInt32(_dtInvPage.Rows[0]["InvPageID"]);
        _ReadOnly = Convert.ToBoolean(_dtInvPage.Rows[0]["IsSign"]);

        lblInvDate.Text = _dtInvPage.Rows[0]["Date"].ToString().Replace(' ', ' ');
        lueInvCategoryType.EditValue = Convert.ToInt32(_dtInvPage.Rows[0]["InvCategoryType"]);
        lueSeller.EditValue = Convert.ToInt32(_dtInvPage.Rows[0]["SellerID"]);
        datInvoiceDate.EditValue = Convert.ToDateTime(_dtInvPage.Rows[0]["Date2"]);
        txtInvoiceSerial.Text = _dtInvPage.Rows[0]["InvPageCode2"].ToString();
        txtDescription.Text = _dtInvPage.Rows[0]["Description"].ToString();
        lueTransferee.EditValue = Convert.ToInt32(_dtInvPage.Rows[0]["TransfereeID"]);

        lblTotalBuyAmount.Text = Convert.ToInt64(_dtInvPage.Rows[0]["BuyAmount"]).ToString("n0");
        lblTotalSellAmount.Text = Convert.ToInt64(_dtInvPage.Rows[0]["SellAmount"]).ToString("n0");
        lblTotalDiscountAmount.Text = Convert.ToInt64(_dtInvPage.Rows[0]["TotalDiscountAmount"]).ToString("n0");
        lblTotalBonusAmount.Text = Convert.ToInt64(_dtInvPage.Rows[0]["BonusAmount"]).ToString("n0");
        lblTotalResultAmount.Text = Convert.ToInt64(_dtInvPage.Rows[0]["ResultAmount"]).ToString("n0");

        lblTotalTaxAmount.Text = _dtInvPage.Rows[0]["TotalTaxAmount"].ToString();


        txtOtherAmount.Text = _dtInvPage.Rows[0]["OtherAmount"].ToString();
        txtDiscountAmount2.Text = _dtInvPage.Rows[0]["DiscountAmount2"].ToString();
        txtTaxAmount2.Text = _dtInvPage.Rows[0]["TaxAmount2"].ToString();

        _OtherAmount = Convert.ToInt64(_dtInvPage.Rows[0]["OtherAmount"]);
        _DiscountAmount2 = Convert.ToInt64(_dtInvPage.Rows[0]["DiscountAmount2"]);
        _TaxAmount2 = Convert.ToInt64(_dtInvPage.Rows[0]["TaxAmount2"]);

        ServiceRegLoad();
        CheckReadOnly();

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
        _dtServiceReg = null;
        var bllServiceReg = new InvInput();
        _dtServiceReg = bllServiceReg.ServiceRegLoad(_InvPageID);

        if (_dtServiceReg.Rows.Count == 0)
        {
            grdServiceReg.DataSource = null;
            return;
        }

        grdServiceReg.DataSource = _dtServiceReg;
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.LookupEditLine(lueService, e);
        Control.TextEditLine(txtNum2, e);
        Control.TextEditLine(txtSellPer, e);
        Control.TextEditLine(lueUnit2, e);
        Control.TextEditLine(lueUnit2, e);
        Control.TextEditLine(txtUnitKNum, e);
        Control.TextEditLine(txtSellFeeAmount, e);
        Control.TextEditLine(txtSellFeeAmount, e);
        Control.TextEditLine(txtTaxAmount, e);
        Control.TextEditLine(txtDiscountAmount, e);
        Control.TextEditLine(txtOtherAmount, e);
        Control.TextEditLine(txtDiscountAmount2, e);
        Control.TextEditLine(txtBuyFeeAmount2, e);
        Control.TextEditLine(txtTaxAmount2, e);
        Control.TextEditLine(datExpirationDate, e);
    }


    private bool CheckValidationServiceAdd()
    {
        var isValid = true;

        if (_InvType == 0)
        {
            lueInvCategoryType.ErrorText = "لطفا نوع رسید وارد نمایید";
            isValid = false;
        }

        if (Convert.ToDouble(txtNum2.EditValue) == 0)
        {
            txtNum2.ErrorText = "لطفا تعداد را وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(lueUnit2.Text))
        {
            lueUnit2.ErrorText = "لطفا واحد را انتخاب نمایید";
            isValid = false;
        }

        long buyFeeAmount2 = 0;
        buyFeeAmount2 = Convert.ToInt64(txtBuyFeeAmount2.Text.Replace(",", ""));


        if (buyFeeAmount2 == 0 && _InvType != 2)
        {
            txtBuyFeeAmount2.ErrorText = "لطفا مبلغ خرید را وارد نمایید";
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
        //if (_dtInvPage.Rows.Count > 0)
        //    return;

        _OtherAmount = Convert.ToInt64(txtOtherAmount.Text.Replace(",", ""));

        _DiscountAmount2 = (long)Convert.ToUInt64(txtDiscountAmount2.Text.Replace(",", ""));
        _TotalDiscountAmount = _DiscountAmount + _DiscountAmount2;
        lblTotalDiscountAmount.Text = _TotalDiscountAmount.ToString("n0");

        _TaxAmount2 = (long)Convert.ToUInt64(txtTaxAmount2.Text.Replace(",", ""));
        _TotalTaxAmount = _TaxAmount + _TaxAmount2;
        lblTotalTaxAmount.Text = _TotalTaxAmount.ToString("n0");

        _TotalResultAmount = _TotalBuyAmount - _TotalDiscountAmount - _TotalBonusAmount + _OtherAmount +
                             _TotalTaxAmount;

        lblTotalResultAmount.Text = _TotalResultAmount.ToString("n0");
    }


    private void btnServiceAdd_Click(object sender, EventArgs e)
    {
        if (!CheckValidationServiceAdd()) return;

        panFactorUnit2.Visible = false;

        if (lueService.EditValue != null && _rowCurServie != null)
        {
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

            rowService["Num"] = _Num;

            _IsBonus = (bool)chkIsBonus.EditValue;
            rowService["IsBonus"] = _IsBonus;
            rowService["BuyFeeAmount2"] = _BuyFeeAmount2;

            rowService["BuyAmount2"] = _BuyAmount2;


            rowService["SellPer"] = txtSellPer.EditValue;
            rowService["SellFeeAmount"] = _SellFeeAmount;
            rowService["SellFeeAmount2"] = _SellFeeAmount2;

            rowService["BuyFeeAmount"] = _BuyFeeAmount;
            rowService["BuyAmount"] = _BuyAmount;

            _TaxAmountItem = Convert.ToInt64(txtTaxAmount.Text.Replace(",", ""));
            rowService["TaxAmount"] = _TaxAmountItem;

            _DiscountAmountItem = Convert.ToInt64(txtDiscountAmount.Text.Replace(",", ""));
            rowService["DiscountAmount"] = _DiscountAmountItem;

            rowService["ExpirationDate"] = datExpirationDate.Text + " 00:00";


            rowService["BuyAmountLast"] = _BuyAmountLast;
            rowService["StockNum"] = _StockNum;
            rowService["StockNumAll"] = _StockNumAll;
            rowService["SellAmountLast"] = _SellAmountLast;

            rowService["SellAmount"] = _SellAmount;
            rowService["SellAmount2"] = _SellAmount2;


            if (_IsBonus)
                _ResultAmount = 0;
            else
                _ResultAmount = _BuyAmount + _TaxAmountItem - _DiscountAmountItem;

            rowService["ResultAmount"] = _ResultAmount;

            _dtServiceReg.Rows.Add(rowService);
            grdServiceReg.DataSource = _dtServiceReg;

            lueService.EditValue = null;
            lblServiceCode.Text = null;
            txtNum2.Text = "0";
            lueUnit2.EditValue = 0;
            txtUnitKNum.Text = "1";
            datExpirationDate.Text = null;
            txtBuyFeeAmount2.Text = "0";
            txtSellFeeAmount.Text = "0";
            txtTaxAmount.Text = "0";
            txtDiscountAmount.Text = "0";
            lblStockNum.Text = null;
            lblBuyAmountLast.Text = null;
            lblSellAmountLast.Text = null;
            lblTitleName.Text = null;
            lblUnit.Text = null;

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
            _ResultAmount = 0;

            _StockNum = 0;
            _StockNumAll = 0;
            _IsBonus = false;

            txtSellFeeAmount2.EditValue = 0;
            txtSellPer.EditValue = 0;

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

        panFactorUnit2.Visible = false;

        panServiceProp.Visible = true;
        _rowCurServie = lueService.Properties.GetDataSourceRowByKeyValue(lueService.EditValue) as DataRowView;

        lblServiceCode.Text = _rowCurServie["ServiceCode"].ToString();
        lblServiceUnit.Text = _rowCurServie["ServiceUnitName"].ToString();

        _ServiceUnit = Convert.ToInt32(_rowCurServie["ServiceUnit"]);
        _ServiceUnit2 = Convert.ToInt32(_rowCurServie["ServiceUnit2"]);
        lueUnit2.EditValue = _ServiceUnit2;

        _SellAmountLast = Convert.ToInt64(_rowCurServie["Price"]);
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
            lblBuyAmountLast.Text = _BuyAmountLast.ToString("n0");

            _StockNum = Convert.ToDouble(_ItemData.Rows[0]["StockNum"]);
            lblStockNum.Text = _StockNum.ToString();

            lblStockNum.ForeColor = _StockNum <= 0 ? Color.Red : Color.FromArgb(103, 252, 100);
        }
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (string.IsNullOrEmpty(lueInvCategoryType.Text))
        {
            lueInvCategoryType.ErrorText = "لطفا نوع حواله ورود را انتخاب نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(lueSeller.Text))
        {
            lueSeller.ErrorText = "لطفا فروشنده را انتخاب نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(lueTransferee.Text))
        {
            lueTransferee.ErrorText = "لطفا تحویل گیرنده را انتخاب نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(datInvoiceDate.Text))
        {
            datInvoiceDate.ErrorText = "لطفا تاریخ فاکتور را وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(txtInvoiceSerial.Text))
        {
            txtInvoiceSerial.ErrorText = "لطفا شماره فاکتور را وارد نمایید";
            isValid = false;
        }

        return isValid;
    }

    private void btnServiceRegSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        _dtInvPage = _bllInvInput.InvPageSave
        (_InvPageID,
            lueInvCategoryType.EditValue,
            _StorageID,
            _StoragePeriodID,
            txtInvoiceSerial.Text,
            datInvoiceDate.Text + " 00:00",
            lueSeller.EditValue,
            lueTransferee.EditValue,
            _DiscountAmount2,
            _OtherAmount,
            txtDescription.Text,
            _TaxAmount2,
            GL.UserPersID);

        if (_dtInvPage.Rows.Count == 0) return;

        _InvPageID = Convert.ToInt32(_dtInvPage.Rows[0]["InvPageID"]);
        _InvPageCode = Convert.ToInt32(_dtInvPage.Rows[0]["InvPageCode"]);

        txtInvPageCode.Text = _InvPageCode.ToString();
        //ucMessage1.Visible = true;

        InvPageItemSave();
        _bllInvInput.InvPageAmountSave(_InvPageID, GL.UserPersID);

        //bllPage.PageAmountSave(_PageID);

        //EncPageLoad();

        PageDataLoad(_InvPageCode);

        ucShowText1.ShowText("سند ورودی ذخیره گردید", 5000);
    }

    private void InvPageItemSave()
    {
        if (_dtServiceReg.Rows.Count == 0) return;

        var crud = 0;

        foreach (DataRow row in _dtServiceReg.Rows)
        {
            crud = Convert.ToInt32(row["CRUD"]);
            _bllInvInput.InvPageItemSave
            (crud, _InvPageID, row["InvDID"], lueInvCategoryType.EditValue, _StorageID, _StoragePeriodID,
                row["ServiceID"], row["Num2"], row["Unit2"], row["UnitKNum"], row["Unit"], row["Num"],
                row["ExpirationDate"],
                row["StockNum"], row["StockNumAll"], row["IsBonus"], row["BuyFeeAmount2"], row["BuyAmount2"],
                row["SellPer"],
                row["SellFeeAmount"], row["SellFeeAmount2"], row["BuyFeeAmount"], row["BuyAmount"],
                row["DiscountAmount"], row["TaxAmount"], row["BuyAmountLast"], row["SellAmount"],
                row["SellAmount2"], row["SellAmountLast"], row["ResultAmount"]
            );

            if (crud == 0) row["CRUD"] = 2;

            if (crud == 4) row.Delete();
        }
    }

    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtInvPageCode, e);
        Control.TextEditLine(lueInvCategoryType, e);
        Control.TextEditLine(lueSeller, e);
        Control.TextEditLine(lueTransferee, e);
        Control.TextEditLine(datInvoiceDate, e);
        Control.TextEditLine(txtInvoiceSerial, e);
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
        var bllstorage = new InvInput();
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
    }

    private void txtBuyFeeAmount2_TextChanged(object sender, EventArgs e)
    {
        if (txtBuyFeeAmount2.Text != string.Empty)
        {
            txtBuyFeeAmount2.Text = string.Format("{0:N0}", double.Parse(txtBuyFeeAmount2.Text.Replace(",", "")));
            txtBuyFeeAmount2.Select(txtBuyFeeAmount2.Text.Length, 0);
        }
    }

    private void txtSellFeeAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtSellFeeAmount.Text != string.Empty)
        {
            txtSellFeeAmount.Text = string.Format("{0:N0}", double.Parse(txtSellFeeAmount.Text.Replace(",", "")));
            txtSellFeeAmount.Select(txtSellFeeAmount.Text.Length, 0);
        }
    }

    private void txtTaxAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtTaxAmount.Text != string.Empty)
        {
            txtTaxAmount.Text = string.Format("{0:N0}", double.Parse(txtTaxAmount.Text.Replace(",", "")));
            txtTaxAmount.Select(txtTaxAmount.Text.Length, 0);
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

    private void txtDiscountAmount2_TextChanged(object sender, EventArgs e)
    {
        if (txtDiscountAmount2.Text != string.Empty)
        {
            txtDiscountAmount2.Text =
                string.Format("{0:N0}", double.Parse(txtDiscountAmount2.Text.Replace(",", "")));
            txtDiscountAmount2.Select(txtDiscountAmount2.Text.Length, 0);
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

    private void lueInvCategoryType_EditValueChanged(object sender, EventArgs e)
    {
        _InvType = Convert.ToInt32(lueInvCategoryType.EditValue);
        if (_InvType > 0) lueInvCategoryType.Enabled = false;
    }


    private void txtTaxAmount2_TextChanged(object sender, EventArgs e)
    {
        if (txtTaxAmount2.Text != string.Empty)
        {
            txtTaxAmount2.Text = string.Format("{0:N0}", double.Parse(txtTaxAmount2.Text.Replace(",", "")));
            txtTaxAmount2.Select(txtTaxAmount2.Text.Length, 0);
        }
    }

    private void txtNum2_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 43 && _rowCurServie != null) btnServiceAdd_Click(null, null);

        //if (e.KeyChar == 43 && _rowCurServie != null)
        //{
        //    btnServiceAdd_Click(sender, e);
        //    lueService.EditValue = null;
        //    e.KeyChar = (char)0;
        //}
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

    private void lblSeller_Click(object sender, EventArgs e)
    {
        var frm = new FrmDefADR
        {
            _ADID = 33
        };
        var result = frm.ShowDialog();

        lueSeller.EditValue = frm._ID;
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

    private void txtSellFeeAmount_Leave(object sender, EventArgs e)
    {
        if (Convert.ToDouble(txtSellPer.EditValue) < 1)
        {
            var sellPer = Convert.ToDouble(txtSellFeeAmount.EditValue) / Convert.ToDouble(txtBuyFeeAmount2.EditValue);
            txtSellPer.EditValue = sellPer.ToString("0.00");
            CalcSellAmount();
        }
    }

    private void CalcSellAmount()
    {
        if (string.IsNullOrEmpty(txtSellPer.EditValue.ToString())) return;

        //double sellPer = (100 + Convert.ToDouble(txtSellPer.EditValue)) / 100;
        //_SellFeeAmount = (long)(_BuyFeeAmount * sellPer);
        //_SellFeeAmount2 = (long)(_BuyFeeAmount2 * sellPer);

        //txtSellFeeAmount.EditValue = _SellFeeAmount;
        //txtSellFeeAmount2.EditValue = _SellFeeAmount2;

        //_BuyAmount = (long)(_BuyFeeAmount * _Num);
        //_BuyAmount2 = (long)(_BuyFeeAmount2 * _Num2);

        //_SellAmount = (long)(_SellFeeAmount * _Num);
        //_SellAmount2 = (long)(_SellFeeAmount2 * _Num2);

        var sellPer = Convert.ToDouble(txtSellPer.EditValue);
        if (sellPer > 1)
        {
            sellPer = (100 + Convert.ToDouble(txtSellPer.EditValue)) / 100;

            _SellFeeAmount = (long)(_BuyFeeAmount * sellPer);
            _SellFeeAmount2 = (long)(_BuyFeeAmount2 * sellPer);

            txtSellFeeAmount.EditValue = _SellFeeAmount;
            txtSellFeeAmount2.EditValue = _SellFeeAmount2;

            _BuyAmount = (long)(_BuyFeeAmount * _Num);
            _BuyAmount2 = (long)(_BuyFeeAmount2 * _Num2);

            _SellAmount = (long)(_SellFeeAmount * _Num);
            _SellAmount2 = (long)(_SellFeeAmount2 * _Num2);
        }
        else
        {
            var sellFeeAmount = Convert.ToDouble(txtSellFeeAmount.EditValue);

            _SellFeeAmount = (long)sellFeeAmount;
            //_SellFeeAmount2 = (long)(_BuyFeeAmount2 * sellPer);
            var unitknum = Convert.ToDouble(txtUnitKNum.EditValue);

            _SellFeeAmount2 = (long)(sellFeeAmount / unitknum);

            txtSellFeeAmount.EditValue = _SellFeeAmount;
            txtSellFeeAmount2.EditValue = _SellFeeAmount2;

            _BuyAmount = (long)(_BuyFeeAmount * _Num);
            _BuyAmount2 = (long)(_BuyFeeAmount2 * _Num2);

            _SellAmount = (long)(_SellFeeAmount * _Num);
            _SellAmount2 = (long)(_SellFeeAmount2 * _Num2);
        }
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
        Control.TextEditLine(txtBuyFeeAmount, e);
        Control.TextEditLine(txtSellFeeAmount2, e);
    }

    private void txtBuyFactorFeePrice_EditValueChanged(object sender, EventArgs e)
    {
        _BuyFeeAmount2 = Convert.ToInt64(txtBuyFeeAmount2.Text.Replace(",", ""));
        _UnitKNum = Convert.ToInt32(txtUnitKNum.Text);

        _BuyFeeAmount = Convert.ToDouble(_BuyFeeAmount2) / _UnitKNum;
        // _BuyFeeAmount = _BuyFeeAmount2 / _UnitKNum;

        if (_ServiceUnit == _ServiceUnit2)
            txtBuyFeeAmount.Text = txtBuyFeeAmount2.Text;
        else
            txtBuyFeeAmount.Text = _BuyFeeAmount.ToString();

        CalcSellAmount();
    }

    private void lueUnit2_EditValueChanged(object sender, EventArgs e)
    {
        _ServiceUnit2 = Convert.ToInt32(lueUnit2.EditValue);

        if (_ServiceUnit == _ServiceUnit2)
            panFactorUnit2.Visible = false;
        else
            panFactorUnit2.Visible = true;
    }

    private void txtUnitKNum_EditValueChanged(object sender, EventArgs e)
    {
        CalcTotalNum();
    }


    private void barBtnInvInputReport_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvInvInputPageFullName =
            @"select * 
                from vInvInputPageFullName
                where InvPageID = @InvPageID";

        tsqlvInvInputPageFullName = tsqlvInvInputPageFullName.Replace("@InvPageID", _InvPageID.ToString());


        var tsqlvInvInputPageItemFullName =
            @"select * 
                from vInvInputPageItemFullName
                where InvPageID = @InvPageID";

        tsqlvInvInputPageItemFullName = tsqlvInvInputPageItemFullName.Replace("@InvPageID", _InvPageID.ToString());


        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100015);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;

        ((StiSqlSource)report.Dictionary.DataSources["vInvInputPageFullName"]).SqlCommand =
            tsqlvInvInputPageFullName;
        ((StiSqlSource)report.Dictionary.DataSources["vInvInputPageItemFullName"]).SqlCommand =
            tsqlvInvInputPageItemFullName;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;

        report.Show();
    }

    private void txtDiscountAmount2_Leave(object sender, EventArgs e)
    {
        CalcTotalAmount();
    }

    private void txtOtherAmount_Leave(object sender, EventArgs e)
    {
        CalcTotalAmount();
    }

    private void txtTaxAmount2_Leave(object sender, EventArgs e)
    {
        CalcTotalAmount();
    }

    private void CheckReadOnly()
    {
        //_ReadOnly = false;
        //if (_EncStatus > 2)
        //{
        //_ReadOnly = true;
        btnServiceRegSave.Enabled = !_ReadOnly;
        //} 
    }
}