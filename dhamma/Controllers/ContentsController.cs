using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dhamma.Models;
using static dhamma.Models.Content;

using Newtonsoft.Json;

namespace dhamma.Controllers{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ContentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetContent()
        {
            List<Content> result = getAllContent();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetContentbyId(int id)
        {
            Content result = getContentbyId(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("Post")]
        public IActionResult Post([FromBody]Content content)
        {
            return Ok(content);
        }

        [HttpPost]
        [Route("CreateContent")]
        public IActionResult CreateContent([FromBody]Content content)
        {
            var result = createContent(content);
            return Ok(result);
        }

        [HttpPost]
        [Route("HideContent")]
        public IActionResult HideContent([FromBody]Content content)
        {
            var result = hideContent(content);
            return Ok(result);
        }

        [HttpPost]
        [Route("RevealContent")]
        public IActionResult RevealContent([FromBody]Content content)
        {
            var result = revealContent(content);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating all the contents.");
        }

        [HttpDelete("{DeleteContent}")]
        public IActionResult DeleteContent(Content content)
        {
            var result = deleteContent(content);
            return Ok(result);
        }

    }
}