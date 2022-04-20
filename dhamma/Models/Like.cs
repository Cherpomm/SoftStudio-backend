namespace dhamma.Models;
public class Like
{
    public Like(int userId, String userName, String value)
    {
        UserId = userId;
        UserName = userName;
        Value = value;
    }
    public int UserId { get; set; }
    public String? UserName { get; set; }
    public String? Value { get; set; } // Value consist of {"Like", "Dislike", "None"}
}