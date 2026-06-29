using MazeConsole.MazeModels;

namespace MazeConsole.MazeModels.Cells;

public class IlyaCell : BaseCell
{
	public override char MySymbol => 'I';

	public override bool PlayerStepInMe(Player player)
	{
		Console.WriteLine("You found the Ilya Cell! +3 coins.");
		player.Coin += 3;

		return true;
	}
}