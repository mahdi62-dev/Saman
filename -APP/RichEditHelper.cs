using DevExpress.XtraRichEdit.API.Native;

namespace App;

public class RichEditHelper
{
    public static object ReplaceTextInDocument(Document document, string searchText, string replaceText)
    {
        ReplaceSubDocumentText(document, searchText, replaceText);
        foreach (var section in document.Sections)
        {
            if (section.HasHeader(HeaderFooterType.First))
                ReplaceTextInHeaderHeader(HeaderFooterType.First, section, searchText, replaceText);

            if (section.HasHeader(HeaderFooterType.Primary))
            {
                ReplaceTextInHeaderHeader(HeaderFooterType.Primary, section, searchText, replaceText);
            }
            else
            {
                if (section.HasHeader(HeaderFooterType.Even))
                    ReplaceTextInHeaderHeader(HeaderFooterType.Even, section, searchText, replaceText);

                if (section.HasHeader(HeaderFooterType.Odd))
                    ReplaceTextInHeaderHeader(HeaderFooterType.Odd, section, searchText, replaceText);
            }

            if (section.HasFooter(HeaderFooterType.First))
                ReplaceTextInFooter(HeaderFooterType.First, section, searchText, replaceText);

            if (section.HasFooter(HeaderFooterType.Primary))
            {
                ReplaceTextInFooter(HeaderFooterType.Primary, section, searchText, replaceText);
            }
            else
            {
                if (section.HasFooter(HeaderFooterType.Even))
                    ReplaceTextInFooter(HeaderFooterType.Even, section, searchText, replaceText);

                if (section.HasFooter(HeaderFooterType.Odd))
                    ReplaceTextInFooter(HeaderFooterType.Odd, section, searchText, replaceText);
            }
        }

        return default;
    }

    private static void ReplaceTextInHeaderHeader(HeaderFooterType type, Section section, string searchText,
        string replaceText)
    {
        var header = section.BeginUpdateHeader(type);
        ReplaceSubDocumentText(header, searchText, replaceText);
        section.EndUpdateHeader(header);
    }

    private static void ReplaceTextInFooter(HeaderFooterType type, Section section, string searchText,
        string replaceText)
    {
        var footer = section.BeginUpdateFooter(type);
        ReplaceSubDocumentText(footer, searchText, replaceText);
        section.EndUpdateFooter(footer);
    }

    private static void ReplaceTextInTextBoxes(SubDocument subDocument, string searchText, string replaceText)
    {
        foreach (var shape in subDocument.Shapes)
            if (shape.TextBox is object)
                ReplaceSubDocumentText(shape.TextBox.Document, searchText, replaceText);
    }

    private static void ReplaceSubDocumentText(SubDocument subdocument, string searchText, string replaceText)
    {
        subdocument.ReplaceAll(searchText, replaceText, SearchOptions.None);
        ReplaceTextInTextBoxes(subdocument, searchText, replaceText);
    }
}