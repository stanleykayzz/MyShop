using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyShop.Api.Controller;
using MyShop.Command;
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
            _controller = new OfferController(_mockMediator.Object);
        }

        [Test]
        public async Task GetAllProducts_ReturnListProducts()
        {
            //Arrange
            _mockMediator.Setup(s =>
                s.Send(It.IsAny<GetAllProductsQuery>(), default))
                    .ReturnsAsync(mockProducts);

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

            //Act
            var result = await _controller.GetAllProducts();

            //Asserts
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Product>>>(result);

            var errorResult = result.Result as ObjectResult;
            Assert.That(errorResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            
            var errorResponse = errorResult.Value as dynamic;
            Assert.IsNotNull(errorResponse);
            //Assert.That(Is.EqualTo(errorResponse.ErrorMessage), "We got an exception.");
        }

        [Test]
        public async Task CreateProduct_InsertProductWithSuccess()
        {
            //Arrange
            var product = mockProducts.First();
            var command = new AddProductCommand {
                Brand = product.Brand,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Size = product.Size
            };

            _mockMediator.Setup(s => s.Send(It.IsAny<AddProductCommand>(), default))
                .ReturnsAsync(Unit.Value);

            //Act
            var result = await _controller.CreateProduct(command);

            //Asserts
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Test]
        public async Task CreateProduct_FailsInsertingOnException()
        {
            //Arrange
            var command = new AddProductCommand { };

            _mockMediator.Setup(s => s.Send(It.IsAny<AddProductCommand>(), default))
                .ThrowsAsync(new Exception("Insert exception."));

            //Act
            var result = await _controller.CreateProduct(command);

            //Asserts
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var error = result as ObjectResult;
            Assert.IsNotNull(error);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, error.StatusCode);
        }

        [Test]
        public async Task UpdateProduct_UpdateWithSuccess()
        {
            //Arrange
            var product = mockProducts.First();
            var command = new UpdateProductCommand
            {
                Id = product.Id,
                Brand = product.Brand,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Size = product.Size
            };

            _mockMediator.Setup(s => s.Send(It.IsAny<UpdateProductCommand>(), default))
                .ReturnsAsync(Unit.Value);

            //Act
            var result = await _controller.UpdateProduct(command);

            //Asserts
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var response = result as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }


        [Test]
        public async Task CreateProduct_FailsUpdateOnException()
        {
            //Arrange
            var command = new UpdateProductCommand { };

            _mockMediator.Setup(s => s.Send(It.IsAny<UpdateProductCommand>(), default))
                .ThrowsAsync(new Exception("Update exception."));

            //Act
            var result = await _controller.UpdateProduct(command);

            //Asserts
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var error = result as ObjectResult;
            Assert.IsNotNull(error);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, error.StatusCode);
        }
    }
}