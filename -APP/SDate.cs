using System;
using System.Globalization;
using Persia;
using Calendar = Persia.Calendar;

namespace App;

public class SDate
{
    //==============================================================================================      
    //geogian calendar :     CultureInfo.InvariantCulture)
    public int DateDiffMinute(string SDate1, string STime1, string SDate2, string STime2)
    {
        var pc = new PersianCalendar();
        var SDate1SplitedParts = SDate1.Split('/');
        var STime1SplitedParts = STime1.Split(':');

        var SDate2SplitedParts = SDate2.Split('/');
        var STime2SplitedParts = STime2.Split(':');
        //int year = Convert.ToInt32(SDateSplitedParts[0]);
        //int month = Convert.ToInt32(SDateSplitedParts[1]);
        //int Day = Convert.ToInt32(SDateSplitedParts[2]);

        var dt = new DateTime(Convert.ToInt32(SDate1SplitedParts[0]),
            Convert.ToInt32(SDate1SplitedParts[1]),
            Convert.ToInt32(SDate1SplitedParts[2]),
            Convert.ToInt32(STime1SplitedParts[0]),
            Convert.ToInt32(STime1SplitedParts[1]),
            0, pc);

        //int.Parse(STimeSplitedParts[0]), int.Parse(STimeSplitedParts[1]), 0, pc);

        var dt2 = new DateTime(Convert.ToInt32(SDate2SplitedParts[0]),
            Convert.ToInt32(SDate2SplitedParts[1]),
            Convert.ToInt32(SDate2SplitedParts[2]),
            Convert.ToInt32(STime2SplitedParts[0]),
            Convert.ToInt32(STime2SplitedParts[1]),
            0, pc);


        var span = dt2.Subtract(dt);


        return Convert.ToInt32(span.TotalMinutes);
    }

    public DateTime SDateConvertFromString(string SDate1, string STime1)
    {
        var pc = new PersianCalendar();
        var SDate1SplitedParts = SDate1.Split('/');
        var STime1SplitedParts = STime1.Split(':');

        //var SDate2SplitedParts = SDate2.Split('/');
        //var STime2SplitedParts = STime2.Split(':');
        //int year = Convert.ToInt32(SDateSplitedParts[0]);
        //int month = Convert.ToInt32(SDateSplitedParts[1]);
        //int Day = Convert.ToInt32(SDateSplitedParts[2]);

        var dt = new DateTime(Convert.ToInt32(SDate1SplitedParts[0]),
            Convert.ToInt32(SDate1SplitedParts[1]),
            Convert.ToInt32(SDate1SplitedParts[2]),
            Convert.ToInt32(STime1SplitedParts[0]),
            Convert.ToInt32(STime1SplitedParts[1]),
            0, pc);

        //int.Parse(STimeSplitedParts[0]), int.Parse(STimeSplitedParts[1]), 0, pc);

        //DateTime dt2 = new DateTime(Convert.ToInt32(SDate2SplitedParts[0]),
        //    Convert.ToInt32(SDate2SplitedParts[1]),
        //    Convert.ToInt32(SDate2SplitedParts[2]),
        //    Convert.ToInt32(STime2SplitedParts[0]),
        //    Convert.ToInt32(STime2SplitedParts[1]),
        //    0, pc);


        //TimeSpan span = dt2.Subtract(dt);


        return dt;
    }

    public string ShamsiDate()
    {
        var now = DAL.App.ShamsiDate.GetServerDate();
        var PC = new PersianCalendar();

        string year, month, day, hour, min, today;

        year = PC.GetYear(now).ToString();

        if (PC.GetDayOfMonth(now).ToString().Length == 1)
            day = "0" + PC.GetDayOfMonth(now);
        else
            day = PC.GetDayOfMonth(now).ToString();

        if (PC.GetMonth(now).ToString().Length == 1)
            month = "0" + PC.GetMonth(now);
        else
            month = PC.GetMonth(now).ToString();

        if (PC.GetHour(now).ToString().Length == 1)
            hour = "0" + PC.GetHour(now);
        else
            hour = PC.GetHour(now).ToString();

        if (PC.GetMinute(now).ToString().Length == 1)
            min = "0" + PC.GetMinute(now);
        else
            min = PC.GetMinute(now).ToString();

        today = year + "/" + month + "/" + day + " " + hour + ":" + min;
        return today;
    }

    //==============================================================================================      
    public string ShamsiDateStart()
    {
        var now = DAL.App.ShamsiDate.GetServerDate();
        var PC = new PersianCalendar();

        string year, month, day, today;

        year = PC.GetYear(now).ToString();

        if (PC.GetDayOfMonth(now).ToString().Length == 1)
            day = "0" + PC.GetDayOfMonth(now);
        else
            day = PC.GetDayOfMonth(now).ToString();

        if (PC.GetMonth(now).ToString().Length == 1)
            month = "0" + PC.GetMonth(now);
        else
            month = PC.GetMonth(now).ToString();

        today = year + "/" + month + "/" + day + " 00:00";
        return today;
    }
    //==============================================================================================      


