using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counting_pixels
{
    static class DrawImage
    {
        private static int width = 20;
        private static int height = 20;
        public static string path = $@"{(Environment.ProcessPath).Substring(0, Environment.ProcessPath.LastIndexOf('\\') + 1)}";
        public static void DrawAndSaveImage()
        {
            Bitmap bitmap = new Bitmap(width, height);
            Random r = new Random();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int k = 0; k < bitmap.Height; k++)
                {

                    Color color = Color.FromArgb(255, r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                    bitmap.SetPixel(i, k, color);
                }
            }
            path = $"{path}Subject.png";
            if (File.Exists(path)) File.Delete(path);
            bitmap.Save(path);
        }

        static void WriteStatistics(Bitmap bitmap)
        {
            Bitmap tmp = new Bitmap(bitmap);
            List<string> strings = new List<string>();

        }

        static void CheckSimilarity(Color c)
        {
            Bitmap tmp = new Bitmap(bitmap);
            for (int i = 0; i < tmp.Width; i++)
            {
                for (int j = 0; j < tmp.Height; j++)
                {

                }
            }
        }
    }
}
