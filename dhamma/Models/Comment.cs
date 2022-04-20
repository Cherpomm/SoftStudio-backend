namespace dhamma.Models;
public class Comment
{
    public int CommentId { get; set; }
    public User? User { get; set; }
    public string? Description { get; set; }
    public string? Date { get; set; }
    public List<Like>? LikeList { get; set; }
    public String? CommentStatus { get; set; }
}