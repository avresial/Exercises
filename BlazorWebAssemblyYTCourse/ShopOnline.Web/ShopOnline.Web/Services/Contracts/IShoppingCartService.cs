﻿using ShopOnline.Models.DTos;

namespace ShopOnline.Web.Services.Contracts
{
	public interface IShoppingCartService
	{
		Task<IEnumerable<CartItemDto>> GetItems(int userId);
		Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
	}
}
