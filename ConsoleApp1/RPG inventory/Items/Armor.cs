using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_inventory.Items
{
    public class Armor : BaseItem
    {
        public int DefenseBonus { get; set; }
        public Armor(string name, string description, int defenseBonus)
            : base(name, description)
        {
            DefenseBonus = defenseBonus;
        }
    }
}
