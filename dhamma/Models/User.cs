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


    public static void cancle_member(int userId){
        var user_list = getAllUsers();
        foreach (User user in user_list) {
            if(user.UserId== userId) {
                user.Status = "Cancled";
            }
        }
        var newJson = JsonConvert.SerializeObject(user_list, Formatting.Indented);
        File.WriteAllText("./Database/User.json",newJson);
        

    }
    public static void banned_user(int userId, int adminId ) {
        if (isAdmin(adminId)) {
            var user_list = getAllUsers();
            foreach(User user in user_list){
                if(user.UserId == userId){
                    user.Status = "Banned by : " + (-1*adminId).ToString();
                }
            }
        }
    }
    public static Boolean isAdmin(int adminId){
       
        StreamReader r = new StreamReader("./Database/Admin.json");
        String temp_json = r.ReadToEnd();
        r.Close();
        var Admins = JsonConvert.DeserializeObject<List<Admin>>(temp_json);
        foreach(Admin admin in Admins){
            if(admin.AdminId == adminId ){
                return true; 
            }
        }
        return false;


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
    public static List<User> getAllUsers(){
        String temp_json = getAllUsers_JSON();
        var user_list = JsonConvert.DeserializeObject<List<User>>(temp_json);
        return user_list;
    }
    public static String getAllUsers_JSON() {
        StreamReader r = new StreamReader("./Database/User.json");
        String temp_json = r.ReadToEnd();
        r.Close();
        return temp_json;
    }
    public static User GetUserbyId(int userId) {
        var user_list = getAllUsers();
        foreach (User user  in user_list ){
            if(user.UserId == userId){
                return user; 
            }
            

        }
        return null;
    }
    public static String GetUserbyId_JSON(int userId){
        User user = GetUserbyId(userId);
        if( user == null){
            return "invalid userId";
        }
        String json_user = JsonConvert.SerializeObject(user, Formatting.Indented);
        return json_user ; 
        

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
