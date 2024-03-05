namespace AShop.Catalog.Domain.Specs;

public class Pagination<T> where T : class
{
    public int PagIndex { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
    public IReadOnlyList<T> Data { get; set; }

    public Pagination()
    {
    }

    public Pagination(int pagIndex, int pageSize, long count, IReadOnlyList<T> data)
    {
        PagIndex = pagIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }
}