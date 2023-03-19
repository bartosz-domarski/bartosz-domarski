namespace RestaurantAPI.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemTo { get; set; }
        public int TotalItems { get; set; }

        public PagedResult(List<T> items, int totalItems, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItems = totalItems;
            ItemFrom = pageSize * (pageNumber - 1) + 1;
            ItemTo = ItemFrom + pageSize - 1;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        }
    }
}
