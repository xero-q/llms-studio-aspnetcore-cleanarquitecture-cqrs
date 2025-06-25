namespace Application.Threads.Get;

public class PaginatedThreadResponse
{
    public int CurrentPage { get; set; }
    public bool HasNext { get; set; }
    public IEnumerable<ThreadListResponse> Results{ get; set; }
}
