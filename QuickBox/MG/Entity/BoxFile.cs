using System;
using System.Drawing;

namespace QuickBox.MG.Entity
{
    [Serializable]
    public class BoxFile
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public Bitmap LargeIcon { get; set; }
        public Bitmap SmallIcon { get; set; }
        public string Path { get; set; }
    }
}
