using MazeConsole;
using MazeConsole.MazeModels.Cells;
using Microsoft.AspNetCore.Mvc;
using WebAppSmile.Models;
using WebAppSmile.Services;
using WebAppSmile.Services.Interfaces;

namespace WebAppSmile.Controllers;

public class MazeController : Controller
{
    private readonly IApiHelper _apiHelper;

    private readonly MazeGameSessionStore _store;
    private readonly IceApiDataService _iceApiDataService = new();

    private readonly HttpClient _http = new();
    private readonly FlowerApiService _flowerApi;
    private readonly IMazeSaveService _saveService;

    public MazeController(MazeGameSessionStore store, IApiHelper apiHelper, FlowerApiService flowerApi, IMazeSaveService saveService)
    {
        _store = store;
        _apiHelper = apiHelper;
        _flowerApi = flowerApi;
        _saveService = saveService;
    }

    public IActionResult Index()
    {
        return View(CellCodex.All);
    }
    public IActionResult Flower()
    {
        return View();
    }

    public async Task<IActionResult> Snake()
    {
        var snakeDto = await GetDataFromApiAsync<SnakeDto>("https://uselessfacts.jsph.pl/api/v2/facts/random?language=en");
        var model = new SnakeViewModel
        {
            SnakeDto = snakeDto ?? new SnakeDto { Text = "Информация о змее временно недоступна." }
        };

        return View(model);
    }

    public async Task<IActionResult> Crater()
    {
        var diglettDto = await _apiHelper.GetDataFromApiAsync<CraterApiDto>("https://pokeapi.co/api/v2/pokemon/diglett"); // получвем данные о Diglett(=подземный покемон) и сохраняем объект
        return View(diglettDto);
    }

    public IActionResult CoinInfo()
    {
        return RedirectToAction(nameof(CellInfo), new { type = "Coin" });
    }
    public IActionResult Thief()
    {
        return View();
    }
    private async Task GetApiDndClassAndDamageType(AmongusViewModel amongus)
    {
        var damageTypeTask = GetDataFromApiDndAsync<DamageTypeInfo>("https://www.dnd5eapi.co/api/2014/damage-types/acid");
        var dndClassTask = GetDataFromApiDndAsync<ClassDnDInfo>("https://www.dnd5eapi.co/api/2014/classes/barbarian");

        await Task.WhenAll(dndClassTask, damageTypeTask);
        var damageType = await damageTypeTask;
        var dndClass = await dndClassTask;
        amongus.Class = dndClass;
        amongus.DamageType = damageType;
    }

    [HttpGet]
    public async Task<IActionResult> VodkaBarInfo()
    {
        using var client = new HttpClient();

        // два асинхронных запрос на апишки для Водка бара
        var cocktailTask = client.GetFromJsonAsync<CocktailApiResponse>("https://www.thecocktaildb.com/api/json/v1/1/random.php");
        var chuckTask = client.GetFromJsonAsync<ChuckJokeApiResponse>("https://api.chucknorris.io/jokes/random");

        // ждем выполнения обоих тасок
        await Task.WhenAll(cocktailTask, chuckTask);

        var viewModel = new VodkaBarViewModel
        {
            CurrentDrink = cocktailTask.Result?.Drinks?.FirstOrDefault(),
            ChuckTost = chuckTask.Result?.Value
        };

        return View("VodkaBarInfo", viewModel);
    }

    private async Task<T> GetDataFromApiDndAsync<T>(string url)
    {
        var http = new HttpClient();
        var task = http.GetAsync(url);
        var result = await task;
        var taskDto = await result.Content.ReadFromJsonAsync<T>();
        return taskDto;
    }
    [HttpGet]
    public async Task<IActionResult> CellInfo(string type)
    {
        var info = CellCodex.Find(type);
        if (info is null)
        {
            return NotFound();
        }
        if (info.TypeKey == "Amongus")
        {
            var amongus = new AmongusViewModel();
            amongus.CellInfo = info;
            await GetApiDndClassAndDamageType(amongus);
            return View("AmongUsCellInfo", amongus);
        }


        if (info.TypeKey == "PaidDoor")
        {
            var http = new HttpClient();
            var response = await http.GetAsync("https://api.frankfurter.dev/v2/rate/XAU/USD");
            ViewBag.CurrencyRate = await response.Content.ReadFromJsonAsync<CurrencyRateDto>();
        }

        // иначе все время кидает на заглушку
        if (type == "VodkaBar")
        {
            return RedirectToAction("VodkaBarInfo");
        }

        if (info.TypeKey == "HealthPotion")
        {
            var fox = await GetDataFromApiAsync<FoxDto>("https://randomfox.ca/floof/");
            ViewData["FoxImage"] = fox?.Image;
        }

        if (type == "Flower")
        {
            ViewBag.Flower = await _flowerApi.GetFlower();
        }

        if (type == "Snake")
        {
            ViewBag.Snake = await GetDataFromApiAsync<SnakeDto>("https://uselessfacts.jsph.pl/api/v2/facts/random?language=en");
        }
        return View(info);
    }


