using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuickBox.MG.Common;
using QuickBox.MG.Data;
using QuickBox.MG.Entity;
using QuickBox.MG.Controls;

namespace QuickBox
{
    public partial class FrmFiles : Form
    {
        #region 定义变量
        private const string MenuName_LoveFileItem = "LOVE_FILE_ITEM";                          //标记此项
        private const string MenuName_RemoveFileItem = "REMOVE_FILE_ITEM";                      //移除此项
        private const string MenuName_RemoveFileItemInLove = "REMOVE_FILE_ITEM_IN_LOVE";        //移除此项
        private const string MenuName_ModifyFileItemName = "MODIFY_FILE_ITEM_Name";             //重命名
        private const string MenuName_ModifyFileItemIcon = "MODIFY_FILE_ITEM_ICON";             //更新图标
        private const string MenuName_OpenWorkingDirectory = "MENUNAME_OPEN_WORKING_DIRECTORY"; //打开文件夹
        private const string MenuName_MoveToNewGroup = "MOVE_TO_NEW_GROUP";                     //移动到其他分组
        private const string MenuName_SetGroup = "SET_GROUP";                                   //更新分组
        private const string MenuName_SetView = "SET_View";                                     //更新显示方式
        private const string MenuName_Exit = "EXIT";                                            //退出

        private ImageList largeIconImageList;           //定义大图标
        private ImageList smallIconImageList;           //定义小图标

        private ToolStripMenuItem toolStripItem;            //定义菜单项
        private ToolStripSeparator toolStripSeparator;  //定义菜单分隔符

        private Point mouseOffset;                      //记录鼠标指针的坐标
        private bool isMouseDown = false;               //记录鼠标按键是否按下
        #endregion

        public FrmFiles()
        {
            InitializeComponent();

            BoxConfig.STATIC_MainForm = this;
        }

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void FrmFiles_Load(object sender, EventArgs e)
        {
            //设置显示位置（显示在左下角）
            this.setFormPosition();

            //初始化快捷方式
            this.initShortcutStyle();

            //初始化快捷方式右键菜单
            this.initShortcutContentMenu();

            //初始化Link快捷方式
            this.initLikeShortcutStyle();

            //初始化Link快捷方式右键菜单
            this.initLikeShortcutContentMenu();

            //初始化设置菜单
            this.initSetOptionContextMenu();
        }

        /// <summary>
        /// 关闭窗体后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmFiles_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utils.ClearMemory();            //释放内存
        }

