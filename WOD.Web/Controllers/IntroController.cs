using Microsoft.AspNetCore.Mvc;

namespace WOD.Web.Controllers
{
    public class IntroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}