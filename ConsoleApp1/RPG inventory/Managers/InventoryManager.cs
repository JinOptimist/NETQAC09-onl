using System;
using System.Collections.Generic;
using System.Text;
using RPG_inventory.Characters;
using RPG_inventory.Items;

namespace RPG_inventory.Managers
{
    internal class InventoryManager
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
    }
}
