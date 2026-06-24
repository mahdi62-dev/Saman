using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using LIB;
using Saman.APP.SAC;
using Saman.BLL.DocReport;

namespace Saman.UC.DocReport;

public partial class UcDocReportExtract : XtraUserControl
{
    private readonly BLL.DocReport.DocReport _bllDocReport = new();

    private readonly DocReportExtract _bllDocReportExtract = new();

    private readonly SDate appSDate = new();


    private readonly SACPermission sacPermission = new();

    private DataTable _dtDocReportExtract;


    public UcDocReportExtract()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }

    private void CheckPermission()
    {
        // _CheckPermission = sacPermission.CheckPermission(_ModID, 3000081);
    }

    private void UcDocReportExtract_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        ActiveControl = panEncID;
        ActiveControl = txtEncID;

        CheckPermission();
        FillLookup();
        SetMask();
        datEndDate.DateTime = datStartDate.DateTime = DateTime.Parse(appSDate.ShamsiDate());
        timEndTime.Time = DateTime.Now;

        ShortKey();
    }

    private void ShortKey()
    {
        msiSearch.Click += btnSearch_Click;
        msiClose.Click += btnCancel_Click;

        panMain.Focus();
    }

    private void SetMask()
    {
        txtFirstName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtLastName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtSearchText.Properties.Mask.EditMask = App.UI.MaskEnglishWithNumChar;
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        var col = grvResult.Columns.ColumnByFieldName("RecNo");
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvResult.CustomUnboundColumnData += grvResult_CustomUnboundColumnData;

        _dtDocReportExtract = _bllDocReportExtract.DocReportExtractSearch(
            datStartDate.Text + " " + timStartTime.Text, datEndDate.Text + " " + timEndTime.Text,
            _DepID, txtSearchText.Text, chkIsCaseSensitive.Checked, lueSearchTitle.Text, txtEncID.Text,
            txtFirstName.Text, txtLastName.Text,
            txtNationalCode.Text, Convert.ToInt32(lueDr.EditValue), Convert.ToInt32(lueEncService.EditValue),
            Convert.ToInt32(lueChiefComplaint.EditValue),
            Convert.ToInt32(lueQuestion.EditValue));

        grdResult.DataSource = _dtDocReportExtract;
    }

    private void grvResult_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }

    private void grvPageAns_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        var view = sender as GridView;

        var AnsStatus = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "AnsStatus"));
        if (AnsStatus == 20) e.Appearance.BackColor = Color.LightGreen;
    }

    private void FillLookup()
    {
        UL.FillLookup(lueChiefComplaint, "0", cSet.cnStringA, false, "ID,Name", "vADR", "ChiefComplaint",
            "ADName='ChiefComplaint'", null);
        UL.FillLookup(lueDr, "0", cSet.cnStringA, false, "*", "vDr", "Dr", null, null);
        UL.FillLookup(lueEncService, "0", cSet.cnStringA, false, "ID,Name", "vADR", "EncService",
            "ADName='EncService'", null);
        UL.FillLookup(lueQuestion, "0", cSet.cnStringA, false, "ID,Name", "vADR", "PetQuestion",
            "ADName='PetQuestion'", null);
        UL.FillLookup(lueSearchTitle, "0", cSet.cnStringA, false, "ID,Name", "vADR", "DocTitleParagraph",
            "ADName='DocTitleParagraph'", null);
        UL.FillLookup(lueDocExportType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "DocExportType",
            "ADName='DocExportType'", null);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucEncCur = new UcEncCur
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucEncCur);
    }

    private void barBtnAttExport_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (grvResult.FocusedRowHandle < 0) return;

        var rowSelect = grvResult.GetDataRow(grvResult.FocusedRowHandle);
        //int selecreRo = grvResult.GetFocusedDataSourceRowIndex();


        int EncID, PageID;
        string FullName;

        var rows = grvResult.GetSelectedRows();
        foreach (var i in rows)
        {
            grvResult.GetDataRow(i);

            EncID = Convert.ToInt32(grvResult.GetDataRow(i)["EncID"]);
            PageID = Convert.ToInt32(grvResult.GetDataRow(i)["PageID"]);
            FullName = grvResult.GetDataRow(i)["FullName"].ToString();
        }

        //SUI sui = new SUI();
        //if (_EncID > 0)
        //{
        //    sui.ShowWarningMessageForm("پیام", "مراجعه کننده دارای پذیرش است", true);
        //    return;
        //}


        //this.fbdAttFile.SelectedPath = _FileName;
        //if (fbdAttFile.ShowDialog() == DialogResult.OK)

        //{

        //    Directory.CreateDirectory(fbdAttFile.SelectedPath = fbdAttFile.SelectedPath + _EncID + "-" + lblTitle.Text);

        //    for (int i = 0; i < _dtDocReportExtract.Rows.Count; i++)
        //    {
        //        _FileName = _dtDocReportExtract.Rows[i]["FileName"].ToString();
        //        _attFile = (byte[])_dtDocReportExtract.Rows[i]["AttFile"];

        //        _AttFilePath = fbdAttFile.SelectedPath + "\\" + _FileName;

        //        using (Stream file = File.OpenWrite(_AttFilePath))
        //        {
        //            file.Write(_attFile, 0, _attFile.Length);
        //            ucShowTextSave.ShowText("تصاویر ذخیره گردید", 5000);

        //        }
        //    }

        //}
    }

    private void lblExport_Click(object sender, EventArgs e)
    {
        if (grvResult.SelectedRowsCount < 0) return;

        var rowSelect = grvResult.GetDataRow(grvResult.SelectedRowsCount);

        if (fbdFile.ShowDialog() != DialogResult.OK) return;

        var ExportType = Convert.ToByte(lueDocExportType.EditValue);
        if (ExportType == 0) return;

        if (ExportType < 4) Export(ExportType);

        if (ExportType == 4)
        {
            var FileName = fbdFile.SelectedPath + @"\" + "Result.xlsx";
            grdResult.ExportToXlsx(FileName);
        }
    }

    private void Export(byte ExportType)
    {
        proExport.Visible = true;
        int EncID, PageID;
        string FullName, PageDate;
        string ExportFolder, FolderPath;

        ExportFolder = fbdFile.SelectedPath;

        var rows = grvResult.GetSelectedRows();
        proExport.Properties.Maximum = rows.Length;

        foreach (var i in rows)
        {
            grvResult.GetDataRow(i);

            EncID = Convert.ToInt32(grvResult.GetDataRow(i)["EncID"]);
            FullName = grvResult.GetDataRow(i)["FullName"].ToString().Trim();

            PageID = Convert.ToInt32(grvResult.GetDataRow(i)["PageID"]);
            PageDate = grvResult.GetDataRow(i)["Date"].ToString().Trim();

            FolderPath = ExportFolder + @"\" + EncID + "-" + FullName;
            Directory.CreateDirectory(FolderPath);
            fbdFile.SelectedPath = FolderPath;

            ExportFileType(ExportType, FolderPath, EncID, PageID, PageDate);

            proExport.PerformStep();
            proExport.Update();
        }

        proExport.Visible = false;
        ucShowTextSave.ShowText("ریپورت ذخیره گردید", 5000);
    }

    private void ExportFileType(byte ExportType, string FolderPath, int EncID, int PageID, string PageDate)
    {
        switch (ExportType)
        {
            case 1:
                ExportDoc(FolderPath, PageID, PageDate);
                break;

            case 2:
                ExportAtt(FolderPath, EncID);
                break;

            case 3:
                ExportDoc(FolderPath, PageID, PageDate);
                ExportAtt(FolderPath, EncID);
                break;
        }
    }

    private void ExportDoc(string FolderPath, int PageID, string PageDate)
    {
        var docStream = _bllDocReport.DocReportLoad(1, PageID);

        PageDate = PageDate.Replace("/", "");
        PageDate = PageDate.Replace(":", "");
        PageDate = PageDate.Replace(" ", "-");

        var FileName = "@Report" + "-" + PageDate + ".doc";
        FileName = FolderPath + @"\" + FileName;

        using (Stream file = File.OpenWrite(FileName))
        {
            file.Write(docStream, 0, docStream.Length);
        }
    }

    private void ExportAtt(string FolderPath, int EncID)
    {
        var _bllAttach = new BLL.Attach.Attach();
        var dtAttList = _bllAttach.AttFileListByEncID(EncID);

        if (dtAttList.Rows.Count == 0) return;

        string FileName;
        byte[] fileStream;

        foreach (DataRow row in dtAttList.Rows)
        {
            var EAID = Convert.ToInt64(row["EAID"]);
            var dtFile = _bllAttach.AttFileByEAID(EAID);

            FileName = FolderPath + @"\" + dtFile.Rows[0]["FileName"];

            fileStream = (byte[])dtFile.Rows[0]["AttFile"];
            using (Stream file = File.OpenWrite(FileName))
            {
                file.Write(fileStream, 0, fileStream.Length);
            }
        }
    }

    private void proExport_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
    {
        e.DisplayText = e.Value + " / " + proExport.Properties.Maximum;
    }
}