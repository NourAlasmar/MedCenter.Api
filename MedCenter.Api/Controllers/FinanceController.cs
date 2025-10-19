using Microsoft.AspNetCore.Mvc;

namespace MedCenter.Api.Controllers
{
    public class FinanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
