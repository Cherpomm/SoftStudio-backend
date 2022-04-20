namespace dhamma.Models;
public class Comment
{
    public int CommentId { get; set; }
    public string? Username { get; set; }
    public string? Description { get; set; }
    public string? Date { get; set; }
    public int Like {get; set;}
    public int Dislike {get; set;}
}