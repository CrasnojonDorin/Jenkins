using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Models
{
    public class StoreContext : IdentityDbContext<User>
    {

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public override DbSet<User> Users { get; set; }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Product> Products{ get; set; }
        public virtual DbSet<Sex> Sexes { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<Size> Sizes{ get; set; }



        public StoreContext()
        {
            
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            :base(options)
        {
            
        }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(c =>
            {
                c.HasKey(c => c.Id);
                c.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Product>(c =>
            {
                c.Property(p => p.Id)
                    .ValueGeneratedOnAdd();
            });


           // Dodanie przykładowych produktów do tabeli Products
            modelBuilder.Entity<Product>()
                .HasData(
                    new Product
                    {
                        Id=1,
                        BrandId = 3,
                        ColorId = 2,
                        SexId = 3,
                        SizeId = 3,
                        TypeId = 1,
                        Name = "Air Max 97",
                        Price = 399.99,
                        Description = "But z 97 roku!",
                        PhotoPath = "nikeairmax97.png"
                    },
                    new Product
                    {
                        Id = 2,
                        BrandId = 3,
                        ColorId = 1,
                        SexId = 1,
                        SizeId = 2,
                        TypeId = 1,
                        Name = "Cortez",
                        Price = 199.99,
                        Description = "Klasyk noszony przez Forresta Gumpa!",
                        PhotoPath = "nikecortez.png"
                    },
                    new Product
                    {
                        Id = 3,
                        BrandId = 1,
                        ColorId = 6,
                        SexId = 2,
                        SizeId = 4,
                        TypeId = 1,
                        Name = "30",
                        Price = 599.99,
                        Description = "Kolejny model butów od najlepszego koszykarza w historii!",
                        PhotoPath = "jordan30.png"
                    },
                    new Product
                    {
                        Id = 4,
                        BrandId = 4,
                        ColorId = 6,
                        SexId = 1,
                        SizeId = 9,
                        TypeId = 2,
                        Name = "Bogo Red",
                        Price = 999.99,
                        Description = "Najpopularnieszy model bluzy Supreme!",
                        PhotoPath = "bogored.png"
                    },
                    new Product
                    {
                        Id = 5,
                        BrandId = 4,
                        ColorId = 1,
                        SexId = 1,
                        SizeId = 8,
                        TypeId = 2,
                        Name = "Buju Banton Tee",
                        Price = 599.99,
                        Description = "Świetny T-Shirt od Supreme!",
                        PhotoPath = "bujubanton.png"
                    },
                    new Product
                    {
                        Id = 6,
                        BrandId = 4,
                        ColorId = 1,
                        SexId = 3,
                        SizeId = 7,
                        TypeId = 2,
                        Name = "Camp Cap",
                        Price = 199.99,
                        Description = "Czarna czapka od Supreme!",
                        PhotoPath = "supremecapblack.png"
                    },
                    new Product
                    {
                        Id = 7,
                        BrandId = 2,
                        ColorId = 1,
                        SexId = 2,
                        SizeId = 1,
                        TypeId = 1,
                        Name = "Neo White",
                        Price = 299.99,
                        Description = "Białe adidasy od Adidasa!",
                        PhotoPath = "adidasneowhite.png"
                    }

                );


            //Dodanie płci do tabeli Gender
            modelBuilder.Entity<Gender>()
                .HasData(
                    new Gender
                    {
                        Id = 1,
                        Name = "Mężczyzna"
                    },
                    new Gender
                    {
                        Id = 2,
                        Name = "Kobieta"
                    },
                    new Gender
                    {
                        Id = 3,
                        Name = "Nieznany"
                    }
                );

            //Dodanie płci do tabeli Sex
            modelBuilder.Entity<Sex>()
                .HasData(
                    new Gender
                    {
                        Id = 1,
                        Name = "Mężczyzna"
                    },
                    new Gender
                    {
                        Id = 2,
                        Name = "Kobieta"
                    },
                    new Gender
                    {
                        Id = 3,
                        Name = "Unisex"
                    }
                );

            //Dodanie typów do tabeli Type
            modelBuilder.Entity<Type>()
                .HasData(
                    new Type
                    {
                        Id = 1,
                        Name = "Obuwie"
                    },
                    new Type
                    {
                        Id = 2,
                        Name = "Odzież"
                    }
                );

            //Dodanie rozmiarów do tabeli Size
            modelBuilder.Entity<Size>()
                .HasData(
                    new Size
                    {
                        Id = 1,
                        Name = "10",
                        TypeId = 1
                        
                    },
                    new Size
                    {
                        Id = 2,
                        Name = "11",
                        TypeId = 1

                    },
                    new Size
                    {
                        Id = 3,
                        Name = "12",
                        TypeId = 1

                    },
                    new Size
                    {
                        Id = 4,
                        Name = "13",
                        TypeId = 1

                    },
                    new Size
                    {
                        Id = 5,
                        Name = "14",
                        TypeId = 1

                    },
                    new Size
                    {
                        Id =6,
                        Name = "S",
                        TypeId = 2

                    },
                    new Size
                    {
                        Id = 7,
                        Name = "M",
                        TypeId = 2

                    },
                    new Size
                    {
                        Id = 8,
                        Name = "L",
                        TypeId = 2

                    },
                    new Size
                    {
                        Id = 9,
                        Name = "XL",
                        TypeId = 2

                    }
                );

            //Dodanie marek do tabeli Brand
            modelBuilder.Entity<Brand>()
                .HasData(
                    new Brand
                    {
                        Id = 1,
                        Name = "Jordan",
                        LogoPath = "jordan.png"
                    },
                    new Brand
                    {
                        Id = 2,
                        Name = "Adidas",
                        LogoPath = "adidas.png"
                    },
                    new Brand
                    {
                        Id = 3,
                        Name = "Nike",
                        LogoPath = "nike.png"
                    },
                    new Brand
                    {
                        Id = 4,
                        Name = "Supreme",
                        LogoPath = "supreme.png"
                    }
                );

            //Dodanie kolorów do tabeli Color
            modelBuilder.Entity<Color>()
                .HasData(
                    new Color
                    {
                        Id = 1,
                        Name = "Biały"
                    },
                    new Color
                    {
                        Id = 2,
                        Name = "Czarny"
                    },
                    new Color
                    {
                        Id = 3,
                        Name = "Niebieski"
                    },
                    new Color
                    {
                        Id = 4,
                        Name = "Żółty"
                    },
                    new Color
                    {
                        Id = 5,
                        Name = "Szary"
                    },
                    new Color
                    {
                        Id = 7,
                        Name = "Inny"
                    },
                    new Color
                    {
                        Id = 6,
                        Name = "Red"
                    }
                );


        }
    }
}
