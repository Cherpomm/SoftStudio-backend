namespace dhamma.Models;
public class Comment
{
    public Comment(int id, string user, string des, string date)
    {
        CommentId = id;
        Username = user;
        Description = des;
        Date = date;
    }
    public int CommentId { get; set; }
    public string? Username { get; set; }
    public string? Description { get; set; }
    public string? Date { get; set; }
}