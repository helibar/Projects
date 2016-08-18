using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backgammon;

namespace Backgammon_Game
{
    class Display
    {
        public void DisplayBoard(Board b)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WindowWidth = 100;

            for (int i = 0; i < 24; i++)
            {
                Console.Write(String.Format("|{0,2}", i));
            }
         
            Console.Write("|\n");
            



            int index = 0;
            foreach (var triangle in b.Triangles)
            {

                if (triangle.CheckerColor == Color.White)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (triangle.CheckerColor == Color.Black)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.Write(String.Format("|{0,2}", b.Triangles[index].CheckerNum));
                index++;
                
            }
            Console.Write("|\n");
            

        }


    }
}
