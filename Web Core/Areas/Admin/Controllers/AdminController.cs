using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_Core.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Web_Core.Areas.Admin.Controllers
{
   [Area("Admin")]
   [Authorize(Roles = "Admin")]
   public class AdminController : Controller
   {
      private readonly UserManager<ApplicationUser> _userManager;
      private readonly RoleManager<IdentityRole> _roleManager;

      public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
      {
         _userManager = userManager;
         _roleManager = roleManager;
      }

      // Hiển thị danh sách người dùng
      public async Task<IActionResult> ManageUsers()
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

         ViewBag.Roles = roles;
         ViewBag.UserRoles = userRoles;

         return View(users); // Hiển thị tất cả user
      }

      [HttpPost]
      public async Task<IActionResult> UpdateUserRole(string userId, string roleId)
      {
         var user = await _userManager.FindByIdAsync(userId);
         if (user == null) return NotFound("Người dùng không tồn tại.");

         var role = await _roleManager.FindByIdAsync(roleId);
         if (role == null) return BadRequest("Vai trò không hợp lệ.");

         var currentRoles = await _userManager.GetRolesAsync(user);

         // Nếu user là Admin, không cho phép cập nhật quyền
         if (currentRoles.Contains("Admin"))
         {
            Console.WriteLine($"❌ Không thể thay đổi quyền của Admin: {user.UserName}");
            return BadRequest("Không thể thay đổi quyền của Admin.");
         }

         Console.WriteLine($"🔹 Cập nhật quyền: {user.UserName} - {string.Join(", ", currentRoles)} -> {role.Name}");

         var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
         if (!removeResult.Succeeded)
         {
            Console.WriteLine("❌ Lỗi khi xóa quyền cũ.");
            return BadRequest("Không thể xóa quyền cũ.");
         }

         var addResult = await _userManager.AddToRoleAsync(user, role.Name);
         if (!addResult.Succeeded)
         {
            Console.WriteLine("❌ Lỗi khi thêm quyền mới.");
            return BadRequest("Không thể cập nhật quyền mới.");
         }

         return RedirectToAction("ManageUsers");
      }
   }
}
