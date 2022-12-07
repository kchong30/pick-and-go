using pick_and_go.ViewModel;
using PickAndGo.Models;
using PickAndGo.ViewModel;

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
    }
}
