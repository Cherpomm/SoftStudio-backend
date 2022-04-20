using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dhamma.Models;
using static dhamma.Models.User;

using Newtonsoft.Json;

namespace dhamma.Controllers{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            var result = getAllUsers();
            return Ok(result);
        }

       

    }
}