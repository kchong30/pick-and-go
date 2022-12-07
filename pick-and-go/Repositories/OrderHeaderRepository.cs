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


        public Tuple<int, int> GetOverview(string date)
        {
            // "P" & "C" Will be changed later on.
            int pending = _db.OrderHeaders.Where(oh=>oh.OrderDate.ToString() == date).Where(oh => oh.OrderStatus == "P").Select(oh => oh.OrderStatus).Count();
            int completed = _db.OrderHeaders.Where(oh => oh.OrderDate.ToString() == date).Where(oh => oh.OrderStatus == "C").Select(oh => oh.OrderStatus).Count();

            return Tuple.Create(pending, completed);
        }
    }
}
