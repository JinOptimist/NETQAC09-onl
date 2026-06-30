namespace MazeConsole.MazeModels.Cells
{
    public class Rainbow : BaseCell
    {
        // Буква, которой радуга будет обозначаться в лабиринте
        public override char MySymbol => 'R';

        // Логика шага: когда игрок наступает на радугу, он просто проходит сквозь неё 1
        public override bool PlayerStepInMe(Player player)
        {
            return true;
        }
    }
}