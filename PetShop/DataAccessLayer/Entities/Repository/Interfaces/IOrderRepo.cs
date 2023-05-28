namespace PetShop.DataAccessLayer.Entities.Repository.Interfaces
{
    public interface IOrderRepo
    {
        Task<Order> CreateAsync(Order order);
        Task<List<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task<Order> UpdateAsync(int id, Order order);
        Task<Order> DeleteAsync(int id);
    }
}
