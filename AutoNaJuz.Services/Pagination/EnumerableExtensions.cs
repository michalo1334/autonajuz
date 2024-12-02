using System.Collections;

namespace AutoNaJuz.Services.Pagination;

public static class EnumerableExtensions
{
    public static Paginated<T> Paginate<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
    {
        var enumerable = source as T[] ?? source.ToArray();
        
        return new Paginated<T>(enumerable, pageIndex, pageSize);
    }
}