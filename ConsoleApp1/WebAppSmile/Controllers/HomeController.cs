using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppSmile.Models;

namespace WebAppSmile.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var jokedTask = GetDataFromApiAsync<JokeDto>("https://official-joke-api.appspot.com/jokes/random");
            var restauranTask = GetDataFromApiAsync<List<RestauranDto>>("https://fakerestaurantapi.runasp.net/api/Restaurant");

            await Task.WhenAll(jokedTask, restauranTask);

            var viewModel = new HomeViewModel
            {
                RestauransDto = restauranTask.Result,
                JokeDto = jokedTask.Result
            };

            return View(viewModel);
        }

        private async Task<T> GetDataFromApiAsync<T>(string url)
        {
            var http = new HttpClient();
            var jokeTask = http.GetAsync(url);
            var result = await jokeTask;
            var jokedDto = await result.Content.ReadFromJsonAsync<T>();
            return jokedDto;
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
