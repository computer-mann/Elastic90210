using ElasticWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ElasticWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Familiarity breeds content {activity}", Activity.Current?.Id);
            //Log.Information("Familiarity breeds content {activity}", Activity.Current?.Id);
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