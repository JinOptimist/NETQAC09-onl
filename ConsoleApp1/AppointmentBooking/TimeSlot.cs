//время слотов

namespace AppointmentBooking;

public class TimeSlot

{
 public TimeOnly Time { get; set;  }
 //статус показывает статус занят или свобоен слот
public SlotStatus Status { get; set;  }


public Client? Client { get; set; } // хранит данные клиента если слот занят

public TimeSlot(TimeOnly time)
     {
      Time = time;
      Status = SlotStatus.Free;
      Client = null; // слот если свободен то null
     }
      
public void Book(Client client)
     {
      Client = client;
      Status = SlotStatus.Booked;
     }
public void Cancel()
     {
      Client = null;
      Status = SlotStatus.Free; 
     }
}