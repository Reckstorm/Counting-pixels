using Counting_pixels.Models;
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

        private static float LinearToSRGB(float channel) => channel / 255;

        public static ContainerSRGB LinearToSRGB(Color col)
        {
            return
                new ContainerSRGB(
                    LinearToSRGB(col.R),
                    LinearToSRGB(col.G),
                    LinearToSRGB(col.B)
                );
        }

        public XYZ(Color col, Counting_pixels.Illuminant illuminant = Illuminant.D65)
        {
            ContainerSRGB sRGB = LinearToSRGB(col);

            float r = sRGB.r;
            float g = sRGB.g;
            float b = sRGB.b;

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
