using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using WebStore.Controllers;
using WebStore.Models;
using Xunit;

namespace WebStore.Tests.Controller
{
    public class ProductControllerShould
    {
        private readonly Mock<StoreContext> _mockStoreContext;
        private readonly Mock<IWebHostEnvironment> _mockIWebHostEnvironment;
        private readonly ProductController _sut;

        public ProductControllerShould()
        {
            _mockStoreContext = new Mock<StoreContext>();
            _mockIWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _sut = new ProductController(_mockStoreContext.Object, _mockIWebHostEnvironment.Object); ; 
        }

        [Fact]
        public void ReturnViewForTypeForm()
        {
            IActionResult result = _sut.TypeForm();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewWithDataWhenInvalidModelStateInTypeForm()
        {
            _sut.ModelState.AddModelError("x", "Test Error");

            var type = new Type
            {
                Name = "Koszulka"
            };

            IActionResult result = _sut.TypeForm(type);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsType<Type>(viewResult.Model);

            Assert.Equal(type.Name, model.Name);
        }

        [Fact]
        public void NotSaveTypeWhenModelError()
        {
            _sut.ModelState.AddModelError("x", "Test Error");

            var type = new Type();

            _sut.TypeForm(type);

            _mockStoreContext.Verify(
                x=>x.Types.Add(It.IsAny<Type>()), Times.Never);
        }

        [Fact]
        public void SaveTypeWhenValidModel()
        {
            Type savedType = null;

            _mockStoreContext.Setup(x => x.Types.Add(It.IsAny<Type>()))
                .Returns((EntityEntry<Type>)null)
                .Callback<Type>(x => savedType = x);

            var type = new Type
            {
                Name = "Test Type"
            };

            _sut.TypeForm(type);

            _mockStoreContext.Verify(
                x => x.Types.Add(It.IsAny<Type>()), Times.Once);

            Assert.Equal(type.Name, savedType.Name);
        }
        
        [Fact]
        public void ReturnTypeFormViewWhenValidModel()
        {
            _mockStoreContext.Setup(x => x.Types.Add(It.IsAny<Type>()));

            var type = new Type
            {
                Name = "Test name"
            };

            var result = _sut.TypeForm(type);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("TypeForm", redirectToActionResult.ActionName);
        }

    }
}
