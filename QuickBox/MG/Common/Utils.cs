using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace QuickBox.MG.Common
{
    public class Utils
    {
        #region 根据文件路径获取图标
        /// <summary>
        /// 根据文件路径获取小图标
        /// </summary>
        /// <param name="fileName">文件路径(例如：F:\,F:\Images,F:\Images\Bg.jpg)</param>
        /// <returns>Icon图标</returns>
        public static Icon GetSmallIcon(string fileName)
        {
            IntPtr hImgSmall;
            QuickBox.MG.Common.Win32.SHFILEINFO shinfo = new Win32.SHFILEINFO();

            hImgSmall = Win32.SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);

            return System.Drawing.Icon.FromHandle(shinfo.hIcon);
        }

        /// <summary>
        /// 根据文件路径获取大图标
        /// </summary>
        /// <param name="fileName">文件路径(例如：F:\,F:\Images,F:\Images\Bg.jpg)</param>
        /// <returns>Icon图标</returns>
        public static Icon GetLargeIcon(string fileName)
        {
            IntPtr hImgLarge;
            QuickBox.MG.Common.Win32.SHFILEINFO shinfo = new Win32.SHFILEINFO();

            hImgLarge = Win32.SHGetFileInfo(fileName, 0, ref   shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
            return System.Drawing.Icon.FromHandle(shinfo.hIcon);
        }

        /// <summary>
        /// 从文件扩展名得到文件关联图标
        /// </summary>
        /// <param name="fileName">文件名或文件扩展名</param>
        /// <param name="smallIcon">是否是获取小图标，否则是大图标</param>
        /// <returns>Icon图标</returns>
        public static Icon GetFileLargeIcon(string fileName)
        {
            return GetFileIcon(fileName, false);
        }

        /// <summary>
        /// 从文件扩展名得到文件关联图标
        /// </summary>
        /// <param name="fileName">文件名或文件扩展名</param>
        /// <param name="smallIcon">是否是获取小图标，否则是大图标</param>
        /// <returns>Icon图标</returns>
        public static Icon GetFileSmallIcon(string fileName)
        {
            return GetFileIcon(fileName, true);
        }

        /// <summary>
        /// 从文件扩展名得到文件关联图标
        /// </summary>
        /// <param name="fileName">文件名或文件扩展名</param>
        /// <param name="smallIcon">是否是获取小图标，否则是大图标</param>
        /// <returns>Icon图标</returns>
        public static Icon GetFileIcon(string fileName, bool smallIcon)
        {
            QuickBox.MG.Common.Win32.SHFILEINFO shinfo = new Win32.SHFILEINFO();
            Icon icon = null;

            int iTotal = (int)Win32.SHGetFileInfo(fileName, 100, ref shinfo, 0, (uint)(smallIcon ? 273 : 272));
            if (iTotal > 0)
            {
                icon = Icon.FromHandle(shinfo.hIcon);
            }
            return icon;
        }
        #endregion

        #region 根据快捷方式获取文件信息（真实路径、说明）
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="lnk">快捷方式地址</param>
        /// <param name="targetPath">真实路径</param>
        /// <param name="description">说明</param>
        public static string GetFileTargetPath(string lnkPath)
        {
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(lnkPath);

            string temp = string.Empty;
            temp = shortcut.TargetPath;     //目标
            temp = shortcut.WorkingDirectory; //工作文件夹
            temp = shortcut.WindowStyle.ToString();    //窗体的样式：１为默认，２为最大化，３为最小化
            temp = shortcut.Description;    //快捷方式的描述
            temp = shortcut.IconLocation;   //图标

            return shortcut.TargetPath;
        }
        #endregion

        #region 图片转换
        /// <summary>
        /// 将图片转换成Base64
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static string ConvertImgToBase64(Bitmap bitmap)
        {
            MemoryStream m = new MemoryStream();
            bitmap.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            string icon_base64 = Convert.ToBase64String(m.ToArray());
            return icon_base64;
        }

        /// <summary>
        /// 将图片转换成Base64
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ConvertImgToBase64(string fileName)
        {
            MemoryStream m = new MemoryStream();
            Bitmap bp = new Bitmap(fileName);
            bp.Save(m, ImageFormat.Gif);
            byte[] b = m.GetBuffer();
            string base64string = Convert.ToBase64String(b);
            return base64string;
        }

        /// <summary>
        /// Base64字符串解码
        /// </summary>
        public static Bitmap ConvertBase64ToBitmap(string base64string)
        {
            try
            {
                byte[] bt = Convert.FromBase64String(base64string);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(bt);
                Bitmap bitmap = new Bitmap(stream);
                return bitmap;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 只打开一个实例
        /// <summary> 
        /// 该函数设置由不同线程产生的窗口的显示状态。 
        /// </summary> 
        /// <param name="hWnd">窗口句柄</param> 
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param> 
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// <summary> 
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary> 
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄。</param> 
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary> 
        /// 获取正在运行的实例，没有运行的实例返回null; 
        /// </summary> 
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        /// <summary> 
        /// 显示已运行的程序。 
        /// </summary> 
        public static void HandleRunningInstance(Process instance)
        {
            const int WS_SHOWNORMAL = 1;
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //显示，可以注释掉 
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端 
        }
        #endregion

        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static bool OpenFolder(string folderName)
        {
            try
            {
                Process.Start("explorer", @"/select," + folderName);
                return true;
            }
            catch (Win32Exception ex)
            {
                MessageUtil.ShowError("系统错误：" + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError("错误：" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 打开应用程序
        /// </summary>
        public static bool StartFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    return false;

                string fileExtension = Path.GetExtension(fileName).ToLower();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = fileName;           //设定需要执行的命令
                startInfo.Arguments = "";
                startInfo.WorkingDirectory = fileName.Substring(0, fileName.LastIndexOf("\\"));
                startInfo.UseShellExecute = false;       //不使用系统外壳程序启动
                startInfo.RedirectStandardInput = false; //不重定向输入
                startInfo.RedirectStandardOutput = true; //重定向输出
                startInfo.CreateNoWindow = true;         //不创建窗口
                startInfo.RedirectStandardError = true;
                startInfo.ErrorDialog = false;
                startInfo.CreateNoWindow = true;

                Process process = new Process();//创建进程对象
                process.StartInfo = startInfo;
                process.Start();
                process.Close();
                return true;
            }
            catch (Win32Exception ex)
            {
                MessageUtil.ShowError("系统错误：" + ex.Message);

                if (MessageUtil.ConfirmYesNo("是否打开文件所在的文件夹？"))
                {
                    //打开文件位置
                    Utils.OpenFolder(fileName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError("错误：" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxLength"></param>
        /// <param name="placeholder"></param>
        /// <returns></returns>
        public static string TruncateCnStr(string s, int maxLength, string placeholder)
        {
            byte[] bs = Encoding.ASCII.GetBytes(s);

            int cnLength = 0;
            int extraCount = 0;//额外的/多余的字符数.

            for (int i = 0; i <= bs.Length - 1; i++)
            {
                if (bs[i] == 63)//判断是否为汉字或全脚符号.
                {
                    cnLength++;
                }
                cnLength++;

                if (cnLength > maxLength)
                {
                    extraCount++;
                }
            }

            if (cnLength >= maxLength)
            {
                return s.Substring(0, s.Length - extraCount) + placeholder;
            }
            return s;
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="content">加密的内容</param>
        /// <returns></returns>
        public static string MD5Encrypt(string content)
        {
            if (string.IsNullOrEmpty(content) == false)
            {
                MD5 md5 = MD5.Create();
                string result = "";
                byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(content));
                for (int i = 0; i < data.Length; i++)
                {
                    result += data[i].ToString("x2");
                }
                return result;
            }
            else
            {
                Console.WriteLine("您加密的内容为空！");
                return "";
            }

        }

        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Win32.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        /// <summary>
        /// 获取菜单的文本
        /// </summary>
        /// <param name="cms"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ToolStripMenuItem GetContextMenuItemByText(ContextMenuStrip cms, string text)
        {
            foreach (var item in cms.Items)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem menuItem = item as ToolStripMenuItem;
                    if (menuItem.Text == text)
                    {
                        return menuItem;
                    }
                }
            }
            return null;
        }
    }
}
