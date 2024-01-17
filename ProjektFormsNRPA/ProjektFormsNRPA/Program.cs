using Microsoft.VisualBasic.ApplicationServices;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Text;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.AxHost;
using Banka;

namespace ProjektFormsNRPA
{

        internal static class Program
        {
            /// <summary>
            ///  The main entry point for the application.
            /// </summary>
            [STAThread]
            static void Main()
            {
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new Form1());
            }
        }

    
}