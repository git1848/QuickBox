using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuickBox.MG.Common;
using QuickBox.MG.Data;
using QuickBox.MG.Entity;

namespace QuickBox
{
    public partial class FrmTray : Form
    {
        private const string MenuName_Exit = "EXIT";    //退出

        private ImageList smallIconImageList;           //定义小图标

        private ToolStripMenuItem toolStripSubMenu;
        private ToolStripSeparator toolStripSeparator;  //定义菜单分隔符

        public delegate void InitMenuDelegate();
        public static InitMenuDelegate DoInitMenu;                  //全部更新

        public delegate void UpdateMenuDelegate(string groupName);
        public static UpdateMenuDelegate DoUpdateMenuByGroupName;   //更新指定分组

        short _winQKey;
        string _winQ = "Win-Q";

        public FrmTray()
        {
            InitializeComponent();

            this.InitTrayMenu();        //设置菜单列表

            DoInitMenu = InitTrayMenu;

            DoUpdateMenuByGroupName = FlushTrayChildMenuInGroup;

            Utils.ClearMemory();        //释放内存

            //注册热键
            _winQKey = HotKey.GlobalAddAtom(_winQ);
            HotKey.RegisterHotKey(this.Handle, _winQKey, HotKey.KeyModifiers.WindowsKey, (int)Keys.Q);
        }

        private void InitTrayMenu()
        {
            this.notifyContextMenuStrip.Items.Clear();

            //将快捷的文件添加到托盘右键菜单中
            this.FlushTrayChildMenuInLove();

            //添加分隔符
            toolStripSeparator = new ToolStripSeparator();
            this.notifyContextMenuStrip.Items.Add(toolStripSeparator);

            IList<BoxGroup> groups = BoxFileData.getBoxGroups();
            if (groups != null && groups.Count > 0)
            {
                foreach (BoxGroup group in groups)
                {
                    //创建菜单
                    toolStripSubMenu = new ToolStripMenuItem();
                    toolStripSubMenu.Text = group.Name;
                    toolStripSubMenu.Image = QuickBox.Properties.Resources.Group;

                    //添加到托盘菜单
                    this.notifyContextMenuStrip.Items.Add(toolStripSubMenu);

                    //刷新该分组下的图标
                    this.FlushTrayChildMenuInGroup(toolStripSubMenu, group.Name);
                }
                groups = null;
            }

            //添加分隔符
            toolStripSeparator = new ToolStripSeparator();
            this.notifyContextMenuStrip.Items.Add(toolStripSeparator);

            //添加“退出”
            toolStripSubMenu = new ToolStripMenuItem();
            toolStripSubMenu.Text = "退出(&E)";
            toolStripSubMenu.Name = MenuName_Exit;
            toolStripSubMenu.Image = QuickBox.Properties.Resources.Exit;
            toolStripSubMenu.Click += new EventHandler(toolStripSubMenu_Click);
            this.notifyContextMenuStrip.Items.Add(toolStripSubMenu);

            toolStripSubMenu = null;
        }

        /// <summary>
        /// 将快捷的文件添加到托盘右键菜单中
        /// </summary>
        private void FlushTrayChildMenuInLove()
        {
            this.smallIconImageList = new ImageList() { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };

            IList<BoxFile> boxFiles = new List<BoxFile>();
            boxFiles = BoxFileData.getLikeShortcuts();

            if (boxFiles != null && boxFiles.Count > 0)
            {
                ToolStripMenuItem toolStripMenuItem;
                BoxFile boxFileItem;
                for (int i = 0; i < boxFiles.Count; i++)
                {
                    boxFileItem = boxFiles[i];

                    this.smallIconImageList.Images.Add(boxFileItem.SmallIcon);

                    toolStripMenuItem = new ToolStripMenuItem();
                    toolStripMenuItem.Text = boxFileItem.Name;
                    toolStripMenuItem.ToolTipText = boxFileItem.Name;
                    toolStripMenuItem.Tag = boxFileItem;
                    toolStripMenuItem.Image = this.smallIconImageList.Images[i];
                    toolStripMenuItem.Click += new EventHandler(toolStripSubMenu_Click);

                    this.notifyContextMenuStrip.Items.Add(toolStripMenuItem);
                }
                boxFileItem = null;
                boxFiles = null;
                toolStripMenuItem = null;
            }
        }

        /// <summary>
        /// 将文件添加到托盘右键菜单中
        /// </summary>
        private void FlushTrayChildMenuInGroup(string groupName)
        {
            ToolStripMenuItem toolStripSubMenu = Utils.GetContextMenuItemByText(this.notifyContextMenuStrip, groupName);
            FlushTrayChildMenuInGroup(toolStripSubMenu, groupName);
            toolStripSubMenu = null;
        }

