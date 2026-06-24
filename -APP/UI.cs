using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace App;

public class UI
{
    public static string MaskPersian = @"[ابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهیءئآ ]+";
    public static string MaskPersianWithNum = @"[ابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهیءئآ)(0123456789 ]+";

    public static string MaskEnglish = @"\p{L}+\s+\p{L}+\s+\p{L}+\s+\p{L}+";
    public static string MaskEnglishWithNum = @"[0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ]+";

    public static string MaskEnglishWithNumChar =
        @"[0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+-=.,';>< ]+";

    public static string MaskEmail = @"[0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@_.]+";


    public static string MaskNumWithDecimal = @"[/.0123456789]+";
    public static string MaskNum = @"[0123456789]+";
    public static string MaskNumNegative = @"[0123456789-]+";

    public static bool NationalCodeCValidate(string nationalCode, out string errorText)
    {
        var isValidNationalCode = true;

        ////در صورتی که کد ملی وارد شده تهی باشد
        //if (String.IsNullOrEmpty(nationalCode))
        //    isValidNationalCode = false;


        //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
        if (nationalCode.Length != 10) isValidNationalCode = false;

        //در صورتی که کد ملی ده رقم عددی نباشد
        var regex = new Regex(@"\d{10}");
        if (!regex.IsMatch(nationalCode)) isValidNationalCode = false;

        //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
        var allDigitEqual = new[]
        {
            "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666",
            "7777777777", "8888888888", "9999999999"
        };
        if (((IList)allDigitEqual).Contains(nationalCode)) isValidNationalCode = false;

        if (isValidNationalCode) //تا اینجا مشکلی نداشته باشد
        {
            //عملیات شرح داده شده در بالا
            var chArray = nationalCode.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            var a = Convert.ToInt32(chArray[9].ToString());

            var b = num0 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9;
            var c = b % 11;

            isValidNationalCode = (c < 2 && a == c) || (c >= 2 && 11 - c == a);
        }

        errorText = "کد ملی صحیح نیست";
        return isValidNationalCode;
    }
    //==============================================================================================

    public string SDateCheck(string date)
    {
        if (string.IsNullOrEmpty(date)) date = null;

        return date;
    }


    public static bool SDateValidate(string strInput, out string errorText)
    {
        var retVal = true;


        if (!DateTime.TryParse(strInput, CultureInfo.CreateSpecificCulture("fa-ir"), DateTimeStyles.None, out var dt))
            retVal = false;

        errorText = "تاریخ صحیح نیست";

        return retVal;
    }


    public static void SetLookUp
    (LookUpEdit myLookUp, DataSet ds, string tableName,
        string displayMember, string valueMember)
    {
        myLookUp.Properties.DataSource = ds.Tables[tableName];
        myLookUp.Properties.DisplayMember = displayMember;
        myLookUp.Properties.ValueMember = valueMember;
    }

    //==============================================================================================
    public static void LoadLookupData(DataSet ds, string tableName, LookUpEdit lue)
    {
        lue.Properties.DataSource = ds.Tables[tableName];
        //lue.Properties.DisplayMember = displayMember;
        //lue.Properties.ValueMember = valueMember;
    }

    //==============================================================================================
    public static void ShowLookup
    (DataSet ds, string tableName, TextEdit tb,
        LookUpEdit myLookUp, string code, string Name)
    {
        myLookUp.Properties.SearchMode = SearchMode.AutoComplete;
        for (var i = 0; i < ds.Tables[tableName].Rows.Count; i++)
            if (ds.Tables[tableName].Rows[i][code].ToString() == tb.Text)
                myLookUp.Text = ds.Tables[tableName].Rows[i][Name].ToString();

        myLookUp.Properties.SearchMode = SearchMode.AutoFilter;
    }

    //==============================================================================================
    public static void ShowLookupByCode
    (DataSet ds, string tableName, string iCode,
        LookUpEdit myLookUp, string code, string Name)
    {
        myLookUp.Properties.SearchMode = SearchMode.AutoComplete;
        for (var i = 0; i < ds.Tables[tableName].Rows.Count; i++)
            if (ds.Tables[tableName].Rows[i][code].ToString() == iCode)
                myLookUp.Text = ds.Tables[tableName].Rows[i][Name].ToString();

        myLookUp.Properties.SearchMode = SearchMode.AutoFilter;
    }
    //==============================================================================================