    private async Task<T> GetDataFromApiAsync<T>(string url)
    {
        var response = await _http.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<IActionResult> Ice() // асинхронный метод, который будет вызываться при переходе на страницу Ice
    {
        // запускаем оба запроса
        var affirmationTask = _iceApiDataService.GetDataFromApiAsync<AffirmationDto>("https://www.affirmations.dev/");
        var dogImageTask = _iceApiDataService.GetDataFromApiAsync<DogImageDto>("https://dog.ceo/api/breeds/image/random");
        //ждем, когда получим все ответы
        await Task.WhenAll(affirmationTask, dogImageTask);
        //складываем результаты в модель, которая будет передана на страницу
        var viewIceModel = new IceViewModel
        {
            AffirmationDto = affirmationTask.Result,
            DogImageDto = dogImageTask.Result,
        };
        //возвращаем на страницу модель, которая содержит данные с обоих API
        return View(viewIceModel);
    }
    public IActionResult Dirt()
    {
        return View();
    }

    public IActionResult PileOfSand()
    {
        return View();
    }

    [HttpGet]
    public IActionResult State()
    {
        var game = _store.GetOrCreate(GetSessionId());
        return Json(MazeStateMapper.ToDto(game));
    }

    //[HttpPost]
    public IActionResult NewGame()
    {
        var game = _store.Restart(GetSessionId());
        return Json(MazeStateMapper.ToDto(game));
    }

    [HttpPost]
    public IActionResult Move([FromBody] MazeMoveRequest request)
    {
        var sessionId = GetSessionId();
        var game = _store.GetOrCreate(sessionId);

        if (!game.IsAlive)
        {
            return Json(MazeStateMapper.ToDto(game));
        }

        if (!TryParseAction(request.Action, out var action))
        {
            return BadRequest(new { error = $"Unknown action: {request.Action}" });
        }

        try
        {
            game.PerformAction(action);
            return Json(MazeStateMapper.ToDto(game));
        }
        catch (Exception ex)
        {
            return Json(MazeStateMapper.ToDto(game, isFailed: true, errorMessage: ex.Message));
        }
    }

    [HttpPost]
    public IActionResult SaveGame()
    {
        var sessionId = GetSessionId();
        var game = _store.GetOrCreate(sessionId);

        try
        {
            _saveService.Save(game, sessionId);
            game.Maze.LogMessages.Add("Лабиринт сохранён.");
            return Json(MazeStateMapper.ToDto(game));
        }
        catch (Exception ex)
        {
            game.Maze.LogMessages.Add($"Не удалось сохранить лабиринт: {ex.Message}");
            return Json(MazeStateMapper.ToDto(game));
        }
    }

    [HttpPost]
    public IActionResult LoadGame()
    {
        var sessionId = GetSessionId();

        MazeContoller? loadedGame;
        try
        {
            loadedGame = _saveService.Load(sessionId);
        }
        catch (Exception ex)
        {
            var current = _store.GetOrCreate(sessionId);
            current.Maze.LogMessages.Add($"Не удалось загрузить лабиринт: {ex.Message}");
            return Json(MazeStateMapper.ToDto(current));
        }

        if (loadedGame is null)
        {
            var current = _store.GetOrCreate(sessionId);
            current.Maze.LogMessages.Add("Нет сохранённого лабиринта.");
            return Json(MazeStateMapper.ToDto(current));
        }

        _store.Set(sessionId, loadedGame);
        loadedGame.Maze.LogMessages.Add("Лабиринт загружен.");
        return Json(MazeStateMapper.ToDto(loadedGame));
    }

    private string GetSessionId()
    {
        HttpContext.Session.SetString("maze-ready", "1");
        return HttpContext.Session.Id;
    }

    private static bool TryParseAction(string? action, out UserAction userAction)
    {
        userAction = default;
        if (string.IsNullOrWhiteSpace(action))
        {
            return false;
        }

        switch (action.Trim().ToLowerInvariant())
        {
            case "up":
            case "stepup":
                userAction = UserAction.StepUp;
                return true;
            case "down":
            case "stepdown":
                userAction = UserAction.StepDown;
                return true;
            case "left":
            case "stepleft":
                userAction = UserAction.StepLeft;
                return true;
            case "right":
            case "stepright":
                userAction = UserAction.StepRight;
                return true;
            default:
                return false;
        }
    }
}
