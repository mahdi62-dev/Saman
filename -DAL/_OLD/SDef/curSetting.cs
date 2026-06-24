namespace DAL;

public class curSetting
{
    public static string serverName;

    public static string cnString = "Data Source=" + serverName +
                                    "; Database=HIS;" +
                                    " Integrated Security=True";
    //"Data Source=192.168.50.34; Database=HIS; User ID=HIS;Password=1234";

    //public static void setReg()
    //{
    //  RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"Software\FaraTeb", true);


    //}
}