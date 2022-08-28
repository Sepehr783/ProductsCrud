using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductCrud.Data;
using ProductCrud.Models;

namespace ProductCrud.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult SortBy()
    {
        var result = _db.Mobiles.OrderBy(m => m.MobileName);
        return View("Index", result);
    }

    public IActionResult? SortByDesc()
    {
        var result = _db.Mobiles.OrderByDescending(m => m.Price);
        return View("Index", result);
    }

    public IActionResult Index()
    {
        var mobileList = _db.Mobiles.ToList();
        return View(mobileList);
    }

    [HttpGet]
    public IActionResult Search(string q)
    {
        IEnumerable<Mobile> result;
        if (!string.IsNullOrWhiteSpace(q))
        {
            result = _db.Mobiles.Where(m => m.MobileName.Contains(q)).ToList();
        }
        else
        {
            result = _db.Mobiles.ToList();
        }

        return View("Index", result);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}