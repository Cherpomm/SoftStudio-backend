using Newtonsoft.Json;
using System.Diagnostics;
namespace dhamma.Models;



public class Admin
{
    public static int nowId  = 0 ;

    public Admin(){
        this.Status = "Active"; 
        this.AdminId = nowId ; 
    }

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


    public static void cancle_member(int AdminId){
        var Admin_list = getAllAdmins();
        foreach (Admin Admin in Admin_list) {
            if(Admin.AdminId== AdminId) {
                Admin.Status = "Cancled";
            }
        }
        var newJson = JsonConvert.SerializeObject(Admin_list, Formatting.Indented);
        File.WriteAllText("./Database/Admin.json",newJson);
        

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
    public String edit_Admin(int AdminId, String newname, String newLname, String newEmail, String newAdminname, String newPassword){
        
            var Admin_list = getAllAdmins();
            foreach(Admin Admin in Admin_list){
                if(Admin.AdminId == AdminId){
                    Admin.Name = newname;
                    Admin.LastName = newLname; 
                    Admin.Email = newEmail ;
                    Admin.AdminName = newAdminname;
                    Admin.Password = newPassword ;

                    return "Edit Successfull";
                }
            }
            return " No Id found ";

        
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
        nowId  +=1; 
        return "Success";
    }
    public static String Register_Admin(Admin newAdmin){
       
        newAdmin.append_Admin();
        nowId  +=1; 
        return "Success";
    }
    
  


}
