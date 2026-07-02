namespace MazeConsole.MazeModels.Cells;

public class Treasure : BaseCell
{
    private int coinTreasure = 5;
    private int armorTreasure = 2;
    private int healthPotionTreasure = 1;
    public override char MySymbol => 'z';

    public override bool PlayerStepInMe(Player player)
    {
       var treasureType = new Random().Next(1, 4); 

       if (treasureType == 1)
       {
           player.Coin++;
           coinTreasure--;

           if (coinTreasure == 0)
            {
                MazeWhereIWasCreated.ReplaceCellToGround(this);
            }
       }
       else if (treasureType == 2)
       {
           player.Armor++;
           armorTreasure--;
           if (armorTreasure == 0)
           {
               MazeWhereIWasCreated.ReplaceCellToGround(this);
           }
       }
       else
       {
           player.HealthPotion++;
           healthPotionTreasure--;
       

        if (coinTreasure == 0)
            {
            MazeWhereIWasCreated.ReplaceCellToGround(this);
            }
       }
        return true;
    }
}