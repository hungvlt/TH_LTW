using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Thêm thư viện này
using Web_Core.Models;
using Web_Core.Repositories;

namespace Web_Core.Controllers
{
   public class CategoriesController : Controller
   {
      private readonly IProductRepository _productRepository;
      private readonly ICategoryRepository _categoryRepository;

      public CategoriesController(IProductRepository productRepository, ICategoryRepository categoryRepository)
      {
         _productRepository = productRepository;
         _categoryRepository = categoryRepository;
      }

      // Ai cũng có thể xem danh sách danh mục
      public async Task<IActionResult> Index()
      {
         var categories = await _categoryRepository.GetAllAsync();
         return View(categories);
      }

      // Ai cũng có thể xem chi tiết danh mục
      public async Task<IActionResult> Display(int id)
      {
         var category = await _categoryRepository.GetByIdAsync(id);
         if (category == null)
         {
            return NotFound();
         }
         return View(category);
      }

      // Chỉ Admin và Employee được thêm danh mục
      [Authorize(Roles = "Admin,Employee")]
      public IActionResult Add()
      {
         return View();
      }

      [HttpPost]
      [Authorize(Roles = "Admin,Employee")]
      public async Task<IActionResult> Add(Category category)
      {
         if (ModelState.IsValid)
         {
            await _categoryRepository.AddAsync(category);
            return RedirectToAction(nameof(Index));
         }
         return View(category);
      }

      // Chỉ Admin và Employee được cập nhật danh mục
      [Authorize(Roles = "Admin,Employee")]
      public async Task<IActionResult> Update(int id)
      {
         var category = await _categoryRepository.GetByIdAsync(id);
         if (category == null)
         {
            return NotFound();
         }
         return View(category);
      }

      [HttpPost]
      [Authorize(Roles = "Admin,Employee")]
      public async Task<IActionResult> Update(int id, Category category)
      {
         if (id != category.Id)
         {
            return NotFound();
         }
         if (ModelState.IsValid)
         {
            await _categoryRepository.UpdateAsync(category);
            return RedirectToAction(nameof(Index));
         }
         return View(category);
      }

      // Chỉ Admin và Employee được xóa danh mục
      [Authorize(Roles = "Admin,Employee")]
      public async Task<IActionResult> Delete(int id)
      {
         var category = await _categoryRepository.GetByIdAsync(id);
         if (category == null)
         {
            return NotFound();
         }
         return View(category);
      }

      [HttpPost, ActionName("DeleteConfirmed")]
      [Authorize(Roles = "Admin,Employee")]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var category = await _categoryRepository.GetByIdAsync(id);
         if (category != null)
         {
            await _categoryRepository.DeleteAsync(id);
         }
         return RedirectToAction(nameof(Index));
      }
   }
}
