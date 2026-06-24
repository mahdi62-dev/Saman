using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using App;
using DAL;
using LIB;
using Stimulsoft.Base;

namespace Saman;

internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        UL.CultureChange(2);

        Thread.CurrentThread.CurrentUICulture = new CultureInfo("fa-IR");
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var exePAth = Application.StartupPath;
        var SamanConfig = new IniFile(exePAth + @"\SamanConfig.ini");
        var AppID = SamanConfig.Read("AppID");
        var SamanServer = SamanConfig.Read("SamanServer");


        cSet.AppID = Convert.ToInt32(AppID);

        cSet.SetCon(SamanServer);
        cSet.SetConA(SamanServer);
        cSet.SetConERP(SamanServer);
        cSet.SetConReport(SamanServer);

        GL.EncCurStart();
        GL.PersCurStart();

        GL.IsDevelopStatus = false;

        StiLicense.Key =
            "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkgpgFGkUl79uxVs8X+uspx6K+tqdtOB5G1S6PFPRrlVNvMUiSiNYl724EZbrUAWwAYHlGLRbvxMviMExTh2l9xZJ2xc4K1z3ZVudRpQpuDdFq+fe0wKXSKlB6okl0hUd2ikQHfyzsAN8fJltqvGRa5LI8BFkA/f7tffwK6jzW5xYYhHxQpU3hy4fmKo/BSg6yKAoUq3yMZTG6tWeKnWcI6ftCDxEHd30EjMISNn1LCdLN0/4YmedTjM7x+0dMiI2Qif/yI+y8gmdbostOE8S2ZjrpKsgxVv2AAZPdzHEkzYSzx81RHDzZBhKRZc5mwWAmXsWBFRQol9PdSQ8BZYLqvJ4Jzrcrext+t1ZD7HE1RZPLPAqErO9eo+7Zn9Cvu5O73+b9dxhE2sRyAv9Tl1lV2WqMezWRsO55Q3LntawkPq0HvBkd9f8uVuq9zk7VKegetCDLb0wszBAs1mjWzN+ACVHiPVKIk94/QlCkj31dWCg8YTrT5btsKcLibxog7pv1+2e4yocZKWsposmcJbgG0";


        var frmLogin = new FrmLogin();
        if (frmLogin.ShowDialog() == DialogResult.OK)
            Application.Run(new FrmMain());
        else
            Application.Exit();

        // Application.Run(new FrmPComment1());
    }
}