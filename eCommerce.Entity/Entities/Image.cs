﻿using eCommerce.Core.Entities;

namespace eCommerce.Entity.Entities
{
    public class Image : EntityBase
    {
        public string NameWithPath { get; set; }
        public string Extension { get; set; }
    }
}
