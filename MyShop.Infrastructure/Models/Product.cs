﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

namespace MyShop.Infrastructure.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductBrand { get; set; }

    public string ProductSize { get; set; }

    public virtual Price Price { get; set; }

    public virtual Stock Stock { get; set; }
}