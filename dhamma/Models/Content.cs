using Newtonsoft.Json;
using System.Diagnostics;

namespace dhamma.Models;
public class Content
{
    public static int nowId = 0;
    public static int nowCommentId = 0;
    public Content() { }
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
    public String Location { get; set; }
    public String ImageURL { get; set; }
    public List<String> Tag { get; set; }
    public List<Comment> CommentList { get; set; }
    public List<Like> LikeList { get; set; }
    public String ContentStatus { get; set; } // Consist of {"Active", "Hidden"}

    public static List<Content> getAllContent()
    {
        String json = getAllContent_JSON();
        var result = JsonConvert.DeserializeObject<List<Content>>(json) ?? new List<Content>();
        return result;
    }

    private static String getAllContent_JSON()
    {
        StreamReader r = new StreamReader("./Database/ContentDatabase.json");
        String json = r.ReadToEnd();
        r.Close();
        return json;
    }

    public static Content getContentbyId(int contentId)
    {
        var content_list = getAllContent();
        foreach (Content content in content_list)
        {
            if (content.ContentId == contentId)
            {
                return content;
            }
        }
        return null;
    }

    private static String loadDatabase()
    {
        StreamReader r = new StreamReader("./Database/ContentDatabase.json");
        String json = r.ReadToEnd();
        r.Close();
        return json;
    }

    private static void updateDatabase(List<Content> content_list)
    {
        var newJson = JsonConvert.SerializeObject(content_list, Formatting.Indented);
        File.WriteAllText("./Database/ContentDatabase.json", newJson);
    }

    private void append_Content()
    {
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        if (content_list != null)
        {
            content_list.Add(this);
            updateDatabase(content_list);
        }
    }

    public static String createContent(Content content)
    {
        StreamReader r = new StreamReader("./Database/ContentDatabase.json");
        String json = r.ReadToEnd();
        r.Close();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        var newJson = JsonConvert.SerializeObject(content_list, Formatting.Indented);
        foreach (Content aContent in content_list)
        {
            if (aContent.ContentId >= nowId)
            {
                nowId = aContent.ContentId + 1;
            }
        }
        content.ContentId = nowId;
        content.append_Content();
        nowId = 0;
        return "Success";
    }

    private bool remove_Content()
    {
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        if (content_list != null)
        {
            try
            {
                var itemToRemove = content_list.Single(r => r.ContentId == this.ContentId);
                content_list.Remove(itemToRemove);
                updateDatabase(content_list);
                return true;
            }
            catch
            {
                return false;
            }
        }
        return false;
    }

    public static String deleteContent(int contentId)
    {
        bool result = false;
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        foreach (Content content in content_list)
        {
            Console.WriteLine("ContentId: " + content.ContentId);

            Console.WriteLine("ContentId: " + contentId);
            if (content.ContentId == contentId)
            {
                Console.WriteLine("ContentId: " + content.ContentId);
                result = content.remove_Content();
            }
        }
        if (result == true) return "Success";
        return "Fail";
    }

    private bool changeStatus(String status)
    {
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        if (content_list != null)
        {
            foreach (Content content in content_list)
            {
                if (content.ContentId == this.ContentId)
                {
                    content.ContentStatus = status;
                    updateDatabase(content_list);
                    return true;
                }
            }
        }
        return false;
    }

    public static String hideContent(int contentId)
    {
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        foreach (Content content in content_list)
        {
            if (content.ContentId == contentId)
            {
                bool result = content.changeStatus("Hidden");
                if (result == true) return "hideContent Success";
            }
        }
        return "hideContent Fail";
    }
    public static String revealContent(int contentId)
    {
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        foreach (Content content in content_list)
        {
            if (content.ContentId == contentId)
            {
                bool result = content.changeStatus("Active");
                if (result == true) return "revealContent Success";
            }
        }
        return "revealContent Fail";
    }


    //---------------------Comment------------------------------------------------------    

    private bool append_Comment(Comment comment)
    {
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        if (content_list == null) return false;
        foreach (Content content in content_list)
        {
            if (this.ContentId == content.ContentId)
            {

                content.CommentList.Add(comment);
                updateDatabase(content_list);
                return true;
            }
        }
        return false;
    }

    public static String comment(int contentId, Comment acomment)
    {
        bool result = false;
        Comment comment = acomment;
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        foreach (Content content in content_list)
        {
            if (contentId == content.ContentId)
            {
                foreach (Comment aComment in content.CommentList)
                {
                    if (aComment.CommentId >= nowCommentId)
                    {
                        nowCommentId = aComment.CommentId + 1;
                    }
                }
                comment.CommentId = nowCommentId;
                comment.LikeList = new List<Like>();
                comment.CommentStatus = "Active";
                result = content.append_Comment(comment);
                nowCommentId = 0;
            }
        }
        if (result == true) return "comment Success";
        return "comment Fail";
    }

    private bool remove_Comment(int commentId)
    {
        var content_list = getAllContent();
        Content content = null;
        Comment comment = null;
        foreach (Content acontent in content_list)
        {
            if (acontent.ContentId == this.ContentId)
            {
                content = acontent;
                foreach (Comment acomment in content.CommentList)
                {
                    if (acomment.CommentId == commentId)
                    {
                        comment = acomment;
                    }
                }
            }
        }
        String json = loadDatabase();
        content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        if (content_list != null && content != null)
        {
            int index = content_list.FindIndex(c => c.ContentId == content.ContentId);
            try
            {
                var itemToRemove = content_list[index].CommentList.Single(r => r.CommentId == comment.CommentId);
                Console.WriteLine(itemToRemove);
                content_list[index].CommentList.Remove(itemToRemove);
                updateDatabase(content_list);
                return true;
            }
            catch
            {
                return false;
            }
        }
        return false;
    }

    public static String deleteComment(int contentId, int commentId)
    {
        bool result = false;
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        foreach (Content content in content_list)
        {
            if (content.ContentId == contentId)
            {
                result = content.remove_Comment(commentId);
            }
        }
        if (result == true) return "deleteComment Success";
        return "deleteComment Fail";
    }

    private bool changeCommentStatus(String status, int commentId)
    {
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        if (content_list != null)
        {
            foreach (Content content in content_list)
            {
                if (content.ContentId == this.ContentId)
                {
                    foreach (Comment comment in content.CommentList)
                    {
                        if (comment.CommentId == commentId)
                        {
                            comment.CommentStatus = status;
                            updateDatabase(content_list);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public static String hideComment(int contentId, int commentId)
    {
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        foreach (Content content in content_list)
        {
            if (content.ContentId == contentId)
            {
                foreach (Comment comment in content.CommentList)
                {
                    if (comment.CommentId == commentId)
                    {
                        bool result = content.changeCommentStatus("Hidden", commentId);
                        if (result == true) return "hideComment Success";
                    }
                }
            }
        }
        return "hideComment Fail";
    }
    public static String revealComment(int contentId, int commentId)
    {
        String json = loadDatabase();
        var content_list = JsonConvert.DeserializeObject<List<Content>>(json);
        foreach (Content content in content_list)
        {
            if (content.ContentId == contentId)
            {
                foreach (Comment comment in content.CommentList)
                {
                    if (comment.CommentId == commentId)
                    {
                        bool result = content.changeCommentStatus("Active", commentId);
                        if (result == true) return "revealComment Success";
                    }
                }
            }
        }
        return "revealComment Fail";
    }
}