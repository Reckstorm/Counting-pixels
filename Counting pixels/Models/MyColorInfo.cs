using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counting_pixels.Models
{
    public class MyColorInfo
    {
        public Color Sample { get; set; }
        public List<Coordinate> Coordinates { get; set; }

        public MyColorInfo()
        {
            Coordinates = new List<Coordinate>();
        }
    }
}
