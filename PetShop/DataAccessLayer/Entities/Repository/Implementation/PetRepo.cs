using Microsoft.EntityFrameworkCore;
using PetShop.DataAccessLayer.Context;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;

namespace PetShop.DataAccessLayer.Entities.Repository.Implementation
{
    public class PetRepo : IPetRepo
    {
        private readonly PetShopDbContext dbContext;

        public PetRepo(PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Pet> CreateAsync(Pet pet)
        {
            await dbContext.Pets.AddAsync(pet);
            await dbContext.SaveChangesAsync();
            return pet;
        }

        public async Task<List<Pet>> GetAllAsync()
        {
            return await dbContext.Pets.ToListAsync();
        }

        public async Task<Pet> GetByIdAsync(int id)
        {
            return await dbContext.Pets.FirstOrDefaultAsync(x => x.PetID == id);
        }
        public async Task<Pet> UpdateAsync(int id, Pet pet)
        {
            var newpet = await dbContext.Pets.FirstOrDefaultAsync(x => x.PetID == id);
            if (newpet == null)
            {
                return null;
            }

            newpet.Name = pet.Name;
            newpet.Species = pet.Species;
            newpet.Age = pet.Age;
            newpet.Price = pet.Price;
            newpet.ImgUrl = pet.ImgUrl;


            await dbContext.SaveChangesAsync();
            return newpet;
        }
        public async Task<Pet> DeleteAsync(int id)
        {
            var pet = await dbContext.Pets.FirstOrDefaultAsync(x => x.PetID == id);
            if (pet == null)
            {
                return null;
            }
            dbContext.Pets.Remove(pet);

            await dbContext.SaveChangesAsync();
            return pet;
        }

    }
}
