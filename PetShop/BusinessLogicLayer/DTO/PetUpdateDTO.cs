using System.ComponentModel.DataAnnotations;

namespace PetShop.BusinessLogicLayer.DTO
{
    public class PetUpdateDTO
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
        public string Status { get; set; }
    }
}
