using Microsoft.EntityFrameworkCore;
using PickAndGo.Models;
using PickAndGo.ViewModels;

namespace PickAndGo.Repositories
{
    public class ProductRepository
    {
        private readonly PickAndGoContext _db;

        public ProductRepository(PickAndGoContext context)
        {
            _db = context;
        }

        public IQueryable<ProductVM> GetProducts()
        {
            var vmList = from p in _db.Products
                         select new ProductVM
                         {
                            ProductId = p.ProductId,
                            Description = p.Description,
                            BasePrice = p.BasePrice,
                            Image = p.Image,
                            SelectedProductId = 0
                         };

            return (vmList);
        }
    }
}