        /// <summary>
        /// 将文件添加到托盘右键菜单中
        /// </summary>
        private void FlushTrayChildMenuInGroup(ToolStripMenuItem toolStripSubMenu, string groupName)
        {
            this.smallIconImageList = new ImageList() { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };

            IList<BoxFile> boxFiles = BoxFileData.getShortcuts(groupName);

            toolStripSubMenu.DropDownItems.Clear();

            if (boxFiles != null && boxFiles.Count > 0)
            {
                ToolStripMenuItem toolStripMenuItem;
                BoxFile boxFileItem;

                for (int i = 0; i < boxFiles.Count; i++)
                {
                    boxFileItem = boxFiles[i];

                    this.smallIconImageList.Images.Add(boxFileItem.SmallIcon);

                    toolStripMenuItem = new ToolStripMenuItem();
                    toolStripMenuItem.Text = boxFileItem.Name;
                    toolStripMenuItem.ToolTipText = boxFileItem.Name;
                    toolStripMenuItem.Tag = boxFileItem;
                    toolStripMenuItem.Image = this.smallIconImageList.Images[i];
                    toolStripMenuItem.Click += new EventHandler(toolStripSubMenu_Click);

                    toolStripSubMenu.DropDownItems.Add(toolStripMenuItem);
                }

                boxFileItem = null;
                boxFiles = null;
                toolStripMenuItem = null;
            }
        }

        /// <summary>
        /// 托盘的右键菜单单击事件
        /// </summary>
        private void toolStripSubMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            if (toolStripMenuItem.Name == MenuName_Exit)
            {
                //取消注册热键
                HotKey.UnregisterHotKey(this.Handle, _winQKey);
                HotKey.GlobalDeleteAtom(_winQKey);

                //退出
                this.Close();

                //(Exit)
                Application.Exit();
            }
            else
            {
                //打开文件
                BoxFile selectBoxFileItem = toolStripMenuItem.Tag as BoxFile;
                bool state = Utils.StartFile(selectBoxFileItem.Path);
                if (!state)
                {
                    MessageUtil.ShowTips(string.Format("文件“{0}”不存在。", selectBoxFileItem.Name));
                }
                selectBoxFileItem = null;
            }
            toolStripMenuItem = null;
        }

        /// <summary>
        /// 关闭窗体前
        /// </summary>
        private void FrmTray_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.notifyIcon.Visible = false;    //关闭前隐藏托盘图标，否则右下角托盘图标不消失
        }

        /// <summary>
        /// 托盘双击事件
        /// </summary>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (BoxConfig.STATIC_MainForm == null || BoxConfig.STATIC_MainForm.IsDisposed)
                {
                    BoxConfig.STATIC_MainForm = new FrmFiles();
                    BoxConfig.STATIC_MainForm.Show();
                    BoxConfig.STATIC_MainForm.Activate();
                }
                else
                {
                    BoxConfig.STATIC_MainForm.Show();
                    BoxConfig.STATIC_MainForm.Activate();
                }
            }
        }

        /// <summary>
        /// 关闭窗体后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmTray_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utils.ClearMemory();            //释放内存
        }

        private void notifyContextMenuStrip_VisibleChanged(object sender, EventArgs e)
        {
            bool flag = this.notifyContextMenuStrip.Visible;
            if (!flag)
            {
                Utils.ClearMemory();            //释放内存
            }
        }

        protected override void WndProc(ref Message m)// 监视Windows消息
        {
            switch (m.Msg)
            {
                case HotKey.WM_HOTKEY:
                    ProcessHotkey(m);//按下热键时调用ProcessHotkey()函数
                    break;
            }
            base.WndProc(ref m); //将系统消息传递自父类的WndProc
        }

        private void ProcessHotkey(Message m) //按下设定的键时调用该函数
        {
            IntPtr id = m.WParam;//IntPtr用于表示指针或句柄的平台特定类型
            int sid = id.ToInt32();
            if (sid == _winQKey)
            {
                if (BoxConfig.STATIC_StartForm == null || BoxConfig.STATIC_StartForm.IsDisposed)
                {
                    BoxConfig.STATIC_StartForm = new FrmStart();
                    BoxConfig.STATIC_StartForm.Show();
                    BoxConfig.STATIC_StartForm.Activate();
                }
                else
                {
                    BoxConfig.STATIC_StartForm.Show();
                    BoxConfig.STATIC_StartForm.Activate();
                }
            }
        }

    }
}
