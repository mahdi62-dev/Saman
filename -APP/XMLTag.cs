using System;

namespace Saman.APP;

public class XMLTag
{
    public string TagStartGenerator(string tag, string val, bool NextLine)
    {
        string str;
        str = "<" + tag + ">";

        if (val != null)
        {
            str += val;
            str += "</" + tag + ">";
        }

        if (NextLine) str += Environment.NewLine;

        return str;
    }

    public string TagEndGenerator(string tag, string val, bool NextLine)
    {
        string str;
        str = "</" + tag + ">";

        if (val != null)
        {
            str += val;
            str += "</" + tag + ">";
        }

        if (NextLine) str += Environment.NewLine;

        return str;
    }

    public string TagGenerator(string tag, string val, bool NextLine)
    {
        string str;
        str = "<" + tag + ">";
        str += val;
        str += "</" + tag + ">";

        if (NextLine) str += Environment.NewLine;

        return str;
    }
}