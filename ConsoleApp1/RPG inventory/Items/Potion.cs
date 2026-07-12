using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_inventory.Items
{
    public class Potion : BaseItem
    {
        public int HealAmount { get; set; }
        public Potion(string name, string description, int healAmount)
            : base(name, description)
        {
        HealAmount = healAmount;
        }
    }
}
