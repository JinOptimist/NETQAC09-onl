using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_inventory.Characters
{
        public class Monster
        {
            public string Name { get; set; }
            public int HP { get; set; }
            public int Attack { get; set; }
            public Monster(string name, int hp, int attack)
            {
                Name = name;
                HP = hp;
                Attack = attack;
            }
        public Monster Clone()//клон нужен, что бы позже, когда (если) буду делать реальнный бой, то с ним гораздо проще уменьшать хп монстра
        {
            return new Monster(Name, HP, Attack);
        }

    }
 }