    public string ShamsiDateLong()
    {
        var now = DAL.App.ShamsiDate.GetServerDate();
        var PC = new PersianCalendar();

        string year, month, day, today;
        year = PC.GetYear(now).ToString();

        if (PC.GetDayOfMonth(now).ToString().Length == 1)
            day = "0" + PC.GetDayOfMonth(now);
        else
            day = PC.GetDayOfMonth(now).ToString();

        if (PC.GetMonth(now).ToString().Length == 1)
            month = "0" + PC.GetMonth(now);
        else
            month = PC.GetMonth(now).ToString();

        today = year + "/" + month + "/" + day + " 00:00";
        return today;
    }
    //==============================================================================================      

    public string ShamsiDateTime()
    {
        return DAL.App.ShamsiDate.GetServerDateTime();
    }

    //===============================================================================================
    public string MiladiToShamsiLong(DateTime mDate)
    {
        var PC = new PersianCalendar();

        string year, month, day, sDate;
        year = PC.GetYear(mDate).ToString();

        if (PC.GetDayOfMonth(mDate).ToString().Length == 1)
            day = "0" + PC.GetDayOfMonth(mDate);
        else
            day = PC.GetDayOfMonth(mDate).ToString();

        if (PC.GetMonth(mDate).ToString().Length == 1)
            month = "0" + PC.GetMonth(mDate);
        else
            month = PC.GetMonth(mDate).ToString();

        sDate = year + "/" + month + "/" + day + " 00:00";
        return sDate;
    }

    //===============================================================================================

    public string ShmasiDateText()
    {
        var shamsiDate = new string[8];
        string shamsiDateText = null, weekDay, day, month, year;

        var now = DateTime.Now; // DAL.App.ShamsiDate.GetServerDate();
        var PC = new PersianCalendar();
        weekDay = PC.GetDayOfWeek(now).ToString().ToUpper();

        switch (weekDay)
        {
            case "SATURDAY":
                weekDay = "شنبه";
                break;
            case "SUNDAY":
                weekDay = "یکشنبه";
                break;
            case "MONDAY":
                weekDay = "دوشنبه";
                break;
            case "TUESDAY":
                weekDay = "سه شنبه";
                break;
            case "WEDNESDAY":
                weekDay = "چهارشنبه";
                break;
            case "THURSDAY":
                weekDay = "پنجشنبه";
                break;
            case "FRIDAY":
                weekDay = "جمعه";
                break;
        }

        day = PC.GetDayOfMonth(now).ToString();
        month = PC.GetMonth(now).ToString();
        year = PC.GetYear(now).ToString();

        switch (month)
        {
            case "1":
                month = "فروردین";
                break;
            case "2":
                month = "اردیبهشت";
                break;
            case "3":
                month = "خرداد";
                break;
            case "4":
                month = "تیر";
                break;
            case "5":
                month = "مرداد";
                break;
            case "6":
                month = "شهریور";
                break;
            case "7":
                month = "مهر";
                break;
            case "8":
                month = "آبان";
                break;
            case "9":
                month = "آذر";
                break;
            case "10":
                month = "دی";
                break;
            case "11":
                month = "بهمن";
                break;
            case "12":
                month = "اسفند";
                break;
        }

        shamsiDateText = weekDay + " " + day + " " + month + " " + year;
        return shamsiDateText;
    }

    //===============================================================================================

    public string ClientTime()
    {
        var now = DateTime.Now;
        return now.TimeOfDay.ToString();
    }

    //===============================================================================================
    public string Today()
    {
        var PC = new PersianCalendar();

        string tYear, tMonth, tDay;
        tYear = PC.GetYear(DateTime.Now).ToString();

        if (PC.GetDayOfMonth(DateTime.Now).ToString().Length == 1)
            tDay = "0" + PC.GetDayOfMonth(DateTime.Now);
        else
            tDay = PC.GetDayOfMonth(DateTime.Now).ToString();

        if (PC.GetMonth(DateTime.Now).ToString().Length == 1)
            tMonth = "0" + PC.GetMonth(DateTime.Now);
        else
            tMonth = PC.GetMonth(DateTime.Now).ToString();

        return tYear + "/" + tMonth + "/" + tDay;
        ;
    }


    //====================================================================================

    public DateTime Shamsi2Miladi(string sDate)
    {
        var year = int.Parse(sDate.Substring(0, 4));
        var month = int.Parse(sDate.Substring(5, 2));
        var day = int.Parse(sDate.Substring(8, 2));

        return Calendar.ConvertToGregorian(year, month, day, DateType.Gerigorian);
    }

    public string AddDayLong(string sDate, double dayCount)
    {
        var mDate = Shamsi2Miladi(sDate);
        mDate = mDate.AddDays(dayCount);

        return MiladiToShamsiLong(mDate);
    }

    //===========================================================================================

