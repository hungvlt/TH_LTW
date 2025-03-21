using System.Collections.Generic;
using System.Linq;

namespace Web_Core.Models
{
   public class ShoppingCart
   {
      public List<CartItem> Items { get; set; } = new List<CartItem>();

      /// <summary>
      /// Thêm sản phẩm vào giỏ hàng
      /// </summary>
      public void AddItem(CartItem item)
      {
         var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
         if (existingItem != null)
         {
            existingItem.Quantity += item.Quantity;
         }
         else
         {
            Items.Add(item);
         }
      }

      /// <summary>
      /// Xóa một sản phẩm khỏi giỏ hàng theo ID
      /// </summary>
      public void RemoveItem(int productId)
      {
         Items.RemoveAll(i => i.ProductId == productId);
      }

      /// <summary>
      /// Cập nhật số lượng sản phẩm trong giỏ hàng
      /// </summary>
      public void UpdateQuantity(int productId, int newQuantity)
      {
         var item = Items.FirstOrDefault(i => i.ProductId == productId);
         if (item != null)
         {
            if (newQuantity > 0)
            {
               item.Quantity = newQuantity;
            }
            else
            {
               RemoveItem(productId);
            }
         }
      }

      /// <summary>
      /// Lấy tổng số lượng sản phẩm trong giỏ hàng
      /// </summary>
      public int GetTotalQuantity()
      {
         return Items.Sum(i => i.Quantity);
      }

      /// <summary>
      /// Tính tổng giá trị của giỏ hàng
      /// </summary>
      public decimal GetTotalPrice()
      {
         return Items.Sum(i => i.Quantity * i.Price);
      }

      /// <summary>
      /// Kiểm tra xem sản phẩm có trong giỏ hàng không
      /// </summary>
      public bool ContainsProduct(int productId)
      {
         return Items.Any(i => i.ProductId == productId);
      }

      /// <summary>
      /// Xóa toàn bộ giỏ hàng
      /// </summary>
      public void Clear()
      {
         Items.Clear();
      }

      /// <summary>
      /// Lấy danh sách sản phẩm trong giỏ hàng
      /// </summary>
      public List<CartItem> GetItems()
      {
         return Items;
      }
   }
}
