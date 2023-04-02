using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PickAndGo.Models;
using PickAndGo.ViewModels;
using System.Data;
using System.Drawing;
using System.Linq;

namespace PickAndGo.Repositories
{
    public class OrderRepository
    {
        private readonly PickAndGoContext _db;
        private readonly IConfiguration _configuration;

        public OrderRepository(PickAndGoContext context, IConfiguration configuration)
        {
            _db = context;
            _configuration = configuration;
        }

        public IQueryable<OrderListVM> BuildOrderListVM(string orderFilter, string searchName, string searchOrder)
        {
            var vmList = from l in _db.OrderLines
                         join o in _db.OrderHeaders on l.OrderId equals o.OrderId
                         join c in _db.Customers on o.CustomerId equals c.CustomerId
                         join p in _db.Products on l.ProductId equals p.ProductId
                         where (o.OrderStatus.Contains(orderFilter))
                         orderby o.PickupTime descending
                         select new OrderListVM
                         {
                             OrderId = o.OrderId,
                             OrderStr = "Order" + o.OrderId + l.LineId,
                             CustomerId = o.CustomerId,
                             FirstName = c.FirstName,
                             LastName = c.LastName,
                             FullName = c.FirstName + " " + c.LastName,
                             OrderDate = ((DateTime)o.OrderDate).ToString("MM-dd-yyyy"),
                             PickupTime = ((DateTime)o.PickupTime).ToString("MM-dd-yyyy hh:mm tt"),
                             LineId = l.LineId,
                             ProductId = l.ProductId,
                             Description = p.Description,
                             Quantity = l.Quantity,
                             Price = l.Price,
                             OrderValue = (decimal)o.OrderValue,
                             OrderStatus = o.OrderStatus,
                             LineStatus = l.LineStatus,
                             LineColor = (l.LineStatus == "C" & o.OrderStatus != "C") ? "#FF0000" : "#000000",
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
                                                   Price = li.Price
                                               }),
                         };
            if (searchName != null && searchName != "")
            {
                vmList = from v in vmList
                         where v.FirstName.Contains(searchName) || v.LastName.Contains(searchName)
                         select v;
            }

            if (searchOrder != null && searchOrder != "")
            {
                vmList = from v in vmList
                         where v.OrderId.ToString() == searchOrder
                         select v;
            }

