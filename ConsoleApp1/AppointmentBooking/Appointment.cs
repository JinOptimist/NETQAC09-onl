// тут хранит время записи клиента на прием 

namespace AppointmentBooking;

public class Appointment
{
    public Client Client { get; set; }
    public TimeOnly Time { get; set; }

     public Appointment(Client client, TimeOnly time)
     {
         Client = client;
         Time = time;
     }
}

