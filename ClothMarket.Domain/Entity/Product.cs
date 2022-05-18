﻿using ClothMarket.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothMarket.Domain.Entity
{

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreate { get; set; }
        public Category Category { get; set; }
        public byte[]? Avatar { get; set; }
    }
}
