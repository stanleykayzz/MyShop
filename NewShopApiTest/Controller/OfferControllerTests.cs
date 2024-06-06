using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyShop.Api.Controller;
using MyShop.Core.Entities;
using MyShop.Queries;

namespace MyShopApiTest.Controller
{
    [TestFixture]
    public class OfferControllerTests
    {
        public static IEnumerable<Product> mockProducts = new List<Product>
        {
            new Product
            {
                Id = 1,
                Brand = "Zara",
                Size = "M",
                Name = "T-shirt",
                Quantity = 10,
                Price = 12
            },
            new Product
            {
                Id = 2,
                Brand = "Celio",
                Size = "XL",
                Name = "Jean Slim",
                Quantity = 1,
                Price = 15
            },
            new Product
            {
                Id = 3,
                Brand = "Uniquro",
                Size = "XL",
                Name = "Short",
                Quantity = 1,
                Price = 20
            },
            new Product
            {
                Id = 4,
                Brand = "H&M",
                Size = "l",
                Name = "Sweat shirt",
                Quantity = 1,
                Price = 12
            }
        }; 
        private Mock<IMediator> _mockMediator;
        private OfferController _controller;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _mockMediator = new Mock<IMediator>();
        }

        [Test]
        public async Task GetAllProducts_ReturnListProducts()
        {
            //Arrange
            _mockMediator.Setup(s =>
                s.Send(It.IsAny<GetAllProductsQuery>(), default))
                    .ReturnsAsync(mockProducts);

            _controller = new OfferController(_mockMediator.Object);

            //Act
            var result = await _controller.GetAllProducts();

            //Asserts
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Product>>>(result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedProducts = okResult.Value as IEnumerable<Product>;
            Assert.IsNotNull(returnedProducts);
            Assert.That(returnedProducts.Count, Is.EqualTo(mockProducts.Count()));
            Assert.That(returnedProducts, Is.EqualTo(mockProducts));
        }

        [Test]
        public async Task GetAllProducts_ReturnInternalErrorOnException()
        {
            //Arrange
            _mockMediator.Setup(s =>
                s.Send(It.IsAny<GetAllProductsQuery>(), default))
                .ThrowsAsync(new Exception("We got an exception."));

            _controller = new OfferController(_mockMediator.Object);
            //Act
            var result = await _controller.GetAllProducts();

            //Asserts
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Product>>>(result);

            var errorResult = result.Result as ObjectResult;
            Assert.That(errorResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            
            var errorResponse = errorResult.Value as dynamic;
            Assert.IsNotNull(errorResponse);
            Assert.That(Is.EqualTo(errorResponse), "We got an exception.");
        }
    }
}