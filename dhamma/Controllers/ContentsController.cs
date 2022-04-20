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

        [HttpPut]
        [Route("HideContent")]
        public IActionResult HideContent([FromBody]Content content)
        {
            var result = hideContent(content);
            return Ok(result);
        }

        [HttpPut]
        [Route("RevealContent")]
        public IActionResult RevealContent([FromBody]Content content)
        {
            var result = revealContent(content);
            return Ok(result);
        }

        public class CommentParam{
            public CommentParam(){}
            public Content? content {get;set;}
            public Comment? comment {get;set;}
        }

        [HttpPut]
        [Route("Comment")]
        public IActionResult CommentContent([FromBody]CommentParam param)
        {
            var result = comment(param.content, param.comment);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating all the contents.");
        }

        [HttpDelete]
        [Route("DeleteContent")]
        public IActionResult DeleteContent(Content content)
        {
            var result = deleteContent(content);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteComment")]
        public IActionResult DeleteComment(CommentParam param)
        {
            var result = deleteComment(param.content, param.comment);
            return Ok(result);
        }
    }
}