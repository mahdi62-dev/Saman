using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Services;
using DevExpress.XtraSpellChecker;
using LIB;
using Saman.APP;
using Saman.APP.SAC;
using Saman.BLL.DocReport;

namespace Saman.Form.DocReport;

public partial class FrmRichReport : XtraForm
{
    private readonly BLL.DocReport.DocReport _bllDocReport = new();


    private readonly DataTable _dtDep = new();

    private readonly string _FormName = "PET Scan";


    private readonly bool _IsQuit = false;

    private readonly RicHelper _ricHelper = new();
    private readonly SACPermission sacPermission = new();

    public string _Age,
        _GenderEn = "GenderEn",
        _PageDate,
        _ChiefComplaint,
        _QuestionName,
        _ChiefComplaintName,
        _RefDrName,
        _Height,
        _Weight;

    public int _DepID = 30;

    public string _DrReportSignTextFa1,
        _DrReportSignTextEn1,
        _DrReportSignTextFa2,
        _DrReportSignTextEn2,
        _PersSignTextEn,
        _PersSignTextFa,
        _PageDateEn,
        _GenderFa,
        _curDateTime;

    public int _EncID, _PageID, _Gender;

    public string _FullName;

    public int _ModID = 30;

    public int _StorageID = 0, _StoragePeriodID = 0;
    public int _TemplateID, _IsSign;


    public FrmRichReport()
    {
        InitializeComponent();

        SetAutoCorrectOptions();

        #region #autocorrectservice

        var svc = ricMain.GetService<IAutoCorrectService>();
        if (svc != null) svc.SetReplaceTable(_bllDocReport.AutoCorrectLoad(_ModID));

        #endregion #autocorrectservice
    }

    public bool _CheckPermission { get; set; }


    private void spcMain_WordAdded(object sender, WordAddedEventArgs e)
    {
        SaveFileDicToDB();
        LoadFileDicFromDB();

        // Set spell checking options.
        spcMain.SpellCheckMode = SpellCheckMode.AsYouType;
        spcMain.ParentContainer = this;
        spcMain.CheckAsYouTypeOptions.CheckControlsInParentContainer = true;
    }


    private void FrmRichReport_Load(object sender, EventArgs e)
    {
        LoadFileFromDB();
        ricMain.Select();
        LoadFileDicFromDB();
        CheckPermission();

        TextMdiTab = _FullName;

        ////Set spell checking options.
        spcMain.SpellCheckMode = SpellCheckMode.AsYouType;
        spcMain.ParentContainer = this;
        spcMain.CheckAsYouTypeOptions.CheckControlsInParentContainer = true;

        if (_CheckPermission) ricMain.ReadOnly = true;

        if (_IsSign == 1)
        {
            ricMain.ReadOnly = true;
            barBtnSign.Enabled = false;
            barBtnSave.Enabled = false;
            barBtnSaveClose.Enabled = false;
            barBtnUndoSign.Enabled = true;
        }

        //ricMain.prop.SpellCheckerOptions.AutoDetectDocumentCulture = false;
        //ricMain.SetAutoURLDetect(TRUE);
        //ricMain.SpellChecker.Culture = System.Globalization.CultureInfo.InvariantCulture;
    }

    private void CheckPermission()
    {
        _CheckPermission = sacPermission.CheckPermission(_ModID, 3000082);
    }

    private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
    {
        var sui = new SUI();
        var result = sui.FormShowMessage("حذف جواب", "جواب << " + _FullName + " >> حذف گردد؟", "بله", "خیر");

        if (result == DialogResult.Yes)
        {
            _bllDocReport.DocReportSave(4, _PageID, null, null, null, null, null, null,
                GL.UserPersID);
            Close();
        }
    }

    private void barBtnSave_ItemClick(object sender, ItemClickEventArgs e)
    {
        var streamPDF = new MemoryStream();
        var options = new PdfExportOptions();
        options.DocumentOptions.Author = "SamanNovin.com";
        options.Compressed = true;
        options.ImageQuality = PdfJpegImageQuality.Medium;

        streamPDF.Position = 0;
        ricMain.ExportToPdf(streamPDF);


        streamPDF.CopyTo(streamPDF);
        var PDFBytes = streamPDF.ToArray();


        //using (FileStream file = new FileStream("d:\\file.pdf", FileMode.Create, FileAccess.Write)) {
        //    streamPDF.WriteTo(file);
        //}

        if (_TemplateID > 0)
        {
            _bllDocReport.DocReportSave(0, _PageID, ricMain.DocBytes, ricMain.RtfText, ricMain.Text,
                ricMain.HtmlText, PDFBytes, null, GL.UserPersID);
            _TemplateID = 0;
        }


        else
        {
            _bllDocReport.DocReportSave(2, _PageID, ricMain.DocBytes, ricMain.RtfText, ricMain.Text,
                ricMain.HtmlText, PDFBytes, null, GL.UserPersID);
        }
    }


