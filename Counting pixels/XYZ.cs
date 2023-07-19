using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Counting_pixels
{
    public enum Illuminant
    {
        D50,
        D65
    }
    public class XYZ
    {
        // точки самого яркого цвета - белого в обоих стандартах
        private static readonly Vector3 D50 = new Vector3(0.966797f, 1.0f, 0.825188f);
        private static readonly Vector3 D65 = new Vector3(0.95047f, 1.0f, 1.0883f);

        // цветовые компоненты
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        //private static float LinearToSRGB(float channel)
        //{
        //    if (channel > 0.0031308f)
        //    {
        //        channel = 1.055f * MathF.Pow(channel, 1 / 2.4f) - 0.055f;
        //    }
        //    else
        //    {
        //        channel *= 12.92f;
        //    }
        //    return channel;
        //}

        //public static Color LinearToSRGB(Color col)
        //{
        //    Color rgbColor = Color.FromScRgb(
        //            LinearToSRGB(col.R),
        //            LinearToSRGB(col.G),
        //            LinearToSRGB(col.B));
        //    return
        //        new Color(
                    
        //        );
        //}

        public XYZ(Color col, Counting_pixels.Illuminant illuminant = Illuminant.D65)
        {
            float r = col.R;
            float g = col.G;
            float b = col.B;

            // в зависимости от стандарта выбираем матрицу и самую яркую точку
            // перемножаем матрицы "вручную", как было описано выше
            // после чего нормализуем значения всех компонент цвета
            switch (illuminant)
            {
                case Counting_pixels.Illuminant.D50:
                    // sRGB -> XYZ
                    x = 0.4360747f * r + 0.3850649f * g + 0.1430804f * b;
                    y = 0.2225045f * r + 0.7168786f * g + 0.0606169f * b;
                    z = 0.0139322f * r + 0.0971045f * g + 0.7141733f * b;

                    float D50x = D50.X;
                    float D50y = D50.Y;
                    float D50z = D50.Z;

                    // Clamping to D50 white point & normalizing them afterwards
                    x = Math.Clamp(x, 0f, D50x) / D50x;
                    y = Math.Clamp(y, 0f, D50y) / D50y;
                    z = Math.Clamp(z, 0f, D50z) / D50z;
                    break;
                case Counting_pixels.Illuminant.D65:
                    // sRGB -> XYZ
                    x = 0.4124564f * r + 0.3575761f * g + 0.1804375f * b;
                    y = 0.2126729f * r + 0.7151522f * g + 0.0721750f * b;
                    z = 0.0193339f * r + 0.1191920f * g + 0.9503041f * b;

                    float D65x = D65.X;
                    float D65y = D65.Y;
                    float D65z = D65.Z;

                    // Clamping to D65 white point & normalizing them afterwards
                    x = Math.Clamp(x, 0f, D65x) / D65x;
                    y = Math.Clamp(y, 0f, D65y) / D65y;
                    z = Math.Clamp(z, 0f, D65z) / D65z;
                    break;
            }
        }
    }
}
