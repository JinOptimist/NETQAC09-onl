using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppSmile.Models;

namespace WebAppSmile.Controllers;

public class TreeController
{
    public async Task<IActionResult> Index()
    {
        var http = new HttpClient();
        var treeTask = http.GetAsync("https://catfact.ninja/fact");
        var result = await treeTask;
        var treeDto = await result.Content.ReadFromJsonAsync<TreeDto>();

        return View(treeDto);
    }

    private IActionResult View(TreeDto? treeDto)
    {
        throw new NotImplementedException();
    }
}