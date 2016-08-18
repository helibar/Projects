using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public abstract class Player
    {
        public Color Color { get; set; }
        public  PlayerType Type { get; set; }
        public bool IsItMyTurn { get; set; }
       public int CheckersAtHome { get; set; }

        public Player(PlayerType type, Color color)
        {
            Type = type;
            Color = color;
        }

    
        public bool HasAvailableMoves(Board board, Dice dice)
        {
            return GetAvailableMoves(board, dice).ToList().Count + GetAvailableMovesEat(board, dice).ToList().Count > 0 ? true : false;
        }

        public bool HasAvailableMovesToGetOut(Board board, Dice dice)
        {
            return GetAvailableMovesToGetOut(board, dice).ToList().Count > 0 ? true : false;
        }

        public abstract IEnumerable<KeyValuePair<int, int>> GetAvailableMoves(Board board, Dice dice);

        public abstract IEnumerable<KeyValuePair<int, int>> GetAvailableMovesFromBar(Board board, Dice dice);

        public abstract IEnumerable<KeyValuePair<int, int>> GetAvailableMovesEat(Board board, Dice dice);

        public abstract IEnumerable<KeyValuePair<int, int>> GetAvailableMovesEatFromBar(Board board, Dice dice);

        public abstract IEnumerable<KeyValuePair<int, int>> GetAvailableMovesToGetOut(Board board, Dice dice);

        public abstract bool IsLegalPlayerSourceMove(Board board, int index);

        public abstract bool IsLegalPlayerTargetMove(Board board, int fromIndex, int toIndex, int diceResult);

        public abstract bool IsLegalPlayerTargetMoveEat(Board board, int fromIndex, int toIndex, int diceResult);

        public abstract bool IsLegalPlayerGetOutMove(int fromIndex, int diceResult);

        public abstract bool CanCheckersGetOut(Board board);

        public abstract void UpdateCheckersAtHome(Board board);


    }
}
