using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using QuickBox.MG.Entity;
using QuickBox.MG.Common;
using System.IO;
using System.Windows.Forms;
using QuickBox.MG.Serialize;

namespace QuickBox.MG.Data
{
    public partial class BoxFileData
    {
        public static string STATIC_Shortcuts_Folder = "Shortcut";
        public static string STATIC_Shortcuts_FileName = "Shortcut.Binary";
        public static string STATIC_LikeShortcuts_FileName = "LikeShortcut.Binary";
        public static string STATIC_GroupName = "默认";

        public static Dictionary<BoxGroup, List<BoxFile>> STATIC_Shortcuts = new Dictionary<BoxGroup, List<BoxFile>>();

        public static List<BoxFile> STATIC_LikeShortcuts = new List<BoxFile>();

        static BoxFileData()
        {
            InitShortcutFolder();

            LoadShortcut();

            LoadLikeShortcut();
        }

        #region Shortcut Folder
        private static void InitShortcutFolder()
        {
            string path = Path.Combine(Application.StartupPath, STATIC_Shortcuts_Folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        #endregion

        #region Shortcut
        private static void LoadShortcut()
        {
            if (STATIC_Shortcuts.Count == 0)
            {
                string path = Path.Combine(Application.StartupPath, STATIC_Shortcuts_Folder, STATIC_Shortcuts_FileName);
                if (File.Exists(path))
                {
                    using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        ISerializeHelper se = new BinarySerializeHelper();
                        STATIC_Shortcuts = se.DeSerialize<Dictionary<BoxGroup, List<BoxFile>>>(stream);
                    }
                }

                if (STATIC_Shortcuts == null || STATIC_Shortcuts.Count == 0)
                {
                    STATIC_Shortcuts = new Dictionary<BoxGroup, List<BoxFile>>();
                    STATIC_Shortcuts.Add(new BoxGroup() { Key = getUniqueKey(STATIC_GroupName), Name = STATIC_GroupName }, null);
                }
            }
        }

        private static void SaveShortcut()
        {
            string path = Path.Combine(Application.StartupPath, STATIC_Shortcuts_Folder, STATIC_Shortcuts_FileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                ISerializeHelper se = new BinarySerializeHelper();
                se.Serialize<Dictionary<BoxGroup, List<BoxFile>>>(fileStream, STATIC_Shortcuts);
            }
        }
        #endregion

        #region LikeShortcut
        private static void LoadLikeShortcut()
        {
            if (STATIC_LikeShortcuts.Count == 0)
            {
                string path = Path.Combine(Application.StartupPath, STATIC_Shortcuts_Folder, STATIC_LikeShortcuts_FileName);
                if (File.Exists(path))
                {
                    using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        ISerializeHelper se = new BinarySerializeHelper();
                        STATIC_LikeShortcuts = se.DeSerialize<List<BoxFile>>(stream);
                    }
                }

                if (STATIC_LikeShortcuts == null || STATIC_LikeShortcuts.Count == 0)
                {
                    STATIC_LikeShortcuts = new List<BoxFile>();
                }
            }
        }

        private static void SaveLikeShortcut()
        {
            string path = Path.Combine(Application.StartupPath, STATIC_Shortcuts_Folder, STATIC_LikeShortcuts_FileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                ISerializeHelper se = new BinarySerializeHelper();
                se.Serialize<List<BoxFile>>(fileStream, STATIC_LikeShortcuts);
            }
        }
        #endregion

        private static string getUniqueKey(string fileName)
        {
            string key = fileName.Replace("快捷方式", "")
                            .Replace(".", "_")
                            .Replace("  ", "_")
                            .Replace(" ", "_").ToLower();

            return Utils.MD5Encrypt(key);
        }
    }
}
