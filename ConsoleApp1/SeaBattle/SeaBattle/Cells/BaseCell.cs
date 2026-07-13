using SeaBattleConsole.BoardTest;
namespace SeaBattleConsole.SeaBattleModels.Cells
{



    public abstract class BaseCell 
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Board BoardWhereIWasCreated { get; set; }
        public bool IsAvailable { get; set; }

        public CellType Type { get; set; }
        public HitType SecondType { get; set; }

        public abstract char MySymbol { get; }
        public string GetMyPosition()
        {
            return $"[{X}, {Y}]";
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }
        public abstract HitType PlayerShootInMe(int x, int y, int playerNumber);
    }
}