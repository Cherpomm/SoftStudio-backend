using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dhamma.Models;

namespace dhamma.Controllers{
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