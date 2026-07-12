using System;
using System.Collections.Generic;
using System.Text;
using RPG_inventory.Items;

namespace RPG_inventory.Characters
{
    public class Hero
    {
        public const int MaxInventorySize = 20;
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public List<BaseItem> Inventory { get; set; } // Инвентарь героя может хранить любые предметы, потому что все они наследуются от BaseItem.
        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }
        public BaseItem EquippedAccessory { get; set; } //Бижутерию пока не делал, это заделка на будущее, поэтому пока что от бейс айтема

        public int Attack
        {
            get
            {
                return BaseAttack +
                       (EquippedWeapon == null
                            ? 0
                            : EquippedWeapon.AttackBonus);
            }
        }
        public int Defense
        {
            get
            {
                return BaseDefense +
                       (EquippedArmor == null
                            ? 0
                            : EquippedArmor.DefenseBonus);
            }
        }

        //констрктор героя
        public Hero(string name,
                    int maxHP,
                    int baseAttack,
                    int baseDefense)
        {
            Name = name;
            MaxHP = maxHP;
            HP = maxHP;
            BaseAttack = baseAttack;
            BaseDefense = baseDefense;
            Inventory = new List<BaseItem>();
        }
    }
}