        /// <summary>
        /// 窗体移动，鼠标按下
        /// </summary>
        private void FrmFiles_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;
            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X;
                yOffset = -e.Y;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        /// <summary>
        /// 窗体移动，鼠标拖动
        /// </summary>
        private void FrmFiles_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                this.Location = mousePos;
            }
        }

        /// <summary>
        /// 窗体移动，鼠标弹起
        /// </summary>
        private void FrmFiles_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        /// <summary>
        /// 设置菜单显示
        /// </summary>
        private void btnOption_Click(object sender, EventArgs e)
        {
            Point showPoint = PointToScreen(this.btnOption.Location);
            showPoint.Y += 20;
            this.SetOptionContextMenuStrip.Show(showPoint);
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region 列表框事件
        /// <summary>
        /// 拖拽文件到当前选中的TabPage中
        /// </summary>
        private void listViewFiles_DragDrop(object sender, DragEventArgs e)
        {
            TabPage selectTabPage = this.tabFileGroup.SelectedTab;
            ListView selectListView = selectTabPage.Controls[0] as ListView;
            string groupName = selectTabPage.Text;

            //获取拖拽进来的文件数组
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (files.Length > 0)
            {
                string fileName;
                string fileExtension;
                string filePath;

                Bitmap fileLargeIcon;
                Bitmap fileSmallIcon;

                BoxFile boxFileItem;

                foreach (string file in files)
                {
                    if (File.Exists(file))
                    {
                        //文件名称
                        fileName = Path.GetFileNameWithoutExtension(file);
                        //文件格式
                        fileExtension = Path.GetExtension(file).ToLower();
                        //文件路径
                        if (fileExtension == ".lnk")
                        {
                            //如果是快捷方式，就取出真实路径
                            filePath = Utils.GetFileTargetPath(file);//根据快捷方式获取真实路径
                        }
                        else
                        {
                            filePath = Path.GetFullPath(file);
                        }
                        //文件大图标
                        fileLargeIcon = Utils.GetFileLargeIcon(filePath).ToBitmap();
                        //文件小图标
                        fileSmallIcon = Utils.GetFileSmallIcon(filePath).ToBitmap();

                        //添加新文件
                        boxFileItem = new BoxFile()
                        {
                            Name = fileName,
                            LargeIcon = fileLargeIcon,
                            SmallIcon = fileSmallIcon,
                            Path = filePath
                        };

                        //添加新文件到组中
                        BoxFileData.addShortcut(boxFileItem, groupName);
                    }
                }

                //刷新该分组下的图标
                this.refreshShortcut(groupName, selectListView);

                //更新托盘窗体对应分组下面的图标
                FrmTray.DoUpdateMenuByGroupName(groupName);
            }
        }

        /// <summary>
        /// 拖拽文件
        /// </summary>
        private void listViewFiles_DragEnter(object sender, DragEventArgs e)
        {
            //拖拽
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// ListView的鼠标单机事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewLove_MouseClick(object sender, MouseEventArgs e)
        {
            //右键菜单
            if (e.Button == MouseButtons.Right && listViewLove.SelectedItems.Count > 0)
            {
                //显示右键菜单
                this.loveListViewContextMenuStrip.Show(listViewLove, e.X, e.Y);
            }
        }

        /// <summary>
        /// ListView的鼠标双击事件
        /// </summary>
        private void listViewLove_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //双击打开文件
            ListViewItem selectListViewItem = listViewLove.GetItemAt(e.X, e.Y);

            if (null != selectListViewItem)
            {
                BoxFile selectBoxFileItem = selectListViewItem.Tag as BoxFile;

                bool state = Utils.StartFile(selectBoxFileItem.Path);
                if (!state)
                {
                    MessageUtil.ShowTips(string.Format("文件“{0}”不存在。", selectBoxFileItem.Name));
                }
            }
        }

        /// <summary>
        /// ListView的鼠标右键事件
        /// </summary>
        private void listViewFiles_MouseClick(object sender, MouseEventArgs e)
        {
            TabPage selectTabPage = this.tabFileGroup.SelectedTab;
            ListView selectListView = selectTabPage.Controls[0] as ListView;

            //右键菜单
            if (e.Button == MouseButtons.Right && selectListView.SelectedItems.Count > 0)
            {
                //添加“移动到”的菜单子项
                ToolStripMenuItem menuMoveToGroup = (ToolStripMenuItem)(this.listViewContextMenuStrip.Items[MenuName_MoveToNewGroup]);
                menuMoveToGroup.DropDownItems.Clear();

                //添加除当前分组之外的其他分组
                foreach (TabPage tabPage in this.tabFileGroup.TabPages)
                {
                    if (tabPage == selectTabPage)
                        continue;

                    toolStripItem = new ToolStripMenuItem();
                    toolStripItem.Text = tabPage.Text;
                    toolStripItem.Name = "MoveToGroup_" + tabPage.Text;
                    toolStripItem.Image = QuickBox.Properties.Resources.Group;
                    toolStripItem.Click += new EventHandler(toolStripItem_Click);

                    menuMoveToGroup.DropDownItems.Add(toolStripItem);
                }

                //显示右键菜单
                this.listViewContextMenuStrip.Show(selectListView, e.X, e.Y);
            }
        }

        /// <summary>
        /// ListView的鼠标双击事件
        /// </summary>
        private void listViewFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TabPage selectTabPage = this.tabFileGroup.SelectedTab;
            ListView selectListView = selectTabPage.Controls[0] as ListView;

            //双击打开文件
            ListViewItem selectListViewItem = selectListView.GetItemAt(e.X, e.Y);

            if (null != selectListViewItem)
            {
                BoxFile selectBoxFileItem = selectListViewItem.Tag as BoxFile;

                bool state = Utils.StartFile(selectBoxFileItem.Path);
                if (!state)
                {
                    MessageUtil.ShowTips(string.Format("文件“{0}”不存在。", selectBoxFileItem.Name));
                }
            }
        }
        #endregion

        #region 菜单事件
        /// <summary>
        /// ListView的右键菜单事件
        /// </summary>
        private void toolStripItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem.Name == MenuName_MoveToNewGroup)
                return;

            TabPage selectTabPage = this.tabFileGroup.SelectedTab;
            ListView selectListView = selectTabPage.Controls[0] as ListView;
            BoxFile selectBoxFileItem;

            string groupName = selectTabPage.Text;

            if (menuItem.Name == MenuName_LoveFileItem)             //标记此项
            {
                if (selectListView.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < selectListView.SelectedItems.Count; i++)
                    {
                        selectBoxFileItem = selectListView.SelectedItems[i].Tag as BoxFile;

                        BoxFileData.addLikeShortcut(selectBoxFileItem);
                    }

                    // 刷新快捷位置的图标
                    this.refreshLikeShortcut();

                    //刷新托盘菜单
                    FrmTray.DoInitMenu();
                }
            }
            else if (menuItem.Name == MenuName_RemoveFileItem)      //移除选定项
            {
                if (selectListView.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < selectListView.SelectedItems.Count; i++)
                    {
                        selectBoxFileItem = selectListView.SelectedItems[i].Tag as BoxFile;

                        //删除文件
                        BoxFileData.deleteShortcut(selectBoxFileItem, groupName);
                    }

                    //刷新该分组下的图标
                    this.refreshShortcut(groupName, selectListView);

                    //更新托盘窗体对应分组下面的图标
                    FrmTray.DoUpdateMenuByGroupName(groupName);
                }
            }
            else if (menuItem.Name == MenuName_ModifyFileItemName)  //重命名
            {
                if (selectListView.SelectedItems.Count > 0)
                {
                    selectBoxFileItem = selectListView.SelectedItems[0].Tag as BoxFile;

                    string value = selectBoxFileItem.Name;
                    if (MessageUtil.ShowPrompt("重命名", "请输入文件名称:", ref value) == DialogResult.OK)
                    {
                        //修改文件名称
                        BoxFileData.updateShortcut(selectBoxFileItem.Key, value, groupName);

                        //刷新该分组下的图标
                        this.refreshShortcut(groupName, selectListView);

                        //更新托盘窗体对应分组下面的图标
                        FrmTray.DoUpdateMenuByGroupName(groupName);
                    }
                }
            }
            else if (menuItem.Name == MenuName_ModifyFileItemIcon)  //修改文件图标
            {
                if (selectListView.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < selectListView.SelectedItems.Count; i++)
                    {
                        selectBoxFileItem = selectListView.SelectedItems[i].Tag as BoxFile;
                        Bitmap fileLargeIcon = Utils.GetFileIcon(selectBoxFileItem.Path, false).ToBitmap();
                        Bitmap fileSmallIcon = Utils.GetFileIcon(selectBoxFileItem.Path, true).ToBitmap();

                        BoxFileData.updateShortcut(selectBoxFileItem.Key, fileLargeIcon, fileSmallIcon, selectTabPage.Text);
                    }

                    //刷新该分组下的图标
                    this.refreshShortcut(groupName, selectListView);

                    //更新托盘窗体对应分组下面的图标
                    FrmTray.DoUpdateMenuByGroupName(groupName);
                }
            }
            else if (menuItem.Name == MenuName_OpenWorkingDirectory)  //打开文件位置
            {
                if (selectListView.SelectedItems.Count > 0)
                {
                    selectBoxFileItem = selectListView.SelectedItems[0].Tag as BoxFile;

                    //打开文件位置
                    Utils.OpenFolder(selectBoxFileItem.Path);
                }
            }
            else if (menuItem.Name == MenuName_SetGroup)            //设置分组
            {
                //设置分组

                //前置配置分组
                this.tabFileGroup.SendToBack();
                //后置列表
                this.gbConfigGroupName.BringToFront();
            }
            else if (menuItem.Name.StartsWith("MoveToGroup_"))       //移动分组
            {
                if (selectListView.SelectedItems.Count > 0)
                {
                    string newGroupName = menuItem.Name.Split('_')[1];

                    for (int i = 0; i < selectListView.SelectedItems.Count; i++)
                    {
                        selectBoxFileItem = selectListView.SelectedItems[i].Tag as BoxFile;

                        BoxFileData.moveShortcut(selectBoxFileItem, groupName, newGroupName);
                    }

                    //刷新该分组下的图标
                    this.refreshShortcut(groupName, selectListView);

                    //更新托盘窗体对应分组下面的图标
                    FrmTray.DoUpdateMenuByGroupName(groupName);

                    //刷新新分组下的图标
                    this.refreshShortcut(newGroupName);

                    //更新托盘窗体对应新分组下面的图标
                    FrmTray.DoUpdateMenuByGroupName(newGroupName);
                }
            }
        }

        /// <summary>
        /// 快捷ListView的右键菜单事件
        /// </summary>
        private void loveToolStripItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            BoxFile selectBoxFileItem;

            if (menuItem.Name == MenuName_RemoveFileItemInLove)      //移除选定项
            {
                if (listViewLove.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < listViewLove.SelectedItems.Count; i++)
                    {
                        selectBoxFileItem = listViewLove.SelectedItems[i].Tag as BoxFile;
                        BoxFileData.deleteLikeShortcut(selectBoxFileItem);
                    }

                    //刷新该分组下的图标
                    this.refreshLikeShortcut();

                    //刷新托盘菜单
                    FrmTray.DoInitMenu();
                }
            }
        }
        #endregion

        #region 配置分组事件
        /// <summary>
        /// 添加组
        /// </summary>
        private void btnOPAddGroupName_Click(object sender, EventArgs e)
        {
            this.showDoChangeGroupPanel(OperateGroup.AddName);
        }

        /// <summary>
        /// 修改组名称
        /// </summary>
        private void btnOPModifyGroupName_Click(object sender, EventArgs e)
        {
            this.showDoChangeGroupPanel(OperateGroup.ModifyName);
        }

        /// <summary>
        /// 取消编辑
        /// </summary>
        private void btnOPCancelModifyName_Click(object sender, EventArgs e)
        {
            this.showOperateGroupPanel();
        }

        /// <summary>
        /// 执行添加组
        /// </summary>
        private void btnDoAddGroup_Click(object sender, EventArgs e)
        {
            string groupName = this.txtGroupName.Text.Trim();
            if (string.IsNullOrEmpty(groupName))
            {
                MessageUtil.ShowWarning("添加失败，新分组名称为空");
                return;
            }

            IList<BoxGroup> groups = BoxFileData.getBoxGroups();
            if (null != groups.FirstOrDefault(o => o.Name == groupName))
            {
                MessageUtil.ShowWarning("添加失败，分组名称已存在");
                return;
            }

            BoxFileData.addBoxGroup(groupName);

            MessageUtil.ShowTips("添加成功");

            this.refreshShortcutGroup();

            this.showOperateGroupPanel();
        }

        /// <summary>
        /// 执行修改组名称
        /// </summary>
        private void btnDoModifyGroup_Click(object sender, EventArgs e)
        {
            string oldGroupName = this.lbGroupNames.SelectedItem.ToString();
            string newGroupName = this.txtGroupName.Text.Trim();
            if (string.IsNullOrEmpty(newGroupName))
            {
                MessageUtil.ShowWarning("保存失败，新的分组名称为空");
                return;
            }

            IList<BoxGroup> groups = BoxFileData.getBoxGroups();
            if (null == groups.FirstOrDefault(o => o.Name == oldGroupName))
            {
                MessageUtil.ShowWarning("保存失败，旧的分组名称不存在");
                return;
            }

            if (newGroupName == oldGroupName)
            {
                MessageUtil.ShowWarning("保存失败，新的分组名称跟旧的分组名称一致");
                return;
            }

            BoxFileData.updateBoxGroup(oldGroupName, newGroupName);

            MessageUtil.ShowTips("保存成功");

            //刷新列表
            this.refreshShortcutGroup();

            this.showOperateGroupPanel();
        }

        /// <summary>
        /// 执行删除组
        /// </summary>
        private void btnDoDeleteGroupName_Click(object sender, EventArgs e)
        {
            string oldGroupName = this.lbGroupNames.SelectedItem.ToString();
            if (string.IsNullOrEmpty(oldGroupName))
            {
                MessageUtil.ShowWarning("删除失败，请选择要删除的分组名称");
                return;
            }

            IList<BoxGroup> groups = BoxFileData.getBoxGroups();
            if (null == groups.FirstOrDefault(o => o.Name == oldGroupName))
            {
                MessageUtil.ShowWarning("删除失败，旧的分组名称不存在");
                return;
            }

            if (!MessageUtil.ConfirmYesNo("是否删除该选择的分组？"))
            {
                return;
            }

            BoxFileData.deleteBoxGroup(oldGroupName);

            MessageUtil.ShowTips("删除成功");

            //刷新列表
            this.refreshShortcutGroup();

            this.showOperateGroupPanel();
        }

        /// <summary>
        /// 返回到管理界面
        /// </summary>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            //前置列表
            this.tabFileGroup.BringToFront();
            //后置配置分组
            this.gbConfigGroupName.SendToBack();

            this.tabFileGroup.Visible = false;
            //刷新图标列表
            this.initShortcutStyle();
            this.tabFileGroup.Visible = true;

            //刷新托盘菜单
            FrmTray.DoInitMenu();
        }
        #endregion

        #region 方法

        #region 设置窗体布局
        /// <summary>
        /// 设置显示位置
        /// </summary>
        private void setFormPosition()
        {
            this.StartPosition = FormStartPosition.Manual;
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int x = 5;
            int y = screenHeight - this.Height - 5;
            this.Location = new Point(x, y);
        }
        #endregion

        #region 设置快捷方式
        /// <summary>
        /// 创建组与文件
        /// </summary>
        private void initShortcutStyle()
        {
            ListView listView;
            TabPage tabPage;
            ToolStripMenuItem toolStripSubMenu;

            this.tabFileGroup.TabPages.Clear();

            this.lbGroupNames.Items.Clear();

            IList<BoxGroup> groups = BoxFileData.getBoxGroups();
            foreach (BoxGroup group in groups)
            {
                //将图标填充到ListView
                listView = new ListView();
                listView.BorderStyle = BorderStyle.Fixed3D;
                listView.MultiSelect = true;
                listView.View = View.List;
                listView.LabelEdit = false;
                listView.Scrollable = true;
                listView.Dock = DockStyle.Fill;
                listView.AllowDrop = true;
                listView.BackgroundImage = global::QuickBox.Properties.Resources.bg2;
                listView.BackgroundImageTiled = true;

                //ListView事件
                listView.MouseClick += new MouseEventHandler(listViewFiles_MouseClick);
                listView.MouseDoubleClick += new MouseEventHandler(listViewFiles_MouseDoubleClick);
                listView.DragDrop += new DragEventHandler(listViewFiles_DragDrop);
                listView.DragEnter += new DragEventHandler(listViewFiles_DragEnter);

                //创建新的TabPage
                tabPage = new TabPage();
                tabPage.Text = group.Name;
                tabPage.Tag = tabPage;
                tabPage.Controls.Add(listView);

                //添加到选项卡
                this.tabFileGroup.TabPages.Add(tabPage);

                //动态创建菜单
                toolStripSubMenu = new ToolStripMenuItem();
                toolStripSubMenu.Text = group.Name;

                //刷新该分组下的图标
                this.refreshShortcut(group.Name, listView);
            }

            //初始化分组列表
            this.refreshShortcutGroup();
        }

        /// <summary>
        /// 设置列表框右键菜单
        /// </summary>
        private void initShortcutContentMenu()
        {
            //标记此项
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "收藏";
            toolStripItem.Name = MenuName_LoveFileItem;
            toolStripItem.Image = QuickBox.Properties.Resources.Love;
            toolStripItem.Click += new EventHandler(toolStripItem_Click);
            this.listViewContextMenuStrip.Items.Add(toolStripItem);

            //移除此项
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "删除";
            toolStripItem.Name = MenuName_RemoveFileItem;
            toolStripItem.Image = QuickBox.Properties.Resources.Delete;
            toolStripItem.Click += new EventHandler(toolStripItem_Click);
            this.listViewContextMenuStrip.Items.Add(toolStripItem);

            //重命名
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "重命名";
            toolStripItem.Name = MenuName_ModifyFileItemName;
            toolStripItem.Image = QuickBox.Properties.Resources.Rename;
            toolStripItem.Click += new EventHandler(toolStripItem_Click);
            this.listViewContextMenuStrip.Items.Add(toolStripItem);

            //更新图标
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "更新图标";
            toolStripItem.Name = MenuName_ModifyFileItemIcon;
            toolStripItem.Image = QuickBox.Properties.Resources.Flush;
            toolStripItem.Click += new EventHandler(toolStripItem_Click);
            this.listViewContextMenuStrip.Items.Add(toolStripItem);
            

            //分隔符
            toolStripSeparator = new ToolStripSeparator();
            this.listViewContextMenuStrip.Items.Add(toolStripSeparator);

            //打开文件位置
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "打开文件位置";
            toolStripItem.Name = MenuName_OpenWorkingDirectory;
            toolStripItem.Image = QuickBox.Properties.Resources.OpenFolder;
            toolStripItem.Click += new EventHandler(toolStripItem_Click);
            this.listViewContextMenuStrip.Items.Add(toolStripItem);


            //分隔符
            toolStripSeparator = new ToolStripSeparator();
            this.listViewContextMenuStrip.Items.Add(toolStripSeparator);

            //移动到
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "移动到->";
            toolStripItem.Name = MenuName_MoveToNewGroup;
            toolStripItem.Image = QuickBox.Properties.Resources.Move;
            toolStripItem.Click += new EventHandler(toolStripItem_Click);
            this.listViewContextMenuStrip.Items.Add(toolStripItem);
        }

        /// <summary>
        /// 根据分组名称，将文件添加到ListView中
        /// </summary>
        /// <param name="groupName"></param>
        private void refreshShortcut(string groupName)
        {
            TabPage selectTabPage = null;
            foreach (TabPage tabPage in this.tabFileGroup.TabPages)
            {
                if (tabPage.Text == groupName)
                {
                    selectTabPage = tabPage;
                    break;
                }
            }

            if (selectTabPage != null)
            {
                ListView selectListView = selectTabPage.Controls[0] as ListView;
                refreshShortcut(groupName, selectListView);
            }
        }

        /// <summary>
        /// 将文件添加到ListView中
        /// </summary>
        private void refreshShortcut(string groupName, ListView listViewFiles)
        {
            this.largeIconImageList = new ImageList() { ImageSize = new Size(32, 32), ColorDepth = ColorDepth.Depth32Bit };
            this.smallIconImageList = new ImageList() { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };

            listViewFiles.LargeImageList = largeIconImageList;
            listViewFiles.SmallImageList = smallIconImageList;

            IList<BoxFile> boxFiles = BoxFileData.getShortcuts(groupName);

            ListViewItem listViewItem;
            BoxFile boxFileItem;

            listViewFiles.Items.Clear();

            if (boxFiles != null && boxFiles.Count > 0)
            {
                for (int i = 0; i < boxFiles.Count; i++)
                {
                    boxFileItem = boxFiles[i];

                    this.largeIconImageList.Images.Add(boxFileItem.LargeIcon);
                    this.smallIconImageList.Images.Add(boxFileItem.SmallIcon);

                    listViewItem = new ListViewItem();
                    listViewItem.Text = boxFileItem.Name;
                    listViewItem.Tag = boxFileItem;
                    listViewItem.ImageIndex = i;

                    listViewItem.SubItems.AddRange(new string[] { boxFileItem.Path });

                    listViewFiles.Items.Add(listViewItem);
                }
            }
        }

        /// <summary>
        /// 刷新组
        /// </summary>
        private void refreshShortcutGroup()
        {
            this.lbGroupNames.Items.Clear();

            IList<BoxGroup> groups = BoxFileData.getBoxGroups();
            foreach (BoxGroup group in groups)
            {
                //添加到分组列表
                this.lbGroupNames.Items.Add(group.Name);
            }

            if (groups.Count > 0)
            {
                //分组列表默认选择第一项
                this.lbGroupNames.SelectedIndex = 0;
            }
        }
        #endregion

        #region 设置Link快捷方式
        /// <summary>
        /// 设置快捷列表框
        /// </summary>
        private void initLikeShortcutStyle()
        {
            //将图标填充到ListView
            listViewLove.BorderStyle = BorderStyle.None;
            listViewLove.MultiSelect = true;
            listViewLove.View = View.LargeIcon;
            listViewLove.LabelEdit = false;
            listViewLove.Scrollable = true;
            listViewLove.AllowDrop = false;
            listViewLove.BackgroundImage = global::QuickBox.Properties.Resources.bg2;
            listViewLove.BackgroundImageTiled = true;

            //ListView事件
            listViewLove.MouseClick += new MouseEventHandler(listViewLove_MouseClick);
            listViewLove.MouseDoubleClick += new MouseEventHandler(listViewLove_MouseDoubleClick);

            // 刷新快捷位置的图标
            this.refreshLikeShortcut();
        }

        /// <summary>
        /// 设置快捷列表框的右键菜单
        /// </summary>
        private void initLikeShortcutContentMenu()
        {
            //移除此项
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "移除";
            toolStripItem.Name = MenuName_RemoveFileItemInLove;
            toolStripItem.Image = QuickBox.Properties.Resources.UnLove;
            toolStripItem.Click += new EventHandler(loveToolStripItem_Click);
            this.loveListViewContextMenuStrip.Items.Add(toolStripItem);
        }

        /// <summary>
        /// 刷新快捷位置的图标
        /// </summary>
        private void refreshLikeShortcut()
        {
            this.largeIconImageList = new ImageList() { ImageSize = new Size(32, 32), ColorDepth = ColorDepth.Depth32Bit };
            this.smallIconImageList = new ImageList() { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };

            listViewLove.LargeImageList = largeIconImageList;
            listViewLove.SmallImageList = smallIconImageList;

            IList<BoxFile> boxFiles = BoxFileData.getLikeShortcuts();

            ListViewItem listViewItem;
            BoxFile boxFileItem;

            listViewLove.Items.Clear();

            if (boxFiles != null && boxFiles.Count > 0)
            {
                for (int i = 0; i < boxFiles.Count; i++)
                {
                    boxFileItem = boxFiles[i];

                    this.largeIconImageList.Images.Add(boxFileItem.LargeIcon);
                    this.smallIconImageList.Images.Add(boxFileItem.SmallIcon);

                    listViewItem = new ListViewItem();
                    listViewItem.Text = boxFileItem.Name;
                    listViewItem.Tag = boxFileItem;
                    listViewItem.ImageIndex = i;

                    listViewItem.SubItems.AddRange(new string[] { boxFileItem.Path });

                    listViewLove.Items.Add(listViewItem);
                }
            }
        }
        #endregion

        #region 设置菜单
        /// <summary>
        /// 设置菜单列表
        /// </summary>
        private void initSetOptionContextMenu()
        {
            //更新图标
            toolStripItem = new ToolStripMenuItem();
            toolStripItem.Text = "设置分组";
            toolStripItem.Name = MenuName_SetGroup;
            toolStripItem.Image = QuickBox.Properties.Resources.Option;
            toolStripItem.Click += new EventHandler(toolStripItem_Click);
            this.SetOptionContextMenuStrip.Items.Add(toolStripItem);
        }
        #endregion

        #region 设置操作分组名称（添加、删除、修改分组名称）
        private enum OperateGroup
        {
            AddName,
            ModifyName
        }

        /// <summary>
        /// 显示组的列表界面
        /// </summary>
        private void showOperateGroupPanel()
        {
            //显示分组名称列表界面
            this.gbOperateGroup.Location = new Point(100, 20);
            this.gbOperateGroup.Visible = true;

            //隐藏编辑分组界面
            this.gbDoChangeGroup.Visible = false;
        }

        /// <summary>
        /// 显示组的操作界面
        /// </summary>
        private void showDoChangeGroupPanel(OperateGroup action)
        {
            switch (action)
            {
                case OperateGroup.AddName:
                    {
                        this.txtGroupName.Text = "";
                        this.btnDoAddGroupName.Visible = true;
                        this.btnDoModifyGroupName.Visible = false;
                    }
                    break;
                case OperateGroup.ModifyName:
                    {
                        string oldGroupName = this.lbGroupNames.SelectedItem.ToString();
                        this.txtGroupName.Text = oldGroupName;

                        this.btnDoAddGroupName.Visible = false;
                        this.btnDoModifyGroupName.Visible = true;
                    }
                    break;
                default:
                    break;
            }

            //隐藏分组名称列表界面
            this.gbOperateGroup.Visible = false;

            //显示编辑分组界面
            this.gbDoChangeGroup.Location = new Point(100, 20);
            this.gbDoChangeGroup.Visible = true;
        }
        #endregion

        #endregion
    }
}

