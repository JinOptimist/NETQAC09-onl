using MazeConsole;
using Microsoft.AspNetCore.Mvc;
using WebAppSmile.Models;
using WebAppSmile.Services;

namespace WebAppSmile.Controllers;

public class MazeController : Controller
{
    private readonly MazeGameSessionStore _store;
    private readonly IceApiDataService _iceApiDataService = new();

    public MazeController(MazeGameSessionStore store)
    {
        _store = store;
    }

    public IActionResult Index()
    {
        return View(CellCodex.All);
    }
    public IActionResult Flower()
{
    return View();
}

    public IActionResult CoinInfo()
    {
        return RedirectToAction(nameof(CellInfo), new { type = "Coin" });
    }

    [HttpGet]
    public IActionResult CellInfo(string type)
    {
        var info = CellCodex.Find(type);
        if (info is null)
        {
            return NotFound();
        }

        return View(info);
    }

    public async Task<IActionResult> Ice() // асинхронный метод, который будет вызываться при переходе на страницу Ice
    {
        // запускаем оба запроса
        var affirmationTask = _iceApiDataService.GetDataFromApiAsync<AffirmationDto>("https://www.affirmations.dev/");
        var dogImageTask = _iceApiDataService.GetDataFromApiAsync<DogImageDto>("https://dog.ceo/api/breeds/image/random");
        //ждем, когда получим все ответы
        await Task.WhenAll(affirmationTask, dogImageTask);
        //складываем результаты в модель, которая будет передана на страницу
        var viewIceModel = new IceModel
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
