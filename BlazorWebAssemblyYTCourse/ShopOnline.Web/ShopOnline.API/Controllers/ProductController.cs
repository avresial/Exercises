﻿using Microsoft.AspNetCore.Mvc;
using ShopOnline.API.Extensions;
using ShopOnline.API.Repositories.Contracts;
using ShopOnline.Models.Dtos;

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
	}
}
