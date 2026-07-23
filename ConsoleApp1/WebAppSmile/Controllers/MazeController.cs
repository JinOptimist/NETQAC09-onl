using MazeConsole;
using Microsoft.AspNetCore.Mvc;
using WebAppSmile.Models;
using WebAppSmile.Services;

namespace WebAppSmile.Controllers;

public class MazeController : Controller
{
    private readonly MazeGameSessionStore _store;

    private readonly HttpClient _http = new();

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
    public async Task<IActionResult> CellInfo(string type)
    {
        var info = CellCodex.Find(type);
        if (info is null)
        {
            return NotFound();
        }

        if (info.TypeKey == "HealthPotion")
        {
            var fox = await GetDataFromApiAsync<FoxDto>("https://randomfox.ca/floof/");
            ViewData["FoxImage"] = fox?.Image;
        }

        return View(info);
    }


    private async Task<T> GetDataFromApiAsync<T>(string url)
    {
        var response = await _http.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
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
