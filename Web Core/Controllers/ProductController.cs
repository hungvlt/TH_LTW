using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Thêm thư viện này
using Web_Core.Models;
using Web_Core.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web_Core.Controllers
{
   public class ProductController : Controller
   {
      private readonly IProductRepository _productRepository;
      private readonly ICategoryRepository _categoryRepository;

      public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
      {
         _productRepository = productRepository;
         _categoryRepository = categoryRepository;
      }

      // Hiển thị danh sách sản phẩm (Mọi người đều xem được)
      public async Task<IActionResult> Index(int? categoryId, int page = 1, int pageSize = 10)
      {
         var products = await _productRepository.GetAllAsync();
         var categories = await _categoryRepository.GetAllAsync();

         if (categoryId.HasValue)
         {
            products = products.Where(p => p.CategoryId == categoryId).ToList();
         }

         int totalProducts = products.Count();
         int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
         var paginatedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

         ViewBag.Categories = new SelectList(categories, "Id", "Name");
         ViewBag.SelectedCategory = categoryId;
         ViewBag.CurrentPage = page;
         ViewBag.TotalPages = totalPages;

         return View(paginatedProducts);
      }

      // Chỉ Admin và Employee được thêm sản phẩm
      [Authorize(Roles = "Admin,Employee")]
      public async Task<IActionResult> Add()
      {
         var categories = await _categoryRepository.GetAllAsync();
         ViewBag.Categories = new SelectList(categories, "Id", "Name");
         return View();
      }

      [HttpPost]
      [Authorize(Roles = "Admin,Employee")]
      public async Task<IActionResult> Add(Product product, IFormFile imageUrl)
      {
         if (ModelState.IsValid)
         {
            if (imageUrl != null)
            {
               product.ImageUrl = await SaveImage(imageUrl);
            }
            await _productRepository.AddAsync(product);
            return RedirectToAction(nameof(Index));
         }
         var categories = await _categoryRepository.GetAllAsync();
         ViewBag.Categories = new SelectList(categories, "Id", "Name");
         return View(product);
      }

      private async Task<string> SaveImage(IFormFile image)
      {
         var savePath = Path.Combine("wwwroot/images/product_image", image.FileName);
         using (var fileStream = new FileStream(savePath, FileMode.Create))
         {
            await image.CopyToAsync(fileStream);
         }
         return "/images/product_image/" + image.FileName;
      }

      // Mọi người đều có thể xem sản phẩm
      public async Task<IActionResult> Display(int id)
      {
         var product = await _productRepository.GetByIdAsync(id);
         if (product == null) return NotFound();
         return View(product);
      }

      // Chỉ Admin và Employee được cập nhật sản phẩm
      [Authorize(Roles = "Admin,Employee")]
      public async Task<IActionResult> Update(int id)
      {
         var product = await _productRepository.GetByIdAsync(id);
         if (product == null) return NotFound();

         var categories = await _categoryRepository.GetAllAsync();
         ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
         return View(product);
      }

      [HttpPost]
      [Authorize(Roles = "Admin,Employee")]
      public async Task<IActionResult> Update(int id, Product product, IFormFile imageUrl)
      {
         ModelState.Remove("ImageUrl");
         if (id != product.Id) return NotFound();

         if (ModelState.IsValid)
         {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (imageUrl == null)
            {
               product.ImageUrl = existingProduct.ImageUrl;
            }
            else
            {
               product.ImageUrl = await SaveImage(imageUrl);
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.ImageUrl = product.ImageUrl;

            await _productRepository.UpdateAsync(existingProduct);
            return RedirectToAction(nameof(Index));
         }

         var categories = await _categoryRepository.GetAllAsync();
         ViewBag.Categories = new SelectList(categories, "Id", "Name");
         return View(product);
      }

      // Chỉ Admin và Employee được xóa sản phẩm
      [Authorize(Roles = "Admin,Employee")]
      public async Task<IActionResult> Delete(int id)
      {
         var product = await _productRepository.GetByIdAsync(id);
         if (product == null) return NotFound();
         return View(product);
      }

      [HttpPost, ActionName("DeleteConfirmed")]
      [Authorize(Roles = "Admin,Employee")]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         await _productRepository.DeleteAsync(id);
         return RedirectToAction(nameof(Index));
      }
   }
}