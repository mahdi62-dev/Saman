using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using LIB;
using Saman.APP;
using Saman.BLL.Attach;
using Saman.Form.Attach;
using Saman.Properties;

namespace Saman.UC.Attach;

public partial class UcAttach : XtraUserControl
{
    private readonly BLL.Attach.Attach _bllAttFile = new();
    private readonly ImageLib _ImageLib = new();
    private byte[] _attFile;
    private string _AttFilePath;
    private byte[] _CurAttFile;

    private string _CurFileName;

    private DataTable _dtFileList = new();


    public int _EAID;

    private int _EncStatus;
    public string _extension;
    public string _file;

    private string _FileName;
    public bool _ReadOnly;
    private DataRow[] _rowEncCur;


    private DataRow _rowImageSelect;

    public string _str;


    public UcAttach()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public byte _EncType { get; set; }
    public int _EncID { get; set; }
    public int _PageID { get; set; }

    public void UcAttach_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        UL.FillLookupRep(repLueTag, "0", cSet.cnStringA, false, "ID,Name", "vADR", "AttFileTag",
            "ADName='AttFileTag'", null);

        var bllAttachTableFileList = new DT();
        bllAttachTableFileList.CreateTableFileList(_dtFileList);

        _rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);
        if (_rowEncCur.Length > 0)
        {
            _EncID = Convert.ToInt32(_rowEncCur[0]["EncID"]);
            _EncStatus = Convert.ToInt32(_rowEncCur[0]["EncStatus"]);
            if (_EncStatus == 5)
            {
                _str = " کنسل گردیده است";
                lblTitle.ForeColor = Color.DarkRed;
            }

            if (_EncStatus == 4)
            {
                _str = " ترخیص نهایی گردیده است";
                lblTitle.ForeColor = Color.Lime;
            }

            lblTitle.Text = _rowEncCur[0]["FirstName"] + " " + _rowEncCur[0]["LastName"] + _str;
            txtEncID.Text = _rowEncCur[0]["EncId"].ToString();
            lblTitle2.Text = _rowEncCur[0]["Age"] + @" سال - فرزند " + _rowEncCur[0]["FatherName"];
            lblTitleEncDate.Text = "تاریخ پذیرش " + _rowEncCur[0]["StartDate"];
            picGender.Image = Convert.ToByte(_rowEncCur[0]["Gender"]) == 1 ? Resources.male : Resources.female;
            lblDrName.Text = _rowEncCur[0]["DrName"].ToString();
            lblDepName.Text = _rowEncCur[0]["DepName"].ToString();

            AttLoad(_EncID);
            grdFileList.DataSource = _dtFileList;
        }
    }


    public void lblNew_Click(object sender, EventArgs e)
    {
        var dr = ofdOpen.ShowDialog();

        if (dr == DialogResult.OK)
            foreach (var _file in ofdOpen.FileNames)
            {
                var fileInfo = new FileInfo(_file);
                var ext = Path.GetExtension(_file).ToUpper();
                _extension = fileInfo.Extension;

                //  if (_extension != ".jpg" || _extension != ".bmp" || _extension != ".png" ||_extension != ".tif") return;
                // var size = fileInfo.Length / 1024;
                // rowFile["AttFileType"] = _extension;
                var rowFile = _dtFileList.NewRow();

                rowFile["CRUD"] = 0;
                rowFile["FileName"] = fileInfo.Name;
                _attFile = _ImageLib.ComperessTifArray(_file, 1608, 2339);
                rowFile["AttFile"] = _attFile;
                rowFile["AttTag"] = 100;


                // rowFile["AttSelect"] = false;
                _dtFileList.Rows.Add(rowFile);
            }

        grdFileList.DataSource = _dtFileList;

        ActiveControl = grdFileList;
    }


    private void grvFileList_Click(object sender, EventArgs e)
    {
        if (grvFileList.FocusedRowHandle < 0) return;

        _rowImageSelect = grvFileList.GetDataRow(grvFileList.FocusedRowHandle);
        var _file = _rowImageSelect["FilePath"].ToString();
    }

    private void repBtnDel_Click(object sender, EventArgs e)
    {
        //SUI sui = new SUI();

        //var result = sui.FormShowMessage("حذف تصویر" , " تصویر حذف گردد؟", "بله", "خیر");

        //if (result == DialogResult.Yes)
        //{
        //    _rowImageSelect = grvFileList.GetDataRow(grvFileList.FocusedRowHandle);

        //    if (Convert.ToInt32(_rowImageSelect["EAID"]) == 0)

        //        _rowImageSelect.Delete();
        //    else
        //    {
        //        _EAID = Convert.ToInt32(_rowImageSelect["EAID"]);
        //        _bllAttFile.AttFile(3, _EncID, _PageID,_EAID,null, null, null, null, GL.UserPersID);
        //    }

        //    AttLoad(_EncID);
        //    grdFileList.DataSource = _dtFileList;

        //    int rowSelectgrv = grvFileList.GetFocusedDataSourceRowIndex();
        //    grvFileList.SelectRow(rowSelectgrv);
        //    grvFileList.FocusedRowHandle = rowSelectgrv;

        //    grvFileList_Click(null, null);
        //}
    }

    private void lblSave_Click(object sender, EventArgs e)
    {
        for (var i = 0; i < _dtFileList.Rows.Count; i++)
        {
            //int NO = Convert.ToInt32(grvFileList.GetDataRow(0)["attTag"].ToString());

            byte[] attFile = null;


            var File1 = _dtFileList.Rows[i]["FileName"].ToString();
            attFile = (byte[])_dtFileList.Rows[i]["AttFile"];

            var CRUD = Convert.ToInt32(_dtFileList.Rows[i]["CRUD"]);
            var attTag = Convert.ToInt32(_dtFileList.Rows[i]["attTag"]);


            if (CRUD != 1)
                _bllAttFile.AttFile(CRUD, _EncID, _PageID, _DepID, 0, File1, 1, attTag, attFile, GL.UserPersID);
            //  File.WriteAllBytes(@"D:\pic\1111.tif", attFile);
        }

        AttLoad(_EncID);
        grdFileList.DataSource = _dtFileList;
    }


    private void AttLoad(int EncID)
    {
        _dtFileList = _bllAttFile.AttFile(1, _EncID, 0, 0, 0, null, null, null, null, 0);

        if (!_dtFileList.Columns.Contains("FilePath"))
        {
            var newColumn = new DataColumn("FilePath");
            _dtFileList.Columns.Add(newColumn);
        }
    }

    private void grvFileList_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        byte[] attFile = null;

        var view = sender as CardView;
        if (e.Column.Name == "gcThumbnail")
        {
            var row = grvFileList.GetDataRow(e.ListSourceRowIndex);
            attFile = (byte[])row["AttFile"];
            e.Value = _ImageLib.ByteArrayToImage(attFile);
        }
    }


    private void repPic_MouseHover(object sender, EventArgs e)
    {
        grdFileList_Click(null, null);

        var frmAttDisplay = new FrmAttDisplay
        {
            _AttFile = _CurAttFile,
            _FileName = _CurFileName
        };

        frmAttDisplay.Show();
    }

    private void grdFileList_Click(object sender, EventArgs e)
    {
        if (grvFileList.FocusedRowHandle < 0) return;

        var row = grvFileList.GetDataRow(grvFileList.FocusedRowHandle);
        _CurAttFile = (byte[])row["AttFile"];
        _CurFileName = row["FileName"].ToString();

        var frmAttDisplay = new FrmAttDisplay
        {
            _AttFile = _CurAttFile,
            _FileName = _CurFileName
        };
        frmAttDisplay.Show();
    }

    private void barBtnDelAll_ItemClick(object sender, ItemClickEventArgs e)
    {
        var sui = new SUI();

        var result = sui.FormShowMessage("حذف تصاویر", " تمام تصاویر حذف گردد؟", "بله", "خیر");

        if (result == DialogResult.Yes)
        {
            _bllAttFile.AttFile(4, _EncID, 0, 0, 0, null, null, null, null, GL.UserPersID);
            _dtFileList.Clear();

            AttLoad(_EncID);
            grdFileList.DataSource = _dtFileList;

            var rowSelectgrv = grvFileList.GetFocusedDataSourceRowIndex();
            grvFileList.SelectRow(rowSelectgrv);
            grvFileList.FocusedRowHandle = rowSelectgrv;

            grvFileList_Click(null, null);
        }
    }

    private void barBtnDelSelect_ItemClick(object sender, ItemClickEventArgs e)
    {
        var AttSelect = 0;
        var SumSelect = 0;

        for (var i = 0; i < grvFileList.RowCount; i++)
        {
            AttSelect = Convert.ToInt32(grvFileList.GetRowCellValue(i, "AttSelect"));
            SumSelect += AttSelect;
        }

        if (SumSelect == 0)
        {
            var strMessage = "مدرکی انتخاب نشده است";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
        }
        else
        {
            var sui = new SUI();

            var result = sui.FormShowMessage("حذف تصویر", " تصویر حذف گردد؟", "بله", "خیر");
            if (result == DialogResult.Yes)
            {
                for (var i = 0; i < grvFileList.RowCount; i++)
                {
                    AttSelect = Convert.ToInt32(grvFileList.GetRowCellValue(i, "AttSelect"));

                    if (Convert.ToInt32(grvFileList.GetRowCellValue(i, "CRUD")) == 0 && AttSelect == 1)
                        grvFileList.DeleteRow(i);

                    if (Convert.ToInt32(grvFileList.GetRowCellValue(i, "CRUD")) > 0 && AttSelect == 1)
                    {
                        _EAID = Convert.ToInt32(grvFileList.GetRowCellValue(i, "EAID"));
                        _bllAttFile.AttFile(3, _EncID, _PageID, _DepID, _EAID, null, null, null, null, GL.UserPersID);
                    }
                }

                AttLoad(_EncID);
                grdFileList.DataSource = _dtFileList;

                var rowSelectgrv = grvFileList.GetFocusedDataSourceRowIndex();
                grvFileList.SelectRow(rowSelectgrv);
                grvFileList.FocusedRowHandle = rowSelectgrv;

                grvFileList_Click(null, null);
            }
        }
    }

    private void barBtnExportAll_ItemClick(object sender, ItemClickEventArgs e)
    {
        fbdAttFile.SelectedPath = _FileName;
        if (fbdAttFile.ShowDialog() == DialogResult.OK)

        {
            //if (!Directory.Exists(fbdAttFile.SelectedPath + "\\" + _EncID))
            //{
            //    Directory.CreateDirectory(fbdAttFile.SelectedPath = fbdAttFile.SelectedPath + "\\" + _EncID);
            //};

            Directory.CreateDirectory(fbdAttFile.SelectedPath =
                fbdAttFile.SelectedPath + _EncID + "-" + lblTitle.Text);

            for (var i = 0; i < _dtFileList.Rows.Count; i++)
            {
                _FileName = _dtFileList.Rows[i]["FileName"].ToString();
                _attFile = (byte[])_dtFileList.Rows[i]["AttFile"];

                _AttFilePath = fbdAttFile.SelectedPath + "\\" + _FileName;

                using (Stream file = File.OpenWrite(_AttFilePath))
                {
                    file.Write(_attFile, 0, _attFile.Length);
                    ucShowTextSave.ShowText("تصاویر ذخیره گردید", 5000);
                }
            }
        }
    }

    private void barBtnExportSelect_ItemClick(object sender, ItemClickEventArgs e)
    {
        var AttSelect = 0;
        var SumSelect = 0;

        for (var i = 0; i < grvFileList.RowCount; i++)
        {
            AttSelect = Convert.ToInt32(grvFileList.GetRowCellValue(i, "AttSelect"));
            SumSelect += AttSelect;
        }

        if (SumSelect == 0)
        {
            var strMessage = "مدرکی انتخاب نشده است";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
        }
        else
        {
            fbdAttFile.SelectedPath = _FileName;
            if (fbdAttFile.ShowDialog() == DialogResult.OK)
            {
                //if (!Directory.Exists(fbdAttFile.SelectedPath  + _EncID))
                //{
                //    Directory.CreateDirectory(fbdAttFile.SelectedPath = fbdAttFile.SelectedPath  + _EncID);
                //};
                Directory.CreateDirectory(fbdAttFile.SelectedPath =
                    fbdAttFile.SelectedPath + _EncID + "-" + lblTitle.Text);
                {
                    for (var i = 0; i < grvFileList.RowCount; i++)
                    {
                        AttSelect = Convert.ToInt32(grvFileList.GetRowCellValue(i, "AttSelect"));
                        if (Convert.ToInt32(grvFileList.GetRowCellValue(i, "CRUD")) > 0 && AttSelect == 1)
                        {
                            _FileName = grvFileList.GetRowCellValue(i, "FileName").ToString();
                            _attFile = (byte[])grvFileList.GetRowCellValue(i, "AttFile");

                            _AttFilePath = fbdAttFile.SelectedPath + "\\" + _FileName;

                            using (Stream file = File.OpenWrite(_AttFilePath))
                            {
                                file.Write(_attFile, 0, _attFile.Length);
                                ucShowTextSave.ShowText("تصاویر ذخیره گردید", 5000);
                            }
                        }
                    }
                }
            }
        }

        AttLoad(_EncID);
        grdFileList.DataSource = _dtFileList;

        var rowSelectgrv = grvFileList.GetFocusedDataSourceRowIndex();
        grvFileList.SelectRow(rowSelectgrv);
        grvFileList.FocusedRowHandle = rowSelectgrv;

        grvFileList_Click(null, null);
    }
}