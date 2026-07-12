namespace MazeConsole.MazeModels.Cells;
//огонь наносит урон игроку и не позволяет ему пройти в эту клетку
public class Fire : BaseCell
{   private const int DAMAGE = 5;
    public override char MySymbol => 'F';
    public override bool PlayerStepInMe(Player player)
    {
        player.CurrentHealth -= DAMAGE;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You stepped into fire. Damage: {DAMAGE}");
        Console.WriteLine("Current health: {player.CurrentHealth}");
        Console.ResetColor();

        if (player.CurrentHealth <= 0)
        {
            Console.WriteLine("You died from fire damage!");
            // Handle player death logic here
        }
        return false;
    }
}