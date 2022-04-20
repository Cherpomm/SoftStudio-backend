using Newtonsoft.Json;
using System.Diagnostics;
namespace dhamma.Models;



public class Admin
{
    public static int nowId  = 0 ;

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


    public static String cancle_member(int AdminId){
        var Admin_list = getAllAdmins();
        Console.WriteLine(AdminId);
        Boolean check  =false ;  
        foreach (Admin Admin in Admin_list) {
            if(Admin.AdminId== AdminId) {
                Admin.Status = "Cancled";
                check  = true ;
            }
        }
       
        writeTodb(Admin_list);
        if(check ){
            return  "Cancled Successfully " + AdminId.ToString();
        }
        else {
            return " Cancled Fail";
        }
        

    }
    public static String activate_member(int AdminId){
        var Admin_list = getAllAdmins();
        Boolean check  =false ;  
        foreach (Admin Admin in Admin_list) {
            if(Admin.AdminId== AdminId) {
                Admin.Status = "Active";
                check  = true ;
            }
        }
       
        var newJson = JsonConvert.SerializeObject(Admin_list, Formatting.Indented);
        File.WriteAllText("./Database/Admin.json",newJson);
        if(check ){
            return  "Active Successfully ";
        }
        else {
            return " Active Fail";
        }
        

    }
    
    public static Admin Admin_Login(String Adminname, String password){
        StreamReader r = new StreamReader("./Database/Admin.json");
        String temp_json = r.ReadToEnd();
        r.Close();
        var Admin_list = JsonConvert.DeserializeObject<List<Admin>>(temp_json);
        foreach(Admin Admin in Admin_list){
            if(Admin.AdminName == Adminname && Admin.Password == password){
                return Admin; 
            }
        }
        return null ; 
        
        

        
    }
    public static List<Admin> getAllAdmins(){
        String temp_json = getAllAdmins_JSON();
        var Admin_list = JsonConvert.DeserializeObject<List<Admin>>(temp_json);
        return Admin_list;
    }
    public static String getAllAdmins_JSON() {
        StreamReader r = new StreamReader("./Database/Admin.json");
        String temp_json = r.ReadToEnd();
        r.Close();
        return temp_json;
    }
    public static Admin GetAdminbyId(int AdminId) {
        var Admin_list = getAllAdmins();
        foreach (Admin Admin  in Admin_list ){
            if(Admin.AdminId == AdminId){
                return Admin; 
            }
            

        }
        return null;
    }
    public static String GetAdminbyId_JSON(int AdminId){
        Admin Admin = GetAdminbyId(AdminId);
        if( Admin == null){
            return "invalid AdminId";
        }
        String json_Admin = JsonConvert.SerializeObject(Admin, Formatting.Indented);
        return json_Admin ; 
        

    }
   
    public static String edit_Admin_object(Admin newAdmin){
            var check =0 ;
            var Admin_list = getAllAdmins();
            foreach(Admin Admin in Admin_list){
                if(Admin.AdminId == newAdmin.AdminId){
                    Admin.Name = newAdmin.Name;
                    Admin.LastName = newAdmin.LastName; 
                    Admin.Email = newAdmin.Email ;
                    Admin.AdminName = newAdmin.AdminName;
                    Admin.Password = newAdmin.Password ;
                    check =1 ;
                    
                }
            }
            var newJson = JsonConvert.SerializeObject(Admin_list, Formatting.Indented);
            File.WriteAllText("./Database/Admin.json",newJson);
            if(check ==1 ) { 
                return "Edit Success";

            }
            else {
                return "Edit Fail";
            }

        
    }
    public static void writeTodb(List<Admin> Admin_list){
        var newJson = JsonConvert.SerializeObject(Admin_list, Formatting.Indented);
        File.WriteAllText("./Database/Admin.json",newJson);
    }
  
  
  
    


    public void append_Admin() {
       
        StreamReader r = new StreamReader("./Database/Admin.json");
        String temp_json = r.ReadToEnd();
        r.Close();
        List<Admin>  Admin_list = new List<Admin>();
        if(temp_json != null ) {
            Admin_list = JsonConvert.DeserializeObject<List<Admin>>(temp_json);
            }
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
    public static String Register_Admin(Admin newAdmin){
       
        newAdmin.append_Admin();
        nowId  -=1; 
        return "Success";
    }

    public static List<Admin> getAdminbyStatus (String status) {
        List<Admin> temp_list =  new List<Admin>();
        var all_Admin = getAllAdmins();
        foreach(Admin Admin in all_Admin) {
            if(Admin.Status == status){
                temp_list.Add(Admin);
            }
        }
        return temp_list;

    }
    
  


}
