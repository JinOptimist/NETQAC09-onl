using System;
using System.Collections.Generic;
using System.Text;
using RPG_inventory.Enums;

namespace RPG_inventory.Items
{
    public class Potion : BaseItem
    {
        public int HealAmount { get; set; }
        public Potion(string name, string description, int healAmount)
            : base(name, description, ItemType.Potion)
        {
        HealAmount = healAmount;
        }

        public override BaseItem Clone()
        {
            return new Potion(
                Name,
                Description,
                HealAmount);
        }
    }
}
