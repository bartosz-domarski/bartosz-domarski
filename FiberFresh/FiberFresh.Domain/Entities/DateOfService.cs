namespace FiberFresh.Domain.Entities
{
    public class DateOfService
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOfDay TimeOfDay { get; set; }
    }
}
