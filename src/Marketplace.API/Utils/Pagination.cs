using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Utils
{
    public class Pagination<T> : IPagination<T> where T : class
    {
        public Pagination(int pageSize = 13)
        {
            PageIndex = 1;
            PageSize = pageSize;
        }

        public IEnumerable<T>? Items { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }

        public void SetCount(int count)
        {
            PageCount = Convert.ToInt32(Math.Ceiling((double)count / (double)PageSize));
        }
    }
}