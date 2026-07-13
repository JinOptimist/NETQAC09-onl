using System;
using System.Collections.Generic;
using System.Text;
using RPG_inventory.Characters;
using RPG_inventory.Enums;
using RPG_inventory.Items;
using RPG_inventory.Managers;

namespace RPG_inventory.Game
{
    internal class RpgInventoryGame
    {
        private Hero hero;
        private InventoryManager inventoryManager;
        private List<Monster> monsters;
        private Random random;
        private List<BaseItem> possibleLoot;
        public RpgInventoryGame() //это конструктор всей игры
        {
            random = new Random();
            // Создается менеджер инвентаря и стартовый герой, пока что это статичная заглушка, позже надо переделать на создание в виде метода типа createHero() и стартовых айтемов
            inventoryManager = new InventoryManager();
            
            hero = new Hero(
                "Воин",
                100, // Максимальное HP
                10,  // Базовая атака
                5    // Базовая защита
            );

            //список возможныых монстров
            monsters = new List<Monster>()
    {
        new Monster("Орк", 18, 8),
        new Monster("Скелет", 10, 4),
        new Monster("Волк", 8, 3),
        new Monster("Тролль", 25, 10),
        new Monster("Призрак", 9, 11)
    };
            possibleLoot = new List<BaseItem>()
{
    new Weapon("Стальной меч", "Прочный меч", 7),

    new Weapon("Боевой топор", "Тяжелый топор", 9),

    new Armor("Железная броня", "Крепкая броня", 5),

    new Armor("Щит", "Усиливает защиту", 4),

    new Potion("Большое зелье", "Восстанавливает 50 HP", 50),

    new Potion("Малое зелье", "Восстанавливает 20 HP", 20)
};

            //Создаем ему стартовый меч, броню и зелье
            Weapon ironSword = new Weapon(
                "Железный меч",
                "Обычный железный меч",
                5
            );
            Armor leatherArmor = new Armor(
                "Кожаная броня",
                "Легкая кожаная броня",
                3
            );
            Potion healthPotion = new Potion(
                "Зелье лечения",
                "Восстанавливает 30 HP",
                30
            );
            //Добавляем предметы в инвентарь.
            inventoryManager.AddItem(hero, ironSword);
            inventoryManager.AddItem(hero, leatherArmor);
            inventoryManager.AddItem(hero, healthPotion);

            //Экипируем оружие и броню
            inventoryManager.EquipItem(hero, ironSword);
            inventoryManager.EquipItem(hero, leatherArmor);
        }
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowHeroInfo();
                ShowMenu();
                Console.Write("Ваш выбор: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine();
                    Console.WriteLine("Некорректный ввод! Нажмите любую клавишу");
                    Console.ReadKey();
                    continue;
                }

                //Выполняем действие в зависимости от выбора пользователя
                switch (choice)
                {
                    case 1:
                        ShowInventory();
                        break;

                    case 2:
                        EquipItemMenu();
                        break;

                    case 3:
                        UsePotionMenu();
                        break;

                    case 4:
                        DropItemMenu();
                        break;

                    case 5:
                        FightMonster();
                        break;

                    case 0:
                        Console.WriteLine("Звери – До скорой встречи.mp3");
                        return;

                    default:
                        Console.WriteLine("Такого пункта меню нет");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу для продолжения");
                Console.ReadKey();
            }
        }

        private void ShowHeroInfo() //отображение характеристик героя
        {
            Console.WriteLine($"Герой: {hero.Name}");
            Console.WriteLine();
            Console.WriteLine($"HP: {hero.HP}/{hero.MaxHP}");
            Console.WriteLine($"Атака: {hero.Attack}");
            Console.WriteLine($"Защита: {hero.Defense}");
            Console.WriteLine();
        }

        //отображение инвентаря
        //TODO тут конечно тоже надо бы это сделать как то поумнее, если типов предметов будет больше, то плодить if else какой-то кринж
        private void ShowInventory()
        {
            Console.WriteLine("Инвентарь:");
            Console.WriteLine();

            int index = 1;

            foreach (BaseItem item in hero.Inventory)
            {
                if (item is Weapon weapon)
                {
                    Console.WriteLine($"{index}. {weapon.Name} (+{weapon.AttackBonus} к атаке)");
                }
                else if (item is Armor armor)
                {
                    Console.WriteLine($"{index}. {armor.Name} (+{armor.DefenseBonus} к защите)");
                }
                else if (item is Potion potion)
                {
                    Console.WriteLine($"{index}. {potion.Name} (+{potion.HealAmount} HP)");
                }

                index++;
            }

            Console.WriteLine();
        }

