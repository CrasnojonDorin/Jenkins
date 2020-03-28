

//using System.Collections.Generic;
//using System.Linq;
//using AutoMapper;
//using Moq;
//using WebStore.Models;
//using WebStore.Models.DTO;
//using Xunit;

//namespace WebStore.Tests.Controller.API
//{
//    public class ProductControllerShould
//    {
//        private readonly Mock<StoreContext> _mockStoreContext;
//        private readonly Mock<IMapper> _mockIMapper;

//        private readonly List<Product> _products;
//        private readonly List<ProductDTO> _productsDTO;



//        public ProductControllerShould()
//        {
//            _mockStoreContext= new Mock<StoreContext>();
//            _mockIMapper = new Mock<IMapper>();
//            _products = new List<Product>
//            {
//                new Product{BrandId = 1, Name = "Test product1", ColorId = 1, Description = "Test description1",
//                    Id = 1, PhotoPath = "Test path1", Price = 9.99, SexId = 1, SizeId=1,TypeId = 1},
//                new Product{BrandId = 2, Name = "Test product2", ColorId = 2, Description = "Test description2",
//                    Id = 2, PhotoPath = "Test path2", Price = 19.99, SexId = 2, SizeId=2,TypeId = 2},
//                new Product{BrandId = 3, Name = "Test product3", ColorId = 3, Description = "Test description3",
//                    Id = 3, PhotoPath = "Test path3", Price = 39.99, SexId = 3, SizeId=3,TypeId = 3}
//            };

//            _productsDTO = new List<ProductDTO>
//            {
//                new ProductDTO {BrandId = 1, Name = "Test product1", ColorId = 1, Description = "Test description1",
//                    Id = 1, PhotoPath = "Test path1", Price = 9.99, SexId = 1, TypeId = 1},
//                new ProductDTO{BrandId = 2, Name = "Test product2", ColorId = 2, Description = "Test description2",
//                    Id = 2, PhotoPath = "Test path2", Price = 19.99, SexId = 2, SizeId=2,TypeId = 2},
//                new ProductDTO{BrandId = 3, Name = "Test product3", ColorId = 3, Description = "Test description3",
//                    Id = 3, PhotoPath = "Test path3", Price = 39.99, SexId = 3, SizeId=3,TypeId = 3}
//            };
//        }


//        [Fact]
//        public void ReturnValues()
//        {
//            _mockStoreContext.Setup(
//                    x => x.Products)
//                .Returns(_products);

//            _mockIMapper.Setup(
//                    x => x.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(It.IsAny<IEnumerable<Product>>()))
//                .Returns(_productsDTO);

//            Assert.Equal(_products.Count, _productsDTO.Count);

//            for (int i = 0; i < _products.Count; i++)
//            {
//                Assert.Equal(_products.ElementAt(0).Name, _productsDTO.ElementAt(0).Name);
//                Assert.Equal(_products.ElementAt(0).TypeId, _productsDTO.ElementAt(0).TypeId);
//                Assert.Equal(_products.ElementAt(0).Name, _productsDTO.ElementAt(0).Description);
//                Assert.Equal(_products.ElementAt(0).Name, _productsDTO.ElementAt(0).PhotoPath);
//                Assert.Equal(_products.ElementAt(0).Price, _productsDTO.ElementAt(0).Price);
//                Assert.Equal(_products.ElementAt(0).Id, _productsDTO.ElementAt(0).Id);
//                Assert.Equal(_products.ElementAt(0).ColorId, _productsDTO.ElementAt(0).ColorId);
//                Assert.Equal(_products.ElementAt(0).SexId, _productsDTO.ElementAt(0).SexId);
//                Assert.Equal(_products.ElementAt(0).SizeId, _productsDTO.ElementAt(0).SizeId);
//                Assert.Equal(_products.ElementAt(0).BrandId, _productsDTO.ElementAt(0).BrandId);
//            }
//        }
//    }
//}
