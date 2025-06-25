namespace Application.Threads.Get;

public sealed class ThreadResponse
{
    public int Id { get; set; }
    
    public int ModelId { get; set; }
    
    public string Title { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string CreatedAtDate { get; set; }
    
    public string ModelName { get; set; }
    
    public string ModelType { get; set; }
    
    public string ModelIdentifier { get; set; }
  
}
