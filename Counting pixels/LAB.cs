using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Counting_pixels
{
    public class LAB
    {
        private const float e = 0.008856f;
        private const float k = 903.3f;

        public float l { get; set; }
        public float a { get; set; }
        public float b { get; set; }

        public LAB(Color col, Counting_pixels.Illuminant illuminant = Counting_pixels.Illuminant.D65)
        {
            Vector3 lab = XYZtoLAB(new XYZ(col, illuminant));
            l = lab.X;
            a = lab.Y;
            b = lab.Z;
        }
        public LAB(XYZ col)
        {
            Vector3 lab = XYZtoLAB(col);
            l = lab.X;
            a = lab.Y;
            b = lab.Z;
        }

        // эта функция и есть нужная нам метрика, но об этом позже
        public float DeltaE(LAB color)
        {
            return MathF.Sqrt(MathF.Pow((this.l - color.l), 2f) + MathF.Pow((this.a - color.a), 2f) + MathF.Pow((this.b - color.b), 2f));
        }

        private static float ApplyLABconversion(float value)
        {
            if (value > e)
            {
                value = MathF.Pow(value, 1.0f / 3.0f);
            }
            else
            {
                value = (k * value + 16) / 116;
            }
            return value;
        }

        private static Vector3 XYZtoLAB(XYZ col)
        {
            float x = col.x;
            float y = col.y;
            float z = col.z;

            float fx = ApplyLABconversion(x);
            float fy = ApplyLABconversion(y);
            float fz = ApplyLABconversion(z);

            return new Vector3(
                     116.0f * fy - 16.0f,
                     500.0f * (fx - fy),
                     200.0f * (fy - fz)
                 );
        }

    }
}
