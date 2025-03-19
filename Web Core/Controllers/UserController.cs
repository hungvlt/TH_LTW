//using Microsoft.AspNetCore.Mvc;
//using Web_Core.Models;
//using Web_Core.Repositories;
//using Microsoft.AspNetCore.Hosting;
//using System;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Web_Core.Controllers
//{
//   public class UserController : Controller
//   {
//      private readonly IUserRepository _userRepository;
//      private readonly IWebHostEnvironment _webHostEnvironment;

//      public UserController(IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
//      {
//         _userRepository = userRepository;
//         _webHostEnvironment = webHostEnvironment;
//      }

//      // Hiển thị danh sách người dùng
//      public async Task<IActionResult> Index()
//      {
//         var users = await _userRepository.GetAllAsync();
//         return View(users);
//      }

//      // Hiển thị form thêm người dùng mới
//      public IActionResult Add()
//      {
//         return View();
//      }

//      // Xử lý thêm người dùng mới
//      [HttpPost]
//      public async Task<IActionResult> Add(User user, IFormFile avatarFile)
//      {
//         if (ModelState.IsValid)
//         {
//            if (avatarFile != null)
//            {
//               user.AvatarUrl = await SaveImage(avatarFile);
//            }

//            await _userRepository.AddAsync(user);
//            return RedirectToAction(nameof(Index));
//         }
//         return View(user);
//      }

//      // Hiển thị thông tin chi tiết người dùng
//      public async Task<IActionResult> Display(int id)
//      {
//         var user = await _userRepository.GetByIdAsync(id);
//         if (user == null)
//         {
//            return NotFound();
//         }
//         return View(user);
//      }

//      // Hiển thị form cập nhật thông tin người dùng
//      public async Task<IActionResult> Update(int id)
//      {
//         var user = await _userRepository.GetByIdAsync(id);
//         if (user == null)
//         {
//            return NotFound();
//         }
//         return View(user);
//      }

//      // Xử lý cập nhật thông tin người dùng
//      [HttpPost]
//      [HttpPost]
//      public async Task<IActionResult> Update(int id, User user, IFormFile avatarFile)
//      {
//         ModelState.Remove("AvatarUrl"); // Bỏ qua validation AvatarUrl

//         if (id != user.ID)
//         {
//            return NotFound();
//         }

//         if (ModelState.IsValid)
//         {
//            var existingUser = await _userRepository.GetByIdAsync(id);
//            if (existingUser == null)
//            {
//               return NotFound();
//            }

//            // Nếu có ảnh mới thì lưu, nếu không thì giữ nguyên ảnh cũ
//            if (avatarFile != null)
//            {
//               string newAvatarUrl = await SaveImage(avatarFile);

//               // Xóa ảnh cũ trước khi cập nhật ảnh mới
//               if (!string.IsNullOrEmpty(existingUser.AvatarUrl))
//               {
//                  DeleteImage(existingUser.AvatarUrl);
//               }

//               existingUser.AvatarUrl = newAvatarUrl;
//            }

//            // Cập nhật thông tin người dùng (không cập nhật AvatarUrl nếu không có ảnh mới)
//            existingUser.Name = user.Name;
//            existingUser.Email = user.Email;
//            existingUser.Password = user.Password;
//            existingUser.Role = user.Role;

//            await _userRepository.UpdateAsync(existingUser);
//            return RedirectToAction(nameof(Index));
//         }

//         // Giữ lại avatar cũ trong View nếu không có ảnh mới
//         user.AvatarUrl = (await _userRepository.GetByIdAsync(id))?.AvatarUrl;

//         return View(user);
//      }


//      // Hiển thị form xác nhận xóa người dùng
//      public async Task<IActionResult> Delete(int id)
//      {
//         var user = await _userRepository.GetByIdAsync(id);
//         if (user == null)
//         {
//            return NotFound();
//         }
//         return View(user);
//      }

//      // Xử lý xóa người dùng
//      [HttpPost, ActionName("DeleteConfirmed")]
//      public async Task<IActionResult> DeleteConfirmed(int id)
//      {
//         var user = await _userRepository.GetByIdAsync(id);

//         if (user != null)
//         {
//            if (!string.IsNullOrEmpty(user.AvatarUrl))
//            {
//               DeleteImage(user.AvatarUrl); // Xóa ảnh nếu có
//            }

//            await _userRepository.DeleteAsync(id);
//         }

//         return RedirectToAction(nameof(Index));
//      }


//      // Hàm lưu ảnh lên server
//      private async Task<string> SaveImage(IFormFile image)
//      {
//         if (image == null || image.Length == 0)
//         {
//            return null; // Không lưu nếu ảnh không hợp lệ
//         }

//         // Kiểm tra định dạng file
//         var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
//         var fileExtension = Path.GetExtension(image.FileName).ToLower();

//         if (!allowedExtensions.Contains(fileExtension))
//         {
//            throw new InvalidOperationException("Chỉ chấp nhận ảnh định dạng JPG, PNG hoặc GIF.");
//         }

//         string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "user_image");
//         Directory.CreateDirectory(uploadsFolder); // Tạo thư mục nếu chưa có

//         string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
//         string filePath = Path.Combine(uploadsFolder, uniqueFileName);

//         using (var fileStream = new FileStream(filePath, FileMode.Create))
//         {
//            await image.CopyToAsync(fileStream);
//         }

//         return "/images/user_image/" + uniqueFileName; // Trả về đường dẫn tương đối
//      }

//      private void DeleteImage(string imageUrl)
//      {
//         if (string.IsNullOrEmpty(imageUrl))
//         {
//            return;
//         }

//         // Đảm bảo đường dẫn không chứa ký tự không hợp lệ
//         string filePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl.TrimStart('/'));

//         if (System.IO.File.Exists(filePath))
//         {
//            System.IO.File.Delete(filePath);
//         }
//      }

//   }
//}
