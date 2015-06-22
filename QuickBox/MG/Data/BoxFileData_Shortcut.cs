using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using QuickBox.MG.Entity;

namespace QuickBox.MG.Data
{
    public partial class BoxFileData
    {
        #region BoxFile
        public static List<BoxFile> getShortcuts()
        {
            IList<BoxGroup> boxGroups = getBoxGroups();
            IList<BoxFile> tmpboxFiles = new List<BoxFile>();

            List<BoxFile> boxFiles = new List<BoxFile>();

            if (boxGroups != null && boxGroups.Count > 0)
            {
                string key = string.Empty;
                foreach (var boxGroup in boxGroups)
                {
                    tmpboxFiles = getShortcuts(boxGroup.Name);
                    if (tmpboxFiles != null && tmpboxFiles.Count > 0)
                    {
                        boxFiles.AddRange(tmpboxFiles);
                        tmpboxFiles = null;
                    }
                }

                boxGroups = null;
            }
            return boxFiles;
        }

        public static IList<BoxFile> getShortcuts(string _groupName)
        {
            var boxFiles = BoxFileData.STATIC_Shortcuts.Where(o => o.Key.Key == BoxFileData.getUniqueKey(_groupName)).FirstOrDefault().Value;
            return boxFiles == null ? null : boxFiles.OrderBy(o => o.Name).ToList();
        }

        public static void addShortcut(BoxFile _boxFile, string _groupName)
        {
            _boxFile.Key = BoxFileData.getUniqueKey(_boxFile.Name);
            if (existShortcut(_boxFile.Key, _groupName))
                return;

            var data = BoxFileData.STATIC_Shortcuts.Where(o => o.Key.Key == BoxFileData.getUniqueKey(_groupName)).FirstOrDefault();
            var boxGroup = data.Key;
            var boxFiles = data.Value;
            if (boxFiles == null)
            {
                BoxFileData.STATIC_Shortcuts.Remove(boxGroup);

                boxFiles = new List<BoxFile>();
                boxFiles.Add(_boxFile);

                BoxFileData.STATIC_Shortcuts.Add(boxGroup, boxFiles);
            }
            else
            {
                boxFiles.Add(_boxFile);
            }

            BoxFileData.SaveShortcut();
        }

        public static void updateShortcut(string _fileKey, string name, string _groupName)
        {
            if (!existShortcut(_fileKey, _groupName))
                return;

            var data = BoxFileData.STATIC_Shortcuts.Where(o => o.Key.Key == BoxFileData.getUniqueKey(_groupName)).FirstOrDefault();
            var _boxFile = data.Value.Where(o => o.Key == _fileKey).FirstOrDefault();

            _boxFile.Name = name;

            BoxFileData.SaveShortcut();
        }

        public static void updateShortcut(string _fileKey, string name, string path, string _groupName)
        {
            if (!existShortcut(_fileKey, _groupName))
                return;

            var data = BoxFileData.STATIC_Shortcuts.Where(o => o.Key.Key == BoxFileData.getUniqueKey(_groupName)).FirstOrDefault();
            var _boxFile = data.Value.Where(o => o.Key == _fileKey).FirstOrDefault();

            _boxFile.Name = name;
            _boxFile.Path = path;

            BoxFileData.SaveShortcut();
        }

        public static void updateShortcut(string _fileKey, Bitmap largeIcon, Bitmap smallIcon, string _groupName)
        {
            if (!existShortcut(_fileKey, _groupName))
                return;

            var data = BoxFileData.STATIC_Shortcuts.Where(o => o.Key.Key == BoxFileData.getUniqueKey(_groupName)).FirstOrDefault();
            var _boxFile = data.Value.Where(o => o.Key == _fileKey).FirstOrDefault();

            _boxFile.LargeIcon = largeIcon;
            _boxFile.SmallIcon = smallIcon;

            BoxFileData.SaveShortcut();
        }

        public static void deleteShortcut(BoxFile _boxFile, string _groupName)
        {
            if (!existShortcut(_boxFile.Key, _groupName))
                return;

            var data = BoxFileData.STATIC_Shortcuts.Where(o => o.Key.Key == BoxFileData.getUniqueKey(_groupName)).FirstOrDefault();
            var boxGroup = data.Key;
            var boxFiles = data.Value;
            if (boxFiles == null)
            {
                BoxFileData.STATIC_Shortcuts.Remove(boxGroup);
                boxFiles = new List<BoxFile>();
            }
            else
            {
                boxFiles.Remove(_boxFile);
            }

            BoxFileData.SaveShortcut();
        }

        public static void moveShortcut(BoxFile _boxFile, string old_group_name, string new_group_name)
        {
            deleteShortcut(_boxFile, old_group_name);
            addShortcut(_boxFile, new_group_name);
        }

        private static bool existShortcut(string _fileKey, string _groupName)
        {
            var data = BoxFileData.STATIC_Shortcuts.Where(o => o.Key.Key == BoxFileData.getUniqueKey(_groupName)).FirstOrDefault();
            var boxGroup = data.Key;
            var boxFiles = data.Value;

            if (boxFiles == null)
                return false;
            else
                return boxFiles.Where(o => o.Key == _fileKey).FirstOrDefault() != null;
        }
        #endregion
    }
}