    private void barBtnSaveClose_ItemClick(object sender, ItemClickEventArgs e)
    {
        var streamPDF = new MemoryStream();
        var options = new PdfExportOptions();
        options.DocumentOptions.Author = "SamanNovin.com";
        options.Compressed = true;
        options.ImageQuality = PdfJpegImageQuality.Medium;

        streamPDF.Position = 0;
        ricMain.ExportToPdf(streamPDF);


        streamPDF.CopyTo(streamPDF);
        var PDFBytes = streamPDF.ToArray();

        if (_TemplateID > 0)
        {
            _bllDocReport.DocReportSave(0, _PageID, ricMain.DocBytes, ricMain.RtfText, ricMain.Text,
                ricMain.HtmlText, PDFBytes, null,
                GL.UserPersID);
            _TemplateID = 0;
        }


        else
        {
            _bllDocReport.DocReportSave(2, _PageID, ricMain.DocBytes, ricMain.RtfText, ricMain.Text,
                ricMain.HtmlText, PDFBytes, null,
                GL.UserPersID);
        }

        Close();
    }

    private void barBtnSign_ItemClick(object sender, ItemClickEventArgs e)
    {
        var _crypt = new Crypt();

        var plainText = _PageID + "," + _EncID;
        var urlAns = _crypt.EncryptString(GL.key, plainText);

        urlAns = WebUtility.UrlEncode(urlAns);


        _bllDocReport.DocReportSave(5, _PageID, null, null, null, null, null, urlAns, GL.UserPersID);
        ricMain.ReadOnly = true;
        barBtnSign.Enabled = false;
        barBtnSave.Enabled = false;
        barBtnSaveClose.Enabled = false;
        barBtnUndoSign.Enabled = true;
    }

    private void barBtnUndoSign_ItemClick(object sender, ItemClickEventArgs e)
    {
        _bllDocReport.DocReportSave(6, _PageID, null, null, null, null, null, null, GL.UserPersID);
        ricMain.ReadOnly = false;
        barBtnSign.Enabled = true;
        barBtnSave.Enabled = true;
        barBtnSaveClose.Enabled = true;
        barBtnUndoSign.Enabled = false;
    }

    private void SetAutoCorrectOptions()
    {
        #region #options

        var correctionOptions = ricMain.Options.AutoCorrect;
        correctionOptions.CorrectTwoInitialCapitals = true;
        correctionOptions.DetectUrls = false;
        correctionOptions.ReplaceTextAsYouType = true;
        correctionOptions.UseSpellCheckerSuggestions = true;

        #endregion #options
    }


    #region #setreplacetable

    private AutoCorrectReplaceInfoCollection LoadAbbrevs(string path)
    {
        var coll = new AutoCorrectReplaceInfoCollection();
        var aLine = "";

        if (File.Exists(path))
        {
            var sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                aLine = sr.ReadLine();
                if (aLine != "START") continue;

                while (!sr.EndOfStream)
                {
                    aLine = sr.ReadLine();
                    aLine = aLine.Trim();
                    var words = aLine.Split('=');
                    if (words.Length == 2)
                    {
                        var acrInfo = new AutoCorrectReplaceInfo(words[0], words[1]);
                        coll.Add(acrInfo);
                    }
                }
            }

            sr.Close();
        }

