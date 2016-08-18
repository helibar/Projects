using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class BlackPlayer : Player
    {
        public BlackPlayer(PlayerType type, Color color) : base(type, color)
        {
        }

        public override IEnumerable<KeyValuePair<int, int>> GetAvailableMoves(Board mainBoard, Dice diceGame)
        {
            if (mainBoard.Bar.CountBlackInBar > 0)
            {
                return GetAvailableMovesFromBar(mainBoard, diceGame);
            }


            List<KeyValuePair<int, int>> AvailableMoves = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < mainBoard.Triangles.Count; i++)
            {
                AddAvilableMovesOfTriangle(mainBoard, diceGame, i, AvailableMoves);
            }

            return AvailableMoves;

        }

        private void AddAvilableMovesOfTriangle(Board mainBoard, Dice diceGame, int i, List<KeyValuePair<int, int>> AvailableMoves)
        {

            if (!diceGame.IsDouble())
            {
                AddAvilableMovesForDice(mainBoard, diceGame, i, AvailableMoves, diceGame.FirstDice);
            }

            AddAvilableMovesForDice(mainBoard, diceGame, i, AvailableMoves, diceGame.SecondDice);
        }

        private void AddAvilableMovesForDice(Board mainBoard, Dice diceGame, int i, List<KeyValuePair<int, int>> AvailableMoves, int diceResult)
        {
            if (i + diceResult <= 23 && diceResult != 0)
            {
                AddIfMoveLegal(mainBoard, i, AvailableMoves, diceResult);
            }
        }

        private void AddIfMoveLegal(Board mainBoard, int i, List<KeyValuePair<int, int>> AvailableMoves, int diceResult)
        {
            if (IsLegalPlayerSourceMove(mainBoard, i) && 
                IsLegalPlayerTargetMove(mainBoard, i, i + diceResult, diceResult))
            {
                AvailableMoves.Add(new KeyValuePair<int, int>(i, i + diceResult));
            }
        }
        public override IEnumerable<KeyValuePair<int, int>> GetAvailableMovesFromBar(Board mainBoard, Dice diceGame)
        {
            List<KeyValuePair<int, int>> AvailableMovesFromBar = new List<KeyValuePair<int, int>>();

            AddMoveIfAvailable(mainBoard,  AvailableMovesFromBar, diceGame.FirstDice);

            AddMoveIfAvailable(mainBoard, AvailableMovesFromBar,diceGame.SecondDice);

            return AvailableMovesFromBar;
        }

        private void AddMoveIfAvailable(Board mainBoard, List<KeyValuePair<int, int>> AvailableMoves,int diceResult)
        {
            if (diceResult != 0)
            {
                AddIfMoveLegalFromBar(mainBoard, AvailableMoves, diceResult);
            }
        }

        private void AddIfMoveLegalFromBar(Board mainBoard, List<KeyValuePair<int, int>> AvailableMoves,int diceResult)
        {
            if (IsLegalPlayerTargetMove(mainBoard, -1, -1 + diceResult, diceResult))
            {
                AvailableMoves.Add(new KeyValuePair<int, int>(-1, -1 + diceResult));
            }
        }

        public override IEnumerable<KeyValuePair<int, int>> GetAvailableMovesEat(Board mainBoard, Dice diceGame)
        {
            if (mainBoard.Bar.CountBlackInBar > 0)
            {
                return GetAvailableMovesFromBar(mainBoard, diceGame);
            }


            List<KeyValuePair<int, int>> AvailableMovesEat = new List<KeyValuePair<int, int>>();

                for (int i = 0; i < mainBoard.Triangles.Count; i++)
                {
                    AddAvilableMovesOfTriangleEat(mainBoard, diceGame, i, AvailableMovesEat);
                }

                return AvailableMovesEat;
        }

        private void AddAvilableMovesOfTriangleEat(Board mainBoard, Dice diceGame, int i, List<KeyValuePair<int, int>> AvailableMovesEat)
        {
            if (!diceGame.IsDouble())
            {
                AddAvilableMovesForDiceEat(mainBoard, i, AvailableMovesEat, diceGame.FirstDice);
            }

            AddAvilableMovesForDiceEat(mainBoard, i, AvailableMovesEat, diceGame.SecondDice);
        }

        private void AddAvilableMovesForDiceEat(Board mainBoard, int i, List<KeyValuePair<int, int>> AvailableMovesEat, int diceResult)
        {
            if (i + diceResult <= 23 && diceResult != 0)
            {
                AddIfMoveLegalToEat(mainBoard, i, AvailableMovesEat, diceResult);
            }
        }

        private void AddIfMoveLegalToEat(Board mainBoard, int i, List<KeyValuePair<int, int>> AvailableMovesEat, int diceResult)
        {
            if (IsLegalPlayerSourceMove(mainBoard, i) &&
                IsLegalPlayerTargetMoveEat(mainBoard, i, i + diceResult, diceResult))
            {
                AvailableMovesEat.Add(new KeyValuePair<int, int>(i, i + diceResult));
            }
        }

        public override IEnumerable<KeyValuePair<int, int>> GetAvailableMovesEatFromBar(Board mainBoard, Dice diceGame)
        {
            List<KeyValuePair<int, int>> AvailableMovesEatFromBar = new List<KeyValuePair<int, int>>();

            AddMoveIfAvailableEatFromBar(mainBoard, AvailableMovesEatFromBar, diceGame.FirstDice);
            AddMoveIfAvailableEatFromBar(mainBoard,AvailableMovesEatFromBar, diceGame.SecondDice);

            return AvailableMovesEatFromBar;
        }
        private void AddMoveIfAvailableEatFromBar(Board mainBoard,  List<KeyValuePair<int, int>> AvailableMoves, int diceResult)
        {
            if (diceResult != 0)
            {
                AddIfMoveLegalToEatFromBar(mainBoard, AvailableMoves, diceResult);
            }
        }
        private void AddIfMoveLegalToEatFromBar(Board mainBoard, List<KeyValuePair<int, int>> AvailableMoves, int diceResult)
        {
            if (IsLegalPlayerTargetMoveEat(mainBoard, -1, -1 + diceResult, diceResult))
            {
                AvailableMoves.Add(new KeyValuePair<int, int>(-1, -1 + diceResult));
            }
        }

        public override IEnumerable<KeyValuePair<int, int>> GetAvailableMovesToGetOut(Board mainBoard, Dice diceGame)
        {
            List<KeyValuePair<int, int>> AvailableMoves = new List<KeyValuePair<int, int>>();

            for (int i = 18; i <= 23; i++)
            {
                AddAvilableMovesOfTriangleToGetOut(mainBoard, diceGame, i, AvailableMoves);
            }

            return AvailableMoves;
        }

        private void AddAvilableMovesOfTriangleToGetOut(Board mainBoard, Dice diceGame, int i, List<KeyValuePair<int, int>> AvailableMoves)
        {
            if (!diceGame.IsDouble())
            {
                AddMoveIfAvailableToGetOut(mainBoard, diceGame, i, AvailableMoves, diceGame.FirstDice);
            }

            AddMoveIfAvailableToGetOut(mainBoard, diceGame, i, AvailableMoves, diceGame.SecondDice);
        }

        private void AddMoveIfAvailableToGetOut(Board mainBoard, Dice diceGame, int i, List<KeyValuePair<int, int>> AvailableMoves, int diceResult)
        {
            if (diceResult != 0)
            {
                AddIfMoveLegalToGoOut(mainBoard, diceGame, i, AvailableMoves, diceResult);
            }
        }

        private void AddIfMoveLegalToGoOut(Board mainBoard, Dice diceGame, int i, List<KeyValuePair<int, int>> AvailableMoves,int diceResult)
        {
            if (IsLegalPlayerSourceMove(mainBoard, i) && IsLegalPlayerGetOutMove(i, diceResult))
            {
                AvailableMoves.Add(new KeyValuePair<int, int>(i, diceResult));
            }
        }

        public override bool IsLegalPlayerSourceMove(Board mainBoard, int index)
        {
            if (mainBoard.Triangles[index].CheckerColor == Color.Black && mainBoard.Bar.CountBlackInBar == 0)
            {
                return true;
            }

            return false;
        }

        public override bool IsLegalPlayerTargetMove(Board mainBoard, int sourceIndex, int tergetIndex, int diceResult)
        {
            if (tergetIndex - sourceIndex == diceResult)
            {
                if (mainBoard.Triangles[tergetIndex].CheckerColor == null || mainBoard.Triangles[tergetIndex].CheckerColor == Color.Black) 
                {
                    return true;
                }
            }

            return false;
        }

        public override bool IsLegalPlayerTargetMoveEat(Board mainBoard, int sourceIndex, int tergetIndex, int diceResult)
        {
            if (tergetIndex - sourceIndex == diceResult)
            {
                if (mainBoard.Triangles[tergetIndex].CheckerColor == Color.Black && mainBoard.Triangles[tergetIndex].CheckerNum == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public override bool IsLegalPlayerGetOutMove(int sourceIndex, int diceResult)
        {
            return sourceIndex + diceResult >= 24;
        }

        public override bool CanCheckersGetOut(Board mainBoard)
        {
            int CheckerNumOutsideHome = mainBoard.Bar.CountBlackInBar;

            for (int i = 0; i <= 17; i++)
            {
                if (mainBoard.Triangles[i].CheckerColor == Color.Black)
                {
                    CheckerNumOutsideHome += mainBoard.Triangles[i].CheckerNum;
                }
            }

            return CheckerNumOutsideHome == 0;
        }

        public override void UpdateCheckersAtHome(Board mainBoard)
        {
            CheckersAtHome = 0;

            for (int i = 18; i <= 23; i++)
            {
                if (mainBoard.Triangles[i].CheckerColor == Color.Black)
                {
                    CheckersAtHome += mainBoard.Triangles[i].CheckerNum;
                }
            }
        }


    }
}
