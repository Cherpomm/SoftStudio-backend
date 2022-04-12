using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dhamma.Models;
using System;
using System.IO;
using System.Text.Json;

namespace dhamma.Controllers{
    public void LoadJson()
    {
        using(StreamReader r = new StreamReader("~/D"))
        {
            
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ContentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Reading all the contents.");
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