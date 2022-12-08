using Microsoft.EntityFrameworkCore;
using PickAndGo.ViewModels;
using PickAndGo.Models;

namespace PickAndGo.Repositories
{
    public class IngredientsRepository
    {
        private readonly PickAndGoContext _db;

        public IngredientsRepository(PickAndGoContext context)
        {
            _db = context;
        }

        public IEnumerable<IngredientVM> ReturnAllIngredients()
        {

            var vm = from i in _db.Ingredients
                     select new IngredientVM
                     {
                         IngredientId = i.IngredientId,
                         Description = i.Description,
                         Price = i.Price,
                         CategoryId = i.CategoryId,
                         InStock = i.InStock
                     };

            return vm;
        }

        public IngredientVM ReturnIngredientById(int ingredientId)
        {

            var vm = ReturnAllIngredients().Where(c => c.IngredientId == ingredientId).FirstOrDefault();

            return vm;
        }

        public string EditIngredient(Ingredient ingredient)
        {
            string message = "";
            _db.Update(new Ingredient
            {
                IngredientId = ingredient.IngredientId,
                Description= ingredient.Description,
                Price = ingredient.Price,
                CategoryId = ingredient.CategoryId,
                InStock = ingredient.InStock
            });
            _db.SaveChanges();
            return message;
        }
    }
}
