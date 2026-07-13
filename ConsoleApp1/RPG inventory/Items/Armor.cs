using System;
using System.Collections.Generic;
using System.Text;
using RPG_inventory.Enums;

namespace RPG_inventory.Items
{
    public class Armor : BaseItem
    {
        public int DefenseBonus { get; set; }
        public Armor(string name, string description, int defenseBonus)
            : base(name, description, ItemType.Armor)
        {
            DefenseBonus = defenseBonus;
        }

        public override BaseItem Clone()
        {
            return new Armor(
                Name,
                Description,
                DefenseBonus);
        }
    }
}
