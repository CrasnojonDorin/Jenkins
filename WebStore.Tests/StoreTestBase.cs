using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Tests
{
    public class StoreTestBase : IDisposable
    {
        protected readonly StoreContext _context;

        public StoreTestBase()
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new StoreContext(options);

            _context.Database.EnsureCreated();
            Seed();
        }

        public void Seed()
        {
            var products = new List<Product>
            {
                new Product
                {
                    BrandId = 1, Name = "Some Kicks", ColorId = 1, Description = "Some description about kicks",
                    Id = 100, PhotoPath = "Path to kicks image", Price = 9.99, SexId = 1, SizeId = 1, TypeId = 1
                },
                new Product
                {
                    BrandId = 2, Name = "Some t-shirt", ColorId = 2, Description = "Some t-shirt description",
                    Id = 101, PhotoPath = "Path to t-shirt image", Price = 19.99, SexId = 2, SizeId = 2, TypeId = 2
                },
                new Product
                {
                    BrandId = 3, Name = "Some panties", ColorId = 3, Description = "Some panties description",
                    Id = 102, PhotoPath = "Path to panties image", Price = 39.99, SexId = 3, SizeId = 3, TypeId = 2
                }
            };

            foreach (var product in products)
            {
                _context.Products.Add(product);
            }

            _context.SaveChanges();
        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();

            _context.Dispose();
        }


    }
}
