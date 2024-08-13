using Light.Contracts;

namespace ModularMonolith.Contracts.Dtos
{
    /// <summary>
    /// Lookup data entries with pagination
    /// </summary>
    public class PageLookup : IPage
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}