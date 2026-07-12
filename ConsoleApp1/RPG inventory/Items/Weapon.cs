using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_inventory.Items
{
    public class Weapon : BaseItem
    {
        public int AttackBonus { get; set; }
        public Weapon(string name, string description, int attackBonus)
            : base(name, description)
        {
            AttackBonus = attackBonus;
        }
    }
}