    public string sWeekDayText(string sDate)
    {
        var mDate = Shamsi2Miladi(sDate);
        string weekDay = null;

        switch (mDate.DayOfWeek.ToString().ToUpper())
        {
            case "SATURDAY":
                weekDay = "شنبه";
                break;
            case "SUNDAY":
                weekDay = "یکشنبه";
                break;
            case "MONDAY":
                weekDay = "دوشنبه";
                break;
            case "TUESDAY":
                weekDay = "سه شنبه";
                break;
            case "WEDNESDAY":
                weekDay = "چهارشنبه";
                break;
            case "THURSDAY":
                weekDay = "پنجشنبه";
                break;
            case "FRIDAY":
                weekDay = "جمعه";
                break;
        }

        return weekDay;
    }


    //===========================================================================================

    public int sWeekDay(string sDate)
    {
        var mDate = Shamsi2Miladi(sDate);
        var weekDay = 0;

        switch (mDate.DayOfWeek.ToString().ToUpper())
        {
            case "SATURDAY":
                weekDay = 0;
                break;
            case "SUNDAY":
                weekDay = 1;
                break;
            case "MONDAY":
                weekDay = 2;
                break;
            case "TUESDAY":
                weekDay = 3;
                break;
            case "WEDNESDAY":
                weekDay = 4;
                break;
            case "THURSDAY":
                weekDay = 5;
                break;
            case "FRIDAY":
                weekDay = 6;
                break;
        }

        return weekDay;
    }
    //===========================================================================================

    public int TwoTimeMinDiff(string hour1, string hour2)
    {
        var t1 = TimeSpan.Parse(hour1);
        var t2 = TimeSpan.Parse(hour2);

        var tBase = TimeSpan.Parse("23:59");
        double minDiff;

        if (t2 > t1)
            minDiff = (t2 - t1).TotalMinutes;
        else
            minDiff = (tBase - t1).TotalMinutes + 1 + (t2 - TimeSpan.Parse("00:00")).TotalMinutes;

        return Convert.ToInt32(minDiff);
    }


    public string S2MDateTimeYMDDash(string SDate)
    {
        int year, mon, day, hour, min;
        year = Convert.ToInt32(SDate.Substring(0, 4));
        mon = Convert.ToInt32(SDate.Substring(5, 2));
        day = Convert.ToInt32(SDate.Substring(8, 2));
        hour = Convert.ToInt32(SDate.Substring(11, 2));
        min = Convert.ToInt32(SDate.Substring(14, 2));
        var p = new PersianCalendar();

        var DateEn = p.ToDateTime(year, mon, day, hour, min, 0, 0);


        string enYear, enMonth, enDay, enHour, enMin;

        enYear = DateEn.Year.ToString();

        if (DateEn.Month.ToString().Length == 1)
            enMonth = "0" + DateEn.Month;
        else
            enMonth = DateEn.Month.ToString();

        if (DateEn.Day.ToString().Length == 1)
            enDay = "0" + DateEn.Day;
        else
            enDay = DateEn.Day.ToString();

        if (DateEn.Hour.ToString().Length == 1)
            enHour = "0" + DateEn.Hour;
        else
            enHour = DateEn.Hour.ToString();

        if (DateEn.Minute.ToString().Length == 1)
            enMin = "0" + DateEn.Minute;
        else
            enMin = DateEn.Minute.ToString();

        var strEnDateTime = enYear + '-' + enMonth + '-' + enDay + ' ' + enHour + ':' + enMin;

        return strEnDateTime;
    }


    public string S2MDateMDYSlash(string SDate)
    {
        int year, mon, day, hour, min;
        year = Convert.ToInt32(SDate.Substring(0, 4));
        mon = Convert.ToInt32(SDate.Substring(5, 2));
        day = Convert.ToInt32(SDate.Substring(8, 2));
        hour = Convert.ToInt32(SDate.Substring(11, 2));
        min = Convert.ToInt32(SDate.Substring(14, 2));
        var p = new PersianCalendar();

        var DateEn = p.ToDateTime(year, mon, day, hour, min, 0, 0);


        string enYear, enMonth, enDay, enHour, enMin;

        enYear = DateEn.Year.ToString();

        if (DateEn.Month.ToString().Length == 1)
            enMonth = "0" + DateEn.Month;
        else
            enMonth = DateEn.Month.ToString();

        if (DateEn.Day.ToString().Length == 1)
            enDay = "0" + DateEn.Day;
        else
            enDay = DateEn.Day.ToString();

        if (DateEn.Hour.ToString().Length == 1)
            enHour = "0" + DateEn.Hour;
        else
            enHour = DateEn.Hour.ToString();

        if (DateEn.Minute.ToString().Length == 1)
            enMin = "0" + DateEn.Minute;
        else
            enMin = DateEn.Minute.ToString();

        var strEnDateTime = enMonth + '/' + enDay + '/' + enYear;

        return strEnDateTime;
    }
}