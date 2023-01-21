using Microsoft.EntityFrameworkCore;
using PickAndGo.Models;
using PickAndGo.ViewModels;
using System.Data;
using System.Linq;

namespace PickAndGo.Repositories
{
    public class OrderRepository
    {
        private readonly PickAndGoContext _db;

        public OrderRepository(PickAndGoContext context)
        {
            _db = context;
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
                             Price = p.BasePrice,
                             OrderValue = (decimal)o.OrderValue,
                             OrderStatus = o.OrderStatus,
                             LineStatus = l.LineStatus,
                             Ingredients = (List<OrderIngredientVM>)(from li in _db.LineIngredients
                                                                     join i in _db.Ingredients on li.IngredientId equals i.IngredientId
                                                                     where o.OrderId == li.OrderId && l.LineId == li.LineId
                                                                     orderby o.OrderId, li.LineId
                                                                     select new OrderIngredientVM
                                                                     {
                                                                         IngredientId = li.IngredientId,
                                                                         IngDescription = i.Description,
                                                                         Quantity = li.Quantity,
                                                                         Price = i.Price
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
                //editMessage = $"{orderLine.Description}, for customer {order.FullName} on order number {order.OrderId} has been completed";
            }
            catch (Exception ex)
            {
                editMessage = ex.Message;
            }

            if (editMessage == "")
            {
                try
                {
                    UpdateOrderHeaderStatus(orderId);
                }
                catch (Exception ex)
                {
                    editMessage = ex.Message;
                }
            }

            return editMessage;
        }

        public void UpdateOrderHeaderStatus(int orderId)
        {
            _db.OrderHeaders.FromSqlInterpolated($"spUpdateOrderHeaderStatus {orderId}");
        }
    }
}
