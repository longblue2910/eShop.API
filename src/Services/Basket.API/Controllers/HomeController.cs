using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllerss;

public class HomeController : ControllerBase
{
    public IActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
