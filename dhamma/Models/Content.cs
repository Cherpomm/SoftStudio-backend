namespace dhamma.Models;
public class Content
{
    public Content(int id, string title, string description)
    {
        ContentId = id;
        Title = title;
        Description = description;
        CommentList = new List<Comment>();
    }
    public int ContentId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Comment> CommentList { get; set; }
}