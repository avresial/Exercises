using Microsoft.AspNetCore.Mvc;
using ShopOnline.API.Repositories.Contracts;
using ShopOnline.Models.DTos;

namespace ShopOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var products = await productRepository.GetItems();
                var productsCathegories = await productRepository.GetCategories();

                if (products is null || productsCathegories is null)
                    return NotFound();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
    }
}