        return coll;
    }

    #endregion #setreplacetable

    private void LoadFileFromDB()
    {
        byte[] _docByte = { };
        var bllDocReportDef = new DocReportDef();

        if (_TemplateID > 0) _docByte = bllDocReportDef.DocReportFileLoad(_TemplateID);

        if (_TemplateID == 0) _docByte = _bllDocReport.DocReportLoad(1, _PageID);

        ricMain.LoadDocument(_docByte);

        // ricMain.ReadOnly = true;
        SetValue();
    }

    private void SetValue()
    {
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#EncID#", _EncID.ToString());
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#PageID#", _PageID.ToString());
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#FullName#", _FullName);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#GenderFa#", _GenderFa);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#GenderEn#", _GenderEn);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#Age#", _Age);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#PageDate#", _PageDate);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#PageDateEn#", _PageDateEn);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#ChiefComplaintName#", _ChiefComplaintName);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#QuestionName#", _QuestionName);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#RefDrName#", _RefDrName);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#DrReportSignTextFa1#", _DrReportSignTextFa1);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#DrReportSignTextEn1#", _DrReportSignTextEn1);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#DrReportSignTextFa2#", _DrReportSignTextFa2);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#DrReportSignTextEn2#", _DrReportSignTextEn2);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#PersSignTextFa#", _PersSignTextFa);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#PersSignTextEn#", _PersSignTextEn);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#PersTypeDate#", _curDateTime);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#Height#", _Height);
        _ricHelper.ReplaceTextInDocument(ricMain.Document, "#Weight#", _Weight);
    }


    private void LoadFileDicFromDB()
    {
        byte[] dicByte1 = { };
        dicByte1 = _bllDocReport.DicFileLoad(1);
        Stream stream1 = new MemoryStream(dicByte1);
        var dictionary1 = new SpellCheckerCustomDictionary
        {
            Culture = new CultureInfo("en-US")
        };
        dictionary1.LoadFromStream(stream1);
        spcMain.Dictionaries.Add(dictionary1);

        byte[] dicByte2 = { };
        dicByte2 = _bllDocReport.DicFileLoad(2);
        Stream stream2 = new MemoryStream(dicByte2);
        var dictionary2 = new SpellCheckerCustomDictionary
        {
            Culture = new CultureInfo("fa-IR"),
            Encoding = new UTF8Encoding(true)
        };
        dictionary2.LoadFromStream(stream2);
        spcMain.Dictionaries.Add(dictionary2);

        byte[] dicByte3 = { };
        dicByte3 = _bllDocReport.DicFileLoad(3);
        Stream stream3 = new MemoryStream(dicByte3);
        var dictionary3 = new SpellCheckerCustomDictionary
        {
            Culture = new CultureInfo("en-US")
        };
        dictionary3.LoadFromStream(stream3);
        spcMain.Dictionaries.Add(dictionary3);
    }

    private void SaveFileDicToDB()
    {
        var dicSave = new SpellCheckerCustomDictionary();
        dicSave = (SpellCheckerCustomDictionary)spcMain.Dictionaries[0];
        var dicID = 1;


        var fileName = Path.GetTempPath();
        fileName = fileName + dicID + ".dic";
        dicSave.SaveAs(fileName);

        byte[] fileDic;
        using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new BinaryReader(stream))
            {
                fileDic = reader.ReadBytes((int)stream.Length);
            }
        }

        var bllDocReportDef = new DocReportDef();
        bllDocReportDef.DicFileSave(dicID, fileDic, GL.UserPersID);

        //Delete temp file for dic
        if (File.Exists(fileName)) File.Delete(fileName);


        //dicSave = (SpellCheckerCustomDictionary) spcMain.Dictionaries[1];
        //dicID = 2;

        //fileName = Path.GetTempPath();
        //fileName = fileName + dicID.ToString() + ".dic";
        //dicSave.SaveAs(fileName);

        //using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        //{
        //    using (var reader = new BinaryReader(stream))
        //    {
        //        fileDic = reader.ReadBytes((int) stream.Length);
        //    }
        //}

        //bllDocReportDef.DicFileSave(dicID, fileDic, GL.UserPersID);

        ////Delete temp file for dic
        //if (File.Exists(fileName)) File.Delete(fileName);


        //dicSave = (SpellCheckerCustomDictionary) spcMain.Dictionaries[2];
        //dicID = 3;

        //fileName = Path.GetTempPath();
        //fileName = fileName + dicID.ToString() + ".dic";
        //dicSave.SaveAs(fileName);

        //using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        //{
        //    using (var reader = new BinaryReader(stream))
        //    {
        //        fileDic = reader.ReadBytes((int) stream.Length);
        //    }
        //}

        //bllDocReportDef.DicFileSave(dicID, fileDic, GL.UserPersID);

        ////Delete temp file for dic
        //if (File.Exists(fileName)) File.Delete(fileName);
    }

    private void barBtnAutoCorrect_ItemClick(object sender, ItemClickEventArgs e)
    {
        var frm = new FrmAutoCorrect { _ModID = _ModID };
        var result = frm.ShowDialog();


        var svc = ricMain.GetService<IAutoCorrectService>();
        if (svc != null) svc.SetReplaceTable(_bllDocReport.AutoCorrectLoad(_ModID));
    }


    private void barBtnFormatPainter_ItemClick(object sender, ItemClickEventArgs e)
    {
    }

    private void spcMain_AfterLoadDictionaries(object sender, EventArgs e)
    {
    }

    private void ricMain_AutoCorrect(object sender, AutoCorrectEventArgs e)
    {
        //AutoCorrectInfo info = e.AutoCorrectInfo;
        //e.AutoCorrectInfo = null;

        //if (info.Text.Length <= 0)
        //    return;

        ////Find a dollar and the percent sign in the obtained text
        //for (; ; )
        //{
        //    if (!info.DecrementStartPosition())
        //        return;

        //    if (info.Text[0] == '%')
        //    {
        //        //Replace a text between dollar signs with a custom content
        //        string replaceString = "CalculateFunction(info.Text)";
        //        if (!String.IsNullOrEmpty(replaceString))
        //        {
        //            info.ReplaceWith = replaceString;
        //            e.AutoCorrectInfo = info;
        //        }
        //        return;
        //    }
        //}
    }

    private void ricMain_Click(object sender, EventArgs e)
    {
        CaretPositionLanguageCheck();
    }

    private void ricMain_SelectionChanged(object sender, EventArgs e)
    {
        CaretPositionLanguageCheck();
    }

    private void CaretPositionLanguageCheck()
    {
        var doc = ricMain.Document;
        var intPos = 0;
        intPos = doc.CaretPosition.ToInt();

        if (intPos > 1) intPos -= 1;

        var pos1 = doc.CreatePosition(intPos);

        var r1 = doc.CreateRange(pos1, 1);
        var str = doc.GetText(r1);

        if (str.Length == 0) return;

        var c = str[0];
        int ascii = c;

        if (ascii >= 1570 && ascii <= 1641)
            UL.LangChange(2);
        else
            UL.LangChange(1);
    }
}