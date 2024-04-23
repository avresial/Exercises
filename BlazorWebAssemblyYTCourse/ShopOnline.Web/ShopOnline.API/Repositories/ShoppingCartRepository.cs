using Microsoft.EntityFrameworkCore;
using ShopOnline.API.Data;
using ShopOnline.API.Entities;
using ShopOnline.API.Repositories.Contracts;
using ShopOnline.Models.DTos;

namespace ShopOnline.API.Repositories
{
	public class ShoppingCartRepository : IShoppingCartRepository
	{
		private readonly ShopOnlineDbContext shopOnlineDbContext;

		public ShoppingCartRepository(ShopOnlineDbContext shopOnlineDbContext)
		{
			this.shopOnlineDbContext = shopOnlineDbContext;
		}

		public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
		{
			if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId))
				return null;

			var item = await (from product in shopOnlineDbContext.Products
							  where product.Id == cartItemToAddDto.ProductId
							  select new CartItem
							  {
								  CartId = cartItemToAddDto.CartId,
								  ProductId = product.Id,
								  Qty = cartItemToAddDto.Qty
							  }).SingleOrDefaultAsync();

			if (item is not null)
			{
				var result = await shopOnlineDbContext.CartItems.AddAsync(item);
				await shopOnlineDbContext.SaveChangesAsync();
				return result.Entity;
			}

			return null;
		}
		public async Task<CartItem> DeleteItem(int id)
		{
			var item = await this.shopOnlineDbContext.CartItems.FindAsync(id);
			
			if (item is not null)
			{
				this.shopOnlineDbContext.CartItems.Remove(item);
				await this.shopOnlineDbContext.SaveChangesAsync();
            }

			return item;
		}
		public async Task<CartItem> GetItem(int id)
		{
			return await (from cart in shopOnlineDbContext.Carts
						  join cartItem in shopOnlineDbContext.CartItems 
						  on cart.Id equals cartItem.Id
						  where cartItem.Id == id
						  select new CartItem
						  {
							  Id = cartItem.Id,
							  ProductId = cartItem.ProductId,
							  Qty = cartItem.Qty
						  }).SingleOrDefaultAsync();
		}
		public async Task<IEnumerable<CartItem>> GetItems(int userId)
		{
			return await (from cart in shopOnlineDbContext.Carts
						  join cartItem in shopOnlineDbContext.CartItems
						  on cart.Id equals cartItem.CartId
						  where cart.UserId == userId
						  select new CartItem
						  {
							  Id = cartItem.Id,
							  Qty = cartItem.Qty,
							  CartId = cartItem.CartId,
							  ProductId = cartItem.ProductId
							  
						  }).ToListAsync();
		}
		public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
		{
			throw new NotImplementedException();
		}


		private async Task<bool> CartItemExists(int cartId, int productId)
		{
			return await shopOnlineDbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
		}
	}
}
