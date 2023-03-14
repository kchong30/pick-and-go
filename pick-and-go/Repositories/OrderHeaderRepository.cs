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
                                .Select(oh => oh.OrderStatus).Count(),
                Completed = _db.OrderHeaders
                                .Where(oh => oh.OrderDate.ToString() == date)
                                .Where(oh => oh.OrderStatus == "C")
                                .Select(oh => oh.OrderStatus).Count(),
                OutstandingVal = (decimal)_db.OrderHeaders
                                    .Where(oh => oh.OrderDate.ToString() == date)
                                    .Where(oh => oh.OrderStatus == "O")
                                    .Select(oh => oh.OrderValue ?? 0).Sum(),
                CompletedVal = (decimal)_db.OrderHeaders
                                    .Where(oh => oh.OrderDate.ToString() == date)
                                    .Where(oh => oh.OrderStatus == "C")
                                    .Select(oh => oh.OrderValue ?? 0).Sum(),
                Accounts = _db.Customers
                              .Where(c => c.DateSignedUp != null)
                              .Select(c => c.CustomerId).Count(),
                Guests = _db.Customers
                              .Where(c => c.DateSignedUp == null)
                              .Select(oh => oh.CustomerId).Count(),
                //Ingredients = (from i in _db.Ingredients
                 
                // where i.InStock == "N"
                // orderby i.CategoryId, i.Description
                // select new IngredientVM
                // {
                //     Description = i.Description,
                //     CategoryId = i.CategoryId
                // }),
            };

            return vm;
        }
    }
}
