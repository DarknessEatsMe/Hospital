using Health.Models;

namespace Health.ViewModels
{
    public class AppointmentModels
    {
        public Ticket Ticket { get; set; } = null!;
        public ClientsCard? Card { get; set; }
        public PersonInfo? Info { get; set; }
        public List<dynamic>? Appointments { get; set; }
    }
}
