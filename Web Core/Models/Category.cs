﻿using System.ComponentModel.DataAnnotations;

namespace Web_Core.Models
{
   public class Category
   {
      public int Id { get; set; }
      [Required, StringLength(50)]
      public string Name { get; set; }
      public List<Product>? Products { get; set; }
   }
}
