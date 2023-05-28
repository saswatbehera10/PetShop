namespace PetShop.DataAccessLayer.Entities.Repository.Interfaces
{
    public interface IUserRepo
    {
        Task<User> CreateAsync(User user);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> UpdateAsync(int id, User user);
        Task<User> DeleteAsync(int id);
        Task<User> GetByEmailAsync(string email);
    }
}
