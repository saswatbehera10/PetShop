using PetShop.BusinessLogicLayer.DTO;
using PetShop.DataAccessLayer.Context;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;

namespace PetShop.DataAccessLayer.Entities.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly PetShopDbContext _context;
        public UserRepository(PetShopDbContext context)
        {
            _context = context;
        }
        
        public bool IsUnique(string Email)
        {
            var user = _context.LocalUsers.FirstOrDefault(x => x.Email == Email);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(UserLoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<LocalUser> Register(UserRegisterDTO registerDTO)
        {
            LocalUser user = new LocalUser();
            {
                user.Email = registerDTO.Email;
                user.Name = registerDTO.Name;
                user.Password = registerDTO.Password;
                user.Role = registerDTO.Role;
            };
            
            _context.LocalUsers.Add(user);
            await _context.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
