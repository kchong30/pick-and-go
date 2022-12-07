using PickAndGo.Models;
using PickAndGo.ViewModels;

namespace PickAndGo.Repositories
{
    public class OrderHeaderRepository
    {
        private readonly PickAndGoContext _db;

        public OrderHeaderRepository(PickAndGoContext context)
        {
            _db = context;
        }


        public Tuple<int, int> GetAll()
        {
            int pending = _db.OrderHeaders.Where(oh => oh.OrderStatus == "P").Select(oh => oh.OrderStatus).Count();
            int completed = _db.OrderHeaders.Where(oh => oh.OrderStatus == "C").Select(oh => oh.OrderStatus).Count();


            //OrderHeaderVM ohVM = new OrderHeaderVM()
            //{
            //    Pending = pending,
            //    Completed = completed
            //};
            return Tuple.Create(pending, completed);
        }
    }
}
