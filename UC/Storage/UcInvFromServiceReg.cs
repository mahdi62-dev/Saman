using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DAL;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using LIB;
using Saman.BLL.Storage;
using Saman.UC.Storage;

namespace Saman.UC;

public partial class UcInvFromServiceReg : XtraUserControl
{
    private readonly InvFromServiceReg _bllInvFromServiceReg = new();
    private readonly InvOutput _bllInvOutput = new();
    private readonly BLL.Storage.Storage _bllStorage = new();
    private readonly long _BuyFeeAmount2 = 0;

    private readonly int _ServiceUnit, _ServiceUnit2;
    private long _BuyAmount, _BuyAmount2;


    private long _BuyAmountLast, _SellAmountLast, _ResultAmount;
    private long _BuyFeeAmount;

    private DataTable _dtInvPage = new();

    private int _InvPageID, _InvPageCode;
    private DataTable _ItemData = new();
    private double _Num, _Num2;

    private long _SellFeeAmount, _SellFeeAmount2, _SellAmount, _SellAmount2;
    private double _SellPer;

    private DataTable _ServiceRegDataList;
    private double _StockNum, _StockNumAll;


    private int _UnitKNum;


    public UcInvFromServiceReg()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }
    public int _DepID { get; set; }
    public int _StorageID { get; set; }
    public int _StoragePeriodID { get; set; }

    public bool _ReadOnly { get; set; }

    private void UcInvFromServiceReg_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        RepTxtConfirmNum.Mask.EditMask = App.UI.MaskNumWithDecimal;

        ActiveControl = datStartServiceReg;
        UL.LangChange(2);
        UL.FillLookup(lueDep, "1", cSet.cnStringA, true, "DepID,Name", "vDep", "Dep", null, null);
    }

    private void lueDep_EditValueChanged(object sender, EventArgs e)
    {
        _DepID = Convert.ToInt32(lueDep.EditValue);
        FormNull();
    }


    private void FormNull()
    {
        grdServiceConsume.DataSource = null;

        ActiveControl = lueDep;
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        //App.Control.TextEditLine(lueDayOffType, e);
        //App.Control.TextEditLine(luePers, e);
        //App.Control.TextEditLine(datDayOffEnd, e);
        //App.Control.TextEditLine(datDayOffStart, e);
    }

    private void btnRefreshInvFromServiceReg_Click(object sender, EventArgs e)
    {
        if (!CheckValidationServiceAdd()) return;

        ServiceRegSearch();
    }

    private bool CheckValidationServiceAdd()
    {
        var isValid = true;

        if (string.IsNullOrEmpty(datStartServiceReg.Text))
        {
            datStartServiceReg.Text = "لطفا تاریخ را وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(datEndServiceReg.Text))
        {
            datEndServiceReg.Text = "لطفا تاریخ را وارد نمایید";
            isValid = false;
        }

        if (lueDep.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueDep.Text))
        {
            lueDep.ErrorText = "لطفا بخش را انتخاب نمایید";
            isValid = false;
        }


        return isValid;
    }

    private void ServiceRegSearch()
    {
        var col = grvServiceConsume.Columns.ColumnByFieldName("RecNo");
        col.UnboundType = UnboundColumnType.Integer;
        grvServiceConsume.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
        }

        _ServiceRegDataList = _bllInvFromServiceReg.InvServiceDrugConsumeLoad
            (_StorageID, _StoragePeriodID, _DepID, datStartServiceReg.Text, datEndServiceReg.Text);
        grdServiceConsume.DataSource = _ServiceRegDataList;
    }

    private void btnConvertToInvOutput_Click(object sender, EventArgs e)
    {
        var HasStockNum = InvPageItemCheckStockNum();
        if (!HasStockNum) return;

        _dtInvPage = _bllInvOutput.InvPageSave
        (0, 2, _StorageID, _StoragePeriodID, null, null, 0,
            0, 0, 0, _DepID, null, GL.UserPersID, 1
        );


        _InvPageID = Convert.ToInt32(_dtInvPage.Rows[0]["InvPageID"]);
        _InvPageCode = Convert.ToInt32(_dtInvPage.Rows[0]["InvPageCode"]);

        InvPageItemSave();

        _bllInvOutput.InvPageAmountSave(_InvPageID, GL.UserPersID);

        _bllInvFromServiceReg.InvOutputApplyStatusSave(_DepID, datStartServiceReg.Text, datEndServiceReg.Text,
            _InvPageID, 1);
        var panUc = Parent as PanelControl;
        panUc.Controls.Clear();

        var ucInvOutput = new UcInvOutput
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Dock = DockStyle.Fill,
            _ModID = _ModID,
            _DepID = _DepID,

            _StorageID = _StorageID,
            _StoragePeriodID = _StoragePeriodID
        };

        panUc.Controls.Add(ucInvOutput);
    }

    private bool InvPageItemCheckStockNum()
    {
        double ConfirmNum = 0;
        double StockNum = 0;


        var HasStockNum = true;

        for (var i = 0; i < grvServiceConsume.RowCount; i++)
        {
            ConfirmNum = Convert.ToDouble(grvServiceConsume.GetRowCellValue(i, "ConfirmNum"));
            StockNum = Convert.ToDouble(grvServiceConsume.GetRowCellValue(i, "StockNum"));

            if (ConfirmNum > StockNum)
            {
                HasStockNum = false;
                i = grvServiceConsume.RowCount;
                MessageBox.Show("مقادیر تاییدی بیش از موجودی است!!!");
            }
        }

        return HasStockNum;
    }


    private void CalcTotalNum()
    {
        _Num = _Num2 * _UnitKNum;
    }

    private void CalcSellAmount()
    {
        _BuyAmount = (long)(_BuyFeeAmount * _Num);
        _BuyAmount2 = (long)(_BuyFeeAmount2 * _Num2);

        _SellAmount = (long)(_SellFeeAmount * _Num);
        _SellAmount2 = (long)(_SellFeeAmount2 * _Num2);

        _ResultAmount = _SellAmount;

        if (_BuyFeeAmount2 > 0)
        {
            _SellPer = _SellFeeAmount2 / Convert.ToDouble(_BuyFeeAmount2);
            _SellPer = Math.Round(_SellPer, 2);
        }
    }

    private void InvPageItemSave()
    {
        var ServiceID = 0;
        double ConfirmNum = 0;
        var Unit = 0;


        for (var i = 0; i < grvServiceConsume.RowCount; i++)
        {
            ServiceID = Convert.ToInt32(grvServiceConsume.GetRowCellValue(i, "ServiceID"));
            ConfirmNum = Convert.ToDouble(grvServiceConsume.GetRowCellValue(i, "ConfirmNum"));
            Unit = Convert.ToInt32(grvServiceConsume.GetRowCellValue(i, "ServiceUnit"));

            _Num2 = ConfirmNum;
            _UnitKNum = 1;
            CalcTotalNum();

            _ItemData = _bllStorage.GetItemData(0, _StoragePeriodID,
                grvServiceConsume.GetRowCellValue(i, "ServiceCategory")
                , grvServiceConsume.GetRowCellValue(i, "ServiceCode"));

            if (_ItemData.Rows.Count > 0) _StockNumAll = Convert.ToDouble(_ItemData.Rows[0]["StockNum"]);

            _ItemData = _bllStorage.GetItemData(_StorageID, _StoragePeriodID,
                grvServiceConsume.GetRowCellValue(i, "ServiceCategory")
                , grvServiceConsume.GetRowCellValue(i, "ServiceCode"));

            if (_ItemData.Rows.Count > 0)
            {
                _BuyFeeAmount = _BuyAmountLast = Convert.ToInt64(_ItemData.Rows[0]["BuyAmountLast"]);
                _SellAmountLast = _SellFeeAmount =
                    _SellFeeAmount2 = Convert.ToInt64(_ItemData.Rows[0]["SellAmountLast"]);

                _StockNum = Convert.ToDouble(_ItemData.Rows[0]["StockNum"]);
                CalcSellAmount();

                //CheckStockNumForInvOutput();
            }


            _bllInvOutput.InvPageItemSave
            (0, _InvPageID, 0, 2, _StorageID, _StoragePeriodID, ServiceID,
                ConfirmNum, Unit, 1, Unit, ConfirmNum,
                _StockNum, _StockNumAll, false, _BuyFeeAmount2, _BuyAmount2,
                _SellPer, _SellFeeAmount, _SellFeeAmount2, _BuyFeeAmount, _BuyAmount,
                0, 0, 0, _SellAmount, _SellAmount2, _SellAmountLast, _ResultAmount
            );
        }
    }


    private void lblPrint_Click(object sender, EventArgs e)
    {
        grvServiceConsume.BestFitColumns();
        grdServiceConsume.ShowRibbonPrintPreview();
    }

    private void grvServiceConsume_PrintInitialize(object sender, PrintInitializeEventArgs e)
    {
        var pb = e.PrintingSystem as PrintingSystemBase;
        var printableComponentLink = new PrintableComponentLink();
        pb.PageSettings.Landscape = false;
        pb.PageSettings.PaperKind = PaperKind.A4;
        pb.Document.AutoFitToPagesWidth = 1;
        pb.PageSettings.TopMargin = 100;
        pb.PageSettings.BottomMargin = 100;
        pb.PageSettings.LeftMargin = 40;
        pb.PageSettings.RightMargin = 40;

        grvServiceConsume.OptionsPrint.PrintHeader = true;
        grvServiceConsume.OptionsPrint.PrintFooter = true;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.LineAlignment = BrickAlignment.Center;
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.LineAlignment = BrickAlignment.Center;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Font = new Font("IRANSansFaNum", 10, FontStyle.Regular);
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Font = new Font("IRANSansFaNum", 8);

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.AddRange(
            new[] { grdServiceConsume.Tag.ToString() });
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.AddRange(new[]
        {
            "[Date Printed]-[Time Printed] : تاریخ گزارش گیری  ", " [Pages #] از [Page #] : صفحه  ",
            "گزارش گیرنده : " + GL.UserPersName
        });
    }
}