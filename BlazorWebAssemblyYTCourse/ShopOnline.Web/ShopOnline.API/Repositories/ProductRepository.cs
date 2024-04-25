using Microsoft.EntityFrameworkCore;
using ShopOnline.API.Data;
using ShopOnline.API.Entities;
using ShopOnline.API.Repositories.Contracts;

namespace ShopOnline.API.Repositories
{
    public class ProductRepository : IProductRepository
	{
		private readonly ShopOnlineDbContext shopOnlineDbContext;


		public ProductRepository(ShopOnlineDbContext shopOnlineDbContext)
		{
			this.shopOnlineDbContext = shopOnlineDbContext;
		}


		public async Task<IEnumerable<ProductCategory>> GetCategories()
		{
			return await shopOnlineDbContext.ProductCategories.ToListAsync();
		}

		public async Task<ProductCategory> GetCategory(int id)
		{
			return await shopOnlineDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Product> GetItem(int id)
		{
			return await shopOnlineDbContext.Products.FindAsync(id);
		}

		public async Task<IEnumerable<Product>> GetItems()
		{
			return await shopOnlineDbContext.Products.ToListAsync();
		}

        public async Task<IEnumerable<Product>> GetItemsByCategory(int categoryId)
        {
            return await shopOnlineDbContext.Products.Where(x => x.CategoryId == categoryId).ToListAsync(); 
        }
    }
}
