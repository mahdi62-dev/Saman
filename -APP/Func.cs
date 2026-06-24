using System.Windows.Forms;

namespace App;

public class Func
{
    //==============================================================================================
    public static void SetFarsiKeyboard()
    {
        foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            if (lang.LayoutName.ToLower() == "farsi")
                //keyLang = lang;      
                InputLanguage.CurrentInputLanguage = lang;
    }

    //==============================================================================================
    public static void SetEngKeyboard()
    {
        foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            if (lang.LayoutName != null && lang.LayoutName.ToLower() == "us")
                //keyLang = lang;      
                InputLanguage.CurrentInputLanguage = lang;
    }

    //==============================================================================================
    public static string AgeCal(string birthDate)
    {
        var birthYear = int.Parse(birthDate.Substring(2, 2));
        var appSDate = new SDate();
        var curYear = int.Parse(appSDate.Today().Substring(2, 2));
        var age = curYear - birthYear;
        return age.ToString();
    }

    //==============================================================================================
    public static string BirthDateCal(string strAge)
    {
        var intAge = int.Parse(strAge);
        var appSDate = new SDate();
        var curYear = int.Parse(appSDate.Today().Substring(2, 2));
        var birthYear = curYear - intAge;

        var birthDate = "13" + birthYear + appSDate.Today().Substring(4, 6);
        return birthDate;
    }
    //==============================================================================================
}