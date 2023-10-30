using System.ComponentModel.DataAnnotations.Schema;

namespace FiberFresh.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Client Client { get; set; } = default!;
        public List<Service> Services { get; set; } = default!;

        [NotMapped]
        public PriceRange TotalPriceRange { get; set; } = new PriceRange(0, 0);

        [Column("Valuation")]
        public string TotalPriceRangeAsString
        {
            get
            {
                return $"{TotalPriceRange.From} - {TotalPriceRange.To}";
            }
            set
            {
                string[] parts = value.Split(new[] { " - " }, StringSplitOptions.None);

                if (parts.Length == 2 && int.TryParse(parts[0], out int from) && int.TryParse(parts[1], out int to))
                {
                    TotalPriceRange.From = from;
                    TotalPriceRange.To = to;
                }
            }
        }

        public DateOnly DateOfService { get; set; } = default!;
        public TimeOfDay TimeOfDay { get; set; }

        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(2);

        public Booking() { }

        public Booking(List<Service> services)
        {
            services.ForEach(x => TotalPriceRange.Add(x.PriceRange));
        }

        public Booking(Client client, List<Service> services, DateOnly dateOfService, TimeOfDay timeOfDay, string note = "")
        {
            Client = client;
            Services = services;
            DateOfService = dateOfService;
            TimeOfDay = timeOfDay;
            Note = note;

            services.ForEach(x => TotalPriceRange.Add(x.PriceRange));
        }
    }
}
