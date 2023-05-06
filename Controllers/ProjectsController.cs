using Microsoft.AspNetCore.Mvc;

namespace AltruCode.Controllers;

public class ProjectsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddProject()
    {
        return View();
    }
}