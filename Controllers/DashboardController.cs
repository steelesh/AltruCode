using Microsoft.AspNetCore.Mvc;

namespace AltruCode.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}