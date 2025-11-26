namespace ebay.Shared.Common;
public class PagingResult<T>
{
    public List<T> TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int TotalRow { get; set; }
}