//данные клиента Имя телефон

namespace AppointmentBooking
{
    public class Client
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        
        public Client(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
    }
}