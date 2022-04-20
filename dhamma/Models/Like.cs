namespace dhamma.Models;
public class Like
{
    public int UserId {get; set;}
    public String? UserName {get; set;}
    public String? Value {get; set;} // Value consist of {"Like", "Dislike", "None"}
}