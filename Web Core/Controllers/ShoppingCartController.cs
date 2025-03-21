using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_Core.Extensions;
using Web_Core.Models;
using Web_Core.Repositories;

namespace Web_Core.Controllers
{
   [Authorize]
   public class ShoppingCartController : Controller
   {
      private readonly IProductRepository _productRepository;
      private readonly ApplicationDbContext _context;
      private readonly UserManager<ApplicationUser> _userManager;

      private const string CartSessionKey = "Cart";

      public ShoppingCartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IProductRepository productRepository)
      {
         _productRepository = productRepository;
         _context = context;
         _userManager = userManager;
      }

      // Hiển thị giỏ hàng
      public IActionResult Index()
      {
         var cart = GetCart();
         return View(cart);
      }

      // Thêm sản phẩm vào giỏ hàng (mặc định số lượng = 1)
      public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
      {
         var product = await _productRepository.GetByIdAsync(productId);
         if (product == null) return NotFound();

         var cart = GetCart();
         cart.AddItem(new CartItem
         {
            ProductId = productId,
            Name = product.Name,
            Price = product.Price,
            Quantity = quantity
         });

         SaveCart(cart);
         return RedirectToAction("Index");
      }

      // Xóa sản phẩm khỏi giỏ hàng
      public IActionResult RemoveFromCart(int productId)
      {
         var cart = GetCart();
         cart.RemoveItem(productId);
         SaveCart(cart);
         return RedirectToAction("Index");
      }

      // Tăng số lượng sản phẩm
      public IActionResult IncreaseQuantity(int productId)
      {
         var cart = GetCart();
         var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
         if (item != null)
         {
            item.Quantity++;
         }
         SaveCart(cart);
         return RedirectToAction("Index");
      }

      // Giảm số lượng sản phẩm (không giảm dưới 1)
      public IActionResult DecreaseQuantity(int productId)
      {
         var cart = GetCart();
         var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
         if (item != null && item.Quantity > 1)
         {
            item.Quantity--;
         }
         SaveCart(cart);
         return RedirectToAction("Index");
      }

      // Hiển thị trang thanh toán
      public IActionResult Checkout()
      {
         return View(new Order());
      }

      // Xử lý đơn hàng khi đặt hàng
      [HttpPost]
      public async Task<IActionResult> Checkout(Order order)
      {
         var cart = GetCart();
         if (!cart.Items.Any()) return RedirectToAction("Index");

         var user = await _userManager.GetUserAsync(User);
         if (user == null) return Unauthorized();

         order.UserId = user.Id;
         order.OrderDate = DateTime.UtcNow;
         order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity);
         order.OrderDetails = cart.Items.Select(i => new OrderDetail
         {
            ProductId = i.ProductId,
            Quantity = i.Quantity,
            Price = i.Price
         }).ToList();

         _context.Orders.Add(order);
         await _context.SaveChangesAsync();

         ClearCart();
         return View("OrderCompleted", order.Id);
      }

      // Lấy giỏ hàng từ Session
      private ShoppingCart GetCart()
      {
         return HttpContext.Session.GetObjectFromJson<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();
      }

      // Lưu giỏ hàng vào Session
      private void SaveCart(ShoppingCart cart)
      {
         HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
      }

      // Xóa giỏ hàng khi hoàn tất đặt hàng
      private void ClearCart()
      {
         HttpContext.Session.Remove(CartSessionKey);
      }
   }
}
