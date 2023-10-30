using System.ComponentModel.DataAnnotations.Schema;

namespace FiberFresh.Domain.Entities
{
    [NotMapped]
    public class PriceRange
    {
        public int From { get; set; }
        public int To { get; set; }

        public PriceRange(int from, int to)
        {
            From = from;
            To = to;
        }

        public void Add(PriceRange priceRange)
        {
            From += priceRange.From;
            To += priceRange.To;
        }

        public void Remove(PriceRange priceRange)
        {
            From -= priceRange.From;
            To -= priceRange.To;
        }

        public override string ToString() =>
            $"{From} - {To}";
    }
}