    public static void ShowFooterMessage(PictureBox picFooter, Label lblFooter, Bitmap icon,
        string message, Timer timFooter)
    {
        picFooter.Visible = true;
        lblFooter.Visible = true;
        picFooter.Image = icon;
        lblFooter.Text = message;

        timFooter.Enabled = true;
    }

    //===============================================================================================
    public static void ControlsBind(DataSet ds, string dsTableName, TextEdit tb,
        string field)
    {
        tb.DataBindings.Add(new Binding("Text", ds.Tables[dsTableName], field, true));
    }

    //==============================================================================================
    public static string C2Unicode(string str)
    {
        str = str.Replace('آ', 'ا');
        str = str.Replace('ئ', 'ی');
        str = str.Replace('ك', 'ک');
        str = str.Replace('ي', 'ی');
        str = str.Replace(" ", string.Empty);
        return str;
    }
    //==============================================================================================

    public static void LoadComboListData(DataSet ds, string tableName, ComboBoxEdit cbo)
    {
        cbo.Properties.Items.Clear();
        for (var i = 0; i < ds.Tables[tableName].Rows.Count; i++)
        {
            var name = ds.Tables[tableName].Rows[i]["Name"].ToString();
            cbo.Properties.Items.Add(name);
        }
    }

    //==============================================================================================

    public static string LoadCodeFromCombo(DataSet ds, string tableName, string text)
    {
        for (var i = 0; i < ds.Tables[tableName].Rows.Count; i++)
        {
            var Name = ds.Tables[tableName].Rows[i]["Name"].ToString();
            if (Name == text) return ds.Tables[tableName].Rows[i]["Code"].ToString();
        }

        return "";
    }

    //==============================================================================================

    public static string LoadFieldFromCombo(DataSet ds, string tableName, string searchFiled, string text,
        string returnField)
    {
        for (var i = 0; i < ds.Tables[tableName].Rows.Count; i++)
        {
            var Name = ds.Tables[tableName].Rows[i][searchFiled].ToString();
            if (Name == text) return ds.Tables[tableName].Rows[i][returnField].ToString();
        }

        return "";
    }


    public static void RaiseError(TextEdit tb, ErrorProvider ep, bool error)
    {
        if (string.IsNullOrEmpty(tb.Text))
        {
            error = true;
            //epPartList.BlinkRate = 300;
            ep.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            ep.SetError(tb, "لطفا جاهای خالی را پر نمایید");
        }

        else
        {
            error = false;
        }
    }


    //==============================================================================================
    public static void ShowComboByCode
    (DataSet ds, string tableName,
        ComboBoxEdit cbo, string code)
    {
        //         myLookUp.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
        for (var i = 0; i < ds.Tables[tableName].Rows.Count; i++)
            if (ds.Tables[tableName].Rows[i]["Code"].ToString() == code)
                cbo.Text = ds.Tables[tableName].Rows[i]["Name"].ToString();

        //myLookUp.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoFilter;
    }
    //==============================================================================================

    public static int GetComboCode
        (DataSet ds, string tableName, string text)
    {
        for (var i = 0; i < ds.Tables[tableName].Rows.Count; i++)
        {
            var Name = ds.Tables[tableName].Rows[i]["Name"].ToString();
            if (Name == text) return int.Parse(ds.Tables[tableName].Rows[i]["Code"].ToString());
        }

        return 0;
    }


    public static int GetComboCode
        (DataSet ds, string tableName, string searchField, string text, string returnField)
    {
        if (ds.Tables[tableName] != null)
            for (var i = 0; i < ds.Tables[tableName].Rows.Count; i++)
            {
                var Name = ds.Tables[tableName].Rows[i][searchField].ToString();
                if (Name == text) return int.Parse(ds.Tables[tableName].Rows[i][returnField].ToString());
            }

        return 0;
    }

    public static string GetComboStr
        (DataSet ds, string tableName, string searchField, string text, string returnField)
    {
        if (ds.Tables[tableName] != null)
            for (var i = 0; i < ds.Tables[tableName].Rows.Count; i++)
            {
                var Name = ds.Tables[tableName].Rows[i][searchField].ToString();
                if (Name == text) return ds.Tables[tableName].Rows[i][returnField].ToString();
            }

        return "";
    }
    //==============================================================================================
}