using Newtonsoft.Json;
using System.Diagnostics;
namespace dhamma.Models;


public class User
{
    public static int nowId  = 0 ;

    public User(int cur_id , String username , String password , String name,String lastname ,  String email)
    {
        this.UserId = cur_id;
        this.LastName = lastname; 
        this.Name = name;
        this.Email = email;
        this.Password = password; 
        this.UserName = username; 
        this.Status = "Active"; // has 3 status : {'Cancle' ,'bannedbyAdmin+ "AdminId"','Active'}
        

    }
    public int UserId { get; set; }

    public String UserName {get; set ;}
    public String Password{get ; set; }
    public String Name { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    public String Status {get; set;}


    public void cancle_member(){
        this.Status = "Cancle";
    }
    public void banned_user(int adminId ) {
        if (isAdmin(adminId)) {
            this.Status = "Banned by : " + (-1*adminId).ToString();
        }
    }
    public static Boolean isAdmin(int admindId){
        // we will use adminId as minus number
        return admindId < 0 ;
    }
    public static Boolean User_Login(String username, String password){
        StreamReader r = new StreamReader("./Database/User.json");
        String temp_json = r.ReadToEnd();
        r.Close();
        var user_list = JsonConvert.DeserializeObject<List<User>>(temp_json);
        foreach(User user in user_list){
            if(user.UserName == username && user.Password == password){
                return true; 
            }
        }
        return false;
        
     
        
    }


    public void append_User() {
       
        StreamReader r = new StreamReader("./Database/User.json");
        String temp_json = r.ReadToEnd();
        r.Close();
        var user_list = JsonConvert.DeserializeObject<List<User>>(temp_json);
        user_list.Add(this);
        var newJson = JsonConvert.SerializeObject(user_list, Formatting.Indented);
        File.WriteAllText("./Database/User.json",newJson);
            
     
        
        
    }
    public static String Register_User(String username,String password, String name, String  lastname,String email){
        
        User newUser  = new User(nowId, username , password, name,lastname, email);
        newUser.append_User();
        nowId  +=1; 
        return "Success";
    }
    
  


}
