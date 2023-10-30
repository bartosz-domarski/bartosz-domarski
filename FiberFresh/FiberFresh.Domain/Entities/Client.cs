namespace FiberFresh.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public int Floor { get; set; }
        public bool IsElevator { get; set; }
    }
}
