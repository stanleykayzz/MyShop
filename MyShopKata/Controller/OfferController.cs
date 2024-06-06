using MediatR;
using Microsoft.AspNetCore.Mvc;
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
                return BadRequest(e);
            }
        }

        /// <summary>
        /// GET api/offer/5
        /// return existing product matching id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            try
            {
                var query = new GetProductByIdQuery(id);
                var result = await _mediator.Send(query);

                return Ok(result);

            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        // POST api/offer
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/offer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/offer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
