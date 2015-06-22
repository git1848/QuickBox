using System;
using System.Windows.Forms;

namespace QuickBox.MG.Common
{
    public class HideOnStartupApplicationContext : ApplicationContext
    {
        private Form mainFormInternal;

        // 构造函数，主窗体被存储在mainFormInternal
        public HideOnStartupApplicationContext(Form mainForm)
        {
            this.mainFormInternal = mainForm;
            this.mainFormInternal.Closed += new EventHandler(mainFormInternal_Closed);
        }

        // 当主窗体被关闭时，退出应用程序
        void mainFormInternal_Closed(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
