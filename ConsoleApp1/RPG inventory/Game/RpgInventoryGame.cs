using System;
using System.Collections.Generic;
using System.Text;
using RPG_inventory.Characters;
using RPG_inventory.Items;
using RPG_inventory.Managers;

namespace RPG_inventory.Game
{
    internal class RpgInventoryGame
    {
        private Hero hero;
        private InventoryManager inventoryManager;
        public RpgInventoryGame()
        {
            // Создается менеджер инвентаря и стартовый герой, пока что это статичная заглушка, позже надо переделать на создание в виде метода типа createHero() и стартовых айтемов
            inventoryManager = new InventoryManager();
            hero = new Hero(
                "Воин",
                100, // Максимальное HP
                10,  // Базовая атака
                5    // Базовая защита
            );

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
                        Console.WriteLine("Использование зелий пока не готово");
                        break;

                    case 4:
                        Console.WriteLine("Удаление предметов пока не готово.");
                        break;

                    case 0:
                        Console.WriteLine("Звери – До скорой встречи.mp3");
                        return;

                    default:
                        Console.WriteLine("Такого пункта меню нет.");
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
            Console.WriteLine("4 - Выбросить предмет");
            Console.WriteLine("0 - Выход");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
        }

        //попытка надесь вещь из инвентарю (выбор в меню) - если закоменчено, значит переписал на новый вид
        /*
        private void EquipItemMenu()
        {
            ShowInventory();
            Console.Write("Введите номер предмета для экипировки: ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Некорректный ввод");
                return;
            }
            //Проверяем, существует ли такой номер.
            if (choice < 1 || choice > hero.Inventory.Count)
            {
                Console.WriteLine("Предмет с таким номером отсутствует");
                return;
            }
            //Преобразуем номер пользователя в индекс списка (что бы не считать с 0)
            int index = choice - 1;
            BaseItem item = hero.Inventory[index];
            bool success = inventoryManager.EquipItem(hero, item);
            if (success)
            {
                Console.WriteLine($"Предмет \"{item.Name}\" успешно экипирован");
            }
            else
            {
                Console.WriteLine("Этот предмет нельзя экипировать");
            }
        }*/
        

    }

}
