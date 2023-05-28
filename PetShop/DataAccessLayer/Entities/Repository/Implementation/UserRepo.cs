using Microsoft.EntityFrameworkCore;
using PetShop.DataAccessLayer.Context;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;

namespace PetShop.DataAccessLayer.Entities.Repository.Implementation
{
    public class UserRepo : IUserRepo
    {
        private readonly PetShopDbContext dbContext;

        public UserRepo(PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User> CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.UserID == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await dbContext.Users.Include("Role").FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<User> UpdateAsync(int id, User user)
        {
            var newuser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserID == id);
            if (newuser == null)
            {
                return null;
            }

            newuser.Name = user.Name;
            newuser.Email = user.Email;
            newuser.Phone = user.Phone;
            newuser.Password = user.Password;

            await dbContext.SaveChangesAsync();
            return newuser;
        }
        public async Task<User> DeleteAsync(int id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserID == id);
            if (user == null)
            {
                return null;
            }
            dbContext.Users.Remove(user);

            await dbContext.SaveChangesAsync();
            return user;
        }
    }
}
