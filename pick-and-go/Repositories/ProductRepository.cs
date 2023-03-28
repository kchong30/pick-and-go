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

        public Product ReturnProductById(int productId)
        {
            var product = _db.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            return product;
        }

        public string EditProduct(Product product)
        {
            string editMessage = "";

            try
            {
                _db.Update(new Product
                {
                    ProductId = product.ProductId,
                    Description = product.Description,
                    BasePrice = product.BasePrice,
                    Image = product.Image,
                }); ;
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                editMessage = "An error occurred while updating the product in the database." +
                              " Please try again later." + " " + e.Message;
            }

            if (editMessage == "")
            {
                editMessage = $"Success editing product {product.Description}";
            }
            return editMessage;
        }
    }
}
