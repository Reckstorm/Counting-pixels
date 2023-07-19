using Counting_pixels.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Counting_pixels
{
    static class DrawImage
    {
        public static string path = $@"{(Environment.ProcessPath).Substring(0, Environment.ProcessPath.LastIndexOf('\\') + 1)}";

        public static Bitmap FillOutBitmap(int x, int y)
        {
            Bitmap bitmap = new Bitmap(x, y);
            Random r = new Random();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int k = 0; k < bitmap.Height; k++)
                {

                    Color color = Color.FromArgb(255, r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                    bitmap.SetPixel(i, k, color);
                }
            }
            return bitmap;
        }

        public static void SaveImage(Bitmap bitmap)
        {
            if (File.Exists($"{path}Subject.png")) File.Delete($"{path}Subject.png");
            bitmap.Save($"{path}Subject.png");
        }

        public static void WriteStats(Bitmap bitmap)
        {
            List<MyColorInfo> stats = new List<MyColorInfo>();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    if (stats.Count > 0 && stats.Any(x => x.Coordinates.Any(c => c.x == i && c.y == j))) continue;
                    MyColorInfo tmp = new MyColorInfo() { Sample = bitmap.GetPixel(i, j) };
                    tmp.Coordinates.Add(new Coordinate(i, j));
                    CheckSimilarity(i, j, bitmap, tmp);
                    stats.Add(tmp);
                }
            }
            string str = JsonSerializer.Serialize(stats);
            File.WriteAllText($@"{path}Stats.json", str);
        }

        private static void CheckSimilarity(int x, int y, Bitmap bitmap, MyColorInfo info)
        {
            for (int i = x; i < bitmap.Width; i++)
            {
                for (int j = y + 1; j < bitmap.Height - 1; j++)
                {
                    if (Similarity.AreSimilar(bitmap.GetPixel(x, y), bitmap.GetPixel(i, j), 20))
                    {
                        info.Coordinates.Add(new Coordinate(i, j));
                    }
                }
            }
        }
    }
}
