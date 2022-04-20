using Newtonsoft.Json;
using System.Diagnostics;

namespace dhamma.Models;
public class Content
{
    public static int nowId = 0;
    public Content(){}
    public Content(String title, String description, String location, String imgURL, List<String> tag)
    {
        ContentId = nowId;
        Title = title;
        Description = description;
        Location = location;
        ImageURL = imgURL;
        Tag = tag;
        CommentList = new List<Comment>();
        LikeList = new List<Like>();
        ContentStatus = "Active";
    }
    public int ContentId { get; set; }
    public String Title { get; set; }
    public String Description { get; set; }
    public String Location {get; set;}
    public String ImageURL { get; set; }
    public List<String> Tag { get; set; }
    public List<Comment> CommentList { get; set; }
    public List<Like> LikeList {get; set;}
    public String ContentStatus {get;set;} // Consist of {"Active", "Hidden"}

    public static List<Content> getAllContent(){
        String json = getAllContent_JSON();
        var result =  JsonConvert.DeserializeObject<List<Content>>(json) ?? new List<Content>();
        return result;
    }

    private static String getAllContent_JSON(){
        StreamReader r = new StreamReader("./Database/ContentDatabase.json");
        String json = r.ReadToEnd();
        r.Close();
        return json;
    }

    public static Content getContentbyId(int contentId){
        var content_list = getAllContent();
        foreach(Content content in content_list){
            if(content.ContentId == contentId){
                return content;
            }
        }
        return null;
    }

    private String loadDatabase(){
        StreamReader r = new StreamReader("./Database/ContentDatabase.json");
        String json = r.ReadToEnd();
        r.Close();
        return json;
    }

    private void updateDatabase(List<Content> content_list){
        var newJson = JsonConvert.SerializeObject(content_list, Formatting.Indented);
        File.WriteAllText("./Database/ContentDatabase.json",newJson);
    }

    private void append_Content(){
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        if(content_list != null){
            content_list.Add(this);
            updateDatabase(content_list);
        }
    }

    public static String createContent(Content content){
        StreamReader r = new StreamReader("./Database/ContentDatabase.json");
        String json = r.ReadToEnd();
        r.Close();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        var newJson = JsonConvert.SerializeObject(content_list, Formatting.Indented);
        foreach(Content aContent in content_list){
            if(aContent.ContentId >= nowId){
                Console.WriteLine(nowId);
                Console.WriteLine(aContent.ContentId);
                nowId = aContent.ContentId + 1;
            }
        }
        content.ContentId = nowId;
        content.append_Content();
        nowId+=1;
        return "Success";
    }

    private bool remove_Content(){
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        if(content_list != null){
            try{
                var itemToRemove = content_list.Single(r => r.ContentId == this.ContentId);
                content_list.Remove(itemToRemove);
                updateDatabase(content_list);
                return true;
            }
            catch{
                return false;
            }
        }
        return false;
    }

    public static String deleteContent(Content content){
        bool result = content.remove_Content();
        if(result == true) return "Success";
        return "Fail";
    }

    private bool changeStatus(String status){
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        if(content_list != null){
            foreach(Content content in content_list){
                if(content.ContentId == this.ContentId) 
                {
                    content.ContentStatus = status;
                    updateDatabase(content_list);
                    return true;
                }
            }
        }
        return false;
    }

    public static String hideContent(Content content){
        bool result = content.changeStatus("Hidden");
        if(result == true) return "hideContent Success";
        return "hideContent Fail";
    }
    public static String revealContent(Content content){
        bool result = content.changeStatus("Active");
        if(result == true) return "revealContent Success";
        return "revealContent Fail";
    }




    public bool append_Comment(Comment comment){
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        if(content_list == null) return false;
        foreach(Content content in content_list){
            if(this.ContentId == content.ContentId){
                content.CommentList.Add(comment);
                updateDatabase(content_list);
                return true;
            }
        }
        return false;
    }

    public static String comment(Content content, Comment comment){
        bool result = content.append_Comment(comment);
        if(result == true) return "Success";
        return "Fail";
    }

    private bool remove_Comment(Comment comment){
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        if(content_list == null) return false;
        foreach(Content content in content_list){
            if(this.ContentId == content.ContentId){
                content.CommentList.Remove(comment);
                updateDatabase(content_list);
                return true;
            }
        }
        return false;
    }

    public static String deleteComment(Content content, Comment comment){
        bool result = content.remove_Comment(comment);
        if(result == true) return "Success";
        return "Fail";
    }

    public String deleteBannedUserComment(User user){
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        if(content_list == null) return "Fail";
        foreach(Content content in content_list){
            foreach(Comment comment in content.CommentList){
                if(user.UserName == comment.Username){
                    remove_Comment(comment);
                }
            }
        }
        return "Success";
    }
}