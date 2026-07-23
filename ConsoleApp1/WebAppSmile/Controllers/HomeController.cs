using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppSmile.Models;
using WebAppSmile.Services;
using WebAppSmile.Services.Interfaces;

namespace WebAppSmile.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiHelper _apiHelper;

        public HomeController(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<IActionResult> Index()
        {
            var jokedTask = _apiHelper.GetDataFromApiAsync<JokeDto>("https://official-joke-api.appspot.com/jokes/random");
            var restauranTask = _apiHelper.GetDataFromApiAsync<List<RestauranDto>>("https://fakerestaurantapi.runasp.net/api/Restaurant");

            await Task.WhenAll(jokedTask, restauranTask);

            var viewModel = new HomeViewModel
            {
                RestauransDto = restauranTask.Result,
                JokeDto = jokedTask.Result
            };

            return View(viewModel);
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
