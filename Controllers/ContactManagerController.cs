using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    public class ContactManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
