using Newtonsoft.Json;
using System.Diagnostics;
namespace dhamma.Models;


public class Admin
{
    public static int nowId  = -1 ;

    public Admin(int cur_id , String Adminname , String password , String name,String lastname ,  String email)
    {
        this.AdminId = cur_id;
        this.LastName = lastname; 
        this.Name = name;
        this.Email = email;
        this.Password = password; 
        this.AdminName = Adminname; 
        this.Status = "Active"; // has 3 status : {'Cancle' ,'bannedbyAdmin+ "AdminId"','Active'}
        

    }
    public int AdminId { get; set; }

    public String AdminName {get; set ;}
    public String Password{get ; set; }
    public String Name { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    public String Status {get; set;}


    public void cancle_member(){
        this.Status = "Cancle";
    }
    
    public static Boolean Admin_Login(String Adminname, String password){
        StreamReader r = new StreamReader("./Database/Admin.json");
        String temp_json = r.ReadToEnd();
        r.Close();
        var Admin_list = JsonConvert.DeserializeObject<List<Admin>>(temp_json);
        foreach(Admin Admin in Admin_list){
            if(Admin.AdminName == Adminname && Admin.Password == password){
                return true; 
            }
        }
        return false;
        
     
        
    }


    public void append_Admin() {
       
        StreamReader r = new StreamReader("./Database/Admin.json");
        String temp_json = r.ReadToEnd();
        r.Close();
        var Admin_list = JsonConvert.DeserializeObject<List<Admin>>(temp_json);
        Admin_list.Add(this);
        var newJson = JsonConvert.SerializeObject(Admin_list, Formatting.Indented);
        File.WriteAllText("./Database/Admin.json",newJson);
            
     
        
        
    }
    public static String Register_Admin(String Adminname,String password, String name, String  lastname,String email){
        
        Admin newAdmin  = new Admin(nowId, Adminname , password, name,lastname, email);
        newAdmin.append_Admin();
        nowId  -=1; 
        return "Success";
    }
    
  


}
