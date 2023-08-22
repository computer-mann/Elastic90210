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
            var response = await SendBulkDataToElastic<Users>("typicode-users", new Uri("https://jsonplaceholder.typicode.com/users"));
            if (response == false)
            {
                _logger.LogError("Something went wrong with the elastic or web request");
            }
            
            _logger.LogInformation("typicode bulk data was send to elastic search");
            _logger.LogInformation("Familiarity breeds content {activity}", Activity.Current?.Id);
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Recognize me -> {traceId}", HttpContext.TraceIdentifier);
            return View();
        }

        public async Task<IActionResult> DeleteIndex()
        {
            var response=await elastic.Indices.DeleteAsync("typicode-users");
            _logger.LogInformation("DelteResponseObject: {object}", response);
            _logger.LogInformation("Delte Index={index} is successful", "typicode-users");
            return Json(new {message="Delete"});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public  async Task<bool> SendBulkDataToElastic<T>(string index,Uri uri) where T:class
        {
            var posts = await http.GetStringAsync(uri);
            if (!string.IsNullOrEmpty(posts)){
                var comments = JsonSerializer.Deserialize<IEnumerable<T>>(posts);
                if ((await elastic.Indices.ExistsAsync(index)).Exists)
                {
                    _logger.LogInformation("The index=> {index} is already in the engine. ", index);
                    return true;
                }
                var el = await elastic.BulkAsync(b => b.Index(index)
                .CreateMany(comments, (co, d) => co.Document(d)));
                if (el.Errors ==false)
                {
                    _logger.LogInformation("{type} data was actually sent to the engine.",typeof(T));
                    return true;
                }
                _logger.LogError("DebugElasticErrors: {errors}", el.DebugInformation);
            }
            return false;

        }

        public async Task<bool> UpdateBulkDataInElastic<T>(string index, Uri uri) where T : class
        {
            var posts = await http.GetStringAsync(uri);
            if (!string.IsNullOrEmpty(posts))
            {
                var comments = JsonSerializer.Deserialize<IEnumerable<T>>(posts);
                //if ((await elastic.Indices.ExistsAsync(index)).Exists)
                //{
                //    _logger.LogInformation("The index=> {index} is already in the engine. ", index);
                //    return true;
                //}
                var el = await elastic.BulkAsync(b => b.Index(index)
                .UpdateMany(comments, (co, d) => co.Doc(d).DocAsUpsert(true)));
                if (el.Errors == false)
                {
                    _logger.LogInformation("{type} data was actually sent to the engine.", typeof(T));
                    return true;
                }
                _logger.LogError("DebugElasticErrors: {errors}", el.DebugInformation);
            }
            return false;

        }
        public async Task<bool> SendSingleItem<T>(string index, Uri uri) where T : class
        {

            return true;
        }



    }
}