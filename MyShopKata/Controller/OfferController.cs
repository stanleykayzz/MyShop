using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyShop.Command;
using MyShop.Core.Entities;
using MyShop.Queries;

namespace MyShop.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// GET: api/offer/all
        /// return all products
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {
                var query = new GetAllProductsQuery();
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// GET api/offer/5
        /// return existing product matching id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var query = new GetProductByIdQuery(id);
                var result = await _mediator.Send(query);

                return Ok(result);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// POST api/offer
        /// Create a new product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateProduct([FromBody] AddProductCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { Created = "New product created" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// PUT api/offer/
        /// Update Product from body
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] UpdateProductCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
            }
        }
    }
}
