using PickAndGo.Models;
using PickAndGo.ViewModels;
using Microsoft.AspNetCore.Http;
using System;

namespace PickAndGo.Repositories
{
    public class OrderHeaderRepository
    {
        private readonly PickAndGoContext _db;

        public OrderHeaderRepository(PickAndGoContext context)
        {
            _db = context;
        }

        public OverviewVM GetOverviewCounts(string date)
        {
            var vm = new OverviewVM
            {
                ViewDate = date,
                Outstanding = _db.OrderHeaders
                                .Where(oh => oh.OrderDate.ToString() == date)
                                .Where(oh => oh.OrderStatus == "O")
                                .Count(),
                Completed = _db.OrderHeaders
                                .Where(oh => oh.OrderDate.ToString() == date)
                                .Where(oh => oh.OrderStatus == "C")
                                .Count(),
                OutstandingVal = (decimal)_db.OrderHeaders
                                    .Where(oh => oh.OrderDate.ToString() == date)
                                    .Where(oh => oh.OrderStatus == "O")
                                    .Select(oh => oh.OrderValue ?? 0).Sum(),
                CompletedVal = (decimal)_db.OrderHeaders
                                    .Where(oh => oh.OrderDate.ToString() == date)
                                    .Where(oh => oh.OrderStatus == "C")
                                    .Select(oh => oh.OrderValue ?? 0).Sum(),
                Accounts = _db.Customers
                                .Count(c => c.DateSignedUp != null && c.AdminUser != "Y"),
                Guests = _db.Customers
                                .Count(c => c.DateSignedUp == null && c.AdminUser != "Y"),
                Ingredients = _db.Ingredients
                                  .Where(i => i.InStock != "Y")
                                  .OrderBy(i => i.CategoryId)
                                  .ThenBy(i => i.Description),
                TopFive = _db.Ingredients
                                .Where(li => _db.LineIngredients
                                .Where(li => li.Quantity != null && li.Price != null)
                                .GroupBy(li => li.IngredientId)
                                .Select(g => g.Key)
                                .Contains(li.IngredientId))
                                .Select(i => new
                                {
                                    Ingredient = i,
                                    Description = i.Description,
                                    CategoryId = i.CategoryId,
                                    SalesValue = _db.LineIngredients
                                        .Where(li => li.IngredientId == i.IngredientId)
                                        .Sum(li => li.Quantity * li.Price)
                                })
                                .OrderByDescending(g => g.SalesValue)
                                .Take(5)
                                .Select(g => g.Ingredient)
                                .AsQueryable()
            };
            return vm;
        }
    }
}
