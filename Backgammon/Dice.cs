using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Dice
    {
        public int FirstDice { get; private set; }
        public int SecondDice { get; private set; }
        public bool RolledDouble { get; private set; }

        Random rnd = new Random();
        public void RollDice()
        {
            FirstDice = rnd.Next(1, 7);
            SecondDice = rnd.Next(1, 7);
        }

        public bool IsDouble()
        {
            return FirstDice == SecondDice;
        }

        public void ResetFirstDice()
        {
            FirstDice = 0;
        }

        public void ResetSecondDice()
        {
            SecondDice = 0;
        }
    }
}
