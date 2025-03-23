using System.ComponentModel.DataAnnotations;

namespace Web_Core.Areas.Admin.Models
{
   public class RegisterViewModel
   {
      [Required(ErrorMessage = "Họ tên không được để trống.")]
      public string FullName { get; set; }

      [Required(ErrorMessage = "Email không được để trống.")]
      [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
      public string Email { get; set; }

      [Required(ErrorMessage = "Mật khẩu không được để trống.")]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống.")]
      [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
      [DataType(DataType.Password)]
      public string ConfirmPassword { get; set; }
   }
}
