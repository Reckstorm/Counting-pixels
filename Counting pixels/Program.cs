using Counting_pixels;
using System.Drawing;

Bitmap bitmap = DrawImage.FillOutBitmap(5, 5);
object locker = new object();

Thread SaveImageThread = new Thread(x =>
{
    lock (locker)
    {
        DrawImage.SaveImage(bitmap);
    }
});

Thread CountStatsThread = new Thread(x =>
{
    lock(locker)
    {
        DrawImage.WriteStats(bitmap);
    }
});

SaveImageThread.Start();
CountStatsThread.Start();