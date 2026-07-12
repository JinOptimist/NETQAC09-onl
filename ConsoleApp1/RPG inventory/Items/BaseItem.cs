using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_inventory.Items
{
    public abstract class BaseItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        protected BaseItem(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
