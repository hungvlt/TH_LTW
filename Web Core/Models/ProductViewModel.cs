using System.Collections.Generic;

namespace Web_Core.Models
{
   public class ProductViewModel
   {
      public List<Category> Categories { get; set; } // Danh sách danh mục
      public List<Product> Products { get; set; }    // Danh sách sản phẩm
      public int? SelectedCategoryId { get; set; }   // ID của danh mục đang được chọn
   }
}
