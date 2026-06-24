using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Def;

public class PersDef
{
    public DataTable Personnel
        (object CRUD, object PersID, object IsDisable, object PersType, object NationalCode, object FirstName,
            object LastName,
            object FatherName, object Gender, object IDCardNumber, object BirthDate, object MaritalStatus,
            object Mobile, object Tel,
            object Province, object City, object Street, object AddressDetail, object Bank, object BankAccountNumber,
            object Dep, object Photo,
            object Sign, object ContractType, object IsSalary, object IsInsurance, object IsPercase, object ShareCount,
            object RegNumber,
            object AcademicLevel, object IsFullTime, object Speciality, object DrExpert, object DrExpertise,
            object DrLevel,
            object DrUiType, object DrRoleType, object UserPersID, object ResuID, object ResuName, object Drowssap)
        //object Insurance, object TaxExemption, object BirthArea, object EducationLevel)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@PersID", PersID);
        com.Parameters.AddWithValue("@IsDisable", IsDisable);
        com.Parameters.AddWithValue("@PersType", PersType);
        com.Parameters.AddWithValue("@NationalCode", NationalCode);
        com.Parameters.AddWithValue("@FirstName", FirstName);
        com.Parameters.AddWithValue("@LastName", LastName);
        com.Parameters.AddWithValue("@FatherName", FatherName);
        com.Parameters.AddWithValue("@Gender", Gender);
        com.Parameters.AddWithValue("@IDCardNumber", IDCardNumber);
        com.Parameters.AddWithValue("@BirthDate", BirthDate);
        com.Parameters.AddWithValue("@MaritalStatus", MaritalStatus);
        com.Parameters.AddWithValue("@Mobile", Mobile);
        com.Parameters.AddWithValue("@Tel", Tel);
        com.Parameters.AddWithValue("@Province", Province);
        com.Parameters.AddWithValue("@City", City);
        com.Parameters.AddWithValue("@Street", Street);
        com.Parameters.AddWithValue("@AddressDetail", AddressDetail);
        com.Parameters.AddWithValue("@Bank", Bank);
        com.Parameters.AddWithValue("@BankAccountNumber", BankAccountNumber);
        com.Parameters.AddWithValue("@Dep", Dep);
        com.Parameters.AddWithValue("@Photo", Photo);
        com.Parameters.AddWithValue("@Sign", Sign);
        com.Parameters.AddWithValue("@ContractType", ContractType);
        com.Parameters.AddWithValue("@IsSalary", IsSalary);
        com.Parameters.AddWithValue("@IsInsurance", IsInsurance);
        com.Parameters.AddWithValue("@IsPercase", IsPercase);
        com.Parameters.AddWithValue("@ShareCount", ShareCount);
        com.Parameters.AddWithValue("@RegNumber", RegNumber);
        com.Parameters.AddWithValue("@AcademicLevel", AcademicLevel);
        com.Parameters.AddWithValue("@IsFullTime", IsFullTime);
        com.Parameters.AddWithValue("@Speciality", Speciality);
        com.Parameters.AddWithValue("@DrExpert", DrExpert);
        com.Parameters.AddWithValue("@DrExpertise", DrExpertise);
        com.Parameters.AddWithValue("@DrLevel", DrLevel);
        com.Parameters.AddWithValue("@DrUiType", DrUiType);
        com.Parameters.AddWithValue("@DrRoleType", DrRoleType);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        com.Parameters.AddWithValue("@ResuID", ResuID);
        com.Parameters.AddWithValue("@ResuName", ResuName);
        com.Parameters.AddWithValue("@Drowssap", Drowssap);

        //com.Parameters.AddWithValue("@Insurance", Insurance);
        //com.Parameters.AddWithValue("@TaxExemption", TaxExemption);
        //com.Parameters.AddWithValue("@BirthArea", BirthArea);
        //com.Parameters.AddWithValue("@EducationLevel", EducationLevel);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spPersonnel");
    }

    public DataTable ResuInfo
        (object CRUD, object ResuID, object ResuName, object Drowssap, object PersID, object UserPersID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@ResuID", ResuID);
        com.Parameters.AddWithValue("@ResuName", ResuName);
        com.Parameters.AddWithValue("@Drowssap", Drowssap);
        com.Parameters.AddWithValue("@PersID", PersID);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spSACResuInfo");
    }
}