using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Dictionary;

namespace Saman.APP.Stimul;

public class Stimul
{
    public void PrintReport(string cnString, string ReportPath, string ColumnName, string FilterString)
    {
        ReportPath = ReportPath.Replace(@"\", @"/");
        var report = new StiReport();
        report.Load(ReportPath);


        var sti = new StiSqlDatabase("SamanServer", cnString);
        report.Dictionary.Databases[0] = sti;


        var band = report.GetComponentByName("DataMain") as StiDataBand;

        var filter = new StiFilter
        {
            Column = ColumnName,
            Condition = StiFilterCondition.EqualTo,
            DataType = StiFilterDataType.String,
            Item = StiFilterItem.Value,
            Value1 = FilterString
        };


        band.Filters.Add(filter);

        report.ScriptUpdate();
        report.Compile();
        report.Render();

        //report.Show();

        report.Print(false);
    }

    public void PrintReport(string cnString, StiReport report, string ColumnName, string FilterString)
    {
        var sti = new StiSqlDatabase("SamanServer", cnString);
        report.Dictionary.Databases[0] = sti;


        var band = report.GetComponentByName("DataMain") as StiDataBand;

        var filter = new StiFilter
        {
            Column = ColumnName,
            Condition = StiFilterCondition.EqualTo,
            DataType = StiFilterDataType.String,
            Item = StiFilterItem.Value,
            Value1 = FilterString
        };


        band.Filters.Add(filter);

        report.ScriptUpdate();
        report.Compile();
        report.Render();

        //report.Show();

        report.Print(false);
    }
}