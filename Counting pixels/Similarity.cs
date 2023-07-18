using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counting_pixels
{
    static class Similarity
    {
        public static bool AreSimilar(Color color1, Color color2, float delta, Illuminant illuminant = Illuminant.D65)
        {
            return (new LAB(color1, illuminant)).DeltaE(new LAB(color2, illuminant)) <= delta;
        }
    }
}
