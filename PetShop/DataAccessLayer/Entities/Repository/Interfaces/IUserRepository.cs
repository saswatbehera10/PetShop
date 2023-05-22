using PetShop.BusinessLogicLayer.DTO;

namespace PetShop.DataAccessLayer.Entities.Repository.Interfaces
{
    public interface IUserRepository
    {
        bool IsUnique (string email);
        Task<LoginResponseDTO> Login(UserLoginDTO loginDTO);
        Task<LocalUser> Register(UserRegisterDTO registerDTO);
    }
}
