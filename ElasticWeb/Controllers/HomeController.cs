using ElasticWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Diagnostics;
using System.Text.Json;

namespace ElasticWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IElasticClient elastic;
        private readonly HttpClient http;

        public HomeController(ILogger<HomeController> logger,IElasticClient elastic,HttpClient http)
        {
            _logger = logger;
            this.elastic = elastic;
            this.http = http;
        }

        public async Task<IActionResult> Index()
        {
            var posts =await http.GetStringAsync("https://jsonplaceholder.typicode.com/comments");
            if (!string.IsNullOrEmpty(posts){
                var comments=JsonSerializer.Deserialize<Comments>(posts);
                var el=elastic.In
            }
            _logger.LogInformation("Familiarity breeds content {activity}", Activity.Current?.Id);
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Recognize me -> {traceId}", HttpContext.TraceIdentifier);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}