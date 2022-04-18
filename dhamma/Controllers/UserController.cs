using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dhamma.Models;
using System;
using System.IO;
using System.Text.Json;
using static dhamma.Models.User;

namespace dhamma.Controllers{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {   
            string result = Register_User("uname","passworldd","name","lname","@email.com");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            return Ok($"Reading content #{id}.");
        }

        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {

            var x = Register_User(user);
            return Ok(x);

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