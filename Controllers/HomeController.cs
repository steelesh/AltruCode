using Microsoft.AspNetCore.Mvc;

namespace AltruCode.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}