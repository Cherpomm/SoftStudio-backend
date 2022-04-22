using Newtonsoft.Json;
using System.Diagnostics;
namespace dhamma.Models;



public class User
{
    public static int nowId  = 0 ;

    public User(){
        this.Status = "Active"; 
        this.UserId = nowId ; 
    }

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


    public static String cancle_member(int userId){
        var user_list = getAllUsers();
        Console.WriteLine(userId);
        Boolean check  =false ;  
        foreach (User user in user_list) {
            if(user.UserId== userId) {
                user.Status = "Cancled";
                check  = true ;
            }
        }
       
        writeTodb(user_list);
        if(check ){
            return  "Cancled Successfully " + userId.ToString();
        }
        else {
            return " Cancled Fail";
        }
        

    }
    public static String activate_member(int userId){
        var user_list = getAllUsers();
        Boolean check  =false ;  
        foreach (User user in user_list) {
            if(user.UserId== userId) {
                user.Status = "Active";
                check  = true ;
            }
        }
       
        var newJson = JsonConvert.SerializeObject(user_list, Formatting.Indented);
        File.WriteAllText("./Database/User.json",newJson);
        if(check ){
            return  "Active Successfully ";
        }
        else {
            return " Active Fail";
        }
        

    }
    public static String banned_user(int userId, int adminId ) {
        List<User> user_list = new List<User>();
        Boolean check = false;
        if (isAdmin(adminId)) {
            user_list = getAllUsers();
            foreach(User user in user_list){
                if(user.UserId == userId){
                    user.Status = "Banned";
                    check = true ;
                }
            }
        }
        if(check){
            writeTodb(user_list);
            return "Banned Succcessfully";
        }else {
            return "Banned Fail";
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
    
    public static User User_Login(String username, String password){
        StreamReader r = new StreamReader("./Database/User.json");
        String temp_json = r.ReadToEnd();
        r.Close();
        var user_list = JsonConvert.DeserializeObject<List<User>>(temp_json);
        foreach(User user in user_list){
            if(user.UserName == username && user.Password == password){
                return user; 
            }
        }
        return null ; 
        
        

        
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
   
    public static String edit_User_object(User newUser){
            var check =0 ;
            var user_list = getAllUsers();
            foreach(User user in user_list){
                if(user.UserId == newUser.UserId){
                    user.Name = newUser.Name;
                    user.LastName = newUser.LastName; 
                    user.Email = newUser.Email ;
                    user.UserName = newUser.UserName;
                    user.Password = newUser.Password ;
                    check =1 ;
                    
                }
            }
            var newJson = JsonConvert.SerializeObject(user_list, Formatting.Indented);
            File.WriteAllText("./Database/User.json",newJson);
            if(check ==1 ) { 
                return "Edit Success";

            }
            else {
                return "Edit Fail";
            }

        
    }
    public static void writeTodb(List<User> user_list){
        var newJson = JsonConvert.SerializeObject(user_list, Formatting.Indented);
        File.WriteAllText("./Database/User.json",newJson);
    }
  
  
  
    


    public void append_User() {
       
        StreamReader r = new StreamReader("./Database/User.json");
        String temp_json = r.ReadToEnd();
        r.Close();
        List<User>  user_list = new List<User>();
        if(temp_json != null ) {
            user_list = JsonConvert.DeserializeObject<List<User>>(temp_json);
            }
        user_list.Add(this);
        var newJson = JsonConvert.SerializeObject(user_list, Formatting.Indented);
        File.WriteAllText("./Database/User.json",newJson);
            
     
        
        
    }
    public static String Register_User(String username,String password, String name, String  lastname,String email){
        User newUser  = new User(nowId, username , password, name,lastname, email);
        if(isRedundantEmail(newUser.Email)){
            return  " Email redundant : Register Fail";
        }
        if(isRedundantUserName(newUser.UserName)){
            return " User name redunddant :Register Fail ";
        }
        
        newUser.UserId = findlastId();
        newUser.append_User();
        nowId  +=1; 
        return "Success";
    }
    public static String Register_User(User newUser){
        if(isRedundantEmail(newUser.Email)){
            return  " Email redundant : Register Fail";
        }
        if(isRedundantUserName(newUser.UserName)){
            return " User name redunddant :Register Fail ";
        }
        newUser.UserId = findlastId();
        newUser.Status = "Active";
        newUser.append_User();
        nowId  +=1; 
        return "Success";
    }

    public static List<User> getUserbyStatus (String status) {
        List<User> temp_list =  new List<User>();
        var all_user = getAllUsers();
        foreach(User user in all_user) {
            if(user.Status == status){
                temp_list.Add(user);
            }
        }
        return temp_list;

    }
        public static Boolean  isRedundantUserName(String newUsername ){
        List<User> exist_users = getAllUsers();
        foreach(User user in  exist_users){
            if (user.UserName == newUsername){
                return true; 
            }

        }
        return false ; 
    }
    public static Boolean  isRedundantEmail(String newEmail ){
        List<User> exist_users = getAllUsers();
        foreach(User user in  exist_users){
            if (user.Email == newEmail){
                Console.WriteLine("EmailRedundant");
                return true; 
            }

        }
        return false ; 
    }
    public static User GetUserbyEmail(String Email) {
        var user_list = getAllUsers();
        foreach (User user  in user_list ){
            if(user.Email == Email){
                return user; 
            }
            

        }
        return null;
    }
    public static int findlastId() {
        List<User> exist_users = getAllUsers();
        int last =  0;
        if(!exist_users.Any()){
            return 0 ; 
        }
        foreach(User user in exist_users){
            if(user.UserId > last){
                last = user.UserId;
            }
        }
        return last + 1 ; 
    }
    
  


}
