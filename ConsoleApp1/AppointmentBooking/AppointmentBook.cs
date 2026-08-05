//будет хранится расписание и работает с записями

namespace AppointmentBooking;

public class AppointmentBook

{
public List<TimeSlot> Slots { get; set; } // список всех слотов на день

public AppointmentBook()
{
    Slots = new List<TimeSlot>();
    //указать временой промежуток приемов в расписании

    var startTime = new TimeOnly(9, 0);
    var endTime = new TimeOnly(17, 30);

    while (startTime < endTime)
    {
        Slots.Add(new TimeSlot(startTime));
        startTime = startTime.AddMinutes(30); // время приема на каждый слот
    }

}
//возвращается список свободных слотоы
public List<TimeSlot> FindFree()
{
    return Slots
        .Where(slot => slot.Status == SlotStatus.Free)
        .ToList();
}

//изет по времени
public TimeSlot? FindSlotByTime(TimeOnly time)
{
    return Slots.FirstOrDefault(slot => slot.Time == time);
    
}

//запись клиента на выбранное им время
public bool Book(Client client, TimeOnly time)
{
    var slot = FindSlotByTime(time);
    if (slot == null) //нет времени то не записывать клиента
    {
        return false;
    }
    else if (slot.Status == SlotStatus.Booked) //время занято,  то не записывать клиента

    {
        return false;
    }
    else slot.Book(client);
    {
        return true;
    }
    
}
//cancel записи ранее созданной
public bool Cancel(string clientName)
{
    var slot = FindByClientName(clientName);
    if (slot == null)
    {
        return false;
    }
    
    slot.Cancel();
    return true;
}
//поиск записи времени клиента по name 
public TimeSlot? FindByClientName(string clientName)
{
    return Slots.FirstOrDefault(slot =>
        slot.Client != null &&
        slot.Client.Name.Equals(clientName, StringComparison.OrdinalIgnoreCase));
}
}