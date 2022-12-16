﻿using eCommerce.Entity.ViewModels.Brand;
using eCommerce.Entity.ViewModels.Category;

namespace eCommerce.Entity.ViewModels.Product
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
        public SimpleBrandViewModel Brand { get; set; }
        public SimpleCategoryViewModel Category { get; set; }
    }
}
