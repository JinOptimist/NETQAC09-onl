// работа с записями и расписанием

using System.Diagnostics;

namespace AppointmentBooking;

public class AppointmentApp
{
    private AppointmentBook _appointmentBook;
    public AppointmentApp()
    {
        _appointmentBook = new AppointmentBook();
    }

    public void Run() //выбор в меню для клиента
    {
        while (true)
        {
            ShowMenu();
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ShowSchedule();
                    break;
                case "2":
                    ShowFreeSlots();
                    break;
                case "3":
                    BookClient();
                    break;
                case "4":
                    CancelAppointment();
                    break;
                case "5":
                    FindByClientName();
                    break;
                default:
                    Console.WriteLine("Такого пункта меню нет");
                    break;
            }

            Console.WriteLine();
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("1. Показать расписание");
        Console.WriteLine("2. Показать свободные слоты");
        Console.WriteLine("3. Записать клиента");
        Console.WriteLine("4. Отменить запись");
        Console.WriteLine("5. Найти запись по имени");
        Console.WriteLine("0. Выход");
        Console.Write("> ");
    }

    private void ShowSchedule() // выполнение требования показать расписание свободно/занято
    {
        Console.WriteLine("Расписание на сегодня");
        foreach (var slot in _appointmentBook.Slots)
        {
            if (slot.Status == SlotStatus.Free)
            {
                Console.WriteLine($"{slot.Time:HH:mm} [свободно]");
            }
            else
            {
                if (slot.Client != null)
                    Console.WriteLine($"{slot.Time:HH:mm} [занято] {slot.Client.Name}, {slot.Client.Phone}");
            }
        }

    }

    private void ShowFreeSlots() //показать свободные слоты
    {
        var freeSlots = _appointmentBook.FindFree();
        Console.WriteLine("Свободные слоты:");
        foreach (var slot in freeSlots)
        {
            Console.WriteLine($"{slot.Time:HH:mm}");
        }
    }

    private void BookClient()
    {
        Console.WriteLine("Имя клиента");
        var clientName = Console.ReadLine();
        Console.WriteLine("Телефон клиента");
        var phone = Console.ReadLine();
        Console.WriteLine("Выберите удобное для вас время (В формате hh:mm с 9:00 до 18:00 )");
        var timeText = Console.ReadLine();

        if (!TimeOnly.TryParse(timeText, out var time))
        {
            Console.WriteLine("Некорректное время.");
            return;
        }

        if (clientName != null)
        {
            if (phone != null)
            {
                var client = new Client(clientName, phone);
                var isBooked = _appointmentBook.Book(client, time);
                if (isBooked)
                {
                    Console.WriteLine($"{client.Name} записан на {time:HH:mm}");
                }
                else
                {
                    Console.WriteLine("Запись не создана, возможно выбранное время занято");
                }
            }
        }
    }

    private void CancelAppointment()
    {
        Console.Write("Укажите имя клиента на которого создана запись, которую требуется отменить");
        var clientName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(clientName))
        {
            Console.WriteLine("Введите имя клиента, поле не может быть пустым");
        }

        if (clientName != null)
        {
            var slot = _appointmentBook.FindByClientName(clientName);
            if (slot == null)
            {
                Console.WriteLine("Указанное имя не найдено");
                return;
            }

            Debug.Assert(slot.Client != null);  
            var canceledClientName =
                slot.Client.Name; // сохранение данных о записи что нашли, чтобы их передать в соощение при отмене записи
            var cancelTime = slot.Time;
            {
                Console.WriteLine($"{canceledClientName} записан на {cancelTime:HH:mm} ОТМЕНЕНА");
            }
        }
    }

    public void FindByClientName()
        {
            Console.WriteLine("Введите имя клиента");
            var clientName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(clientName))
            {
                Console.WriteLine("Введите имя клиента, поле не может быть пустым");
            }

            if (clientName != null)
            {
                var slot = _appointmentBook.FindByClientName(clientName);
                if (slot == null)
                {
                    Console.WriteLine("Указанное имя не найдено");
                    return;
                }

                if (slot.Client != null)
                    Console.WriteLine($"{slot.Client.Name} записан на {slot.Time:HH:mm}, телефон: {slot.Client.Phone}");
            }
        }
    
    }


