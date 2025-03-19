using Microsoft.AspNetCore.Mvc;

namespace Web_Core.Areas.Admin.Controllers
{
   public class OrderController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }
   }
}
