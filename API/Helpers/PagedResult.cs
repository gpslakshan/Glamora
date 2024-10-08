namespace API.Helpers;

public class PagedResult<T>
{
    public PagedResult(IEnumerable<T> items, int totalItemsCount, int pageSize, int pageNum)
    {
        Items = items;
        TotalItemsCount = totalItemsCount;
        TotalPages = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
        ItemsFrom = pageSize * (pageNum - 1) + 1;
        ItemsTo = pageSize * pageNum;
    }

    // pagesize = 5 pageNum 2 -> (6-10)
    // pagesize = 5 pageNum 3 -> (11-15)

    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
}
