using System;
using System.Collections.Generic;
using System.Text;
using RPG_inventory.Enums;

namespace RPG_inventory.Items
{
    public class Weapon : BaseItem
    {
        public int AttackBonus { get; set; }
        public Weapon(string name, string description, int attackBonus)
            : base(name, description, ItemType.Weapon)
        {
            AttackBonus = attackBonus;
        }
        public override BaseItem Clone()
        {
            return new Weapon(
                Name,
                Description,
                AttackBonus);
        }
    }
}
