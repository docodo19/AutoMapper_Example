using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day26_AutoMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User
            {
                Id = 1,
                Name = "Bob",
                IsActive = true,
                MemberSince = new DateTime(2005, 8, 19),
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Laptop", Price = 999.99m, IsActive = true },
                    new Product { Id = 2, Name = "Mouse", Price = 5m, IsActive = true },
                    new Product { Id = 3, Name = "Desk", Price = 100.00m, IsActive = true },
                }
            };

            var data = MapUtility.Map<User, UserDto>(user);

            Console.ReadLine();
        }
    }

    class MapUtility
    {
        static public T2 Map<T1, T2>(T1 obj)
        {
            // Configure Mapper 
            var config = new MapperConfiguration(cfg => {
                // Create a map between Product and ProductDto
                cfg.CreateMap<Product, ProductDto>();
                // Create a map between User and UserDto
                cfg.CreateMap<User, UserDto>().ForMember(
                    member => member.ProductDtos, // For UserDto.ProductDtos....(see line below)
                    opt => opt.MapFrom(src => src.Products // Load UserDto.ProductDtos with information in User.Products
                ));
            });

            // Create Mapper
            var mapper = config.CreateMapper();
            // Map it!
            return mapper.Map<T2>(obj);
        }
    }


    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime MemberSince { get; set; }
        public ICollection<Product> Products { get; set; }
    }

    class UserDto
    {
        public string Name { get; set; }
        public ICollection<ProductDto> ProductDtos { get; set; }
        
    }

    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }

    class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
