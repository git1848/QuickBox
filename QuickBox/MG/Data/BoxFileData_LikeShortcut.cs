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
        #region LoveFavorite
        public static IList<BoxFile> getLikeShortcuts()
        {
            return BoxFileData.STATIC_LikeShortcuts;
        }

        public static void addLikeShortcut(BoxFile _boxFile)
        {
            _boxFile.Key = BoxFileData.getUniqueKey(_boxFile.Name);

            if (existLikeShortcut(_boxFile.Key))
                return;

            BoxFileData.STATIC_LikeShortcuts.Add(_boxFile);

            BoxFileData.SaveLikeShortcut();
        }

        public static void deleteLikeShortcut(BoxFile _boxFile)
        {
            if (!existLikeShortcut(_boxFile.Key))
                return;

            BoxFileData.STATIC_LikeShortcuts.Remove(_boxFile);

            BoxFileData.SaveLikeShortcut();
        }

        private static bool existLikeShortcut(string _fileKey)
        {
            return BoxFileData.STATIC_LikeShortcuts.Where(o => o.Key == _fileKey).FirstOrDefault() != null;
        }
        #endregion
    }
}
