﻿using eCommerce.Core.Entities;

namespace eCommerce.Entity.Entities
{
    public class Cart : EntityBase
    {
        public bool IsVisible { get; set; }


        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public Guid ShoppingSessionId { get; set; }
        public ShoppingSession ShoppingSession { get; set; }
    }
}