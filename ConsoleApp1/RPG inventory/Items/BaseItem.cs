using System;
using System.Collections.Generic;
using System.Text;
using RPG_inventory.Enums;

namespace RPG_inventory.Items
{
    public abstract class BaseItem
    {   
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType ItemType { get; }
        protected BaseItem(string name, string description, ItemType itemType)
        {
            Name = name;
            Description = description;
            ItemType = itemType;
        }
        public abstract BaseItem Clone();
    }
}
