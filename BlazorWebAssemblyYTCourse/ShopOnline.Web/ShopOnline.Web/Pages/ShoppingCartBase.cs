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
        public string TotalPrice { get; set; }
        public string TotalQuantity { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CalculateCartSummaryTotals();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private void SetTotalPrice()
        {
            TotalPrice = ShoppingCartItems.Sum(x => x.TotalPrice)
                            .ToString("C");
        }
        private void UpdateTotalPrice(CartItemDto cartItem)
        {
            var item = GetCartItemDto(cartItem.Id);
            if (item is not null)
            {
                item.TotalPrice = item.Price * item.Qty;
            }
        }

        private void CalculateCartSummaryTotals()
        {
            SetTotalQuantity();
            SetTotalPrice();
        }
        private void SetTotalQuantity()
        {
                TotalQuantity = ShoppingCartItems.Sum(x => x.Qty)
                                .ToString("C");
        }
        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);
            RemoveCartItem(id);
            CalculateCartSummaryTotals();
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
                    UpdateTotalPrice(returnedUpdateItemDto);
                }
                else
                {
                    var item = ShoppingCartItems.FirstOrDefault(x => x.Id == id);
                    if (item is not null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                        UpdateTotalPrice(item);
                    }
                }
                CalculateCartSummaryTotals();

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