        //само меню
        private void ShowMenu()
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("1 - Показать инвентарь");
            Console.WriteLine("2 - Экипировать предмет");
            Console.WriteLine("3 - Использовать зелье");
            Console.WriteLine("5 - Сразиться с монстром");
            Console.WriteLine("4 - Выбросить предмет");
            Console.WriteLine("0 - Выход");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
        }

        //выбор предмета из инвентаря в меню, v1
        /*
        private BaseItem? SelectItemFromInventory()
        {

            ShowInventory();
            Console.Write("Введите номер предмета: ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int choice))   
            {
                Console.WriteLine("Некорректный ввод.");
                return null;
            }
            // Проверяем, существует ли такой предмет
            if (choice < 1 || choice > hero.Inventory.Count)
            {
                Console.WriteLine("Предмет с таким номером отсутствует.");
                return null;
            }
            // Преобразуем номер пользователя в индекс списка (что бы не считать с 0)
            int index = choice - 1;
            return hero.Inventory[index];
        }*/



        //выбор предмета из инвентаря в меню,v2
        private BaseItem? SelectItemFromInventory(params ItemType[] allowedTypes)
        {
            //Создаем список предметов, который будем показывать пользователю.
            List<BaseItem> itemsToShow;
            //Если типы не переданы, показываем весь инвентарь
            if (allowedTypes.Length == 0)
            {
                itemsToShow = hero.Inventory;
            }
            else
            {
                //Оставляем только предметы нужных типов
                itemsToShow = hero.Inventory
                    .Where(item => allowedTypes.Contains(item.ItemType))
                    .ToList();
            }
            //Проверяем, есть ли вообще подходящие предметы
            if (itemsToShow.Count == 0)
            {
                Console.WriteLine("Нет подходящих предметов");
                return null;
            }
            //Показываем список предметов
            Console.WriteLine("\nИнвентарь:");

            for (int i = 0; i < itemsToShow.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {itemsToShow[i].Name}");
            }
            Console.Write("\nВведите номер предмета: ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Некорректный ввод");
                return null;
            }
            if (choice < 1 || choice > itemsToShow.Count)
            {
                Console.WriteLine("Такого предмета нет");
                return null;
            }
            return itemsToShow[choice - 1];
        }


        //надеть вещь из инвентаря
        private void EquipItemMenu()
        {
            //Показываем только предметы, которые можно экипировать
            BaseItem? item = SelectItemFromInventory(
        ItemType.Weapon,
        ItemType.Armor,
        ItemType.Accessory
        );
            if (item == null)
            {
                return;
            }
            bool success = inventoryManager.EquipItem(hero, item);// Пытаемся экипировать предмет
            if (success)
            {
                Console.WriteLine($"Предмет \"{item.Name}\" успешно экипирован.");
            }
            else
            {
                Console.WriteLine("Этот предмет нельзя экипировать.");
            }
        }

        //использование зелья
        private void UsePotionMenu()
        {
            //показываются только зелья
            BaseItem? item = SelectItemFromInventory(ItemType.Potion);
            if (item == null)
            {
                return;
            }
            bool success = inventoryManager.UsePotion(hero, item);
            if (success)
            {
                Console.WriteLine($"Зелье \"{item.Name}\" использовано.");
            }
            else
            {
                Console.WriteLine("Выбранный предмет не является зельем.");
            }
        }

        //Выбросить предмет из инвентаря
        private void DropItemMenu()
        {
            BaseItem? item = SelectItemFromInventory();
            if (item == null)
            {
                return;
            }
            bool success = inventoryManager.RemoveItem(hero, item);
            if (success)
            {
                Console.WriteLine($"Предмет \"{item.Name}\" выброшен");
            }
            else
            {
                Console.WriteLine("Не удалось выбросить предмет");
            }
        }

        private Monster CreateRandomMonster()//выбор(создание) монстра из списка
        {
            Monster original = monsters[random.Next(monsters.Count)];

            return original.Clone();
        }

        private void FightMonster() //монстр, TODO когда нибудь потом бы все это тоже раскидать по отдельным методам
        {
            Monster monster = CreateRandomMonster();
            Console.WriteLine($"\nНа вас напал {monster.Name}!");
            Console.WriteLine($"HP монстра: {monster.HP}");
            Console.WriteLine($"Атака монстра: {monster.Attack}");
            Console.WriteLine($"\nВаша атака: {hero.Attack}");
            if (hero.Attack > monster.HP)
            {
                Console.WriteLine($"\n{monster.Name} побежден!");
                BaseItem reward = CreateRandomLoot();
                inventoryManager.AddItem(hero, reward);
                Console.WriteLine($"Получен предмет: {reward.Name}");
                return;
            }
            hero.HP -= monster.Attack;
            if (hero.HP < 0)
            {
                hero.HP = 0;
            }
            Console.WriteLine($"\n{monster.Name} оказался сильнее!");
            Console.WriteLine($"Вы получили {monster.Attack} урона.");
            Console.WriteLine($"Текущее HP: {hero.HP}/{hero.MaxHP}");
        }

        private BaseItem CreateRandomLoot()
        {
            BaseItem original =
                possibleLoot[random.Next(possibleLoot.Count)];

            return original.Clone();
        }
    }
}
