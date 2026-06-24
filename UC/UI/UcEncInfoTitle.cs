using System;
using DevExpress.XtraEditors;

namespace Saman.UC.UI;

public partial class UcEncInfoTitle : XtraUserControl
{
    private string _Age = "";

    private string _ChiefComplaintName = "";
    private string _Country = "";
    private string _Description = "";
    private string _DrName = "";
    private string _EncEndDate = "";
    private string _EncStartDate = "";
    private string _FatherName = "";

    private string _FullName = "";

    private string _InsuranceName = "";
    private string _InsuranceName2 = "";

    private string _TitlePrefix = "";

    public UcEncInfoTitle()
    {
        InitializeComponent();
    }


    public string TitlePrefix
    {
        get => _TitlePrefix;
        set
        {
            if (!string.IsNullOrEmpty(value)) _TitlePrefix = value;
            SetTitle();
        }
    }

    public string FullName
    {
        get => _FullName;
        set
        {
            if (!string.IsNullOrEmpty(value)) _FullName = value;
            SetTitle();
        }
    }

    public string FatherName
    {
        get => _FatherName;
        set
        {
            if (!string.IsNullOrEmpty(value)) _FatherName = value;
            SetTitle();
        }
    }

    public string Age
    {
        get => _Age;
        set
        {
            if (!string.IsNullOrEmpty(value)) _Age = value;
            SetTitle();
        }
    }

    public string DrName
    {
        get => _DrName;
        set
        {
            if (!string.IsNullOrEmpty(value)) _DrName = value;
            SetTitle();
        }
    }

    public string EncStartDate
    {
        get => _EncStartDate;
        set
        {
            if (!string.IsNullOrEmpty(value))
                _EncStartDate = value.Replace(' ', ' ');
            SetTitle();
        }
    }

    public string EncEndDate
    {
        get => _EncEndDate;
        set
        {
            if (!string.IsNullOrEmpty(value)) _EncEndDate = value.Replace(' ', ' ');
            SetTitle();
        }
    }

    public string InsuranceName
    {
        get => _InsuranceName;
        set
        {
            if (!string.IsNullOrEmpty(value)) _InsuranceName = value;
            SetTitle();
        }
    }

    public string InsuranceName2
    {
        get => _InsuranceName2;
        set
        {
            if (!string.IsNullOrEmpty(value)) _InsuranceName2 = value;
            SetTitle();
        }
    }

    public string ChiefComplaintName
    {
        get => _ChiefComplaintName;
        set
        {
            if (!string.IsNullOrEmpty(value)) _ChiefComplaintName = value;
            SetTitle();
        }
    }

    public string Description
    {
        get => _Description;
        set
        {
            if (!string.IsNullOrEmpty(value)) _Description = value;
            SetTitle();
        }
    }

    public string Country
    {
        get => _Country;
        set
        {
            if (!string.IsNullOrEmpty(value)) _Country = value;
            SetTitle();
        }
    }

    //#endregion

    private void SetTitle()
    {
        string
            fullName = null,
            age = null,
            fatherName = null,
            drName = null,
            encStartDate = null,
            encEndDate = null,
            insuranceName = null,
            insuranceName2 = null,
            chiefComplaintName = null,
            description = null,
            country = null;

        if (_FullName.Length > 0)
            fullName = "  " + _TitlePrefix + " " + FullName;

        if (_Age.Length > 0)
            age = "  " + _Age + " سال";

        if (_FatherName.Length > 0)
            fatherName = "  فرزند: " + _FatherName;

        if (_DrName.Length > 0)
            drName = "  پزشک: " + _DrName;

        if (_InsuranceName.Length > 0)
            insuranceName = "  بیمه: " + _InsuranceName;

        if (_InsuranceName2.Length > 0)
            insuranceName2 = "  تکمیلی: " + _InsuranceName2;

        if (_EncEndDate.Length > 0)
            encEndDate = "  تاریخ ترخیص: " + _EncEndDate;

        if (_EncStartDate.Length > 0)
            encStartDate = "  تاریخ پذیرش: " + _EncStartDate;

        if (_ChiefComplaintName.Length > 0)
            chiefComplaintName = "  دلیل مراجعه: " + _ChiefComplaintName;

        if (_Description.Length > 0)
            description = "  توضیحات: " + _Description;

        if (_Country.Length > 0)
            country = "  ملیت: " + _Country;

        lblTitle1.Text = fullName;
        lblTitle2.Text = age + fatherName;
        lblTitle3.Text = drName + insuranceName + insuranceName2;

        lblTitleLeft1.Text = encEndDate + encStartDate;
        lblTitleLeft2.Text = chiefComplaintName + country;
        lblTitleLeft3.Text = description;
    }

    private void UcEncInfoTitle_Load(object sender, EventArgs e)
    {
    }
}