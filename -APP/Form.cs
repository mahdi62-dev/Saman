using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace App;

public class Control
{
    //=========================================================================
    public static void TextEditLine(TextEdit txt, PaintEventArgs e)
    {
        var redPen = new Pen(Color.Gray, 1);

        float x1 = txt.Left, y1 = txt.Top + txt.Height;
        float x2 = txt.Left + txt.Width, y2 = txt.Top + txt.Height;


        e.Graphics.DrawLine(redPen, x1, y1, x2, y2);
    }

    //=========================================================================
    //=========================================================================
    public static void TextEditLineClear(TextEdit txt, PaintEventArgs e)
    {
        var redPen = new Pen(Color.White, 1);

        float x1 = txt.Left, y1 = txt.Top + txt.Height;
        float x2 = txt.Left + txt.Width, y2 = txt.Top + txt.Height;


        e.Graphics.DrawLine(redPen, x1, y1, x2, y2);
    }

    //=========================================================================
    //=========================================================================
    public static void LookupEditLine(LookUpEdit lue, PaintEventArgs e)
    {
        var redPen = new Pen(Color.Gray, 1);

        float x1 = lue.Left, y1 = lue.Top + lue.Height;
        float x2 = lue.Left + lue.Width, y2 = lue.Top + lue.Height;


        e.Graphics.DrawLine(redPen, x1, y1, x2, y2);
    }
    //=========================================================================
}