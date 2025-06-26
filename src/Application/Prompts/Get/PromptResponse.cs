namespace Application.Prompts.Get;

public sealed class PromptResponse
{
    public int Id { get; set; }
    
    public string Prompt { get; set; }
    
    public string Response { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int ThreadId { get; set; }
  
}
