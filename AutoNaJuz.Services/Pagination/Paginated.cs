using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoNaJuz.Services.Pagination
{
    public class Paginated<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }

        public Paginated(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                throw new ArgumentOutOfRangeException(nameof(pageIndex), "PageIndex must be greater than 0.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize must be greater than 0.");
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            TotalCount = source.Count();
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            // If PageIndex exceeds TotalPages, return an empty list
            if (PageIndex > TotalPages && TotalPages != 0)
            {
                Items = new List<T>().AsReadOnly();
            }
            else
            {
                Items = source.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList().AsReadOnly();
            }
        }
    }
}