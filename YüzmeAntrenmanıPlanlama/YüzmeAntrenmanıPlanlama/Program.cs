using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace YüzmeAntrenmanıPlanlama
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            WindowsFormsSettings.SetDPIAware();
            WindowsFormsSettings.EnableFormSkins();
            WindowsFormsSettings.DefaultFont = new Font("Segoe UI", 9.75F);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("WXI");
            Application.Run(new Form1());
        }
    }
}