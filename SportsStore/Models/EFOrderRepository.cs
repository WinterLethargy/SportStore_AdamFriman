using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private StoreDbContext _dbContext;
        public EFOrderRepository(StoreDbContext storeDb)
        {
            _dbContext = storeDb;
        }
        public IQueryable<Order> Orders => _dbContext.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);
        public void SaveOrder(Order order)
        {
            _dbContext.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderId == 0)
            {
                _dbContext.Orders.Add(order);
            }
            _dbContext.SaveChanges();
        }
    }
}
