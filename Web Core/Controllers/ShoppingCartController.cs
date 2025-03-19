using Microsoft.AspNetCore.Mvc;

namespace Web_Core.Controllers
{
   public class ShoppingCartController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }
   }
}
