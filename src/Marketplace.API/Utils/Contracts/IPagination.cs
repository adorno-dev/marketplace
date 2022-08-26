namespace Marketplace.API.Utils.Contracts
{
    public interface IPagination<T> where T : class
    {
        IEnumerable<T>? Items { get; set; }
        int TotalItems { get; set; }
        int PageSize { get; set; }
        int PageCount { get; set; }
        int PageIndex { get; set; }

        void SetCount(int count);
    }
}