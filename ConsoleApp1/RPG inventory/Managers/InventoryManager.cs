using System;
using System.Collections.Generic;
using System.Text;
using RPG_inventory.Characters;
using RPG_inventory.Items;

namespace RPG_inventory.Managers
{
     class InventoryManager
    {
        public bool AddItem(Hero hero, BaseItem item) //добавление предмета в инвентарь
        {
            if (hero.Inventory.Count >= Hero.MaxInventorySize)
            {
                return false;
            }

            hero.Inventory.Add(item);

            return true;
        }

        public bool RemoveItem(Hero hero, BaseItem item) //Удалаение предмета из инвентаря
        {
            return hero.Inventory.Remove(item);
        }

        //Честно говоря мне не нравится реализация этого метода, как то слишком костыльно что-ли, особенно если типов вещей будет больше, может попозже подумаю как переделать
        public bool EquipItem(Hero hero, BaseItem item)//Надеть предмет
        {
            if (item is Weapon weapon)
            {
                hero.EquippedWeapon = weapon;
                return true;
            }

            if (item is Armor armor)
            {
                hero.EquippedArmor = armor;
                return true;
            }

            return false;
        }

        public bool UsePotion(Hero hero, BaseItem item)//Использование зелья
        {
            if (item is Potion potion)
            {
                hero.HP += potion.HealAmount;

                if (hero.HP > hero.MaxHP)
                {
                    hero.HP = hero.MaxHP;
                }

                RemoveItem(hero, item);

                return true;
            }

            return false;
        }


    }
}
