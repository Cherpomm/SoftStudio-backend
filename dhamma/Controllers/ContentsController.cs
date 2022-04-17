using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dhamma.Models;

using Newtonsoft.Json;

namespace dhamma.Controllers;

public class ContentsController : Controller
{
    private readonly ILogger<ContentsController> _logger;

    public ContentsController(ILogger<ContentsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private List<Content> LoadJson()
    {
        using (StreamReader reader = new StreamReader("./Database/ContentDatabase.json"))
        {
            string json = reader.ReadToEnd();
            List<Content>? contentList = JsonConvert.DeserializeObject<List<Content>>(json) ?? new List<Content>();
            return contentList;
        }
    }

    public JsonResult GetContent()
    {
        List<Content> content = LoadJson();
        var json = JsonConvert.SerializeObject(content);
        return Json(json);
    }

    [HttpPost]
    public JsonResult AddContent(string content)
    {
        try
        {
            Content json = JsonConvert.DeserializeObject<Content>(content);
            List<Content>? contentList = LoadJson();
            contentList.Add(json);
            System.IO.File.WriteAllText("./Database/ContentDatabase.json", JsonConvert.SerializeObject(contentList));
            Console.WriteLine("Form Controller success");
            return Json(JsonConvert.SerializeObject(contentList, Formatting.Indented));
        }
        catch (Exception e)
        {
            Console.WriteLine("Form Controller error:" + e.Message);
            return Json(new { status = e.Message });
        }
    }
}