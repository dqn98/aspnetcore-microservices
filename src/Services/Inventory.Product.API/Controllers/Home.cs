using Microsoft.AspNetCore.Mvc;

namespace Inventory.Product.API.Controllers;

public class Home : Controller
{
    // GET
    public IActionResult Index()
    {
        return Redirect("~/swagger");
    }
}