using Newtonsoft.Json;
using System.Diagnostics;
namespace dhamma.Models;

public class User
{
    public User(int userId, String name,String lastname ,  String email)
    {
        this.UserId = userId;
        this.LastName = lastname; 
        this.Name = name;
        this.Email = email;
        

    }
    public int UserId { get; set; }
    public String Name { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }

    public String Register_user() {
        if(this.UserId != null){
            StreamReader r = new StreamReader("./Database/User.json");
            String temp_json = r.ReadToEnd();
            r.Close();
            var user_list = JsonConvert.DeserializeObject<List<User>>(temp_json);
            user_list.Add(this);
            var newJson = JsonConvert.SerializeObject(user_list, Formatting.Indented);
            File.WriteAllText("./Database/User.json",newJson);
            return "Success";
        }
        else {
            Debug.WriteLine("This userID is null"); 
            return "Fail";
        }
        
        
    }
  


}
