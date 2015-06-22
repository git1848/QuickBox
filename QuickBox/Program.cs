using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using QuickBox.MG.Common;

namespace QuickBox
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Process instance = Utils.RunningInstance();
            if (instance == null)
            {
                HideOnStartupApplicationContext context = new HideOnStartupApplicationContext(new FrmTray());
                Application.Run(context);
            }
            else
            {
                Utils.HandleRunningInstance(instance);
            }
        }
    }
}
