using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using WebStore.Controllers;
using WebStore.Tests.FakeClasses;
using WebStore.ViewModels.ProductViewModels;
using Xunit;
using Type = WebStore.Models.Type;

namespace WebStore.Tests.Controller
{
    public class ProductControllerShould : StoreTestBase
    {
        private readonly ProductController _sut;

        public ProductControllerShould()
        {
            var mockIWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _sut = new ProductController(_context, mockIWebHostEnvironment.Object);
        }

        [Fact]
        public void ReturnViewForIndex()
        {
            //Act
            IActionResult result = _sut.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForDetailsIfElementExist()
        {
            //Arrange
            var random = new Random();
            
            //Act
            IActionResult result = _sut.Details(random.Next(100,102));

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        //ProductForm Tests
        [Fact]
        public void ReturnViewForProductForm()
        {
            //Act
            IActionResult result = _sut.ProductForm();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewWithDataWhenInvalidModelStateInProductForm()
        {
            //Arrange
            _sut.ModelState.AddModelError("x", "Test Error");

            var productViewModel = new ProductFormViewModel{Name = "Test", BrandId = 2, ColorId = 2, Description = "test",
                SexId = 1, SizeId = 2, TypeId = 2, Price = 9.99};

            //Act
            IActionResult result = _sut.ProductForm(productViewModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ProductFormViewModel>(viewResult.Model);
            Assert.Equal(productViewModel.Name, model.Name);
            Assert.Equal(productViewModel.BrandId, model.BrandId);
            Assert.Equal(productViewModel.ColorId, model.ColorId);
            Assert.Equal(productViewModel.Description, model.Description);
            Assert.Equal(productViewModel.SexId, model.SexId);
            Assert.Equal(productViewModel.Price, model.Price);
            Assert.Equal(productViewModel.TypeId, model.TypeId);
        }

        [Fact]
        public void NotSaveProductWhenModelError()
        {
            //Arrange
            _sut.ModelState.AddModelError("x", "Test Error");

            var productViewModel = new ProductFormViewModel {Name = "NoSaveTest", Price = 20.00,BrandId = 1,ColorId = 1,SexId = 1,SizeId = 1,TypeId = 1,Description = "NoSaveTest"};

            //Act
            _sut.ProductForm(productViewModel);
            var result = _context.Products.FirstOrDefault(x => x.Name.Equals("NoSaveTest"));

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public void SaveProductWhenValidModel()
        {
            //Arrange
            var productViewModel = new ProductFormViewModel() { Name = "SaveTest", Price = 20.00, BrandId = 1, ColorId = 1, SexId = 1, SizeId = 1, TypeId = 1, Description = "SaveTest" };

            //Act
            _sut.ProductForm(productViewModel);
            var savedType = _context.Products.FirstOrDefault(x => x.Name.Equals("SaveTest"));


            //Assert
            Assert.NotNull(savedType);
            Assert.Equal(productViewModel.Name, savedType.Name);
        }

        [Fact]
        public void RedirectToProductFormWhenValidModel()
        {
            //Arrange
            var productViewModel = new ProductFormViewModel() { Name = "SaveTest", Price = 20.00, BrandId = 1, ColorId = 1, SexId = 1, SizeId = 1, TypeId = 1, Description = "SaveTest"};

            //Act
            var result = _sut.ProductForm(productViewModel);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            //Assert
            Assert.Equal("ProductForm", redirectToActionResult.ActionName);
        }

        //TypeForm Tests
        [Fact]
        public void ReturnViewForTypeForm()
        {
            //Act
            IActionResult result = _sut.TypeForm();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewWithDataWhenInvalidModelStateInTypeForm()
        {
            //Arrange
            _sut.ModelState.AddModelError("x", "Test Error");

            var type = new Type
            {
                Name = "Test"
            };

            //Act
            IActionResult result = _sut.TypeForm(type);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Type>(viewResult.Model);
            Assert.Equal(type.Name, model.Name);

        }

        [Fact]
        public void NotSaveTypeWhenModelError()
        {
            //Arrange
            _sut.ModelState.AddModelError("x", "Test Error");

            var type = new Type
            {
                Name = "NoSaveTest"
            };

            //Act
            _sut.TypeForm(type);
            var result = _context.Types.FirstOrDefault(x => x.Name.Equals("NoSaveTest"));

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public void SaveTypeWhenValidModel()
        {
            //Arrange
            var type = new Type
            {
                Name = "TestSave"
            };
            
            //Act
            _sut.TypeForm(type);
            var savedType = _context.Types.FirstOrDefault(x => x.Name.Equals("TestSave"));


            //Assert
            Assert.NotNull(savedType);
            Assert.Equal(type.Name, savedType.Name);
        }

        [Fact]
        public void RedirectToTypeFormWhenValidModel()
        {
            //Arrange
            var type = new Type
            {
                Name = "Test name"
            };

            //Act
            var result = _sut.TypeForm(type);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            //Assert
            Assert.Equal("TypeForm", redirectToActionResult.ActionName);
        }

        //ColorFormTests

        [Fact]
        public void ReturnViewForColorForm()
        {
            //Act
            IActionResult result = _sut.ColorForm();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewWithDataWhenInvalidModelStateInColorForm()
        {
            //Arrange
            _sut.ModelState.AddModelError("x", "Test Error");

            var colorViewModel = new ColorFormViewModel
            {
                Name = "Test"
            };

            //Act
            IActionResult result = _sut.ColorForm(colorViewModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ColorFormViewModel>(viewResult.Model);
            Assert.Equal(colorViewModel.Name, model.Name);
        }

        [Fact]
        public void NotSaveColorWhenModelError()
        {
            //Arrange
            _sut.ModelState.AddModelError("x", "Test Error");

            var colorViewModel = new ColorFormViewModel()
            {
                Name = "NoSaveTest"
            };

            //Act
            _sut.ColorForm(colorViewModel);
            var result = _context.Colors.FirstOrDefault(x => x.Name.Equals("NoSaveTest"));

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public void SaveColorWhenValidModel()
        {
            //Arrange
            var colorViewModel = new ColorFormViewModel
            {
                Name = "TestSave"
            };

            //Act
            _sut.ColorForm(colorViewModel);
            var savedType = _context.Colors.FirstOrDefault(x => x.Name.Equals("TestSave"));


            //Assert
            Assert.NotNull(savedType);
            Assert.Equal(colorViewModel.Name, savedType.Name);
        }

        [Fact]
        public void RedirectToColorFormWhenValidModel()
        {
            //Arrange
            var colorViewModel = new ColorFormViewModel
            {
                Name = "Test name"
            };

            //Act
            var result = _sut.ColorForm(colorViewModel);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            //Assert
            Assert.Equal("ColorForm", redirectToActionResult.ActionName);
        }

        //BrandFormTests
        [Fact]
        public void ReturnViewForBrandForm()
        {
            //Act
            IActionResult result = _sut.BrandForm();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewWithDataWhenInvalidModelStateInBrandForm()
        {
            //Arrange
            _sut.ModelState.AddModelError("x", "Test Error");

            var brandViewModel = new BrandFormViewModel
            {
                Name = "Test",
                Description = "Test description"
            };

            //Act
            IActionResult result = _sut.BrandForm(brandViewModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<BrandFormViewModel>(viewResult.Model);
            Assert.Equal(brandViewModel.Name, model.Name);
        }

        [Fact]
        public void NotSaveBrandWhenModelError()
        {
            //Arrange
            _sut.ModelState.AddModelError("x", "Test Error");

            var brandViewModel = new BrandFormViewModel
            {
                Name = "NoSaveTest",
                Description = "No save test description"
            };

            //Act
            _sut.BrandForm(brandViewModel);
            var result = _context.Brands.FirstOrDefault(x => x.Name.Equals("NoSaveTest"));

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public void SaveBrandWhenValidModel()
        {
            //Arrange
            var brandViewModel = new BrandFormViewModel
            {
                Name = "TestSave",
                Description = "Test description save"
            };

            //Act
            _sut.BrandForm(brandViewModel);
            var savedType = _context.Brands.FirstOrDefault(x => x.Name.Equals("TestSave"));


            //Assert
            Assert.NotNull(savedType);
            Assert.Equal(brandViewModel.Name, savedType.Name);
        }

        [Fact]
        public void RedirectToBrandFormWhenValidModel()
        {
            //Arrange
            var brandViewModel = new BrandFormViewModel
            {
                Name = "Test name",
                Description = "Test description"
            };

            //Act
            var result = _sut.BrandForm(brandViewModel);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            //Assert
            Assert.Equal("BrandForm", redirectToActionResult.ActionName);
        }


        //SizeFormTests
        [Fact]
        public void ReturnViewForSizeForm()
        {
            //Act
            IActionResult result = _sut.SizeForm();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewWithDataWhenInvalidModelStateInSizeForm()
        {
            //Arrange
            _sut.ModelState.AddModelError("x", "Test Error");

            var sizeViewModel = new SizeFormViewModel
            {
                Name = "Test",
                TypeId = 1
            };

            //Act
            IActionResult result = _sut.SizeForm(sizeViewModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<SizeFormViewModel>(viewResult.Model);
            Assert.Equal(sizeViewModel.Name, model.Name);
        }

        [Fact]
        public void NotSaveSizeWhenModelError()
        {
            //Arrange
            _sut.ModelState.AddModelError("x", "Test Error");

            var sizeViewModel = new SizeFormViewModel
            {
                Name = "NoSaveTest",
                TypeId = 1
            };

            //Act
            _sut.SizeForm(sizeViewModel);
            var result = _context.Sizes.FirstOrDefault(x => x.Name.Equals("NoSaveTest"));

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public void SaveSizeWhenValidModel()
        {
            //Arrange
            var sizeViewModel = new SizeFormViewModel
            {
                Name = "TestSave",
                TypeId = 1
            };

            //Act
            _sut.SizeForm(sizeViewModel);
            var savedType = _context.Sizes.FirstOrDefault(x => x.Name.Equals("TestSave"));


            //Assert
            Assert.NotNull(savedType);
            Assert.Equal(sizeViewModel.Name, savedType.Name);
        }

        [Fact]
        public void RedirectToSizeFormWhenValidModel()
        {
            //Arrange
            var sizeViewModel = new SizeFormViewModel
            {
                Name = "Test name",
                TypeId = 1
            };

            //Act
            var result = _sut.SizeForm(sizeViewModel);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            //Assert
            Assert.Equal("SizeForm", redirectToActionResult.ActionName);
        }


    }
}
