namespace MyWebApp.Services
{
    public class FilterOptions
    {
        public string? SearchKey { get; set; }
        public double? FromPrice { get; set; }
        public double? ToPrice { get; set; }
        public string? SortBy { get; set; }

        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 3;
    }
}
