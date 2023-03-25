using PickAndGo.Models;
using PickAndGo.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using NuGet.Packaging.Signing;

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
                            .Join(_db.LineIngredients, i => i.IngredientId, li => li.IngredientId,
                                 (i, li) => new { i.IngredientId, i.Description, i.CategoryId, li.Quantity })
                            .Where(joined => joined.Quantity != null && joined.CategoryId != "Breads")
                            .GroupBy(x => new { x.IngredientId, x.Description, x.CategoryId })
                            .Select(g => new IngredientOVVM
                            {
                                IngredientId = g.Key.IngredientId,
                                Description = g.Key.Description,
                                CategoryId = g.Key.CategoryId,
                                SalesQty = g.Sum(x => x.Quantity)
                            })
                            .OrderByDescending(x => x.SalesQty)
                            .Take(5)
                            .AsQueryable()
            };
        
            return vm;
        }
    }
}
