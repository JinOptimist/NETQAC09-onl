using MazeConsole;
using Microsoft.AspNetCore.Mvc;
using WebAppSmile.Models;
using WebAppSmile.Services;

namespace WebAppSmile.Controllers;

public class MazeController : Controller
{
    private readonly MazeGameSessionStore _store;

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
    private async Task<CraterApiDto> GetDataFromApi(string url) //асинхронно через task получает данные из API и возвращает их как объект
    {
        var http = new HttpClient();
        var craterTask = http.GetAsync(url);
        var result = await craterTask;
        var craterDto = await result.Content.ReadFromJsonAsync<CraterApiDto>();
        return craterDto; // объект с данными Diglett (подземный покемон) в action Crater
    }
    public async Task<IActionResult> Crater()
    {
        var diglettDto = await GetDataFromApi("https://pokeapi.co/api/v2/pokemon/diglett"); // получвем данные о Diglett(=подземный покемон) и сохраняем объект
        return View(diglettDto);
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
