﻿using PickAndGo.Models;
using PickAndGo.ViewModels;

namespace PickAndGo.Repositories
{
    public class FavoritesRepository
    {
        private readonly PickAndGoContext _db;

        public FavoritesRepository(PickAndGoContext context)
        {
            _db = context;
        }

        public IQueryable<FavoritesVM> BuildFavoritesVM(int customerId)
        {
            var vmList = from f in _db.Favorites
                         join l in _db.OrderLines on f.LineId equals l.LineId
                         join o in _db.OrderHeaders on f.OrderId equals o.OrderId
                         join c in _db.Customers on f.CustomerId equals c.CustomerId
                         join p in _db.Products on l.ProductId equals p.ProductId
                         where (f.CustomerId.Equals(customerId))
                         orderby o.OrderDate descending
                         let iSum = (from li in _db.LineIngredients
                                     join i in _db.Ingredients on li.IngredientId equals i.IngredientId
                                     where f.OrderId == li.OrderId && l.LineId == li.LineId
                                     select (li.Price * li.Quantity)).Sum()

                         select new FavoritesVM
                         {
                             OrderId = o.OrderId,
                             OrderStr = "Order" + o.OrderId + l.LineId,
                             CustomerId = o.CustomerId,
                             FirstName = c.FirstName,
                             LastName = c.LastName,
                             FullName = c.FirstName + " " + c.LastName,
                             OrderDate = ((DateTime)o.OrderDate).ToString("MM-dd-yyyy"),
                             LineId = l.LineId,
                             ProductId = l.ProductId,
                             Description = p.Description,
                             FavoriteName = f.FavoriteName,
                             Quantity = l.Quantity,
                             Price = l.Price,
                             CurrentPrice = p.BasePrice,
                             LineValue = (decimal)(l.Price + iSum),
                             Ingredients = (List<OrderIngredientVM>)
                                              (from li in _db.LineIngredients
                                               join i in _db.Ingredients on li.IngredientId equals i.IngredientId
                                               where o.OrderId == li.OrderId && l.LineId == li.LineId
                                               orderby o.OrderId, li.LineId
                                               select new OrderIngredientVM
                                               {
                                                   IngredientId = li.IngredientId,
                                                   IngDescription = i.Description,
                                                   Quantity = li.Quantity,
                                                   Price = li.Price,
                                                   CurrentPrice = i.Price,
                                                   IngValue = (decimal)(li.Quantity * li.Price)
                                               }),
                         };

            return vmList;
        }

        public Favorite GetFavoritesRecord(int customerId, int orderId, int lineId)
        {
            var favorite = _db.Favorites.Where(f => f.CustomerId == customerId &
                                               f.OrderId == orderId &
                                               f.LineId == lineId).FirstOrDefault();
            return favorite;
        }

        public string DeleteFavoritesRecord(int customerId, int orderId, int lineId)
        {
            string deleteMessage = "";
            Favorite favorite = GetFavoritesRecord(customerId, orderId, lineId);

            try
            {
                _db.Favorites.Remove(favorite);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                deleteMessage = "The favourite may not exist or " +
                                "there could be a foreign key restriction." + " " + e.Message;
            }
            return deleteMessage;
        }

        public string CreateFavoritesRecord(int customerId, int orderId, int lineId, string name)
        {
            string message = "";
            try
            {
                _db.Favorites.Add(new Favorite
                {
                    CustomerId = customerId,
                    OrderId = orderId,
                    LineId = lineId,
                    FavoriteName = name,
                });
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                message = "An error occurred while adding the favourite to the database." +
                          " Please try again later." + " " + e.Message;
            }
            return message;
        }

        public string ChangeFavoritesRecord(int customerId, int orderId, int lineId, string name)
        {
            string message = "";
            Favorite favorite = GetFavoritesRecord(customerId, orderId, lineId);

            favorite.FavoriteName = name;

            try
            {
                _db.Favorites.Update(favorite);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                message = "An error occurred while updating the favourite in the database." +
                          " Please try again later." + " " + e.Message;
            }

            return message;

        }
    }
}
