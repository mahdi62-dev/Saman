using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace UI;

internal class UF
{
    public static bool isFormOpen(Form frm)
    {
        if (Application.OpenForms[frm.Name] != null) return true;

        return false;
    }

    public static string C2Uni(string str)
    {
        str = str.Replace('آ', 'ا');
        str = str.Replace('ئ', 'ی');
        str = str.Replace('ك', 'ک');
        str = str.Replace('ي', 'ی');
        str = str.Replace('ﻲ', 'ﯽ');
        str = str.Replace(" ", string.Empty);
        return str;
    }

    public static string C2Unicode(string str)
    {
        str = str.Replace('آ', 'ا');
        str = str.Replace('ئ', 'ی');
        str = str.Replace('ك', 'ک');
        str = str.Replace('ي', 'ی');
        str = str.Replace('ﻲ', 'ﯽ');
        return str;
    }

    public static string C2Unicode(TextEdit tb)
    {
        var str = tb.Text;
        str = str.Replace('آ', 'ا');
        str = str.Replace('ئ', 'ی');
        str = str.Replace('ك', 'ک');
        str = str.Replace('ي', 'ی');
        return str;
    }

    public static string C2Uni(TextEdit tb)
    {
        var str = tb.Text;
        str = str.Replace('آ', 'ا');
        str = str.Replace('ئ', 'ی');
        str = str.Replace('ك', 'ک');
        str = str.Replace('ي', 'ی');
        str = str.Replace(" ", string.Empty);
        return str;
    }

    public static void ShowForm(Form frm)
    {
        if (!isFormOpen(frm))
        {
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }
    }

    public static void ShowFormTop(Form frm)
    {
        if (!isFormOpen(frm))
        {
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.TopMost = true;
            frm.Show();
        }
    }

    public static void ShowDialogForm(Form frm)
    {
        if (!isFormOpen(frm))
        {
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }
    }

    public static void ShowDefForm(string tableName, string caption)
    {
        ////App.List.LoadList(GL.ds, tableName, "DefList");
        ////var objFrmDefine = new UI.Define.frmDefine();
        ////var frmParent = new frmMain();

        ////if (!isFormOpen(objFrmDefine))
        ////{
        ////    objFrmDefine._caption = caption;
        ////    objFrmDefine._tableName = tableName;
        ////    objFrmDefine.StartPosition = FormStartPosition.CenterScreen;
        ////    objFrmDefine.TopMost = true;
        ////    objFrmDefine.Show();

        ////}
    }

    public static void CboAddItem(ComboBoxEdit cbo, string str)
    {
        cbo.Properties.Items.Add(str);
    }

    public static bool IsEmptyTb(TextEdit tb)
    {
        if (string.IsNullOrEmpty(tb.Text)) return true;

        return false;
    }

    public static void DelFile(string filePath)
    {
        File.Delete(filePath);
    }

    public static string C2ShortSDate(string longDate)
    {
        return longDate.Substring(2, 8);
    }

    public static string C2LongSDateTime(string shortDate)
    {
        return "13" + shortDate + " 00:00";
    }

    public static byte[] ConvertImageToBytes(Image image)
    {
        if (image != null)
        {
            var ms = new MemoryStream();
            using (ms)
            {
                image.Save(ms, image.RawFormat);
                var bytes = ms.ToArray();

                return bytes;
            }
        }

        return null;
    }

    public static byte[] ImageToByteArray(Image imageIn)
    {
        var ms = new MemoryStream();
        imageIn.Save(ms, ImageFormat.Jpeg);
        return ms.ToArray();
    }
}