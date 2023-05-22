namespace PetShop.DataAccessLayer.Entities.Repository.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string email, string password);
        Task<User> GetById(int UserID);
        Task<IEnumerable<User>> GetAll();
        Task<User> Register(User user);
        Task Update(User user);
        Task Delete(int UserID);
        Task<bool> UserExists(object email);
    }
}
