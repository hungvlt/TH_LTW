using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Import Logger
using Web_Core.Areas.Admin.Models;
using Web_Core.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services; // Import EmailSender Service

namespace Web_Core.Areas.Admin.Controllers
{
   [Area("Admin")]
   [Authorize(Roles = "Admin")]
   public class AdminController : Controller
   {
      private readonly UserManager<ApplicationUser> _userManager;
      private readonly RoleManager<IdentityRole> _roleManager;
      private readonly ILogger<AdminController> _logger; // Logger
      private readonly IEmailSender _emailSender; // Email Sender

      public AdminController(
          UserManager<ApplicationUser> userManager,
          RoleManager<IdentityRole> roleManager,
          ILogger<AdminController> logger, // Inject Logger
          IEmailSender emailSender) // Inject EmailSender
      {
         _userManager = userManager;
         _roleManager = roleManager;
         _logger = logger;
         _emailSender = emailSender;
      }

      public async Task<IActionResult> Index(string userId = null, string roleId = null)
      {
         var users = _userManager.Users.ToList();
         var roles = _roleManager.Roles.ToList();
         var userRoles = new Dictionary<string, string>();

         foreach (var user in users)
         {
            var roleNames = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault(r => roleNames.Contains(r.Name));
            if (role != null)
            {
               userRoles[user.Id] = role.Id;
            }
         }

         if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleId))
         {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("Người dùng không tồn tại.");

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return BadRequest("Vai trò không hợp lệ.");

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Contains("Admin"))
            {
               return BadRequest("Không thể thay đổi quyền của Admin.");
            }

            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
               return BadRequest("Không thể xóa quyền cũ.");
            }

            var addResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!addResult.Succeeded)
            {
               return BadRequest("Không thể cập nhật quyền mới.");
            }

            return RedirectToAction("Index");
         }

         ViewBag.Roles = roles;
         ViewBag.UserRoles = userRoles;
         return View(users);
      }

      [HttpGet]
      public IActionResult RegisterEmployee()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> RegisterEmployee(RegisterViewModel model)
      {
         if (!ModelState.IsValid)
         {
            return View(model);
         }

         var user = new ApplicationUser
         {
            FullName = model.FullName,
            UserName = model.Email,
            Email = model.Email
         };

         var result = await _userManager.CreateAsync(user, model.Password);

         if (result.Succeeded)
         {
            // Gán vai trò mặc định là Employee
            var roleResult = await _userManager.AddToRoleAsync(user, "Employee");

            if (!roleResult.Succeeded)
            {
               ModelState.AddModelError("", "Lỗi khi gán vai trò Employee.");
               return View(model);
            }

            // Gửi email xác nhận (nếu cần)
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(model.Email, "Xác nhận tài khoản",
                $"Vui lòng xác nhận tài khoản bằng cách <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>nhấn vào đây</a>.");

            TempData["SuccessMessage"] = "Đăng ký nhân viên thành công!";
            return RedirectToAction("Index");
         }

         foreach (var error in result.Errors)
         {
            ModelState.AddModelError(string.Empty, error.Description);
         }

         return View(model);
      }
   }
}
