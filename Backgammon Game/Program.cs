using Backgammon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon_Gam
{
    class Program
    {
        static void Main(string[] args)
        {
            Display display = new Display();
            Controller controllerGame = new Controller();
            ConsoleColor BlackText = Console.ForegroundColor;
            ConsoleColor WhiteText = Console.ForegroundColor;

            display.DisplayBoard(controllerGame.BoardGame);
            Console.WriteLine("Black player turn");
            Console.WriteLine("From where would you like to move:");
            int from = Int32.Parse(Console.ReadLine());
            Console.WriteLine("To where would you like to move:");
            int to = Int32.Parse(Console.ReadLine());


            controllerGame.IsLegalSourceMove(from);

            Console.ReadLine();
        }

     
    }
}
