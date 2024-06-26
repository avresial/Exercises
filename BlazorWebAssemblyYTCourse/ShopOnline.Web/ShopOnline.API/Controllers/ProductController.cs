﻿using Microsoft.AspNetCore.Mvc;
using ShopOnline.API.Entities;
using ShopOnline.API.Extensions;
using ShopOnline.API.Repositories.Contracts;
using ShopOnline.Models.Dtos;
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
				var productsCategories = await productRepository.GetCategories();

				if (products is null || productsCategories is null)
					return NotFound();

				var productDtos = products.ConvertToDto(productsCategories).ToList();

				return Ok(productDtos);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
			}
		}

        [HttpGet]
        [Route("{categoryId}/GetItemsByCategory")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItemsByCategory(int categoryId)
        {
            try
            {
                var products = await productRepository.GetItemsByCategory(categoryId);
                var productsCategories = await productRepository.GetCategories();

                if (products is null || productsCategories is null)
                    return NotFound();

                var productDtos = products.ConvertToDto(productsCategories).ToList();

                return Ok(productDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
		{
			try
			{
				var product = await productRepository.GetItem(id);

				if (product is null) return BadRequest();

				var category = await productRepository.GetCategory(product.CategoryId);

				ProductDto productDto = product.ConvertToDto(category);
				return Ok(productDto);

			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
			}
		}
        
		[HttpGet]
		[Route(nameof(GetProductCategories))]
        public async Task<ActionResult<ProductCategoryDto>> GetProductCategories()
        {
            try
            {
                var categories = await productRepository.GetCategories();

                if (categories is null) return BadRequest();

                var productDto = categories.ConvertToDto();
                return Ok(productDto);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }
}
