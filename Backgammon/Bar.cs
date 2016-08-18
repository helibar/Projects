using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
  public class Bar
  {
      public int CountBlackInBar { get; private set; }
      public int CountWhiteInBar { get; private set; }

        public void AddToBar(Color color)
      {
            if (color == Color.Black)
            {
                CountBlackInBar++;
            }
            CountWhiteInBar++;
      }

        public void RemoveFromBar(Color color)
        {
            if (color == Color.Black)
            {
                CountBlackInBar--;
            }
            CountWhiteInBar--;
        }
    }
}
