using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Models.DTos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
	public class ProductsBase : ComponentBase
	{
		[Inject]
		public IProductService ProductService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
        public IEnumerable<CartItemDto> ShoppingCartsItems { get; set; }


		protected override async Task OnInitializedAsync()
		{
			Products = await ProductService.GetItems();
            ShoppingCartsItems = await ShoppingCartService.GetItems(HardCoded.UserId);

			var totalQty = ShoppingCartsItems.Sum(x => x.Qty);
			ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
        }

		protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGrouppedProductsByCathegory()
		{
			return from product in Products
				   group product by product.CategoryId into prodByCatGroup
				   orderby prodByCatGroup.Key
				   select prodByCatGroup;
		}
		protected string GetCathegoryName(IGrouping<int, ProductDto> grouppedProductDtos)
		{
			return grouppedProductDtos.FirstOrDefault(pg => pg.CategoryId == grouppedProductDtos.Key).CategoryName;
		}

	}
}
