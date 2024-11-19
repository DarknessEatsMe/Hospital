using Health.Models;

namespace Health.ViewModels
{
    public class DocPageModels
    {
        public Doctor Doc { get; set; } = null!;
        public IEnumerable<Ticket>? Tickets { get; set; }
        public PersonInfo? Person { get; set; }
    }
}
