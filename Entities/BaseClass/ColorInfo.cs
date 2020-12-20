using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entities
{
    public class ColorInfo
    {
        public int Posistion { get; set; }
        public int Length { get; set; }
        public Color Color { get; set; }

        public ColorInfo(Match match, Color color)
        {
            Posistion = match.Index;
            Length = match.Length;
            Color = color;
        }

    }
}
