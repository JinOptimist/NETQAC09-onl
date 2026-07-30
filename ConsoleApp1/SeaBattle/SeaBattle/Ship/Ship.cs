using SeaBattleConsole.BoardTest;
using SeaBattleConsole.SeaBattleModels.Cells;

namespace SeaBattleConsole.SeaBattleModels
{
    public class Ship
    {
        public int Size { get; set; }
        public List<ShipCell> ShipParts { get; set; } =  new List<ShipCell>();
        public Ship(int size, CellType playerType)
        {
            Size = size;
            NumberOfLives = size;
            if (playerType != CellType.player1 && playerType != CellType.player2)
            {
                throw new ArgumentException("ShipCell requires player1 or player2");
            }

            var WhoControlsShip = playerType;
        }
        public bool IsSunk{ get; set; }
        public int NumberOfLives{ get; set; }
    }
}

