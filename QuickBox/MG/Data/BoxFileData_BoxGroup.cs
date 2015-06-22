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
        #region BoxGroup
        public static void addBoxGroup(string _groupName)
        {
            BoxFileData.STATIC_Shortcuts.Add(new BoxGroup() { Key = BoxFileData.getUniqueKey(_groupName), Name = _groupName }, null);

            BoxFileData.SaveShortcut();
        }

        public static void updateBoxGroup(string oldGroupName, string newGroupName)
        {
            var data = BoxFileData.STATIC_Shortcuts.Where(o => o.Key.Key == BoxFileData.getUniqueKey(oldGroupName)).FirstOrDefault();
            var oldBoxGroup = data.Key;
            var newBoxGroup = new BoxGroup() { Key = BoxFileData.getUniqueKey(newGroupName), Name = newGroupName };
            var newBoxFileItems = data.Value;

            BoxFileData.STATIC_Shortcuts.Remove(oldBoxGroup);
            BoxFileData.STATIC_Shortcuts.Add(newBoxGroup, newBoxFileItems);

            BoxFileData.SaveShortcut();
        }

        public static void deleteBoxGroup(string oldGroupName)
        {
            var data = BoxFileData.STATIC_Shortcuts.Where(o => o.Key.Key == BoxFileData.getUniqueKey(oldGroupName)).FirstOrDefault();
            var oldBoxGroup = data.Key;

            BoxFileData.STATIC_Shortcuts.Remove(oldBoxGroup);

            BoxFileData.SaveShortcut();
        }

        public static IList<BoxGroup> getBoxGroups()
        {
            return BoxFileData.STATIC_Shortcuts.Keys.OrderBy(o => o.Name).ToList();
        }
        #endregion
    }
}
