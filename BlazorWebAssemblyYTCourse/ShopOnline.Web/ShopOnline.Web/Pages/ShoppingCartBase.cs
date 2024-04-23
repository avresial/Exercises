using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DTos;
using ShopOnline.Web.Services;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public string ErrorMessage { get; set; }
        public List<CartItemDto> ShoppingCartItems { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);
            RemoveCartItem(id);
        }

        protected async Task UpdateQtyCartItem_Click(int id, int qty) 
        {
            try
            {
                if (qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto()
                    {
                        CartItemId = id,
                        Qty = qty
                    };

                    var returnedUpdateItemDto = await ShoppingCartService.UpdateQty(updateItemDto);
                }
                else 
                {
                    var item = ShoppingCartItems.FirstOrDefault(x => x.Id == id);
                    if (item is not null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }
                
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private CartItemDto GetCartItemDto(int id)
        {
            return ShoppingCartItems.FirstOrDefault(x => x.Id == id);
        }

        private void RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItemDto(id);
            ShoppingCartItems.Remove(cartItemDto);

        }
    }
}
