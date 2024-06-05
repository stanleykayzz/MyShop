﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyShop.Infrastructure.Models;

public partial class MyShopDbContext : DbContext
{
    public MyShopDbContext(DbContextOptions<MyShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__price__B40CC6CDAFEC334F");

            entity.ToTable("price");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.Price1)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Price");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__product__B40CC6CDE1714549");

            entity.ToTable("product");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.ProductBrand)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ProductSize)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__stock__B40CC6CDE758BA1D");

            entity.ToTable("stock");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}