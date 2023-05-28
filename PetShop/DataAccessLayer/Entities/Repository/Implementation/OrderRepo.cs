using Microsoft.EntityFrameworkCore;
using PetShop.DataAccessLayer.Context;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;

namespace PetShop.DataAccessLayer.Entities.Repository.Implementation
{
    public class OrderRepo : IOrderRepo
    {
        private readonly PetShopDbContext dbContext;

        public OrderRepo(PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Order> CreateAsync(Order order)
        {
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await dbContext.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await dbContext.Orders.FirstOrDefaultAsync(x => x.OrderID == id);
        }
        public async Task<Order> UpdateAsync(int id, Order order)
        {
            var neworder = await dbContext.Orders.FirstOrDefaultAsync(x => x.OrderID == id);
            if (neworder == null)
            {
                return null;
            }

            neworder.OrderDate = order.OrderDate;


            await dbContext.SaveChangesAsync();
            return neworder;
        }
        public async Task<Order> DeleteAsync(int id)
        {
            var order = await dbContext.Orders.FirstOrDefaultAsync(x => x.OrderID == id);
            if (order == null)
            {
                return null;
            }
            dbContext.Orders.Remove(order);

            await dbContext.SaveChangesAsync();
            return order;
        }

    }
}
