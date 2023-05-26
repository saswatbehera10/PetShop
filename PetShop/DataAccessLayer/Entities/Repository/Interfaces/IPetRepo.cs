using PetShop.DataAccessLayer.Entities;

namespace PetShop.DataAccessLayer.Entities.Repository.Interfaces
{
    public interface IPetRepo
    {
        Task<Pet> CreateAsync(Pet pet);
        Task<List<Pet>> GetAllAsync();
        Task<Pet> GetByIdAsync(int id);
        Task<Pet> UpdateAsync(int id, Pet pet);
        Task<Pet> DeleteAsync(int id);
    }
}
