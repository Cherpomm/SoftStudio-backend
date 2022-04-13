using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dhamma.Models;
using System;
using System.IO;
using System.Text.Json;


namespace dhamma.Controllers{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {   
            User test_user = new User(16,"C","G","mail");
            string result  = test_user.Register_user();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Reading content #{id}.");
        }

        [HttpPost]
        public IActionResult Post([FromBody]Content content)
        {
            return Ok(content);
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating all the contents.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting a content #{id}.");
        }

    }
}