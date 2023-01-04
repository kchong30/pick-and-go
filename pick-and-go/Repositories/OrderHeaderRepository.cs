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
            int outstanding = _db.OrderHeaders.Where(oh => oh.OrderStatus == "O").Select(oh => oh.OrderStatus).Count();
            int completed = _db.OrderHeaders.Where(oh => oh.OrderStatus == "C").Select(oh => oh.OrderStatus).Count();


            //OrderHeaderVM ohVM = new OrderHeaderVM()
            //{
            //    Pending = pending,
            //    Completed = completed
            //};
            return Tuple.Create(outstanding, completed);
        }
    }
}
