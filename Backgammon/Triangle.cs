using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Triangle
    {
        public int CheckerNum { get; set; }
        public Color? CheckerColor { get; set; }

        public Triangle(int checkerNum, Color color)
        {
            CheckerNum = checkerNum;
            CheckerColor = color;
        }

        public Triangle()
        {
            
        }
}
}