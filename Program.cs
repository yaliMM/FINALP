using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using CSharpToCpp;



namespace CSharpToCpp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // מפעיל את הממשק הגרפי
            Application.Run(new Form1());
        }
    }
}
