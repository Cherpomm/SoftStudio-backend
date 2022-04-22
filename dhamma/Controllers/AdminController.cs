using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dhamma.Models;
using System;
using System.IO;
using System.Text.Json;
using static dhamma.Models.Admin;

namespace dhamma.Controllers{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
       
        
        [HttpGet]
        public IActionResult GetAdmins() {
            var result = getAllAdmins();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetAdmin_ID(int id){
            Console.WriteLine("GetbyID");
            var result = GetAdminbyId(id);
            return Ok(result);
        }
        

        [HttpPost]
        [Route("Register")]
        public IActionResult Register_Admin([FromBody]Admin Admin)
        {
          
            var result = Register_Admin(Admin);
            return Ok(result);
        }

        [HttpGet("{Adminname,password}")]
        [Route("Login")]
        public IActionResult Login_Admin(String Adminname, String password){
            var result = Admin_Login(Adminname,password);
            return  Ok(result);
        }

        [HttpGet] // Pass as KEY and VALUE to use  || Note : Some error occur so I can't write this function properly
        [Route("GetbyStatus")]
        public IActionResult GetAdminbyStatus(String status){ //  "Active" ,"Cancled" , "Banned"
            Console.WriteLine("Status");
            var result = getAdminbyStatus(status) ;
            return Ok(result);
        }
        
        [HttpGet] // Pass as KEY and VALUE to use  || Note : Some error occur so I can't write this function properly
        [Route("GetbyEmail")]
        public IActionResult GetbyEmail(String Email){ //  "Active" ,"Cancled" , "Banned"
            Console.WriteLine("Status");
            var result =GetAdminbyEmail(Email);
            return Ok(result);
        }

        [HttpPut]
        [Route("Edit_Admin")]
        
        public IActionResult Edit_Admin([FromBody] Admin newAdmin) { 
            var result = edit_Admin_object(newAdmin);
            return Ok(result);

        }

        
        
        [HttpPut]
        [Route("cancle_Admin")]
        
         public IActionResult Cancle_Admin(int AdminId) { 
            var result = cancle_member(AdminId );
            //var result = GetAdminbyId(AdminId);
            return Ok(result);

        }
         [HttpPut]
        [Route("activate_Admin")]
        
         public IActionResult activate_Admin(int AdminId) { 
            var result = activate_member(AdminId );
            return Ok(result);

        }



        




       

    }
}