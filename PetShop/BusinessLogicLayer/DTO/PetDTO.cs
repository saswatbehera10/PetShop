using PetShop.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.BusinessLogicLayer.DTO
{
    public class PetDTO
    {
        [Key]
        public int PetID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
    }
}
