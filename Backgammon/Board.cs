using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
   public class Board
    {
        public int BlackCheckers;
        public int WhiteCheckers;

       public List<Triangle> Triangles { get; private set; }
       public Bar Bar { get; private set; }

       public Board()
       {
            Triangles = new List<Triangle>(new Triangle[24]);
            Bar=new Bar();
            SetNewBoard();
          
        }

    
        public void RemoveCheakerFromTriangle(int triangleNumber)
        {
            var myTriangle = Triangles[triangleNumber];
            if (myTriangle.CheckerNum == 1)
            {
                myTriangle.CheckerNum = 0;
                myTriangle.CheckerColor = null;
            }

            else if (myTriangle.CheckerNum > 1)
            {
                myTriangle.CheckerNum -= 1;
            }
            else
            {
                Console.WriteLine("No checkers to remove");
            }
        }

        public void SetNewBoard()
       {
            for (int i = 0; i < 24; i++)
            {
                switch (i)
                {
                    case 0:
                        Triangles[i] = new Triangle(2, Color.Black);
                        break;

                    case 5:
                        Triangles[i] = new Triangle(5, Color.White);
                        break;

                    case 7:
                        Triangles[i] = new Triangle(3, Color.White);
                        break;

                    case 11:
                        Triangles[i] = new Triangle(5, Color.Black);
                        break;

                    case 12:
                        Triangles[i] = new Triangle(5, Color.White);
                        break;

                    case 16:
                        Triangles[i] = new Triangle(3, Color.Black);
                        break;

                    case 18:
                        Triangles[i] = new Triangle(5, Color.Black);
                        break;

                    case 23:
                        Triangles[i] = new Triangle(2, Color.White);
                        break;

                    default:
                        Triangles[i] = new Triangle();
                        break;
                }
            }
        }

      



   
    }

}
