using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dhamma.Models;
using static dhamma.Models.Content;

using Newtonsoft.Json;

namespace dhamma.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ContentsController : ControllerBase
    {
        public class CreateParam
        {
            public CreateParam() { }
            public int contentId { get; set; }
            public Comment? comment { get; set; }
        }
        public class Param
        {
            public Param() { }
            public int contentId { get; set; }
            public int commentId { get; set; }
        }

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
        [Route("CreateContent")]
        public IActionResult CreateContent([FromBody] Content content)
        {
            var result = createContent(content);
            return Ok(result);
        }

        [HttpPut]
        [Route("HideContent")]
        public IActionResult HideContent([FromBody] int contentId)
        {
            var result = hideContent(contentId);
            return Ok(result);
        }

        [HttpPut]
        [Route("RevealContent")]
        public IActionResult RevealContent([FromBody] int contentId)
        {
            var result = revealContent(contentId);
            return Ok(result);
        }

        [HttpPost]
        [Route("Comment")]
        public IActionResult CommentContent([FromBody] CreateParam commentparam)
        {
            var result = comment(commentparam.contentId, commentparam.comment);
            return Ok(result);
        }

        [HttpPut]
        [Route("HideComment")]
        public IActionResult HideComment([FromBody] Param param)
        {
            var result = hideComment(param.contentId, param.commentId);
            return Ok(result);
        }

        [HttpPut]
        [Route("RevealComment")]
        public IActionResult RevealComment([FromBody] Param param)
        {
            var result = revealComment(param.contentId, param.commentId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteContent")]
        public IActionResult DeleteContent([FromBody] int contentId)
        {
            var result = deleteContent(contentId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteComment")]
        public IActionResult DeleteComment([FromBody] Param param)
        {
            var result = deleteComment(param.contentId, param.commentId);
            return Ok(result);
        }
    }
}