            return vmList;
        }

        public IQueryable<OrderHistoryVM> BuildOrderHistoryVM(int customerId)
        {
            var vmList = from l in _db.OrderLines
                         join o in _db.OrderHeaders on l.OrderId equals o.OrderId
                         join c in _db.Customers on o.CustomerId equals c.CustomerId
                         join p in _db.Products on l.ProductId equals p.ProductId
                         where (c.CustomerId.Equals(customerId) /*&& o.OrderDate >= c.DateSignedUp*/)
                         orderby o.OrderDate descending
                         let iSum = (from li in _db.LineIngredients
                                     where o.OrderId == li.OrderId && l.LineId == li.LineId
                                     select (li.Price * li.Quantity)).Sum()
                         let fCount = (from f in _db.Favorites
                                       where c.CustomerId == f.CustomerId &&
                                                                     o.OrderId == f.OrderId &&
                                                                     l.LineId == f.LineId
                                       select o).Count()
                         select new OrderHistoryVM
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
                             Quantity = l.Quantity,
                             Price = l.Price,
                             OrderValue = (decimal)o.OrderValue,
                             LineValue = (decimal)(l.Price + iSum),
                             IsFavorite = fCount > 0 ? true : false,
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
                                                   IngValue = (decimal)(li.Quantity * li.Price)
                                               }),
                         };

            return vmList;
        }

        public IQueryable<OrderTransactionVM> BuildOrderTransactionVM(string searchName, string searchOrder,
                                                                      DateTime fromDate, DateTime toDate)
        {
            var oCount = _db.OrderHeaders
                                .Where(oh => oh.OrderDate >= fromDate && oh.OrderDate <= toDate)
                                .Count();
            var oVal = (decimal)_db.OrderHeaders
                                    .Where(oh => oh.OrderDate >= fromDate && oh.OrderDate <= toDate)
                                    .Sum(oh => oh.OrderValue ?? 0);
            var vmList = from o in _db.OrderHeaders
                         join c in _db.Customers on o.CustomerId equals c.CustomerId
                         where o.OrderDate >= fromDate && o.OrderDate <= toDate
                         orderby o.OrderDate descending, o.OrderId descending
                         select new OrderTransactionVM
                         {
                             OrderId = o.OrderId,
                             CustomerId = o.CustomerId,
                             FirstName = c.FirstName,
                             LastName = c.LastName,
                             FullName = c.FirstName + " " + c.LastName,
                             Email = c.EmailAddress,
                             OrderDate = ((DateTime)o.OrderDate).ToString("MM-dd-yyyy"),
                             OrderValue = (decimal)o.OrderValue,
                             Currency = o.Currency,
                             PaymentType = o.PaymentType,
                             PaymentId = o.PaymentId,
                             PaymentDate = ((DateTime)o.PaymentDate).ToString("MM-dd-yyyy"),
                             OrderCount = oCount,
                             OrderTotalVal = oVal
                         };

            if (searchName != null && searchName != "")
            {
                vmList = from v in vmList
                         where v.FirstName.Contains(searchName) || v.LastName.Contains(searchName)
                         select v;
            }

            if (searchOrder != null && searchOrder != "")
            {
                vmList = from v in vmList
                         where v.OrderId.ToString() == searchOrder
                         select v;
            }

            return vmList;
        }

        public OrderHeader GetOrderHeader(int orderId)
        {
            var orderHeader = _db.OrderHeaders.Where(o => o.OrderId == orderId).FirstOrDefault();

            return orderHeader;
        }

        public OrderLine GetOrderLine(int orderId, int lineId)
        {
            var orderLine = _db.OrderLines.Where(ol => ol.OrderId == orderId &&
                                                 ol.LineId == lineId).FirstOrDefault();

            return orderLine;
        }

        public string UpdateOrderLineStatus(int orderId, int lineId, string orderStatus)
        {
            string editMessage = "";
            OrderLine orderLine = GetOrderLine(orderId, lineId);
            orderLine.LineStatus = orderStatus;

            try
            {
                _db.OrderLines.Update(orderLine);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                editMessage = "An error occurred while updating the order line status in the database." +
                              " Please try again later." + " " + e.Message;
            }

            if (editMessage == "")
            {
                try
                {
                    UpdateOrderHeaderStatus(orderId);
                }
                catch (Exception e)
                {
                    editMessage = "An error occurred while updating the order header status in the database." +
                                  " Please try again later." + " " + e.Message;
                }
            }

            return editMessage;
        }

        public void UpdateOrderHeaderStatus(int orderId)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("spUpdateOrderHeaderStatus", connection);
            command.CommandType = CommandType.StoredProcedure;


            SqlParameter parameter = new SqlParameter("@OrderId", SqlDbType.Int);
            parameter.Value = orderId;
            command.Parameters.Add(parameter);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public string CreateOrder(int customerId, string firstName, string lastName, DateTime pickupTime, string paymentId, decimal orderTotal, string sandwichJson, string email)
        {
            string message = "";

            List<ShoppingCartVM> products = JsonConvert.DeserializeObject<List<ShoppingCartVM>>(sandwichJson);


            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    CustomerRepository cr = new CustomerRepository(_db);
                    if (customerId == 0)
                    {
                        var customer = cr.ReturnCustomerByEmail(email);
                        if (customer == null)
                        {
                            var tuple3 = cr.CreateRecord(email, firstName, lastName, "");
                            message = tuple3.Item1;
                            customerId = tuple3.Item2;
                        }
                        else
                        {
                            customerId = customer.CustomerId;
                        }
                    }

                    if (message == "")
                    {
                        var tuple = CreateOrderHeader(customerId, firstName, orderTotal, pickupTime, paymentId);

                        message = tuple.Item1;
                        var orderId = tuple.Item2;

                        if (message == "")
                        {
                            foreach (var product in products)
                            {
                                var tuple2 = CreateOrderLine(orderId, Convert.ToInt32(product.productId), Convert.ToDecimal(product.productPrice));

                                message = tuple2.Item1;
                                var lineId = tuple2.Item2;

                                if (message == "")
                                {
                                    foreach (var ingredient in product.ingredients)
                                    {
                                        message = CreateLineIngredient(orderId, lineId, ingredient.ingredientId,
                                                                       Convert.ToInt32(ingredient.quantity), Convert.ToDecimal(ingredient.price));
                                        if (message != "")
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (message == "")
                            {
                                message = cr.UpdateCustomerRecord(customerId);
                            }
                        }
                    }
                    // Commit all the database updates within the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Roll back the transaction if any update fails
                    transaction.Rollback();
                    throw ex;
                }
            }
            return message;
        }

        public Tuple<string, int> CreateOrderHeader(int customerId, string? firstName, decimal orderTotal, DateTime pickupTime,
                                                    string paymentId)
        {
            string message = "";

            OrderHeader orderHeader = new OrderHeader
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                OrderValue = orderTotal,
                PickupTime = pickupTime,
                OrderStatus = "O",
                Currency = "CAD",
                PaymentType = "Paypal",
                PaymentId = paymentId,
                PaymentDate = DateTime.Now
            };

            try
            {
                _db.OrderHeaders.Add(orderHeader);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                message = "An error occurred while creating the order header in the database." +
                          " Please try again later." + " " + e.Message;
            }

            return new Tuple<string, int>(message, orderHeader.OrderId);
        }

        public Tuple<string, int> CreateOrderLine(int orderId, int productId, decimal price)
        {
            string message = "";

            OrderLine orderLine = new OrderLine
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = 1,
                LineStatus = "O",
                Price = price
            };

            try
            {
                _db.OrderLines.Add(orderLine);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                message = "An error occurred while creating the order line in the database." +
                          " Please try again later." + " " + e.Message;
            }

            return new Tuple<string, int>(message, orderLine.LineId);
        }

        public string CreateLineIngredient(int orderId, int lineId, int ingredientId, int quantity, decimal price)
        {
            string message = "";

            LineIngredient lineIngredient = new LineIngredient
            {
                OrderId = orderId,
                LineId = lineId,
                IngredientId = ingredientId,
                Quantity = quantity,
                Price = price
            };

            try
            {
                _db.LineIngredients.Add(lineIngredient);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                message = "An error occurred while creating the order line ingredient in the database." +
                          " Please try again later." + " " + e.Message;
            }

            return message;
        }

        public OrderHeader GetConfirmationInfo(string confirmationId)
        {
            return _db.OrderHeaders.Where(t => t.PaymentId == confirmationId).FirstOrDefault();
        }
    }
}
