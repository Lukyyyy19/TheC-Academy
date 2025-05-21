using Microsoft.AspNetCore.Mvc;

namespace MyToDo_List.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}