using Microsoft.AspNetCore.Mvc;

namespace SimulationDay6.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
