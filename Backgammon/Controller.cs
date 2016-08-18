using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Controller
    {
        public Board BoardGame { get; private set; }
        public Dice DiceGame { get; set; }
        public BlackPlayer BlackPlayer { get; private set; }
        public WhitePlayer WhitePlayer{ get; private set; }
        public int? PlayerSourceTriangleChoice { get; private set; }
        public int MovesLeft { get; private set; }
        public bool RolledDice { get; set; }
        public Controller()
        {
            BoardGame = new Board();
            DiceGame = new Dice();
            BlackPlayer = new BlackPlayer(PlayerType.Human, Color.Black);
            WhitePlayer = new WhitePlayer(PlayerType.Human, Color.White);
            BlackPlayer.IsItMyTurn = true;
        }

    
        public bool PlayerHasAvailableMoves()
        {
            if (BlackPlayer.IsItMyTurn == true)
            {
                return BlackPlayer.HasAvailableMoves(BoardGame, DiceGame);
            }
            else
            {
                return WhitePlayer.HasAvailableMoves(BoardGame, DiceGame);
            }
        }

        public bool PlayerHasAvailableGetOutMoves()
        {
            if (BlackPlayer.IsItMyTurn == true)
            {
                return BlackPlayer.HasAvailableMovesToGetOut(BoardGame, DiceGame);
            }
            else
            {
                return WhitePlayer.HasAvailableMovesToGetOut(BoardGame, DiceGame);
            }
        }

        public bool IsLegalGetOutMove(out int diceUsed)
        {
            diceUsed = DiceGame.FirstDice < DiceGame.SecondDice ? DiceGame.FirstDice : DiceGame.SecondDice;

            if (BlackPlayer.IsItMyTurn == true)
            {
                if (DiceGame.IsDouble() == true)
                {
                    return BlackPlayer.IsLegalPlayerGetOutMove(PlayerSourceTriangleChoice.Value, DiceGame.FirstDice);
                }
                else
                {
                    bool firstDiceLegalMove = BlackPlayer.IsLegalPlayerGetOutMove(PlayerSourceTriangleChoice.Value, DiceGame.FirstDice);
                    bool secondDiceLegalMove = BlackPlayer.IsLegalPlayerGetOutMove(PlayerSourceTriangleChoice.Value, DiceGame.SecondDice);

                    UpdateDiceUsed(ref diceUsed, firstDiceLegalMove, secondDiceLegalMove);

                    return firstDiceLegalMove || secondDiceLegalMove;
                }
            }
            else
            {
                if (DiceGame.IsDouble() == true)
                {
                    return WhitePlayer.IsLegalPlayerGetOutMove(PlayerSourceTriangleChoice.Value, DiceGame.FirstDice);
                }
                else
                {
                    bool firstDiceLegalMove = WhitePlayer.IsLegalPlayerGetOutMove(PlayerSourceTriangleChoice.Value, DiceGame.FirstDice);
                    bool secondDiceLegalMove = WhitePlayer.IsLegalPlayerGetOutMove(PlayerSourceTriangleChoice.Value, DiceGame.SecondDice);

                    UpdateDiceUsed(ref diceUsed, firstDiceLegalMove, secondDiceLegalMove);

                    return firstDiceLegalMove || secondDiceLegalMove;
                }
            }
        }

        private void UpdateDiceUsed(ref int diceUsed, bool firstDiceLegalMove, bool secondDiceLegalMove)
        {
            if (firstDiceLegalMove == true)
            {
                diceUsed = DiceGame.FirstDice;
            }
            if (secondDiceLegalMove == true)
            {
                diceUsed = DiceGame.SecondDice;
            }
            if (firstDiceLegalMove  && secondDiceLegalMove)
            {
                diceUsed = DiceGame.FirstDice < DiceGame.SecondDice ? DiceGame.FirstDice : DiceGame.SecondDice;
            }
        }

        public bool IsLegalTargetMoveEat(int toIndex)
        {
            if (BlackPlayer.IsItMyTurn == true)
            {
                if (DiceGame.IsDouble() == true)
                {
                    return BlackPlayer.IsLegalPlayerTargetMoveEat(BoardGame, PlayerSourceTriangleChoice.Value, toIndex, DiceGame.FirstDice);
                }
                else
                {
                    return BlackPlayer.IsLegalPlayerTargetMoveEat(BoardGame, PlayerSourceTriangleChoice.Value, toIndex, DiceGame.FirstDice) ||
                           BlackPlayer.IsLegalPlayerTargetMoveEat(BoardGame, PlayerSourceTriangleChoice.Value, toIndex, DiceGame.SecondDice);
                }
            }
            else
            {
                if (DiceGame.IsDouble() == true)
                {
                    return WhitePlayer.IsLegalPlayerTargetMoveEat(BoardGame, PlayerSourceTriangleChoice.Value, toIndex, DiceGame.FirstDice);
                }
                else
                {
                    return WhitePlayer.IsLegalPlayerTargetMoveEat(BoardGame, PlayerSourceTriangleChoice.Value, toIndex, DiceGame.FirstDice) ||
                           WhitePlayer.IsLegalPlayerTargetMoveEat(BoardGame, PlayerSourceTriangleChoice.Value, toIndex, DiceGame.SecondDice);
                }
            }
        }
        
        public bool IsLegalTargetMove(int toIndex)
        {
            if (BlackPlayer.IsItMyTurn == true)
            {
                if (DiceGame.IsDouble() == true)
                {
                    return BlackPlayer.IsLegalPlayerTargetMove(BoardGame, PlayerSourceTriangleChoice.Value, toIndex, DiceGame.FirstDice);
                }
                else
                {
                    return BlackPlayer.IsLegalPlayerTargetMove(BoardGame, PlayerSourceTriangleChoice.Value, toIndex, DiceGame.FirstDice) ||
                           BlackPlayer.IsLegalPlayerTargetMove(BoardGame, PlayerSourceTriangleChoice.Value, toIndex, DiceGame.SecondDice);
                }
            }
            else 
            {
                if (DiceGame.IsDouble() == true)
                {
                    return WhitePlayer.IsLegalPlayerTargetMove(BoardGame, PlayerSourceTriangleChoice.Value, toIndex, DiceGame.FirstDice);
                }
                else
                {
                    return WhitePlayer.IsLegalPlayerTargetMove(BoardGame, PlayerSourceTriangleChoice.Value, toIndex, DiceGame.FirstDice) ||
                           WhitePlayer.IsLegalPlayerTargetMove(BoardGame, PlayerSourceTriangleChoice.Value, toIndex, DiceGame.SecondDice);
                }
            }
        }

        public bool IsLegalSourceMove(int index)
        {
            if (BlackPlayer.IsItMyTurn == true)
            {
                return BlackPlayer.IsLegalPlayerSourceMove(BoardGame, index);
            }
            else 
            {
                return WhitePlayer.IsLegalPlayerSourceMove(BoardGame, index);
            }
        }

        public void SetPlayerGetOutMove(int dice)
        {
            ResetAppropriatediceByNumber(dice);

            BoardGame.Triangles[PlayerSourceTriangleChoice.Value].CheckerNum--;
            
            if (BoardGame.Triangles[PlayerSourceTriangleChoice.Value].CheckerNum == 0)
            {
                BoardGame.Triangles[PlayerSourceTriangleChoice.Value].CheckerColor = null;
            }

            PlayerSourceTriangleChoice = null;

            MovesLeft--;
        }

        private void ResetAppropriatediceByNumber(int dice)
        {
            if (DiceGame.IsDouble() == false)
            {
                if (dice == DiceGame.FirstDice)
                {
                    DiceGame.ResetFirstDice();
                }
                if (dice == DiceGame.SecondDice)
                {
                    DiceGame.ResetSecondDice();
                }
            }
        }

        public void SetPlayerSourceMove(int? index)
        {
            PlayerSourceTriangleChoice = index;
        }

        public void SetPlayerTargetMoveEat(int index)
        {
            if (BlackPlayer.IsItMyTurn)
            {
                BoardGame.Bar.AddToBar(Color.White);
            }
            else
            {
                BoardGame.Bar.AddToBar(Color.Black);
            }

            SetPlayerTargetMove(index, true);
        }

        public void SetPlayerTargetMove(int index, bool eaten)
        {
            ResetAppropriatedice(index);

            if (eaten == false) 
            {
                BoardGame.Triangles[index].CheckerNum++;
            }

            if (PlayerSourceTriangleChoice >= 0 && PlayerSourceTriangleChoice <= 23)
            {
                BoardGame.Triangles[PlayerSourceTriangleChoice.Value].CheckerNum--;
            }
            else
            {
                if (BlackPlayer.IsItMyTurn == true)
                {
                    BoardGame.Bar.RemoveFromBar(Color.Black);
                }
                else
                {
                    BoardGame.Bar.RemoveFromBar(Color.White);
                }
            }


            if (PlayerSourceTriangleChoice >= 0 && PlayerSourceTriangleChoice <= 23)
            {
                if (BoardGame.Triangles[PlayerSourceTriangleChoice.Value].CheckerNum == 0)
                {
                    BoardGame.Triangles[PlayerSourceTriangleChoice.Value].CheckerColor = null;
                }
            }

            if (BoardGame.Triangles[index].CheckerNum == 1)
            {
                BoardGame.Triangles[index].CheckerColor = BlackPlayer.IsItMyTurn ? BlackPlayer.Color : WhitePlayer.Color;
            }

            PlayerSourceTriangleChoice = null;

            MovesLeft--;
        }

        private void ResetAppropriatedice(int toIndex)
        {
            if (DiceGame.IsDouble() == false)
            {
                if (WhitePlayer.IsItMyTurn)
                {
                    if (PlayerSourceTriangleChoice - toIndex == DiceGame.FirstDice)
                    {
                        DiceGame.ResetFirstDice();
                    }
                    else
                    {
                        DiceGame.ResetSecondDice();
                    }
                }
                else
                {
                    if (toIndex - PlayerSourceTriangleChoice == DiceGame.FirstDice)
                    {
                        DiceGame.ResetFirstDice();
                    }
                    else
                    {
                        DiceGame.ResetSecondDice();
                    }
                }
            }
        }

        public void SwapTurns()
        {
            BlackPlayer.IsItMyTurn = !BlackPlayer.IsItMyTurn;
            WhitePlayer.IsItMyTurn = !WhitePlayer.IsItMyTurn;
        }

        public void RollDice()
        {
            DiceGame.RollDice();

            RolledDice = true;
        }

        public void UpdateMovesLeft()
        {
            if (DiceGame.IsDouble())
            {
                MovesLeft = 4;
            }
            else
            {
                MovesLeft = 2;
            }
        }

        public void ResetMovesLeft()
        {
            MovesLeft = 0;
        }
    }
}
