namespace dhamma.Models;
public class Content
{
    public int ContentId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<Comment>? CommentList { get; set;}
}