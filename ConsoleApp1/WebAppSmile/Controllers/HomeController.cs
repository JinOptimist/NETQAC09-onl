using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppSmile.Models;

namespace WebAppSmile.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var mySecond = DateTime.Now.Second;
            Console.WriteLine($"Privacy was called {mySecond}");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
