using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Core.Models;
using Web_Core.Repositories;

namespace Web_Core.Controllers
{
   public class HomeController : Controller
   {
      private readonly IProductRepository _productRepository;

      public HomeController(IProductRepository productRepository)
      {
         _productRepository = productRepository;
      }

      public async Task<IActionResult> Index()
      {
         var products = await _productRepository.GetAllAsync();
         return View(products);
      }

      //public IActionResult Index()
      //{
      //   return View();
      //}

      public IActionResult Reservation()
      {
         return View();
      }

      [HttpPost]
      public IActionResult Reservation(string Name, string Phone, string Time, string Date, int NumberOfPeople, string Message)
      {
         if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Phone) && !string.IsNullOrEmpty(Date) && NumberOfPeople > 0)
         {
            ViewBag.Message = "Đặt bàn thành công!";
         }
         else
         {
            ViewBag.Message = "Vui lòng điền đầy đủ thông tin.";
         }
         return View();
      }

      [HttpPost]
      public IActionResult Contact(string Name, string Email, string Message)
      {
         if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Message))
         {
            ViewBag.Message = "Tin nhắn của bạn đã được gửi thành công!";
         }
         else
         {
            ViewBag.Message = "Vui lòng điền đầy đủ các trường.";
         }
         return View("Index");
      }
   }
}
